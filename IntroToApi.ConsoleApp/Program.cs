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
    string responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseContent);
}

