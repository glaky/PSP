<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newOrder.aspx.cs" Inherits="PSP.general.orderPat" MasterPageFile="~/PSP.master" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="showPatBrief.ascx" TagName="show_pat_brief" TagPrefix="uc1" %>
<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
    <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbSurname" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="nk_rez" ContentPlaceHolderID="cph_main" runat="Server">
    <asp:ScriptManager ID="scOrder" runat="server" EnablePartialRendering="true" SupportsPartialRendering="true" />
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
            <table width="600px" border="0" class="title">
                <tr>
                    <td align="left" class="Text14">
                        <asp:Label ID="lbTitle" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:CheckBox ID="cbArzt" runat="server" AutoPostBack="true" Text="  Versand an Arzt"
                            OnCheckedChanged="cbArzt_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="600px" class="verror">
                <tr>
                    <td align="right" class="Text12"></td>
                    <td align="left" valign="top" class="Text12">
                        <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="order" DisplayMode="BulletList"
                            ID="orderValSum" runat="server" HeaderText="Es wurden fehlende oder fehlerhafte Eingaben entdeckt:" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:UpdatePanel ID="upAdressat" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mvAdressat" runat="server">
                        <asp:View ID="vwAdressatArztNo" runat="server">
                        </asp:View>
                        <asp:View ID="vwAdressatArzt" runat="server">
                            <table border="0" width="600px" class="frm_purple">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left">
                                        <asp:Label ID="lbAnon" runat="server" CssClass="Text12b" Text="Bestellung über Arzt"></asp:Label><br />
                                        <asp:Label ID="lbAnon2" runat="server" CssClass="Text12" Text="Daten des adressierten Arztes werden gespeichert, eventuell vorhandene Information dabei überschrieben!"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table border="0" width="600px" class="frm_blue">
                                <tr>
                                    <td>&nbsp;<asp:RequiredFieldValidator EnableClientScript="false" ID="rfvAnrede" runat="server"
                                        ErrorMessage="Bitte eine Anrede wählen" CssClass="TextBoldRed" ControlToValidate="ddl_Anrede"
                                        ValidationGroup="order">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="lbAnrede" runat="server" Text="Anrede:&nbsp;" CssClass="Text12b" Width="60px"></asp:Label>
                                        <asp:DropDownList ID="ddl_Anrede" runat="server" Width="50px">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:Label ID="lbTitelus" runat="server" Text="Titel:&nbsp;" CssClass="Text12b" Width="40px"></asp:Label>
                                        <asp:DropDownList ID="ddlTitel" runat="server" Width="60px">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator EnableClientScript="false" ID="rfvPatName" runat="server"
                                            ErrorMessage="Bitte einen Namen eingeben" CssClass="TextBoldRed" ControlToValidate="tbAdrName"
                                            ValidationGroup="order">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="lbAdrName" runat="server" Text="Name:&nbsp;" CssClass="Text12b"></asp:Label>
                                        <asp:RequiredFieldValidator EnableClientScript="false" ID="RequiredFieldValidator1"
                                            runat="server" ErrorMessage="Bitte einen Vornamen eingeben" CssClass="TextBoldRed"
                                            ControlToValidate="tbAdrVorname" ValidationGroup="order">*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="tbAdrName" runat="server" Width="100px"></asp:TextBox>
                                        &nbsp;
                                        <asp:Label ID="lbAdrVorname" runat="server" Text="Vorname:&nbsp;" CssClass="Text12b"></asp:Label>
                                        <asp:TextBox ID="tbAdrVorname" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;<asp:RequiredFieldValidator EnableClientScript="false" ID="rfvAdresse" runat="server"
                                        ErrorMessage="Bitte eine Adresse eingeben" CssClass="TextBoldRed" ControlToValidate="tbAdrAdresse"
                                        ValidationGroup="order">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="lbAdresse" runat="server" Text="Adresse:&nbsp;" CssClass="Text12b"
                                            Width="60px"></asp:Label>
                                        <asp:TextBox ID="tbAdrAdresse" runat="server" Width="389px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvPlz" EnableClientScript="false" ControlToValidate="tbAdrPlz"
                                            ValidationGroup="order" runat="server" ErrorMessage="Bitte ein Postleitzahl eingeben">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator EnableClientScript="false" ControlToValidate="tbAdrPlz"
                                            ValidationGroup="order" ValidationExpression="\d{4}" ID="revPlz" runat="server"
                                            ErrorMessage="Plz: 4.stellig, nur Zahlen erlaubt">*</asp:RegularExpressionValidator>
                                        <asp:Label ID="lbAdrPlz" runat="server" Text="Plz:&nbsp;" CssClass="Text12b" Width="57px"></asp:Label>
                                        <asp:TextBox ID="tbAdrPlz" runat="server" Width="50px"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvOrt" EnableClientScript="false" ControlToValidate="tbAdrOrt"
                                            ValidationGroup="order" runat="server" ErrorMessage="Bitte einen Ort eingeben">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="lbAdrOrt" runat="server" Text="Ort:&nbsp;" CssClass="Text12b"></asp:Label>
                                        <asp:TextBox ID="tbAdrOrt" runat="server" Width="291px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <uc1:show_pat_brief ID="show_pat_brief1" runat="server" />
            <asp:UpdatePanel ID="upProducts" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <asp:MultiView ID="mvItem1" runat="server">
                        <asp:View ID="vwNoItem1" runat="server"></asp:View>
                        <asp:View ID="vwitem1" runat="server">
                            <table id="tbItem1" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200">
                                        <asp:CustomValidator ID="cvValddlKb" ValidationGroup="order" runat="server" OnServerValidate="cvValddlNotAllNull_ServerValidate"
                                            ErrorMessage="Bitte wählen Sie zumindest ein Produkt!">*</asp:CustomValidator>
                                    </td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem1" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem1" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem2" runat="server">
                        <asp:View ID="vwNoItem2" runat="server"></asp:View>
                        <asp:View ID="vwItem2" runat="server">
                            <table id="tbItem2" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem2" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem2" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem3" runat="server">
                        <asp:View ID="vwNoItem3" runat="server"></asp:View>
                        <asp:View ID="vwItem3" runat="server">
                            <table id="tbItem3" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem3" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem3" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem4" runat="server">
                        <asp:View ID="vwNoItem4" runat="server"></asp:View>
                        <asp:View ID="vwItem4" runat="server">
                            <table id="tbItem4" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem4" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem4" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem5" runat="server">
                        <asp:View ID="vwNoItem5" runat="server"></asp:View>
                        <asp:View ID="vwItem5" runat="server">
                            <table id="tbItem5" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem5" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem5" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem6" runat="server">
                        <asp:View ID="vwNoItem6" runat="server"></asp:View>
                        <asp:View ID="vwItem6" runat="server">
                            <table id="tbItem6" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem6" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem6" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem7" runat="server">
                        <asp:View ID="vwNoItem7" runat="server"></asp:View>
                        <asp:View ID="vwItem7" runat="server">
                            <table id="tbItem7" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem7" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem7" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem8" runat="server">
                        <asp:View ID="vwNoItem8" runat="server"></asp:View>
                        <asp:View ID="vwItem8" runat="server">
                            <table id="tbItem8" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem8" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem8" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem9" runat="server">
                        <asp:View ID="vwNoItem9" runat="server"></asp:View>
                        <asp:View ID="vwItem9" runat="server">
                            <table id="tbItem9" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem9" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem9" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem10" runat="server">
                        <asp:View ID="vwNoItem10" runat="server"></asp:View>
                        <asp:View ID="vwItem10" runat="server">
                            <table id="tbItem10" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem10" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem10" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem11" runat="server">
                        <asp:View ID="vwNoItem11" runat="server"></asp:View>
                        <asp:View ID="vwItem11" runat="server">
                            <table id="tbItem11" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem11" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem11" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem12" runat="server">
                        <asp:View ID="vwNoItem12" runat="server"></asp:View>
                        <asp:View ID="vwItem12" runat="server">
                            <table id="tbItem12" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem12" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem12" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem13" runat="server">
                        <asp:View ID="vwNoItem13" runat="server"></asp:View>
                        <asp:View ID="vwItem13" runat="server">
                            <table id="tbItem13" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem13" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem13" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem14" runat="server">
                        <asp:View ID="vwNoItem14" runat="server"></asp:View>
                        <asp:View ID="vwItem14" runat="server">
                            <table id="tbItem14" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem14" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem14" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem15" runat="server">
                        <asp:View ID="vwNoItem15" runat="server"></asp:View>
                        <asp:View ID="vwItem15" runat="server">
                            <table id="tbItem15" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem15" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem15" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mvItem16" runat="server">
                        <asp:View ID="vwNoItem16" runat="server"></asp:View>
                        <asp:View ID="vwItem16" runat="server">
                            <table id="tbItem16" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem16" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem16" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem17" runat="server">
                        <asp:View ID="vwNoItem17" runat="server"></asp:View>
                        <asp:View ID="vwItem17" runat="server">
                            <table id="tbItem17" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem17" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem17" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem18" runat="server">
                        <asp:View ID="vwNoItem18" runat="server"></asp:View>
                        <asp:View ID="vwItem18" runat="server">
                            <table id="tbItem18" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem18" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem18" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem19" runat="server">
                        <asp:View ID="vwNoItem19" runat="server"></asp:View>
                        <asp:View ID="vwItem19" runat="server">
                            <table id="tbItem19" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem19" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem19" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem20" runat="server">
                        <asp:View ID="vwNoItem20" runat="server"></asp:View>
                        <asp:View ID="vwItem20" runat="server">
                            <table id="tbItem20" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem20" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem20" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem21" runat="server">
                        <asp:View ID="vwNoItem21" runat="server"></asp:View>
                        <asp:View ID="vwItem21" runat="server">
                            <table id="tbItem21" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem21" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem21" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem22" runat="server">
                        <asp:View ID="vwNoItem22" runat="server"></asp:View>
                        <asp:View ID="vwItem22" runat="server">
                            <table id="tbItem22" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem22" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem22" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem23" runat="server">
                        <asp:View ID="vwNoItem23" runat="server"></asp:View>
                        <asp:View ID="vwItem23" runat="server">
                            <table id="tbItem23" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem23" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem23" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem24" runat="server">
                        <asp:View ID="vwNoItem24" runat="server"></asp:View>
                        <asp:View ID="vwItem24" runat="server">
                            <table id="tbItem24" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem24" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem24" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem25" runat="server">
                        <asp:View ID="vwNoItem25" runat="server"></asp:View>
                        <asp:View ID="vwItem25" runat="server">
                            <table id="tbItem25" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem25" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem25" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem26" runat="server">
                        <asp:View ID="vwNoItem26" runat="server"></asp:View>
                        <asp:View ID="vwItem26" runat="server">
                            <table id="tbItem26" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem26" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem26" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem27" runat="server">
                        <asp:View ID="vwNoItem27" runat="server"></asp:View>
                        <asp:View ID="vwItem27" runat="server">
                            <table id="tbItem27" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem27" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem27" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem28" runat="server">
                        <asp:View ID="vwNoItem28" runat="server"></asp:View>
                        <asp:View ID="vwItem28" runat="server">
                            <table id="tbItem28" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem28" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem28" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem29" runat="server">
                        <asp:View ID="vwNoItem29" runat="server"></asp:View>
                        <asp:View ID="vwItem29" runat="server">
                            <table id="tbItem29" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem29" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem29" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem30" runat="server">
                        <asp:View ID="vwNoItem30" runat="server"></asp:View>
                        <asp:View ID="vwItem30" runat="server">
                            <table id="tbItem30" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem30" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem30" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem31" runat="server">
                        <asp:View ID="vwNoItem31" runat="server"></asp:View>
                        <asp:View ID="vwItem31" runat="server">
                            <table id="tbItem31" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem31" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem31" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem32" runat="server">
                        <asp:View ID="vwNoItem32" runat="server"></asp:View>
                        <asp:View ID="vwItem32" runat="server">
                            <table id="tbItem32" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem32" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem32" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem33" runat="server">
                        <asp:View ID="vwNoItem33" runat="server"></asp:View>
                        <asp:View ID="vwItem33" runat="server">
                            <table id="tbItem33" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem33" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem33" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem34" runat="server">
                        <asp:View ID="vwNoItem34" runat="server"></asp:View>
                        <asp:View ID="vwItem34" runat="server">
                            <table id="tbItem34" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem34" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem34" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem35" runat="server">
                        <asp:View ID="vwNoItem35" runat="server"></asp:View>
                        <asp:View ID="vwItem35" runat="server">
                            <table id="tbItem35" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem35" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem35" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem36" runat="server">
                        <asp:View ID="vwNoItem36" runat="server"></asp:View>
                        <asp:View ID="vwItem36" runat="server">
                            <table id="tbItem36" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem36" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem36" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem37" runat="server">
                        <asp:View ID="vwNoItem37" runat="server"></asp:View>
                        <asp:View ID="vwItem37" runat="server">
                            <table id="tbItem37" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem37" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem37" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem38" runat="server">
                        <asp:View ID="vwNoItem38" runat="server"></asp:View>
                        <asp:View ID="vwItem38" runat="server">
                            <table id="tbItem38" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem38" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem38" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem39" runat="server">
                        <asp:View ID="vwNoItem39" runat="server"></asp:View>
                        <asp:View ID="vwItem39" runat="server">
                            <table id="tbItem39" border="0" width="600px" class="frm_green_99CC99" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem39" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem39" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>

                    <asp:MultiView ID="mvItem40" runat="server">
                        <asp:View ID="vwNoItem40" runat="server"></asp:View>
                        <asp:View ID="vwItem40" runat="server">
                            <table id="tbItem40" border="0" width="600px" class="frm_green_CC9999" runat="server">
                                <tr>
                                    <td class="frm_left" valign="middle" align="left" style="width: 10px" width="200"></td>
                                    <td class="frm_left" valign="middle" align="left" style="width: 551px">
                                        <asp:Label ID="lbItem40" runat="server" Text="" Width="350px"></asp:Label>
                                    </td>
                                    <td class="frm_right" valign="middle" align="left" width="300px">
                                        <asp:DropDownList ID="ddlItem40" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table border="0" width="600px">
                <tr align="center">
                    <td class="Text12" width="300px" align="right">
                        <asp:Button OnClick="bt_save_click" ID="btSave" runat="server" CssClass="button"
                            Text="Speichern" Width="190px" ValidationGroup="order" />&nbsp;
                    </td>
                    <td class="Text12" width="300px" align="left">
                        <asp:Button ID="btCancel" runat="server" CssClass="button" Text="Abbrechen" Width="190px"
                            OnClick="bt_cancel_click" />&nbsp;
                    </td>
                </tr>
            </table>
        </div> </div>
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
   
</asp:Content>
