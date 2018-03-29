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
    public class HttpClientBase : IHttpClient
    {
        private static HttpClient _client;

        public virtual async Task<HttpResponseMessage> GetAsync(string requestUri) => await _client.GetAsync(requestUri);
        
        public virtual async Task<HttpResponseMessage> PostAsync<TContent>(string requestUri, TContent content)
        {
            var serialized = JsonConvert.SerializeObject(content);
            return await _client.PostAsync(requestUri, new StringContent(serialized));
        }

        public virtual async Task<HttpResponseMessage> PutAsync<TContent>(string requestUri, TContent content)
        {
            var serialized = JsonConvert.SerializeObject(content);
            return await _client.PutAsync(requestUri, new StringContent(serialized));
        }

        public virtual async Task<HttpResponseMessage> PatchAsync<TContent>(string requestUri, TContent content)
        {
            var serialized = JsonConvert.SerializeObject(content);
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = new StringContent(serialized)
            };
            
            return await _client.SendAsync(request);
        }

        public virtual async Task<HttpResponseMessage> DeleteAsync(string requestUri) => await _client.DeleteAsync(requestUri);

        public HttpClientBase(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }

        public HttpClientBase(string baseAddress, IEnumerable<MediaTypeWithQualityHeaderValue> headers) : this(baseAddress)
        {
            headers.ToList().ForEach(h => _client.DefaultRequestHeaders.Accept.Add(h));
        }


    }
}
