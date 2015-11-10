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
using System.Drawing;

namespace PSP.general
{
    public partial class overviewPat : System.Web.UI.Page
    {
        static int iTotalPat, iAffectedPat;
        protected void Page_Load(object sender, EventArgs e)
        {

            int i;
            Session["PatID"] = Convert.ToInt32(Request["PatID"]);

            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());

            SqlConnection cnConn;
            SqlCommand cmdConn;
            SqlDataReader drConn;

            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdConn = new SqlCommand("dbo.count_patienten", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            Session["itp"] = Convert.ToInt32(cmdConn.ExecuteScalar());
            cnConn.Close();




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
                    case "sec":
                        mvMenu.SetActiveView(vwSec);
                        cbNurse.Checked = false;
                        break;
                    case "nrs":
                        mvMenu.SetActiveView(vwNrs);
                        cbService.Checked = false;
                        break;
                    case "ass":
                        mvMenu.SetActiveView(vwAss);
                        cbService.Checked = false;
                        break;
                    default:
                        break;
                }

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

                for (i = 0; i < ddl_intabs.Items.Count; i++)
                    if (ddl_intabs.Items[i].Value.ToString() == Session["ovintabs"].ToString()) ddl_intabs.Items[i].Selected = true;

                txFName.Text = Session["ovname"].ToString();
                txFID.Text = Session["ovid"].ToString();
                txFPlz.Text = Session["ovplz"].ToString();
                txFOrt.Text = Session["ovort"].ToString();
                cbAllOnOnePage.Checked = Convert.ToBoolean(Session["aoop"]);

                if (Session["ovlfd"].ToString().Contains("A")) cbAbge.Checked = true; else cbAbge.Checked = false;
                if (Session["ovlfd"].ToString().Contains("F")) cbLfd.Checked = true; else cbLfd.Checked = false;
                if (Session["ovzustaendigkeit"].ToString().Contains("N")) cbNurse.Checked=true;else cbNurse.Checked=false;
                if (Session["ovzustaendigkeit"].ToString().Contains("S")) cbService.Checked = true; else cbService.Checked = false;
                if (Session["ovmedikament"].ToString().Contains("P")) cbPlegridy.Checked = true; else cbTecfidera.Checked = false;
                if (Session["ovmedikament"].ToString().Contains("T")) cbTecfidera.Checked = true; else cbPlegridy.Checked = false;
                if (Session["ovmedikament"].ToString().Contains("A")) cbAvonex.Checked = true; else cbAvonex.Checked = false;
                if ((Boolean)Session["ovintervall"]) cbIntervall.Checked = true; else cbIntervall.Checked = false;
                if ((Boolean)Session["ovwechsel"]) cbWechsel.Checked = true; else cbWechsel.Checked = false;
            }

            Session["aoop"] = cbAllOnOnePage.Checked;
            if (cbAllOnOnePage.Checked) gv_patlist.AllowPaging = false;
            else gv_patlist.AllowPaging = true;

            txFIDV.Text = txFID.Text.ToString() + "%";
            Session["ovid"] = txFID.Text.ToString();
            txFNameV.Text = txFName.Text.ToString() + "%";
            Session["ovname"] = txFName.Text.ToString();

            /*txMedikamentV.Text = "[";
            if (cbAvonex.Checked) txMedikamentV.Text += "A";
            if (cbTysabri.Checked) txMedikamentV.Text += "T";
            txMedikamentV.Text += "]%";
            Session["ovmedikament"] = txMedikamentV.Text.Substring(0,txMedikamentV.Text.Length-1);*/



            txFPlzV.Text = txFPlz.Text.ToString() + "%";
            Session["ovplz"] = txFPlz.Text.ToString();
            txFOrtV.Text = txFOrt.Text.ToString() + "%";
            Session["ovort"] = txFOrt.Text.ToString();
            txFIntabsV.Text = ddl_intabs.SelectedItem.Text.ToString();
            Session["ovintabs"] = ddl_intabs.SelectedItem.Text.ToString();
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

            txFNrsSecV.Text = "";
            txFNrsSecV.Text += "[k";
            if (cbNurse.Checked) txFNrsSecV.Text += "N";
            if (cbService.Checked) txFNrsSecV.Text += "S";
            txFNrsSecV.Text += "]%";


