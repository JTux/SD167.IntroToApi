using System.Net.Http.Json;
using System.Text.Json;

// C# class that can send HTTP requests
HttpClient client = new HttpClient();

// The base URL for Star Wars API
string baseUrl = "https://swapi.dev/api";

// First GET request to get people/1 endpoint
// GetAsync method is async meaning we need the await keyword
HttpResponseMessage response = await client.GetAsync($"{baseUrl}/people/1");
Console.WriteLine(response.IsSuccessStatusCode);

if (response.IsSuccessStatusCode)
{
    // Takes our response body and converts it to a string
    string responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseContent);
    // Takes our stringified JSON object and converts it to a C# Person object
    Person? person = JsonSerializer.Deserialize<Person>(responseContent);
    // Console.WriteLine(person.Name);

    // This does what lines 18 and 21 do but in one line
    Person? luke = await response.Content.ReadFromJsonAsync<Person>();

    if (luke != null)
    {
        // Console.WriteLine(luke.Name);
        foreach (string vehicleUrl in luke.Vehicles)
        {
            // Sending a GET request to the vehicleUrl
            HttpResponseMessage vehicleResponse = await client.GetAsync(vehicleUrl);

            // Taking the response content and converting to a C# object
            Vehicle? vehicle = await vehicleResponse.Content.ReadFromJsonAsync<Vehicle>();

            // Optional chaining because vehicle is nullable
            Console.WriteLine(vehicle?.Name);
        }
    }
}

SwapiService service = new SwapiService();

// Person? person11 = await service.GetPersonAsync("https://swapi.dev/api/people/11");
Person? person11 = await service.GetPersonAsync($"{baseUrl}/people/11");

if (person11 != null)
{
    Console.WriteLine(person11.Name);

    foreach (var vehicleUrl in person11.Vehicles)
    {
        Vehicle? vehicle = await service.GetVehicleAsync(vehicleUrl);

        Console.WriteLine(vehicle?.Name);
    }
}

var genericResponse = await service.GetAsync<Person>($"{baseUrl}/people/4");
// var genericResponse = await service.GetAsync<Vehicle>($"{baseUrl}/vehicles/4");
if (genericResponse != null)
{
    Console.WriteLine(genericResponse.Name);
    foreach (var starship in genericResponse.Starships)
    {
        var ship = await service.GetAsync<Starship>(starship);
        Console.WriteLine(ship?.Name);
    }
}
else
{
    Console.WriteLine("Generic object does not exist.");
}

string searchUrl = $"{baseUrl}/people?search=f";
SearchResult<Person>? skywalkers = await service.GetAsync<SearchResult<Person>>(searchUrl);

foreach (Person p in skywalkers?.Results ?? new())
{
    Console.WriteLine(p.Name);
}
