using Entity.Aggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    [Table("Product")]
    public partial class Product : AuditableEntity
    {

        [Key]
        [Column("ProductId")]
        public Guid ProductId { get; set; }


        [Required(ErrorMessage = "Product Name is a required field.")]
        public string? ProductName { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Product Price is a required field.")]
        public decimal? ProductPrice { get; set; }

        public decimal? ProductPriceAfterDiscount { get; set; }

        public int? StockQuantity { get; set; }

        public string? Image { get; set; }

        [ForeignKey(nameof(Promotion))]
        public Guid? PromotionId { get; set; } = null;
        public virtual Promotion? Promotion { get; set;}

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderItem>? OrderItemss { get; set; } = new List<OrderItem>();

    }
}
