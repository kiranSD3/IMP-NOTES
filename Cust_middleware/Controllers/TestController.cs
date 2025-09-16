using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cust_middleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("divide")]
        public IActionResult DivideByZero()
        {
            int x = 10;
            int y = 2;
            int result = x / y;  // ❌ This will throw DivideByZeroException
            return Ok(result);
        }

        [HttpGet("normal")]
        public IActionResult Normal()
        {
            return Ok("This is working fine!");
        }
    }
}
