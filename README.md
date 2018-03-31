# HttpClient.Abstractions


**Nuget Package**: https://www.nuget.org/packages/HttpClient.Abstractions/

**HttpClient.Abstractions** is a lightweight package providing a complete abstraction over System.Net.Http.HttpClient. The package consists of: 

  - Abstraction layer supporting async generic Get/Post/Put/Patch/Delete operations.
  - Basic implementation of the interface.

# Examples

```csharp
public class SomeClass
{
    public string BaseUrl { get; set; }
    private IHttpClient _client;
    
    [importing constructor]
    public SomeClass(IHttpClient client) {
        _client = client;
        client.BaseAddress = BaseUrl;
    }
    
    //Get   
    public async IEnumerable<CustomrerDto> GetCustomersAsync() {
        return await _client.GetAsync<IEnumerable<CustomerDto>>("customers");
    }
	
	//Post
    public async void PostCustomerAsync(CustomerDto customer) {
        var result = await _client.PostAsync<CustomerDto,object>("customers",customer);
        //handle result...
    }
}
```