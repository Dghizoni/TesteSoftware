using ConsoleApp1.Models;
using ConsoleApp1.Services;
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

            //var teste = ConsoleApp1.Services.RequestServiceTests();

            string baseUrl = "https://api.thenewsapi.com/v1/news/top";
            string locale = "us";
            string limit = "3";
            string apiToken = "rcFgbY64i2j6EIrvei2wYoQbVQIzcWmEGJkH2mo6";
            //string endpoint = "";


            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Fazendo Requisição");

            RestResponse response = RequestService.RequestAsync(baseUrl,locale,limit,apiToken);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Requisição Status:{response.StatusCode}");

                var responseContent = response.Content;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Iniciando Manipulação do Json");

                JObject json = JObject.Parse(responseContent);
                IList<JToken> results = json["data"].Children().ToList();

                Console.WriteLine("Finalizado Manipulação do Json");

                IList<News> news = new List<News>();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Iniciando Transformação Json no Objeto News");

                foreach (JToken result in results)
                {
                    News new1 = result.ToObject<News>();
                    news.Add(new1);
                }

                Console.WriteLine("Finalizado Transformação Json no Objeto News");

                string linha = "";
                List<string> linhas = new();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Criando Arquivo .CSV");

                foreach (News noticia in news)
                {
                    linha = $"{noticia.title};{noticia.description};{noticia.url};{noticia.image_url};{noticia.source}";
                    linhas.Add(linha);
                }

                string filePath = $@"C:\Downloads\NoticiasTOP3-{DateTime.UtcNow:dd-MM-yyyy}.csv";
                await File.WriteAllLinesAsync(filePath, new[] { "Título;Descrição;Link(URL);Imagem;Portal" });
                await File.AppendAllLinesAsync(filePath, linhas);

                Console.WriteLine("Arquivo .CSV Salvo");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {response.StatusCode} - {response.ErrorMessage}");
            }

        }
    }
}