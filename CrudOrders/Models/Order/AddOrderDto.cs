using System.ComponentModel.DataAnnotations;
using CrudOrders.Entities;

namespace CrudOrders.Models
{
    public class AddOrderDto
    {
        [Required]
        public string Name { get; set;} = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Amount must be positive")]
        public int Amount { get; set; }

        [Required]
        public OrderCatagoty OrderCatagoty { get; set; }
    }
}