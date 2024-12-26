using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_v3.Models
{
    [Table("Hotels")]
    public class Hotel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("HotelName")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("HotelAddress")]
        public string Address { get; set; }

        [MaxLength(15)]
        [Column("Phone")]
        public string Phone { get; set; }

        [MaxLength(100)]
        [Column("Email")]
        public string Email { get; set; }
    }
}
