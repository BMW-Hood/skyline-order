using System;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure
{
    public static class EncryptHelper
    {
        public static string Md5(string str)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}