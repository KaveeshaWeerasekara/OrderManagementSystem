using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;

namespace OrderManagement.Service.ProductService
{
    public interface IOrderService
    {
        BaseResponse CreateOrder(CreateOrderRequest request);
        BaseResponse OrderList();
        BaseResponse GetOrderById(int id);
      /*  BaseResponse UpdateOrderById(int id, UpdateOrderRequest request);*/
        BaseResponse DeleteOrderById(int id);
    }
}
