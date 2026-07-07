using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksCrudApi.Models;

[Table("Product", Schema = "Production")]
public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(25)]
    public string ProductNumber { get; set; } = string.Empty;

    public bool MakeFlag { get; set; }

    public bool FinishedGoodsFlag { get; set; }

    [MaxLength(15)]
    public string? Color { get; set; }

    public short SafetyStockLevel { get; set; }

    public short ReorderPoint { get; set; }

    public decimal StandardCost { get; set; }

    public decimal ListPrice { get; set; }

    [MaxLength(5)]
    public string? Size { get; set; }

    public decimal? Weight { get; set; }

    public int DaysToManufacture { get; set; }

    public int? ProductSubcategoryID { get; set; }

    public int? ProductModelID { get; set; }

    public ProductModel? ProductModel { get; set; }

    public DateTime SellStartDate { get; set; }

    public DateTime? SellEndDate { get; set; }

    public DateTime? DiscontinuedDate { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    // Many-to-many with ProductPhoto via the ProductProductPhoto join table.
    public List<ProductPhoto> ProductPhotos { get; set; } = [];

    // Join-entity navigation: needed to read the payload columns (IsPrimary, ModifiedDate).
    public List<ProductProductPhoto> ProductProductPhotos { get; set; } = [];
}
