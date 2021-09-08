using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API;
using Database.ViewModels;
using Newtonsoft.Json;
using Xunit;

namespace Test
{
    public class GetAllProducts : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public GetAllProducts(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            var httpResponse = await _client.GetAsync("/v1/products");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDetailViewModel>>(stringResponse);
            Console.WriteLine(products);
            Assert.Contains(products, p => p.Name == "Red Bag");
            Assert.Contains(products, p => p.Name == "Green Bag");
            Assert.Contains(products, p => p.Name == "Blue Bag");
        }
    }
}