            Session["ovlfd"] = txFLfdV.Text.Substring(0, txFLfdV.Text.Length - 1);
            /*Session["ovkontakt"] = cbKontakt.Checked;

           if (cbKontakt.Checked) txonkaV.Text = genSettings.get_pat_onka(txFOwnerV.Text);
           else txonkaV.Text = genSettings.get_pat_onka("nofilter");*/

            DateTime dtCurrent = DateTime.Now;
            string stCompare;
            if (cbIntervall.Checked)
            {
                Session["ovintervall"] = true;
                switch (ddl_intabs.SelectedItem.Text.ToString())
                {
                    case "Wöchentlich":
                        dtCurrent = dtCurrent.AddDays(-8);
                        stCompare = dtCurrent.ToString("yyyy-MM-dd");
                        txIntervallV.Text = stCompare;
                        break;
                    case "Alle 2 Wochen":
                        dtCurrent = dtCurrent.AddDays(-15);
                        stCompare = dtCurrent.ToString("yyyy-MM-dd");
                        txIntervallV.Text = stCompare;
                        break;
                    case "Monatlich":
                        dtCurrent = dtCurrent.AddDays(-32);
                        stCompare = dtCurrent.ToString("yyyy-MM-dd");
                        txIntervallV.Text = stCompare;
                        break;
                    case "Alle 2 Monate":
                        dtCurrent = dtCurrent.AddDays(-62);
                        stCompare = dtCurrent.ToString("yyyy-MM-dd");
                        txIntervallV.Text = stCompare;
                        break;
                    case "Alle 3 Monate":
                        dtCurrent = dtCurrent.AddDays(-92);
                        stCompare = dtCurrent.ToString("yyyy-MM-dd");
                        txIntervallV.Text = stCompare;
                        break;
                    case "Alle 4 Monate":
                        dtCurrent = dtCurrent.AddDays(-122);
                        stCompare = dtCurrent.ToString("yyyy-MM-dd");
                        txIntervallV.Text = stCompare;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                txIntervallV.Text = "3000-12-31";
                txIntervallL.Text = "1900-01-01";
                Session["ovintervall"] = false;
            }
            dtCurrent = DateTime.Now;
            if (cbWechsel.Checked)
            {
                Session["ovwechsel"] = true;
                dtCurrent = dtCurrent.AddDays(-62);
                stCompare = dtCurrent.ToString("yyyy-MM-dd");
                txWechselV.Text = stCompare;
                txWechselL.Text = "1900-01-01";
            }
            else
            {
                txWechselV.Text = "3000-12-31";
                txWechselL.Text = "1900-01-01";
            }
        }

