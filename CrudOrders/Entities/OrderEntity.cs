namespace CrudOrders.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public string ReferenceId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; } = 1;
        public OrderCatagoty OrderCatagory { get; set; }
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

    public enum OrderCatagoty
    {
        Food = 0, 
        Beverages = 1,
        Household = 2,
        Electronics = 3,
        Clothing = 4
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
                { OrderStatus.InProgress,  new[]{ OrderStatus.Shipped, OrderStatus.Cancelled}},
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