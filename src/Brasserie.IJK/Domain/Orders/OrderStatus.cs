namespace Brasserie.IJK.Domain.Orders
{
    public enum OrderStatus
    {
        Pending,
        Preparing,
        Ready,
        Served,
        Paid,
        Cancelled
    }
}