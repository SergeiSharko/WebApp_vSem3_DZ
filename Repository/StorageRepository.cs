using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApp_vSem3.Abstraction;
using WebApp_vSem3.Data;
using WebApp_vSem3.DTO;
using WebApp_vSem3.Models;

namespace WebApp_vSem3.Repository
{
    public class StorageRepository(WebAppContext _webAppCtx, IMapper _mapper, IMemoryCache _memoryCache) : IStorageRepository
    {
        public int AddStorage(StorageModel storageModel)
        {
            using (_webAppCtx)
            {
                if (_webAppCtx.Storages.Any(p => p.ProductId == storageModel.ProductId))
                    throw new Exception("Уже есть хранилище для данного товара");

                var entity = _mapper.Map<Storage>(storageModel);
                _webAppCtx.Storages.Add(entity);
                _webAppCtx.SaveChanges();
                _memoryCache.Remove("storages");
                return entity.Id;
            }
        }

        public void DeleteStorage(int id)
        {
            using (_webAppCtx)
            {
                var storage = _webAppCtx.Storages.Find(id);
                if (storage == null)
                    throw new Exception($"Нет хранилища с id = {id}");

                _webAppCtx.Storages.Remove(storage);
                _webAppCtx.SaveChanges();
                _memoryCache.Remove("storages");
            }
        }

        public IEnumerable<StorageModel> GetAllStorages()
        {
            if (_memoryCache.TryGetValue("storages", out List<StorageModel>? listStorages))
            {
                return listStorages!;
            }
            else
            {
                using (_webAppCtx)
                {
                    listStorages = _webAppCtx.Storages.Select(_mapper.Map<StorageModel>).ToList();
                    _memoryCache.Set("storages", listStorages, TimeSpan.FromMinutes(30));
                    return listStorages;
                }
            }
        }
    }
}
