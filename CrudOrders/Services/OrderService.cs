using CrudOrders.Data;
using CrudOrders.Entities;
using CrudOrders.Models;
using Microsoft.EntityFrameworkCore;


namespace CrudOrders.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        // Add
        public async Task<OrderEntity> AddOrder(AddOrderDto request)
        {
            if(string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentException("Title error");   

            if (await _context.Orders.AnyAsync(o => o.Name == request.Name)) 
                throw new InvalidOperationException("Order already exists");

            var newOrders = new OrderEntity
            {
                Name = request.Name,
                Amount = request.Amount,
                orderType = request.orderType
            };

            _context.Orders.Add(newOrders);

            await _context.SaveChangesAsync();

            return newOrders;
        }     

        // Update
        public async Task<OrderEntity> UpdateAmount(AddAmountOrderDto request)
        {
            var order = await _context.Orders
                .FindAsync(request.Id);

            if(order == null) throw new KeyNotFoundException($"$Todo with ID '{request.Id}' not found"); // якщо такого id немає

            order.Amount = request.Amount;

            await _context.SaveChangesAsync(); // Зберігання

            return order;            
        }   

        public async Task ChangeOrderStatus(ChangeStatusOrderDto request)
        {
            var order = await _context.Orders.
                FindAsync(request.Id);

            if(order == null) throw new KeyNotFoundException($"$Todo with ID '{request.Id}' not found");

            // Перевірка: ящко із поточного статусу не можна перейти на новий, тоді викликаємо помилку
            if(!OrderStatusTransitions.CanMoveTo(order.orderStatus, request.Status))
                throw new InvalidOperationException(
                    $"Cannot change status from {order.orderStatus} to {request.Status}"
                ); 

            order.orderStatus = request.Status;

            await _context.SaveChangesAsync();
        }
    }
}