using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();            
        }
    }
}