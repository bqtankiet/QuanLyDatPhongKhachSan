using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_v3.Models
{
    [Table("room")]
    public class Room
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("roomnumber")]
        public string RoomNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("roomtype")]
        public string RoomType { get; set; }

        [Required]
        [Column("price")]
        public double Price { get; set; }

        [Required]
        [Column("capacity")]
        public int Capacity { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } 
    }
}
