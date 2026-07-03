using AdventureWorksCrudApi.Common;
using AdventureWorksCrudApi.Data;
using AdventureWorksCrudApi.Dtos;
using AdventureWorksCrudApi.Models;

namespace AdventureWorksCrudApi.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        group.MapGet("/", async (string? cursor, int? limit, IProductRepository repo, CancellationToken ct) =>
        {
            const int defaultLimit = 20, maxLimit = 100;
            var take = Math.Clamp(limit ?? defaultLimit, 1, maxLimit);

            int? afterId = null;
            if (!string.IsNullOrEmpty(cursor))
            {
                if (!Cursor.TryDecode(cursor, out var decoded))
                    return Results.BadRequest("Invalid cursor.");
                afterId = decoded;
            }

            var rows = await repo.GetPageAsync(afterId, take, ct);
            var hasMore = rows.Count > take;
            var items = hasMore ? rows.Take(take).ToList() : rows;
            var nextCursor = hasMore ? Cursor.Encode(items[^1].ProductID) : null;

            return Results.Ok(new PagedResult<Product>(items, nextCursor));
        });

        group.MapGet("/{id:int}", async (int id, IProductRepository repo, CancellationToken ct) =>
        {
            var product = await repo.GetByIdAsync(id, ct);
            return product is null ? Results.NotFound() : Results.Ok(product);
        });

        group.MapGet("/n-plus-one", async (int? limit, IProductRepository repo, CancellationToken ct) =>
        {
            var products = await repo.GetWithModelNaiveAsync(limit ?? 10, ct);
            return Results.Ok(products.Select(p => new ProductWithModelDto(p.ProductID, p.Name, p.ProductModelID, p.ProductModel?.Name)));
        });

        group.MapGet("/eager", async (int? limit, IProductRepository repo, CancellationToken ct) =>
        {
            var products = await repo.GetWithModelEagerAsync(limit ?? 10, ct);
            return Results.Ok(products);
        });

        group.MapPost("/", async (CreateProductDto dto, IProductRepository repo, CancellationToken ct) =>
        {
            var product = new Product
            {
                Name = dto.Name,
                ProductNumber = dto.ProductNumber,
                MakeFlag = dto.MakeFlag,
                FinishedGoodsFlag = dto.FinishedGoodsFlag,
                Color = dto.Color,
                SafetyStockLevel = dto.SafetyStockLevel,
                ReorderPoint = dto.ReorderPoint,
                StandardCost = dto.StandardCost,
                ListPrice = dto.ListPrice,
                Size = dto.Size,
                Weight = dto.Weight,
                DaysToManufacture = dto.DaysToManufacture,
                ProductSubcategoryID = dto.ProductSubcategoryID,
                ProductModelID = dto.ProductModelID,
                SellStartDate = dto.SellStartDate,
                Rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow,
            };

            var created = await repo.AddAsync(product, ct);
            return Results.Created($"/api/products/{created.ProductID}", created);
        });

        group.MapPut("/{id:int}", async (int id, UpdateProductDto dto, IProductRepository repo, CancellationToken ct) =>
        {
            var product = new Product
            {
                ProductID = id,
                Name = dto.Name,
                ProductNumber = dto.ProductNumber,
                MakeFlag = dto.MakeFlag,
                FinishedGoodsFlag = dto.FinishedGoodsFlag,
                Color = dto.Color,
                SafetyStockLevel = dto.SafetyStockLevel,
                ReorderPoint = dto.ReorderPoint,
                StandardCost = dto.StandardCost,
                ListPrice = dto.ListPrice,
                Size = dto.Size,
                Weight = dto.Weight,
                DaysToManufacture = dto.DaysToManufacture,
                ProductSubcategoryID = dto.ProductSubcategoryID,
                ProductModelID = dto.ProductModelID,
                SellStartDate = dto.SellStartDate,
                SellEndDate = dto.SellEndDate,
                DiscontinuedDate = dto.DiscontinuedDate,
                ModifiedDate = DateTime.UtcNow,
            };

            var updated = await repo.UpdateAsync(product, ct);
            return updated ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id:int}", async (int id, IProductRepository repo, CancellationToken ct) =>
        {
            var deleted = await repo.DeleteAsync(id, ct);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
