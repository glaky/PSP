using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using General.Util;
namespace PSP
{
    public partial class confirm_exports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbSurname, Session["forename"].ToString());
        }
        protected void bt_confirm_click(object sender, EventArgs e)
        {
            Response.Redirect("~/general/overviewPat.aspx");

        }
    }
}