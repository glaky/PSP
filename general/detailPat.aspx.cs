using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using General.Util;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;

namespace PSP.general
{
    public partial class detailPat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string tel, email;

            Session["PatID"] = Convert.ToInt32(Request["PatID"]);
            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());

            if (!IsPostBack)
            {
                Session["prevpage"] = Request.UrlReferrer.ToString();
                switch (Session["role"].ToString())
                {
                    /*case "rez":
                        mvMenu.SetActiveView(vwRez);
                        break;
                    case "dkso":
                    case "dksw":
                        mvMenu.SetActiveView(vwDks);
                        break;*/
                    case "nrs":
                        mvMenu.SetActiveView(vwNrs);
                        break;
                    case "sec":
                        mvMenu.SetActiveView(vwSec);
                        break;
                    case "ass":
                        mvMenu.SetActiveView(vwAss);
                        break;
                    default:
                        break;
                }

                SqlDataReader drConn;
                SqlCommand cmdConn;

                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                cmdConn = new SqlCommand("get_patdat", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                drConn = cmdConn.ExecuteReader();
                drConn.Read();
                switch (drConn["medikament"].ToString())
                {
                    case "Plegridy":
                        {
                            medView.Style.Add("background-color", "#99ccff");
                            medView1.Style.Add("background-color", "#99ccff");
                            lbMedIndicator.Text = "Plegridy";
                            lbMedIndicator1.Text = "Plegridy";
                            break;
                        }
                    case "Tecfidera":
                        {
                            medView.Style.Add("background-color", "#ff66ff");
                            medView1.Style.Add("background-color", "#ff66ff");
                            lbMedIndicator.Text = "Tecfidera";
                            lbMedIndicator1.Text = "Tecfidera";
                            break;
                        }
                    case "Avonex":
                        {
                            medView.Style.Add("background-color", "#ff9900");
                            medView1.Style.Add("background-color", "#ff9900");
                            lbMedIndicator.Text = "Avonex";
                            lbMedIndicator1.Text = "Avonex";
                            break;
                        }
                    default: break;
                }
                mvMedInd.SetActiveView(vwMedInd);
                mvMedInd1.SetActiveView(vwMedInd1);
            }
        }

        public void bt_uber_click(object sender, System.EventArgs e)
        {

            string reurl = "~/general/overviewPat.aspx";
            Response.Redirect(reurl);
            /*object referrer = Session["prevpage"];
            if (referrer != null)
              Response.Redirect("~/dks/menu_dks.aspx");
            else
              Response.Redirect("~/Default.aspx");
             * */
        }

        public void bt_termin_click(object sender, System.EventArgs e)
        {

            string reurl = "~/general/schedulePat.aspx";
            Response.Redirect(reurl);
            /*object referrer = Session["prevpage"];
            if (referrer != null)
              Response.Redirect("~/dks/menu_dks.aspx");
            else
              Response.Redirect("~/Default.aspx");
             * */
        }

        public void bt_offen_click(object sender, System.EventArgs e)
        {

            string reurl = "~/general/pat_offen.aspx";
            Response.Redirect(reurl);
            /*object referrer = Session["prevpage"];
            if (referrer != null)
              Response.Redirect("~/dks/menu_dks.aspx");
            else
              Response.Redirect("~/Default.aspx");
             * */
        }
    }
}