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
    [Table("Table")]
    public partial class Table : AuditableEntity
    {

        [Key]
        [Column("TableID")]
        public Guid TableID { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }

        public string? TableDescription { get; set; }

        public bool? IsOccupied { get; set; } = false;

        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
