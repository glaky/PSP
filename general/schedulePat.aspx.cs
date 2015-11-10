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
using System.IO;
using System.Text;

namespace PSP.general
{
    public partial class schedulePat : System.Web.UI.Page
    {
        static int iTotalPat, iAffectedPat;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatID"] = Convert.ToInt32(Request["PatID"]);

            string stday, stmon, styear;
            int i;

            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbSurname, Session["forename"].ToString());

            if (!IsPostBack)
            {
                /*Session["prevpage"] = Request.UrlReferrer.ToString();*/

                string stCurYear = DateTime.Now.ToString("yyyy");
                string stNextYear = ((Convert.ToInt32(stCurYear)) + 1).ToString();
                string stLastYear = ((Convert.ToInt32(stCurYear)) - 1).ToString();

                ArrayList day = new ArrayList();
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
                ddl_sday.DataSource = day;
                ddl_sday.DataBind();
                ddl_eday.DataSource = day;
                ddl_eday.DataBind();


                ArrayList month = new ArrayList();
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
                ddl_smonth.DataSource = month;
                ddl_smonth.DataBind();
                ddl_emonth.DataSource = month;
                ddl_emonth.DataBind();

                ArrayList dyear = new ArrayList();
                dyear.Add(stLastYear);
                dyear.Add(stCurYear);
                dyear.Add(stNextYear);
                ddl_syear.DataSource = dyear;
                ddl_syear.DataBind();
                ddl_eyear.DataSource = dyear;
                ddl_eyear.DataBind();

                ArrayList intervall = new ArrayList();
                intervall.Add("Kein Filter");
                intervall.Add("keine Angabe");
                intervall.Add("Nie");
                intervall.Add("Wöchentlich");
                intervall.Add("Alle 2 Wochen");
                intervall.Add("Monatlich");
                intervall.Add("Alle 2 Monate");
                intervall.Add("Alle 3 Monate");
                intervall.Add("Alle 4 Monate");

                ddl_intabs.DataSource = intervall;
                ddl_intabs.DataBind();
;

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

                for (i = 0; i < ddl_intabs.Items.Count; i++)
                    if (ddl_intabs.Items[i].Value.ToString() == Session["tmintabs"].ToString()) ddl_intabs.Items[i].Selected = true;

                txFName.Text = Session["tmname"].ToString();
                txFID.Text = Session["tmid"].ToString();
                txFPlz.Text = Session["tmplz"].ToString();
                txFOrt.Text = Session["tmort"].ToString();
                cbAllOnOnePage.Checked = Convert.ToBoolean(Session["tmaoop"]);

                if (Session["tmlfd"].ToString().Contains("A")) cbAbge.Checked = true; else cbAbge.Checked = false;
                if (Session["tmlfd"].ToString().Contains("F")) cbLfd.Checked = true; else cbLfd.Checked = false;
                if (Session["tmmedikament"].ToString().Contains("P")) cbPlegridy.Checked = true; else cbTecfidera.Checked = false;
                if (Session["tmmedikament"].ToString().Contains("T")) cbTecfidera.Checked = true; else cbPlegridy.Checked = false;
                if (Session["tmmedikament"].ToString().Contains("A")) cbAvonex.Checked = true; else cbAvonex.Checked = false;
                if (Session["tmzustaendigkeit"].ToString().Contains("N")) cbNurse.Checked = true; else cbNurse.Checked = false;
                if (Session["tmzustaendigkeit"].ToString().Contains("S")) cbService.Checked = true; else cbService.Checked = false;


                stday = Session["tmsdate"].ToString().Substring(8, 2);
                stmon = Session["tmsdate"].ToString().Substring(5, 2);
                styear = Session["tmsdate"].ToString().Substring(0, 4);
                for (i = 0; i < ddl_sday.Items.Count; i++)
                {
                    if (ddl_sday.Items[i].Value.ToString() == stday)
                    {
                        ddl_sday.Items[i].Selected = true;
                    }
                }
                for (i = 0; i < ddl_smonth.Items.Count; i++)
                {
                    if (ddl_smonth.Items[i].Value.ToString() == stmon)
                    {
                        ddl_smonth.Items[i].Selected = true;
                    }
                }
                for (i = 0; i < ddl_syear.Items.Count; i++)
                {
                    if (ddl_syear.Items[i].Value.ToString() == styear)
                    {
                        ddl_syear.Items[i].Selected = true;
                    }
                }

                stday = Session["tmedate"].ToString().Substring(8, 2);
                stmon = Session["tmedate"].ToString().Substring(5, 2);
                styear = Session["tmedate"].ToString().Substring(0, 4);
                for (i = 0; i < ddl_eday.Items.Count; i++)
                {
                    if (ddl_eday.Items[i].Value.ToString() == stday)
                    {
                        ddl_eday.Items[i].Selected = true;
                    }
                }
                for (i = 0; i < ddl_emonth.Items.Count; i++)
                {
                    if (ddl_emonth.Items[i].Value.ToString() == stmon)
                    {
                        ddl_emonth.Items[i].Selected = true;
                    }
                }
                for (i = 0; i < ddl_eyear.Items.Count; i++)
                {
                    if (ddl_eyear.Items[i].Value.ToString() == styear)
                    {
                        ddl_eyear.Items[i].Selected = true;
                    }
                }

                if ((Boolean)Session["tmuber"])
                {
                    cbUber.Checked = true;
                }
                else
                {
                    cbUber.Checked = false;
                }
            }

