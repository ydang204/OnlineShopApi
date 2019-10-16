using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OnlineShop.Common.Common
{
    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(HttpStatusCode statusCode, string message)
        {

        }
    }
}
