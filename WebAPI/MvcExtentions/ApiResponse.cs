using Common;
using Common.CustomExceptions;

namespace WebAPI.MvcExtentions
{
    public class ApiResponse<T>
    {
        public Code code { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public static ApiResponse<T> SUCCESS(T data) => new ApiResponse<T> { code = Code.SUCCESS, message = "success", data = data };

        public static ApiResponse<T> ERROR(T data, Code code, string message) => new ApiResponse<T> { code = code, message = message, data = data };

        public static ApiResponse<T> BUSINESSERROR(BusinessException exception, T data) => new ApiResponse<T> { code = exception.Descriptor.ErrorCode, message = exception.Descriptor.ErrorMessage, data = data };
    }
}