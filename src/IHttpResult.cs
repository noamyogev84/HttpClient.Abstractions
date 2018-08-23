using System;

namespace Http.Abstractions
{
    public interface IHttpGeneralResult
    {
        int? StatusCode { get; set; }
        bool IsSuccessfull { get; set; }
    }

    public interface IHttpResult : IHttpGeneralResult
    {
        object Content { get; set; }        
    }

    public interface IHttpResult<T> : IHttpGeneralResult
    {
        T Content { get; set; }        
    }
}

