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

namespace TecfiCare.general
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_patdat", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();
                lbPNameV.Text = drConn["name"].ToString();
                lbPVornameV.Text = drConn["vorname"].ToString();
                lbGebdatV.Text = drConn["gebdat"].ToString();
                lbErrei.Text = "Erreichbarkeit:&nbsp;" + drConn["erreichbarkeit"].ToString();
                lbTheStart.Text = "Start der Therapie (MM.JJJJ):&nbsp;" + drConn["thestart"].ToString();


                switch (drConn["geschlecht"].ToString())
                {
                    case "M":
                        {
                            lbGeschlechtV.Text = "M&auml;nnlich";
                            break;
                        }
                    case "W":
                        {
                            lbGeschlechtV.Text = "Weiblich";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }


                lbAdresseV.Text = drConn["adresse"].ToString();
                lbPlzOrt.Text = drConn["plz"].ToString() + "&nbsp;" + drConn["ort"].ToString();
                lbFestnetz.Text = drConn["tel"].ToString();
                lbEmail.Text = drConn["email"].ToString();

                drConn.Close();
                cnConn.Close();
            }
        }
    }
}