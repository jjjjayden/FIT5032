using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tryass.Models;

namespace tryass.Controllers
{
    public class XrayImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // for user return users image
        //for doctor and admin return all suers images
        // GET: XrayImages
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();


            if (User.IsInRole("Staff") || User.IsInRole("Doctor"))
            {
                return View(db.XrayImages.ToList()); 
            }
            else
            {
                return View(db.XrayImages.Where(x => x.UserId == userId).ToList()); 
            }
        }



        // GET: XrayImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XrayImage xrayImage = db.XrayImages.Find(id);
            if (xrayImage == null)
            {
                return HttpNotFound();
            }
            return View(xrayImage);
        }

        // GET: XrayImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: XrayImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase ImageFile, XrayImage xrayImage)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Saving files to the server
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/UploadedImages/"), fileName);
                    ImageFile.SaveAs(path);

                    xrayImage.ImageUrl = "/Content/UploadedImages/" + fileName;
                    xrayImage.UploadDate = DateTime.Now;

                    // Set the UserId to the ID of the currently logged-in user
                    xrayImage.UserId = User.Identity.GetUserId();

                    db.XrayImages.Add(xrayImage);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(xrayImage);
        }



        // GET: XrayImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XrayImage xrayImage = db.XrayImages.Find(id);
            if (xrayImage == null)
            {
                return HttpNotFound();
            }
            return View(xrayImage);
        }

        // POST: XrayImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageUrl,UploadDate,UserId")] XrayImage xrayImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xrayImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(xrayImage);
        }

        // GET: XrayImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XrayImage xrayImage = db.XrayImages.Find(id);
            if (xrayImage == null)
            {
                return HttpNotFound();
            }
            return View(xrayImage);
        }

        // POST: XrayImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            XrayImage xrayImage = db.XrayImages.Find(id);
            db.XrayImages.Remove(xrayImage);
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
