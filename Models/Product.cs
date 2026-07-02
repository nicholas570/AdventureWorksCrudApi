using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksCrudApi.Models;

/// <summary>
/// Maps to the Production.Product table in the full AdventureWorks2022 sample database.
/// (The lightweight AdventureWorksLT uses SalesLT.Product with slightly different columns.)
/// </summary>
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

    public DateTime SellStartDate { get; set; }

    public DateTime? SellEndDate { get; set; }

    public DateTime? DiscontinuedDate { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }
}
