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
    [Table("OrderItem")]
    public partial class OrderItem : AuditableEntity
    {

        [Key]
        [Column("OrderItemsId")]
        public Guid OrderItemsId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set;}


        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

    }
}
