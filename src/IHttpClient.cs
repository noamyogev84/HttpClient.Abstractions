using System.Threading.Tasks;


namespace Http.Abstractions
{
    public interface IHttpClient
    {
        Task<IHttpResult<TResult>> GetAsync<TResult>(string requestUri);
        Task<IHttpResult> PostAsync<TContent>(string requestUri, TContent content);
        Task<IHttpResult> PutAsync<TContent>(string requestUri, TContent content);
        Task<IHttpResult> PatchAsync<TContent>(string requestUri, TContent content);
        Task<IHttpResult> DeleteAsync(string requestUri);
    }
}
