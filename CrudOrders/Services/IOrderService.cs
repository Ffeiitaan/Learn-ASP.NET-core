using CrudOrders.Entities;
using CrudOrders.Models;

namespace CrudOrders.Services
{
    public interface IOrderService
    {
        Task<OrderEntity> AddOrder(AddOrderDto reqyest);
        Task<OrderEntity> UpdateAmount(AddAmountOrderDto request);
        Task ChangeOrderStatus(ChangeStatusOrderDto request);
    }
}