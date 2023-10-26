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
    [Table("Tax")]
    public partial class Tax : AuditableEntity
    {

        [Key]
        [Column("TaxId")]
        public Guid TaxId { get; set; }

        public string? TaxName { get; set;}

        public float? TaxRate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
