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
    public class AnnotationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Annotations
        public ActionResult Index()
        {
            var annotation = db.Annotation.Include(a => a.XrayImage);
            return View(annotation.ToList());
        }

        // GET: Annotations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annotation annotation = db.Annotation.Find(id);
            if (annotation == null)
            {
                return HttpNotFound();
            }
            return View(annotation);
        }

        // GET: Annotations/Create
        public ActionResult Create()
        {
            ViewBag.XrayImageId = new SelectList(db.XrayImages, "Id", "ImageUrl");
            return View();
        }

        // POST: Annotations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,DoctorId,XrayImageId")] Annotation annotation)
        {
            if (ModelState.IsValid)
            {
                db.Annotation.Add(annotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.XrayImageId = new SelectList(db.XrayImages, "Id", "ImageUrl", annotation.XrayImageId);
            return View(annotation);
        }

        // GET: Annotations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annotation annotation = db.Annotation.Find(id);
            if (annotation == null)
            {
                return HttpNotFound();
            }
            ViewBag.XrayImageId = new SelectList(db.XrayImages, "Id", "ImageUrl", annotation.XrayImageId);
            return View(annotation);
        }

        // POST: Annotations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,DoctorId,XrayImageId")] Annotation annotation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annotation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.XrayImageId = new SelectList(db.XrayImages, "Id", "ImageUrl", annotation.XrayImageId);
            return View(annotation);
        }

        // GET: Annotations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annotation annotation = db.Annotation.Find(id);
            if (annotation == null)
            {
                return HttpNotFound();
            }
            return View(annotation);
        }

        // POST: Annotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annotation annotation = db.Annotation.Find(id);
            db.Annotation.Remove(annotation);
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
