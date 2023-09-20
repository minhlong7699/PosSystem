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
    public partial class Payment : AuditableEntity
    {

        [Key]
        [Column("PaymentId")]
        public Guid PaymentId { get; set; }

        public decimal? PaymentAmount { get; set; }

        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
