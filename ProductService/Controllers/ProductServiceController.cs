using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Repository;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductServiceController : Controller
    {
        private readonly ProductRepository repository;

        public ProductServiceController(ProductRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public JsonResult GetAllProductsDetails()
        {
            List<Product> listOfProducts = repository.GetAllProducts();
            return Json(listOfProducts);
        }

        [HttpGet("{id}")]
        public JsonResult GetProductById(string id)
        {
            List<Product> listOfProducts = repository.GetAllProducts();
            Product product = listOfProducts.Find(p => p.ProductId == id);
            return Json(product);
        }

        [HttpPost]
        public JsonResult AddNewProduct(Product product)
        {
            return Json(repository.AddNewProduct(product));
        }

        [HttpPut]
        public JsonResult UpdateProduct(Product product)
        {
            int result = repository.UpdateProductDetails(product);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteProduct(string id)
        {
            bool result = repository.DeleteProduct(id);
            return Json(result);
        }
    }
}
