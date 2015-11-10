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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;

namespace PSP.general
{
    public partial class orderPat : System.Web.UI.Page
    {
        static string stAnrede, stTitel, stAdrName, stAdrVorname, stAdrAdr, stAdrPlz, stAdrOrt, stAdrSex;
        static string stPatName, stPatVorname, stPatAdr, stPatPlz, stPatOrt, stPatSex,stMedRef;
        static string item1, item2, item3, item4;
        static string stPdf;
        static string procdate;
        static Boolean bArzt;
        static int iNumProductsMax = 40, iItem = 0, iNumProducts = 0, iNp = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            scOrder.RegisterAsyncPostBackControl(cbArzt);
            mvMedInd.SetActiveView(vwMedIndNo);
            mvMedInd1.SetActiveView(vwMedInd1No);
            Session["PatID"] = Convert.ToInt32(Request["PatID"]);
            Session["CtcID"] = Convert.ToInt32(Request["CtcID"]);
            if ((Session["login"] == null)) Response.Redirect("~/error/error01.aspx");
            if (!(SecurityHelper.isLog(Session["login"].ToString()))) Response.Redirect("~/error/error01.aspx");
            genSettings.setHeader(lbRole, Session["role"].ToString(), lbTitel, Session["titel"].ToString(), lbName, Session["name"].ToString(), lbSurname, Session["forename"].ToString());
            int i;
            if (!IsPostBack)
            {
                Session["prevpage"] = Request.UrlReferrer.ToString();
                lbTitle.Text = "Neue Bestellung";
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
                mvAdressat.SetActiveView(vwAdressatArztNo);
                bArzt = false;
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_patdat", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();

                stPatName = drConn["name"].ToString();
                stAdrName = stPatName;
                stAdrSex = (drConn["geschlecht"].ToString() == "Weiblich") ? "Frau" : "Herr";
                stPatVorname = drConn["vorname"].ToString();
                stAdrVorname = stPatVorname;

                stPatAdr = drConn["adresse"].ToString();
                stAdrAdr = stPatAdr;
                stPatPlz = drConn["plz"].ToString();
                stAdrPlz = stPatPlz;
                stPatOrt = drConn["ort"].ToString();
                stAdrOrt = stPatOrt;
                stMedRef = drConn["medikament"].ToString();
                drConn.Close();
                cnConn.Close();

                switch (stMedRef)
                {
                    case "Plegridy":
                        {
                            medView.Style.Add("background-color", "#99ccff");
                            medView1.Style.Add("background-color", "#99ccff");
                            lbMedIndicator.Text = "Plegridy";
                            lbMedIndicator1.Text = "Plegridy";
                            break;
                        }
                    case "Tecfidera":
                        {
                            medView.Style.Add("background-color", "#ff66ff");
                            medView1.Style.Add("background-color", "#ff66ff");
                            lbMedIndicator.Text = "Tecfidera";
                            lbMedIndicator1.Text = "Tecfidera";
                            break;
                        }
                    case "Avonex":
                        {
                            medView.Style.Add("background-color", "#ff9900");
                            medView1.Style.Add("background-color", "#ff9900");
                            lbMedIndicator.Text = "Avonex";
                            lbMedIndicator1.Text = "Avonex";
                            break;
                        }
                    default: break;
                }
                mvMedInd.SetActiveView(vwMedInd);
                mvMedInd1.SetActiveView(vwMedInd1);

                ArrayList anrede = new ArrayList();
                anrede.Add("");
                anrede.Add("Frau");
                anrede.Add("Herr");
                ddl_Anrede.DataSource = anrede;
                ddl_Anrede.DataBind();

                ArrayList nz = new ArrayList();
                nz.Add("0");
                nz.Add("1");
                nz.Add("2");
                nz.Add("3");
                nz.Add("4");
                nz.Add("5");
                nz.Add("6");
                nz.Add("7");
                nz.Add("8");
                nz.Add("9");
                nz.Add("10");
                ddlItem1.DataSource = nz;
                ddlItem1.DataBind();
                ddlItem2.DataSource = nz;
                ddlItem2.DataBind();
                ddlItem3.DataSource = nz;
                ddlItem3.DataBind();
                ddlItem4.DataSource = nz;
                ddlItem4.DataBind();

                ArrayList title = new ArrayList();
                title.Add("");
                title.Add("Dr.");
                title.Add("OA Dr.");
                title.Add("Dr.Dr.");
                title.Add("Doz.");
                title.Add("Prim.");
                title.Add("Prof.");
                ddlTitel.DataSource = title;
                ddlTitel.DataBind();

                cnConn = DBTools.getConnection();
                cnConn.Open();
                cmdConn = new SqlCommand("dbo.get_orderproducts", cnConn);
                drConn = cmdConn.ExecuteReader();
                string stlbName = "", stddlName = "", sttbName = "", stmvName = "", stvwName = "";

                Label lb;
                DropDownList ddl;
                ContentPlaceHolder cph;
                MultiView mv;
                View vw;
                cph = (ContentPlaceHolder)Master.FindControl("cph_main");
                mv = (MultiView)cph.FindControl("mvProducts");
                iNumProducts = 0;
                while (drConn.Read())
                {
                    if (drConn[stMedRef].ToString() == "Ja")
                    {
                        iNumProducts++;
                        stlbName = "lbItem" + iNumProducts.ToString();
                        stddlName = "ddlItem" + iNumProducts.ToString();
                        sttbName = "tb" + iNumProducts.ToString();
                        stmvName = "mvItem" + iNumProducts.ToString();
                        stvwName = "vwItem" + iNumProducts.ToString();
                        lb = (Label)cph.FindControl(stlbName);
                        ddl = (DropDownList)cph.FindControl(stddlName);
                        mv = (MultiView)cph.FindControl(stmvName);
                        vw = (View)cph.FindControl(stvwName);
                        lb.Text = drConn["produkt"].ToString();
                        lb.Visible = true;
                        ddl.Visible = true;
                        ddl.DataSource = nz;
                        ddl.DataBind();
                        mv.SetActiveView(vw);
                    }
                }
                upProducts.Update();
            }
        }

