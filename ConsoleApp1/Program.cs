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

            List<string> linhas = new();

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

                string linha = new();

                foreach (News noticia in news){
                    linha = $"{noticia.title};{noticia.description};{noticia.url};{noticia.image_url};{noticia.source}";
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Writeline(linha);
                    linhas.add(linha);
                }

                byte[] bytesArquivo = null;

                using (MemoryStream arquivo = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(arquivo))
                    {
                        sw.WriteLine("Título;Descrição;Link(URL);Imagem;Portal");
                        foreach (string linha in linhas)
                            sw.WriteLine(linha);
                    }
                    bytesArquivo = arquivo.ToArray();
                }

                string hoje = DateTime.UtcNow.ToString("-dd-MM-yyyy");

                string diretorio = $@"C:\NoticiasTOP3{hoje}.csv";
                File.WriteAllBytes(diretorio, bytesArquivo);

            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

        }
    }
}