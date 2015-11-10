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

namespace PSP
{
    public partial class msdatabase2PSP : System.Web.UI.Page
    {

        int iPat;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbForename, Session["forename"].ToString());

        }

        public void bt_patdat_click(object sender, System.EventArgs e)
        {
            SqlConnection cnConn, cnConnOld;
            SqlCommand cmdConn, cmdConnOld;
            SqlDataReader drConn, drConnOld;

            string stOwner;
            string stVorthetext = "";
            string stConsent = "";
            string stSchule = "";

            cnConnOld = DBTools.getConnection();
            cnConnOld.Open();
            cmdConnOld = new SqlCommand("dbo.get_patallmsdatabase", cnConnOld);
            cmdConnOld.CommandType = CommandType.StoredProcedure;
            drConnOld = cmdConnOld.ExecuteReader();
            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdConn = new SqlCommand("dbo.insert_msdatabase2patdat", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            iPat = 0;
            try
            {
                while (drConnOld.Read())
                {
                    if (drConnOld["owner"].ToString().Contains("dks")) stOwner = "Nurse"; else stOwner = "Service";
                    if (drConnOld["einschulung"].ToString().Contains("Nurse")) stSchule = "Nurse"; else stSchule = "Service";
                    if (drConnOld["Consent"].ToString().Contains("J")) stConsent = "Ja"; else stConsent = "Nein";
                    if (drConnOld["prae1"].ToString() == "Rebif22µg") stVorthetext += drConnOld["prae1"].ToString() + " ";
                    if (drConnOld["prae2"].ToString() == "Rebif44µg") stVorthetext += drConnOld["prae2"].ToString() + " ";
                    if (drConnOld["prae3"].ToString() == "Betaferon") stVorthetext += drConnOld["prae3"].ToString() + " ";
                    if (drConnOld["prae4"].ToString() == "Copaxone") stVorthetext += drConnOld["prae4"].ToString() + " ";
                    if (drConnOld["prae5"].ToString() == "Tysabri") stVorthetext += drConnOld["prae5"].ToString() + " ";
                    if (drConnOld["prae6"].ToString() == "Immunglobuline") stVorthetext += drConnOld["prae6"].ToString() + " ";
                    if (drConnOld["prae7"].ToString() == "Gilenya") stVorthetext += drConnOld["prae7"].ToString() + " ";
                    if (drConnOld["praeanderes"].ToString() != "Nein") stVorthetext += drConnOld["praeanderes"].ToString() + " ";

                    cmdConn.Parameters.Add(new SqlParameter("@patid", Convert.ToInt32(drConnOld["patid"])));
                    cmdConn.Parameters.Add(new SqlParameter("@btkid", -1));
                    cmdConn.Parameters.Add(new SqlParameter("@sourceid", drConnOld["quelleid"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@ident", drConnOld["ident"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@name", drConnOld["name"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@vorname", drConnOld["vorname"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@titel", drConnOld["titel"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@geschlecht", drConnOld["geschlecht"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@gebdat", drConnOld["gebdat"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@adresse", drConnOld["adresse"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@plz", drConnOld["plz"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@ort", drConnOld["ort"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@tel", drConnOld["tel"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@email", drConnOld["email"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@gendate", drConnOld["gendate"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@artkontakt", drConnOld["typ"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@consent", stConsent));
                    cmdConn.Parameters.Add(new SqlParameter("@consdate", "2015-01-01"));
                    cmdConn.Parameters.Add(new SqlParameter("@consget", "2015-01-01"));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", "Avonex"));
                    cmdConn.Parameters.Add(new SqlParameter("@diagnose", drConnOld["diagnose"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@zentrum", drConnOld["ka"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@intervall", drConnOld["intervall"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@errei", drConnOld["errei"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@anonym", drConnOld["anonym"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@vorthe", drConnOld["vorthe"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@vorthetext", stVorthetext));
                    cmdConn.Parameters.Add(new SqlParameter("@thestart", drConnOld["avostart"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@einschulung", stSchule));
                    cmdConn.Parameters.Add(new SqlParameter("@status", drConnOld["status"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@leko", drConnOld["leko"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@injektionstag", drConnOld["injektionstag"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@neko", drConnOld["neko"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@groesse", drConnOld["groesse"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@gewicht", drConnOld["gewicht"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@zustaendigkeit", stOwner));
                    cmdConn.ExecuteNonQuery();
                    iPat++;
                    stVorthetext = "";
                    cmdConn.Parameters.Clear();
                }
                lbResult.Text = iPat.ToString() + " Patientendatensätze übernommen";
            }
            catch
            {
                lbResult.Text = "Fehler bei der Datenportierung";
            }
            finally
            {
                drConnOld.Close();
                cnConnOld.Close();
                cnConn.Close();
            }
            drConnOld.Close();
            cnConnOld.Close();
            cnConn.Close();
        }

        public void bt_btkdat_click(object sender, System.EventArgs e)
        {
            SqlConnection cnConn, cnConnOld;
            SqlCommand cmdConn, cmdConnOld;
            SqlDataReader drConn, drConnOld;

            iPat = 0;
            cnConnOld = DBTools.getConnection();
            cnConnOld.Open();
            cmdConnOld = new SqlCommand("dbo.get_btkmsdatabase", cnConnOld);
            cmdConnOld.CommandType = CommandType.StoredProcedure;
            drConnOld = cmdConnOld.ExecuteReader();
            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdConn = new SqlCommand("dbo.insert_msdatabase2btk", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;

            try
            {
                string stTheweMed = "";
                string stFLSAnderes = "";
                string stInjekOrt = "";
                string stAADate = "";
                string stQuelle = "";
                while (drConnOld.Read())
                {
                    stTheweMed = drConnOld["thewemed"].ToString();
                    if (drConnOld["thewemed"].ToString() == "Anderes") {
                        if (drConnOld["thewemedandere"].ToString() == "") stTheweMed = "Anderes Präparat";
                        else stTheweMed = drConnOld["thewemedandere"].ToString();
                    }

                    stFLSAnderes = drConnOld["andere"].ToString();
                    if (drConnOld["andere_text"].ToString() != "") stFLSAnderes = drConnOld["andere_text"].ToString();
                    if (drConnOld["oberarm"].ToString() == "Ja") stInjekOrt = "Oberarm";
                    if (drConnOld["oberschenkel"].ToString() == "Ja") stInjekOrt = "Oberschenkel";
                    if (drConnOld["aadatum"].ToString() == "unbekannt") stAADate = "????.??.??";
                    if (drConnOld["aadatum"] == DBNull.Value) stAADate = "????.??.??";
                    if (drConnOld["quelle"].ToString().Contains("dks")) stQuelle = "Nurse"; else stQuelle = "Service";
                    
                    cmdConn.Parameters.Add(new SqlParameter("@btkid",Convert.ToInt32(drConnOld["dksid"].ToString())));
                    cmdConn.Parameters.Add(new SqlParameter("@patid",Convert.ToInt32(drConnOld["patid"].ToString())));
                    cmdConn.Parameters.Add(new SqlParameter("@sourceid", Convert.ToInt32(drConnOld["iddks"].ToString())));
                    cmdConn.Parameters.Add(new SqlParameter("@gendate", drConnOld["datum"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@btkdate", drConnOld["datum"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@art", drConnOld["art"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@status", drConnOld["status"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@status_grund", drConnOld["status_grund"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@status_date", drConnOld["status_datum"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", drConnOld["medikament"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@thewe", drConnOld["thewe"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@thewemed", stTheweMed));
                    cmdConn.Parameters.Add(new SqlParameter("@prophy_fls", drConnOld["fsm"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_mexalen", drConnOld["mexalen"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_naproxen", drConnOld["naproxen"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_ibuprofen", drConnOld["ibuprofen"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@fls_andere", stFLSAnderes));
                    cmdConn.Parameters.Add(new SqlParameter("@prophy_haut", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@haut_coolpack", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@haut_andere","n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@gastro", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@gastro_comment", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@flush", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@flush_comment", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@pen", drConnOld["pen"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@bioset", drConnOld["bioset"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@fesp", drConnOld["fesp"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@nala", drConnOld["nala"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@aaname", drConnOld["arztanweisung"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@aadate", stAADate));
                    cmdConn.Parameters.Add(new SqlParameter("@aacomment", drConnOld["aacomment"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@injekort", stInjekOrt));
                    cmdConn.Parameters.Add(new SqlParameter("@avoject", drConnOld["avoject"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@titration", drConnOld["titration"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@titration_dauer", drConnOld["titration_dauer"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@titration_art", drConnOld["titration_art"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@tecfidosis", "n/a"));
                    cmdConn.Parameters.Add(new SqlParameter("@angschule", drConnOld["angschule"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@angschulecomment", drConnOld["angschulecomment"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@curmass", drConnOld["curmass"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@groesse", drConnOld["groesse"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@bmi", drConnOld["bmi"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@nw", drConnOld["nw"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@bhv", drConnOld["bhv"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@nwtext", drConnOld["nwtext"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@nwspontan", drConnOld["spontan"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@neko", drConnOld["neko"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@medan", drConnOld["medan"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@medancomment", drConnOld["medancomment"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@medancomplete", drConnOld["medancomplete"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@abgeschlossen", drConnOld["abgeschlossen"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@quelle", stQuelle));
                    cmdConn.Parameters.Add(new SqlParameter("@nrskontakt", "Nein"));
                    cmdConn.ExecuteNonQuery();
                    iPat++;
                    cmdConn.Parameters.Clear();
                }
                lbResult.Text = iPat.ToString() + " Betreuungsdatensätze übernommen";
            }

            catch
            {
                lbResult.Text = "Fehler bei der Datenportierung";
            }

            finally
            {
                drConnOld.Close();
                cnConnOld.Close();
                cnConn.Close();
            }

        }

        public void bt_orddat_click(object sender, System.EventArgs e)
        {

            SqlConnection cnConn, cnConnOld;
            SqlCommand cmdConn, cmdConnOld;
            SqlDataReader drConn, drConnOld;
            string stItemNum, stItemNumValue;
            string stItem, stItemValue;
            string stQuelle;

            iPat = 0;
            int iIndex = 0;
            cnConnOld = DBTools.getConnection();
            cnConnOld.Open();
            cmdConnOld = new SqlCommand("dbo.get_ordersmsdatabase", cnConnOld);
            cmdConnOld.CommandType = CommandType.StoredProcedure;
            drConnOld = cmdConnOld.ExecuteReader();
            cnConn = DBTools.getConnection();
            cnConn.Open();
            cmdConn = new SqlCommand("dbo.insert_msdatabase2orders", cnConn);
            cmdConn.CommandType = CommandType.StoredProcedure;
            string[] arProducts = { "kb","fs","tb","ab","at","bs","af","pb","au","pf","ek","ns","nl","nk","ac","kbb","kbf","kbp","fsn","ekp","abf","abp","ig","th","bm","ia","yd","bd","bg"};
            string[] arProductsNames = { "Kühlbox", "Patientpass für Avonex-Fertigspritze", "Patiententagebuch", "Nadelabwurfbox", "Alkoholtupfer, Pflaster und Ersatzkanülen Box", "Patientenbroschüre zur Anwendung des Avonex Bio-Set", "Patientenbroschüre zur Anwendung der Avonex-Fertigspritze", "Patientenbroschüre MS 2007", "Alkoholtupfer", "Pflaster", "Ersatzkanülen", "Nadel Standard", "Nadel Lang", "Nadel Kurz", "Avostartclip", "Kühlbox Bioset", "Kühlbox Fertigspritze", "Kühlbox Pen", "Patientpass", "Ersatzkanülen Pen", "Nadelabwurfbox Bioset/Fertigspritze", "Nadelabwurfbox Pen", "Igelball", "Theraband", "Mit Multipler Sklerose leben lernen 2011", "Information zur Avonex Anwendung", "Yoga DVD", "Buch - Diagnose Multiple Sklerose (Prof. Fuchs/Prof. Fazekas", "Buch - Gesund essen bei Multipler Sklerose (Trias-Verlag)"};
            int iItem = 1;
            try
            {
                while (drConnOld.Read())
                {
                    iIndex = 0;
                    iItem = 1;
                    cmdConn.Parameters.Add(new SqlParameter("@ordid",Convert.ToInt32(drConnOld["ordid"].ToString())));
                    cmdConn.Parameters.Add(new SqlParameter("@patid",Convert.ToInt32(drConnOld["patid"].ToString())));
                    cmdConn.Parameters.Add(new SqlParameter("@procdate", drConnOld["procdate"].ToString()));
                    stQuelle = drConnOld["quelle"].ToString();
                    if (stQuelle.Contains("dks")) stQuelle = "12";
                    if (stQuelle.Contains("rez")) stQuelle = "12";
                    if (stQuelle.Contains("13")) stQuelle = "12";
                    cmdConn.Parameters.Add(new SqlParameter("@quelle", stQuelle));
                    cmdConn.Parameters.Add(new SqlParameter("@pdf", drConnOld["pdf"].ToString()));
                    cmdConn.Parameters.Add(new SqlParameter("@medikament", "Avonex"));
                    iPat++;
                    foreach (string stProduct in arProducts)
                    {
                        if (drConnOld[stProduct].ToString() != "0") {
                            stItem = "@item" + iItem.ToString();
                            stItemValue = arProductsNames[iIndex].ToString();
                            stItemNum = "@itemnum" + iItem.ToString();
                            stItemNumValue = drConnOld[stProduct].ToString();
                            cmdConn.Parameters.Add(new SqlParameter(stItem, stItemValue));
                            cmdConn.Parameters.Add(new SqlParameter(stItemNum, stItemNumValue));
                            iIndex++;
                            iItem++;
                        }
                         
                    }
                    for (int i = iItem; i <= 40; i++)
                    {
                        stItem = "@item" + i.ToString();
                        stItemValue = "n/a";
                        stItemNum = "@itemnum" + i.ToString();
                        stItemNumValue = "0";
                        cmdConn.Parameters.Add(new SqlParameter(stItem, stItemValue));
                        cmdConn.Parameters.Add(new SqlParameter(stItemNum, stItemNumValue));
                    }
                    cmdConn.ExecuteNonQuery();
                    cmdConn.Parameters.Clear();
                }
                

                    lbResult.Text = iPat.ToString() + " Bestelldatensätze übernommen";
            }

            /*catch
            {
                lbResult.Text = "Fehler bei der Datenportierung";
            }*/

            finally
            {
                /*drConnOld.Close();
                cnConnOld.Close();
                cnConn.Close();*/
            }
        }
    }
}