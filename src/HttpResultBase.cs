using System;

namespace Http.Abstractions
{
    public class HttpResultBase : IHttpResult
    {
        public object Content { get; set; }
        public int? StatusCode { get; set; }
        public bool IsSuccessfull { get; set; }
    }

    public class HttpResultBase<T> : IHttpResult<T>
    {
        public T Content { get; set; }
        public int? StatusCode { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
