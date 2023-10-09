using Microsoft.AspNet.Identity;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using tryass.Models;
using DinkToPdf;
using Xceed.Words.NET;
using System.Data.Entity;


namespace tryass.Controllers
{
    public class AnnotationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Annotations
        [Authorize]
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

        public ActionResult DownloadReport(int id, string format)
        {
            var annotation = db.Annotation.Include(a => a.XrayImage).FirstOrDefault(a => a.Id == id);
            if (annotation == null)
            {
                return HttpNotFound();
            }

            if (format.ToLower() == "pdf")
            {
                // Rotativa
                return new Rotativa.ViewAsPdf("ReportForPdf", annotation) { FileName = "report.pdf" };
            }
            else if (format.ToLower() == "word")
            {
                using (var doc = DocX.Create("report.docx"))
                {
                    doc.InsertParagraph("User: " + annotation.UserId);
                    doc.InsertParagraph("Comment: " + annotation.Comment);
                    doc.Save();
                }
                return File(System.IO.File.ReadAllBytes("report.docx"), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "report.docx");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unsupported format");
        }


    }
}
