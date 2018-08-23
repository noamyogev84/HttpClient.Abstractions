# HttpClient.Abstractions


**Nuget Package**: https://www.nuget.org/packages/HttpClient.Abstractions/

**HttpClient.Abstractions** is a lightweight package providing a complete abstraction over System.Net.Http.HttpClient. The package consists of: 

  - Abstraction layer supporting async generic Get/Post/Put/Patch/Delete operations.
  - Basic implementation of the interface.

# Examples

```csharp
public class HttpClientConsumer
{
    public string BaseUrl { get; set; }
    private IHttpClient _client;
    
    [importing constructor]
    public HttpClientConsumer(IHttpClient client) {
        _client = client;
        _client.BaseAddress = BaseUrl;
    }
    
    //Get   
    public async IEnumerable<CustomrerDto> GetCustomersAsync() {
        IHttpResult result = await _client.GetAsync<IEnumerable<CustomerDto>>("customers");
		return result.Content;
    }
	
	//Post
    public async void CreateCustomerAsync(CustomerDto customer) {
        IHttpResult result = await _client.PostAsync<CustomerDto>("customers",customer);
        //handle result...
    }
    
    //Put
    public async void ReplaceCustomerAsync(CustomerUpdateDto customer) {
        IHttpResult result = await _client.PutAsync<CustomerUpdateDto>($"customers/{customer.id}",customer);
    }
    
    //Patch
	public async void UpdateCustomerAsync(CustomerPatchDoc patchDoc) {
        IHttpResult result = await _client.PatchAsync<CustomerPatchDoc>($"customers/{patchDoc.id}", patchDoc);
    }
    
    //Delete
    public async void RemoveCustomerAsync(CustomerDto customer) {
        IHttpResult result = await _client.DeleteAsync($"customers/{customer.id}");
    }
}
```