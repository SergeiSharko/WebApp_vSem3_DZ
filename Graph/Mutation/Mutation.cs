using WebApp_vSem3.Abstraction;
using WebApp_vSem3.DTO;

namespace WebApp_vSem3.Graph.Mutation
{
    public class Mutation
    {
        public int AddProduct(ProductModel productModel, [Service] IProductRepository productRepository) => productRepository.AddProduct(productModel);
        public int AddProductGroup(ProductGroupModel productGroupModel, [Service] IProductGroupRepository groupRepository) => groupRepository.AddProductGroup(productGroupModel);
        public int AddStorage(StorageModel storageModel, [Service] IStorageRepository storageRepository) => storageRepository.AddStorage(storageModel);
    }
}
