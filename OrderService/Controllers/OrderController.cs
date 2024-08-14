using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs.Requests;
using OrderService.DTOs.Responses;
using OrderService.Interservice.OrderProductService;
using OrderService.Services;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
      
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
            
        }

        [HttpPost("createOrder")]
        public Task<BaseResponse> CreateOrder(CreateOrderRequestDTO request)
        {
            return orderService.CreateOrder(request);
        }
        [HttpGet("orderList")]
        public BaseResponse OrderList()
        {
            return orderService.OrderList();
        }

        [HttpGet("getOrderById/{id}")]
        public BaseResponse GetOrderById(int id)
        {
            return orderService.GetOrderById(id);
        }
        [HttpDelete("deleteOrderById/{id}")]
        public BaseResponse DeleteOrderById(int id)
        {
            return orderService.DeleteOrderById(id);
        }
    }
}
