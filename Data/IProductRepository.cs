using AdventureWorksCrudApi.Dtos;
using AdventureWorksCrudApi.Models;

namespace AdventureWorksCrudApi.Data;

public interface IProductRepository
{
    Task<IReadOnlyList<ProductDetailDto>> GetPageAsync(int? afterId, int limit, CancellationToken ct = default);

    Task<ProductDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<Product> AddAsync(Product product, CancellationToken ct = default);

    Task<bool> UpdateAsync(Product product, CancellationToken ct = default);

    Task<bool> DeleteAsync(int id, CancellationToken ct = default);

    Task<IReadOnlyList<Product>> GetWithModelNaiveAsync(int limit, CancellationToken ct = default);

    Task<IReadOnlyList<ProductWithModelDto>> GetWithModelEagerAsync(int limit, CancellationToken ct = default);
}
