using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net;
using WebAPI.CustomExceptions;
using WebAPI.MvcExtentions;

namespace WebAPI.Controllers
{
    public class BaseController : ControllerBase, IActionFilter
    {
        public string UsId { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var key = "x-btcapi-usid";
            StringValues values = new StringValues("");
            if (context.HttpContext.Request.Headers.TryGetValue(key, out values))
            {
                var usid = values.FirstOrDefault();
                Guid result;
                if (!string.IsNullOrEmpty(usid) && Guid.TryParse(usid, out result))
                {
                    this.UsId = usid;
                }
            }
            if (string.IsNullOrEmpty(UsId))
            {
                var data = ApiResponse<string>.ERROR("用户没有登陆", Code.NOTLOGIN, "");
                context.Result = Result(data, HttpStatusCode.Forbidden);
            }
        }

        public JsonResult Result(ApiResponse<string> apiResponse, HttpStatusCode statusCode)
        {
            var result = new JsonResult(apiResponse);
            result.StatusCode = (int)statusCode;
            return result;
        }

        public override OkObjectResult Ok(object value)
        {
            var result = ApiResponse<object>.SUCCESS(value);
            return base.Ok(result);
        }

        public JsonResult Error(Exception exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            object response = null;
            //解析错误
            if (exception != null && exception is BusinessException)
            {
                var businessException = exception as BusinessException;
                response = ApiResponse<string>.BUSINESSERROR(businessException, "");
            }
            //未知异常
            else
            {
                response = ApiResponse<string>.ERROR(exception.Message, Code.UNKNOW, "未知异常");
            }

            return new JsonResult(response);
        }

        public BadRequestObjectResult BadRequest(object error, string message)
        {
            var result = ApiResponse<object>.ERROR(error, Code.BADREQUEST, message);
            return base.BadRequest(result);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ExceptionHandled&&context.Exception!=null)
            {
                

                context.Result = Error(context.Exception);
            }
        }
    }
}