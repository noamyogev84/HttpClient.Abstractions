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
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync<TContent>(string requestUri, TContent content);
        Task<HttpResponseMessage> PutAsync<TContent>(string requestUri, TContent content);
        Task<HttpResponseMessage> PatchAsync<TContent>(string requestUri, TContent content);
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}
