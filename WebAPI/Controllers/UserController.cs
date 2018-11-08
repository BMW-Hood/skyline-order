using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:BaseController
    {

        [HttpPost]
        public void Post([FromServices] IUserService userService)
        {
            userService.Add();
        }
        
    }
}
