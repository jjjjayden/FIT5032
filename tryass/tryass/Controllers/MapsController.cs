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
    public class MapsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Maps
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Maps.ToList());
        }


        public JsonResult GetMaps() {
             var maps = db.Maps.ToList();
            return new JsonResult{ Data = maps, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
        }
        // GET: Maps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // GET: Maps/Create
        [Authorize(Roles = "Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maps/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Latitude,Longitude")] Map map)
        {
            if (ModelState.IsValid)
            {
                db.Maps.Add(map);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(map);
        }

        // GET: Maps/Edit/5
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // POST: Maps/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Latitude,Longitude")] Map map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(map);
        }

        // GET: Maps/Delete/5
        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // POST: Maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            Map map = db.Maps.Find(id);
            db.Maps.Remove(map);
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
