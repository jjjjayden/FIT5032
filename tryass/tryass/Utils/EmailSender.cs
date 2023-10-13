using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic; // For List<>
using System.Threading.Tasks;
using System.Configuration;

namespace tryass.Utils
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public async Task SendAsync(String toEmail, String subject, String contents, System.Web.HttpPostedFileBase attachment = null)
        {
            try
            {
                var client = new SendGridClient(API_KEY);
                var from = new EmailAddress("zjin7593@gmail.com", "FIT5032 Example Email User");
                var to = new EmailAddress(toEmail, "");
                var plainTextContent = contents;
                var htmlContent = "<p>" + contents + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                if (attachment != null && attachment.ContentLength > 0)
                {
                    var bytes = new byte[attachment.ContentLength];
                    attachment.InputStream.Read(bytes, 0, attachment.ContentLength);

                    var file = new SendGrid.Helpers.Mail.Attachment
                    {
                        Content = Convert.ToBase64String(bytes),
                        Filename = attachment.FileName,
                        Type = attachment.ContentType,
                        Disposition = "attachment", // This specifies the file as an attachment
                        ContentId = "Attachment"
                    };

                    if (msg.Attachments == null)
                    {
                        msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>();
                    }

                    msg.Attachments.Add(file);
                }


                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    throw new Exception("Wrong：" + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong: " + ex.Message);
            }
        }
    }
}
