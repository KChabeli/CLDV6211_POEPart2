using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventEaseBookingSystem.Models
{
    public class BookingDetailsViewModel
    {
        public int BookingId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime BookingDate { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
    }
}