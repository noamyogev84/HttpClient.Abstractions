using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Http.Abstractions
{
    public class HttpClientBase : HttpClient, IHttpClient
    {
        public HttpClientBase(string baseAddress) : base()
        {
            BaseAddress = new Uri(baseAddress);
        }

        public HttpClientBase(string baseAddress, IEnumerable<MediaTypeWithQualityHeaderValue> headers) : this(baseAddress)
        {
            headers.ToList().ForEach(h => DefaultRequestHeaders.Accept.Add(h));
        }

        public virtual async Task<TResult> GetAsync<TResult>(string requestUri)
        {
            return await HandleHttp<TResult>(async () =>
            {
                return await GetAsync(requestUri);
            });
        }
        
        public virtual async Task<TResult> PostAsync<TContent, TResult>(string requestUri, TContent content)
        {
            return await HandleHttp<TResult>(async () =>
            {
                var serialized = JsonConvert.SerializeObject(content);
                return await PostAsync(requestUri, new StringContent(serialized));
            });
        }

        public virtual async Task<TResult> PutAsync<TContent, TResult>(string requestUri, TContent content)
        {
            return await HandleHttp<TResult>(async () =>
            {
                var serialized = JsonConvert.SerializeObject(content);
                return await PutAsync(requestUri, new StringContent(serialized));
            });            
        }

        public virtual async Task<TResult> PatchAsync<TContent, TResult>(string requestUri, TContent content)
        {
            return await HandleHttp<TResult>(async () =>
            {
                var serialized = JsonConvert.SerializeObject(content);
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
                {
                    Content = new StringContent(serialized)
                };

                return await SendAsync(request);
            });           
        }

        public virtual async Task<TResult> DeleteAsync<TResult>(string requestUri)
        {
            return await HandleHttp<TResult>(async () =>
            {
                return await DeleteAsync(requestUri);
            });
        }
                   
        protected virtual async Task<TResult> HandleHttp<TResult>(Func<Task<HttpResponseMessage>> func)
        {
            var result = default(TResult);
            try
            {
                var httpTask = await func();
                result = await httpTask.Content.ReadAsStringAsync()
                    .ContinueWith(t => JsonConvert.DeserializeObject<TResult>(t.Result));
            }
            catch(Exception ex)
            {
                //TODO: handle
            }
            return result;
        }


    }
}
