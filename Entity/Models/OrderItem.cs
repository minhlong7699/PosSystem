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
    public class OrderItem : AuditableEntity
    {

        [Key]
        [Column("OrderItemsId")]
        public Guid OrderItemsId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set;}


    }
}
