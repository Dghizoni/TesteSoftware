using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using System.Net;
using UBEC.GenneraReports.Services;

namespace TestProject1
{
    public class Tests
    {
        private RestClient client;
        private string baseUrl = "https://api.thenewsapi.com/v1/news/top";
        private string locale = "us";
        private string limit = "3";
        private string apiToken = "rcFgbY64i2j6EIrvei2wYoQbVQIzcWmEGJkH2mo6";
        private RestResponse response;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
            response = RequestService.RequestAsync(baseUrl, locale, limit, apiToken);
        }

        [Test]
        public void TestRequestStatusCode()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestResponseContentIsNotNull()
        {
            Assert.IsNotNull(response.Content);
        }

        [Test]
        public void TestResponseContentTypeIsJson()
        {
            Assert.AreEqual("application/json", response.ContentType);
        }

        [Test]
        public void TestResponseTime()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            JObject json = JObject.Parse(response.Content);

            stopwatch.Stop();

            Assert.Less(stopwatch.Elapsed.TotalSeconds, 3);
        }

        [Test]
        public void TestResponseHasData()
        {
            JObject json = JObject.Parse(response.Content);
            JArray data = (JArray)json["data"];
            Assert.Greater(data.Count, 0);
        }
    }
}