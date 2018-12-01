using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        [HttpPost]
        public void Post([FromServices] IUserService userService)
        {
            userService.Add();
        }
    }
}