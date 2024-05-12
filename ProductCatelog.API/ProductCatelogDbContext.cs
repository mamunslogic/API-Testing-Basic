using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductCatelog.API
{
    public class ProductCatelogDbContext : DbContext
    {
        //private readonly DbContextOptions<ProductCatelogDbContext> _options;

        public ProductCatelogDbContext(DbContextOptions<ProductCatelogDbContext> options) : base(options)
        {
            //_options = options;
        }

        public DbSet<Product> Products => Set<Product>();
    }

    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
