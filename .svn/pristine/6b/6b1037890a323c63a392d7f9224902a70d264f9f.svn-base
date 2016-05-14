using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoShare.BusinessLogic;
using PhotoShare.Dto;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public string totals(Guid folderId)
        {
            new GeneralLogic().UpdateTotalSales(folderId);
            return "worked";
        }

        public async Task<string> SendEmail()
        {
            EmailService emailService = new EmailService();
            var identityMessage = new IdentityMessage();
            identityMessage.Destination = "karenmitrueimage@gmail.com";

            var emailBody = string.Format(@"Test {0}", DateTime.Now);

            identityMessage.Body = emailBody;
            identityMessage.Subject = "Test";
            await emailService.SendAsync(identityMessage);
            return "worked";
        }
    }
}