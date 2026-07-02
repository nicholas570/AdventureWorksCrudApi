using AdventureWorksCrudApi.Models;

namespace AdventureWorksCrudApi.Data;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetPageAsync(int? afterId, int limit, CancellationToken ct = default);

    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<Product> AddAsync(Product product, CancellationToken ct = default);

    Task<bool> UpdateAsync(Product product, CancellationToken ct = default);

    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
