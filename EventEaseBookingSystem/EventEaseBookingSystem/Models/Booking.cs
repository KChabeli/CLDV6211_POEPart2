namespace EventEaseBookingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int BookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        public int NumberOfTickets { get; set; }

        public DateTime? BookingDate { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
