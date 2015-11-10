using System;
using System.Collections.Generic;
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
using System.Net.Mail;

namespace PSP.general
{
    public partial class newBtk : System.Web.UI.Page
    {

        static string stMedRef;
        static string stNrsKontaktOld = "";
        static string stNwOld = "";
        static string stNwTextOld = "";
        static string stMedanOld = "";
        static string stMedanCommentOld = "";
        static string[] oldEntry;
        static string[] newEntry;
        static string[] fieldName;
        static string stInt;
        static string stNekoRef;
        static string stGroesseRef;
        static string stMassRef;
        static string stLekoRef, stIntervallRef;
        static string stAvostart;
        static bool bMedan;
        static string stStatusRef;
        static string stAbgeschlossenRef;
        static string stOwnerRef, stOwnerID;
        static Boolean bWechsel;
        static string stKV = "";
        static string stZustaendigkeitCur = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            string stprocday = "";
            string stprocmonth = "";
            string stprocyear = "";
            int i;
            string stCurYear = DateTime.Now.ToString("yyyy");
            string stNextYear = ((Convert.ToInt32(stCurYear)) + 1).ToString();
            string stLastYear = ((Convert.ToInt32(stCurYear)) - 1).ToString();

            bMedan = true;

            scStatus.RegisterAsyncPostBackControl(ddl_status);
            scStatus.RegisterAsyncPostBackControl(rbNw);
            scStatus.RegisterAsyncPostBackControl(rbStatusThewe);
            scStatus.RegisterAsyncPostBackControl(ddlTheweMed);
            scStatus.RegisterAsyncPostBackControl(ddl_art);
            scStatus.RegisterAsyncPostBackControl(rbMedan);


            Session["PatID"] = Convert.ToInt32(Request["PatID"]);

            mvNCom.SetActiveView(vwNComNo);
            lbsaved.Text = "";

            if (Request["src"] == "save")
            {
                lbsaved.Text = "Daten wurden gespeichert.";
                btCancel.Text = "Beenden";
            }



            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());

            lbZustaendigkeit.Text = (stZustaendigkeitCur == "Nurse") ? "Zuständigkeit ändern auf Service Center" : "Zuständigkeit ändern auf Nurse Service";

            if (!IsPostBack)
            {
                Session["prevpage"] = Request.UrlReferrer.ToString();

                switch (Session["role"].ToString())
                {
                    case "sec":
                        mvMenu.SetActiveView(vwSec);
                        break;
                    case "nrs":
                        mvMenu.SetActiveView(vwNrs);
                        break;
                    case "ass":
                        mvMenu.SetActiveView(vwAss);
                        break;
                    default:
                        break;
                }



                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_patdat", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();

                stGroesseRef = drConn["groesse"].ToString();
                stMedRef = drConn["medikament"].ToString();
                Session["med"] = drConn["medikament"].ToString();
                stNekoRef = drConn["neko"].ToString();
                stLekoRef = drConn["leko"].ToString();
                stStatusRef = drConn["status"].ToString();
                stIntervallRef = drConn["intervall"].ToString();
                stZustaendigkeitCur = drConn["zustaendigkeit"].ToString();
                drConn.Close();
                lbZustaendigkeit.Text = (stZustaendigkeitCur == "Nurse") ? "Zuständigkeit ändern auf Service Center" : "Zuständigkeit ändern auf Nurse Service";
                if (Session["role"].ToString() == "sec") mvNrsKontakt.SetActiveView(vwNrsKontakt); else mvNrsKontakt.SetActiveView(vwNrsKontaktNo);


                if (Request["reason"] == "edit")
                {
                    cmdConn = new SqlCommand("get_btkbybtkid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", Convert.ToInt32(Request["btkid"])));
                    Session["btkid"] = Convert.ToInt32(Request["btkid"]);
                    drConn = cmdConn.ExecuteReader();
                    drConn.Read();
                    stMedRef = drConn["medikament"].ToString();
                    drConn.Close();
                }

                switch (stMedRef)
                {
                    case "Plegridy":
                        {
                            medView.Style.Add("background-color", "#99ccff");
                            medView1.Style.Add("background-color", "#99ccff");
                            lbMedIndicator.Text = "Plegridy";
                            lbMedIndicator1.Text = "Plegridy";
                            mvMedFLS.SetActiveView(vwMedFLS);
                            mvMedHaut.SetActiveView(vwMedHaut);
                            mvMedGastroFlush.SetActiveView(vwMedGastroFlushNo);
                            mvInjek.SetActiveView(vwInjek);
                            mvNL.SetActiveView(vwNoNL);
                            mvInjekOrt.SetActiveView(vwInjekOrt);
                            mvAvoject.SetActiveView(vwAvojectNo);
                            mvTitDos.SetActiveView(vwTit);
                            mvAngSchule.SetActiveView(vwAngSchule);
                            tbAndere.Enabled = false;
                            tbHautAndere.Enabled = false;
                            rbBioFeSp.Items[2].Enabled = false;
                            break;
                        }
                    case "Tecfidera":
                        {
                            medView.Style.Add("background-color", "#ff66ff");
                            medView1.Style.Add("background-color", "#ff66ff");
                            lbMedIndicator.Text = "Tecfidera";
                            lbMedIndicator1.Text = "Tecfidera";
                            mvMedFLS.SetActiveView(vwMedFLSNo);
                            mvMedHaut.SetActiveView(vwMedHautNo);
                            mvMedGastroFlush.SetActiveView(vwMedGastroFlush);
                            mvInjek.SetActiveView(vwInjekNo);
                            mvInjekOrt.SetActiveView(vwInjekOrtNo);
                            mvAvoject.SetActiveView(vwAvojectNo);
                            mvTitDos.SetActiveView(vwDos);
                            mvAngSchule.SetActiveView(vwAngSchuleNo);
                            tbGastro.Enabled = false;
                            tbFlush.Enabled = false;
                            break;
                        }
                    case "Avonex":
                        {
                            medView.Style.Add("background-color", "#ff9900");
                            medView1.Style.Add("background-color", "#ff9900");
                            lbMedIndicator.Text = "Avonex";
                            lbMedIndicator1.Text = "Avonex";
                            mvMedFLS.SetActiveView(vwMedFLS);
                            mvMedGastroFlush.SetActiveView(vwMedGastroFlushNo);
                            mvMedHaut.SetActiveView(vwMedHautNo);
                            mvInjek.SetActiveView(vwInjek);
                            mvInjekOrt.SetActiveView(vwInjekOrt);
                            mvAvoject.SetActiveView(vwAvoject);
                            mvAngSchule.SetActiveView(vwAngSchule);
                            foreach (ListItem item in rbInjekOrt.Items)
                            {
                                if ((item.Value == "Oberarm") || (item.Value == "Bauch")) item.Enabled = false;
                                if (item.Value == "Oberschenkel") item.Selected = true;
                            }
                            mvTitDos.SetActiveView(vwTit);
                            tbAndere.Enabled = false;
                            break;
                        }
                    default: break;
                }
                mvMedInd.SetActiveView(vwMedInd);
                mvMedInd1.SetActiveView(vwMedInd1);
               


                

                ArrayList art = new ArrayList();
                art.Add("");
                art.Add("Einschulungsbesuch 1");
                art.Add("Einschulungsbesuch 2");
                art.Add("Weiterer Einschulungsbesuch");
                art.Add("Eingehender Anruf");
                art.Add("Ausgehender Anruf");
                art.Add("Sonstiger Kontakt");
                art.Add("Kontaktversuch");
                ddl_art.DataSource = art;
                ddl_art.DataBind();

                ArrayList status = new ArrayList();
                status.Add("Fortsetzung");
                status.Add("Beginn");
                status.Add("Wiederaufnahme");
                status.Add("Abbruch");
                ddl_status.DataSource = status;
                ddl_status.DataBind();


                ArrayList dday = new ArrayList();
                dday.Add("");
                dday.Add("01");
                dday.Add("02");
                dday.Add("03");
                dday.Add("04");
                dday.Add("05");
                dday.Add("06");
                dday.Add("07");
                dday.Add("08");
                dday.Add("09");
                dday.Add("10");
                dday.Add("11");
                dday.Add("12");
                dday.Add("13");
                dday.Add("14");
                dday.Add("15");
                dday.Add("16");
                dday.Add("17");
                dday.Add("18");
                dday.Add("19");
                dday.Add("20");
                dday.Add("21");
                dday.Add("22");
                dday.Add("23");
                dday.Add("24");
                dday.Add("25");
                dday.Add("26");
                dday.Add("27");
                dday.Add("28");
                dday.Add("29");
                dday.Add("30");
                dday.Add("31");
                ddl_procday.DataSource = dday;
                ddl_procday.DataBind();
                ddl_nekoday.DataSource = dday;
                ddl_nekoday.DataBind();

                ArrayList ddaystat = new ArrayList();
                ddaystat.Add("??");
                ddaystat.Add("01");
                ddaystat.Add("02");
                ddaystat.Add("03");
                ddaystat.Add("04");
                ddaystat.Add("05");
                ddaystat.Add("06");
                ddaystat.Add("07");
                ddaystat.Add("08");
                ddaystat.Add("09");
                ddaystat.Add("10");
                ddaystat.Add("11");
                ddaystat.Add("12");
                ddaystat.Add("13");
                ddaystat.Add("14");
                ddaystat.Add("15");
                ddaystat.Add("16");
                ddaystat.Add("17");
                ddaystat.Add("18");
                ddaystat.Add("19");
                ddaystat.Add("20");
                ddaystat.Add("21");
                ddaystat.Add("22");
                ddaystat.Add("23");
                ddaystat.Add("24");
                ddaystat.Add("25");
                ddaystat.Add("26");
                ddaystat.Add("27");
                ddaystat.Add("28");
                ddaystat.Add("29");
                ddaystat.Add("30");
                ddaystat.Add("31");
                ddl_statday.DataSource = ddaystat;
                ddl_statday.DataBind();
                ddl_AAday.DataSource = ddaystat;
                ddl_AAday.DataBind();

                ArrayList dmonth = new ArrayList();
                dmonth.Add("");
                dmonth.Add("01");
                dmonth.Add("02");
                dmonth.Add("03");
                dmonth.Add("04");
                dmonth.Add("05");
                dmonth.Add("06");
                dmonth.Add("07");
                dmonth.Add("08");
                dmonth.Add("09");
                dmonth.Add("10");
                dmonth.Add("11");
                dmonth.Add("12");
                ddl_procmonth.DataSource = dmonth;
                ddl_procmonth.DataBind();
                ddl_nekomonth.DataSource = dmonth;
                ddl_nekomonth.DataBind();


                ArrayList dmonthstat = new ArrayList();
                dmonthstat.Add("??");
                dmonthstat.Add("01");
                dmonthstat.Add("02");
                dmonthstat.Add("03");
                dmonthstat.Add("04");
                dmonthstat.Add("05");
                dmonthstat.Add("06");
                dmonthstat.Add("07");
                dmonthstat.Add("08");
                dmonthstat.Add("09");
                dmonthstat.Add("10");
                dmonthstat.Add("11");
                dmonthstat.Add("12");
                ddl_statmonth.DataSource = dmonthstat;
                ddl_statmonth.DataBind();
                ddl_AAmonth.DataSource = dmonthstat;
                ddl_AAmonth.DataBind();

                ArrayList dyear = new ArrayList();
                dyear.Add("");
                dyear.Add(stNextYear);
                dyear.Add(stCurYear);
                for (i = 1; i < 10; i++)
                {
                    stLastYear = ((Convert.ToInt32(stCurYear)) - i).ToString();
                    dyear.Add(stLastYear);
                }
                ddl_procyear.DataSource = dyear;
                ddl_procyear.DataBind();
                ddl_nekoyear.DataSource = dyear;
                ddl_nekoyear.DataBind();

                dyear.Clear();
                dyear.Add("????");
                dyear.Add(stCurYear);
                for (i = 1; i < 10; i++)
                {
                    stLastYear = ((Convert.ToInt32(stCurYear)) - i).ToString();
                    dyear.Add(stLastYear);
                }
                ddl_AAyear.DataSource = dyear;
                ddl_AAyear.DataBind();
                ddl_statyear.DataSource = dyear;
                ddl_statyear.DataBind();

