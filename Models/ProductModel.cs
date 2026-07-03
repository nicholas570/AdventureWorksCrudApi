using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksCrudApi.Models;

[Table("ProductModel", Schema = "Production")]
public class ProductModel
{
    [Key]
    public int ProductModelID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = [];
}
