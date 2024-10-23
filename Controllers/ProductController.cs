using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebApp_vSem3.Abstraction;
using WebApp_vSem3.DTO;
using WebApp_vSem3.Models;

namespace WebApp_vSem3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductRepository _productRepository) : ControllerBase
    {

        [HttpPost("add_product")]
        public ActionResult<int> AddProduct(ProductModel productModel)
        {
            try
            {
                var id = _productRepository.AddProduct(productModel);
                return Ok(id);
            }
            catch (Exception)
            {
                return StatusCode(409);
            }

        }

        [HttpGet("get_products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_productRepository.GetAllProducts());
        }

        [HttpDelete("delete_product_on_id")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(409);
            }
        }

        [HttpGet("get_url_on_products_csv")]
        public FileContentResult GetProductsCsvUrl()
        {
            var (content, fileName) = _productRepository.GetProductsCsv();
            var utf8BomContent = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(content));
            return new FileContentResult(utf8BomContent, "text/csv")
            {
                FileDownloadName = fileName
            };

        }
    }
}
