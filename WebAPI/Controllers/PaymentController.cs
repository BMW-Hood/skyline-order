using Contracts.Dtos;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.MvcExtentions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]PaymentQueryRequest paymentQuery)
        {
            var response = _paymentService.QueryPayments(paymentQuery);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var response = _paymentService.GetPayments(pageIndex, pageSize);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentDto payment)
        {

            return Ok();
        }


    }
}
