namespace Brasserie.IJK.Api.Domain.Orders
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