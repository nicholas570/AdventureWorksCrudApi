using AdventureWorksCrudApi.Data;
using AdventureWorksCrudApi.Dtos;
using AdventureWorksCrudApi.Models;

namespace AdventureWorksCrudApi.Endpoints;

/// <summary>
/// CRUD HTTP endpoints for products. These are fully implemented and only depend
/// on <see cref="IProductRepository"/> — the data access details live behind that seam.
/// </summary>
public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        group.MapGet("/", async (IProductRepository repo, CancellationToken ct) =>
        {
            var products = await repo.GetAllAsync(ct);
            return Results.Ok(products);
        });

        group.MapGet("/{id:int}", async (int id, IProductRepository repo, CancellationToken ct) =>
        {
            var product = await repo.GetByIdAsync(id, ct);
            return product is null ? Results.NotFound() : Results.Ok(product);
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
