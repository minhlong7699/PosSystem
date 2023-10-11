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
    [Table("Order")]
    public partial class Order : AuditableEntity
    {

        [Key]
        [Column("OrderId")]
        public Guid OrderId { get; set; }

        public string? OrderType { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<Invoice>? Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }

        [ForeignKey(nameof(Table))]
        public Guid? TableId { get; set; }
        public virtual Table? Table { get; set; }

        [ForeignKey(nameof(Tax))]
        public Guid TaxId { get; set; } 
        public virtual Tax? Tax { get; set; }

        [ForeignKey(nameof(Promotion))]
        public Guid? PromotionId { get; set; }
        public virtual Promotion? Promotion { get; set; }
    }
}
