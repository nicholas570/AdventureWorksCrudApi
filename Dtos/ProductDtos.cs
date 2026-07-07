namespace AdventureWorksCrudApi.Dtos;

public record PagedResult<T>(IReadOnlyList<T> Items, string? NextCursor);

public record ProductWithModelDto(int ProductId, string Name, int? ProductModelId, string? ProductModelName);

public record ProductPhotoDto(
    int ProductPhotoId,
    string? ThumbnailPhotoFileName,
    string? LargePhotoFileName,
    bool IsPrimary);

public record ProductDetailDto(
    int ProductId,
    string Name,
    string ProductNumber,
    string? Color,
    decimal StandardCost,
    decimal ListPrice,
    string? Size,
    decimal? Weight,
    int? ProductModelId,
    DateTime SellStartDate,
    DateTime? SellEndDate,
    DateTime? DiscontinuedDate,
    IReadOnlyList<ProductPhotoDto> Photos);

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
