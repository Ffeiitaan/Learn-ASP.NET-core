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
        public async Task<OrderDto> AddOrder(AddOrderDto request)
        {
            if(string.IsNullOrWhiteSpace(request.Name)) 
                throw new ArgumentException(
                    "The Name field must be filled in."
                );   

            if (await _context.Orders.AnyAsync(o => o.Name == request.Name)) 
                throw new InvalidOperationException(
                    "Order already exists"
                );

            if(request.Amount < 0)
                throw new ArgumentException("Amount cannot be negative"); 

            var newOrder = new OrderEntity
            {
                Name = request.Name,
                Amount = request.Amount,
                OrderCatagory = request.OrderCatagoty,
                ReferenceId = Guid.NewGuid().ToString()
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            var orderResponse = new OrderDto
            {
                Name = newOrder.Name,
                Amount = newOrder.Amount,
                OrderCatagoty = newOrder.OrderCatagory,
                ReferenceId = newOrder.ReferenceId
            };

            return orderResponse;
        }     

        // Update
        public async Task<OrderDto> UpdateOrder(string referenceId, UpdateOrderDto request)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.ReferenceId == referenceId);

            if(order == null) 
                throw new KeyNotFoundException(
                    $"Order with ID '{referenceId}' not found"
                ); // якщо такого id немає

            // Exception якщо нове поле пусте
            if(string.IsNullOrWhiteSpace(request.Name)) 
                throw new ArgumentException(
                    "The Name field must be filled in."
                );

            if(request.Amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            order.Name = request.Name;
            order.OrderCatagory = request.OrderCatagoty;
            order.Amount = request.Amount;

            await _context.SaveChangesAsync(); // Зберігання

            return new OrderDto
            {
                ReferenceId = order.ReferenceId,
                Name = order.Name,
                Amount = order.Amount,
                OrderCatagoty = order.OrderCatagory
            };            
        }   

        public async Task ChangeOrderStatus(string referenceId, ChangeStatusOrderDto request)
        {
            var order = await _context.Orders.
                FirstOrDefaultAsync(o => o.ReferenceId == referenceId);

            if(order == null) 
            throw new KeyNotFoundException(
                $"Order with ID '{referenceId}' not found"
            );

            // Перевірка: ящко із поточного статусу не можна перейти на новий, тоді викликаємо помилку
            if(!OrderStatusTransitions.CanMoveTo(order.orderStatus, request.Status))
                throw new InvalidOperationException(
                    $"Cannot change status from {order.orderStatus} to {request.Status}"
                ); 

            order.orderStatus = request.Status;

            await _context.SaveChangesAsync();
        }

        // Viev Orders
        public async Task<List<OrderDto>> GetAllOrders()
        {
            return await _context.Orders
            .Select(o => new OrderDto
            {
                ReferenceId = o.ReferenceId,
                Name = o.Name,
                Amount = o.Amount,
                OrderCatagoty = o.OrderCatagory
            })
            .ToListAsync();
        }

        public async Task<OrderDto> GetByIdOrder(string referenceId)
        {
            var order = await _context.Orders
                .Where(o => o.ReferenceId == referenceId)
                .Select(o => new OrderDto
                {
                    ReferenceId = o.ReferenceId,
                    Name = o.Name,
                    Amount = o.Amount,
                    OrderCatagoty = o.OrderCatagory
                }).FirstOrDefaultAsync();

            if(order == null) throw new KeyNotFoundException($"$Order with ID '{referenceId}' not found");

            return order;
        }

        // Видалення
        public async Task DeleteOrder(string referenceId)
        {
            var orderDelete = await _context.Orders
                .FirstOrDefaultAsync(o => o.ReferenceId == referenceId);

            if(orderDelete == null)
                throw new KeyNotFoundException(
                $"Order with ID '{referenceId}' not found"
            );

            _context.Orders.Remove(orderDelete);

            await _context.SaveChangesAsync();
        }
    }
}