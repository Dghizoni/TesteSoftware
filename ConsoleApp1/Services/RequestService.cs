using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;

namespace UBEC.GenneraReports.Services
{
    public class RequestService
    {

        public static RestResponse RequestAsync(
          string url,
          string locale,
          string limit,
          string apiToken,
          string endpoint = "/",
          Dictionary<string, string>? urlSegments = null,
          Method method = Method.Get,
          string body = null
        )
        {
            Uri baseUrl = new Uri(url);
            RestClient client = new RestClient(baseUrl);

            RestRequest request = new RestRequest(endpoint, method);

            request.AddQueryParameter("api_token", apiToken);
            request.AddQueryParameter("locale", locale);
            request.AddQueryParameter("limit", limit);

            if (urlSegments != null)
            {
                foreach (var item in urlSegments)
                    request.AddUrlSegment(item.Key, item.Value);
            }

            if (body != null)
                request.AddJsonBody(body);

            var result = client.Execute(request);
            return result;
        }
    }
}