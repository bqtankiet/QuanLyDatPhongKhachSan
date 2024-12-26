using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_v3.Models
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("fullname")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("idcardnumber")]
        public string IdCardNumber { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("phone")]
        public string Phone { get; set; }

        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(255)]
        [Column("address")]
        public string Address { get; set; }

        [MaxLength(5)]
        [Column("gender")]
        public string Gender { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } 


        public Customer(string fullName, string idCardNumber, string phone, string email, string address, string gender)
        {
            FullName = fullName;
            IdCardNumber = idCardNumber;
            Phone = phone;
            Email = email;
            Address = address;
            Gender = gender;
        }

    }
}
