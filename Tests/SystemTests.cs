using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    [Trait("Category", "System")]
    public class SystemTests
    {
        private readonly Uri baseUri = new Uri("http://localhost:5000/");

        [Fact]
        public async Task  GetProductsShouldReturn401WhenNotAuthenticated()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri(baseUri, "products"));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task  GetProductsShouldReturn200WhenAuthenticated()
        {
            var client = new TokenHttpClient();
            var response = await client.GetAsync(new Uri(baseUri, "products"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
