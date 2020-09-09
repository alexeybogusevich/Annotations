using KNU.PR.NewsSaver.Constants;
using KNU.PR.NewsSaver.Models.NewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KNU.PR.NewsSaver.Servcies.ApiHandler
{
    public class ApiHandler : IApiHandler
    {
        private readonly HttpClient client;
        private readonly string apiKey;
        private readonly ILogger<ApiHandler> logger;

        public ApiHandler(HttpClient client, ILogger<ApiHandler> logger)
        {
            this.client = client;
            this.apiKey = Environment.GetEnvironmentVariable(EnvironmentVariablesConstants.ApiKey, EnvironmentVariableTarget.Process);
            this.logger = logger;
        }

        public async Task<List<Article>> GetLast24HoursNewsAsync()
        {
            var yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            var requestUri = $"https://newsapi.org/v2/everything?from={yesterday}&q=Barcelona&apiKey={apiKey}";
            var response = await client.GetAsync(requestUri);

            if(response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                logger.LogError($"API Request failed: {response.StatusCode}");
            }

            logger.LogInformation($"API Request success: {response.StatusCode}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var deserializedObject = JsonConvert.DeserializeObject<ResponseModel>(jsonResult);

            return deserializedObject.Articles.Take(100).ToList();
        }
    }
}
