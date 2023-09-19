using Entity.Aggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Product : AuditableEntity
    {

        [Key]
        [Column("ProductId")]
        public Guid ProductId { get; set; }


        [Required(ErrorMessage = "Product Name is a required field.")]
        public string? ProductName { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Product Price is a required field.")]
        public float? ProductPrice { get; set; }

        public float? ProductPriceAfterDiscount { get; set; }

        public int? StockQuantity { get; set; }

        public string? Image { get; set; }

    }
}
