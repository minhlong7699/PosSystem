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
    public class Order : AuditableEntity
    {

        [Key]
        [Column("OrderId")]
        public Guid OrderId { get; set; }

        public float? TotalAmount { get; set; }

        public string? OrderType { get; set; }

        public string? Status { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [ForeignKey(nameof(Table))]
        public Guid TableId { get; set; }
        public Table? Table { get; set; }
    }
}
