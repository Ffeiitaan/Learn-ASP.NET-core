using CrudOrders.Models;
using CrudOrders.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudOrders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost("add-order")]
        public async Task<ActionResult<OrderService>> AddOrder(AddOrderDto request)
        {
            try
            {
                var addOrder = await orderService.AddOrder(request);

                return Ok(addOrder);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch(ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> ChangeStatus(ChangeStatusOrderDto request)
        {
            try
            {
                await orderService.ChangeOrderStatus(request);
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}