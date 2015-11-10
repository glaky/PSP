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

    public static class Utils
    {
        public static string CheckFullDate(string stDate, string stDirection)
        {
            string ErrorMessage = "";
            string stGenDate, stYear, stMonth, stDay;
            string str30Months = "0204060911";
            string strFeb = "30";
            DateTime dtDate, dtGenDate;

            if (stDate.Length < 10)
            {
                ErrorMessage = "Ungültiges Datum!";
                return (ErrorMessage);
            }
            stYear = stDate.Substring(0, 4);
            stMonth = stDate.Substring(5, 2);
            stDay = stDate.Substring(8, 2);

            if (stDay == "??")
            {
                if ((stMonth == "??") && (stYear == "????")) return (ErrorMessage);
                if ((stMonth == "??") ^ (stYear == "????"))
                {
                    ErrorMessage = "Ungültiges Datum!";
                    return (ErrorMessage);
                }
                else
                {
                    stDay = DateTime.Now.ToString("dd").ToString();
                }
            }

            if (!(DateTime.TryParseExact(stDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDate)))
            {
                ErrorMessage = "Ungültiges Datum!";
                return (ErrorMessage);
            }

            stGenDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            stGenDate = stGenDate.Substring(0, 10);
            dtGenDate = DateTime.ParseExact(stGenDate, "yyyy-MM-dd", null);


            if ((dtDate > dtGenDate) && (stDirection == "Back"))
            {
                ErrorMessage = "Datum in der Zukunft nicht möglich!";
                return (ErrorMessage);
            }

            if ((dtDate < dtGenDate) && (stDirection == "Forward"))
            {
                ErrorMessage = "Datum in der Vergangenheit nicht möglich!";
                return (ErrorMessage);
            }


            if ((!(DateTime.IsLeapYear(Convert.ToInt32(stYear)))) && (stDay == "29") && (stMonth == "02"))
            {
                ErrorMessage = stYear + " ist kein Schaltjahr!";
                return (ErrorMessage);
            }

            if ((stDay == "31") && (str30Months.Contains(stMonth)))
            {
                ErrorMessage = "Ungültiges Datum!";
                return (ErrorMessage);

            }

            if ((strFeb.Contains(stDay)) && (stMonth == "02"))
            {
                ErrorMessage = "Ungültiges Datum!";
                return (ErrorMessage);
            }


            try
            {
                return (ErrorMessage); ;
            }
            catch
            {

            }
            finally
            {

            }
            return (ErrorMessage);
        }
    }

    public static class DBTools
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["SqlDb"].ConnectionString;

        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }


    }

    public static class DBToolsOld
    {
        private static readonly string connectionStringOld = ConfigurationManager.ConnectionStrings["SqlDbOld"].ConnectionString;

        public static SqlConnection getConnection()
        {
            return new SqlConnection(connectionStringOld);
        }


    }

    public static class SecurityHelper
    {

        public static bool isLog(string login)
        {
            if (login == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    public static class msdbEmail
    {

        public static void SentEmail(string email, string from, string to, string cc, string subject, string msg)
        {

            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("tecficare@biogenidec.com", "Tecficare Service");

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
            if (subject.ToString() == "TecfiCare Service::Neues Konto") msg = String.Concat(msg, "\n\nhttps://studienserver.at/msdatabase/pat/default_pat.aspx");
            else msg = String.Concat(msg, "\n\nhttps://studienserver.at/tecficare/default.aspx");
            message.Body = msg;
            message.IsBodyHtml = false;
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
        public static void SentEmailAttach(string email, string from, string to, string cc, string subject, string msg, ArrayList attach)
        {

            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("tecficare@biogenidec.com", "Tecficare Service");
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
            msg = String.Concat(msg, "\n\nhttps://studienserver.at/PSP/default.aspx");
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



    public static class genSettings
    {
        public static void setHeader(Label lbRole, string stRole, Label lbTitel, string stTitel, Label lbName, string stName, Label lbForename, string stForename)
        {


            switch (stRole)
            {
                case "nrs":
                    lbRole.Text = "Nurse Service";
                    break;
                case "dkso":
                    lbRole.Text = "Nurse Service Ost";
                    break;
                case "dksw":
                    lbRole.Text = "Nurse Service West";
                    break;
                case "msc":
                    lbRole.Text = "MS-Coach";
                    break;
                case "adm":
                    lbRole.Text = "Administrator";
                    break;
                case "ass":
                    lbRole.Text = "Medizinische Assistenz";
                    break;
                case "pat":
                    lbRole.Text = "Patient";
                    break;
                case "sec":
                    lbRole.Text = "Service Center";
                    break;
                default:
                    break;
            }
            lbTitel.Text = stTitel;
            lbName.Text = stName;
            lbForename.Text = stForename;
            return;
        }

        public static string get_pat_onka(string stRole)
        {
            string stPatids = "%";
            SqlDataReader drConn1;
            SqlDataReader drConn2;
            SqlCommand cmdConn1;
            SqlCommand cmdConn2;
            bool bIsOnKa = true;
            string patid1, patid2, source1, source2, status;

            if (stRole == "Alle") stRole = "";

            SqlConnection cnConn1 = DBTools.getConnection();
            SqlConnection cnConn2 = DBTools.getConnection();
            cnConn1.Open();
            cnConn2.Open();
            cmdConn1 = new SqlCommand("sp_get_patienten", cnConn1);
            cmdConn1.CommandType = CommandType.StoredProcedure;
            cmdConn2 = new SqlCommand("sp_get_dkskontakts", cnConn2);
            cmdConn2.CommandType = CommandType.StoredProcedure;
            drConn1 = cmdConn1.ExecuteReader();
            if (stRole == "nofilter")
            {
                while (drConn1.Read()) stPatids = stPatids + "_" + drConn1["patid"].ToString() + "_,";
                drConn1.Close();
                cnConn1.Close();
                cnConn2.Close();
                stPatids = stPatids.Substring(0, stPatids.Length - 1);
                stPatids = stPatids + "%";
                return stPatids;
            }

            while (drConn1.Read())
            {
                patid1 = drConn1["patid"].ToString();
                bIsOnKa = true;
                drConn2 = cmdConn2.ExecuteReader();
                while (drConn2.Read())
                {
                    patid2 = drConn2["patid"].ToString();
                    source2 = drConn2["source"].ToString();
                    status = drConn2["status"].ToString();
                    if (drConn1["patid"].ToString() == drConn2["patid"].ToString())
                    {
                        if (drConn2["source"] != DBNull.Value)
                            if (drConn2["source"].ToString().Contains(stRole))
                                if (!(drConn2["status"].ToString().Contains("Noch kein DKS-Kontakt"))) bIsOnKa = false;
                    }
                }
                drConn2.Close();
                if (!bIsOnKa) stPatids = stPatids + "_" + drConn1["patid"].ToString() + "_,";
            }
            drConn1.Close();
            cnConn1.Close();
            cnConn2.Close();
            stPatids = stPatids.Substring(0, stPatids.Length - 1);
            stPatids = stPatids + "%";
            return stPatids;
        }
    }



    public static class manageAuditTrail
    {

        public static string[] get_fields(string which_sp, int which_entry, string which_id)
        {
            System.Collections.ArrayList al = new ArrayList();
            int i;
            string fieldname;

            SqlConnection cnConn = DBTools.getConnection();
            cnConn.Open();
            SqlCommand cmdConn = new SqlCommand(which_sp, cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            cmdConn.Parameters.Add(new SqlParameter(which_id, which_entry));
            SqlDataReader drConn = cmdConn.ExecuteReader();
            drConn.Read();
            for (i = 0; i < drConn.FieldCount; i++)
            {
                fieldname = drConn.GetName(i);
                al.Add((string)fieldname);
            }
            drConn.Close();
            cnConn.Close();

            return (string[])al.ToArray(typeof(string));
        }

        public static string[] get_entry(string which_sp, int which_entry, string which_id)
        {
            System.Collections.ArrayList al = new ArrayList();
            int i;
            string fieldname;

            SqlConnection cnConn = DBTools.getConnection();
            cnConn.Open();
            SqlCommand cmdConn = new SqlCommand(which_sp, cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            cmdConn.Parameters.Add(new SqlParameter(which_id, which_entry));
            SqlDataReader drConn = cmdConn.ExecuteReader();
            drConn.Read();
            for (i = 0; i < drConn.FieldCount; i++)
            {
                fieldname = drConn.GetName(i);
                al.Add((string)drConn[fieldname].ToString());
            }
            drConn.Close();
            cnConn.Close();

            return (string[])al.ToArray(typeof(string));
        }

        public static void audit_changes(string[] oldEntry, string[] newEntry, string[] fieldName, int userID, int tableID, int entryID)
        {
            SqlParameter oldValue, newValue;
            int j = 1;
            for (int i = 0; i < oldEntry.Length; i++)
            {
                if (oldEntry[i].ToString() != newEntry[i].ToString())
                {
                    oldValue = new SqlParameter("@OldValue", SqlDbType.NVarChar);
                    newValue = new SqlParameter("@NewValue", SqlDbType.NVarChar);
                    oldValue.Size = 500;
                    newValue.Size = 500;
                    oldValue.Value = oldEntry[i].ToString();
                    newValue.Value = newEntry[i].ToString();
                    IAuditTrailEntryWithValues entry = new AuditTrailEntryWithValues(fieldName[i], tableID, entryID, oldValue, newValue);
                    entry.UserId = userID;
                    entry.SecondaryObjectId = entryID;
                    AuditTrailManager.AddEntry(entry);
                }
            }

        }



    }




}