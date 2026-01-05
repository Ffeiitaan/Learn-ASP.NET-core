using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthTestProject.Authorization;

namespace AuthTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Read)]
        public IActionResult GetText()
        {
            return Ok(new List<object>
            {
                new
                {
                    Name = "Супер дупер срака",
                    Duration = TimeSpan.FromHours(2)
                },
                new
                {
                    Name = "Я хуєю із цієї дури",
                    Duration = TimeSpan.FromHours(4.32)
                }
            });
        }

        [HttpDelete]
        [Authorize(Policy = Permissions.Delete)]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}