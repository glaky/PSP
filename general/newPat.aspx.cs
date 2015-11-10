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
using System.Drawing;


namespace PSP.general
{
    public partial class newPat : System.Web.UI.Page
    {
        static string[] oldEntry;
        static string[] newEntry;
        static string[] fieldName;
        static string stQuelleRef;
        static string stOwnerId, stOwnerRef, stPrevOwner;
        static string stConsDateRef = "", stConsGetDateRef = "";
        static DateTime dtConsGetDateRef, dtConsDateRef;

        protected void Page_Load(object sender, EventArgs e)
        {


            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());



            scStamm.RegisterAsyncPostBackControl(rbMedikament);
            scStamm.RegisterAsyncPostBackControl(rbVorthe);

            string stCurYear = DateTime.Now.ToString("yyyy");
            string stNextYear = ((Convert.ToInt32(stCurYear)) + 1).ToString();
            string stLastYear = ((Convert.ToInt32(stCurYear)) - 1).ToString();

            if (!IsPostBack)
            {
                /*Session["prevpage"] = Request.UrlReferrer.ToString();*/

                mvMedInd.SetActiveView(vwMedIndNo);
                mvMedInd1.SetActiveView(vwMedInd1);

                int i;
                string stprocday;
                string stprocmonth;
                string stprocyear;
                string stgebday;
                string stgebmonth;
                string stgebyear;
                string stFormDay;
                string stFormMonth;
                string stFormYear;

                if (!IsPostBack)
                {
                    switch (Session["role"].ToString())
                    {
                        case "sec":
                            mvMenu.SetActiveView(vwSec);
                            break;
                        case "nrs":
                            mvMenu.SetActiveView(vwNrs);
                            break;
                        default:
                            break;
                    }

                    switch (Request["typ"].ToString())
                    {
                        case "new":
                            mvTitel.SetActiveView(vwNewTitel);
                            break;
                        case "edit":
                            mvTitel.SetActiveView(vwEditTitel);
                            break;
                        default:
                            break;
                    }

                    stprocday = "";
                    stprocmonth = "";
                    stprocyear = "";

                    ArrayList artKontakt = new ArrayList();
                    artKontakt.Add("keine Angabe");
                    artKontakt.Add("Telefon");
                    artKontakt.Add("Fax");
                    artKontakt.Add("E-Mail");
                    artKontakt.Add("Antragsformular");
                    artKontakt.Add("Sonstige");
                    ddlArtKontakt.DataSource = artKontakt;
                    ddlArtKontakt.DataBind();

                    ArrayList anrufIntervall = new ArrayList();
                    anrufIntervall.Add("keine Angabe");
                    anrufIntervall.Add("Nie");
                    anrufIntervall.Add("Wöchentlich");
                    anrufIntervall.Add("Alle 2 Wochen");
                    anrufIntervall.Add("Monatlich");
                    anrufIntervall.Add("Alle 2 Monate");
                    anrufIntervall.Add("Alle 3 Monate");
                    anrufIntervall.Add("Alle 4 Monate");
                    ddlAnrufIntervall.DataSource = anrufIntervall;
                    ddlAnrufIntervall.DataBind();

                    ArrayList title = new ArrayList();
                    title.Add("");
                    title.Add("Dr.");
                    title.Add("Dr.Dr.");
                    title.Add("Mag.");
                    title.Add("Dipl.Ing.");
                    title.Add("Mag.(FH)");
                    title.Add("Dipl.Ing.(FH)");
                    title.Add("Ing.");
                    ddlTitle.DataSource = title;
                    ddlTitle.DataBind();

                    ArrayList day = new ArrayList();
                    day.Add("");
                    day.Add("01");
                    day.Add("02");
                    day.Add("03");
                    day.Add("04");
                    day.Add("05");
                    day.Add("06");
                    day.Add("07");
                    day.Add("08");
                    day.Add("09");
                    day.Add("10");
                    day.Add("11");
                    day.Add("12");
                    day.Add("13");
                    day.Add("14");
                    day.Add("15");
                    day.Add("16");
                    day.Add("17");
                    day.Add("18");
                    day.Add("19");
                    day.Add("20");
                    day.Add("21");
                    day.Add("22");
                    day.Add("23");
                    day.Add("24");
                    day.Add("25");
                    day.Add("26");
                    day.Add("27");
                    day.Add("28");
                    day.Add("29");
                    day.Add("30");
                    day.Add("31");
                    ddl_consday.DataSource = day;
                    ddl_consday.DataBind();
                    ddl_gebday.DataSource = day;
                    ddl_gebday.DataBind();
                    ddl_consgetday.DataSource = day;
                    ddl_consgetday.DataBind();

                    ArrayList month = new ArrayList();
                    month.Add("");
                    month.Add("01");
                    month.Add("02");
                    month.Add("03");
                    month.Add("04");
                    month.Add("05");
                    month.Add("06");
                    month.Add("07");
                    month.Add("08");
                    month.Add("09");
                    month.Add("10");
                    month.Add("11");
                    month.Add("12");
                    ddl_consmonth.DataSource = month;
                    ddl_consmonth.DataBind();
                    ddl_gebmonth.DataSource = month;
                    ddl_gebmonth.DataBind();
                    ddl_consgetmonth.DataSource = month;
                    ddl_consgetmonth.DataBind();

                    ArrayList dNum = new ArrayList();
                    dNum.Add("??");
                    for (i = 40; i < 250; i++)
                    {
                        dNum.Add(i);
                    }
                    ddlGewicht.DataSource = dNum;
                    ddlGewicht.DataBind();

                    dNum.Clear();

                    dNum.Add("??");
                    for (i = 100; i < 220; i++)
                    {
                        dNum.Add(i);
                    }
                    ddlGroesse.DataSource = dNum;
                    ddlGroesse.DataBind();


                    ArrayList dyear = new ArrayList();
                    dyear.Add("");
                    dyear.Add(stCurYear);
                    for (i = 1; i < 7; i++)
                    {
                        stLastYear = ((Convert.ToInt32(stCurYear)) - i).ToString();
                        dyear.Add(stLastYear);
                    }

                    ddl_consyear.DataSource = dyear;
                    ddl_consyear.DataBind();
                    ddl_consgetyear.DataSource = dyear;
                    ddl_consgetyear.DataBind();


                    dyear.Clear();

                    dyear.Add("");
                    dyear.Add(stCurYear);
                    for (i = 1; i < 99; i++)
                    {
                        stLastYear = ((Convert.ToInt32(stCurYear)) - i).ToString();
                        dyear.Add(stLastYear);
                    }

                    ddl_gebyear.DataSource = dyear;
                    ddl_gebyear.DataBind();

                    ddl_diagnose.DataSource = dyear;
                    ddl_diagnose.DataBind();

                    dyear.Clear();

                    dyear.Add("????");
                    dyear.Add(stCurYear);
                    for (i = 1; i < 15; i++)
                    {
                        stLastYear = ((Convert.ToInt32(stCurYear)) - i).ToString();
                        dyear.Add(stLastYear);
                    }
                    ddl_tsyear.DataSource = dyear;
                    ddl_tsyear.DataBind();

                    dyear.Clear();

                    ArrayList avom = new ArrayList();
                    avom.Add("??");
                    avom.Add("01");
                    avom.Add("02");
                    avom.Add("03");
                    avom.Add("04");
                    avom.Add("05");
                    avom.Add("06");
                    avom.Add("07");
                    avom.Add("08");
                    avom.Add("09");
                    avom.Add("10");
                    avom.Add("11");
                    avom.Add("12");
                    ddl_tsmonth.DataSource = avom;
                    ddl_tsmonth.DataBind();

                    ArrayList injektionstag = new ArrayList();
                    injektionstag.Add("keine Angabe");
                    injektionstag.Add("Montag");
                    injektionstag.Add("Dienstag");
                    injektionstag.Add("Mittwoch");
                    injektionstag.Add("Donnerstag");
                    injektionstag.Add("Freitag");
                    injektionstag.Add("Samstag");
                    injektionstag.Add("Sonntag");
                    ddlInjektionstag.DataSource = injektionstag;
                    ddlInjektionstag.DataBind();

                    ArrayList errei = new ArrayList();
                    errei.Add("keine Angabe");
                    errei.Add("8h-12h");
                    errei.Add("12h-16h");
                    errei.Add("16h-20h");
                    errei.Add("Uhrzeit egal");
                    ddl_errei.DataSource = errei;
                    ddl_errei.DataBind();

                    if (Request["typ"] == "new")
                    {
                        stgebday = "";
                        stgebmonth = "";
                        stgebyear = "";
                        if (stprocday == "")
                        {
                            for (i = 0; i < ddl_consday.Items.Count; i++)
                            {
                                if (ddl_consday.Items[i].Value.ToString() == DateTime.Now.ToString("dd").ToString())
                                {
                                    ddl_consday.Items[i].Selected = true;
                                    ddl_consgetday.Items[i].Selected = true;
                                }
                            }
                        }


                        if (stprocmonth == "")
                        {
                            for (i = 0; i < ddl_consmonth.Items.Count; i++)
                            {
                                if (ddl_consmonth.Items[i].Value.ToString() == DateTime.Now.ToString("MM").ToString())
                                {
                                    ddl_consmonth.Items[i].Selected = true;
                                    ddl_consgetmonth.Items[i].Selected = true;
                                }
                            }
                        }


                        if (stprocyear == "")
                        {
                            for (i = 0; i < ddl_consyear.Items.Count; i++)
                            {
                                if (ddl_consyear.Items[i].Value.ToString() == DateTime.Now.ToString("yyyy").ToString())
                                {
                                    ddl_consyear.Items[i].Selected = true;
                                    ddl_consgetyear.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                    if (Request["typ"] == "edit")
                    {

                        Session["patid"] = Convert.ToInt32(Request["patid"].ToString());

                        fieldName = manageAuditTrail.get_fields("dbo.get_patdat", Convert.ToInt32(Session["patid"]), "@patid");
                        oldEntry = manageAuditTrail.get_entry("dbo.get_patdat", Convert.ToInt32(Session["patid"]), "@patid");

                        SqlConnection cnConn = DBTools.getConnection();
                        cnConn.Open();
                        SqlCommand cmdConn = new SqlCommand("get_patdat", cnConn);
                        cmdConn.CommandType = CommandType.StoredProcedure;
                        cmdConn.Parameters.Add(new SqlParameter("@patid", Request["patid"]));
                        SqlDataReader drConn = cmdConn.ExecuteReader();
                        drConn.Read();

                        tbPatname.Text = drConn["name"].ToString();
                        tbPatvorname.Text = drConn["vorname"].ToString();
                        for (i = 0; i < ddlTitle.Items.Count; i++)
                        {
                            if (ddlTitle.Items[i].Value.ToString() == drConn["titel"].ToString())
                            {
                                ddlTitle.Items[i].Selected = true;
                            }
                        }

                        string stGeschlecht = drConn["geschlecht"].ToString();
                        switch (stGeschlecht)
                        {
                            case "M":
                                {
                                    rbGeschlecht.Items[0].Selected = true;
                                    break;
                                }
                            case "W":
                                {
                                    rbGeschlecht.Items[1].Selected = true;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        string stGebDate = drConn["gebdat"].ToString();
                        if (stGebDate == "unbekannt")
                        {
                            stgebday = "";
                            stgebmonth = "";
                            stgebyear = "";
                            cb_gebdate.Checked = true; ;
                        }
                        else
                        {
                            stgebday = (stGebDate.Length == 10) ? stGebDate.Substring(8, 2) : "";
                            stgebmonth = (stGebDate.Length >= 7) ? stGebDate.Substring(5, 2) : "";
                            stgebyear = (stGebDate.Length >= 4) ? stgebyear = stGebDate.Substring(0, 4) : "";
                        }
                        /*for (i = 0; i < ddl_gebday.Items.Count; i++)
                        {
                            if (ddl_gebday.Items[i].Value.ToString() == stgebday)
                            {
                                ddl_gebday.Items[i].Selected = true;
                            }
                        }*/

                        for (i = 0; i < ddl_gebmonth.Items.Count; i++)
                        {
                            if (ddl_gebmonth.Items[i].Value.ToString() == stgebmonth)
                            {
                                ddl_gebmonth.Items[i].Selected = true;
                            }
                        }

                        for (i = 0; i < ddl_gebyear.Items.Count; i++)
                        {
                            if (ddl_gebyear.Items[i].Value.ToString() == stgebyear)
                            {
                                ddl_gebyear.Items[i].Selected = true;
                            }
                        }


                        tbAdresse.Text = drConn["adresse"].ToString();
                        tbPlz.Text = drConn["plz"].ToString();
                        tbOrt.Text = drConn["ort"].ToString();
                        tbTel.Text = drConn["tel"].ToString(); ;
                        tbEmail.Text = drConn["email"].ToString();

                        string stArtKontakt = drConn["artkontakt"].ToString();
                        for (i = 0; i < ddlArtKontakt.Items.Count; i++)
                        {
                            if (ddlArtKontakt.Items[i].Value.ToString() == stArtKontakt)
                            {
                                ddlArtKontakt.Items[i].Selected = true;
                            }
                        }

                        string stConsent = drConn["consent"].ToString();
                        if (stConsent == "Ja") rbConsent.SelectedIndex = 0;
                        else rbConsent.SelectedIndex = 1;

                        string stDay = "";
                        string stMon = "";
                        string stYear = "";

                        string stConsDate = drConn["consdate"].ToString();
                        if (stConsDate != DBNull.Value.ToString())
                        {
                            stDay = stConsDate.Substring(8, 2);
                            stMon = stConsDate.Substring(5, 2);
                            stYear = stConsDate.Substring(0, 4);
                        }
                        for (i = 0; i < ddl_consday.Items.Count; i++)
                        {
                            if (ddl_consday.Items[i].Value.ToString() == stDay)
                            {
                                ddl_consday.Items[i].Selected = true;
                            }
                        }
                        for (i = 0; i < ddl_consmonth.Items.Count; i++)
                        {
                            if (ddl_consmonth.Items[i].Value.ToString() == stMon)
                            {
                                ddl_consmonth.Items[i].Selected = true;
                            }
                        }
                        for (i = 0; i < ddl_consyear.Items.Count; i++)
                        {
                            if (ddl_consyear.Items[i].Value.ToString() == stYear)
                            {
                                ddl_consyear.Items[i].Selected = true;
                            }
                        }


                        string stConsGet = drConn["consget"].ToString();
                        if (stConsGet != DBNull.Value.ToString())
                        {
                            stDay = stConsGet.Substring(8, 2);
                            stMon = stConsGet.Substring(5, 2);
                            stYear = stConsGet.Substring(0, 4);
                        }
                        for (i = 0; i < ddl_consgetday.Items.Count; i++)
                        {
                            if (ddl_consgetday.Items[i].Value.ToString() == stDay)
                            {
                                ddl_consgetday.Items[i].Selected = true;
                            }
                        }
                        for (i = 0; i < ddl_consgetmonth.Items.Count; i++)
                        {
                            if (ddl_consgetmonth.Items[i].Value.ToString() == stMon)
                            {
                                ddl_consgetmonth.Items[i].Selected = true;
                            }
                        }
                        for (i = 0; i < ddl_consgetyear.Items.Count; i++)
                        {
                            if (ddl_consgetyear.Items[i].Value.ToString() == stYear)
                            {
                                ddl_consgetyear.Items[i].Selected = true;
                            }
                        }

                        string stMedikament = drConn["medikament"].ToString();
                        for (i = 0; i < rbMedikament.Items.Count; i++)
                            if (rbMedikament.Items[i].Value.ToString() == stMedikament) rbMedikament.Items[i].Selected = true;

                        mvMed.SetActiveView(vwMed);
                        mvMedInd.SetActiveView(vwMedInd);
                        mvMedInd1.SetActiveView(vwMedInd1);

                        rbMedikament_SelectedIndexChanged(rbMedikament, new EventArgs());

                        for (i = 0; i < ddl_diagnose.Items.Count; i++)
                        {
                            if (ddl_diagnose.Items[i].Value.ToString() == drConn["diagnose"].ToString())
                            {
                                ddl_diagnose.Items[i].Selected = true;
                            }
                        }

                        tbZentrum.Text = drConn["zentrum"].ToString();

                        string stIntervall = drConn["intervall"].ToString();
                        ddlAnrufIntervall.SelectedIndex = -1;
                        for (i = 0; i < ddlAnrufIntervall.Items.Count; i++)
                        {
                            if (ddlAnrufIntervall.Items[i].Value.ToString() == stIntervall)
                            {
                                ddlAnrufIntervall.Items[i].Selected = true;
                            }
                        }

                        string stGroesse = drConn["groesse"].ToString();
                        for (i = 0; i < ddlGroesse.Items.Count; i++)
                        {
                            if (ddlGroesse.Items[i].Value.ToString() == stGroesse)
                            {
                                ddlGroesse.Items[i].Selected = true;
                            }
                        }

                        string stGewicht = drConn["gewicht"].ToString();
                        for (i = 0; i < ddlGewicht.Items.Count; i++)
                        {
                            if (ddlGewicht.Items[i].Value.ToString() == stGewicht)
                            {
                                ddlGewicht.Items[i].Selected = true;
                            }
                        }

                        string stErrei = (drConn["erreichbarkeit"] == DBNull.Value) ? "keine Angabe" : drConn["erreichbarkeit"].ToString();

                        for (i = 0; i < ddl_errei.Items.Count; i++)
                        {
                            if (ddl_errei.Items[i].Value.ToString() == stErrei)
                            {
                                ddl_errei.Items[i].Selected = true;
                            }
                        }

                        string stAnonym = drConn["anonym"].ToString();
                        for (i = 0; i < rbAnonym.Items.Count; i++)
                            if (rbAnonym.Items[i].Value.ToString() == stAnonym) rbAnonym.Items[i].Selected = true;

                        string stVorthe = drConn["vorthe"].ToString();

                        if (stVorthe == "Ja")
                        {
                            rbVorthe.SelectedIndex = 0;
                            tbVorthe.Text = drConn["vorthetext"].ToString();
                            tbVorthe.Enabled = true;
                        }
                        else
                        {
                            rbVorthe.SelectedIndex = 1;
                            tbVorthe.Text = "";
                            tbVorthe.Enabled = false;

                        }


                        string stTherapieM = drConn["thestart"].ToString().Substring(0, 2);
                        string stTherapieJ = drConn["thestart"].ToString().Substring(3, 4);

                        for (i = 0; i < ddl_tsmonth.Items.Count; i++)
                        {
                            if (ddl_tsmonth.Items[i].Value.ToString() == stTherapieM) ddl_tsmonth.Items[i].Selected = true;
                        }

                        for (i = 0; i < ddl_tsyear.Items.Count; i++)
                        {
                            if (ddl_tsyear.Items[i].Value.ToString() == stTherapieJ) ddl_tsyear.Items[i].Selected = true;
                        }

                        string stInjektionstag = drConn["injektionstag"].ToString();

                        for (i = 0; i < ddlInjektionstag.Items.Count; i++)
                        {
                            if (ddlInjektionstag.Items[i].Value.ToString() == stInjektionstag) ddlInjektionstag.Items[i].Selected = true;
                        }

                        string stSchule = (drConn["einschulung"] == DBNull.Value) ? "keine Angabe" : drConn["einschulung"].ToString();
                        for (i = 0; i < rbSchule.Items.Count; i++)
                            if (rbSchule.Items[i].Value.ToString() == stSchule) rbSchule.Items[i].Selected = true;

                        string stZustaendigkeit = (drConn["zustaendigkeit"] == DBNull.Value) ? "keine Angabe" : drConn["zustaendigkeit"].ToString();
                        for (i = 0; i < rbZustaendigkeit.Items.Count; i++)
                            if (rbZustaendigkeit.Items[i].Value.ToString() == stZustaendigkeit) rbZustaendigkeit.Items[i].Selected = true;

                        drConn.Close();
                        cnConn.Close();

                    }
                }
            }

        }

        protected void cvValrbZustaendigkeit_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        protected void cvValddlConsDate11_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string stDate, stYear, stMonth, stDay;

            stYear = ddl_consyear.SelectedItem.Text.ToString();
            stMonth = ddl_consmonth.SelectedItem.Text.ToString();
            stDay = ddl_consday.SelectedItem.Text.ToString();
            stDate = stYear + "-" + stMonth + "-" + stDay;
            stConsDateRef = stDate;

            DateTime.TryParseExact(stConsDateRef, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtConsDateRef);

            string ErrorMessage = Utils.CheckFullDate(stDate, "Back");
            if ((ErrorMessage != "") && (rbConsent.SelectedIndex == 0))
            {
                cvValddlConsDate.ErrorMessage = "Datum schriftliche Einwilligung: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;
        }

        protected void cvValddlConsDate_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string stDate, stYear, stMonth, stDay;

            stYear = ddl_consyear.SelectedItem.Text.ToString();
            stMonth = ddl_consmonth.SelectedItem.Text.ToString();
            stDay = ddl_consday.SelectedItem.Text.ToString();
            stDate = stYear + "-" + stMonth + "-" + stDay;
            stConsDateRef = stDate;

            DateTime.TryParseExact(stConsDateRef, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtConsDateRef);

            string ErrorMessage = Utils.CheckFullDate(stDate, "Back");
            if ((ErrorMessage != "") && (rbConsent.SelectedIndex == 0))
            {
                cvValddlConsDate.ErrorMessage = "Datum schriftliche Einwilligung: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;
        }

        protected void cvValddlConsGetDate_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string stDate, stYear, stMonth, stDay;

            DateTime.TryParseExact(stConsDateRef, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtConsGetDateRef);

            stYear = ddl_consgetyear.SelectedItem.Text.ToString();
            stMonth = ddl_consgetmonth.SelectedItem.Text.ToString();
            stDay = ddl_consgetday.SelectedItem.Text.ToString();
            stDate = stYear + "-" + stMonth + "-" + stDay;
            stConsGetDateRef = stDate;

            string ErrorMessage = Utils.CheckFullDate(stDate, "Back");
            if ((ErrorMessage != "") && (rbConsent.SelectedIndex == 0))
            {
                cvValddlConsDate.ErrorMessage = "Einlagen der schriftlichen Einwilligung: " + ErrorMessage;
                args.IsValid = false;
            }
            else args.IsValid = true;



            if (dtConsDateRef > dtConsGetDateRef)
            {
                args.IsValid = false;
                cvValddlConsGetDate.ErrorMessage = "Einlagen der schriftlichen Einwilligung kann nicht vor dem Datum des der schriftlichen Einwilligung liegen!";
            }

        }


        public void bt_save_click(object sender, System.EventArgs e)
        {

            string stGenDate, stConsDate, stConsGet;

            SqlConnection cnConn;
            SqlCommand cmdConn;
            SqlDataReader drConn;


            if (Page.IsValid)
            {

                cnConn = DBTools.getConnection();
                cnConn.Open();


                string stName = tbPatname.Text.ToString();
                string stVorname = tbPatvorname.Text.ToString();
                string stGeschlecht = rbGeschlecht.SelectedItem.Value.ToString();
                string stAdresse = tbAdresse.Text.ToString();
                string stOrt = tbOrt.Text.ToString();
                string stPlz = tbPlz.Text.ToString();
                string stTel = tbTel.Text.ToString();
                string stEmail = tbEmail.Text.ToString();
                string stIdentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                string stIdent = stName + "_" + stVorname + "_" + ddl_gebyear.SelectedItem.Text.ToString() + ddl_gebmonth.SelectedItem.Text.ToString();
                string stGendate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string stTitel = ddlTitle.SelectedItem.Text.ToString();
                string stConsent = rbConsent.SelectedItem.Value.ToString();
                string stMedikament = rbMedikament.SelectedItem.Value.ToString();
                string stGroesse = ddlGroesse.SelectedItem.Text.ToString();
                string stGewicht = ddlGewicht.SelectedItem.Text.ToString();
                string stArtKontakt = ddlArtKontakt.SelectedItem.Text.ToString();

                string stGebdat = ddl_gebyear.SelectedItem.Text.ToString() + "-" + ddl_gebmonth.SelectedItem.Text.ToString();
                if (stGebdat.Length == 6) stGebdat = stGebdat.Substring(0, 4);
                if (stGebdat.Length == 8) stGebdat = stGebdat.Substring(0, 7);
                if (cb_gebdate.Checked) stGebdat = "unbekannt";
                if (stGebdat == "--") stGebdat = "????.??.??";
                stConsDate = ddl_consyear.SelectedItem.Text.ToString() + "-" + ddl_consmonth.SelectedItem.Text.ToString() + "-" + ddl_consday.SelectedItem.Text.ToString();
                stConsGet = ddl_consgetyear.SelectedItem.Text.ToString() + "-" + ddl_consgetmonth.SelectedItem.Text.ToString() + "-" + ddl_consgetday.SelectedItem.Text.ToString();
                if (stConsGet == "--") stConsGet = "????.??.??";
                if (stConsDate == "--") stConsDate = "????.??.??";

                string stDiagnose = ddl_diagnose.SelectedItem.Text.ToString();
                string stZentrum = tbZentrum.Text.ToString();
                string stIntervall = ddlAnrufIntervall.SelectedItem.Text.ToString();
                string stAnonym = rbAnonym.SelectedItem.Value.ToString();
                string stErrei = ddl_errei.SelectedItem.Text.ToString();
                string stVorthe = rbVorthe.SelectedItem.Value.ToString();
                string stTheStart = ddl_tsmonth.SelectedItem.Text.ToString() + "." + ddl_tsyear.SelectedItem.Text.ToString();
                string stVortheText = tbVorthe.Text;

                string stEinschulung;
                if (rbSchule.SelectedIndex > -1) stEinschulung = rbSchule.SelectedItem.Value.ToString();
                else stEinschulung = "keine Angabe";

                string stZustaendigkeit = "";
                if (rbZustaendigkeit.SelectedIndex > -1) stZustaendigkeit = rbZustaendigkeit.SelectedItem.Value.ToString();
                else stZustaendigkeit = "keine Angabe";

                string stInjektionstag = ddlInjektionstag.SelectedItem.Text.ToString();
                int i = 0;

                if (Request["typ"].ToString() == "new")
                {

                    int btkid = 0;
                    int sourceid = Convert.ToInt32(Session["id"]);

                    stGenDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    cnConn = DBTools.getConnection();
                    cnConn.Open();
                    cmdConn = new SqlCommand("dbo.insert_patdat", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@ident", stIdent));
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", btkid));
                    cmdConn.Parameters.Add(new SqlParameter("@sourceid", sourceid));
                    cmdConn.Parameters.Add(new SqlParameter("@name", stName));
                    cmdConn.Parameters.Add(new SqlParameter("@vorname", stVorname));
                    cmdConn.Parameters.Add(new SqlParameter("@titel", stTitel));
                    cmdConn.Parameters.Add(new SqlParameter("@geschlecht", stGeschlecht));
                    cmdConn.Parameters.Add(new SqlParameter("@gebdat", stGebdat));
                    cmdConn.Parameters.Add(new SqlParameter("@adresse", stAdresse));
                    cmdConn.Parameters.Add(new SqlParameter("@plz", stPlz));
                    cmdConn.Parameters.Add(new SqlParameter("@ort", stOrt));
                    cmdConn.Parameters.Add(new SqlParameter("@tel", stTel));
                    cmdConn.Parameters.Add(new SqlParameter("@email", stEmail));
                    cmdConn.Parameters.Add(new SqlParameter("@gendate", stGendate));
                    cmdConn.Parameters.Add(new SqlParameter("@artkontakt", stArtKontakt));
                    cmdConn.Parameters.Add(new SqlParameter("@consent", stConsent));
                    cmdConn.Parameters.Add(new SqlParameter("@consdate", stConsDate));
                    cmdConn.Parameters.Add(new SqlParameter("@consget", stConsGet));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", stMedikament));
                    cmdConn.Parameters.Add(new SqlParameter("@diagnose", stDiagnose));
                    cmdConn.Parameters.Add(new SqlParameter("@zentrum", stZentrum));
                    cmdConn.Parameters.Add(new SqlParameter("@intervall", stIntervall));
                    cmdConn.Parameters.Add(new SqlParameter("@errei", stErrei));
                    cmdConn.Parameters.Add(new SqlParameter("@anonym", stAnonym));
                    cmdConn.Parameters.Add(new SqlParameter("@vorthe", stVorthe));
                    cmdConn.Parameters.Add(new SqlParameter("@vorthetext", stVortheText));
                    cmdConn.Parameters.Add(new SqlParameter("@thestart", stTheStart));
                    cmdConn.Parameters.Add(new SqlParameter("@status", "Noch kein Betreuungskontakt"));
                    cmdConn.Parameters.Add(new SqlParameter("@leko", stGendate));
                    cmdConn.Parameters.Add(new SqlParameter("@neko", ""));
                    cmdConn.Parameters.Add(new SqlParameter("@einschulung", stEinschulung));
                    cmdConn.Parameters.Add(new SqlParameter("@zustaendigkeit", stZustaendigkeit));
                    cmdConn.Parameters.Add(new SqlParameter("@injektionstag", stInjektionstag));
                    cmdConn.Parameters.Add(new SqlParameter("@groesse", stGroesse));
                    cmdConn.Parameters.Add(new SqlParameter("@gewicht", stGewicht));
                    cmdConn.ExecuteNonQuery();

                    cmdConn = new SqlCommand("dbo.get_patid", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    drConn = cmdConn.ExecuteReader();
                    drConn.Read();
                    Session["patid"] = drConn["patid"].ToString();
                    drConn.Close();
                    cnConn.Close();
                }
                else
                {
                    cmdConn = new SqlCommand("dbo.update_patdat", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                    cmdConn.Parameters.Add(new SqlParameter("@name", stName));
                    cmdConn.Parameters.Add(new SqlParameter("@vorname", stVorname));
                    cmdConn.Parameters.Add(new SqlParameter("@titel", stTitel));
                    cmdConn.Parameters.Add(new SqlParameter("@geschlecht", stGeschlecht));
                    cmdConn.Parameters.Add(new SqlParameter("@gebdat", stGebdat));
                    cmdConn.Parameters.Add(new SqlParameter("@adresse", stAdresse));
                    cmdConn.Parameters.Add(new SqlParameter("@plz", stPlz));
                    cmdConn.Parameters.Add(new SqlParameter("@ort", stOrt));
                    cmdConn.Parameters.Add(new SqlParameter("@tel", stTel));
                    cmdConn.Parameters.Add(new SqlParameter("@email", stEmail));
                    cmdConn.Parameters.Add(new SqlParameter("@artkontakt", stArtKontakt));
                    cmdConn.Parameters.Add(new SqlParameter("@consent", stConsent));
                    cmdConn.Parameters.Add(new SqlParameter("@consdate", stConsDate));
                    cmdConn.Parameters.Add(new SqlParameter("@consget", stConsGet));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", stMedikament));
                    cmdConn.Parameters.Add(new SqlParameter("@diagnose", stDiagnose));
                    cmdConn.Parameters.Add(new SqlParameter("@zentrum", stZentrum));
                    cmdConn.Parameters.Add(new SqlParameter("@intervall", stIntervall));
                    cmdConn.Parameters.Add(new SqlParameter("@errei", stErrei));
                    cmdConn.Parameters.Add(new SqlParameter("@anonym", stAnonym));
                    cmdConn.Parameters.Add(new SqlParameter("@vorthe", stVorthe));
                    cmdConn.Parameters.Add(new SqlParameter("@vorthetext", stVortheText));
                    cmdConn.Parameters.Add(new SqlParameter("@thestart", stTheStart));
                    cmdConn.Parameters.Add(new SqlParameter("@status", "Noch kein Betreuungskontakt"));
                    cmdConn.Parameters.Add(new SqlParameter("@leko", stGendate));
                    cmdConn.Parameters.Add(new SqlParameter("@neko", ""));
                    cmdConn.Parameters.Add(new SqlParameter("@einschulung", stEinschulung));
                    cmdConn.Parameters.Add(new SqlParameter("@zustaendigkeit", stZustaendigkeit));
                    cmdConn.Parameters.Add(new SqlParameter("@injektionstag", stInjektionstag));
                    cmdConn.Parameters.Add(new SqlParameter("@groesse", stGroesse));
                    cmdConn.Parameters.Add(new SqlParameter("@gewicht", stGewicht));
                    cmdConn.ExecuteNonQuery();

                    cnConn.Close();

                    newEntry = manageAuditTrail.get_entry("dbo.get_patdat", Convert.ToInt32(Session["patid"]), "@patid");
                    manageAuditTrail.audit_changes(oldEntry, newEntry, fieldName, Convert.ToInt32(Session["userid"]), 7, Convert.ToInt32(Session["patid"]));
                }
                string reurl = "~/general/detailPat.aspx?patid=" + Session["patid"].ToString();
                Response.Redirect(reurl);
            }
        }



        public void bt_cancel_click(object sender, System.EventArgs e)
        {
            string reurl;
            if (Request["typ"].ToString() == "edit") reurl = "~/general/detailPat.aspx?patid=" + Session["patid"].ToString();
            else reurl = "~/general/overviewPat.aspx";
            Response.Redirect(reurl);
        }

        protected void rbVorthe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stVorthe;

            stVorthe = rbVorthe.SelectedItem.Text.ToString().Substring(0, 1);

            if (stVorthe == "J") tbVorthe.Enabled = true;
            else
            {
                tbVorthe.Enabled = false;
                tbVorthe.Text = "";
            }
        }

        protected void rbMedikament_SelectedIndexChanged(object sender, EventArgs e)
        {

            mvMed.SetActiveView(vwMed);

            switch (rbMedikament.SelectedItem.Value.ToString())
            {
                case "Plegridy":
                    {
                        medView.Style.Add("background-color", "#99ccff");
                        medView1.Style.Add("background-color", "#99ccff");
                        tblMed.Style.Add("background-color", "#99ccff");
                        lbMedIndicator.Text = "Plegridy";
                        lbMedIndicator1.Text = "Plegridy";
                        lbTherapie.Text = "Plegridytherapie seit (MM.JJJJ)";
                        ddlArtKontakt.Enabled = true;
                        ddlInjektionstag.Enabled = true;
                        rbSchule.Enabled = true;
                        rbZustaendigkeit.Enabled = true;
                        rfvZustaendigkeit.Enabled = true;
                        ddlAnrufIntervall.Items.FindByValue("Wöchentlich").Attributes.Add("disabled", "disabled");
                        ddlAnrufIntervall.SelectedValue = "keine Angabe";
                        break;
                    }
                case "Tecfidera":
                    {
                        medView.Style.Add("background-color", "#ff66ff");
                        medView1.Style.Add("background-color", "#ff66ff");
                        tblMed.Style.Add("background-color", "#ff66ff");
                        lbMedIndicator.Text = "Tecfidera";
                        lbMedIndicator1.Text = "Tecfidera";
                        lbTherapie.Text = "Tecfideratherapie seit (MM.JJJJ)";
                        ddlArtKontakt.SelectedIndex = 0;
                        ddlArtKontakt.Enabled = false;
                        ddlInjektionstag.SelectedIndex = 0;
                        ddlInjektionstag.Enabled = false;
                        rbSchule.ClearSelection();
                        rbSchule.Enabled = false;
                        rbZustaendigkeit.ClearSelection();
                        rbZustaendigkeit.Enabled = false;
                        rfvZustaendigkeit.Enabled = false;
                        ddlAnrufIntervall.Items.FindByValue("Wöchentlich").Attributes.Add("enabled", "enabled");
                        break;
                    }
                case "Avonex":
                    {
                        medView.Style.Add("background-color", "#ff9900");
                        medView1.Style.Add("background-color", "#ff9900");
                        tblMed.Style.Add("background-color", "#ff9900");
                        lbMedIndicator.Text = "Avonex";
                        lbMedIndicator1.Text = "Avonex";
                        lbTherapie.Text = "Avonextherapie seit (MM.JJJJ)";
                        ddlArtKontakt.Enabled = true;
                        ddlInjektionstag.Enabled = true;
                        rbSchule.Enabled = true;
                        rbZustaendigkeit.Enabled = true;
                        rfvZustaendigkeit.Enabled = true;
                        ddlAnrufIntervall.Items.FindByValue("Wöchentlich").Attributes.Add("enabled", "enabled");
                        break;
                    }
                default: break;
            }
            mvMedInd.SetActiveView(vwMedInd);
            mvMedInd1.SetActiveView(vwMedInd1);

            upPanel1.Update();
            upPanel2.Update();
            upPanel3.Update();
            upPanel4.Update();
            upPanel5.Update();
            upDummy1.Update();


        }


        protected void rbConsent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbConsent.SelectedIndex == 1)
            {
                ddl_consday.SelectedIndex = -1;
                ddl_consgetday.SelectedIndex = -1;
                ddl_consmonth.SelectedIndex = -1;
                ddl_consgetmonth.SelectedIndex = -1;
                ddl_consyear.SelectedIndex = -1;
                ddl_consgetyear.SelectedIndex = -1; ;
                upPanel5.Update();
            }
        }
    }
}