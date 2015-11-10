using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using General.Util;
using System.Data.SqlClient;
using System.Data;


namespace PSP
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["login"] = "";
            Session["role"] = "";
            Session["name"] = "";
            Session["forename"] = "";
            tx_login.Focus();
            Session["tmsdate"] = DateTime.Now.ToString("yyyy-MM-dd");
            Session["tmedate"] = DateTime.Now.ToString("yyyy-MM-dd");
            Session["tmname"] = "";
            Session["tmid"] = "";
            Session["tmplz"] = "";
            Session["tmort"] = "";
            Session["tmlfd"] = "[ANBFW]";
            Session["tmintabs"] = "Kein Filter";
            Session["tmintervall"] = false;
            Session["tmmedikament"] = "[PTA]";
            Session["tmzustaendigkeit"] = "[NS]";
            Session["tmuber"] = true;
            Session["tmaaop"] = false;

            Session["ovname"] = "";
            Session["ovid"] = "";
            Session["ovplz"] = "";
            Session["ovort"] = "";
            Session["ovlfd"] = "[ANBFW]";
            Session["ovintabs"] = "Kein Filter";
            Session["ovintervall"] = false;
            Session["ovmedikament"] = "[PTA]";
            Session["ovzustaendigkeit"] = "[NS]";
            Session["ovWechsel"] = true;
            Session["aoop"] = false;


            Session["ofname"] = "";
            Session["ofid"] = "";
            Session["ofplz"] = "";
            Session["ofort"] = "";
            Session["ofowner"] = "";

        }

        public void bt_login_click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                switch (Session["role"].ToString())
                {
                     case "adm":
                        Response.Redirect("adm/menu_adm.aspx");
                        break;
                    case "sec":
                        Response.Redirect("~/general/overviewPat.aspx");
                        break;
                    case "nrs":
                        Response.Redirect("~/general/overviewPat.aspx");
                        break;
                    case "ass":
                        Response.Redirect("~/general/overviewPat.aspx");
                        break;
                    default:
                        break;
                }
            }
        }

        public void loginValDB_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string strLogin, strPwd;
            string strSql;

            strLogin = tx_login.Text;
            strPwd = tx_pwd.Text;
            strSql = "SELECT * from accounts WHERE account='" + strLogin + "'";

            SqlConnection cnConn = DBTools.getConnection();
            cnConn.Open();
            SqlCommand cmdConn = new SqlCommand("sp_login", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            cmdConn.Parameters.Add(new SqlParameter("@account", strLogin));
            try
            {
                SqlDataReader drConn = cmdConn.ExecuteReader();
                if (!(drConn.HasRows))
                {
                    args.IsValid = false;
                }
                else drConn.Read();
                if (drConn["aktiv"].ToString() == "N")
                {
                    args.IsValid = false;
                }
                drConn.Close();
                cnConn.Close();
            }
            catch (System.Exception excep)
            {
                Console.WriteLine(excep);
            }
            finally
            {

            }



        }

        public void pwdValDB_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string strLogin, strPwd;
            string strCmp;

            
            strLogin = tx_login.Text;
            strPwd = tx_pwd.Text;
            SqlConnection cnConn = DBTools.getConnection();
            cnConn.Open();
            SqlCommand cmdConn = new SqlCommand("dbo.sp_login", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            cmdConn.Parameters.Add(new SqlParameter("@account", strLogin));
            SqlDataReader drConn = cmdConn.ExecuteReader();

            if (!(drConn.HasRows))
            {
                args.IsValid = false;
            }
            else
            {
                drConn.Read();
                strCmp = drConn["pwd"].ToString();

                if (!(strCmp.Equals(strPwd)))
                {
                    args.IsValid = false;
                }
                else
                {
                    Session["login"] = drConn["account"].ToString();
                    Session["role"] = drConn["role"].ToString();
                    Session["name"] = drConn["name"].ToString();
                    Session["forename"] = drConn["forename"].ToString();
                    Session["titel"] = drConn["title"].ToString();
                    Session["id"] = Convert.ToInt32(drConn["id"]);
                    Session["userid"] = Convert.ToInt32(drConn["id"]);
                    Session["email"] = drConn["email"].ToString();
                    Session["phone"] = drConn["phone"].ToString();
                }

            }
            drConn.Close();
            cnConn.Close();
        }
    }
}