        protected void gv_dkskontakt_rdb(Object sender, GridViewRowEventArgs e)
        {
            HyperLink hl_details, hl_patient, hl_kontakt, hl_order;
            System.Web.UI.WebControls.Image img_details, img_kontakt, img_patient, img_medikament, img_order, img_status, img_wechsel;
            Label lb_patid, lb_owner;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                /*img_medikament = (Image)e.Row.FindControl("img_medikament");
                if (img_medikament.ToolTip == "Avonex") img_medikament.ImageUrl = "~/images/avonex.png";
                else img_medikament.ImageUrl = "~/images/tysabri.png";*/

                img_status = (System.Web.UI.WebControls.Image)e.Row.FindControl("img_status");
                if (img_status.ToolTip == "Abbruch") img_status.ImageUrl = "~/images/abbr.png";
                else img_status.ImageUrl = "~/images/j.png";

                img_medikament = (System.Web.UI.WebControls.Image)e.Row.FindControl("img_medikament");
                switch (img_medikament.ToolTip)
                {
                    case "Plegridy":
                        img_medikament.ImageUrl = "~/images/plegridy.png";
                        break;
                    case "Tecfidera":
                        img_medikament.ImageUrl = "~/images/tecfidera.png";
                        break;
                    case "Avonex":
                        img_medikament.ImageUrl = "~/images/avonex.png";
                        break;
                    default:
                        break;
                }

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
            string stNow = dtNow.ToString("yyMMdd_hhmmss");

            fileName = dirName + "/patienten_" + stNow + ".csv";
            fn.Add((string)fileName);
            fpAll = new StreamWriter(fileName, false, Encoding.UTF8);

            cnConn = DBTools.getConnection();
            cnConn.Open();
            fpAll.WriteLine("PatID;Identifikation;Quelle;Name;Vorname;Titel;Geschlecht;Geburtstdatum;Adresse;Postleitzahl;Ort;Telefon;E-Mail;Art des Kontaktes;Einwilligung;Einwilligung Erstelldatum;Einwilligung Einlangdatum;Medikament;Jahr der Diagnose;Zentrum;Anrufintervall;Erreichbarkeit;Anonymität;Vortherapie;Vortherapie Kommentar;Vortherapie Start; Letzter Kontakt;Status;Nächster Kontakt;Einschulung;Injektionstag;Groesse;Gewicht;Zuständigkeit");
            cmdAll = new SqlCommand("get_patdat", cnConn);
            cmdAll.CommandType = CommandType.StoredProcedure;

            foreach (string id in al)
            {
                pid = Convert.ToInt32(id);
                cmdAll.Parameters.Add(new SqlParameter("@patid", pid));
                drAll = cmdAll.ExecuteReader();
                drAll.Read();
                fpAll.WriteLine(drAll["patid"].ToString() + " ; "  + drAll["ident"].ToString() + " ; " + drAll["sourceid"].ToString() + " ; " + drAll["name"].ToString() + " ; " + drAll["vorname"].ToString() + " ; " + drAll["titel"].ToString() + " ; " + drAll["geschlecht"].ToString() + " ; " + drAll["gebdat"].ToString() + " ; " + drAll["adresse"].ToString() + " ; " + drAll["plz"].ToString() + " ; " + drAll["ort"].ToString() + " ; " + drAll["tel"].ToString() + " ; " + drAll["email"].ToString() + " ; "  + drAll["artkontakt"].ToString() + " ; " + drAll["consent"].ToString() + " ; " + drAll["consdate"].ToString() + " ; " + drAll["consget"].ToString() + " ; " + drAll["medikament"].ToString() + " ; " + drAll["diagnose"].ToString() + " ; " + drAll["zentrum"].ToString() + " ; " + drAll["intervall"].ToString() + " ; " + drAll["erreichbarkeit"].ToString() + " ; " + drAll["anonym"].ToString() + " ; " + drAll["vorthe"].ToString() + " ; " + drAll["vorthetext"].ToString() + " ; " + drAll["thestart"].ToString() + " ; " + drAll["leko"].ToString() + " ; " + drAll["status"].ToString() + " ; " + drAll["neko"].ToString() + " ; " + drAll["einschulung"].ToString() + " ; " + drAll["injektionstag"].ToString() + " ; " + drAll["groesse"].ToString() + " ; " + drAll["gewicht"].ToString() + " ; " + drAll["zustaendigkeit"].ToString());
                iCount++;
                cmdAll.Parameters.Clear();
                fileName = dirName + "/" + drAll["ident"].ToString() + "_" + stNow + ".csv";
                drAll.Close();
                if (!cbExport.Checked)
                {
                    cmdPat = new SqlCommand("get_btkbypatid", cnConn);
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
                    fpPat.WriteLine("Quelle;PatID;Medikament;Eintragsdatum;Datum;Art des Kontaktes;Status;Grund für Status;Datum Statuswechsel;Therapiewechsel;Präparat;FLS Prophylaxe;Mexalen;Naproxen;Ibuprofen;FLS Anderes;Hautprophylaxe;Coolpack;Haut Anderes;GI Medikation; GI Info; Flush Medikation; Flush Info;Nebenwirkungen;Pen;Bioset;Fertigspritze;Nadellänge;Anweisender Arzt;Anweisedatum;Kommentar Anweisung;Injektionsort;Avoject,Titration;Dauer Titration;Art der Titration;Dosierung Tecfidera;Schulung;Schulung Kommentar;Gewicht;Groesse;BMI;Nebenwirkung; Beschreibung;Behandlungsverlauf;Beschreibung Nebenwirkung;Spontane Nebenwirkungsmeldung;Medizinische Anfrage;Medizinische Anfrage Kommentar;Medizinische Anfrage abgeschlossen;Abgeschlossen;Nächster Kontakt");
                    while (drPat.Read())
                    {
                        fpPat.WriteLine(drPat["sourceid"].ToString() + " ; " + drPat["patid"].ToString() + " ; " + drPat["medikament"].ToString() + " ; " + drPat["gendate"].ToString() + " ; " + drPat["btkdate"].ToString() + " ; " + drPat["art"].ToString() + " ; " + drPat["status"].ToString() + " ; " + drPat["status_grund"].ToString() + " ; " + drPat["status_date"].ToString() + " ; " + drPat["thewe"].ToString() + " ; " + drPat["thewemed"].ToString() + " ; " + drPat["prophy_fls"].ToString() + " ; " + drPat["fls_mexalen"].ToString() + " ; " + drPat["fls_naproxen"].ToString() + " ; " + drPat["fls_ibuprofen"].ToString() + " ; " + drPat["fls_andere"].ToString() + " ; " + drPat["prophy_haut"].ToString() + " ; " + drPat["haut_coolpack"].ToString() + " ; " + drPat["haut_andere"].ToString() + " ; " + drPat["gastro"].ToString() + " ; " + drPat["gastro_comment"].ToString() + " ; " + drPat["flush"].ToString() + " ; " + drPat["flush_comment"].ToString() + " ; " + drPat["pen"].ToString() + " ; " + drPat["bioset"].ToString() + " ; " + drPat["fesp"].ToString() + " ; " + drPat["nala"].ToString() + " ; " + drPat["aaname"].ToString() + " ; " + drPat["aadate"].ToString() + " ; " + drPat["aacomment"].ToString() + " ; " + drPat["injekort"].ToString() + " ; " + drPat["avoject"].ToString() + " ; " + drPat["titration"].ToString() + " ; " + drPat["titration_dauer"].ToString() + " ; " + drPat["titration_art"].ToString() + " ; " + drPat["tecfidosis"].ToString() + " ; " + drPat["angschule"].ToString() + " ; " + drPat["angschulecomment"].ToString() + " ; " + drPat["curmass"].ToString() + " ; " + drPat["groesse"].ToString() + " ; " + drPat["bmi"].ToString() + " ; " + drPat["nw"].ToString() + " ; " + drPat["bhv"].ToString() + " ; " + drPat["nwtext"].ToString() + " ; " + drPat["nwspontan"].ToString() + " ; " + drPat["medan"].ToString() + " ; " + drPat["medancomplete"].ToString() + " ; " + drPat["medancomment"].ToString() + " ; " + drPat["abgeschlossen"].ToString() + " ; " + drPat["neko"].ToString());

                    }
                    drPat.Close();
                    fpPat.Close();
                    cmdPat.Parameters.Clear();
                }
            }
            fpAll.Close();
            cnConn.Close();

            int aid = Convert.ToInt32(Session["id"]);
            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdAll = new SqlCommand("dbo.get_account", cnConn);
            cmdAll.CommandType = CommandType.StoredProcedure;
            cmdAll.Parameters.Add(new SqlParameter("@id", aid));
            drAll = cmdAll.ExecuteReader();
            drAll.Read();
            string stEmail = drAll["email"].ToString();
            string cc = "";
            string from = "";
            string to = drAll["email"].ToString();
            string anrede = "Sehr geehrte/r Fr./Hr. " + drAll["Name"].ToString();
            drAll.Close();
            cmdAll.Parameters.Clear();
            cnConn.Close();
            string subject = "PSP::Export von Patientendaten";
            string msgb = anrede + "!";
            msgb = String.Concat(msgb, "\nSie haben Daten von " + iCount.ToString() + " Patienten für den Export gewählt.\nFinden Sie die entsprechenden Dateien im Anhang dieses E-Mail.");
            msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\ntecfiCare Service System");
            MailMessage message = new MailMessage();

            /*msdbEmail.SentEmailAttach(stEmail, from, to, cc, subject, msgb, fn);*/
            Response.Redirect("~/confirm_exports.aspx");

        }

        protected void sqldb_selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            iAffectedPat = e.AffectedRows;

            if (!IsPostBack) Session["itp"] = iAffectedPat;
            lb_countPat.Text = "Gesamtanzahl: " + Session["itp"].ToString() + " / Gefiltert: " + iAffectedPat.ToString();
        }



        protected void cbAllOnOnePage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllOnOnePage.Checked) gv_patlist.AllowPaging = false;
            else gv_patlist.AllowPaging = true;
            Session["aoop"] = cbAllOnOnePage.Checked;
        }
    }
}