<%@ Page Title="" Language="C#" MasterPageFile="~/PSP.master" AutoEventWireup="true" CodeBehind="msdatabase2PSP.aspx.cs" Inherits="PSP.msdatabase2PSP" %>
<asp:Content ID="tcCont1" ContentPlaceHolderID="cph_status" runat="server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
    <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="tcCont2" ContentPlaceHolderID="cph_main" runat="server">
    <div id="main">
        <div id="menu">
             <!--#include file="~/menue/left_menu_adm.aspx"-->
        </div>
        <div id="board">
            <table width="100%" border="0">
                <tr>
                    <td align="left" class="Text14">Datenportierung
                    </td>
                </tr>
                <tr>
                    <td align="left" class="Text12">
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
            <table border="0">
                <tr align="center">
                    <td width="200">&nbsp;</td>
                    <td class="Text12">
                        <asp:Button OnClick="bt_patdat_click" TabIndex="3" ID="btPat" runat="server" CssClass="button"
                            Text="Personaldaten portieren" Width="250px" />&nbsp;
                    </td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
                <tr align="center">
                    <td width="200">&nbsp;</td>
                    <td class="Text12">
                        <asp:Button OnClick="bt_btkdat_click" TabIndex="3" ID="btBtk" runat="server" CssClass="button"
                            Text="Betreuungskontakte portieren" Width="250px" />&nbsp;
                    </td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
                <tr align="center">
                    <td width="200">&nbsp;</td>
                    <td class="Text12">
                        <asp:Button OnClick="bt_orddat_click" TabIndex="3" ID="btOrd" runat="server" CssClass="button"
                            Text="Bestellungen portieren" Width="250px" />&nbsp;
                    </td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
            </table>
            <table border="0">
                <tr align="center">
                    <td width="200">&nbsp;</td>
                    <td class="Text12">
                        <asp:Label ID="lbResult" Text="" runat="server"></asp:Label>
                    </td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
