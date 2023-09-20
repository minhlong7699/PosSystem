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
    public partial class Category : AuditableEntity
    {
        [Key]
        [Column("CategoryId")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is a required field.")]
        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
