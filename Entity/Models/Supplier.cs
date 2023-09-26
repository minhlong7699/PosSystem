using Entity.Aggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    [Table("Supplier")]
    public partial class Supplier : AuditableEntity
    {

        [Key]
        [Column("SupplierId")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name  is a required field.")]
        public string? SupplierName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone Number is a required field.")]
        public int? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address  is a required field.")]
        public string? Address { get; set; }

        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();


    }
}
