using Brasserie.IJK.Api.Mappers;
using Brasserie.IJK.Application.Interfaces;
using Brasserie.IJK.Contracts.Orders;
using Brasserie.IJK.Domain.Orders;
using Brasserie.IJK.Domain.Products;
using Brasserie.IJK.Domain.Shared;
using Brasserie.IJK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.IJK.Application.Services
{
    public class OrderService(BrasserieDbContext dbContext) : IOrderService
    {
        private readonly BrasserieDbContext _dbContext = dbContext;

        public async Task<OrderResponse> CreateAsync(CreateOrderRequest request, ICollection<Product> products)
        {
            var newOrder = OrderMapper.ToDomain(request);

            foreach (var line in request.OrderLines)
            {
                var currentProduct = products.First(p => p.Id == line.ProductId);
                newOrder.AddOrderLine(currentProduct, line.Quantity);
            }

            newOrder.SetStatus(OrderStatus.Pending);
            newOrder.SetOrderDate();

            var createdOrder = (await _dbContext.AddAsync(newOrder)).Entity;

            await _dbContext.SaveChangesAsync();

            return OrderMapper.ToResponse(createdOrder);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order is null)
                return 0;

            _dbContext.Orders.Remove(order);
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<OrderReceiptResponse?> GetReceiptAsync(int id)
        {
            var order = await _dbContext.Orders
                .Include(x => x.OrderLines)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
                return null;

            var subtotal = order.CalculateSubtotal();
            var vatAmount = order.CalculateVatAmount(VatRate.High);
            var total = subtotal + vatAmount;

            return OrderMapper.ToResponse(order, subtotal, total, vatAmount);
        }

        public async Task<OrderResponse?> UpdateAsync(int id, UpdateOrderRequest request)
        {
            var order = await _dbContext.Orders
                .Include(x => x.OrderLines)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
                return null;

            OrderMapper.UpdateDomain(order, request);

            await _dbContext.SaveChangesAsync();
            return OrderMapper.ToResponse(order);
        }

    }
}
