using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace Http.Abstractions
{
    public interface IHttpClient
    {
        Task<TResult> GetAsync<TResult>(string requestUri);
        Task<TResult> PostAsync<TContent, TResult>(string requestUri, TContent content);
        Task<TResult> PutAsync<TContent, TResult>(string requestUri, TContent content);
        Task<TResult> PatchAsync<TContent, TResult>(string requestUri, TContent content);
        Task<TResult> DeleteAsync<TResult>(string requestUri);
    }
}
