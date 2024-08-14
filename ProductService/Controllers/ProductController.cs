using Microsoft.AspNetCore.Mvc;
using ProductsService.DTOs.Requests;
using ProductsService.DTOs.Responses;
using ProductsService.Services;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpPost("createProduct")]
        public BaseResponse CreateProduct(CreateProductRequestDTO request)
        {
            return productService.CreateProduct(request);
        }
        [HttpGet("productList")]
        public BaseResponse ProductList()
        {
            return productService.ProductList();
        }

        [HttpGet("getProductById/{id}")]
        public BaseResponse GetProductById(int id)
        {
            return productService.GetProductById(id);
        }

        [HttpPut("updateProductById/{id}")]
        public BaseResponse UpdateProductById(int id, UpdateProductRequestDTO request)
        {
            return productService.UpdateProductById(id, request);
        }
        [HttpPut("updateProduct/{id},{storedQuantity}")]
        public BaseResponse UpdateProduct(long id,int storedQuantity )
        {
            return productService.UpdateProduct(id, storedQuantity);
        }
        [HttpDelete("deleteProductById/{id}")]
        public BaseResponse DeleteProductById(int id)
        {
            return productService.DeleteProductById(id);
        }
    }
}
