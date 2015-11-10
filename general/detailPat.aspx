<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detailPat.aspx.cs" Inherits="PSP.general.detailPat"
    MasterPageFile="~/PSP.master" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="pat" TagName="details" Src="~/general/showPat.ascx" %>
<%@ Register TagPrefix="pat" TagName="btk" Src="~/general/showBtk.ascx" %>
<%@ Register TagPrefix="pat" TagName="order" Src="~/general/showOrder.ascx" %>
<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
    <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="pat_overview" ContentPlaceHolderID="cph_main" runat="Server">
    <asp:ScriptManager ID="scPat" runat="server" EnablePartialRendering="true" SupportsPartialRendering="true" />
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
            <pat:details ID="show_patdetail" runat="server" UpdateMode="Conditional" />
            <br />
            <pat:btk ID="show_btk" runat="server" UpdateMode="Conditional" />
            <br />
            <pat:order ID="show_order" runat="server" UpdateMode="Conditional" />
            <br />
            <table border="0" width="600px">
                <tr align="center">
                    <td class="Text12" width="600px" align="center">
                        <asp:Button ID="btUber" runat="server" CssClass="button" Text="&Uuml;bersicht" Width="190px"
                            OnClick="bt_uber_click" />&nbsp;
                        <asp:Button ID="btTermin" runat="server" CssClass="button" Text="Terminkalender"
                            Width="190px" OnClick="bt_termin_click" />&nbsp;
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
