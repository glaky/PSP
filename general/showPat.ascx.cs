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
    public partial class showPat : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            hl_pers.NavigateUrl = "~/general/newPat.aspx?patid=" + Session["patid"].ToString() + "&typ=edit";
            if ((Session["role"].ToString() == "ass"))
            {
                hl_pers.Enabled = false;
                hl_pers.Visible = false;
            }

            if (!IsPostBack)
            {
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_patdat", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();
                lbTitle.Text = drConn["titel"].ToString() + drConn["name"].ToString() + ", " + drConn["vorname"].ToString();
                lbPNameV.Text = drConn["name"].ToString();
                lbPVornameV.Text = drConn["vorname"].ToString();
                lbGebdatV.Text = drConn["gebdat"].ToString();
                lbProcdate.Text = "Ersterfassung am&nbsp;" + drConn["gendate"].ToString();
                lbThestart.Text = "Therapiestart:&nbsp;" + drConn["thestart"].ToString();
                lbErrei.Text = "Erreichbarkeit:&nbsp;" + drConn["erreichbarkeit"].ToString();
                lbForm.Text = "Antragsdatum:&nbsp;" + drConn["consdate"].ToString();

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

                switch (drConn["consent"].ToString())
                {
                    case "Ja":
                        {
                            lbConsent.Text = "Schriftlicher Consent am " + drConn["consget"].ToString() + " erteilt";
                            break;
                        }
                    case "Nein":
                        {
                            lbConsent.Text = "Schriftlicher Consent noch nicht erteilt";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                lbAdresse.Text = drConn["adresse"].ToString();
                lbPlzOrt.Text = drConn["plz"].ToString() + "&nbsp;" + drConn["ort"].ToString();
                lbFestnetz.Text = drConn["tel"].ToString();
                lbEmail.Text = drConn["email"].ToString();

                lbDiagnose.Text = "Diagnose: " + drConn["diagnose"].ToString();
                lbZentrum.Text = "Zuständiges MS_Zentrum: " + drConn["zentrum"].ToString();
                lbIntervall.Text = "Anrufintervall: " + drConn["intervall"].ToString();
                lbAnonym.Text = "Anonym gegenüber Dritten: " + drConn["anonym"].ToString();

                string stVorthe = "Vortherapie: ";

                if (drConn["vorthe"].ToString() == "Nein") stVorthe = "Keine Vortherapie";
                else
                {
                    stVorthe = "Vortherapie: ";
                    if (drConn["vorthetext"].ToString() != "") stVorthe = stVorthe + drConn["vorthetext"].ToString();
                    else stVorthe = "Vortherapie: Ja";

                }
                lbVorthe.Text = stVorthe;
                lbMedikament.Text = "Medikament: " + drConn["medikament"].ToString();
                drConn.Close();
                cnConn.Close();
            }
        }
    }
}