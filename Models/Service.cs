using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_v3.Models
{
    [Table("service")]
    public class Service
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("servicesname")]
        public string Name { get; set; }

        [Required]
        [Column("price")]
        public double Price { get; set; }
        public virtual ICollection<ServiceUsage> ServiceUsages { get; set; } 
    }
}
