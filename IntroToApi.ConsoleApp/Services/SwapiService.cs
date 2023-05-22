
// A service is a place to group similar topic logic
using System.Net.Http.Json;

public class SwapiService
{
    // private field
    private readonly HttpClient _httpClient = new(); // new HttpClient();

    // async method
    // async methods use the Task type as its return type
    public async Task<Person?> GetPersonAsync(string url)
    {
        // GET Request -> get the response from the given URL
        HttpResponseMessage res = await _httpClient.GetAsync(url);

        if (res.IsSuccessStatusCode)
        {
            // Request was a success
            Person? person = await res.Content.ReadFromJsonAsync<Person>();
            return person;
        }

        // Request was not a success
        return null;
    }

    public async Task<Vehicle?> GetVehicleAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);

        // Ternary logic operator
        Vehicle? result = response.IsSuccessStatusCode              // Condition
            ? await response.Content.ReadFromJsonAsync<Vehicle>()   // True result
            : null;                                                 // False result

        return result;
    }

    // Generic method that takes in a type parameter called T
    // Returns a value of type T that is nullable
    // This has a constraint where it only accepts types for T that are classes
    public async Task<T?> GetAsync<T>(string url) where T : class
    {
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            T? content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }

        // Default value is good for non-nullable types
        // return default;
        return null;
    }
}