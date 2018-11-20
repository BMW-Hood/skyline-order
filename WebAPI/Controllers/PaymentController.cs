using Contracts.Dtos;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
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
        private ITracer _tracer;
        public PaymentController(IPaymentService paymentService,ITracer tracer)
        {
            _paymentService = paymentService;
            _tracer = tracer;
        }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentQueryRequest paymentQuery)
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



    }
}
