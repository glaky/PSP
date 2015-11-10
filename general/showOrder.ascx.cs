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
    public partial class showOrder : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scCur = ScriptManager.GetCurrent(this.Page);
            scCur.RegisterAsyncPostBackControl(imgBtnOrder);
            if (!IsPostBack) mvShowOrder.SetActiveView(vwShowOrderNo);
            if (Session["role"].ToString() == "ass")
            {
                hl_order.Enabled = false;
                hl_order.Visible = false;
            }
            hl_order.NavigateUrl = "~/general/newOrder.aspx?patid=" + Session["patid"].ToString() + "&typ=p";
            if ((Session["role"].ToString() == "ass"))
            {
                hl_order.Enabled = false;
                hl_order.Visible = false;
            }
        }

        protected void rpBst_ItemDataDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lb1;
            MultiView mv;
            View vw1, vw2;
            HyperLink hl;

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            lb1 = (Label)e.Item.FindControl("lbQuelle");

            hl = (HyperLink)e.Item.FindControl("hlPrint");
            hl.NavigateUrl = "~/pdf_orders/" + hl.NavigateUrl;

            /*lb1 = (Label)e.Item.FindControl("lbKb");
            mv = (MultiView)e.Item.FindControl("mvKb");
            vw1 = (View)e.Item.FindControl("vwNoKb");
            vw2 = (View)e.Item.FindControl("vwKb");

            if (lb1.Text == "0")
            {
                mv.SetActiveView(vw1);
            }
            else
            {
                mv.SetActiveView(vw2);
            }*/

        }

        protected void imgBtn_Click(object sender, ImageClickEventArgs e)
        {
            View ss = mvShowOrder.GetActiveView();
            if (ss == vwShowOrderNo)
            {
                mvShowOrder.SetActiveView(vwShowOrder);
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_ordersbypatid", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();

                if (drConn.HasRows)
                {
                    mvOrder.SetActiveView(vwOrder);
                }
                else
                {
                    mvOrder.SetActiveView(vwNoOrder);
                }
                drConn.Close();
                cnConn.Close();
            }
            else mvShowOrder.SetActiveView(vwShowOrderNo);
            upOrder.Update();
        }
    }
}