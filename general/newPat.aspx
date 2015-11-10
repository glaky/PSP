<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newPat.aspx.cs" Inherits="PSP.general.newPat"
    MasterPageFile="~/PSP.master" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
    <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>

<asp:Content ID="pat_newedit" ContentPlaceHolderID="cph_main" runat="Server">
    <asp:ScriptManager ID="scStamm" runat="server" EnablePartialRendering="true" SupportsPartialRendering="true" />

    <asp:UpdatePanel ID="upPanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:MultiView ID="mvMedInd" runat="server">
                <asp:View ID="vwMedIndNo" runat="server"></asp:View>
                <asp:View ID="vwMedInd" runat="server">
                    <table style="width: 100%" border="0" id="medView" runat="server" class="medindicatort">
                        <tr>
                            <td align="center" class="Text14">
                                <asp:Label runat="server" ID="lbMedIndicator" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="main">
        <asp:MultiView ID="mvMenu" runat="server">
            <asp:View ID="vwNrs" runat="server">
                <!--#include file="~/menue/left_menu_nrs.aspx"-->
            </asp:View>
            <asp:View ID="vwSec" runat="server">
                <!--#include file="~/menue/left_menu_sec.aspx"-->
            </asp:View>
        </asp:MultiView>
        <div id="board">
            <asp:UpdatePanel ID="upPanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 600px" border="0" class="title" id="tabletitle" runat="server">
                        <tr>
                            <td align="left" class="Text14">
                                <asp:MultiView ID="mvTitel" runat="server">
                                    <asp:View ID="vwNewTitel" runat="server">
                                        <asp:Label ID="lbTitle" runat="server" Text="Neuer Patient"></asp:Label>
                                    </asp:View>
                                    <asp:View ID="vwEditTitel" runat="server">
                                        Bearbeitung Patientendaten
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table style="width: 600px" class="verror">
                <tr>
                    <td align="right" class="Text12"></td>
                    <td align="left" valign="top" class="Text12">
                        <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="contact" DisplayMode="BulletList"
                            ID="contactValSum" runat="server" HeaderText="Es wurden fehlende oder fehlerhafte Eingaben entdeckt:" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" style="width: 600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator Style="width: 10px" EnableClientScript="false" ID="rfvPatname"
                            ControlToValidate="tbPatname" runat="server" ValidationGroup="contact" ErrorMessage="Bitte einen Namen eingeben!" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbPatname" runat="server" Text="Name" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbPatname" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvPatvorname" ControlToValidate="tbPatvorname"
                            runat="server" ValidationGroup="contact" ErrorMessage="Bitte einen Vornamen eingeben!" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbPatvorname" runat="server" Text="Vorname" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbPatvorname" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                    <td class="frm_left" valign="middle" align="left" style="width: 200px">
                        <asp:Label ID="lbPatTitel" runat="server" Text="Titel" Style="width: 150px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:DropDownList ID="ddlTitle" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">&nbsp;
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="Label1" runat="server" Text="Geburtsdatum (MM.JJJJ)" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <!--<asp:DropDownList ID="ddl_gebday" runat="server" Style="width: 50px">
                        </asp:DropDownList>-->
                        <asp:DropDownList ID="ddl_gebmonth" runat="server" Style="width: 50px">
                        </asp:DropDownList>
                        .<asp:DropDownList ID="ddl_gebyear" runat="server" Style="width: 88px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="cb_gebdate" runat="server" />&nbsp;unbekannt
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator EnableClientScript="false" CssClass="TextBoldRed" runat="server"
                            ID="rfvrbGeschlecht" ControlToValidate="rbGeschlecht" ValidationGroup="contact"
                            ErrorMessage="Geschlecht: Bitte eine Option wählen" Style="width: 10px">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbGeschlecht" runat="server" Text="Geschlecht" Style="width: 150px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left">
                        <asp:RadioButtonList ID="rbGeschlecht" CssClass="Text12" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Männlich&nbsp;&nbsp;" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Weiblich&nbsp;&nbsp;" Value="W"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">&nbsp;
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbGroesse" runat="server" Text="Größe (in cm)" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:DropDownList ID="ddlGroesse" runat="server" Style="width: 88px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">&nbsp;
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbGewicht" runat="server" Text="Gewicht (in kg)" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:DropDownList ID="ddlGewicht" runat="server" Style="width: 88px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <br />
            <asp:UpdatePanel ID="upPanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" style="width: 600px" class="frm_green_99CC99">
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                            <td class="frm_left" valign="middle" align="left" style="width: 100px">
                                <asp:Label ID="lbArtKontakt" runat="server" Text="Art der Kontaktaufnahme" Style="width: 150px"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left">
                                <asp:DropDownList ID="ddlArtKontakt" runat="server" Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px">
                                <asp:RequiredFieldValidator EnableClientScript="false" CssClass="TextBoldRed" runat="server"
                                    ID="rfvConsent" ControlToValidate="rbConsent" ValidationGroup="contact"
                                    ErrorMessage="Patienteneinwilligung: Bitte eine Option wählen" Style="width: 10px">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="frm_left" valign="middle" align="left" style="width: 200px">
                                <asp:Label ID="Label2" runat="server" Text="Patienteneinwilligung vorhanden?" Style="width: 200px"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left">
                                <asp:RadioButtonList ID="rbConsent" CssClass="Text12" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbConsent_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Ja&nbsp;" Value="Ja"></asp:ListItem>
                                    <asp:ListItem Text="Nein&nbsp;" Value="Nein"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="frm_left" valign="middle" align="left">
                                <asp:CustomValidator Style="width: 10px" ID="cvValddlConsDate" ValidationGroup="contact"
                                    runat="server" OnServerValidate="cvValddlConsDate_ServerValidate" CssClass="TextBoldRed">*</asp:CustomValidator>
                            </td>
                            <td class="frm_left" valign="middle" align="left" style="width: 200px">
                                <asp:Label ID="lbDate" runat="server" Text="Datum Einwilligung"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left" style="width: 300px">
                                <asp:DropDownList ID="ddl_consday" runat="server" Style="width: 50px">
                                </asp:DropDownList>
                                .<asp:DropDownList ID="ddl_consmonth" runat="server" Style="width: 50px">
                                </asp:DropDownList>
                                .<asp:DropDownList ID="ddl_consyear" runat="server" Style="width: 88px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px">
                                <asp:CustomValidator Style="width: 10px" ID="cvValddlConsGetDate" ValidationGroup="contact"
                                    runat="server" OnServerValidate="cvValddlConsGetDate_ServerValidate" CssClass="TextBoldRed">*</asp:CustomValidator>
                            </td>
                            <td class="frm_left" valign="middle" align="left" style="width: 200px">
                                <asp:Label ID="lbConsGet" runat="server" Text="Einwilligung eingelangt am"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left" style="width: 300px">
                                <asp:DropDownList ID="ddl_consgetday" runat="server" Style="width: 50px">
                                </asp:DropDownList>
                                .<asp:DropDownList ID="ddl_consgetmonth" runat="server" Style="width: 50px">
                                </asp:DropDownList>
                                .<asp:DropDownList ID="ddl_consgetyear" runat="server" Style="width: 88px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table border="0" style="width: 600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvAdresse" ControlToValidate="tbAdresse"
                            runat="server" ValidationGroup="contact" ErrorMessage="Bitte eine Adresse eingeben!" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 200px">
                        <asp:Label ID="lbAdresse" runat="server" Text="Adresse" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbAdresse" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvPlz" ControlToValidate="tbPlz"
                            runat="server" ValidationGroup="contact" ErrorMessage="Bitte einen Postleitzahl eingeben!" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 200px">
                        <asp:Label ID="lbPlz" runat="server" Text="Postleitzahl" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbPlz" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvOrt" ControlToValidate="tbOrt"
                            runat="server" ValidationGroup="contact" ErrorMessage="Bitte einen Ort eingeben!" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbOrt" runat="server" Text="Ort" Style="width: 150px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbOrt" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                        <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvTelefon" ControlToValidate="tbPlz"
                            runat="server" ValidationGroup="contact" ErrorMessage="Bitte eine Telefonnummer eingeben!" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbtTel" runat="server" Text="Telefon" Style="width: 150px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbTel" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                    <td class="frm_left" valign="middle" align="left" style="width: 100px">
                        <asp:Label ID="lbEmail" runat="server" Text="E-Mail" Style="width: 150px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbEmail" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" style="width: 600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px">&nbsp;
                    </td>
                    <td class="frm_left" valign="middle" align="left">
                        <asp:Label ID="Label3" runat="server" Text="Jahr der Diagnose" Style="width: 200px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:DropDownList ID="ddl_diagnose" runat="server" Style="width: 200px">
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                    <td class="frm_left" valign="middle" align="left" style="width: 200px">Zuständiges MS-Zentrum
                    </td>
                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                        <asp:TextBox ID="tbZentrum" runat="server" CssClass="text" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>

            </table>
            <br />
            <asp:UpdatePanel ID="upPanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" style="width: 600px" class="frm_green_99CC99" id="tblMed" runat="server">
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px">
                                <asp:RequiredFieldValidator EnableClientScript="false" CssClass="TextBoldRed" runat="server"
                                    ID="rfvMedikament" ControlToValidate="rbMedikament" ValidationGroup="contact"
                                    ErrorMessage="Medikament: Bitte eine Option wählen" Style="width: 10px">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="frm_left" valign="middle" align="left" style="width: 220px">
                                <asp:Label ID="lbMedikament" runat="server" Text="Medikament" Style="width: 200px"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left">
                                <asp:RadioButtonList AutoPostBack="true" ID="rbMedikament" CssClass="Text12" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbMedikament_SelectedIndexChanged">
                                    <asp:ListItem Text="Plegridy&nbsp;&nbsp;" Value="Plegridy"></asp:ListItem>
                                    <asp:ListItem Text="Tecfidera&nbsp;&nbsp;" Value="Tecfidera"></asp:ListItem>
                                    <asp:ListItem Text="Avonex" Value="Avonex"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:MultiView ID="mvMed" runat="server">
                        <asp:View ID="vwMedNo" runat="server"></asp:View>
                        <asp:View ID="vwMed" runat="server">
                            <table border="0" style="width: 600px" class="frmtb_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 200px">
                                        <asp:Label ID="lbTherapie" runat="server" Text="" Style="width: 200px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                                        <asp:DropDownList ID="ddl_tsmonth" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddl_tsyear" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <table border="0" style="width: 600px" class="frmtb_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="top" align="left" style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvVorthe" runat="server" ErrorMessage="Vortherapie: Bitte Option wählen" ValidationGroup="contact"
                                            ControlToValidate="rbVorthe" EnableClientScript="false" CssClass="TextBoldRed">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="top" align="left" style="width: 200px">Vortherapie
                                    </td>
                                    <td class="frm_right" valign="top" align="left" style="width: 300px">
                                        <asp:RadioButtonList ID="rbVorthe" RepeatDirection="Horizontal" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="rbVorthe_SelectedIndexChanged">
                                            <asp:ListItem Text="Ja" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 200px">
                                        <asp:Label ID="Label4" runat="server" Text="" Style="width: 200px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                                        <asp:TextBox ID="tbVorthe" runat="server" TextMode="SingleLine" Width="300px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 200px">Einschulung durch
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                                        <asp:RadioButtonList ID="rbSchule" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Text="MS Zentrum" Value="Zentrum"></asp:ListItem>
                                            <asp:ListItem Text="Nurse Service" Value="Nurse"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" style="width: 600px" class="frmtb_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvZustaendigkeit" runat="server" ControlToValidate="rbZustaendigkeit" ErrorMessage="Zuständigkeit: Bitte eine Option wählen">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 200px">Aktuelle Zuständigkeit
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" style="width: 300px">
                                        <asp:RadioButtonList ID="rbZustaendigkeit" RepeatDirection="Horizontal" runat="server" CausesValidation="True" ValidationGroup="contact">
                                            <asp:ListItem Text="Service Center" Value="Service"></asp:ListItem>
                                            <asp:ListItem Text="Nurse Service" Value="Nurse"></asp:ListItem>
                                        </asp:RadioButtonList>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table border="0" style="width: 600px" class="frmt_green_99CC99">
                <tr>
                    <td class="frm_left" valign="middle" align="left" width="10px"></td>
                    <td class="frm_left" valign="top" align="left" width="200px">Injektionstag
                    </td>
                    <td class="frm_right" valign="top" align="left" width="300px">
                        <asp:DropDownList ID="ddlInjektionstag" runat="server" Style="width: 200px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <br />
            <asp:UpdatePanel ID="upDummy1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" style="width: 600px" class="frm_green_99CC99">
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                            <td class="frm_left" valign="top" align="left" style="width: 200px">Anrufintervall
                            </td>
                            <td class="frm_right" valign="top" align="left" style="width: 300px">
                                <asp:DropDownList ID="ddlAnrufIntervall" runat="server" Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                            <td class="frm_left" valign="top" align="left" style="width: 200px">Erreichbarkeit
                            </td>
                            <td class="frm_right" valign="top" align="left" style="width: 300px">
                                <asp:DropDownList ID="ddl_errei" runat="server" Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="frm_left" valign="middle" align="left" style="width: 10px"></td>
                            <td class="frm_left" valign="top" align="left" style="width: 200px">Anonym gegenüber Dritten
                            </td>
                            <td class="frm_right" valign="top" align="left" style="width: 300px">
                                <asp:RadioButtonList ID="rbAnonym" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">keine Angabe</asp:ListItem>
                                    <asp:ListItem>Ja</asp:ListItem>
                                    <asp:ListItem>Nein</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table border="0" style="width: 600px">
                <tr align="center">
                    <td class="Text12" style="width: 300px" align="right">
                        <asp:Button OnClick="bt_save_click" ID="btSave" runat="server" CssClass="button"
                            Text="Speichern" Style="width: 190px" ValidationGroup="contact" CausesValidation="true" />&nbsp;
                    </td>
                    <td class="Text12" style="width: 300px" align="left">
                        <asp:Button ID="btCancel" runat="server" CssClass="button" Text="Abbrechen" Style="width: 190px"
                            OnClick="bt_cancel_click" />&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        &nbsp;
        <asp:UpdatePanel ID="upPanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:MultiView ID="mvMedInd1" runat="server">
                    <asp:View ID="vwMedInd1No" runat="server"></asp:View>
                    <asp:View ID="vwMedInd1" runat="server">
                        <table style="width: 100%" border="0" id="medView1" runat="server" class="medindicatorb">
                            <tr>
                                <td align="center" class="Text14">
                                    <asp:Label runat="server" ID="lbMedIndicator1" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
