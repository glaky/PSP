<%@ Page Title="" Language="C#" MasterPageFile="~/PSP.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PSP._default" %>

<asp:Content ID="tcCont1" ContentPlaceHolderID="cph_status" runat="server">
    <asp:Label ID="lbRole" runat="server" Text="Nicht angemeldet" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="tcCont2" ContentPlaceHolderID="cph_main" runat="server">
    <div id="main">
        <div id="menu">
            <div class="menuitem_darkgrey">&nbsp;</div>
             <div class="menuitem_lightgrey">&nbsp;</div>
            <div class="menuitem_darkgrey">&nbsp;</div>
            <div class="menuitem_lightgrey">&nbsp;</div>
            <div class="menuitem_darkgrey">&nbsp;</div>
            <div class="menuitem_lightgrey">&nbsp;</div>
            <div class="menuitem_darkgrey">&nbsp;</div>  
            <div class="menuitem_lightgrey">&nbsp;</div>
            <div class="menuitem_darkgrey">&nbsp;</div>
            <div class="menuitem_lightgrey">&nbsp;</div>
        </div>
        <div id="board">
            <table width="100%" border="0">
                <tr>
                    <td align="left" class="Text14">Herzlich willkommen!
                    </td>
                </tr>
                <tr>
                    <td align="left" class="Text12">Bitte melden Sie sich mit Ihrem Benutzernamen und Ihrem Kennwort beim System an.
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
            <table>
                <tr>
                    <td align="left" valign="top" class="TextBoldRed">
                        <asp:ValidationSummary ID="loginValSum" runat="server" HeaderText="Folgende Fehler wurden gefunden:"
                            DisplayMode="List" />
                    </td>
                    <td align="left" class="Text12" valign="top"></td>
                </tr>
            </table>
            <table border="0">
                <tr>
                    <td width="200" align="right" valign="bottom" class="Text12">Benutzername:</td>
                    <td align="left" valign="bottom" class="TextBoldRed">
                        <asp:TextBox ID="tx_login" runat="server" CssClass="text" Width="250px" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="loginVal" runat="server" ControlToValidate="tx_login"
                            ErrorMessage="Benutzername ist leer!">*
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="loginValDB" runat="server" OnServerValidate="loginValDB_ServerValidate"
                            ControlToValidate="tx_login" ErrorMessage="Ungültiger oder inaktiver Benutzername">*</asp:CustomValidator></td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="200" align="right" valign="bottom" class="Text12">Kennwort:</td>
                    <td align="left" valign="bottom" class="TextBoldRed">
                        <asp:TextBox ID="tx_pwd" runat="server" TextMode="Password" CssClass="text" Width="250px"
                            TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="pwdVal" runat="server" ControlToValidate="tx_pwd"
                            ErrorMessage="Kennwort ist leer!">*
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="pwdValDB" runat="server" OnServerValidate="pwdValDB_ServerValidate"
                            ControlToValidate="tx_pwd" ErrorMessage="Ungültiges Kennwort">*</asp:CustomValidator></td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" class="Text12" valign="bottom" width="200">&nbsp;</td>
                    <td align="left" class="Text12" valign="bottom">
                        <asp:HyperLink CssClass="standard" NavigateUrl="~/general/pwd_reminder.aspx" ID="hl_pwdreminder" runat="server">Kennwort vergessen?</asp:HyperLink>
                    </td>
                    <td align="left" valign="bottom" width="150">&nbsp;</td>
                </tr>
            </table>
            <table border="0">
                <tr align="center">
                    <td width="200">&nbsp;</td>
                    <td class="Text12">
                        <asp:Button OnClick="bt_login_click" TabIndex="3" ID="btLogin" runat="server" CssClass="button"
                            Text="Anmelden" Width="250px" />&nbsp;
                    </td>
                    <td width="150" align="left" valign="bottom">&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
