using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var identityMessage = new IdentityMessage();
            identityMessage.Destination = "brendan.caylor@gmail.com";
            identityMessage.Body = $"{HttpContext.Current.Request.Url.OriginalString} {ex.Message}";
            identityMessage.Subject = "mi-photo error";
            SendEmail(identityMessage);
            Server.TransferRequest("~/error/NotWorking");
        }

        private void SendEmail(IdentityMessage message)
        {
            // Plug in your email service here to send an email.

            string routeAllEmails = ConfigurationManager.AppSettings["routeAllEmails"];

            // Credentials:

            string smtpServer = ConfigurationManager.AppSettings["EmailSmtpServer"];
            //int smtpPort = int.Parse(ConfigurationManager.AppSettings["EmailSmtpPort"]);
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EmailEnableSSL"]);
            string smtpUsername = ConfigurationManager.AppSettings["EmailSmtpUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["EmailSmtpPassword"];
            string sentFrom = ConfigurationManager.AppSettings["EmailSentFrom"];
            string port = ConfigurationManager.AppSettings["EmailPort"];

            // Configure the client:
            //var client = new SmtpClient(smtpServer, Convert.ToInt32(587));

            var client = new SmtpClient(smtpServer);

            if (!string.IsNullOrEmpty(port))
            {
                client.Port = int.Parse(port);
            }

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = enableSsl;
            client.Timeout = 200000;

            // Create the credentials:
            if (!string.IsNullOrEmpty(smtpPassword))
            {
                var credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.Credentials = credentials;
            }


            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            if (routeAllEmails != string.Empty)
            {
                mail.To.Clear();
                mail.To.Add(new MailAddress(routeAllEmails));
            }
            mail.From = new MailAddress(sentFrom);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.Bcc.Add("brendan.caylor@gmail.com");
            client.Send(mail);
        }
    }
}
