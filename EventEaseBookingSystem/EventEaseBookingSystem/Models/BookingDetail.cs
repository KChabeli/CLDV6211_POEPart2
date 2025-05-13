using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventEaseBookingSystem.Models
{
    public class BookingDetail
    {
        [Key]
        public int BookingDetailsId { get; set; }

        public int BookingId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        public int NumberOfTickets { get; set; }

        public DateTime BookingDate { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(100)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(200)]
        public string VenueLocation { get; set; }
    }
}