using System.Security.Claims;
using CrudOrders.Models;
using CrudOrders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudOrders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        // Додавання нового товару
        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder(AddOrderDto request)
        {
            if(!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var userid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var addOrder = await orderService.AddOrder(request, userid);
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
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");

            await orderService.DeleteOrder(referenceId, userId, isAdmin);
            return NoContent();
        }
    }
}