using Entity.Aggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public partial class User : AuditableEntity
    {

        [Key]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName  is a required field.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "UserPassword is a required field.")]
        public string? UserPassword { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public int? PhoneNumber { get; set; }

        [Required(ErrorMessage = "EmailAddress is a required field.")]
        public string? EmailAddress { get; set; }

        public string? Image { get; set; }

        public virtual ICollection<UserAuthentication>? UserAuthentications { get; set; } = new List<UserAuthentication>();
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();


        [ForeignKey(nameof(UserRole))]
        public Guid RoleId { get; set; }
        public virtual UserRole? UserRole { get; set; }
    }
}
