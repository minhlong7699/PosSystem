using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("UserAuthentication")]
    public partial class UserAuthentication
    {

        [Key]
        [Column("UserAuthenticationId")]
        public Guid UserAuthenticationId { get; set; }

        public string? AuthenticationCode { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool IsUsed { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

    }
}
