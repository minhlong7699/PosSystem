using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Aggregate
{
    public class AuditableEntity
    {
        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public void CreateAuditFields(string userId)
        {
            CreatedAt = DateTime.Now;
            CreatedBy = userId;
            UpdatedAt = DateTime.Now;
            UpdatedBy = userId;
        }

        public void UpdateAuditFields(string userId)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = userId;
        }
    }
}
