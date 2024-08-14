using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;
using OrderManagement.Service.ProductService;
using OrderManagement.Service.UserService;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService) 
        { 
        this.productService = productService;
        }

        [HttpPost("save")]
        public BaseResponse CreateProduct(CreateProductRequest request)
        {
            return productService.CreateProduct(request);
        }
        [HttpGet("list")]
        public BaseResponse ProductList()
        {
            return productService.ProductList();
        }

        [HttpGet("{id}")]
        public BaseResponse GetProductById(int id)
        {
            return productService.GetProductById(id);
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateProductById(int id,UpdateProductRequest request)
        {
            return productService.UpdateProductById(id,request);
        }
        [HttpDelete("{id}")]
        public BaseResponse DeleteProductById(int id)
        {
            return productService.DeleteProductById(id);
        }
    }
}
