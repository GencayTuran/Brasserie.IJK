namespace Brasserie.IJK.Contracts.Orders
{
    public class OrderReceiptResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public decimal VatAmount { get; set; }


        public IReadOnlyCollection<OrderLineResponse> OrderLines { get; set; } = [];
    }
}