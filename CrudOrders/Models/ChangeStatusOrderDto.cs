using CrudOrders.Entities;

namespace CrudOrders.Models
{
    public class ChangeStatusOrderDto
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}