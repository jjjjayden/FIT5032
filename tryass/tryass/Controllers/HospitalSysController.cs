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
    public class HospitalSysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HospitalSys
        public ActionResult Index()
        {
            return View(db.HospitalSyses.ToList());
        }

        // GET: HospitalSys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalSys hospitalSys = db.HospitalSyses.Find(id);
            if (hospitalSys == null)
            {
                return HttpNotFound();
            }
            return View(hospitalSys);
        }

        // GET: HospitalSys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HospitalSys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] HospitalSys hospitalSys)
        {
            if (ModelState.IsValid)
            {
                db.HospitalSyses.Add(hospitalSys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hospitalSys);
        }

        // GET: HospitalSys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalSys hospitalSys = db.HospitalSyses.Find(id);
            if (hospitalSys == null)
            {
                return HttpNotFound();
            }
            return View(hospitalSys);
        }

        // POST: HospitalSys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] HospitalSys hospitalSys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitalSys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hospitalSys);
        }

        // GET: HospitalSys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalSys hospitalSys = db.HospitalSyses.Find(id);
            if (hospitalSys == null)
            {
                return HttpNotFound();
            }
            return View(hospitalSys);
        }

        // POST: HospitalSys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HospitalSys hospitalSys = db.HospitalSyses.Find(id);
            db.HospitalSyses.Remove(hospitalSys);
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
