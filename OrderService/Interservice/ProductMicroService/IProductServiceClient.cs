using OrderService.Interservice.ProductMicroService.DTOs;

namespace OrderService.Interservice.ProductMicroService
{
    public interface IProductServiceClient
    {
        Task<ProductDTO?> GetProductById(long productId);
        Task<string?> UpdateProductById(long productID, int storedQuantity);
    }
}
