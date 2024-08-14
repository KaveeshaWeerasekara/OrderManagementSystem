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
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService orderProductService;
        public OrderProductController(IOrderProductService orderProductService) 
        { 
        this.orderProductService = orderProductService;
        }

        [HttpPost("save")]
        public BaseResponse CreateOrderProduct(CreateOrderProductRequest request)
        {
            return orderProductService.CreateOrderProduct(request);
        }
        [HttpGet("list-products")]
        public BaseResponse OrderProductList()
        {
            return orderProductService.OrderProductList();
        }

        [HttpGet("orderProducts/{id}")]
        public BaseResponse GetOrderProductsById(int id)
        {
            return orderProductService.GetOrderProductsById(id);
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateOrderProductsById(int id, UpdateOrderProductRequest request)
        {
            return orderProductService.UpdateOrderProductsById(id, request);
        }
        [HttpDelete("{id}")]
        public BaseResponse DeleteOrderProductsById(int id)
        {
            return orderProductService.DeleteOrderProductsById(id);
        }
        [HttpGet("productList/{id}")]
        public BaseResponse GetProductListByOrderID(int id)
        {
            return orderProductService.GetProductListByOrderID(id);
        }
        [HttpGet("list/{id}")]
        public BaseResponse GetProductListByUserID(int id)
        {
            return orderProductService.GetProductListByUserID(id);
        }
    }
}
