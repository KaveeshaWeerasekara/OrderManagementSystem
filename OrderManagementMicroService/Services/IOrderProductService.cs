using OrderProductService.DTOs.Requests;
using OrderProductService.DTOs.Responses;

namespace OrderProductService.Services
{
    public interface IOrderProductService
    {
        BaseResponse CreateOrderProduct(CreateOrderProductRequestDTO request);
        BaseResponse OrderProductList();
        BaseResponse GetOrderProductsById(int id);
        BaseResponse UpdateOrderProductsById(int id, UpdateOrderProductRequestDTO request);
        BaseResponse DeleteOrderProductsById(int id);
    }
}
