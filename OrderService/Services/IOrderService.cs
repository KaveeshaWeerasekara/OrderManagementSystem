using OrderService.DTOs.Requests;
using OrderService.DTOs.Responses;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<BaseResponse> CreateOrder(CreateOrderRequestDTO request);
        BaseResponse OrderList();
        BaseResponse GetOrderById(int id);
        /*  BaseResponse UpdateOrderById(int id, UpdateOrderRequest request);*/
        BaseResponse DeleteOrderById(int id);
    }
}
