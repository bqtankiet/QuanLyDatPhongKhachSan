using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_v3.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("UserName")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("UserPassWord")]
        public string Password { get; set; }

        [Required]
        [Column("UserRole")]
        public int Role { get; set; }

        [MaxLength(100)]
        [Column("FullName")]
        public string FullName { get; set; }

        [MaxLength(15)]
        [Column("Phone")]
        public string Phone { get; set; }

        [MaxLength(100)]
        [Column("Email")]
        public string Email { get; set; }
    }
}
