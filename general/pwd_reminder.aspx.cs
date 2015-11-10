using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using General.Util;
using System.Net.Mail;

namespace TecfiCare
{
    public partial class _pwd_reminder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) mvStart.SetActiveView(vwStart);
        }

        public void pwdReminder_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string strLogin, strEmail, strPwd;
            string strCmp;

            strLogin = tx_login.Text;
            strEmail = tx_pwd.Text;

            SqlConnection cnConn = DBTools.getConnection();
            cnConn.Open();
            SqlCommand cmdConn = new SqlCommand("dbo.sp_login", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            cmdConn.Parameters.Add(new SqlParameter("@account", strLogin));
            SqlDataReader drConn = cmdConn.ExecuteReader();

            if (!(drConn.HasRows))
            {
                args.IsValid = false;
                loginValDB.ErrorMessage = "Unbekannte Benutzer/E-Mailaddress Kombination.";
            }
            else
            {
                drConn.Read();
                strCmp = drConn["email"].ToString();

                if (!(strCmp.Equals(strEmail)))
                {
                    args.IsValid = false;
                    loginValDB.ErrorMessage = "Unbekannte Benutzer/E-Mailaddress Kombination.";
                }
                else
                {
                    strPwd = drConn["pwd"].ToString();
                    string stEmail = drConn["email"].ToString();
                    string to = drConn["email"].ToString();
                    string subject = "TecfiCare::Kennworterinnerung";
                    string cc = "";
                    string from = "TecfiCare Administration";
                    string msgb = "Sehr geehrter Benutzer!";
                    msgb = String.Concat(msgb, "\nIhr Kennwort lautet: " + strPwd);
                    msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\nTecfiCare System");
                    MailMessage message = new MailMessage();
                    msdbEmail.SentEmail(stEmail, from, to, cc, subject, msgb);
                }

            }
            drConn.Close();
            cnConn.Close();

        }
        protected void bt_login_click(object sender, EventArgs e)
        {
            if (Page.IsValid) mvStart.SetActiveView(vwEnd);
        }
    }
}