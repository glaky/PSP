using System;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using General.Util;

namespace PSP.general
{
    public partial class showBtk : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager scCur = ScriptManager.GetCurrent(this.Page);
            scCur.RegisterAsyncPostBackControl(imgBtnKontakte);
            hl_btk.NavigateUrl = "~/general/newBtk.aspx?patid=" + Session["patid"].ToString() + "&reason=new&id=" + Session["id"].ToString();
            if ((Session["role"].ToString() == "ass"))
            {
                hl_btk.Enabled = false;
                hl_btk.Visible = false;
            }
            if (!IsPostBack) mvShowKontakte.SetActiveView(vwShowKontakteNo);
        }

        protected void rpDksKontakt_ItemDataDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lb, lb1;
            HyperLink hl;
            Image img;
            TextBox tb;
            MultiView mv;
            View vw, vwno;

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType !=
              ListItemType.AlternatingItem)
                return;

            hl = (HyperLink)e.Item.FindControl("hlEdit");
            img = (Image)e.Item.FindControl("imView");
            hl.NavigateUrl = "~/general/newBtk.aspx?btkid=" + hl.NavigateUrl + "&reason=edit&patid=" + hl.ToolTip;
           

            lb1 = (Label)e.Item.FindControl("lbArt");
            mv = (MultiView)e.Item.FindControl("mvKntk");
            vw = (View)e.Item.FindControl("vwKntk");
            vwno = (View)e.Item.FindControl("vwKntkNo");
            if (lb1.Text != "Kontaktversuch") mv.SetActiveView(vw);
            else mv.SetActiveView(vwno);


            lb = (Label)e.Item.FindControl("lbStatus");

            if (lb.Text.ToString() == "Fortsetzung")
            {
                lb = (Label)e.Item.FindControl("lbStatusDatum");
                lb.Visible = false;
                lb = (Label)e.Item.FindControl("lbStatusDatumV");
                lb.Visible = false;
                lb = (Label)e.Item.FindControl("lbStatusG");
                lb.Visible = false;
                lb = (Label)e.Item.FindControl("lbStatusGV");
                lb.Visible = false;
            }

            if (lb.Text.ToString() == "Beginn")
            {
                lb = (Label)e.Item.FindControl("lbStatusG");
                lb.Visible = false;
                lb = (Label)e.Item.FindControl("lbStatusGV");
                lb.Visible = false;
            }

            lb = (Label)e.Item.FindControl("lbAbge");
            lb.Text = (lb.Text.ToString() == "Ja") ? "Abgeschlossen" : "Nicht abgeschlossen";
            if (lb.Text.ToString() == "Abgeschlossen")
            {
                img = (Image)e.Item.FindControl("imStatus");
                img.ImageUrl = "~/images/j.png";
                img.ToolTip = "Betreuungskontakt abgeschlossen";
                img = (Image)e.Item.FindControl("imView");
                img.ImageUrl = "~/images/lupe.png";
                img.ToolTip = "Alle Details";
            }
            else
            {
                img = (Image)e.Item.FindControl("imStatus");
                img.ImageUrl = "~/images/n.png";
                img.ToolTip = "Betreuungskontakt nicht abgeschlossen";
                img = (Image)e.Item.FindControl("imView");
                img.ImageUrl = "~/images/edit.png";
                img.ToolTip = "Betreuungskontakt bearbeiten";
            }

            if (Session["role"].ToString() == "ass")
            {
                img = (Image)e.Item.FindControl("imView");
                img.ImageUrl = "~/images/lupe.png";
                img.ToolTip = "Alle Details";
            }

        }

        protected void imgBtn_Click(object sender, ImageClickEventArgs e)
        {
            View ss = mvShowKontakte.GetActiveView();
            if (ss == vwShowKontakteNo)
            {
                mvShowKontakte.SetActiveView(vwShowKontakte);
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_btkbypatid", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();

                if (!drConn.HasRows) mvKontakte.SetActiveView(vwNoKontakte);
                else mvKontakte.SetActiveView(vwKontakte);

                drConn.Close();
                cnConn.Close();
            }
            else mvShowKontakte.SetActiveView(vwShowKontakteNo);
            upKontakte.Update();
        }
    }
}