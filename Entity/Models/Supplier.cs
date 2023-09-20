using Entity.Aggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
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
