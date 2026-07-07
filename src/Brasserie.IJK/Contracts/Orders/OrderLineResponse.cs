namespace Brasserie.IJK.Contracts.Orders
{
    public class OrderLineResponse
    {
        public int ProductId { get; init; }

        public string ProductName { get; init; } = string.Empty;

        public int Quantity { get; init; }

        public decimal UnitPrice { get; init; }

        public decimal LineTotal { get; init; }
    }
}
