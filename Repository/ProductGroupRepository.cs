using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApp_vSem3.Abstraction;
using WebApp_vSem3.Data;
using WebApp_vSem3.DTO;
using WebApp_vSem3.Models;

namespace WebApp_vSem3.Repository
{
    public class ProductGroupRepository(WebAppContext _webAppCtx, IMapper _mapper, IMemoryCache _memoryCache) : IProductGroupRepository
    {
        public int AddProductGroup(ProductGroupModel productGroupModel)
        {
            using (_webAppCtx)
            {
                if (_webAppCtx.ProductGroups.Any(p => p.Name == productGroupModel.Name))
                    throw new Exception("Уже есть категория с таким именем");

                var entity = _mapper.Map<ProductGroup>(productGroupModel);
                _webAppCtx.ProductGroups.Add(entity);
                _webAppCtx.SaveChanges();
                _memoryCache.Remove("productGroups");
                return entity.Id;
            }
        }


        public void DeleteProductGroup(int id)
        {
            using (_webAppCtx)
            {
                var productGroup = _webAppCtx.ProductGroups.Find(id);
                if (productGroup == null)
                    throw new Exception($"Нет категории с id = {id}");

                _webAppCtx.ProductGroups.Remove(productGroup);
                _webAppCtx.SaveChanges();
                _memoryCache.Remove("productGroups");
            }
        }


        public IEnumerable<ProductGroupModel> GetAllProductGroups()
        {
            if (_memoryCache.TryGetValue("productGroups", out List<ProductGroupModel>? listGroupModel))
            {
                return listGroupModel!;
            }
            else
            {
                using (_webAppCtx)
                {
                    listGroupModel = _webAppCtx.ProductGroups.Select(_mapper.Map<ProductGroupModel>).ToList();
                    _memoryCache.Set("productGroups", listGroupModel, TimeSpan.FromMinutes(30));
                    return listGroupModel;
                }
            }
        }
    }
}
