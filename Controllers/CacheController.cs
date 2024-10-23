using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApp_vSem3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController(IMemoryCache _memoryCache) : ControllerBase
    {
        [HttpGet("cache_statistics")]
        public ActionResult GetCacheStatisticsUrl()
        {
            SaveCacheStatisticsToFile();

            var stats = _memoryCache.GetCurrentStatistics();
            var statisticsInfo = new
            {
                Entries = stats!.CurrentEntryCount,
                Size = stats.CurrentEstimatedSize,
                Hits = stats.TotalHits,
                Misses = stats.TotalMisses,
                FileUrl = Url.Content("https://" + Request.Host.ToString() + "/static/cacheStatistics.txt")
            };
            return Ok(statisticsInfo);
        }

        [HttpGet("download_cache_statistics")]
        public FileContentResult DownloadCacheStatisticsFile()
        {
            var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "cacheStatistics.txt");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/plain", "cacheStatistics.txt");
        }

        private void SaveCacheStatisticsToFile()
        {
            var stats = _memoryCache.GetCurrentStatistics();
            var content = $"Entries: {stats!.CurrentEntryCount}\nSize: {stats.CurrentEstimatedSize}\nHits: {stats.TotalHits}\nMisses: {stats.TotalMisses}";
            var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "cacheStatistics.txt");

            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath)!);
            System.IO.File.WriteAllText(filePath, content);
        }
    }
}
