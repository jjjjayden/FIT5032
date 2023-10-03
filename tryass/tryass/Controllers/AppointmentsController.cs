using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tryass.Models;

namespace tryass.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        [Authorize]
        public ActionResult Index()
        {
            DateTime today = DateTime.Now.Date; 
            DateTime sevenDaysLater = today.AddDays(7);

            var appointments = db.Appointments
                                 .Include(a => a.Map)
                                 .Include(a => a.User)
                                 .Where(a => a.DateTime >= today && a.DateTime < sevenDaysLater)
                                 .ToList();

            return View(appointments);
        }


        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Staff")]
        public ActionResult Create()
        {
            ViewBag.MapId = new SelectList(db.Maps, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Create([Bind(Include = "Id,MapId,DateTime,IsBooked,UserId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MapId = new SelectList(db.Maps, "Id", "Name", appointment.MapId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MapId = new SelectList(db.Maps, "Id", "Name", appointment.MapId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit([Bind(Include = "Id,MapId,DateTime,IsBooked,UserId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MapId = new SelectList(db.Maps, "Id", "Name", appointment.MapId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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


        [HttpPost]
        public ActionResult BookAppointment(int appointmentId)
        {
            var appointment = db.Appointments.Find(appointmentId);
            if (appointment == null)
            {
                return Json(new { success = false });
            }

            if (!appointment.IsBooked)
            {
                appointment.IsBooked = true;
                appointment.UserId = User.Identity.GetUserId();
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }



    }
}
