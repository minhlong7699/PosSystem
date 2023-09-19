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
    public class Promotion : AuditableEntity
    {

        [Key]
        [Column("PromotionId")]
        public Guid PromotionId { get; set; }


        [Required(ErrorMessage = "Promotion Name  is a required field.")]
        public string? PromotionName { get; set; }

        public string? PromotionDescription { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "DisCount Percent is a required field.")]
        public float? DisCountPercent { get; set; }
    }
}
