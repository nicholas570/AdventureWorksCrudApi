using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksCrudApi.Models;

[Table("ProductPhoto", Schema = "Production")]
public class ProductPhoto
{
    [Key]
    public int ProductPhotoID { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }

    [MaxLength(50)]
    public string? ThumbnailPhotoFileName { get; set; }

    public byte[]? LargePhoto { get; set; }

    [MaxLength(50)]
    public string? LargePhotoFileName { get; set; }

    public DateTime ModifiedDate { get; set; }

    public List<Product> Products { get; set; } = [];

    // Join-entity navigation: needed to read the payload columns (IsPrimary, ModifiedDate).
    public List<ProductProductPhoto> ProductProductPhotos { get; set; } = [];
}
