namespace AdventureWorksCrudApi.Dtos;

/// <summary>Payload for creating a product (POST). Fields map to Production.Product.</summary>
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

/// <summary>Payload for updating a product (PUT).</summary>
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
