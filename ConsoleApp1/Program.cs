namespace MyApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.thenewsapi.com/v1/news/top?api_token=rcFgbY64i2j6EIrvei2wYoQbVQIzcWmEGJkH2mo6&locale=us&limit=3");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

        //https://www.thenewsapi.com/documentationk
        }
    }
}

