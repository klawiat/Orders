namespace Orders.Domain.Enums
{
    public enum Status
    {
        New,
        AwaitingPayment,
        Paid,
        SentForDelivery,
        Delivered,
        Completed
    }
}
