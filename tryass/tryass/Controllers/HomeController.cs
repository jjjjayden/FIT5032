using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tryass.Models;
using tryass.Utils;
using Xceed.Document.NET;

namespace tryass.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Send_Email()
        {
            var model = new SendEmailViewModel();
            model.Users = db.Users.Select(u => new UserEmailModel
            {
                Id = u.Id,
                Email = u.Email,
                IsSelected = false
            }).ToList();

            model.AvailableTemplates = db.EmailTemplates.ToList();  

            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Send_Email(SendEmailViewModel model)
        {

            if (ModelState.IsValid)
            {

                model.AvailableTemplates = db.EmailTemplates.ToList();
                model.SelectedUsers = model.Users.Where(u => u.IsSelected).Select(u => u.Id).ToList();

                var selectedEmails = db.Users.Where(u => model.SelectedUsers.Contains(u.Id))
                    .Select(u => u.Email).ToList();

                foreach (var email in selectedEmails)
                {
                    EmailSender es = new EmailSender();
                    await es.SendAsync(email, model.Subject, model.Contents, model.Attachment);
                }


                ViewBag.Result = "Successful send!";


                model = new SendEmailViewModel();
                model.Users = db.Users.Select(u => new UserEmailModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    IsSelected = false
                }).ToList();

                return View(model);
            }


            model.Users = db.Users.Select(u => new UserEmailModel
            {
                Id = u.Id,
                Email = u.Email,
                IsSelected = false
            }).ToList();

            return View(model);
        }

    }


}