using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventEaseBookingSystem.Models;

namespace EventEaseBookingSystem.Controllers
{
    public class BookingDetailsController : Controller
    {
        private EventEaseDBContext db = new EventEaseDBContext();

        // GET: BookingDetails
        public ActionResult Index()
        {
            return View(db.BookingDetail.ToList());
        }

        // GET: BookingDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = db.BookingDetail.Find(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // GET: BookingDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingDetailsId,BookingId,FullName,Email,NumberOfTickets,BookingDate,EventName,EventDate,VenueName,VenueLocation")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                db.BookingDetail.Add(bookingDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingDetail);
        }

        // GET: BookingDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = db.BookingDetail.Find(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingDetailsId,BookingId,FullName,Email,NumberOfTickets,BookingDate,EventName,EventDate,VenueName,VenueLocation")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingDetail);
        }

        // GET: BookingDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = db.BookingDetail.Find(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingDetail bookingDetail = db.BookingDetail.Find(id);
            db.BookingDetail.Remove(bookingDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string query)
        {
            var data = db.Database.SqlQuery<BookingDetailsViewModel>(
                "SELECT b.BookingId, b.FullName, b.Email, b.NumberOfTickets, b.BookingDate, " +
                "e.EventName, e.EventDate, v.Name AS VenueName, v.Location " +
                "FROM Bookings b " +
                "INNER JOIN Events e ON b.EventId = e.EventId " +
                "INNER JOIN Venues v ON e.VenueId = v.VenueId")
                .ToList();

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                data = data.Where(d =>
                    d.BookingId.ToString().Contains(query) ||
                    d.EventName.ToLower().Contains(query)).ToList();
            }

            return View("Search", data);
        }

        public ActionResult RefreshTable()
        {
            var details = db.BookingDetails.ToList();

            var viewModelList = details.Select(d => new BookingDetailsViewModel
            {
                BookingId = d.BookingId,
                FullName = d.FullName,
                Email = d.Email,
                NumberOfTickets = d.NumberOfTickets,
                BookingDate = d.BookingDate,
                EventName = d.EventName,
                EventDate = d.EventDate,
                VenueName = d.VenueName,
                Location = d.Location
            }).ToList();

            return PartialView("_BookingDetailsTable", viewModelList);
        }
    }
}
