using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksCrudApi.Models;

[Table("ProductProductPhoto", Schema = "Production")]
public class ProductProductPhoto
{
    public int ProductID { get; set; }

    public int ProductPhotoID { get; set; }

    // "Primary" is a T-SQL reserved word, so the property is renamed and mapped explicitly.
    [Column("Primary")]
    public bool IsPrimary { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Product Product { get; set; } = null!;

    public ProductPhoto ProductPhoto { get; set; } = null!;
}
