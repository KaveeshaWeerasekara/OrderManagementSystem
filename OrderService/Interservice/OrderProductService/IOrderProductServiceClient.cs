using OrderService.Interservice.OrderProductService.OrderProductDTOs;
//using OrderService.Interservice.OrderService.DTOs;

namespace OrderService.Interservice.OrderProductService
{
    public interface IOrderProductServiceClient
    {
        // Task<bool> IsValidUserAsync(int userId);
        Task<OrderProductDTO> GetOrderProductByIdAsync();
    }
}
