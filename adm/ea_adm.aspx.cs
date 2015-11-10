using System;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using General.Util;
using CAM.PasswordGeneratorLibrary;
using System.Net.Mail;


public partial class adm_PSP : System.Web.UI.Page
{

    static string[] oldEntry;
    static string[] newEntry;
    static string[] fieldName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
        if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");

        Session["ID"] = Convert.ToInt32(Request["ID"]);
        genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());
        if (!IsPostBack)
        {
            Session["prevpage"] = Request.UrlReferrer.ToString();

            ArrayList role = new ArrayList();
            role.Add("Administrator");
            /* role.Add("Rezeption"); */
            role.Add("Nurse Service");
            /* role.Add("Medizinische Information"); */
            role.Add("Service Center");
            role.Add("Medizinische Assistenz");
            /*role.Add("MS-Coach");*/
            ddl_role.DataSource = role;
            ddl_role.DataBind();

            ArrayList title = new ArrayList();
            title.Add("");
            title.Add("Dr.");
            title.Add("Dr. Dr.");
            title.Add("Sr.");
            title.Add("OSr.");
            title.Add("Mag.");
            title.Add("Dipl.Ing.");
            title.Add("Mag.(FH)");
            title.Add("Dipl.Ing.(FH)");
            title.Add("Ing.");
            ddl_title.DataSource = title;
            ddl_title.DataBind();


            if (Request["reason"] == "edit")
            {

                lbTitle.Text = "Kontodaten bearbeiten";

                fieldName = manageAuditTrail.get_fields("dbo.get_account", Convert.ToInt32(Request["ID"]), "@id");
                oldEntry = manageAuditTrail.get_entry("dbo.get_account", Convert.ToInt32(Request["ID"]), "@id");

                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("dbo.get_account", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@id", Convert.ToInt32(Request["ID"])));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();
                tbAccount.Text = drConn["account"].ToString();
                tbName.Text = drConn["name"].ToString();
                tbSurname.Text = drConn["forename"].ToString();
                tbPwd.Attributes.Add("value", drConn["pwd"].ToString());
                tbPwdC.Attributes.Add("value", drConn["pwd"].ToString());
                for (int i = 0; i < ddl_title.Items.Count; i++)
                {
                    if (ddl_title.Items[i].Value.ToString() == drConn["title"].ToString())
                    {
                        ddl_title.Items[i].Selected = true;
                    }
                }
                string stRole = "";
                switch (drConn["role"].ToString())
                {
                    case "rez":
                        stRole = "Rezeption";
                        break;
                    case "dkso":
                        stRole = "Nurse Service";
                        for (int i = 0; i < ddl_role.Items.Count; i++)
                        {
                            if (ddl_role.Items[i].Value.ToString() == stRole)
                            {
                                ddl_role.Items[i].Selected = true;
                            }
                        }
                        break;
                    case "dksw":
                        stRole = "Nurse Service";
                        for (int i = 0; i < ddl_role.Items.Count; i++)
                        {
                            if (ddl_role.Items[i].Value.ToString() == stRole)
                            {
                                ddl_role.Items[i].Selected = true;
                            }
                        }
                        break;
                    case "adm":
                        stRole = "Administrator";
                        break;
                    case "sec":
                        stRole = "Service Center";
                        for (int i = 0; i < ddl_role.Items.Count; i++)
                        {
                            if (ddl_role.Items[i].Value.ToString() == stRole)
                            {
                                ddl_role.Items[i].Selected = true;
                            }
                        }
                        break;
                    case "ass":
                        stRole = "Medizinische Assistenz";
                        for (int i = 0; i < ddl_role.Items.Count; i++)
                        {
                            if (ddl_role.Items[i].Value.ToString() == stRole)
                            {
                                ddl_role.Items[i].Selected = true;
                            }
                        }
                        break;
                        break;
                    default:
                        break;
                }

                tbPhone.Text = drConn["phone"].ToString();
                tbFax.Text = drConn["fax"].ToString();
                tbEmail.Text = drConn["email"].ToString();
                string stAktiv = drConn["status"].ToString();

                switch (stAktiv)
                {
                    case "J":
                        rbAktiv.Items[0].Selected = true;
                        break;
                    case "N":
                        rbAktiv.Items[1].Selected = true;
                        break;
                    default:
                        break;
                }
                Session["gendate"] = drConn["gendate"].ToString();
                Session["ID"] = drConn["id"].ToString();
                drConn.Close();
                cnConn.Close();
            }
        }
    }

    protected void cvValKonto_ServerValidate(object source, ServerValidateEventArgs args)
    {
        SqlConnection cnConn;
        SqlCommand cmdConn;
        SqlDataReader drConn;

        if (Request["reason"] == "new")
        {
            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdConn = new SqlCommand("dbo.get_accountname", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            cmdConn.Parameters.Add(new SqlParameter("@account", tbAccount.Text.ToString()));
            drConn = cmdConn.ExecuteReader();
            if (drConn.HasRows) args.IsValid = false;
            drConn.Close();
            cnConn.Close();
        }
        else args.IsValid = true;

    }

    protected void bt_save_click(object sender, EventArgs e)
    {
        string stRole;
        if (Page.IsValid)
        {
            string stName = tbName.Text.ToString();
            string stSurname = tbSurname.Text.ToString();
            string stAktiv = rbAktiv.SelectedItem.Value.ToString();
            stRole = ddl_role.SelectedItem.Text.ToString();
            string stTitle = ddl_title.SelectedItem.Text.ToString();
            string stAccount = tbAccount.Text.ToString();
            string stPhone = tbPhone.Text.ToString();
            string stFax = tbFax.Text.ToString();
            string stEmail = tbEmail.Text.ToString();
            string stPwd = tbPwd.Text.ToString();
            string stProcDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string stGenDate;

            switch (stAktiv)
            {
                case "Ja":
                    stAktiv = "J";
                    break;
                case "Nein":
                    stAktiv = "N";
                    break;
                default:
                    break;
            }

            string stRoleE = stRole;

            switch (stRole)
            {
                case "Rezeption":
                    stRole = "rez";
                    break;
                case "Nurse Service Ost":
                    stRole = "dkso";
                    break;
                case "Nurse Service West":
                    stRole = "dksw";
                    break;
                case "MS-Coach":
                    stRole = "msc";
                    break;
                case "Administrator":
                    stRole = "adm";
                    break;
                case "Medizinische Assistenz":
                    stRole = "ass";
                    break;
                case "Medizinische Information":
                    stRole = "mei";
                    break;
                case "Service Center":
                    stRole = "sec";
                    break;
                default:
                    break;
            }


            if (Request["reason"] == "new")
            {
                stGenDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("dbo.insert_account", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@name", stName));
                cmdConn.Parameters.Add(new SqlParameter("@forename", stSurname));
                cmdConn.Parameters.Add(new SqlParameter("@status", stAktiv));
                cmdConn.Parameters.Add(new SqlParameter("@role", stRole));
                cmdConn.Parameters.Add(new SqlParameter("@account", stAccount));
                cmdConn.Parameters.Add(new SqlParameter("@fax", stFax));
                cmdConn.Parameters.Add(new SqlParameter("@phone", stPhone));
                cmdConn.Parameters.Add(new SqlParameter("@email", stEmail));
                cmdConn.Parameters.Add(new SqlParameter("@pwd", stPwd));
                cmdConn.Parameters.Add(new SqlParameter("@gendate", stGenDate));
                cmdConn.Parameters.Add(new SqlParameter("@title", stTitle));
                cmdConn.ExecuteNonQuery();
                cnConn.Close();

                string email = stEmail;
                string to = stEmail;
                string cc = "glaky@gmx.at";
                string from = "system@studienserver.at";
                string subject = "PSP::Neues Konto";
                string msgb = "Sehr geehrter Benutzer!";
                msgb = String.Concat(msgb, "\nFür Sie wurde bei PSP ein Konto angelegt. ");
                msgb = String.Concat(msgb, "\n\nBenutzername: " + stAccount);
                msgb = String.Concat(msgb, "\nKennwort: ", stPwd);
                msgb = String.Concat(msgb, "\n\nRolle: ", stRoleE);
                msgb = String.Concat(msgb, "\nTelefon: ", stPhone);
                msgb = String.Concat(msgb, "\nE-Mail: ", stEmail);
                msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen");
                msgb = String.Concat(msgb, "\n\nPSP Systemadministration");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("system@studienserver.at", "PSP Administration");

                /*msdbEmail.SentEmail(email, from, to, cc, subject, msgb);*/

                Response.Redirect("~/adm/menu_adm.aspx");
            }

            if (Request["reason"] == "edit")
            {
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("dbo.update_account", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@id", Session["ID"]));
                cmdConn.Parameters.Add(new SqlParameter("@name", stName));
                cmdConn.Parameters.Add(new SqlParameter("@forename", stSurname));
                cmdConn.Parameters.Add(new SqlParameter("@status", stAktiv));
                cmdConn.Parameters.Add(new SqlParameter("@role", stRole));
                cmdConn.Parameters.Add(new SqlParameter("@account", stAccount));
                cmdConn.Parameters.Add(new SqlParameter("@fax", stFax));
                cmdConn.Parameters.Add(new SqlParameter("@phone", stPhone));
                cmdConn.Parameters.Add(new SqlParameter("@email", stEmail));
                cmdConn.Parameters.Add(new SqlParameter("@pwd", stPwd));
                cmdConn.Parameters.Add(new SqlParameter("@title", stTitle));
                cmdConn.Parameters.Add(new SqlParameter("@gendate", Session["gendate"].ToString()));
                cmdConn.ExecuteNonQuery();
                cnConn.Close();

                newEntry = manageAuditTrail.get_entry("dbo.get_account", Convert.ToInt32(Request["ID"]), "@id");
                manageAuditTrail.audit_changes(oldEntry, newEntry, fieldName, Convert.ToInt32(Session["userid"]), 1, Convert.ToInt32(Request["ID"]));

                string email = stEmail;
                string to = stEmail;
                string cc = "glaky@gmx.at";
                string from = "system@studienserver.at";
                string subject = "PSP::Kontodaten neu gespeichert";
                string msgb = "Sehr geehrter Benutzer!";
                msgb = String.Concat(msgb, "\nIhre Kontodaten bei PSP wurden neu gepeichert.");
                msgb = String.Concat(msgb, "\n\nBenutzername: " + stAccount);
                msgb = String.Concat(msgb, "\nKennwort: ", stPwd);
                msgb = String.Concat(msgb, "\n\nRolle: ", stRoleE);
                msgb = String.Concat(msgb, "\nTelefon: ", stPhone);
                msgb = String.Concat(msgb, "\nE-Mail: ", stEmail);
                msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen");
                msgb = String.Concat(msgb, "\n\nPSP Systemadministration");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("system@studienserver.at", "MSdatabase Administrator");

                /*msdbEmail.SentEmail(email, from, to, cc, subject, msgb);*/

                Response.Redirect("~/adm/menu_adm.aspx");
            }

        }
    }
    protected void bt_cancel_click(object sender, EventArgs e)
    {
        object referrer = Session["prevpage"];
        if (referrer != null)
            Response.Redirect("~/adm/menu_adm.aspx");
        else
            Response.Redirect("Default.aspx");
    }
    protected void btAccount_Click(object sender, EventArgs e)
    {
        tbAccount.Text = tbName.Text + "." + tbSurname.Text;
    }
    protected void btPwd_Click(object sender, EventArgs e)
    {
        PasswordGenerator pwdGen = new RandomPasswordGenerator(PasswordOptions.Numbers | PasswordOptions.LowercaseCharacters);
        string pawd = pwdGen.Generate(6).ToString();
        tbPwd.Attributes.Add("value", pawd);
        tbPwdC.Attributes.Add("value", pawd);
    }




}
