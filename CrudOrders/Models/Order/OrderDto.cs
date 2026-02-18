using CrudOrders.Entities;

namespace CrudOrders.Models;

public class OrderDto
{
        public string ReferenceId { get; set; } = string.Empty;
        public string Name { get; set;} = string.Empty;
        public int Amount { get; set; }
        public OrderCatagoty OrderCatagoty { get; set; }
}