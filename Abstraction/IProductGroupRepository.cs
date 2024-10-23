using WebApp_vSem3.DTO;

namespace WebApp_vSem3.Abstraction
{
    public interface IProductGroupRepository
    {
        IEnumerable<ProductGroupModel> GetAllProductGroups();
        int AddProductGroup(ProductGroupModel productGroupModel);
        void DeleteProductGroup(int id);
    }
}