                ArrayList thewemed = new ArrayList();
                thewemed.Add("keine Angabe");
                thewemed.Add("Rebif 22µg");
                thewemed.Add("Rebif 44µg");
                thewemed.Add("Betaferon");
                thewemed.Add("Copaxone");
                thewemed.Add("Tysabri");
                thewemed.Add("Anderes Präparat");
                ddlTheweMed.DataSource = thewemed;
                ddlTheweMed.DataBind();

                ArrayList titra = new ArrayList();
                titra.Add("??");
                titra.Add("0");
                titra.Add("1");
                titra.Add("2");
                titra.Add("3");
                titra.Add("4");
                titra.Add("5");
                titra.Add("6");
                titra.Add("7");
                titra.Add("8");
                titra.Add("9");
                titra.Add("10");
                titra.Add("11");
                titra.Add("12");
                ddl_titra.DataSource = titra;
                ddl_titra.DataBind();

                cmdConn = new SqlCommand("get_btkbypatid", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                drConn = cmdConn.ExecuteReader();
                drConn.Read();
                if (!drConn.HasRows)
                {
                    lbKontaktV.Text = "Bisher kein erfolgreicher Kontakt";
                    btDefault.Enabled = false;
                    btDefault.CssClass = "button.disabled";
                }
                else
                {
                    lbKontaktV.Text = "Letzer erfolgreicher Kontakt:&nbsp;" + drConn["btkdate"].ToString() + ",&nbsp;" + drConn["art"].ToString();
                }
                drConn.Close();

                if (Request["reason"] == "edit")
                {
                    cmdConn = new SqlCommand("get_btkbybtkid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", Convert.ToInt32(Request["btkid"])));
                    Session["btkid"] = Convert.ToInt32(Request["btkid"]);
                    drConn = cmdConn.ExecuteReader();
                    drConn.Read();
                    lbKontaktV.Visible = false;

                    lbTitle.Text = "Betreuungskontakt bearbeiten";
                    btDefault.CssClass = "button.disabled";
                    btDefault.Enabled = false;
                    stAbgeschlossenRef = drConn["abgeschlossen"].ToString();
                    if ((stAbgeschlossenRef == "Ja") || (Session["role"].ToString() == "ass"))
                    {
                        ddl_procday.Enabled = false;
                        ddl_procmonth.Enabled = false;
                        ddl_procyear.Enabled = false;
                        ddl_art.Enabled = false;
                        ddl_status.Enabled = false;
                        ddl_statday.Enabled = false;
                        ddl_statmonth.Enabled = false;
                        ddl_statyear.Enabled = false;
                        ddl_AAday.Enabled = false;
                        ddl_AAmonth.Enabled = false;
                        ddl_AAyear.Enabled = false;
                        ddl_titra.Enabled = false;
                        tbStatusGrund.Enabled = false;
                        rbStatusThewe.Enabled = false;
                        /*rbStatusThewe.Attributes["onclick"] = "return false;";*/
                        ddlTheweMed.Enabled = false;
                        tbTheweMedAndere.Enabled = false;
                        /*tbTheweMedAndere.Attributes["onclick"] = "return false;";*/
                        cbAndere.Enabled = false;
                        cbAngschule.Enabled = false;
                        cbCoolpack.Enabled = false;
                        cbHautAndere.Enabled = false;
                        cbIbuprofen.Enabled = false;
                        cbMexalen.Enabled = false;
                        cbNaproxen.Enabled = false;
                        cbMedan1.Enabled = false;
                        cbMedanComplete.Enabled = false;
                        cbSpontan.Enabled = false;
                        rbNw.Enabled = false;
                        rbNw.Attributes["onclick"] = "return false;";
                        cbSpontan.Enabled = false;
                        ddl_nekoday.Enabled = false;
                        ddl_nekomonth.Enabled = false;
                        ddl_nekoyear.Enabled = false;
                        tbNw.ReadOnly = true;
                        tbMedan.Enabled = false;
                        tbBhv.Enabled = false;
                        /*rbMedan.Attributes["onclick"] = "return false;";*/
                        btCancel.Text = "Beenden";
                        btSave.Text = "Speichern";
                        btSave.Enabled = false;
                        btSave.CssClass = "button.disabled";
                        btFinish.CssClass = "button.disabled";
                        btFinish.Enabled = false;
                        btDefault.CssClass = "button.disabled";
                        btDefault.Enabled = false;
                        lbTitle.Text = "Betreuungskontakt einsehen";
                        rbAvoject.Enabled = false;
                        rbBioFeSp.Enabled = false;
                        rbDos.Enabled = false;
                        rbFLS.Enabled = false;
                        rbFlush.Enabled = false;
                        rbGastro.Enabled = false;
                        rbHaut.Enabled = false;
                        rbInjekOrt.Enabled = false;
                        rbMedan.Enabled = false;
                        rbNaLa.Enabled = false;
                        rbNw.Enabled = false;
                        rbStatusThewe.Enabled = false;
                        rbTitra.Enabled = false;
                        rbTitraArt.Enabled = false;
                        rbTitraPlegridy.Enabled = false;
                        rbZustaendigkeit.Enabled = false;
                        rbNurseKontakt.Enabled = false;
                        tbAngschule.Enabled = false;
                        tbAA.Enabled = false;
                        tbAndere.Enabled = false;
                        tbCurMass.Enabled = false;
                        tbFlush.Enabled = false;
                        tbGastro.Enabled = false;
                        tbHautAndere.Enabled = false;
                    }


                    string stArt = drConn["art"].ToString();
                    for (i = 0; i < ddl_art.Items.Count; i++)
                    {
                        if (ddl_art.Items[i].Value.ToString() == stArt)
                        {
                            ddl_art.Items[i].Selected = true;
                        }
                    }

                    string stStatus = drConn["status"].ToString();
                    for (i = 0; i < ddl_status.Items.Count; i++)
                    {
                        if (ddl_status.Items[i].Value.ToString() == stStatus)
                        {
                            ddl_status.Items[i].Selected = true;
                        }
                    }

                    string[] arStatusIsDate = { "Beginn", "Abbruch", "Wiederaufnahme" };

                    string ststatday = "";
                    string ststatmonth = "";
                    string ststatyear = "";

                    if (((IList<string>)arStatusIsDate).Contains(stStatus))
                    {
                        mvStatusDatum.SetActiveView(vwStatusDatum);
                        string stStatusDatum = drConn["status_date"].ToString();
                        if (stStatusDatum.Length > 9)
                        {
                            ststatday = stStatusDatum.Substring(8, 2);
                            ststatmonth = stStatusDatum.Substring(5, 2);
                            ststatyear = stStatusDatum.Substring(0, 4);
                            for (i = 0; i < ddl_statday.Items.Count; i++)
                            {
                                if (ddl_statday.Items[i].Value.ToString() == ststatday)
                                {
                                    ddl_statday.Items[i].Selected = true;
                                }
                            }

                            for (i = 0; i < ddl_statmonth.Items.Count; i++)
                            {
                                if (ddl_statmonth.Items[i].Value.ToString() == ststatmonth)
                                {
                                    ddl_statmonth.Items[i].Selected = true;
                                }
                            }

                            for (i = 0; i < ddl_statyear.Items.Count; i++)
                            {
                                if (ddl_statyear.Items[i].Value.ToString() == ststatyear)
                                {
                                    ddl_statyear.Items[i].Selected = true;
                                }
                            }
                        }

                    }

                    string[] arStatus = { "Abbruch", "Wiederaufnahme" };

                    if (((IList<string>)arStatus).Contains(stStatus))
                    {
                        mvStatusGrund.SetActiveView(vwStatusGrund);
                        string stStatusGrund = drConn["status_grund"].ToString();
                        tbStatusGrund.Text = stStatusGrund;
                    }

                    if (stStatus == "Abbruch") mvStatusThewe.SetActiveView(vwStatusThewe);

                    string stThewe = drConn["thewe"].ToString();
                    if (stThewe == "Ja")
                    {
                        rbStatusThewe.Items[0].Selected = true;
                        mvTheweMed.SetActiveView(vwTheweMed);
                        string stTheweMed = drConn["thewemed"].ToString();
                        Boolean bFound = false;
                        for (i = 0; i < ddlTheweMed.Items.Count; i++)
                        {
                            if (ddlTheweMed.Items[i].Value.ToString() == stTheweMed)
                            {
                                ddlTheweMed.Items[i].Selected = true;
                                bFound = true;
                            }
                        }
                        if (!bFound)
                        {
                            ddlTheweMed.Items[ddlTheweMed.Items.Count - 1].Selected = true;
                            tbTheweMedAndere.Enabled = true;
                            tbTheweMedAndere.Text = drConn["thewemed"].ToString();
                        }
                    }

                    else
                    {
                        rbStatusThewe.Items[1].Selected = true;
                        mvTheweMed.SetActiveView(vwTheweMedNo);
                    }

                    if (drConn["prophy_fls"].ToString() == "Ja")
                    {
                        mvFLS.SetActiveView(vwFLS);
                        rbFLS.SelectedIndex = 0;
                        if (drConn["fls_mexalen"].ToString() == "Ja") cbMexalen.Checked = true;
                        if (drConn["fls_naproxen"].ToString() == "Ja") cbNaproxen.Checked = true;
                        if (drConn["fls_ibuprofen"].ToString() == "Ja") cbIbuprofen.Checked = true;
                        if (drConn["fls_andere"].ToString() != "n/a")
                        {
                            cbAndere.Checked = true;
                            if (drConn["fls_andere"].ToString() != "Ja")
                            {
                                tbAndere.Text = drConn["fls_andere"].ToString();
                                tbAndere.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        mvFLS.SetActiveView(vwFLSNo);
                        rbFLS.SelectedIndex = 1;
                    }

                    if (drConn["prophy_haut"].ToString() == "Ja")
                    {
                        mvHaut.SetActiveView(vwHaut);
                        rbHaut.SelectedIndex = 0;
                        if (drConn["haut_coolpack"].ToString() == "Ja") cbCoolpack.Checked = true;
                        if (drConn["haut_andere"].ToString() != "n/a")
                        {
                            cbHautAndere.Checked = true;
                            if (drConn["haut_andere"].ToString() != "Ja")
                            {
                                tbHautAndere.Text = drConn["haut_andere"].ToString();
                                tbHautAndere.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        mvHaut.SetActiveView(vwHautNo);
                        rbHaut.SelectedIndex = 1;
                    }

                    if (drConn["gastro"].ToString() == "Ja")
                    {
                        rbGastro.SelectedIndex = 0;
                        tbGastro.Text = drConn["gastro_comment"].ToString();
                        tbGastro.Enabled = true;
                    }
                    else
                    {
                        rbGastro.SelectedIndex = 1;
                        tbGastro.Text = "";
                        tbGastro.Enabled = false;
                    }

                    if (drConn["flush"].ToString() == "Ja")
                    {
                        rbFlush.SelectedIndex = 0;
                        tbFlush.Text = drConn["flush_comment"].ToString();
                        tbFlush.Enabled = true;
                    }
                    else
                    {
                        rbFlush.SelectedIndex = 1;
                        tbFlush.Text = "";
                        tbFlush.Enabled = false;
                    }

                    bool bInjek = false;
                    string stBio = drConn["bioset"].ToString();
                    if (stBio == "Ja") { rbBioFeSp.Items[2].Selected = true; bInjek = true; }

                    string stFesp = drConn["fesp"].ToString();
                    if (stFesp == "Ja") { rbBioFeSp.Items[1].Selected = true; bInjek = true; }

                    string stPen = drConn["pen"].ToString();
                    if (stPen == "Ja") rbBioFeSp.Items[0].Selected = true;

                    bool bNala = false;
                    if ((bInjek) && (stMedRef  == "Avonex"))
                    {
                        mvNL.SetActiveView(vwNL);
                        string stNala = drConn["nala"].ToString();
                        if (stNala == "Standard") { rbNaLa.Items[0].Selected = true; bNala = false; }
                        if (stNala == "Lang") { rbNaLa.Items[1].Selected = true; bNala = true; }
                        if (stNala == "Kurz") { rbNaLa.Items[2].Selected = true; bNala = true; }
                        if (stNala == "Keine Angabe") { rbNaLa.Items[3].Selected = true; bNala = false; }
                    }

                    if (bNala)
                    {
                        mvAA.SetActiveView(vwAA);
                        string stAA = drConn["aaname"].ToString();
                        tbAA.Text = stAA;
                        tbAAComment.Text = drConn["aacomment"].ToString();
                        string stAADatum = drConn["aadate"].ToString();

                        for (i = 0; i < ddl_statday.Items.Count; i++)
                        {
                            if (ddl_AAday.Items[i].Value.ToString() == stAADatum.Substring(8, 2))
                            {
                                ddl_AAday.Items[i].Selected = true;
                            }
                        }

                        for (i = 0; i < ddl_statmonth.Items.Count; i++)
                        {
                            if (ddl_AAmonth.Items[i].Value.ToString() == stAADatum.Substring(5, 2))
                            {
                                ddl_AAmonth.Items[i].Selected = true;
                            }
                        }

                        for (i = 0; i < ddl_statyear.Items.Count; i++)
                        {
                            if (ddl_AAyear.Items[i].Value.ToString() == stAADatum.Substring(0, 4))
                            {
                                ddl_AAyear.Items[i].Selected = true;
                            }
                        }
                    }

                    string stAvoject = drConn["avoject"].ToString();
                    if (stAvoject == "Ja") { rbAvoject.Items[0].Selected = true; mvAvoject.SetActiveView(vwAvoject); }
                    else { rbAvoject.Items[1].Selected = true; mvAvoject.SetActiveView(vwAvojectNo); }

                    string stTitration = drConn["titration"].ToString();
                    string stTecfiDosis;
                    string stTitraDauer, stTitraArt;
                    stTitraArt = drConn["titration_art"].ToString();
                    stTecfiDosis = drConn["tecfidosis"].ToString();
                    if (stTitration == "Ja")
                    {
                        rbTitra.Items[0].Selected = true;
                        if (stMedRef == "Avonex")
                        {
                            mvTitra.SetActiveView(vwTitraAvonex);
                            stTitraDauer = drConn["titration_dauer"].ToString();
                            for (i = 0; i < ddl_titra.Items.Count; i++) if (ddl_titra.Items[i].Value.ToString() == stTitraDauer) ddl_titra.Items[i].Selected = true;
                            if (stTitraArt == "1/4") rbTitraArt.Items[0].Selected = true;
                            if (stTitraArt == "1/2") rbTitraArt.Items[1].Selected = true;
                            if (stTitraArt == "3/4") rbTitraArt.Items[2].Selected = true;
                        }
                        else
                        {
                            mvTitra.SetActiveView(vwTitraPlegridy);
                            if (stTitraArt == "63") rbTitraPlegridy.Items[0].Selected = true;
                            if (stTitraArt == "94") rbTitraPlegridy.Items[1].Selected = true;
                        }
                    }
                    else
                    {
                        rbTitra.Items[1].Selected = true;
                        if (stTecfiDosis == "120") rbDos.Items[0].Selected = true;
                        if (stTecfiDosis == "240") rbDos.Items[1].Selected = true;
                    }


                    string stAngschule = drConn["angschule"].ToString();
                    if (stAngschule == "Ja")
                    {
                        mvAngSchule.SetActiveView(vwAngSchule);
                        cbAngschule.Checked = true;
                        tbAngschule.Text = drConn["angschulecomment"].ToString();
                    }
                    else mvAngSchule.SetActiveView(vwAngSchuleNo);

                    string stInjekOrt = drConn["injekort"].ToString();
                    if (stInjekOrt != "n/a")
                    {
                        mvInjekOrt.SetActiveView(vwInjekOrt);
                            switch (stInjekOrt)
                            {
                                case "Oberschenkel":
                                    rbInjekOrt.Items[0].Selected = true;
                                    break;
                                case "Bauch":
                                    rbInjekOrt.Items[1].Selected = true;
                                    break;
                                case "Oberarm":
                                    rbInjekOrt.Items[2].Selected = true;
                                    break;
                                default:
                                    break;
                            }
                    }
                    else mvInjekOrt.SetActiveView(vwInjekOrtNo);

                    tbCurMass.Text = drConn["curmass"].ToString();
                    lbBMI.Text = drConn["bmi"].ToString();

                    string stMedan = drConn["medan"].ToString();
                    string stMedanT;
                    if (stMedan == "Ja")
                    {
                        rbMedan.Items[0].Selected = true;
                        mvMedan.SetActiveView(vwMedan);
                        tbMedan.Text = drConn["medancomment"].ToString();
                        stMedanT = drConn["medancomplete"].ToString();
                        if (stMedanT == "Ja") cbMedanComplete.Checked = true;
                    }
                    else
                    {
                        rbMedan.Items[1].Selected = true;
                        mvMedan.SetActiveView(vwMedanNo);
                    }

                    string stNw = drConn["nw"].ToString();
                    if (stNw == "Ja")
                    {
                        rbNw.Items[0].Selected = true;
                        mvNw.SetActiveView(vwNw);
                        tbNw.Text = drConn["nwtext"].ToString();
                        cbSpontan.Enabled = true;
                        if (drConn["nwspontan"].ToString() == "Ja") cbSpontan.Checked = true;
                    }
                    else
                    {
                        rbNw.Items[1].Selected = true;
                        mvNw.SetActiveView(vwNoNw);
                        cbSpontan.Enabled = false;
                    }

                    if (drConn["nrskontakt"].ToString() == "Ja") rbNurseKontakt.SelectedIndex = 0; else rbNurseKontakt.SelectedIndex = 1;

                    tbBhv.Text = drConn["bhv"].ToString();
                    stNwTextOld = drConn["nwtext"].ToString();
                    stNwOld = drConn["nw"].ToString();
                    stMedanCommentOld = drConn["medancomment"].ToString();
                    stNrsKontaktOld = drConn["nrskontakt"].ToString(); 

                    string strDatum = drConn["btkdate"].ToString();
                    stprocday = strDatum.Substring(8, 2);
                    stprocmonth = strDatum.Substring(5, 2);
                    stprocyear = strDatum.Substring(0, 4);

                    for (i = 0; i < ddl_procday.Items.Count; i++)
                    {
                        if (ddl_procday.Items[i].Value.ToString() == stprocday)
                        {
                            ddl_procday.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_procmonth.Items.Count; i++)
                    {
                        if (ddl_procmonth.Items[i].Value.ToString() == stprocmonth)
                        {
                            ddl_procmonth.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_procyear.Items.Count; i++)
                    {
                        if (ddl_procyear.Items[i].Value.ToString() == stprocyear)
                        {
                            ddl_procyear.Items[i].Selected = true;
                        }
                    }


                    string stNeko = drConn["neko"].ToString();
                    string stnekoday = "";
                    string stnekomonth = "";
                    string stnekoyear = "";
                    if (stNeko.Length > 0)
                    {
                        stnekoday = stNeko.Substring(8, 2);
                        stnekomonth = stNeko.Substring(5, 2);
                        stnekoyear = stNeko.Substring(0, 4);
                    }


                    for (i = 0; i < ddl_nekoday.Items.Count; i++)
                    {
                        if (ddl_nekoday.Items[i].Value.ToString() == stnekoday)
                        {
                            ddl_nekoday.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_nekomonth.Items.Count; i++)
                    {
                        if (ddl_nekomonth.Items[i].Value.ToString() == stnekomonth)
                        {
                            ddl_nekomonth.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_nekoyear.Items.Count; i++)
                    {
                        if (ddl_nekoyear.Items[i].Value.ToString() == stnekoyear)
                        {
                            ddl_nekoyear.Items[i].Selected = true;
                        }
                    }

                    fieldName = manageAuditTrail.get_fields("get_btkbybtkid", Convert.ToInt32(Request["btkid"]), "@btkid");
                    oldEntry = manageAuditTrail.get_entry("get_btkbybtkid", Convert.ToInt32(Request["btkid"]), "@btkid");
                    drConn.Close();
                }



                if (Request["reason"] == "new")
                {

                    lbTitle.Text = "Neuer Betreuungskontakt";
                    cmdConn = new SqlCommand("get_patdat", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                    drConn = cmdConn.ExecuteReader();
                    drConn.Read();
                    string stMedikament = drConn["Medikament"].ToString();
                    stInt = drConn["intervall"].ToString();
                    drConn.Close();
                    stAbgeschlossenRef = "Nein";
                    cbSpontan.Enabled = false;

                    for (i = 0; i < ddl_procday.Items.Count; i++)
                    {
                        if (ddl_procday.Items[i].Value.ToString() == DateTime.Now.ToString("dd").ToString())
                        {
                            ddl_procday.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_procmonth.Items.Count; i++)
                    {
                        if (ddl_procmonth.Items[i].Value.ToString() == DateTime.Now.ToString("MM").ToString())
                        {
                            ddl_procmonth.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_procyear.Items.Count; i++)
                    {
                        if (ddl_procyear.Items[i].Value.ToString() == DateTime.Now.ToString("yyyy").ToString())
                        {
                            ddl_procyear.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_statday.Items.Count; i++)
                    {
                        if (ddl_statday.Items[i].Value.ToString() == DateTime.Now.ToString("dd").ToString())
                        {
                            ddl_statday.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_statmonth.Items.Count; i++)
                    {
                        if (ddl_statmonth.Items[i].Value.ToString() == DateTime.Now.ToString("MM").ToString())
                        {
                            ddl_statmonth.Items[i].Selected = true;
                        }
                    }


                    for (i = 0; i < ddl_statyear.Items.Count; i++)
                    {
                        if (ddl_statyear.Items[i].Value.ToString() == DateTime.Now.ToString("yyyy").ToString())
                        {
                            ddl_statyear.Items[i].Selected = true;
                        }
                    }

                    DateTime dtCurrent = DateTime.Now;
                    string stNekoDay = "";
                    string stNekoMonth = "";
                    string stNekoYear = "";
                    switch (stInt)
                    {
                        case "Wöchentlich":
                            dtCurrent = dtCurrent.AddDays(+8);
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                        case "Alle 2 Wochen":
                            dtCurrent = dtCurrent.AddDays(+15);
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                        case "Monatlich":
                            dtCurrent = dtCurrent.AddDays(+32);
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                        case "Alle 2 Monate":
                            dtCurrent = dtCurrent.AddDays(+62);
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                        case "Alle 3 Monate":
                            dtCurrent = dtCurrent.AddDays(+92);
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                        case "Alle 4 Monate":
                            dtCurrent = dtCurrent.AddDays(+112);
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                        default:
                            stNekoDay = dtCurrent.ToString("dd");
                            stNekoMonth = dtCurrent.ToString("MM");
                            stNekoYear = dtCurrent.ToString("yyyy");
                            break;
                    }

                    for (i = 0; i < ddl_nekoday.Items.Count; i++)
                    {
                        if (ddl_nekoday.Items[i].Value.ToString() == stNekoDay)
                        {
                            ddl_nekoday.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_nekomonth.Items.Count; i++)
                    {
                        if (ddl_nekomonth.Items[i].Value.ToString() == stNekoMonth)
                        {
                            ddl_nekomonth.Items[i].Selected = true;
                        }
                    }

                    for (i = 0; i < ddl_nekoyear.Items.Count; i++)
                    {
                        if (ddl_nekoyear.Items[i].Value.ToString() == stNekoYear)
                        {
                            ddl_nekoyear.Items[i].Selected = true;
                        }
                    }

                }
                cnConn.Close();
            }
        }

        protected void cvValddlDate_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string stDate, stYear, stMonth, stDay;

            stYear = ddl_procyear.SelectedItem.Text.ToString();
            stMonth = ddl_procmonth.SelectedItem.Text.ToString();
            stDay = ddl_procday.SelectedItem.Text.ToString();

            stDate = stYear + "-" + stMonth + "-" + stDay;

            string ErrorMessage = Utils.CheckFullDate(stDate, "Back");
            if (ErrorMessage != "")
            {
                cvValddlDate.ErrorMessage = "Datum Betreuungskontakt: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;
        }


        protected void cvValddlArt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((ddl_art.SelectedItem.Text.ToString() == ""))
            {
                args.IsValid = false;
                cvValddlArt.ErrorMessage = "Bitte wählen Sie die Art des Kontaktes";
            }

        }


        protected void bt_save_click(object sender, EventArgs e)
        {

            string stGenDate = "n/a", stBtkDate = "n/a", stArt = "n/a", stStatus = "n/a", stStatusGrund = "n/a", stStatusDate = "n/a", stMedikament = "n/a";
            string stThewe = "n/a", stTheweMed = "n/a";
            string stProphyFLS = "n/a", stFLSMexalen = "n/a", stFLSNaproxen = "n/a", stFLSIbuprofen = "n/a", stFLSAndere = "n/a";
            string stProphyHaut = "n/a", stHautCoolpack = "n/a", stHautAndere = "n/a";
            string stGastro = "n/a", stGastroComment = "n/a", stFlush = "n/a", stFlushComment = "n/a";
            string stPen = "n/a", stBioset = "n/a", stFesp = "n/a", stNala = "n/a", stAAComment = "n/a", stAADate = "n/a", stAAName = "n/a";
            string stInjekOrt = "n/a", stAvoject = "n/a", stTitration = "n/a", stTitrationDauer = "n/a", stTitrationArt = "n/a", stTecfiDosis = "n/a";
            string stAngschule = "n/a", stAngschuleComment = "n/a", stCurMass = "n/a", stBMI = "n/a";
            string stMedAn = "n/a", stMedAnComment = "n/a", stMedanComplete = "n/a";
            string stNw = "n/a", stBhv = "n/a", stNwText = "n/a", stNwSpontan = "n/a";
            string stAbgeschlossen = "n/a", stNeko = "n/a", stNrsKontakt= "n/a", stZustaendigkeit = "";
            bool pageIsValid;
            Button btn;

            SqlConnection cnConn;
            SqlCommand cmdConn;
            SqlDataReader drConn;

            btn = (Button)sender;

            if (btn.ID.Equals("btSave"))
            {
                mvNCom.SetActiveView(vwNComNo);
                upNCom.Update();
            }

            Validate("nkp_plaus");
            pageIsValid = IsValid;
            /*if (ddl_art.SelectedItem.Text.ToString() != "Kontaktversuch")
            {
                Validate("nkp_medan");
            }
            pageIsValid &= IsValid;*/

            if (pageIsValid)
            {
                if (btn.ID.Equals("btFinish"))
                {
                    lbsaved.Text = "";
                    if (ddl_art.SelectedItem.Text.ToString() != "Kontaktversuch") Validate("nkp_complete");
                    pageIsValid &= IsValid;
                    if (!pageIsValid)
                    {
                        if (bMedan)
                        {
                            mvNCom.SetActiveView(vwNCom);
                            upNCom.Update();
                        }
                    }
                }
            }
            else lbsaved.Text = "";
            upVds.Update();
            if (!pageIsValid) ddl_art.Focus();

            if (pageIsValid)
            {
                if ((btn.ID.Equals("btFinish")) || (btn.ID.Equals("btComplete")))
                {
                    stAbgeschlossen = "Ja";
                    lbsaved.Text = "";
                }
                else stAbgeschlossen = stAbgeschlossenRef;

                stGenDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                stBtkDate = ddl_procyear.SelectedItem.Text.ToString() + "-" + ddl_procmonth.SelectedItem.Text.ToString() + "-" + ddl_procday.SelectedItem.Text.ToString();
                stArt = ddl_art.SelectedItem.Text.ToString();
                stMedikament = Session["med"].ToString();

                if (stArt != "Kontaktversuch")
                {
                    stStatus = ddl_status.SelectedItem.Text.ToString();
                    if (mvStatusDatum.GetActiveView() == vwStatusDatum)
                    {
                        stStatusDate = ddl_statyear.SelectedItem.Text.ToString() + "-" + ddl_statmonth.SelectedItem.Text.ToString() + "-" + ddl_statday.SelectedItem.Text.ToString();
                    }
                    if (mvStatusGrund.GetActiveView() == vwStatusGrund) stStatusGrund = tbStatusGrund.Text.ToString();
                    if (mvStatusThewe.GetActiveView() == vwStatusThewe)
                        stThewe = (rbStatusThewe.SelectedItem == null) ? "" : rbStatusThewe.SelectedItem.Value.ToString();
                    if (mvTheweMed.GetActiveView() == vwTheweMed)
                    {
                        for (int i = 0; i < ddlTheweMed.Items.Count; i++)
                        {
                            if (ddlTheweMed.Items[i].Selected)
                            {
                                stTheweMed = ddlTheweMed.SelectedItem.Text.ToString();
                                if (stTheweMed == "Anderes Präparat")
                                {
                                    stTheweMed = tbTheweMedAndere.Text.ToString();
                                    if (stTheweMed == "") stTheweMed = ddlTheweMed.SelectedItem.Text.ToString();
                                }
                            }
                        }
                    }

                    if (mvMedFLS.GetActiveView() == vwMedFLS) stProphyFLS = (rbFLS.SelectedItem == null) ? "" : rbFLS.SelectedItem.Value.ToString();
                    if (mvFLS.GetActiveView() == vwFLS)
                    {
                        stFLSMexalen = cbMexalen.Checked ? "Ja" : "Nein";
                        stFLSNaproxen = cbNaproxen.Checked ? "Ja" : "Nein";
                        stFLSIbuprofen = cbIbuprofen.Checked ? "Ja" : "Nein";
                        if (cbAndere.Checked)
                        {
                            if (tbAndere.Text.ToString() == "") stFLSAndere = "Ja";
                            else stFLSAndere = tbAndere.Text.ToString();
                        }
                    }

                    if (mvHaut.GetActiveView() == vwHaut) stProphyHaut = (rbHaut.SelectedItem == null) ? "" : rbHaut.SelectedItem.Value.ToString();
                    if (mvMedHaut.GetActiveView() == vwMedHaut)
                    {
                        stHautCoolpack = cbCoolpack.Checked ? "Ja" : "Nein";
                        if (cbHautAndere.Checked)
                        {
                            if (tbHautAndere.Text.ToString() == "") stHautAndere = "Ja";
                            else stHautAndere = tbHautAndere.Text.ToString();
                        }
                    }

                    if (mvMedGastroFlush.GetActiveView() == vwMedGastroFlush)
                    {
                        stGastro = (rbGastro.SelectedItem == null) ? "" : rbGastro.SelectedItem.Value.ToString();
                        if (stGastro == "Ja") stGastroComment = (tbGastro.Text.ToString() == "") ? "keine Angabe" : tbGastro.Text.ToString();
                        stFlush = (rbFlush.SelectedItem == null) ? "" : rbFlush.SelectedItem.Value.ToString();
                        if (stFlush == "Ja") stFlushComment = (tbFlush.Text.ToString() == "") ? "keine Angabe" : tbFlush.Text.ToString();
                    }

                    if (mvInjek.GetActiveView() == vwInjek)
                    {
                        if (rbBioFeSp.SelectedItem == null)
                        {
                            stBioset = "Nein";
                            stFesp = "Nein";
                            stPen = "Nein";
                        }
                        else
                        {
                            stBioset = (rbBioFeSp.SelectedItem.Value == "Bioset") ? "Ja" : "Nein";
                            stFesp = (rbBioFeSp.SelectedItem.Value == "Fertigspritze") ? "Ja" : "Nein";
                            stPen = (rbBioFeSp.SelectedItem.Value == "Pen") ? "Ja" : "Nein";
                        }
                    }
                    if (mvNL.GetActiveView() == vwNL)
                    {
                        stNala = (rbNaLa.SelectedItem == null) ? "keine Angabe" : rbNaLa.SelectedItem.Value.ToString();
                    }
                    if (mvAA.GetActiveView() == vwAA)
                    {
                        stAAName = tbAA.Text.ToString();
                        stAAComment = tbAAComment.Text.ToString();
                        stAADate = ddl_AAyear.SelectedItem.Text.ToString() + "-" + ddl_AAmonth.SelectedItem.Text.ToString() + "-" + ddl_AAday.SelectedItem.Text.ToString();
                    }
                    if (mvInjekOrt.GetActiveView() == vwInjekOrt)
                    {
                        if (rbInjekOrt.SelectedItem == null)
                        {
                            stInjekOrt = "keine Angabe";
                        }
                        else
                        {
                            stInjekOrt = (rbInjekOrt.SelectedItem.Value.ToString());
                        }
                    }
                    if (mvAvoject.GetActiveView() == vwAvoject) stAvoject = (rbAvoject.SelectedItem == null) ? "" : rbAvoject.SelectedItem.Value.ToString();
                    if (mvTitDos.GetActiveView() == vwTit)
                    {
                        stTitration = (rbTitra.SelectedItem == null) ? "" : rbTitra.SelectedItem.Value.ToString();
                        if (mvTitra.GetActiveView() == vwTitraAvonex)
                        {
                            stTitrationDauer = ddl_titra.SelectedItem.Text.ToString();
                            stTitrationArt = (rbTitraArt.SelectedItem == null) ? "" : rbTitraArt.SelectedItem.Value.ToString();
                        }

                        if (mvTitra.GetActiveView() == vwTitraPlegridy)
                        {
                            stTitrationArt = (rbTitraPlegridy.SelectedItem == null) ? "" : rbTitraPlegridy.SelectedItem.Value.ToString();
                        }

                        if ((mvTitra.GetActiveView() == vwTitraNo) && (stMedRef == "Plegridy")) stTitrationArt = "125";

                    }

                    else
                    {
                        stTecfiDosis = (rbDos.SelectedItem == null) ? "" : rbDos.SelectedItem.Value.ToString();
                    }

                    if (mvAngSchule.GetActiveView() == vwAngSchule)
                    {
                        if (cbAngschule.Checked)
                        {
                            stAngschule = "Ja";
                            stAngschuleComment = tbAngschule.Text.ToString();
                        }
                        else stAngschule = "Nein";
                    }

                    stCurMass = tbCurMass.Text.ToString();
                    stBMI = lbBMI.Text.ToString();

                    stMedAn = (rbMedan.SelectedItem == null) ? "" : rbMedan.SelectedItem.Value.ToString();
                    if (stMedAn == "Ja") { 
                        stMedAnComment = tbMedan.Text.ToString();
                        stMedanComplete = cbMedanComplete.Checked ? "Ja" : "Nein";
                    }
                    else
                    {
                        stMedAnComment = "n/a";
                        stMedanComplete = "n/a";
                    }

                    if (mvNrsKontakt.GetActiveView() == vwNrsKontakt)
                        stNrsKontakt = (rbNurseKontakt.SelectedItem == null) ? "" : rbNurseKontakt.SelectedItem.Value.ToString();
                    else stNrsKontakt = "n/a";

                    if (rbZustaendigkeit.SelectedItem.Value == "Nein") stZustaendigkeit = stZustaendigkeitCur;
                    else {
                        if (lbZustaendigkeit.Text.ToString().Contains("Nurse")) stZustaendigkeit  = "Nurse";
                        else stZustaendigkeit = "Service";
                    }
                    

                    stNw = (rbNw.SelectedItem == null) ? "" : rbNw.SelectedItem.Value.ToString();
                    if (stNw == "Ja")
                    {
                        if (mvNw.GetActiveView() == vwNw)
                        {
                            stNwText = tbNw.Text.ToString();
                            stNwText = stNwText.Replace(";", "|");
                        }
                        stNwSpontan = cbSpontan.Checked ? "Ja" : "Nein";
                    }
                    else
                    {
                        stNwText = "n/a";
                        stNwSpontan = "n/a";
                    }

                    stBhv = tbBhv.Text.ToString();
                    stBhv = stBhv.Replace(";", "|");
                    stNeko = ddl_nekoyear.SelectedItem.Text.ToString() + "-" + ddl_nekomonth.SelectedItem.Text.ToString() + "-" + ddl_nekoday.SelectedItem.Text.ToString();
                    if (stNeko == "--") stNeko = "";
                }
                else
                {
                    stNeko = stNekoRef;
                    stStatus = stStatusRef;
                }

                string stQuelle = (Session["role"].ToString() == "nrs") ? "Nurse" : "Service";

                cnConn = (SqlConnection)DBTools.getConnection();
                cnConn.Open();

                if (Request["reason"] == "new")
                {
                    cmdConn = new SqlCommand("dbo.insert_btk", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@quelle", stQuelle));
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["patid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@sourceid", Session["id"]));
                    cmdConn.Parameters.Add(new SqlParameter("@gendate", stGenDate));
                    cmdConn.Parameters.Add(new SqlParameter("@btkdate", stBtkDate));
                    cmdConn.Parameters.Add(new SqlParameter("@art", stArt));
                    cmdConn.Parameters.Add(new SqlParameter("@status", stStatus));
                    cmdConn.Parameters.Add(new SqlParameter("@status_grund", stStatusGrund));
                    cmdConn.Parameters.Add(new SqlParameter("@status_date", stStatusDate));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", stMedikament));
                    cmdConn.Parameters.Add(new SqlParameter("@thewe", stThewe));
                    cmdConn.Parameters.Add(new SqlParameter("@thewemed", stTheweMed));
                    cmdConn.Parameters.Add(new SqlParameter("@prophy_fls", stProphyFLS));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_mexalen", stFLSMexalen));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_naproxen", stFLSNaproxen));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_ibuprofen", stFLSIbuprofen));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_andere", stFLSAndere));
                    cmdConn.Parameters.Add(new SqlParameter("@prophy_haut", stProphyHaut));
                    cmdConn.Parameters.Add(new SqlParameter("@haut_coolpack", stHautCoolpack));
                    cmdConn.Parameters.Add(new SqlParameter("@haut_andere", stHautAndere));
                    cmdConn.Parameters.Add(new SqlParameter("@gastro", stGastro));
                    cmdConn.Parameters.Add(new SqlParameter("@gastro_comment", stGastroComment));
                    cmdConn.Parameters.Add(new SqlParameter("@flush", stFlush));
                    cmdConn.Parameters.Add(new SqlParameter("@flush_comment", stFlushComment));
                    cmdConn.Parameters.Add(new SqlParameter("@pen", stPen));
                    cmdConn.Parameters.Add(new SqlParameter("@bioset", stBioset));
                    cmdConn.Parameters.Add(new SqlParameter("@fesp", stFesp));
                    cmdConn.Parameters.Add(new SqlParameter("@nala", stNala));
                    cmdConn.Parameters.Add(new SqlParameter("@aaname", stAAName));
                    cmdConn.Parameters.Add(new SqlParameter("@aadate", stAADate));
                    cmdConn.Parameters.Add(new SqlParameter("@aacomment", stAAComment));
                    cmdConn.Parameters.Add(new SqlParameter("@injekort", stInjekOrt));
                    cmdConn.Parameters.Add(new SqlParameter("@avoject", stAvoject));
                    cmdConn.Parameters.Add(new SqlParameter("@titration", stTitration));
                    cmdConn.Parameters.Add(new SqlParameter("@titration_dauer", stTitrationDauer));
                    cmdConn.Parameters.Add(new SqlParameter("@titration_art", stTitrationArt));
                    cmdConn.Parameters.Add(new SqlParameter("@tecfidosis", stTecfiDosis));
                    cmdConn.Parameters.Add(new SqlParameter("@angschule", stAngschule));
                    cmdConn.Parameters.Add(new SqlParameter("@angschulecomment", stAngschuleComment));
                    cmdConn.Parameters.Add(new SqlParameter("@curmass", stCurMass));
                    cmdConn.Parameters.Add(new SqlParameter("@groesse", stGroesseRef));
                    cmdConn.Parameters.Add(new SqlParameter("@bmi", stBMI));
                    cmdConn.Parameters.Add(new SqlParameter("@nw", stNw));
                    cmdConn.Parameters.Add(new SqlParameter("@bhv", stBhv));
                    cmdConn.Parameters.Add(new SqlParameter("@nwtext", stNwText));
                    cmdConn.Parameters.Add(new SqlParameter("@nwspontan", stNwSpontan));
                    cmdConn.Parameters.Add(new SqlParameter("@neko", stNeko));
                    cmdConn.Parameters.Add(new SqlParameter("@medan", stMedAn));
                    cmdConn.Parameters.Add(new SqlParameter("@medancomment", stMedAnComment));
                    cmdConn.Parameters.Add(new SqlParameter("@medancomplete", stMedanComplete));
                    cmdConn.Parameters.Add(new SqlParameter("@abgeschlossen", stAbgeschlossen));
                    cmdConn.Parameters.Add(new SqlParameter("@nrskontakt", stNrsKontakt));
                    cmdConn.ExecuteNonQuery();
                    cnConn.Close();
                    cnConn.Open();
                    cmdConn = new SqlCommand("dbo.get_btkbypatid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["patid"]));
                    drConn = cmdConn.ExecuteReader();
                    drConn.Read();
                    Session["btkid"] = drConn["btkid"].ToString();
                    drConn.Close();
                    cnConn.Close();
                    cnConn.Open();
                    cmdConn = new SqlCommand("dbo.update_patdatbybtkid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["patid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", Session["btkid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@zustaendigkeit",stZustaendigkeit));
                    cmdConn.ExecuteNonQuery();
                }

                if (Request["reason"] == "edit")
                {
                    cmdConn = new SqlCommand("dbo.update_btk", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", Session["btkid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["patid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@sourceid", Session["id"]));
                    cmdConn.Parameters.Add(new SqlParameter("@gendate", stGenDate));
                    cmdConn.Parameters.Add(new SqlParameter("@btkdate", stBtkDate));
                    cmdConn.Parameters.Add(new SqlParameter("@art", stArt));
                    cmdConn.Parameters.Add(new SqlParameter("@status", stStatus));
                    cmdConn.Parameters.Add(new SqlParameter("@status_grund", stStatusGrund));
                    cmdConn.Parameters.Add(new SqlParameter("@status_date", stStatusDate));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", stMedikament));
                    cmdConn.Parameters.Add(new SqlParameter("@thewe", stThewe));
                    cmdConn.Parameters.Add(new SqlParameter("@thewemed", stTheweMed));
                    cmdConn.Parameters.Add(new SqlParameter("@prophy_fls", stProphyFLS));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_mexalen", stFLSMexalen));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_naproxen", stFLSNaproxen));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_ibuprofen", stFLSIbuprofen));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_andere", stFLSAndere));
                    cmdConn.Parameters.Add(new SqlParameter("@prophy_haut", stProphyHaut));
                    cmdConn.Parameters.Add(new SqlParameter("@haut_coolpack", stHautCoolpack));
                    cmdConn.Parameters.Add(new SqlParameter("@haut_andere", stHautAndere));
                    cmdConn.Parameters.Add(new SqlParameter("@gastro", stGastro));
                    cmdConn.Parameters.Add(new SqlParameter("@gastro_comment", stGastroComment));
                    cmdConn.Parameters.Add(new SqlParameter("@flush", stFlush));
                    cmdConn.Parameters.Add(new SqlParameter("@flush_comment", stFlushComment));
                    cmdConn.Parameters.Add(new SqlParameter("@pen", stPen));
                    cmdConn.Parameters.Add(new SqlParameter("@bioset", stBioset));
                    cmdConn.Parameters.Add(new SqlParameter("@fesp", stFesp));
                    cmdConn.Parameters.Add(new SqlParameter("@nala", stNala));
                    cmdConn.Parameters.Add(new SqlParameter("@aaname", stAAName));
                    cmdConn.Parameters.Add(new SqlParameter("@aadate", stAADate));
                    cmdConn.Parameters.Add(new SqlParameter("@aacomment", stAAComment));
                    cmdConn.Parameters.Add(new SqlParameter("@injekort", stInjekOrt));
                    cmdConn.Parameters.Add(new SqlParameter("@avoject", stAvoject));
                    cmdConn.Parameters.Add(new SqlParameter("@titration", stTitration));
                    cmdConn.Parameters.Add(new SqlParameter("@titration_dauer", stTitrationDauer));
                    cmdConn.Parameters.Add(new SqlParameter("@titration_art", stTitrationArt));
                    cmdConn.Parameters.Add(new SqlParameter("@tecfidosis", stTecfiDosis));
                    cmdConn.Parameters.Add(new SqlParameter("@angschule", stAngschule));
                    cmdConn.Parameters.Add(new SqlParameter("@angschulecomment", stAngschuleComment));
                    cmdConn.Parameters.Add(new SqlParameter("@curmass", stCurMass));
                    cmdConn.Parameters.Add(new SqlParameter("@groesse", stGroesseRef));
                    cmdConn.Parameters.Add(new SqlParameter("@bmi", stBMI));
                    cmdConn.Parameters.Add(new SqlParameter("@nw", stNw));
                    cmdConn.Parameters.Add(new SqlParameter("@bhv", stBhv));
                    cmdConn.Parameters.Add(new SqlParameter("@nwtext", stNwText));
                    cmdConn.Parameters.Add(new SqlParameter("@nwspontan", stNwSpontan));
                    cmdConn.Parameters.Add(new SqlParameter("@neko", stNeko));
                    cmdConn.Parameters.Add(new SqlParameter("@medan", stMedAn));
                    cmdConn.Parameters.Add(new SqlParameter("@medancomment", stMedAnComment));
                    cmdConn.Parameters.Add(new SqlParameter("@medancomplete", stMedanComplete));
                    cmdConn.Parameters.Add(new SqlParameter("@abgeschlossen", stAbgeschlossen));
                    cmdConn.Parameters.Add(new SqlParameter("@nrskontakt", stNrsKontakt));
                    cmdConn.ExecuteNonQuery();
                    cnConn.Close();
                    cnConn.Open();
                    cmdConn = new SqlCommand("dbo.update_patdatbybtkid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["patid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", Session["btkid"]));
                    cmdConn.Parameters.Add(new SqlParameter("@zustaendigkeit", stZustaendigkeit));
                    cmdConn.ExecuteNonQuery();

                    newEntry = manageAuditTrail.get_entry("get_btkbybtkid", Convert.ToInt32(Request["btkid"]), "@btkid");
                    manageAuditTrail.audit_changes(oldEntry, newEntry, fieldName, Convert.ToInt32(Session["userid"]), 4, Convert.ToInt32(Request["dksid"]));
                }
                lbsaved.Text = "Daten wurden gespeichert.";
                upVds.Update();
                btCancel.Text = "Beenden";

                string stSender = "";
                string stEmail = "";
                string to = "";
                string cc = "";
                string from = "";
                string subject = "";
                string msgb = "";
                string stSenderTel = "";
                string stPatInfo = "";
                string stBtkInfo = "";
                MailMessage message;

                string stId = Session["titel"].ToString() + " " + Session["forename"].ToString() + " " + Session["name"].ToString();

                cnConn = (SqlConnection)DBTools.getConnection();
                cnConn.Open();

                cmdConn = new SqlCommand("dbo.get_account", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@id", Convert.ToInt32(Session["id"])));
                drConn = cmdConn.ExecuteReader();
                drConn.Read();
                stSender = drConn["email"].ToString();
                string stSenderName = drConn["name"].ToString() + ", " + drConn["forename"].ToString();
                stSenderTel = drConn["phone"].ToString();
                drConn.Close();

                cmdConn = new SqlCommand("dbo.get_patdat", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Convert.ToInt32(Session["PatID"])));
                drConn = cmdConn.ExecuteReader();
                drConn.Read();
                string stSex = drConn["geschlecht"].ToString() == "M" ? "männlich" : "weiblich";
                string stPat = "Patient ist " + stSex + ",";
                string stGb = drConn["gebdat"].ToString();
                string stTecfiStart = drConn["thestart"].ToString();
                if (stGb.Length >= 7)
                {
                    stPat = String.Concat(stPat, " geboren  " + drConn["gebdat"].ToString().Substring(0, 7));
                }
                else
                {
                    stPat = String.Concat(stPat, " geboren  " + drConn["gebdat"].ToString().Substring(0, stGb.Length));
                }
                stPatInfo = "\nID Patient: " + drConn["patid"].ToString();
                stBtkInfo = "ID Betreuungskontakt: " + Session["btkid"].ToString();
                drConn.Close();

                bWechsel = false;
                if ((btn.ID.Equals("btFinish")) || (btn.ID.Equals("btComplete")))
                {
                    cmdConn = new SqlCommand("dbo.update_patdatbybtk", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Convert.ToInt32(Session["patid"])));
                    cmdConn.Parameters.Add(new SqlParameter("@status", stStatus));
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", Convert.ToInt32(Session["btkid"])));
                    if (stArt != "Kontaktversuch")
                    {
                        cmdConn.Parameters.Add(new SqlParameter("@leko", stBtkDate));
                        cmdConn.Parameters.Add(new SqlParameter("@neko", stNeko));
                    }
                    else
                    {
                        cmdConn.Parameters.Add(new SqlParameter("@leko", stLekoRef));
                        cmdConn.Parameters.Add(new SqlParameter("@neko", stNekoRef));
                    }


                    cmdConn.ExecuteNonQuery();
                }


                cmdConn = new SqlCommand("dbo.get_verteilerNW", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                drConn = cmdConn.ExecuteReader();
                while (drConn.Read())
                {
                    if (drConn["typ"].ToString() == "to") to = String.Concat(to, drConn["email"].ToString(), "|");
                    if (drConn["typ"].ToString() == "cc") cc = String.Concat(cc, drConn["email"].ToString(), "|");
                    if (drConn["typ"].ToString() == "from") from = String.Concat(from, drConn["email"].ToString());
                }
                cc = String.Concat(cc, stSender, "|");
                drConn.Close();

                if (stNwTextOld != stNwText)
                {
                    msgb = "Sehr geehrte Damen und Herren!\n\n";
                    if (Request["reason"] == "new")
                    {
                        if (stNwSpontan == "Ja") subject = "PSP Service::Nebenwirkung dokumentiert - SPONTANMELDUNG";
                        else subject = "PSP Service::Nebenwirkung dokumentiert";
                        msgb = String.Concat(msgb, "Für den Patienten mit der ID " + Session["PatID"].ToString() + " wurde eine Nebenwirkung dokumentiert.");
                    }
                    else
                    {
                        if (stNwSpontan == "Ja") subject = "PSP Service::Nebenwirkung oder Behandlungsverlauf geändert - Original SPONTANMELDUNG";
                        else subject = "TecfiCare::Nebenwirkung oder Behandlungsverlauf geändert";
                        subject = "PSP Service::Nebenwirkung oder Behandlungsverlauf geändert";
                        msgb = String.Concat(msgb, "Für den Patienten mit der ID " + Session["PatID"].ToString() + " wurde die Dokumentation einer Nebenwirkung geändert.");
                    }

                    subject += "  !!!TEST only!!! - !!!Ignore!!!";
                    msgb += "\n\n!!!TEST only!!! - !!!Ignore!!!\n\n";

                    msgb = String.Concat(msgb, "\n\n" + stPat);
                    msgb = String.Concat(msgb, "\nMedikament: " + Session["med"].ToString());
                    msgb = String.Concat(msgb, "\nTherapiestart: " + stTecfiStart);
                    msgb = String.Concat(msgb, "\n\nNebenwirkung: " + stNwText + "\n");
                    msgb = String.Concat(msgb, "\nDatenquelle: " + stSenderName + " | " + stSender);
                    msgb = String.Concat(msgb, "\nTelefon: " + stSenderTel);
                    msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\nPSP Service System");
                    message = new MailMessage();
                    /*if (stNw == "Ja") msdbEmail.SentEmail(stEmail, from, to, cc, subject, msgb);*/
                }

                to = "";
                cc = "";
                from = "";
                msgb = "";
                subject = "";
                cmdConn = new SqlCommand("dbo.get_VerteilerMA", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                drConn = cmdConn.ExecuteReader();
                while (drConn.Read())
                {
                    if (drConn["typ"].ToString() == "to") to = String.Concat(to, drConn["email"].ToString(), "|");
                    if (drConn["typ"].ToString() == "cc") cc = String.Concat(cc, drConn["email"].ToString(), "|");
                    if (drConn["typ"].ToString() == "from") from = String.Concat(from, drConn["email"].ToString());
                }
                /*cc = String.Concat(cc, stSender, "|");*/
                drConn.Close();

                Boolean bMed = false;


                if (stMedanCommentOld != stMedAnComment) bMed = true;
                if (bMed)
                {
                    msgb = "Sehr geehrte Damen und Herren!\n\n";
                    if (Request["reason"] == "new")
                    {
                        subject = "PSP Service::Medizinische Anfrage dokumentiert";
                        msgb = String.Concat(msgb, "Für den Patienten mit der ID " + Session["PatID"].ToString() + " wurde eine Medizinische Anfrage dokumentiert.");

                    }
                    else
                    {
                        subject = "PSP::Medizinische Anfrage geändert";
                        msgb = String.Concat(msgb, "Für den Patienten mit der ID " + Session["PatID"].ToString() + " wurde die Dokumentation einer Medizinischen Anfrage geändert.");
                    }

                    subject += "  !!!TEST only!!! - !!!Ignore!!!";
                    msgb += "\n\n!!!TEST only!!! - !!!Ignore!!!\n\n";

                    msgb = String.Concat(msgb, "\n\n" + stPat);
                    msgb = String.Concat(msgb, "\nMedikament: " + Session["med"].ToString());
                    /*msgb = String.Concat(msgb, "\n" + stBtkInfo);*/
                    /*msgb = String.Concat(msgb, "\nMedizinische Anfrage: " + stMedAn1 + " " + stMedAn2 + " " + stMedAn3 + " " + stMedAn4 + " " + stMedAn5 + " " + stMedAnNw + " " + stMedAnSonstige + " " + stMedAnComment + " " + "\n");*/
                    msgb = String.Concat(msgb, "\nMedizinische Anfrage: " + stMedAnComment + " " + "\n");
                    if (stMedanComplete == "Ja") msgb = String.Concat(msgb, "\nAnfrage abgeschlossen\n");
                    else msgb = String.Concat(msgb, "\nAnfrage offen\n");
                    msgb = String.Concat(msgb, "\nDatenquelle: " + stSenderName + " | " + stSender);
                    msgb = String.Concat(msgb, "\nTelefon: " + stSenderTel);
                    msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\nTecfiCare Service System");
                    message = new MailMessage();
                    /*if (stMedAn == "Ja") msdbEmail.SentEmail(stEmail, from, to, cc, subject, msgb);*/
                }

                to = "";
                cc = "";
                from = "";
                msgb = "";
                subject = "";
                cmdConn = new SqlCommand("dbo.get_VerteilerNRSKONTAKT", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                drConn = cmdConn.ExecuteReader();
                while (drConn.Read())
                {
                    if (drConn["typ"].ToString() == "to") to = String.Concat(to, drConn["email"].ToString(), "|");
                    if (drConn["typ"].ToString() == "cc") cc = String.Concat(cc, drConn["email"].ToString(), "|");
                    if (drConn["typ"].ToString() == "from") from = String.Concat(from, drConn["email"].ToString());
                }
                drConn.Close();

                Boolean bNrsKontakt = false;


                if (stNrsKontaktOld != stNrsKontakt) bNrsKontakt = true;
                if (bNrsKontakt)
                {
                    msgb = "Sehr geehrte Damen und Herren!\n\n";
                    if (stNrsKontakt == "Ja")
                    {
                        subject = "PSP Service::Wunsch nach Nurse Kontakt";
                        msgb = String.Concat(msgb, "Für den Patienten mit der ID " + Session["PatID"].ToString() + " wurde der Wunsch nach Nurse Kontakt auf Ja gesetzt.");
                    }
                    else
                    {
                        subject = "PSP::Wunsch nach Nurse Kontakt zurückgezogen ";
                        msgb = String.Concat(msgb, "Für den Patienten mit der ID " + Session["PatID"].ToString() + " wurde der Wunsch nach Nurse Kontakt auf Nein gesetzt.");
                        msgb = String.Concat(msgb, "\nBitte ignorieren Sie das E-Mail, falls der Kontakt bereits stattgefunden hat.");
                    }

                    subject += "  !!!TEST only!!! - !!!Ignore!!!";
                    msgb += "\n\n!!!TEST only!!! - !!!Ignore!!!\n\n";

                    msgb = String.Concat(msgb, "\n\n" + stPat);
                    msgb = String.Concat(msgb, "\nMedikament: " + Session["med"].ToString());
                    msgb = String.Concat(msgb, "\nQuelle: " + stSenderName + " | " + stSender);
                    msgb = String.Concat(msgb, "\nTelefon: " + stSenderTel);
                    msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\nPSP Service");
                    message = new MailMessage();
                    /*if (bNrsKontkat) msdbEmail.SentEmail(stEmail, from, to, cc, subject, msgb);*/
                }

                string reurl = "~/general/detailPat.aspx?patid=" + Session["patid"].ToString();
                if (!sender.Equals(btSave))
                {
                    cnConn.Close();
                    Response.Redirect(reurl);
                }
                else
                {
                    cmdConn = new SqlCommand("dbo.get_btkbypatid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["patid"]));
                    drConn = cmdConn.ExecuteReader();
                    drConn.Read();
                    reurl = "~/general/newBtk.aspx?patid=" + Session["patid"].ToString() + "&reason=edit&btkid=" + drConn["btkid"] + "&src=save";
                    drConn.Close();
                    cnConn.Close();
                    Response.Redirect(reurl);
                }
            }
        }

        protected void bt_cancel_click(object sender, EventArgs e)
        {
            string reurl = "~/general/detailPat.aspx?patid=" + Session["patid"].ToString();
            Response.Redirect(reurl);
        }

        protected void rbNw_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbNw.SelectedValue.ToString() == "Ja")
            {
                mvNw.SetActiveView(vwNw);
                cbSpontan.Enabled = true;
            }
            else
            {
                mvNw.SetActiveView(vwNoNw);
                tbNw.Text = "";
                cbSpontan.Checked = false;
                cbSpontan.Enabled = false;
            }
            upNw.Update();
            upSpontan.Update();
        }

        protected void ddl_status_selectedindexchanged(object sender, EventArgs e)
        {

            switch (ddl_status.SelectedItem.Text.ToString())
            {
                case "Fortsetzung":
                    mvStatusDatum.SetActiveView(vwStatusDatumNo);
                    mvStatusGrund.SetActiveView(vwStatusGrundNo);
                    mvStatusThewe.SetActiveView(vwStatusTheweNo);
                    mvTheweMed.SetActiveView(vwTheweMedNo);
                    /*tbTheweMedAndere.Text = "";
                    rbStatusThewe.ClearSelection();
                    ddlTheweMed.SelectedIndex = 0;*/
                    break;
                case "Beginn":
                    mvStatusDatum.SetActiveView(vwStatusDatum);
                    mvStatusGrund.SetActiveView(vwStatusGrundNo);
                    mvStatusThewe.SetActiveView(vwStatusTheweNo);
                    mvTheweMed.SetActiveView(vwTheweMedNo);
                    break;
                case "Wiederaufnahme":
                    mvStatusDatum.SetActiveView(vwStatusDatum);
                    mvStatusGrund.SetActiveView(vwStatusGrund);
                    mvStatusThewe.SetActiveView(vwStatusTheweNo);
                    mvTheweMed.SetActiveView(vwTheweMedNo);
                    break;
                case "Abbruch":
                    mvStatusDatum.SetActiveView(vwStatusDatum);
                    mvStatusGrund.SetActiveView(vwStatusGrund);
                    mvStatusThewe.SetActiveView(vwStatusThewe);
                    if (rbStatusThewe.SelectedIndex == 0)
                    {
                        mvTheweMed.SetActiveView(vwTheweMed);
                    }
                    else
                    {
                        mvTheweMed.SetActiveView(vwTheweMedNo);
                    }

                    break;
                default:
                    break;
            }

            upStatusDatum.Update();
            upStatus.Update();
            upStatusThewe.Update();
            upTheweMed.Update();
        }


        protected void cvValddlStatusDatum_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string stDate, stYear, stMonth, stDay;

            stYear = ddl_statyear.SelectedItem.Text.ToString();
            stMonth = ddl_statmonth.SelectedItem.Text.ToString();
            stDay = ddl_statday.SelectedItem.Text.ToString();

            stDate = stYear + "-" + stMonth + "-" + stDay;

            string ErrorMessage = Utils.CheckFullDate(stDate, "Back");

            if (ErrorMessage != "")
            {
                cvStatusDatum.ErrorMessage = "Datum Betreuungskontakt: " + ErrorMessage;
                cvStatusDatumPlaus.ErrorMessage = "Datum Statusänderung: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;
        }

        protected void cvValddlNekoDatum_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string stDate, stYear, stMonth, stDay;

            stYear = ddl_nekoyear.SelectedItem.Text.ToString();
            stMonth = ddl_nekomonth.SelectedItem.Text.ToString();
            stDay = ddl_nekoday.SelectedItem.Text.ToString();

            stDate = stYear + "-" + stMonth + "-" + stDay;

            string ErrorMessage = Utils.CheckFullDate(stDate, "Forward");

            if (ErrorMessage != "")
            {
                cvNekoDatum.ErrorMessage = "Datum nächster Kontakt: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;
        }


        protected void rbStatusThewe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stStatusThewe;

            stStatusThewe = rbStatusThewe.SelectedItem.Value.ToString();

            if (stStatusThewe == "Ja") mvTheweMed.SetActiveView(vwTheweMed);
            else mvTheweMed.SetActiveView(vwTheweMedNo);

            upTheweMed.Update();
        }


        protected void ddlTheweMed_selectedindexchanged(object sender, EventArgs e)
        {
            string stTheweMed;

            stTheweMed = ddlTheweMed.SelectedItem.Text.ToString();

            if (stTheweMed == "Anderes Präparat")
            {
                tbTheweMedAndere.Enabled = true;
            }
            else
            {
                tbTheweMedAndere.Enabled = false;
                tbTheweMedAndere.Text = "";
            }

            upTheweMed.Update();
        }

        protected void ddl_art_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stArt = ddl_art.SelectedItem.Text.ToString();
            if (stArt == "Kontaktversuch")
            {
                btSave.Enabled = false;
                btSave.CssClass = "button.disabled";

            }
            else
            {
                btSave.Enabled = true;
                btSave.CssClass = "button";
                stKV = "nkv";
            }
            upButtons.Update();
        }
        protected void rbMedan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stMedan;

            stMedan = rbMedan.SelectedItem.Value.ToString();

            if (stMedan == "Ja")
            {
                mvMedan.SetActiveView(vwMedan);
            }
            else
            {
                tbMedan.Text = "";
                cbMedan1.Checked = false;
                mvMedan.SetActiveView(vwMedanNo);
            }

            upMedan.Update();
        }

        protected void cvMedan_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (ddl_art.SelectedItem.Text.ToString() == "Kontaktversuch") return;

            if (rbMedan.SelectedItem.Value.ToString() == "Ja")
            {
                if (!cbMedanComplete.Checked)
                {
                    args.IsValid = false;
                    cvMedan.ErrorMessage = "Medizinische Anfrage ist nicht als erledigt gekennzeichnet. Ein Abschluß des Betreuungskontaktes ist nicht möglich!";
                    bMedan = false;
                }
            }
        }
        protected void btDefault_Click(object sender, EventArgs e)
        {
            int i, j, multi = 0;
            try
            {
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_btkbypatid", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();
                string stArt = drConn["art"].ToString();
                ddl_art.SelectedIndex = -1;
                if (stArt == "Einschulungsbesuch 3") stArt = "Weiterer Einschulungsbesuch";
                    for (i = 0; i < ddl_art.Items.Count; i++)
                    {
                        if (ddl_art.Items[i].Value.ToString() == stArt)
                        {
                            ddl_art.Items[i].Selected = true;
                            multi++;
                        }
                    }
                    multi = 0;
                    ddl_status.SelectedIndex = -1;
                    string stStatus = drConn["status"].ToString();
                    for (i = 0; i < ddl_status.Items.Count; i++)
                    {
                        if (ddl_status.Items[i].Value.ToString() == stStatus)
                        {
                            ddl_status.Items[i].Selected = true;
                            multi++;
                        }
                    }

                    string[] arStatusIsDate = { "Beginn", "Abbruch", "Wiederaufnahme" };

                    string ststatday = "";
                    string ststatmonth = "";
                    string ststatyear = "";
                    multi = 0;
                    if (((IList<string>)arStatusIsDate).Contains(stStatus))
                    {
                        mvStatusDatum.SetActiveView(vwStatusDatum);
                        ddl_statday.SelectedIndex = -1;
                        ddl_statmonth.SelectedIndex = -1;
                        ddl_statyear.SelectedIndex = -1;
                        string stStatusDatum = drConn["status_date"].ToString();
                        if (stStatusDatum.Length > 9)
                        {
                            ststatday = stStatusDatum.Substring(8, 2);
                            ststatmonth = stStatusDatum.Substring(5, 2);
                            ststatyear = stStatusDatum.Substring(0, 4);
                            for (i = 0; i < ddl_statday.Items.Count; i++)
                            {
                                if (ddl_statday.Items[i].Value.ToString() == ststatday)
                                {
                                    ddl_statday.Items[i].Selected = true;
                                    multi++;
                                }
                            }

                            for (i = 0; i < ddl_statmonth.Items.Count; i++)
                            {
                                if (ddl_statmonth.Items[i].Value.ToString() == ststatmonth)
                                {
                                    ddl_statmonth.Items[i].Selected = true;
                                    multi++;
                                }
                            }

                            for (i = 0; i < ddl_statyear.Items.Count; i++)
                            {
                                if (ddl_statyear.Items[i].Value.ToString() == ststatyear)
                                {
                                    ddl_statyear.Items[i].Selected = true;
                                    multi++;
                                }
                            }
                        }

                    }

                    string[] arStatus = { "Abbruch", "Wiederaufnahme" };

                    if (((IList<string>)arStatus).Contains(stStatus))
                    {
                        mvStatusGrund.SetActiveView(vwStatusGrund);
                        string stStatusGrund = drConn["status_grund"].ToString();
                        tbStatusGrund.Text = stStatusGrund;
                    }
                    
                    if (stStatus == "Abbruch") mvStatusThewe.SetActiveView(vwStatusThewe);
                    multi = 0;
                    string stThewe = drConn["thewe"].ToString();
                    if (stThewe == "Ja")
                    {
                        rbStatusThewe.Items[0].Selected = true;
                        mvTheweMed.SetActiveView(vwTheweMed);
                        string stTheweMed = drConn["thewemed"].ToString();
                        Boolean bFound = false;
                        ddlTheweMed.SelectedIndex = -1;
                        for (i = 0; i < ddlTheweMed.Items.Count; i++)
                        {
                            if (ddlTheweMed.Items[i].Value.ToString() == stTheweMed)
                            {
                                ddlTheweMed.Items[i].Selected = true;
                                bFound = true;
                                multi++;
                            }
                        }
                        if (!bFound)
                        {
                            ddlTheweMed.Items[ddlTheweMed.Items.Count - 1].Selected = true;
                            tbTheweMedAndere.Enabled = true;
                            tbTheweMedAndere.Text = drConn["thewemed"].ToString();
                        }
                    }

                    else
                    {
                        rbStatusThewe.Items[1].Selected = true;
                        mvTheweMed.SetActiveView(vwTheweMedNo);
                    }

                    if (drConn["prophy_fls"].ToString() == "Ja")
                    {
                        mvFLS.SetActiveView(vwFLS);
                        rbFLS.SelectedIndex = 0;
                        if (drConn["fls_mexalen"].ToString() == "Ja") cbMexalen.Checked = true;
                        if (drConn["fls_naproxen"].ToString() == "Ja") cbNaproxen.Checked = true;
                        if (drConn["fls_ibuprofen"].ToString() == "Ja") cbIbuprofen.Checked = true;
                        if (drConn["fls_andere"].ToString() != "n/a")
                        {
                            cbAndere.Checked = true;
                            if (drConn["fls_andere"].ToString() != "Ja")
                            {
                                tbAndere.Text = drConn["fls_andere"].ToString();
                                tbAndere.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        mvFLS.SetActiveView(vwFLSNo);
                        rbFLS.SelectedIndex = 1;
                    }

                    if (drConn["prophy_haut"].ToString() == "Ja")
                    {
                        mvHaut.SetActiveView(vwHaut);
                        rbHaut.SelectedIndex = 0;
                        if (drConn["haut_coolpack"].ToString() == "Ja") cbCoolpack.Checked = true;
                        if (drConn["haut_andere"].ToString() != "n/a")
                        {
                            cbHautAndere.Checked = true;
                            if (drConn["haut_andere"].ToString() != "Ja")
                            {
                                tbHautAndere.Text = drConn["haut_andere"].ToString();
                                tbHautAndere.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        mvHaut.SetActiveView(vwHautNo);
                        rbHaut.SelectedIndex = 1;
                    }

                    if (drConn["gastro"].ToString() == "Ja")
                    {
                        rbGastro.SelectedIndex = 0;
                        tbGastro.Text = drConn["gastro_comment"].ToString();
                        tbGastro.Enabled = true;
                    }
                    else
                    {
                        rbGastro.SelectedIndex = 1;
                        tbGastro.Text = "";
                        tbGastro.Enabled = false;
                    }

                    if (drConn["flush"].ToString() == "Ja")
                    {
                        rbFlush.SelectedIndex = 0;
                        tbFlush.Text = drConn["flush_comment"].ToString();
                        tbFlush.Enabled = true;
                    }
                    else
                    {
                        rbFlush.SelectedIndex = 1;
                        tbFlush.Text = "";
                        tbFlush.Enabled = false;
                    }

                    bool bInjek = false;
                    string stBio = drConn["bioset"].ToString();
                    if (stBio == "Ja") { rbBioFeSp.Items[1].Selected = true; bInjek = true; }

                    string stFesp = drConn["fesp"].ToString();
                    if (stFesp == "Ja") { rbBioFeSp.Items[2].Selected = true; bInjek = true; }

                    string stPen = drConn["pen"].ToString();
                    if (stPen == "Ja") rbBioFeSp.Items[0].Selected = true;

                    bool bNala = false;
                    mvNL.SetActiveView(vwNoNL);
                    if ((bInjek) && (stMedRef == "Avonex"))
                    {

                        mvNL.SetActiveView(vwNL);
                        string stNala = drConn["nala"].ToString();
                        if (stNala == "Standard") { rbNaLa.Items[0].Selected = true; bNala = false; }
                        if (stNala == "Lang") { rbNaLa.Items[1].Selected = true; bNala = true; }
                        if (stNala == "Kurz") { rbNaLa.Items[2].Selected = true; bNala = true; }
                        if (stNala == "Keine Angabe") { rbNaLa.Items[3].Selected = true; bNala = false; }
                    }

                    if (bNala)
                    {
                        mvAA.SetActiveView(vwAA);
                        string stAA = drConn["aaname"].ToString();
                        tbAA.Text = stAA;
                        tbAAComment.Text = drConn["aacomment"].ToString();
                        string stAADatum = drConn["aadate"].ToString();
                        multi = 0;
                        ddl_AAday.SelectedIndex = -1;
                        ddl_AAmonth.SelectedIndex = -1;
                        ddl_AAyear.SelectedIndex = -1;
                        for (i = 0; i < ddl_AAday.Items.Count; i++)
                        {
                            if (ddl_AAday.Items[i].Value.ToString() == stAADatum.Substring(8, 2))
                            {
                                ddl_AAday.Items[i].Selected = true;
                                multi++;
                            }
                        }

                        for (i = 0; i < ddl_AAmonth.Items.Count; i++)
                        {
                            if (ddl_AAmonth.Items[i].Value.ToString() == stAADatum.Substring(5, 2))
                            {
                                ddl_AAmonth.Items[i].Selected = true;
                                multi++;
                            }
                        }

                        for (i = 0; i < ddl_AAyear.Items.Count; i++)
                        {
                            if (ddl_AAyear.Items[i].Value.ToString() == stAADatum.Substring(0, 4))
                            {
                                ddl_AAyear.Items[i].Selected = true;
                                multi++;
                            }
                        }
                    }

                    string stAvoject = drConn["avoject"].ToString();
                    if (stAvoject == "Ja") { rbAvoject.Items[0].Selected = true; mvAvoject.SetActiveView(vwAvoject); }
                    else { rbAvoject.Items[1].Selected = true; mvAvoject.SetActiveView(vwAvojectNo); }

                    string stTitration = drConn["titration"].ToString();
                    string stTecfiDosis;
                    string stTitraDauer, stTitraArt;
                    stTitraArt = drConn["titration_art"].ToString();
                    stTecfiDosis = drConn["tecfidosis"].ToString();
                    if (stTitration == "Ja")
                    {
                        rbTitra.Items[0].Selected = true;
                        if (stMedRef == "Avonex")
                        {
                            mvTitra.SetActiveView(vwTitraAvonex);
                            stTitraDauer = drConn["titration_dauer"].ToString();
                            for (i = 0; i < ddl_titra.Items.Count; i++) if (ddl_titra.Items[i].Value.ToString() == stTitraDauer) ddl_titra.Items[i].Selected = true;
                            if (stTitraArt == "1/4") rbTitraArt.Items[0].Selected = true;
                            if (stTitraArt == "1/2") rbTitraArt.Items[1].Selected = true;
                            if (stTitraArt == "3/4") rbTitraArt.Items[2].Selected = true;
                        }
                        else
                        {
                            mvTitra.SetActiveView(vwTitraPlegridy);
                            if (stTitraArt == "63") rbTitraPlegridy.Items[0].Selected = true;
                            if (stTitraArt == "94") rbTitraPlegridy.Items[1].Selected = true;
                            if (stTitraArt == "125") rbTitraPlegridy.Items[2].Selected = true;
                        }
                    }
                    else
                    {
                        rbTitra.Items[1].Selected = true;
                        if (stTecfiDosis == "120") rbDos.Items[0].Selected = true;
                        if (stTecfiDosis == "240") rbDos.Items[1].Selected = true;
                    }


                    string stAngschule = drConn["angschule"].ToString();
                    if (stAngschule == "Ja")
                    {
                        mvAngSchule.SetActiveView(vwAngSchule);
                        cbAngschule.Checked = true;
                        tbAngschule.Text = drConn["angschulecomment"].ToString();
                    }
                    else mvAngSchule.SetActiveView(vwAngSchuleNo);

                    string stInjekOrt = drConn["injekort"].ToString();
                    if (stInjekOrt != "n/a")
                    {
                        mvInjekOrt.SetActiveView(vwInjekOrt);
                        foreach (ListItem item in rbInjekOrt.Items)
                            switch (item.Value)
                            {
                                case "Oberarm":
                                    rbInjekOrt.Items[0].Selected = true;
                                    break;
                                case "Oberschenkel":
                                    rbInjekOrt.Items[1].Selected = true;
                                    break;
                                case "Bauch":
                                    rbInjekOrt.Items[2].Selected = true;
                                    break;
                                default:
                                    break;
                            }
                    }
                    else mvInjekOrt.SetActiveView(vwInjekOrtNo);

                    tbCurMass.Text = drConn["curmass"].ToString();
                    lbBMI.Text = drConn["bmi"].ToString();

                    string stMedan = drConn["medan"].ToString();
                    string stMedanT;
                    if (stMedan == "Ja")
                    {
                        rbMedan.Items[0].Selected = true;
                        mvMedan.SetActiveView(vwMedan);
                        tbMedan.Text = drConn["medancomment"].ToString();
                        stMedanT = drConn["medancomplete"].ToString();
                        if (stMedanT == "Ja") cbMedanComplete.Checked = true;
                    }
                    else
                    {
                        rbMedan.Items[1].Selected = true;
                        mvMedan.SetActiveView(vwMedanNo);
                    }

                    string stNw = drConn["nw"].ToString();
                    if (stNw == "Ja")
                    {
                        rbNw.Items[0].Selected = true;
                        mvNw.SetActiveView(vwNw);
                        tbNw.Text = drConn["nwtext"].ToString();
                        cbSpontan.Enabled = true;
                        if (drConn["nwspontan"].ToString() == "Ja") cbSpontan.Checked = true;
                    }
                    else
                    {
                        rbNw.Items[1].Selected = true;
                        mvNw.SetActiveView(vwNoNw);
                        cbSpontan.Enabled = false;
                    }

                    tbBhv.Text = drConn["bhv"].ToString();
                    stNwTextOld = drConn["nwtext"].ToString();
                    stNwOld = drConn["nw"].ToString();
                    stMedanCommentOld = drConn["medancomment"].ToString();

                    string strDatum = drConn["btkdate"].ToString();
                    string stprocday = strDatum.Substring(8, 2);
                    string stprocmonth = strDatum.Substring(5, 2);
                    string stprocyear = strDatum.Substring(0, 4);
                    multi = 0; ;
                    ddl_procday.SelectedIndex = -1;
                    ddl_procmonth.SelectedIndex = -1;
                    ddl_procyear.SelectedIndex = -1;
                    for (i = 0; i < ddl_procday.Items.Count; i++)
                    {
                        if (ddl_procday.Items[i].Value.ToString() == stprocday)
                        {
                            ddl_procday.Items[i].Selected = true;
                            multi++;
                        }
                    }

                    for (i = 0; i < ddl_procmonth.Items.Count; i++)
                    {
                        if (ddl_procmonth.Items[i].Value.ToString() == stprocmonth)
                        {
                            ddl_procmonth.Items[i].Selected = true;
                            multi++;
                        }
                    }

                    for (i = 0; i < ddl_procyear.Items.Count; i++)
                    {
                        if (ddl_procyear.Items[i].Value.ToString() == stprocyear)
                        {
                            ddl_procyear.Items[i].Selected = true;
                            multi++;
                        }
                    }


                    string stNeko = drConn["neko"].ToString();
                    string stnekoday = "";
                    string stnekomonth = "";
                    string stnekoyear = "";
                    if (stNeko.Length > 0)
                    {
                        stnekoday = stNeko.Substring(8, 2);
                        stnekomonth = stNeko.Substring(5, 2);
                        stnekoyear = stNeko.Substring(0, 4);
                    }

                    multi = 0;
                    ddl_nekoday.SelectedIndex = -1;
                    ddl_nekomonth.SelectedIndex = -1;
                    ddl_nekoyear.SelectedIndex = -1;
                    for (i = 0; i < ddl_nekoday.Items.Count; i++)
                    {
                        if (ddl_nekoday.Items[i].Value.ToString() == stnekoday)
                        {
                            ddl_nekoday.Items[i].Selected = true;
                            multi++;
                        }
                    }

                    for (i = 0; i < ddl_nekomonth.Items.Count; i++)
                    {
                        if (ddl_nekomonth.Items[i].Value.ToString() == stnekomonth)
                        {
                            ddl_nekomonth.Items[i].Selected = true;
                            multi++;
                        }
                    }

                    for (i = 0; i < ddl_nekoyear.Items.Count; i++)
                    {
                        if (ddl_nekoyear.Items[i].Value.ToString() == stnekoyear)
                        {
                            ddl_nekoyear.Items[i].Selected = true;
                            multi++;
                        }
                    }
                    drConn.Close();

            }
            catch { }

        }

        protected void cvThezu_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddl_art.SelectedItem.Text.ToString() == "Kontaktversuch")
            {
                args.IsValid = true;
                return;
            }

            /* if (ddl_thezu.SelectedItem.Text == "keine Angabe")
             {
                 if (ddl_status.SelectedItem.Text.ToString() != "Abbruch")
                 {
                     args.IsValid = false;
                     cvThezu.ErrorMessage = "Therapiezufriedenheit: Auswahl 'keine Angabe' ungültig!";
                 }
             }*/
            return;
        }

        protected void cvWechsel_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddl_art.SelectedItem.Text.ToString() == "Kontaktversuch")
            {
                args.IsValid = true;
                return;
            }

            /* if (ddl_wechsel.SelectedItem.Text == "keine Angabe")
             {
                 if (ddl_status.SelectedItem.Text.ToString() != "Abbruch")
                 {
                     args.IsValid = false;
                     cvWechsel.ErrorMessage = "Wechseltendenz: Auswahl 'keine Angabe' ungültig!!";
                 }
             }*/
            return;
        }

        protected void rbFLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbFLS.SelectedItem.Value.ToString() == "Ja")
            {
                mvFLS.SetActiveView(vwFLS);
            }
            else mvFLS.SetActiveView(vwFLSNo);
            upMed.Update();
        }

        protected void rbHaut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbHaut.SelectedItem.Value.ToString() == "Ja")
            {
                mvHaut.SetActiveView(vwHaut);
            }
            else mvHaut.SetActiveView(vwHautNo);
            upMed.Update();
        }

        protected void rbFlush_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbFlush.SelectedItem.Value.ToString() == "Ja")
            {
                tbFlush.Enabled = true;
            }
            else
            {
                tbFlush.Enabled = false;
                tbFlush.Text = "";
            }
            upMed.Update();
        }

        protected void rbGastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbGastro.SelectedItem.Value.ToString() == "Ja")
            {
                tbGastro.Enabled = true;
            }
            else
            {
                tbGastro.Enabled = false;
                tbGastro.Text = "";
            }
            upMed.Update();
        }

        protected void cbAndere_OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbAndere.Checked)
            {
                tbAndere.Enabled = true;
            }
            else
            {
                tbAndere.Enabled = false;
                tbAndere.Text = "";
            }
            upMed.Update();
        }

        protected void cbHautAndere_OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbHautAndere.Checked)
            {
                tbHautAndere.Enabled = true;
            }
            else
            {
                tbHautAndere.Enabled = false;
                tbHautAndere.Text = "";
            }
            upMed.Update();
        }

        protected void injek_onselectindexchanged(object sender, EventArgs e)
        {
            string stInjek;

            stInjek = rbBioFeSp.SelectedItem.Value.ToString();
            if (stInjek != "Pen")
            {
                mvNL.SetActiveView(vwNL);
                if (rbNaLa.SelectedIndex > -1)
                {
                    string stNala = rbNaLa.SelectedItem.Text.ToString();
                    if ((stNala == "Keine Angabe") || (stNala == "Standard")) mvAA.SetActiveView(vwNoAA);
                    else mvAA.SetActiveView(vwAA);
                }
            }
            else
            {
                mvNL.SetActiveView(vwNoNL);
                mvAA.SetActiveView(vwNoAA);

            }

            if ((stInjek == "Fertigspritze") && (Session["med"].ToString() == "Plegridy"))
            {
                mvNL.SetActiveView(vwNoNL);
                mvAA.SetActiveView(vwNoAA);
            }
            upInjek.Update();

        }

        protected void nala_onselectindexchanged(object sender, EventArgs e)
        {
            string stNala;

            stNala = rbNaLa.SelectedItem.Text.ToString().Substring(0, 4);
            if ((stNala == "Kein") || (stNala == "Stan")) mvAA.SetActiveView(vwNoAA);
            else mvAA.SetActiveView(vwAA);

            upInjek.Update();

        }
        protected void cvAADatum_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string stDate, stYear, stMonth, stDay;

            stYear = ddl_AAyear.SelectedItem.Text.ToString();
            stMonth = ddl_AAmonth.SelectedItem.Text.ToString();
            stDay = ddl_AAday.SelectedItem.Text.ToString();

            stDate = stYear + "-" + stMonth + "-" + stDay;

            string ErrorMessage = Utils.CheckFullDate(stDate, "Back");
            if (ErrorMessage != "")
            {
                cvAADatum.ErrorMessage = "Datum Arztanweisung: " + ErrorMessage;
                cvAADatumPlaus.ErrorMessage = "Datum Arztanweisung: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;
        }

        protected void rbtit_onselectindexchanged(object sender, EventArgs e)
        {
            string stTitra;

            if (rbTitra.SelectedItem.Value.ToString() == "Ja")
            {
                if (Session["med"].ToString() == "Avonex")
                    mvTitra.SetActiveView(vwTitraAvonex);
                else
                    mvTitra.SetActiveView(vwTitraPlegridy);
            }
            else mvTitra.SetActiveView(vwTitraNo);
            upTitDos.Update();
        }

        protected void rbTitraPlegridy_onselectindexchanged(object sender, EventArgs e)
        {

        }

        protected void rbDos_onselectindexchanged(object sender, EventArgs e)
        {

        }

        protected void tbCurMass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string stCurMass = tbCurMass.Text.ToString();
                Double GQ = Convert.ToDouble(stGroesseRef);
                GQ = GQ * GQ;
                GQ = GQ / 10000;
                lbBMI.Text = (Convert.ToInt32(Convert.ToDouble(stCurMass) / GQ)).ToString();
                upBMI.Update();
            }
            catch
            {
                lbBMI.Text = "BMI kann nicht berechnet werden!";
                upBMI.Update();
            }
            finally
            {
                lbsaved.Text = "";
            }
            return;
        }
    }
}


