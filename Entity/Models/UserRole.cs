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
    [Table("UserRole")]
    public partial class UserRole : AuditableEntity
    {

        [Key]
        [Column("RoleId")]
        public Guid RoleId { get; set; }

        public string? RoleName { get; set; }


        public virtual ICollection<User>? Users { get; set; } = new List<User>();
    }
}
