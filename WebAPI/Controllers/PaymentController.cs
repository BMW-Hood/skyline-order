using Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private IPaymentService _paymentService;
        private ITracer _tracer;
        private ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ITracer tracer, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _tracer = tracer;
            _logger = logger;
        }

        /// <summary>
        /// 查询支付数据
        /// </summary>
        /// <param name="paymentQuery"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]PaymentQueryRequest paymentQuery)
        {
            var response = _paymentService.QueryPayments(paymentQuery);
            return Ok(response);
        }

        /// <summary>
        /// 获取支付记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            _logger.LogInformation("Index page says hello");
            var response = _paymentService.GetPayments(pageIndex, pageSize);
            return Ok(response);
        }
    }
}