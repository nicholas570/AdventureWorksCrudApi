using AdventureWorksCrudApi.Dtos;
using AdventureWorksCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksCrudApi.Data;

public class ProductRepository(AppDbContext db) : IProductRepository
{
    public async Task<IReadOnlyList<ProductDetailDto>> GetPageAsync(int? afterId, int limit, CancellationToken ct = default)
    {
        IQueryable<Product> query = db.Products;
        if (afterId is not null)
            query = query.Where(p => p.ProductID > afterId.Value);

        return await query
            .OrderBy(p => p.ProductID)
            .Take(limit + 1)
            .Select(p => new ProductDetailDto(
                p.ProductID,
                p.Name,
                p.ProductNumber,
                p.Color,
                p.StandardCost,
                p.ListPrice,
                p.Size,
                p.Weight,
                p.ProductModelID,
                p.SellStartDate,
                p.SellEndDate,
                p.DiscontinuedDate,
                p.ProductProductPhotos
                    .Select(pp => new ProductPhotoDto(
                        pp.ProductPhotoID,
                        pp.ProductPhoto.ThumbnailPhotoFileName,
                        pp.ProductPhoto.LargePhotoFileName,
                        pp.IsPrimary))
                    .ToList()))
            .ToListAsync(ct);
    }

    public async Task<ProductDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await db.Products
            .Where(p => p.ProductID == id)
            .Select(p => new ProductDetailDto(
                p.ProductID,
                p.Name,
                p.ProductNumber,
                p.Color,
                p.StandardCost,
                p.ListPrice,
                p.Size,
                p.Weight,
                p.ProductModelID,
                p.SellStartDate,
                p.SellEndDate,
                p.DiscontinuedDate,
                p.ProductProductPhotos
                    .Select(pp => new ProductPhotoDto(
                        pp.ProductPhotoID,
                        pp.ProductPhoto.ThumbnailPhotoFileName,
                        pp.ProductPhoto.LargePhotoFileName,
                        pp.IsPrimary))
                    .ToList()))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<Product> AddAsync(Product product, CancellationToken ct = default)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync(ct);
        return product;
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken ct = default)
    {
        var existing = await db.Products.FindAsync(product.ProductID, ct);
        if (existing is null)
            return false;

        db.Products.Update(product);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await db.Products.FindAsync(id, ct);
        if (existing is null)
            return false;

        db.Products.Remove(existing);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IReadOnlyList<Product>> GetWithModelNaiveAsync(int limit, CancellationToken ct = default)
    {
        // N+1 pattern
        var products = await db.Products
            .Where(p => p.ProductModelID != null)
            .OrderBy(p => p.ProductID)
            .Take(limit)
            .ToListAsync(ct);

        // One extra round-trip PER product to load its model.
        // This is the N+1 problem — exactly what lazy loading would do silently.
        foreach (var p in products)
        {
            p.ProductModel = await db.ProductModels
                .FirstOrDefaultAsync(m => m.ProductModelID == p.ProductModelID, ct);
        }

        return products;
    }


    public async Task<IReadOnlyList<ProductWithModelDto>> GetWithModelEagerAsync(int limit, CancellationToken ct = default)
    {
        // A single query with a LEFT JOIN pulls products + their models.
        return await db.Products
            .Where(p => p.ProductModelID != null)
            .OrderBy(p => p.ProductID)
            .Include(p => p.ProductModel)   // eager loading
            .Take(limit)
            .Select(p => new ProductWithModelDto(p.ProductID, p.Name, p.ProductModelID, p.ProductModel!.Name))
            .ToListAsync(ct);
    }

}
