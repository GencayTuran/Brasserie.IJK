using Brasserie.IJK.Contracts.Orders;
using Brasserie.IJK.Domain.Orders;

namespace Brasserie.IJK.Api.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponse ToResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                TotalPrice = order.CalculateSubtotal(),

                OrderLines = [.. 
                    order.OrderLines.Select(ToResponse)
                ]
            };
        }

        public static OrderLineResponse ToResponse(OrderLine orderLine)
        {
            return new OrderLineResponse
            {
                ProductId = orderLine.ProductId,
                ProductName = orderLine.Product.Name,
                Quantity = orderLine.Quantity,
                UnitPrice = orderLine.UnitPrice,
                LineTotal = orderLine.CalculateLineTotal()
            };
        }

        public static Order ToDomain(CreateOrderRequest request)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
            };

            return order;
        }

        public static OrderReceiptResponse ToResponse(Order order, decimal subtotal, decimal total, decimal vatAmount)
        {
            return new OrderReceiptResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Subtotal = subtotal,
                VatAmount = vatAmount,
                Total = total,

                OrderLines = [..
                    order.OrderLines.Select(ToResponse)
                ]
            };
        }

        internal static void UpdateDomain(Order order, UpdateOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
