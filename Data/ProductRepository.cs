using AdventureWorksCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksCrudApi.Data;

public class ProductRepository(AppDbContext db) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.Products.AsNoTracking().ToListAsync(ct);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await db.Products.FindAsync([id], ct).AsTask();
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
}
