using System;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using General.Util;
using System.Collections;

namespace PSP.general
{
    public partial class menu_adm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());

            txFNameV.Text = txFName.Text.ToString() + "%";
            txFRoleV.Text = ddlFRole.SelectedValue.ToString();
            switch (txFRoleV.Text)
            {
                case "Alle":
                    txFRoleV.Text = "%";
                    break;
                case "Administrator":
                    txFRoleV.Text = "adm";
                    break;
                case "Rezeption":
                    txFRoleV.Text = "rez";
                    break;
                case "Nurse Service":
                    txFRoleV.Text = "nrs";
                    break;
                case "Medizinische Assistenz":
                    txFRoleV.Text = "ass";
                    break;
                case "Service Center":
                    txFRoleV.Text = "sec";
                    break;
                default:
                    break;
            }

            if (!IsPostBack)
            {
                Session["prevpage"] = Request.UrlReferrer.ToString();
                switch (Session["role"].ToString())
                {
                    case "adm":
                        mvMenu.SetActiveView(vwAdm);
                        break;
                    case "sec":
                        mvMenu.SetActiveView(vwSec);
                        break;
                    case "nrs":
                        mvMenu.SetActiveView(vwNrs);
                        break;
                    default:
                        break;
                }

            }
        }

        protected void gv_acclist_rdb(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hl_delete;
                Image img_delete, img_status;

                img_status = (Image)e.Row.FindControl("img_status");

                Label lbRolle = (Label)e.Row.FindControl("lbRolle");
                switch (lbRolle.Text)
                {
                    case "arz":
                        lbRolle.Text = "Arzt";
                        break;
                    case "nrs":
                        lbRolle.Text = "Nurse Service";
                        break;
                    case "rez":
                        lbRolle.Text = "Rezeption";
                        break;
                    case "ass":
                        lbRolle.Text = "Medizinische Assistenz";
                        break;
                    case "adm":
                        lbRolle.Text = "Administrator";
                        break;
                    case "sec":
                        lbRolle.Text = "Service Center";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}