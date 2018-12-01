using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OK");            
        }
    }
}