using System;

namespace Common.CustomExceptions
{
    public class BusinessException : Exception
    {
        public ErrorDescriptor Descriptor { get; }

        public BusinessException(ErrorDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public class ErrorDescriptor
        {
            public Code ErrorCode { get; }
            public string ErrorMessage { get; }

            public ErrorDescriptor(Code code, string message)
            {
                ErrorCode = code;
                ErrorMessage = message;
            }

            //创建自定义的异常解析
            public static ErrorDescriptor USER_NOT_LOGIN = new ErrorDescriptor(Code.NOTLOGIN, "user not login");
        }
    }
}