using WebApp_vSem3.DTO;

namespace WebApp_vSem3.Abstraction
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetAllProducts();
        int AddProduct(ProductModel productModel);
        void DeleteProduct(int id);
        (byte[] Content, string FileName) GetProductsCsv();
    }
}
