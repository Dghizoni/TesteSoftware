using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;


namespace UBEC.GenneraReports.Services
{
    public static class RequestService
    {
        public static RestResponse RequestAsync(
            string url,
            string locale,
            string limit,
            string apiToken,
            string endpoint = "/",
            Dictionary<string, string>? urlSegments = null,
            Method method = Method.Get,
            string? body = null)
        {
            var baseUrl = new Uri(url);
            var client = new RestClient(baseUrl);

            var request = new RestRequest(endpoint, method)
                .AddQueryParameter("api_token", apiToken)
                .AddQueryParameter("locale", locale)
                .AddQueryParameter("limit", limit);

            if (urlSegments is not null)
            {
                foreach (var item in urlSegments)
                {
                    request.AddUrlSegment(item.Key, item.Value);
                }
            }

            if (body is not null)
            {
                request.AddJsonBody(body);
            }

            var result = client.Execute(request);
            return result;
        }
    }
}
