using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Data;
using System.Web.Security;
using General.Util;
using DolphiCom.Data.AuditTrail;


namespace General.Util
{
    public static class DBTools
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["SqlDb"].ConnectionString;

        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }
    }

    public static class msdbEmail
    {

        public static void SentEmail(string email, string from, string to, string cc, string subject, string msg)
        {

            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from, "MS Nurse Service");

            if (to.IndexOf("|") != -1)
            {
                string[] emads = to.Split('|');
                foreach (string emad in emads)
                {
                    if (emad != "") message.To.Add(new MailAddress(emad));
                }
            }
            else
            {
                message.To.Add(new MailAddress(to));
            }

            if (cc.IndexOf("|") != -1)
            {
                string[] emads = cc.Split('|');
                foreach (string emad in emads)
                {
                    if (emad != "") message.CC.Add(new MailAddress(emad));
                }
            }
            else
            {
                if (cc != "") message.CC.Add(new MailAddress(cc));
            }

            message.Subject = subject;
            if (subject.ToString() == "MS Nurse Service::Neues Konto") msg = String.Concat(msg, "\n\nhttps://studienserver.at/msdatabase/pat/default_pat.aspx");
            else msg = String.Concat(msg, "\n\nhttps://studienserver.at/msdatabase/default.aspx");
            message.Body = msg;
            message.IsBodyHtml = false;


            SmtpClient emailClient = new SmtpClient(settings.Smtp.Network.Host);
            emailClient.Send(message);

            return;
        }
        public static void SentEmailAttach(string email, string from, string to, string cc, string subject, string msg, ArrayList attach)
        {

            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("gunter.laky@hermesoft.at", "MS Nurse Service");
            if (to.IndexOf("|") != -1)
            {
                string[] emads = to.Split('|');
                foreach (string emad in emads)
                {
                    if (emad != "") message.To.Add(new MailAddress(emad));
                }
            }
            else
            {
                message.To.Add(new MailAddress(to));
            }

            if (cc.IndexOf("|") != -1)
            {
                string[] emads = cc.Split('|');
                foreach (string emad in emads)
                {
                    if (emad != "") message.CC.Add(new MailAddress(emad));
                }
            }
            else
            {
                if (cc != "") message.CC.Add(new MailAddress(cc));
            }



            message.Subject = subject;
            msg = String.Concat(msg, "\n\nhttps://studienserver.at/msdatabase/default.aspx");
            message.Body = msg;
            message.IsBodyHtml = false;

            foreach (string af in attach)
            {
                message.Attachments.Add(new Attachment(af));
            }

            SmtpClient emailClient = new SmtpClient(settings.Smtp.Network.Host);
            try
            {
                emailClient.Send(message);
            }
            catch
            {
            }
            finally
            {

            }
            return;
        }
    }
        
}