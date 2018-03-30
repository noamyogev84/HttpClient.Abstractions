using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Http.Abstractions
{
    public class HttpClientException : Exception
    {
        public HttpClientException(string message, Exception innerException):base(message,innerException)
        {
            
        }
    }
}
