using CrudOrders.Entities;
using CrudOrders.Models;

namespace CrudOrders.Services
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(AddOrderDto request, Guid userId);

        Task<OrderDto> UpdateOrder(string referenceId, UpdateOrderDto request);
        Task ChangeOrderStatus(string referenceId, ChangeStatusOrderDto request);

        Task<List<OrderDto>> GetAllOrders();
        Task<OrderDto> GetByIdOrder(string referenceId);

        Task DeleteOrder(string referenceId, Guid currentUserId, bool isAdmin);
    }
}