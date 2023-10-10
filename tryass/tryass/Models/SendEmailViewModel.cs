using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryass.Models
{


    public class SendEmailViewModel
    {
        public List<UserEmailModel> Users { get; set; } = new List<UserEmailModel>();
        public List<string> SelectedUsers { get; set; } = new List<string>();
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }
    }

    public class UserEmailModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsSelected { get; set; } // 在前端决定是否选中
    }

}