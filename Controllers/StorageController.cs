using Microsoft.AspNetCore.Mvc;
using WebApp_vSem3.Abstraction;
using WebApp_vSem3.DTO;

namespace WebApp_vSem3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController(IStorageRepository _storageRepository) : ControllerBase
    {
        [HttpPost("add_storage")]
        public ActionResult AddProductGroup(StorageModel storageModel)
        {
            try
            {
                var id = _storageRepository.AddStorage(storageModel);
                return Ok(id);
            }
            catch (Exception)
            {
                return StatusCode(409);
            }
        }

        [HttpDelete("delete_group_on_id")]
        public ActionResult DeleteProductGroup(int id)
        {
            try
            {
                _storageRepository.DeleteStorage(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(409);
            }
        }

        [HttpGet("get_storages")]
        public ActionResult<IEnumerable<StorageModel>> GetAllGroups()
        {
            return Ok(_storageRepository.GetAllStorages());
        }
    }
}
