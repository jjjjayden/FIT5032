using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.Configuration;

namespace tryass.Utils
{


    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public async Task SendAsync(String toEmail, String subject, String contents)
        {
            try
            {
                var client = new SendGridClient(API_KEY);
                var from = new EmailAddress("zjin7593@gmail.com", "FIT5032 Example Email User");
                var to = new EmailAddress(toEmail, "");
                var plainTextContent = contents;
                var htmlContent = "<p>" + contents + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg); 

                if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    throw new Exception("邮件发送失败，错误码：" + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("邮件发送出现异常: " + ex.Message);
            }
        }


    }
}
