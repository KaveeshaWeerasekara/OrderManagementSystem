using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;

namespace OrderManagement.Service.ProductService
{
    public interface IProductService
    {
        BaseResponse CreateProduct(CreateProductRequest request);
        BaseResponse ProductList();
        BaseResponse GetProductById(int id);
        BaseResponse UpdateProductById(int id, UpdateProductRequest request);
        BaseResponse DeleteProductById(int id);
    }
}
