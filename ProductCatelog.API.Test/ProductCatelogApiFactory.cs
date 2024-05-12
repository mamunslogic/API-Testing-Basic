using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCatelog.API.Test
{
    public class ProductCatelogApiFactory : WebApplicationFactory<IApiAssemblyMarker>
    {
        public ProductCatelogDbContext CreateProductCatelogDbContext()
        {
            var _dbContextFactory = Services.GetRequiredService<IDbContextFactory<ProductCatelogDbContext>>();
            var db = _dbContextFactory.CreateDbContextAsync();
            db.Result.Database.EnsureCreated();
            return db.Result;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(
            services =>
                {
                    services.AddDbContextFactory<ProductCatelogDbContext>(o => o.UseInMemoryDatabase("products"));
                });
        }
    }
}
