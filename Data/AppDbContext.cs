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
    public DbSet<ProductPhoto> ProductPhotos => Set<ProductPhoto>();
    public DbSet<ProductProductPhoto> ProductProductPhotos => Set<ProductProductPhoto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-many: Product -> ProductModel
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductModel)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.ProductModelID);

        // Many-to-many WITH PAYLOAD: Product <-> ProductPhoto through ProductProductPhoto.
        // Because the join table carries extra columns (IsPrimary, ModifiedDate),
        // we map it as an explicit join entity via UsingEntity<T>.
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductPhotos)
            .WithMany(ph => ph.Products)
            .UsingEntity<ProductProductPhoto>(
                // right side: join -> ProductPhoto
                j => j.HasOne(pp => pp.ProductPhoto)
                      .WithMany(ph => ph.ProductProductPhotos)
                      .HasForeignKey(pp => pp.ProductPhotoID),
                // left side: join -> Product
                j => j.HasOne(pp => pp.Product)
                      .WithMany(p => p.ProductProductPhotos)
                      .HasForeignKey(pp => pp.ProductID),
                // the join entity itself: composite PK (IsPrimary is mapped via [Column] on the model)
                j => j.HasKey(pp => new { pp.ProductID, pp.ProductPhotoID }));
    }
}
