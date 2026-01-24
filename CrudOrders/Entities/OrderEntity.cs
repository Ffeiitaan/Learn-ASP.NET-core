namespace CrudOrders.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; } = 1;
        public OrderType orderType { get; set; }
        public OrderStatus orderStatus { get; set; } = OrderStatus.Created;

    }

    public enum OrderStatus
    {
        Created = 0,
        Paid = 1,
        InProgress = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }

    public enum OrderType
    {
        techno, 
        eat,
    }


    // Правила статусу.
    public static class OrderStatusTransitions
    {

        // Правила, ключ це поточний статус, значення це статуси на які дозволено перейти із дозволеного
        private static readonly Dictionary<OrderStatus, OrderStatus[]> _allowedTransitions =
            new()
            {
                { OrderStatus.Created,  new[]{ OrderStatus.Paid, OrderStatus.Cancelled}},
                { OrderStatus.Paid,  new[]{ OrderStatus.InProgress, OrderStatus.Cancelled}},
                { OrderStatus.InProgress,  new[]{ OrderStatus.Shipped}},
                { OrderStatus.Shipped,  new[]{ OrderStatus.Delivered}},
                { OrderStatus.Delivered,  Array.Empty<OrderStatus>()},
                { OrderStatus.Cancelled,  Array.Empty<OrderStatus>()}
            };

        // Заміна старого статусу на новий.
        public static bool CanMoveTo(OrderStatus current, OrderStatus next)
        {
            return _allowedTransitions.TryGetValue(current, out var allowed) && allowed.Contains(next);
        }
    }
}