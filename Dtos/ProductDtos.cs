namespace AdventureWorksCrudApi.Dtos;

public record PagedResult<T>(IReadOnlyList<T> Items, string? NextCursor);

/// <summary>Flattened product + its related model name (for the N+1 / eager-loading demo).</summary>
public record ProductWithModelDto(int ProductId, string Name, int? ProductModelId, string? ProductModelName);

public record CreateProductDto(
    string Name,
    string ProductNumber,
    bool MakeFlag,
    bool FinishedGoodsFlag,
    string? Color,
    short SafetyStockLevel,
    short ReorderPoint,
    decimal StandardCost,
    decimal ListPrice,
    string? Size,
    decimal? Weight,
    int DaysToManufacture,
    int? ProductSubcategoryID,
    int? ProductModelID,
    DateTime SellStartDate);

public record UpdateProductDto(
    string Name,
    string ProductNumber,
    bool MakeFlag,
    bool FinishedGoodsFlag,
    string? Color,
    short SafetyStockLevel,
    short ReorderPoint,
    decimal StandardCost,
    decimal ListPrice,
    string? Size,
    decimal? Weight,
    int DaysToManufacture,
    int? ProductSubcategoryID,
    int? ProductModelID,
    DateTime SellStartDate,
    DateTime? SellEndDate,
    DateTime? DiscontinuedDate);
