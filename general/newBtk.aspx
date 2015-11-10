<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newBtk.aspx.cs" Inherits="PSP.general.newBtk"
    MasterPageFile="~/PSP.master" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="showPatBrief.ascx" TagName="brief" TagPrefix="pat" %>
<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
    <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="ep_dks" ContentPlaceHolderID="cph_main" runat="Server">
    <asp:ScriptManager ID="scStatus" runat="server" EnablePartialRendering="true" SupportsPartialRendering="true" />
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
            <asp:View ID="vwAss" runat="server">
                <!--#include file="~/menue/left_menu_ass.aspx"-->
            </asp:View>
        </asp:MultiView>
        <div id="board">
            <table width="600px" border="0" class="title">
                <tr>
                    <td align="left" class="Text14">
                        <asp:Label ID="lbTitle" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <pat:brief ID="show_pat_brief" runat="server" />
            <br />
            <table border="0" width="600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                    </td>
                    <td class="frm_left_white" valign="middle" align="left">
                        <asp:Label ID="lbKontaktV" runat="server"></asp:Label>
                        <asp:Label ID="error" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btDefault" runat="server" Text="Werte übernehmen" CssClass="button"
                            Width="200px" OnClick="btDefault_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:UpdatePanel ID="upVds" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="600px" class="verror">
                        <tr>
                            <td align="right" class="Text12"></td>
                            <td align="left" valign="top" class="Text12">
                                <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="nkp_plaus" DisplayMode="BulletList"
                                    ID="nkpValSum" runat="server" HeaderText="Es wurden fehlerhafte Eingaben entdeckt:" />
                                <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="nkp_medan" DisplayMode="BulletList"
                                    ID="nkpMedanSum" runat="server" HeaderText="" />
                                <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="nkp_complete" DisplayMode="BulletList"
                                    ID="nkp_completeValSum" runat="server" HeaderText="Es wurden fehlende Eingaben entdeckt:" />
                                <asp:Label ID="lbsaved" runat="server" Text="" CssClass="Text12b"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upNCom" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvNCom" runat="server">
                        <asp:View ID="vwNComNo" runat="server">
                            <br />
                        </asp:View>
                        <asp:View ID="vwNCom" runat="server">
                            <br />
                            <table width="600px" class="frm_reen_99CC99">
                                <tr>
                                    <td align="right" class="Text12"></td>
                                    <td align="left" valign="top" class="Text12b">Wollen Sie den Betreuungskontakt trotzdem abschließen?
                                    </td>
                                    <td align="left" valign="top" class="Text12b">
                                        <asp:Button ID="btComplete" runat="server" Text="Ja" CssClass="button" Width="150px"
                                            OnClick="bt_save_click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="600px" border="0" class="frmb_green_99CC99">
                <tr>
                    <td class="frm_left" width="10px">
                        <asp:CustomValidator ID="cvValddlDate" ValidationGroup="nkp_plaus" runat="server"
                            OnServerValidate="cvValddlDate_ServerValidate">*</asp:CustomValidator>&nbsp;
                    </td>
                    <td class="frm_left" width="200px">
                        <asp:Label ID="lbDate" runat="server" Text="Datum des Kontakts" Width="130px"></asp:Label>
                    </td>
                    <td width="270px">
                        <asp:DropDownList ID="ddl_procday" runat="server">
                        </asp:DropDownList>
                        .<asp:DropDownList ID="ddl_procmonth" runat="server">
                        </asp:DropDownList>
                        .<asp:DropDownList ID="ddl_procyear" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="frm_right" width="100px">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="frm_left" width="10px">
                        <asp:CustomValidator ID="cvValddlArt" ValidationGroup="nkp_complete" runat="server"
                            OnServerValidate="cvValddlArt_ServerValidate" EnableClientScript="False">*</asp:CustomValidator>&nbsp;
                    </td>
                    <td class="frm_left" width="220px">
                        <asp:Label ID="lbArt" runat="server" Text="Art des Kontakts" Width="130px"></asp:Label>
                    </td>
                    <td class="frm_right" width="270px">
                        <asp:DropDownList AutoPostBack="true" ID="ddl_art" runat="server" OnSelectedIndexChanged="ddl_art_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="frm_right" width="100px">&nbsp;
                    </td>
                </tr>
            </table>
            <table width="600px" class="frmt_green_99CC99" border="0">
                <tr>
                    <td class="frm_left" width="10px">&nbsp;
                    </td>
                    <td class="frm_left" width="220px">
                        <asp:Label ID="lbStatus" runat="server" Text="Status der Behandlung" Width="130px"></asp:Label>
                    </td>
                    <td class="frm_right" width="270px">
                        <asp:DropDownList ID="ddl_status" AutoPostBack="true" runat="server" Width="200px"
                            OnSelectedIndexChanged="ddl_status_selectedindexchanged">
                        </asp:DropDownList>
                    </td>
                    <td class="frm_right" width="100px">&nbsp;
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="upStatusDatum" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvStatusDatum" runat="server">
                        <asp:View ID="vwStatusDatumNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwStatusDatum" runat="server">
                            <table width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" width="30px">
                                        <asp:CustomValidator ID="cvStatusDatumPlaus" ValidationGroup="nkp_plaus" runat="server"
                                            OnServerValidate="cvValddlStatusDatum_ServerValidate">*</asp:CustomValidator>
                                        <asp:CustomValidator ID="cvStatusDatum" ValidationGroup="nkp_complete" runat="server"
                                            OnServerValidate="cvValddlStatusDatum_ServerValidate">*</asp:CustomValidator>
                                    </td>
                                    <td class="frm_left" width="200px">
                                        <asp:Label ID="Label24" runat="server" Text="Datum:&nbsp;" Width="145px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:DropDownList ID="ddl_statday" runat="server">
                                        </asp:DropDownList>
                                        .<asp:DropDownList ID="ddl_statmonth" runat="server">
                                        </asp:DropDownList>
                                        .<asp:DropDownList ID="ddl_statyear" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="frm_right" width="100px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upStatus" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvStatusGrund" runat="server">
                        <asp:View ID="vwStatusGrundNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwStatusGrund" runat="server">
                            <table width="600px" class="frmt_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="30px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="200px">
                                        <asp:Label ID="Label3" runat="server" Text="Grund:" Width="110px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:TextBox ID="tbStatusGrund" runat="server" Width="145px"></asp:TextBox>
                                    </td>
                                    <td class="frm_right" width="100px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upStatusThewe" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvStatusThewe" runat="server">
                        <asp:View ID="vwStatusTheweNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwStatusThewe" runat="server">
                            <table width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" width="30px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="200px">
                                        <asp:Label ID="lb01" runat="server" Text="Therapiewechsel:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:RadioButtonList ID="rbStatusThewe" runat="server" CssClass="Text12" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rbStatusThewe_SelectedIndexChanged">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_right" width="100px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upTheweMed" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvTheweMed" runat="server">
                        <asp:View ID="vwTheweMedNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwTheweMed" runat="server">
                            <table width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" width="30px">&nbsp;</td>
                                    <td class="frm_left" width="220px">
                                        <asp:Label ID="lb02" runat="server" Text="Präparat:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="350px">
                                        <asp:DropDownList ID="ddlTheweMed" AutoPostBack="true" runat="server" Width="200px"
                                            OnSelectedIndexChanged="ddlTheweMed_selectedindexchanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="frm_right" width="10px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" width="30px">&nbsp;</td>
                                    <td class="frm_left" width="220px">
                                        <asp:Label ID="lb03" runat="server" Text="&nbsp;" Width="110px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="350px">
                                        <asp:TextBox ID="tbTheweMedAndere" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                                    </td>
                                    <td class="frm_right" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upMed" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvMedFLS" runat="server">
                        <asp:View ID="vwMedFLSNo" runat="server"></asp:View>
                        <asp:View ID="vwMedFLS" runat="server">
                            <br />
                            <table width="600px" class="frm_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="220px">
                                        <asp:Label ID="lbFLS" runat="server" Text="FLS-Prophylaxe"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:RadioButtonList ID="rbFLS" runat="server" CssClass="Text12" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rbFLS_SelectedIndexChanged">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_right" width="100px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvFLS" runat="server">
                        <asp:View ID="vwFLSNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwFLS" runat="server">
                            <table width="600px" class="frmt_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="225px">
                                        <asp:Label ID="Label1" runat="server" Text="FLS-Prophylaxe" Visible="false"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:CheckBox ID="cbMexalen" runat="server" Text="Mexalen" /><br />
                                        <asp:CheckBox ID="cbNaproxen" runat="server" Text="Naproxen" /><br />
                                        <asp:CheckBox ID="cbIbuprofen" runat="server" Text="Ibuprofen" /><br />
                                        <asp:CheckBox AutoPostBack="true" ID="cbAndere" runat="server" Text="Andere" OnCheckedChanged="cbAndere_OnCheckedChanged" />&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbAndere" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="frm_right" width="95px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvMedHaut" runat="server">
                        <asp:View ID="vwMedHautNo" runat="server"></asp:View>
                        <asp:View ID="vwMedHaut" runat="server">
                            <table width="600px" class="frmt_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="220px">
                                        <asp:Label ID="lbHaut" runat="server" Text="Hautprohylaxe"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:RadioButtonList ID="rbHaut" runat="server" CssClass="Text12" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rbHaut_SelectedIndexChanged">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_right" width="100px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvHaut" runat="server">
                        <asp:View ID="vwHautNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwHaut" runat="server">
                            <table width="600px" class="frmt_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="225px">
                                        <asp:Label ID="Label2" runat="server" Text="Hauptprophylaxe" Visible="false"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="270px">
                                        <asp:CheckBox ID="cbCoolpack" runat="server" Text="Coolpack" /><br />
                                        <asp:CheckBox AutoPostBack="true" ID="cbHautAndere" runat="server" Text="Andere" OnCheckedChanged="cbHautAndere_OnCheckedChanged" />&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbHautAndere" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="frm_right" width="95px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvMedGastroFlush" runat="server">
                        <asp:View ID="vwMedGastroFlushNo" runat="server"></asp:View>
                        <asp:View ID="vwMedGastroFlush" runat="server">
                            <br />
                            <table width="600px" class="frm_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="220px">
                                        <asp:Label ID="lbGastro" runat="server" Text="Gastrointerstinale NW" Width="130px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="120px">
                                        <asp:RadioButtonList ID="rbGastro" runat="server" CssClass="Text12" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rbGastro_SelectedIndexChanged">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="frm_right" width="250px">
                                        <asp:TextBox ID="tbGastro" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="600px" class="frmt_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px" valign="middle">&nbsp;
                                    </td>
                                    <td class="frm_left" width="220px">
                                        <asp:Label ID="lbFlush" runat="server" Text="Flush" Width="130px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="120px" valign="middle">
                                        <asp:RadioButtonList ID="rbFlush" runat="server" CssClass="Text12" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rbFlush_SelectedIndexChanged">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="frm_left" width="250px" valign="middle">
                                        <asp:TextBox ID="tbFlush" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="upInjek" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvInjek" runat="server">
                        <asp:View ID="vwInjekNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwInjek" runat="server">
                            <br />
                            <table border="0" width="600px" class="frm_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvInjek" runat="server" ErrorMessage="Injektion: Bitte einen Typ wählen!"
                                            ControlToValidate="rbBioFesp" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label12" runat="server" Width="160px">Injektion</asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="270px">
                                        <asp:RadioButtonList ID="rbBioFeSp" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="injek_onselectindexchanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="Pen" Value="Pen"></asp:ListItem>
                                            <asp:ListItem Text="Fertigspitze" Value="Fertigspritze"></asp:ListItem>
                                            <asp:ListItem Text="Bioset" Value="Bioset"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left">
                                        <asp:Label ID="Label4" runat="server" Width="100px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:MultiView ID="mvNL" runat="server">
                                <asp:View ID="vwNoNL" runat="server">
                                </asp:View>
                                <asp:View ID="vwNL" runat="server">
                                    <table border="0" width="600px" class="frmt_green_99CC99">
                                        <tr>
                                            <td class="frm_left" width="10px">&nbsp;
                                            </td>
                                            <td class="frm_left" valign="top" align="left" width="220px">
                                                <asp:Label ID="Label27" runat="server" Text="Nadell&auml;nge" Width="160px"></asp:Label>
                                            </td>
                                            <td class="frm_right" valign="top" align="left" width="270px">
                                                <asp:RadioButtonList AutoPostBack="true" ID="rbNaLa" runat="server" CssClass="Text12"
                                                    RepeatDirection="Vertical" OnSelectedIndexChanged="nala_onselectindexchanged">
                                                    <asp:ListItem Text="Standard&nbsp;&nbsp;" Value="Standard"></asp:ListItem>
                                                    <asp:ListItem Text="Lang&nbsp;&nbsp;" Value="Lang"></asp:ListItem>
                                                    <asp:ListItem Text="Kurz" Value="Kurz"></asp:ListItem>
                                                    <asp:ListItem Text="Keine Angabe" Value="Keine Angabe"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td class="frm_left" width="100px">
                                                <asp:Label ID="Label10" runat="server" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                            </table>
                    <asp:MultiView ID="mvAA" runat="server">
                        <asp:View ID="vwNoAA" runat="server">
                        </asp:View>
                        <asp:View ID="vwAA" runat="server">
                            <table border="0" width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" valign="middle" align="center" width="590px" colspan="3">
                                        <asp:Label ID="Label14" runat="server" Text="Information zur Anweisung der Nadellänge"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvAA" runat="server" ErrorMessage="Arztanweisung: Bitte Namen des Arztes angeben"
                                            ControlToValidate="tbAA" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label13" runat="server" Text="Name des Arztes" Width="200px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="360px">
                                        <asp:TextBox ID="tbAA" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="frm_left" width="10px">
                                        <asp:Label ID="Label5" runat="server" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:CustomValidator ID="cvAADatumPlaus" ValidationGroup="nkp_plaus" runat="server"
                                            OnServerValidate="cvAADatum_ServerValidate">*</asp:CustomValidator>
                                        <asp:CustomValidator ID="cvAADatum" ValidationGroup="nkp_complete" runat="server"
                                            OnServerValidate="cvAADatum_ServerValidate">*</asp:CustomValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label61" runat="server" Text="Datum" Width="200px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="360px">
                                        <asp:DropDownList ID="ddl_AAday" runat="server" Width="55px">
                                        </asp:DropDownList>
                                        .<asp:DropDownList ID="ddl_AAmonth" runat="server" Width="55px">
                                        </asp:DropDownList>
                                        .<asp:DropDownList ID="ddl_AAyear" runat="server" Width="83px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="frm_left" width="10px">
                                        <asp:Label ID="Label6" runat="server" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvAAComment" runat="server" ErrorMessage="Kommentar Arztanweisung: Keine Abgabe"
                                            ControlToValidate="tbAAComment" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label63" runat="server" Text="Kommentar" Width="200px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="360px">
                                        <asp:TextBox ID="tbAAComment" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="frm_left" width="10px">
                                        <asp:Label ID="Label7" runat="server" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="upInjekOrt" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvInjekOrt" runat="server">
                        <asp:View ID="vwInjekOrtNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwInjekOrt" runat="server">
                            <br />
                            <table border="0" width="600px" class="frm_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvInjekOrt" runat="server" ErrorMessage="Injektionsort: Bitte eine Option wählen!"
                                            ControlToValidate="rbInjekOrt" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="top" align="left" width="220px">
                                        <asp:Label ID="Label44" runat="server" Width="160px" Text="Injektionsort"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="270px">
                                        <asp:RadioButtonList ID="rbInjekOrt" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Oberschenkel" Text="Oberschenkel"></asp:ListItem>
                                            <asp:ListItem Value="Bauch" Text="Bauch"></asp:ListItem>
                                            <asp:ListItem Value="Oberarm" Text="Oberarm"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="100px">
                                        <asp:Label ID="Label45" runat="server" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upAvoject" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvAvoject" runat="server">
                        <asp:View ID="vwAvojectNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwAvoject" runat="server">
                            <table border="0" width="600px" class="frm_green_99CC99">
                                <br />
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvAvoject" runat="server" ErrorMessage="Avoject: Bitte eine Option wählen!"
                                            ControlToValidate="rbAvoject" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label46" runat="server" Width="160px" Text="Avoject"></asp:Label>
                                    </td>

                                    <td class="frm_right" valign="middle" align="left" width="350px">
                                        <asp:RadioButtonList ID="rbAvoject" CssClass="Text12" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein&nbsp;&nbsp;" Value="Nein" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="upTitDos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvTitDos" runat="server">
                        <asp:View ID="vwDos" runat="server">
                            <br />
                            <table border="0" width="600px" class="frm_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvTitDos" runat="server" ErrorMessage="Dosis: Bitte eine Option wählen!"
                                            ControlToValidate="rbDos" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label8" runat="server" Width="160px">Aktuelle Dosierung</asp:Label>
                                    </td>

                                    <td class="frm_right" valign="middle" align="left" width="350px">
                                        <asp:RadioButtonList ID="rbDos" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbDos_onselectindexchanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="120" Value="120"></asp:ListItem>
                                            <asp:ListItem Text="240" Value="240"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="vwTit" runat="server">
                            <br />
                            <table border="0" width="600px" class="frm_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvTit" runat="server" ErrorMessage="Titration: Bitte eine Option wählen!"
                                            ControlToValidate="rbTitra" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="lbTit1" runat="server" Width="160px">Titration</asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="350px">
                                        <asp:RadioButtonList ID="rbTitra" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtit_onselectindexchanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="Ja" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvTitra" runat="server">
                        <asp:View ID="vwTitraNo" runat="server"></asp:View>
                        <asp:View ID="vwTitraAvonex" runat="server">
                            <table border="0" width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvTitraAvonex" runat="server" ErrorMessage="Art der Titration: Bitte eine Option wählen!"
                                            ControlToValidate="rbTitraArt" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label29" runat="server" Text="Wochen:" Width="160px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="350px">
                                        <asp:DropDownList ID="ddl_titra" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label221" runat="server" Text="Art der Titration:" Width="160px"></asp:Label>
                                    </td>

                                    <td class="frm_right" valign="middle" align="left" width="350px">
                                        <asp:RadioButtonList ID="rbTitraArt" CssClass="Text12" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="1/4&nbsp;&nbsp;" Value="1/4"></asp:ListItem>
                                            <asp:ListItem Text="1/2&nbsp;&nbsp;" Value="1/2"></asp:ListItem>
                                            <asp:ListItem Text="3/4&nbsp;&nbsp;" Value="3/4"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="vwTitraPlegridy" runat="server">
                            <table border="0" width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">
                                        <asp:RequiredFieldValidator ID="rfvTitraPlegridy" runat="server" ErrorMessage="Art der Titration: Bitte eine Option wählen!"
                                            ControlToValidate="rbTitraPlegridy" ValidationGroup="nkp_complete">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label9" runat="server" Width="160px">Art der Titration</asp:Label>
                                    </td>

                                    <td class="frm_right" valign="middle" align="left" width="350px">
                                        <asp:RadioButtonList ID="rbTitraPlegridy" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbTitraPlegridy_onselectindexchanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="63" Value="63"></asp:ListItem>
                                            <asp:ListItem Text="94" Value="94"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="upAngsSchule" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvAngSchule" runat="server">
                        <asp:View ID="vwAngSchuleNo" runat="server"></asp:View>
                        <asp:View ID="vwAngSchule" runat="server">
                            <br />
                            <table border="0" width="600px" class="frm_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" width="220px">
                                        <asp:Label ID="Label16" runat="server" Text="Angeh&ouml;rigenschulung" Width="160px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left">
                                        <asp:CheckBox ID="cbAngschule" runat="server" />
                                    </td>
                                    <td class="frm_right" valign="middle" align="left">
                                        <asp:TextBox ID="tbAngschule" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:UpdatePanel ID="upBMI" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" width="600px" class="frm_green_99CC99">
                        <tr>
                            <td class="frm_left" valign="middle" align="left" width="10px">
                                <asp:RangeValidator EnableClientScript="false" ValidationGroup="nkp_plaus" ID="ravGewicht"
                                    Type="Integer" MinimumValue="30" MaximumValue="300" ControlToValidate="tbCurMass"
                                    runat="server" ErrorMessage="Gewicht: Nur Zahlen zwischen 30 und 300">*</asp:RangeValidator>
                            </td>
                            <td class="frm_left" valign="middle">
                                <asp:Label ID="Label43" runat="server" Text="Aktuelles Gewicht in kg" Width="220px"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left" width="350px">
                                <asp:TextBox ID="tbCurMass" runat="server" Width="50px" AutoPostBack="true" OnTextChanged="tbCurMass_TextChanged"></asp:TextBox>&nbsp;&nbsp;
                                BMI:&nbsp;
                                <asp:Label ID="lbBMI" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:UpdatePanel ID="upSpontan" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" width="600px" class="frm_green_99CC99">
                        <tr>
                            <td class="frm_left" valign="top" align="left" width="10px">
                                <asp:RequiredFieldValidator CssClass="TextBoldRed" runat="server" ID="rfvrbNw" ControlToValidate="rbNw"
                                    ValidationGroup="nkp_complete" ErrorMessage="Nebenwirkungen: Bitte eine Option wählen"
                                    Width="10px" EnableClientScript="False">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="frm_left" valign="top" align="left" width="220px">
                                <asp:Label ID="lbNw" runat="server" Text="Nebenwirkungen" Width="220px"></asp:Label>
                            </td>
                            <td class="frm_right" valign="middle" align="left" width="350px">
                                <asp:RadioButtonList ID="rbNw" CssClass="Text12" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbNw_SelectedIndexChanged">
                                    <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                    <asp:ListItem Text="Nein&nbsp;&nbsp;" Value="Nein"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="cbSpontan" runat="server" CssClass="Text12" Text="Spontanmeldung" />
                            </td>
                            <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upNw" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvNw" runat="server">
                        <asp:View ID="vwNoNw" runat="server">
                        </asp:View>
                        <asp:View ID="vwNw" runat="server">
                            <table border="0" width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="top" align="left" width="10px">
                                        <asp:RequiredFieldValidator CssClass="TextBoldRed" runat="server" ID="RequiredFieldValidator2"
                                            ControlToValidate="tbNw" ValidationGroup="nkp_complete" ErrorMessage="Nebenwirkungen: Bitte beschreiben"
                                            Width="10px" EnableClientScript="False">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="frm_left" valign="top" align="left" width="220px">
                                        <asp:Label ID="Label40" runat="server" Text="Nebenwirkungen Beschreibung" Width="205px"></asp:Label>
                                    </td>
                                    <td align="left" class="frm_left" valign="middle" width="350px">
                                        <asp:TextBox ID="tbNw" runat="server" CssClass="text" Width="320px" TextMode="MultiLine"
                                            Rows="10"></asp:TextBox>
                                    </td>
                                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table border="0" width="600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="top" align="left" width="10px">
                        <asp:RequiredFieldValidator ID="rfvMedan" runat="server" ErrorMessage="Medizinische Anfrage: Bitte eine Option wählen"
                            ValidationGroup="nkp_complete" ControlToValidate="rbMedan">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="frm_left" valign="top" align="left" width="220px">
                        <asp:Label ID="Label49" runat="server" Text="Medizinische Anfrage" Width="220px"></asp:Label>
                    </td>
                    <td class="frm_right" valign="top" align="left" width="350px">
                        <asp:RadioButtonList ID="rbMedan" CssClass="Text12" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rbMedan_SelectedIndexChanged">
                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                            <asp:ListItem Text="Nein&nbsp;&nbsp;" Value="Nein"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="upMedan" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvMedan" runat="server">
                        <asp:View ID="vwMedanNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwMedan" runat="server">
                            <table border="0" width="600px" class="frmt_green_99CC99">
                                <tr>
                                    <td class="frm_left" valign="top" align="left" width="10px">
                                        <asp:CustomValidator ID="cvMedan" runat="server" ErrorMessage="Medizinische Anfrage: Bitte beschreiben!" ValidationGroup="nkp_complete"
                                            ControlToValidate="rbMedan" OnServerValidate="cvMedan_ServerValidate">*</asp:CustomValidator>
                                    </td>
                                    <td class="frm_left" valign="top" align="left" width="220px">
                                        <asp:CheckBox ID="cbMedanComplete" runat="server" Text="Anfrage erledigt&nbsp;&nbsp;"
                                            Width="210px" TextAlign="Left" CausesValidation="true" />
                                    </td>
                                    <td class="frm_right" valign="top" align="left" width="350px">
                                        <asp:CheckBox ID="cbMedan1" runat="server" Text="Thema1" Visible="false" />
                                    </td>
                                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="frm_left" valign="top" width="10px"></td>
                                    <td align="left" class="frm_left" valign="top" width="220px">
                                        <asp:Label ID="Label37" runat="server" Text="Beschreibung Medizinsche Anfrage" Width="210px"></asp:Label>
                                    </td>
                                    <td width="350px" align="left" class="frm_right" valign="top">
                                        <asp:TextBox ID="tbMedan" runat="server" Rows="5" TextMode="MultiLine" Width="320px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="frm_left" valign="middle" width="10px">&nbsp; </td>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table border="0" width="600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="top" align="left" width="10px"></td>
                    <td class="frm_left" valign="top" align="left" width="220px">
                        <asp:Label ID="Label56" runat="server" Text="Behandlungsverlauf" Width="205px"></asp:Label>
                    </td>
                    <td align="left" class="frm_left" valign="middle" width="350px">
                        <asp:TextBox ID="tbBhv" runat="server" CssClass="text" Width="320px" TextMode="MultiLine"
                            Rows="10"></asp:TextBox>
                    </td>
                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="upNurseKontakt" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <asp:MultiView ID="mvNrsKontakt" runat="server">
                        <asp:View ID="vwNrsKontaktNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwNrsKontakt" runat="server">
                            <table width="600px" class="frm_green_99CC99" border="0">
                                <tr>
                                    <td class="frm_left" width="10px">&nbsp;
                                    </td>
                                    <td class="frm_left" width="350px">
                                        <asp:Label ID="lbNurseKontakt" runat="server" Text="Kontakt durch Nurse Service" Width="130px"></asp:Label>
                                    </td>
                                    <td class="frm_right" width="220px">
                                        <asp:RadioButtonList ID="rbNurseKontakt" runat="server" CssClass="Text12" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                                            <asp:ListItem Text="Nein" Value="Nein" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="frm_right" width="20px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="600px" class="frm_green_99CC99" border="0">
                <tr>
                    <td class="frm_left" width="10px">&nbsp;
                    </td>
                    <td class="frm_left" width="350px">
                        <asp:Label ID="lbZustaendigkeit" runat="server" Text="Zustaendigkeit ändern auf " Width="130px"></asp:Label>
                    </td>
                    <td class="frm_right" width="220px">
                        <asp:RadioButtonList ID="rbZustaendigkeit" runat="server" CssClass="Text12" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="Ja"></asp:ListItem>
                            <asp:ListItem Text="Nein" Value="Service" Selected="Nein"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="frm_right" width="20px">&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" width="600px" class="frm_green_99CC99">
                <tr>
                    <td class="frm_left" valign="middle" align="left" width="10px">
                        <asp:CustomValidator ID="cvNekoDatum" ValidationGroup="nkp_plaus" runat="server"
                            OnServerValidate="cvValddlNekoDatum_ServerValidate">*</asp:CustomValidator>
                    </td>
                    <td class="frm_left" valign="top" align="left" width="220px">
                        <asp:Label ID="Label38" runat="server" Text="Nächster Kontakt" class="Text12b"></asp:Label><br />
                    </td>
                    <td class="frm_right" valign="middle" align="left" width="350px">
                        <asp:DropDownList ID="ddl_nekoday" runat="server" Width="55px">
                        </asp:DropDownList>
                        .<asp:DropDownList ID="ddl_nekomonth" runat="server" Width="55px">
                        </asp:DropDownList>
                        .<asp:DropDownList ID="ddl_nekoyear" runat="server" Width="76px">
                        </asp:DropDownList>
                    </td>
                    <td class="frm_left" valign="middle" align="right" width="10px">&nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <asp:UpdatePanel ID="upButtons" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" width="600px">
                        <tr align="center">
                            <td class="Text12" width="300px" align="right">
                                <asp:Button OnClick="bt_save_click" ID="btSave" runat="server" CssClass="button"
                                    Text="Speichern" Width="190px" />&nbsp;
                            </td>
                            <td class="Text12" width="300px" align="left">
                                <asp:Button ID="btCancel" runat="server" CssClass="button" Text="Abbrechen" Width="190px"
                                    OnClick="bt_cancel_click" />&nbsp;
                            </td>
                            <td class="Text12" width="300px" align="left">
                                <asp:Button ID="btFinish" runat="server" CssClass="button" Text="Abschließen" Width="190px"
                                    OnClick="bt_save_click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
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
