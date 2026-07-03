using AdventureWorksCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksCrudApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductModel> ProductModels => Set<ProductModel>();
}
