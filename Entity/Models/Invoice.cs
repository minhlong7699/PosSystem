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
    [Table("Invoice")]
    public partial class Invoice : AuditableEntity
    {

        [Key]
        [Column("InvoiceId")]
        public Guid InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? CustomerName { get; set; }

        public string? ShippingAddress {get; set; }

        public string? Status { get; set; }

        [ForeignKey(nameof(Order))]
        public virtual Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

        
    }
}