            Session["tmsdate"] = ddl_syear.SelectedItem.Text.ToString() + "-" + ddl_smonth.SelectedItem.Text.ToString() + "-" + ddl_sday.SelectedItem.Text.ToString();
            Session["tmedate"] = ddl_eyear.SelectedItem.Text.ToString() + "-" + ddl_emonth.SelectedItem.Text.ToString() + "-" + ddl_eday.SelectedItem.Text.ToString();

            Session["tmaoop"] = cbAllOnOnePage.Checked;
            if (cbAllOnOnePage.Checked) gv_patlist.AllowPaging = false;
            else gv_patlist.AllowPaging = true;

            txFIDV.Text = txFID.Text + "%";
            Session["tmid"] = txFID.Text;
            txFNameV.Text = txFName.Text + "%";
            Session["tmname"] = txFName.Text;
            txFPlzV.Text = txFPlz.Text + "%";
            Session["tmplz"] = txFPlz.Text;
            txFOrtV.Text = txFOrt.Text + "%";
            Session["tmort"] = txFOrt.Text;
            txFIntabsV.Text = ddl_intabs.SelectedItem.Text.ToString();
            Session["tmintabs"] = ddl_intabs.SelectedItem.Text.ToString();
            if (txFIntabsV.Text == "Kein Filter") txFIntabsV.Text = "%";
            txFLfdV.Text = "[";
            if (cbLfd.Checked) txFLfdV.Text += "NBFWK";
            if (cbAbge.Checked) txFLfdV.Text += "A";
            txFLfdV.Text += "]%";
            txFMedikamentV.Text = "[";
            if (cbPlegridy.Checked) txFMedikamentV.Text += "P";
            if (cbTecfidera.Checked) txFMedikamentV.Text += "T";
            if (cbAvonex.Checked) txFMedikamentV.Text += "A";
            txFMedikamentV.Text += "]%";

            txFNrsSecV.Text = "[";
            if (cbNurse.Checked) txFNrsSecV.Text += "N";
            if (cbService.Checked) txFNrsSecV.Text += "S";
            txFNrsSecV.Text += "]%";

