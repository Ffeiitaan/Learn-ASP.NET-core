using CrudOrders.Entities;

namespace CrudOrders.Models
{
    public class AddOrderDto
    {
        public string Name { get; set;} = string.Empty;
        public int Amount { get; set; }
        public OrderType orderType { get; set; }
    }
}