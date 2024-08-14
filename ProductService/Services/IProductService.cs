using ProductsService.DTOs.Requests;
using ProductsService.DTOs.Responses;

namespace ProductsService.Services
{
    public interface IProductService
    {
        BaseResponse CreateProduct(CreateProductRequestDTO request);
        BaseResponse ProductList();
        BaseResponse GetProductById(int id);
        BaseResponse UpdateProductById(int id, UpdateProductRequestDTO request);
        BaseResponse UpdateProduct(long productId, int storedQuantity);
        BaseResponse DeleteProductById(int id);
    }
}