            txIntervallS.Text = ddl_syear.SelectedItem.Text.ToString() + "-" + ddl_smonth.SelectedItem.Text.ToString() + "-" + ddl_sday.SelectedItem.Text.ToString();
            Session["tmsdate"] = ddl_syear.SelectedItem.Text.ToString() + "-" + ddl_smonth.SelectedItem.Text.ToString() + "-" + ddl_sday.SelectedItem.Text.ToString();
            txIntervallE.Text = ddl_eyear.SelectedItem.Text.ToString() + "-" + ddl_emonth.SelectedItem.Text.ToString() + "-" + ddl_eday.SelectedItem.Text.ToString();
            Session["tmedate"] = ddl_eyear.SelectedItem.Text.ToString() + "-" + ddl_emonth.SelectedItem.Text.ToString() + "-" + ddl_eday.SelectedItem.Text.ToString();
            if (cbUber.Checked)
            {
                txIntervallS.Text = "1900-01-01";
                /*Session["tmsdate"] = "1900-01-01";*/
                Session["tmuber"] = true;
            }
            else Session["tmuber"] = false;

        }

        protected void gv_dkskontakt_rdb(Object sender, GridViewRowEventArgs e)
        {
            HyperLink hl_details, hl_patient, hl_kontakt, hl_order;
            Image img_details, img_kontakt, img_patient, img_medikament, img_order, img_status, img_wechsel;
            Label lb_patid, lb_owner, lb;
            DateTime dtNow, dtNeko;
            string stNow;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lb = (Label)e.Row.FindControl("lbNeko");
                dtNow = DateTime.Now;
                DateTime.TryParseExact(lb.Text.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtNeko);

                if (dtNow > dtNeko)
                {
                    img_details = (Image)e.Row.FindControl("imNeko");
                    img_details.Visible = true;
                    img_details.ToolTip = "Anruf überfällig";
                }

                img_status = (Image)e.Row.FindControl("img_status");
                if (img_status.ToolTip == "Abbruch") img_status.ImageUrl = "~/images/abbr.png";
                else img_status.ImageUrl = "~/images/j.png";

                

                lb_patid = (Label)e.Row.FindControl("lbPatID");
                int pid = Convert.ToInt32(lb_patid.Text.ToString());

                string strScript = "SelectDeSelectHeader(" + ((CheckBox)e.Row.Cells[0].FindControl("cbSingle")).ClientID + ");";
                ((CheckBox)e.Row.Cells[0].FindControl("cbSingle")).Attributes.Add("onclick", strScript);
            }
        }
        protected void bt_export_click(object sender, EventArgs e)
        {

            ArrayList al = new ArrayList();
            ArrayList fn = new ArrayList();
            string dirName;
            string fileName;
            SqlConnection cnConn;
            SqlCommand cmdAll, cmdPat;
            SqlDataReader drAll, drPat;
            string dateid;
            StreamWriter fpAll, fpPat;
            Int32 pid;
            int iCount = 0;

            dateid = DateTime.Now.ToString("yyyyMMddHHmmss");

            foreach (GridViewRow row in gv_patlist.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("cbSingle");
                if (cb != null && cb.Checked)
                {
                    al.Add((string)gv_patlist.DataKeys[row.RowIndex].Value.ToString());
                }
            }
            dirName = Server.MapPath("~/exports/") + dateid;
            Directory.CreateDirectory(dirName);
            DateTime dtNow = DateTime.Now;
            string stNow = dtNow.ToString("yyMMdd");

            fileName = dirName + "/patienten_" + stNow + ".csv";
            fn.Add((string)fileName);
            fpAll = new StreamWriter(fileName, false, Encoding.UTF8);

            cnConn = DBTools.getConnection();
            cnConn.Open();
            fpAll.WriteLine("PatID;Identifikation;Name;Vorname;Titel;Geschlecht;Geburtstdatum;Adresse;Postleitzahl;Ort;Telefon;E-Mail;Jahr der Diagnose;Medikament;Krankenhaus;Arzt;Kontakt Arzt;Anrufintervall;Letzter Kontakt");
            cmdAll = new SqlCommand("sp_get_patientall", cnConn);
            cmdAll.CommandType = CommandType.StoredProcedure;

            foreach (string id in al)
            {
                pid = Convert.ToInt32(id);
                cmdAll.Parameters.Add(new SqlParameter("@patid", pid));
                drAll = cmdAll.ExecuteReader();
                drAll.Read();
                fpAll.WriteLine(drAll["patid"].ToString() + ";" + drAll["ident"].ToString() + ";" + drAll["Name"].ToString() + ";" + drAll["vorname"].ToString() + ";" + drAll["titel"].ToString() + ";" + drAll["geschlecht"].ToString() + ";" + drAll["gebdat"].ToString() + ";" + drAll["adresse"].ToString() + ";" + drAll["plz"].ToString() + ";" + drAll["ort"].ToString() + ";" + drAll["tel"].ToString() + ";" + drAll["email"].ToString() + ";" + drAll["diagnose"] + ";" + drAll["medikament"].ToString() + ";" + drAll["ka"].ToString() + ";" + drAll["arzt_name"].ToString() + ", " + drAll["arzt_vorname"].ToString() + ";" + drAll["telefon"].ToString() + ";" + drAll["intervall"].ToString() + ";" + drAll["leko"].ToString());
                iCount++;
                cmdAll.Parameters.Clear();
                fileName = dirName + "/" + drAll["ident"].ToString() + "_" + stNow + ".csv";
                drAll.Close();
                cmdPat = new SqlCommand("sp_get_dkskontaktbypatid", cnConn);
                cmdPat.CommandType = CommandType.StoredProcedure;
                cmdPat.Parameters.Add(new SqlParameter("@patid", pid));
                drPat = cmdPat.ExecuteReader();
                string testa;
                if (drPat.HasRows)
                    testa = "JA";
                else
                    testa = "NEIN";


                fn.Add((string)fileName);
                fpPat = new StreamWriter(fileName, false, Encoding.UTF8);
                fpPat.WriteLine("Datum;Art des Kontaktes;Status;Grund für Status;Medikament;Nadellänge;Arztabweisung;Bioset;Fertigspritze;Titration;Dauer Titration;Schulung Angehöriger;Avoject;EDSS;Schübe;Nebenwirkungen;FSM;Mexalen;Naproxen;Ibuprofen;Andere;Andere Spezifikation;Oberarm;Oberschenkel;Behandlungsverlauf");
                while (drPat.Read())
                {
                    fpPat.WriteLine(drPat["datum"].ToString() + ";" + drPat["art"].ToString() + ";" + drPat["status"].ToString() + ";" + drPat["status_grund"].ToString() + ";" + drPat["medikament"].ToString() + ";" + drPat["nala"].ToString() + ";" + drPat["arztanweisung"].ToString() + ";" + drPat["bioset"].ToString() + ";" + drPat["fesp"].ToString() + ";" + drPat["titration"].ToString() + ";" + drPat["titration_dauer"].ToString() + ";" + drPat["angschule"].ToString() + ";" + drPat["avoject"].ToString() + ";" + drPat["edss"].ToString() + ";" + drPat["schuebe"].ToString() + ";" + drPat["nw"].ToString() + ";" + drPat["fsm"].ToString() + ";" + drPat["mexalen"].ToString() + ";" + drPat["naproxen"].ToString() + ";" + drPat["ibuprofen"].ToString() + ";" + drPat["andere"].ToString() + ";" + drPat["andere_text"].ToString() + ";" + drPat["oberarm"].ToString() + ";" + drPat["oberschenkel"].ToString() + ";" + drPat["bhv"].ToString());

                }
                drPat.Close();
                fpPat.Close();
                cmdPat.Parameters.Clear();

            }
            fpAll.Close();
            cnConn.Close();

            int aid = Convert.ToInt32(Session["id"]);
            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdAll = new SqlCommand("dbo.sp_get_account", cnConn);
            cmdAll.CommandType = CommandType.StoredProcedure;
            cmdAll.Parameters.Add(new SqlParameter("@id", aid));
            drAll = cmdAll.ExecuteReader();
            drAll.Read();
            string stEmail = drAll["email"].ToString();
            string to = drAll["Name"].ToString() + ", " + drAll["surname"].ToString();
            string cc = "";
            string from = "gunter.laky@hermesoft.at";
            string anrede = "Sehr geehrte/r Fr./Hr. " + drAll["Name"].ToString();
            drAll.Close();
            cmdAll.Parameters.Clear();
            cnConn.Close();
            string subject = "!!TEST!!MS Nurse Servive::Export von Patientendaten";
            string msgb = anrede + "!";
            msgb = String.Concat(msgb, "\nSie haben Daten von " + iCount.ToString() + " Patienten für den Export gewählt.\nFinden Sie die entsprechenden Dateien im Anhang dieses E-Mail.");
            msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\nMS Nurse Service System");
            MailMessage message = new MailMessage();

            msdbEmail.SentEmailAttach(stEmail, from, to, cc, subject, msgb, fn);
            Response.Redirect("~/dks/confirm_export.aspx");

        }

        protected void sqldb_selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            /*  iAffectedPat = e.AffectedRows;

              if (!IsPostBack) Session["itp"] = iAffectedPat;
              lb_countPat.Text = "Gesamtanzahl: " + Session["itp"].ToString() + " / Gefiltert: " + iAffectedPat.ToString();*/
        }

        protected void cvValddlDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string stsDate, steDate, steYear, steMonth, steDay, stsYear, stsMonth, stsDay;
            string str30Months = "0204060911";
            string strFeb = "30";
            DateTime dtsDate, dteDate;


            stsYear = ddl_syear.SelectedItem.Text.ToString();
            stsMonth = ddl_smonth.SelectedItem.Text.ToString();
            stsDay = ddl_sday.SelectedItem.Text.ToString();

            steYear = ddl_eyear.SelectedItem.Text.ToString();
            steMonth = ddl_emonth.SelectedItem.Text.ToString();
            steDay = ddl_eday.SelectedItem.Text.ToString();


            stsDate = stsYear + "-" + stsMonth + "-" + stsDay;
            steDate = steYear + "-" + steMonth + "-" + steDay;
            if (!(DateTime.TryParseExact(stsDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtsDate)))
            {
                cvValddlDate.ErrorMessage = "Ungültiges Startdatum";
                args.IsValid = false;
                return;
            }

            if (!(DateTime.TryParseExact(steDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dteDate)))
            {
                cvValddlDate.ErrorMessage = "Ungültiges Enddatum";
                args.IsValid = false;
                return;
            }


            if (dteDate < dtsDate)
            {
                cvValddlDate.ErrorMessage = "Enddatum vor Startdatum";
                args.IsValid = false;
                return;
            }

            if ((!(DateTime.IsLeapYear(Convert.ToInt32(stsYear)))) && (stsDay == "29") && (stsMonth == "02"))
            {
                cvValddlDate.ErrorMessage = stsYear + " ist kein Schaltjahr";
                args.IsValid = false;
                return;
            }

            if ((stsDay == "31") && (str30Months.Contains(stsMonth)))
            {
                args.IsValid = false;
                cvValddlDate.ErrorMessage = "Ungültiges Startdatum";
            }

            if ((strFeb.Contains(stsDay)) && (stsMonth == "02"))
            {
                args.IsValid = false;
                cvValddlDate.ErrorMessage = "Ungültiges Startdatum";
            }

            if ((!(DateTime.IsLeapYear(Convert.ToInt32(steYear)))) && (steDay == "29") && (steMonth == "02"))
            {
                cvValddlDate.ErrorMessage = stsYear + " ist kein Schaltjahr";
                args.IsValid = false;
                return;
            }

            if ((steDay == "31") && (str30Months.Contains(steMonth)))
            {
                args.IsValid = false;
                cvValddlDate.ErrorMessage = "Ungültiges Enddatum";
            }

            if ((strFeb.Contains(steDay)) && (stsMonth == "02"))
            {
                args.IsValid = false;
                cvValddlDate.ErrorMessage = "Ungültiges Enddatum";
            }
        }

        protected void cbAllOnOnePage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllOnOnePage.Checked) gv_patlist.AllowPaging = false;
            else gv_patlist.AllowPaging = true;
            Session["tmaoop"] = cbAllOnOnePage.Checked;
        }
    }
}