using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;

namespace OrderManagement.Service.ProductService
{
    public interface IOrderProductService
    {
        BaseResponse CreateOrderProduct(CreateOrderProductRequest request);
        BaseResponse OrderProductList();
        BaseResponse GetOrderProductsById(int id);
        BaseResponse UpdateOrderProductsById(int id, UpdateOrderProductRequest request);
        BaseResponse DeleteOrderProductsById(int id);
        BaseResponse GetProductListByOrderID(int id);
        BaseResponse GetProductListByUserID(int id);
    }
}
