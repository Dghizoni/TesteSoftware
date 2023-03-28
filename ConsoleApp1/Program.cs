using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using UBEC.GenneraReports.Services;

namespace MyApp
{

    class Program
    {

        static async Task Main(string[] args)
        {

            string baseUrl = "https://api.thenewsapi.com/v1/news/top";
            string endpoint = "";

            RestResponse response = RequestService.RequestAsync(baseUrl, endpoint);

            if (response.IsSuccessStatusCode)
            {

                var responseContent = response.Content;

                JObject json = JObject.Parse(responseContent);
                IList<JToken> results = json["data"].Children().ToList();

                IList<News> news = new List<News>();

                foreach (JToken result in results)
                {
                    News new1 = result.ToObject<News>();
                    news.Add(new1);
                }

            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

        }
    }
}