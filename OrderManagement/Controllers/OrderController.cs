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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService) 
        { 
        this.orderService = orderService;
        }

        [HttpPost("save")]
        public BaseResponse CreateOrder(CreateOrderRequest request)
        {
            return orderService.CreateOrder(request);
        }
        [HttpGet("list")]
        public BaseResponse OrderList()
        {
            return orderService.OrderList();
        }

        [HttpGet("{id}")]
        public BaseResponse GetOrderById(int id)
        {
            return orderService.GetOrderById(id);
        }

        /*[HttpPut("{id}")]
        public BaseResponse UpdateProductById(int id,UpdateProductRequest request)
        {
            return productService.UpdateProductById(id,request);
        }*/
        [HttpDelete("{id}")]
        public BaseResponse DeleteOrderById(int id)
        {
            return orderService.DeleteOrderById(id);
        }
    }
}
