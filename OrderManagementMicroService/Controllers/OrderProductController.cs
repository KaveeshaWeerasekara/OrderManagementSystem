using Microsoft.AspNetCore.Mvc;
using OrderProductService.DTOs.Requests;
using OrderProductService.DTOs.Responses;
using OrderProductService.Services;

namespace OrderProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : Controller
    {
        private readonly IOrderProductService orderProductService;
        public OrderProductController(IOrderProductService orderProductService)
        {
            this.orderProductService = orderProductService;
        }
        [HttpPost("createOrderProduct")]
        public BaseResponse CreateOrderProduct(CreateOrderProductRequestDTO request)
        {
            return orderProductService.CreateOrderProduct(request);
        }
        [HttpGet("orderProductList")]
        public BaseResponse OrderProductList()
        {
            return orderProductService.OrderProductList();
        }

        [HttpGet("getOrderProductsById/{id}")]
        public BaseResponse GetOrderProductsById(int id)
        {
            return orderProductService.GetOrderProductsById(id);
        }

        [HttpPut("updateOrderProductsById/{id}")]
        public BaseResponse UpdateOrderProductsById(int id, UpdateOrderProductRequestDTO request)
        {
            return orderProductService.UpdateOrderProductsById(id, request);
        }
        [HttpDelete("deleteOrderProductsById/{id}")]
        public BaseResponse DeleteOrderProductsById(int id)
        {
            return orderProductService.DeleteOrderProductsById(id);
        }
    }
}
