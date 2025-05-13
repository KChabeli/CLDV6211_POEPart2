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
    public class BookingDetailsViewModelsController : Controller
    {
        private EventEaseDBContext db = new EventEaseDBContext();

        // GET: BookingDetailsViewModels
        public ActionResult Index()
        {
            return View(db.BookingDetails.ToList());
        }

        // GET: BookingDetailsViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetailsViewModel bookingDetailsViewModel = db.BookingDetails.Find(id);
            if (bookingDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetailsViewModel);
        }

        // GET: BookingDetailsViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingDetailsViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,FullName,Email,NumberOfTickets,BookingDate,EventName,EventDate,VenueName,Location")] BookingDetailsViewModel bookingDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.BookingDetails.Add(bookingDetailsViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingDetailsViewModel);
        }

        // GET: BookingDetailsViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetailsViewModel bookingDetailsViewModel = db.BookingDetails.Find(id);
            if (bookingDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetailsViewModel);
        }

        // POST: BookingDetailsViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,FullName,Email,NumberOfTickets,BookingDate,EventName,EventDate,VenueName,Location")] BookingDetailsViewModel bookingDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingDetailsViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingDetailsViewModel);
        }

        // GET: BookingDetailsViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetailsViewModel bookingDetailsViewModel = db.BookingDetails.Find(id);
            if (bookingDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetailsViewModel);
        }

        // POST: BookingDetailsViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingDetailsViewModel bookingDetailsViewModel = db.BookingDetails.Find(id);
            db.BookingDetails.Remove(bookingDetailsViewModel);
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
    }
}
