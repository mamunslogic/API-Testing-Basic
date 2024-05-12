using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace ProductCatelog.API.Test
{
    public class CreateSpecification
    {
        private ProductCatelogApiFactory _api;
        private HttpClient _httpClient;

        public CreateSpecification()
        {
             _api = new ProductCatelogApiFactory();
            _httpClient = _api.CreateClient();
        }

        [Fact]
        public async Task Should_return_200_ok_when_send_product()
        {
            var response = await _httpClient.PostAsJsonAsync("/products",
              new
              {
                  Name = "Pen",
                  Price = 12
              });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_add_to_storage_When_has_new_product()
        {
            var dbContext = _api.CreateProductCatelogDbContext();
            var response = await _httpClient.PostAsJsonAsync("/products", 
              new
              {
                  Name = "Pen",
                  Price = 12
              });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dbEntry = await dbContext.Products.FirstOrDefaultAsync(product => product.Name == "Pen");
            Assert.NotNull(dbEntry);
        }

        [Fact]
        public async Task Should_return_4000_When_price_is_negetive()
        {
            var dbContext = _api.CreateProductCatelogDbContext();
            var response = await _httpClient.PostAsJsonAsync("/products",
              new
              {
                  Name = "Pen",
                  Price = -1
              });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}