using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Http.Abstractions.Serialization;

namespace Http.Abstractions
{
    public class HttpClientBase : IHttpClient
    {
        private readonly HttpClient _client;

        public ISerialize Serializer { get; set; }
            
        public HttpClientBase()
        {
            _client = new HttpClient();
            Serializer = new HttpClientSerialization();
        }

        public string BaseAddress
        {
            get => _client.BaseAddress.ToString();
            set
            {
                if (_client == null) return;
                if(value != null && Uri.CheckSchemeName(new Uri(value).Scheme))
                    _client.BaseAddress = new Uri(value);
            }
        }

        public virtual async Task<IHttpResult<T>> GetAsync<T>(string requestUri)
        {
            return await HandleHttp<T>(async () => await _client.GetAsync(requestUri));
        }
       
        public virtual async Task<IHttpResult> PostAsync<TContent>(string requestUri, TContent content)
        {
            return await HandleHttp(async () =>
            {
                var serialized = Serializer.SerializeContent(content);
                return await _client.PostAsync(requestUri, new StringContent(serialized));
            });
        }

        public virtual async Task<IHttpResult> PutAsync<TContent>(string requestUri, TContent content)
        {
            return await HandleHttp(async () =>
            {
                var serialized = Serializer.SerializeContent(content);
                return await _client.PutAsync(requestUri, new StringContent(serialized));
            });            
        }

        public virtual async Task<IHttpResult> PatchAsync<TContent>(string requestUri, TContent content)
        {
            return await HandleHttp(async () =>
            {
                var serialized = Serializer.SerializeContent(content);
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
                {
                    Content = new StringContent(serialized)
                };

                return await _client.SendAsync(request);
            });           
        }

        public virtual async Task<IHttpResult> DeleteAsync(string requestUri)
        {
            return await HandleHttp(async () => await _client.DeleteAsync(requestUri));
        }
                   
        protected virtual async Task<IHttpResult> HandleHttp(Func<Task<HttpResponseMessage>> func)
        {
            try
            {
                var httpTask = await func();
                var result = new HttpResultBase
                {
                    StatusCode = (int)httpTask.StatusCode,
                    IsSuccessfull = httpTask.IsSuccessStatusCode
                };

                if (httpTask.IsSuccessStatusCode)
                    result.Content = await httpTask.Content.ReadAsStringAsync();

                return result;
            }
            catch(Exception ex)
            {
                throw new HttpClientException($"{nameof(HandleHttp)} caught an unexpected excpetion", ex);
            }
        }

        protected virtual async Task<IHttpResult<T>> HandleHttp<T>(Func<Task<HttpResponseMessage>> func)
        {
            var httpResult = await HandleHttp(func);
            var result = new HttpResultBase<T>
            {
                IsSuccessfull = httpResult.IsSuccessfull,
                StatusCode = httpResult.StatusCode
            };
            if (httpResult.IsSuccessfull)
                result.Content = Serializer.DeserializeContent<T>(httpResult.Content.ToString());

            return result;
        }

    }
}
