using WebApp_vSem3.Abstraction;
using WebApp_vSem3.DTO;

namespace WebApp_vSem3.Graph.Query
{
    public class Query()
    {
        public IEnumerable<ProductModel> GetProducts([Service] IProductRepository productRepository) => productRepository.GetAllProducts();
        public IEnumerable<ProductGroupModel> GetProductGroups([Service] IProductGroupRepository groupRepository) => groupRepository.GetAllProductGroups();
        public IEnumerable<StorageModel> GetStorages([Service] IStorageRepository storageRepository) => storageRepository.GetAllStorages();
    }
}
