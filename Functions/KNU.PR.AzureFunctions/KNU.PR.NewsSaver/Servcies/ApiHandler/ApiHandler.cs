using KNU.PR.NewsSaver.Constants;
using KNU.PR.NewsSaver.Models.NewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace KNU.PR.NewsSaver.Servcies.ApiHandler
{
    public class ApiHandler : IApiHandler
    {
        private readonly HttpClient client;
        private readonly IRestClient restClient;
        private readonly string apiKey;
        private readonly string rapidApiKey;
        private readonly ILogger<ApiHandler> logger;

        public ApiHandler(HttpClient client, IRestClient restClient, ILogger<ApiHandler> logger)
        {
            this.client = client;
            this.restClient = restClient;
            this.apiKey = Environment.GetEnvironmentVariable(EnvironmentVariablesConstants.ApiKey, EnvironmentVariableTarget.Process);
            this.rapidApiKey = Environment.GetEnvironmentVariable(EnvironmentVariablesConstants.RapidApiKey, EnvironmentVariableTarget.Process);
            this.logger = logger;
        }

        public async Task<List<Article>> GetLast24HoursNewsAsync()
        {
            var yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            var requestUri = $"https://newsapi.org/v2/everything?from={yesterday}&q=Barcelona&apiKey={apiKey}";
            var response = await client.GetAsync(requestUri);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                logger.LogError($"API Request failed: {response.StatusCode}");
            }

            logger.LogInformation($"API Request success: {response.StatusCode}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var deserializedObject = JsonConvert.DeserializeObject<ResponseModel>(jsonResult);
            var articles = deserializedObject.Articles.Take(1).ToList();

            // Getting full text from url using Extract News API
            foreach (Article article in articles)
            {
                var url = article.Url;
                var requestUri2 = $"https://extract-news.p.rapidapi.com/v0/article?url={url}";

                var restClient = new RestClient(requestUri2);
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "extract-news.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", rapidApiKey);

                IRestResponse restResponse = restClient.Execute(request);

                if (restResponse.StatusCode != HttpStatusCode.OK && restResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    logger.LogError($"Extract News API Request failed: {restResponse.StatusCode}");
                }

                logger.LogInformation($"Extract News API Request success: {restResponse.StatusCode}");

                var responseContent = restResponse.Content;

                JObject joResponse = JObject.Parse(responseContent);
                JObject ojObject = (JObject)joResponse["article"];
                string articleText = ((JValue)ojObject["text"]).ToString();

                article.Content = articleText;
            }

            return articles;
        }
    }
}
