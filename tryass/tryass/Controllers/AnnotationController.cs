using Microsoft.AspNet.Identity;
using System.Net;
using System.Linq;
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
            return View(db.Annotation.ToList());
        }


        // GET: Annotations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Annotations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Annotation annotation)
        {
            if (ModelState.IsValid)
            {
                db.Annotation.Add(annotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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

            return View(annotation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Annotation annotation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annotation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

            db.Annotation.Remove(annotation);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Annotations/AddComment/5
        public ActionResult AddComment(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int id, string comment)
        {
            Annotation annotation = db.Annotation.Find(id);
            if (annotation != null)
            {
                annotation.Comment = comment;
                db.Entry(annotation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
