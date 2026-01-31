using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using CrudOrders.Entities;
using CrudOrders.Models;
using CrudOrders.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudOrders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        // Додавання нового товару
        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder(AddOrderDto request)
        {
            if(!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var addOrder = await orderService.AddOrder(request);
            return Ok(addOrder);
        }

        // Зміна статусу доставки товару
        [HttpPatch("{referenceId}/status")]
        public async Task<ActionResult> ChangeStatus(
            string referenceId, 
            [FromBody] ChangeStatusOrderDto request)
        {
            await orderService.ChangeOrderStatus(referenceId, request);
            return Ok();
        }

        // Редагування Опису товару.
        [HttpPut("{referenceId}")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(
            string referenceId, 
            [FromBody] UpdateOrderDto request)
        {
            var updateOrder =  await orderService.UpdateOrder(referenceId, request);
            return Ok(updateOrder); 
        }

        // Переглянути всі товари
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {

            return await orderService.GetAllOrders();
        }

        // Переглянути товар по Id
        [HttpGet("{referenceId}")]
        public async Task<ActionResult<OrderDto>> GetById(string referenceId)
        {

            var order = await orderService.GetByIdOrder(referenceId);
            return Ok(order);
        }

        // Видалення
        [HttpDelete("{referenceId}")]
        public async Task<ActionResult> Delete(string referenceId)
        {
            await orderService.DeleteOrder(referenceId);
            return NoContent();
        }
    }
}