using project_v3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table ("serviceusage")]
public class ServiceUsage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("reservationid")]
    public int ReservationId { get; set; }

    [Required]
    [Column("serviceid")]
    public int ServiceId { get; set; }

    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }

    // Navigation property
    [ForeignKey("serviceid")]
    public Service Service { get; set; }

    [ForeignKey("ReservationId")]
    public Reservation Reservation { get; set; }
}
