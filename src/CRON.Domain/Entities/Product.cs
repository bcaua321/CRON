using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRON.Domain.Entities;

[Table("products")]
public class Product
{
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    [Required(ErrorMessage = "the field {0} is required")]
    public string Code { get; set; }

    [Column("bar_code")]
    [Required(ErrorMessage = "the field {0} is required")]
    public string BarCode { get; set; }

    [Column("url")]
    [Required(ErrorMessage = "the field {0} is required")]
    public string Url { get; set; }

    [Column("product_name")]
    [Required(ErrorMessage = "the field {0} is required")]
    public string ProductName { get; set; }

    [Column("quantity")]
    public string Quantity { get; set; }

    [Column("categories")]
    public string Categories { get; set; }

    [Column("packaging")]
    public string Packaging { get; set; }

    [Column("brands")]
    public string Brands { get; set; }

    [Column("image_url")]
    public string ImageUrl { get; set; }

    [Column("imported_t")]
    public DateTime ImportedTime { get; set; } = DateTime.Now;

    [Column("status")]
    public string Status { get; set; } 
}
