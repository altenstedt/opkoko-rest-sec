using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Tests
{
    internal class TokenHttpClient
    {
        private const string Content = "client_id=myclient&client_secret=secret&scope=read:product&grant_type=client_credentials";
        private readonly Uri tokenUri = new Uri("http://localhost:4000/connect/token");

        private readonly HttpClient client;
        private string accessToken;

        public TokenHttpClient()
        {
            client = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            if (string.IsNullOrEmpty(accessToken)) {
                await Initialize();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            return await client.GetAsync(requestUri);
        }

        private async Task Initialize()
        {
            var client = new HttpClient();

            var body = new StringContent(Content, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(tokenUri, body);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TokenResult>(content);

            accessToken = result.access_token;
        }

        private class TokenResult
        {
            public string access_token { get; set; }
        }
    }
}