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
    public class Payment : AuditableEntity
    {

        [Key]
        [Column("PaymentId")]
        public Guid PaymentId { get; set; }

        public float? PaymentAmount { get; set; }

        public ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