        protected void cvValddlNotAllNull_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string stddlName;
            DropDownList ddl;
            ContentPlaceHolder cph;
            cph = (ContentPlaceHolder)Master.FindControl("cph_main");
            for (int i = 1; i <= iNumProducts; i++) {
                stddlName = "ddlItem" + i.ToString();
                ddl = (DropDownList)cph.FindControl(stddlName);
                if (ddl.SelectedItem.Text != "0") args.IsValid = true;
            }
        }

        public void bt_save_click(object sender, System.EventArgs e)
        {

            string[] opName = new string[40];
            string[] opNum =  new string[40];

            for (int el = 0; el < opName.GetLength(0); el++) opName[el] = "n/a";
            for (int el = 0; el < opNum.GetLength(0); el++) opNum[el] = "0";
 
            if (Page.IsValid)
            {
                procdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string pd = DateTime.Now.ToString("yyyyMMddHHmmss");
                int patid;

                string stlbName = "", stddlName = "";
                Label lb;
                DropDownList ddl;
                ContentPlaceHolder cph;
                cph = (ContentPlaceHolder)Master.FindControl("cph_main");
                iNp = 0;
                for (int i = 1; i <= iNumProducts; i++)
                {
                    
                    stlbName = "lbItem" + i.ToString();
                    stddlName = "ddlItem" + i.ToString();
                    lb = (Label)cph.FindControl(stlbName);
                    ddl = (DropDownList)cph.FindControl(stddlName);
                    if (ddl.SelectedItem.Text.ToString() != "0")
                    {
                        opName[i] = lb.Text.ToString();
                        opNum[i] = ddl.SelectedItem.Text.ToString();
                        iNp++;
                    }
                }

                patid = Convert.ToInt32(Session["PatID"]);
                stPdf = "_" + Session["PatID"].ToString() + "_" + pd + ".pdf";
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("dbo.insert_order", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", patid));
                cmdConn.Parameters.Add(new SqlParameter("@procdate", procdate));
                cmdConn.Parameters.Add(new SqlParameter("@medikament", stMedRef));
                cmdConn.Parameters.Add(new SqlParameter("@quelle", Session["id"].ToString()));
                string stItem,stItemNum;
                for (int i = 0; i < iNumProductsMax ; i++)
                {
                    iItem = i + 1;
                    stItem = "@item" + iItem.ToString();
                    stItemNum = "@itemnum" + iItem.ToString();
                    cmdConn.Parameters.Add(new SqlParameter(stItem,opName[i]));
                    cmdConn.Parameters.Add(new SqlParameter(stItemNum, opNum[i]));

                }
                cmdConn.Parameters.Add(new SqlParameter("@pdf", stPdf));
                cmdConn.ExecuteNonQuery();
                cnConn.Close();
                cnConn.Open();
                cmdConn = new SqlCommand("dbo.get_orderbyprocdate", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@procdate", procdate));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();
                Session["OrdID"] = drConn["ordid"].ToString();
                cnConn.Close();

                int ordid = Convert.ToInt32(Session["OrdID"]);
                stPdf = "Bestellung_" + Session["OrdID"].ToString() + stPdf;

                if (Request["typ"] == "p")
                {
                    cnConn.Open();
                    cmdConn = new SqlCommand("dbo.update_orderbypdf", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@ordid", ordid));
                    cmdConn.Parameters.Add(new SqlParameter("@pdf", stPdf));
                    cmdConn.ExecuteNonQuery();
                    cnConn.Close();

                }

                if (mvAdressat.GetActiveView() == vwAdressatArzt)
                {
                    stAdrSex = ddl_Anrede.SelectedItem.Text.ToString();
                    stTitel = ddlTitel.SelectedItem.Text.ToString();
                    stAdrName = tbAdrName.Text.ToString();
                    stAdrVorname = tbAdrVorname.Text.ToString();
                    stAdrAdr = tbAdrAdresse.Text.ToString();
                    stAdrPlz = tbAdrPlz.Text.ToString();
                    stAdrOrt = tbAdrOrt.Text.ToString();
                    cnConn.Open();
                    cmdConn = new SqlCommand("dbo.update_ordersarzt", cnConn);
                    cmdConn.CommandType = CommandType.StoredProcedure;
                    cmdConn.Parameters.Add(new SqlParameter("@patid", patid));
                    cmdConn.Parameters.Add(new SqlParameter("@name", stAdrName));
                    cmdConn.Parameters.Add(new SqlParameter("@vorname", stAdrVorname));
                    cmdConn.Parameters.Add(new SqlParameter("@adresse", stAdrAdr));
                    cmdConn.Parameters.Add(new SqlParameter("@plz", stAdrPlz));
                    cmdConn.Parameters.Add(new SqlParameter("@ort", stAdrOrt));
                    cmdConn.Parameters.Add(new SqlParameter("@geschlecht", stAdrSex));
                    cmdConn.Parameters.Add(new SqlParameter("@titel", stTitel));
                    cmdConn.ExecuteNonQuery();
                    cnConn.Close();

                }

                string fina = createPDF();

                string to = "";
                string cc = "";
                string from = "";
                string stEmail = "";

                cnConn.Open();
                cmdConn = new SqlCommand("dbo.get_verteilerORDER", cnConn);
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
                cnConn.Close();

                switch (Session["role"].ToString().Substring(0, 3))
                {
                    case "nrs":
                    case "sec":
                        ArrayList fn = new ArrayList();
                        fn.Add((string)fina);
                        string subject = "PSP Service :: Neue Bestellung";
                        string msgb = "Sehr geehrte Damen und Herren der Rezeption!";
                        subject += "  !!!TEST only!!! - !!!Ignore!!!";
                        msgb += "\n\n!!!TEST only!!! - !!!Ignore!!!\n\n";
                        msgb = String.Concat(msgb, "\nBestellung durch " + Session["name"].ToString() + ", " + Session["forename"].ToString());
                        msgb = String.Concat(msgb, "\n\nDas Bestellformular finden Sie im Anhang.");
                        msgb = String.Concat(msgb, "\n\nMit freundlichen Grüßen\nPSP Service System");
                        MailMessage message = new MailMessage();
                        msdbEmail.SentEmailAttach(stEmail, from, to, cc, subject, msgb, fn);
                        string reurl = "~/general/detailPat.aspx?patid=" + Session["patid"].ToString();
                        Response.Redirect(reurl);
                        break;
                    default:
                        break;
                }
            }
        }

        public void bt_cancel_click(object sender, System.EventArgs e)
        {
            string reurl;
            object referrer = Session["prevpage"];
            if (referrer != null)
            {
                switch (Session["role"].ToString())
                {
                    case "nrs":
                    case "sec":
                        reurl = "~/general/detailpat.aspx?patid=" + Session["patid"].ToString();
                        Response.Redirect(reurl);
                        break;
                    default:
                        break;
                }
            }
        }

        private string createPDF()
        {
            string filename;
            string fontPath, fnt, ft, pdfpath;
            string strCell;
            string txt, img;
            PdfPTable table;

            pdfpath = Server.MapPath("~/pdf_orders");
            filename = pdfpath + "/" + stPdf;

            fontPath = Server.MapPath("~/fnt");
            fnt = System.String.Concat(fontPath, "\\verdana.ttf");
            BaseFont verdana10 = BaseFont.CreateFont(fnt, BaseFont.CP1252, BaseFont.EMBEDDED);
            Font font = new Font(verdana10, 10);

            fnt = System.String.Concat(fontPath, "\\verdanab.ttf");
            BaseFont verdana10b = BaseFont.CreateFont(fnt, BaseFont.CP1252, BaseFont.EMBEDDED);
            Font font10b = new Font(verdana10b, 10);

            fnt = System.String.Concat(fontPath, "\\verdanab.ttf");
            BaseFont verdana12b = BaseFont.CreateFont(fnt, BaseFont.CP1252, BaseFont.EMBEDDED);
            Font font12b = new Font(verdana12b, 12);

            fnt = System.String.Concat(fontPath, "\\verdanab.ttf");
            BaseFont verdana14b = BaseFont.CreateFont(fnt, BaseFont.CP1252, BaseFont.EMBEDDED);
            Font font14b = new Font(verdana14b, 14);

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));


            doc.Open();

            float[] w2 = { 0.15f, 0.85f };
            table = new PdfPTable(w2);
            writeCellLeft(table, "", font);
            strCell = "\r\n";
            writeCellLeft(table, strCell, font10b);
            table.SpacingAfter = 30f;
            table.SpacingBefore = 100f;
            doc.Add(table);

            table = new PdfPTable(w2);
            writeCellLeft(table, "", font);
            strCell = stAdrSex + " " + stTitel + " " + stAdrVorname + " " + stAdrName + "\r\n" + stAdrAdr + "\r\n" + stAdrPlz + " " + stAdrOrt;
            writeCellLeft(table, strCell, font10b);
            table.SpacingAfter = 30f;
            table.SpacingBefore = 100f;
            doc.Add(table);

            table = new PdfPTable(1);
            strCell = "Wien, " + procdate.Substring(0, 10);
            writeCellRight(table, strCell, font);
            table.SpacingAfter = 30f;
            doc.Add(table);

            if (!bArzt)
            {

                if (stAdrSex == "Herr") txt = "Sehr geehrter Herr ";
                else txt = "Sehr geehrte Frau ";
                txt = txt + stPatName + "!";

                table = new PdfPTable(w2);
                writeCellLeft(table, "", font);
                writeCellLeft(table, txt, font);
                table.SpacingAfter = 30f;
                doc.Add(table);

                txt = "Herzlichen Dank für Ihre Bestellung. Gerne übersenden wir Ihnen:";
                table = new PdfPTable(w2);
                writeCellLeft(table, "", font);
                writeCellLeft(table, txt, font);
                table.SpacingAfter = 30f;
                doc.Add(table);
            }
            else
            {
                txt = "Bestellung für ";
                txt = txt + stPatName + ", " + stPatVorname + " | " + stPatPlz + " " + stPatOrt + ", " + stPatAdr;
                table = new PdfPTable(w2);
                writeCellLeft(table, "", font);
                writeCellLeft(table, txt, font10b);
                table.SpacingAfter = 30f;
                doc.Add(table);
            }
            float[] w3 = { 0.15f, 0.15f, 0.70f };
            table = new PdfPTable(w3);
            string stlbName = "", stddlName = "";
            Label lb;
            DropDownList ddl;
            ContentPlaceHolder cph;
            cph = (ContentPlaceHolder)Master.FindControl("cph_main");
            for (int i = 0; i < iNumProducts; i++)
            {
                iItem = i + 1;
                stlbName = "lbItem" + iItem.ToString();
                stddlName = "ddlItem" + iItem.ToString();
                lb = (Label)cph.FindControl(stlbName);
                ddl = (DropDownList)cph.FindControl(stddlName);
                if (ddl.SelectedItem.Text.ToString() != "0")
                {
                    writeCellRight(table, "*", font);
                    writeCellLeft(table, ddl.SelectedItem.Text.ToString() + " Stk", font);
                    writeCellLeft(table, lb.Text.ToString(), font); 
                }
            }

            table.SpacingAfter = 30f;
            doc.Add(table);

            txt = "Mit freundlichen Grüßen\r\n\n\nBiogen Idec Austria GmbH";
            table = new PdfPTable(w2);
            writeCellLeft(table, "", font);
            writeCellLeft(table, txt, font);
            table.SpacingAfter = 100f;
            doc.Add(table);

            doc.Close();
            return (filename);
        }

        private void writeCellLeft(PdfPTable tb, string str, Font ft)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(str, ft));
            cell.HorizontalAlignment = (Element.ALIGN_LEFT);
            cell.VerticalAlignment = (Element.ALIGN_LEFT);
            cell.BorderWidth = 0;
            tb.AddCell(cell);
            tb.WidthPercentage = 100;
            tb.HorizontalAlignment = (Element.ALIGN_LEFT);

        }

        private void writeCellCenter(PdfPTable tb, string str, Font ft)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(str, ft));
            cell.HorizontalAlignment = (Element.ALIGN_CENTER);
            cell.VerticalAlignment = (Element.ALIGN_CENTER);
            cell.BorderWidth = 0;
            tb.AddCell(cell);
            tb.WidthPercentage = 100;
            tb.HorizontalAlignment = (Element.ALIGN_CENTER);

        }
        private void writeCellRight(PdfPTable tb, string str, Font ft)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(str, ft));
            cell.HorizontalAlignment = (Element.ALIGN_RIGHT);
            cell.VerticalAlignment = (Element.ALIGN_RIGHT);
            cell.BorderWidth = 0;
            tb.AddCell(cell);
            tb.WidthPercentage = 100;
            tb.HorizontalAlignment = (Element.ALIGN_RIGHT);

        }

        protected void cbArzt_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (cbArzt.Checked)
            {
                mvAdressat.SetActiveView(vwAdressatArzt);
                upAdressat.Update();
                SqlConnection cnConn = DBTools.getConnection();
                cnConn.Open();
                SqlCommand cmdConn = new SqlCommand("get_orderarztbypatid", cnConn);
                cmdConn.CommandType = CommandType.StoredProcedure;
                cmdConn.Parameters.Add(new SqlParameter("@patid", Session["PatID"]));
                SqlDataReader drConn = cmdConn.ExecuteReader();
                drConn.Read();
                if (drConn.HasRows)
                {
                    tbAdrName.Text = drConn["name"].ToString();
                    tbAdrVorname.Text = drConn["vorname"].ToString();
                    tbAdrAdresse.Text = drConn["adresse"].ToString();
                    tbAdrPlz.Text = drConn["plz"].ToString();
                    tbAdrOrt.Text = drConn["ort"].ToString();
                    switch (drConn["geschlecht"].ToString())
                    {
                        case "Herr":
                            ddl_Anrede.Items[2].Selected = true;
                            ddl_Anrede.Items[1].Selected = false;
                            ddl_Anrede.Items[0].Selected = false;
                            break;
                        case "Frau":
                            ddl_Anrede.Items[2].Selected = false;
                            ddl_Anrede.Items[0].Selected = false;
                            ddl_Anrede.Items[1].Selected = true;
                            break;
                        default:
                            ddl_Anrede.Items[0].Selected = true;
                            ddl_Anrede.Items[1].Selected = false;
                            ddl_Anrede.Items[2].Selected = false;
                            break;
                    }

                    switch (drConn["titel"].ToString())
                    {
                        case "Dr.":
                            ddlTitel.Items[6].Selected = false;
                            ddlTitel.Items[5].Selected = false;
                            ddlTitel.Items[4].Selected = false;
                            ddlTitel.Items[3].Selected = false;
                            ddlTitel.Items[2].Selected = false;
                            ddlTitel.Items[1].Selected = true;
                            ddlTitel.Items[0].Selected = false;
                            break;
                        case "OA Dr.":
                            ddlTitel.Items[6].Selected = false;
                            ddlTitel.Items[5].Selected = false;
                            ddlTitel.Items[4].Selected = false;
                            ddlTitel.Items[3].Selected = false;
                            ddlTitel.Items[2].Selected = true;
                            ddlTitel.Items[1].Selected = false;
                            ddlTitel.Items[0].Selected = false;
                            break;
                        case "Dr.Dr.":
                            ddlTitel.Items[6].Selected = false;
                            ddlTitel.Items[5].Selected = false;
                            ddlTitel.Items[4].Selected = false;
                            ddlTitel.Items[3].Selected = true;
                            ddlTitel.Items[2].Selected = false;
                            ddlTitel.Items[1].Selected = false;
                            ddlTitel.Items[0].Selected = false;
                            break;
                        case "Doz.":
                            ddlTitel.Items[6].Selected = false;
                            ddlTitel.Items[5].Selected = false;
                            ddlTitel.Items[4].Selected = true;
                            ddlTitel.Items[3].Selected = false;
                            ddlTitel.Items[2].Selected = false;
                            ddlTitel.Items[1].Selected = false;
                            ddlTitel.Items[0].Selected = false;
                            break;
                        case "Prim.":
                            ddlTitel.Items[6].Selected = false;
                            ddlTitel.Items[5].Selected = true;
                            ddlTitel.Items[4].Selected = false;
                            ddlTitel.Items[3].Selected = false;
                            ddlTitel.Items[2].Selected = false;
                            ddlTitel.Items[1].Selected = false;
                            ddlTitel.Items[0].Selected = false;
                            break;
                        case "Prof.":
                            ddlTitel.Items[6].Selected = true;
                            ddlTitel.Items[5].Selected = false;
                            ddlTitel.Items[4].Selected = false;
                            ddlTitel.Items[3].Selected = false;
                            ddlTitel.Items[2].Selected = false;
                            ddlTitel.Items[1].Selected = false;
                            ddlTitel.Items[0].Selected = false;
                            break;
                        default:
                            ddlTitel.Items[6].Selected = false;
                            ddlTitel.Items[5].Selected = false;
                            ddlTitel.Items[4].Selected = false;
                            ddlTitel.Items[3].Selected = false;
                            ddlTitel.Items[2].Selected = false;
                            ddlTitel.Items[1].Selected = false;
                            ddlTitel.Items[0].Selected = true;
                            break;
                    }



                }
                drConn.Close();
                cnConn.Close();
                upAdressat.Update();
                bArzt = true;
            }
            else
            {
                mvAdressat.SetActiveView(vwAdressatArztNo);
                bArzt = false;
                upAdressat.Update();
            }

        }
    }
}