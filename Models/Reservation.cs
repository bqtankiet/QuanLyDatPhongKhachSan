using project_v3.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

[Table("reservation")]
public class Reservation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Customer")]
    [Column("customerid")]
    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; }

    [Required]
    [ForeignKey("Room")]
    [Column("roomid")]
    public int RoomId { get; set; }

    public virtual Room Room { get; set; }

    [Column("reservationdate")]
    public DateTime ReservationDate { get; set; } = DateTime.Now;

    [Column("checkinday")]
    public DateTime? CheckInDay { get; set; }

    [Column("checkoutday")]
    public DateTime? CheckOutDay { get; set; }

    [MaxLength(50)]
    [Column("paymentmethod")]
    public string PaymentMethod { get; set; }

    // Navigation property
    public virtual ICollection<ServiceUsage> ServiceUsages { get; set; }
}
