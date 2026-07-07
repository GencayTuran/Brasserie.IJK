using Brasserie.IJK.Contracts.Orders;
using Brasserie.IJK.Domain.Products;

namespace Brasserie.IJK.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(CreateOrderRequest request, ICollection<Product> products);
        Task<OrderResponse> UpdateAsync(int id, UpdateOrderRequest request);
        Task<int> DeleteAsync(int id);
        Task<OrderReceiptResponse?> GetReceiptAsync(int id);
    }
}
