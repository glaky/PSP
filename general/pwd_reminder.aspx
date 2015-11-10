<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PSP.master" CodeBehind="pwd_reminder.aspx.cs" Inherits="TecfiCare._pwd_reminder" %>

<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
  <asp:Label ID="lbRole" runat="server" Text="Nicht angemeldet" CssClass="Text12b" ></asp:Label>
</asp:Content>
<asp:Content ID="login" ContentPlaceHolderID="cph_main" runat="Server">
  <div id="main">
    	<div id="menu">
      	<div class="menuitem_green">&nbsp;</div>
        <div class="menuitem_purple">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
      </div>
      <div id="board">
        <asp:MultiView ID="mvStart" runat="server">
          <asp:View ID="vwStart" runat="server">
          
        <table width="100%" border="0">
					<tr>
						<td align="left" class="Text14">
							Kennworterinnerung
						</td>
					</tr>
					<tr>
						<td align="left" class="Text12">
							Bitte geben Sie Ihren Benutzernamen und Ihre E-Mailadresse ein.<br />
							Sollte Ihnen das nicht möglich sein, wenden Sie sich bitte an die Systemadministration.
						</td>
					</tr>
				</table>
				<table width="100%"><tr><td><hr /></td></tr></table>
				<table>
					<tr>
						<td align="left" valign="top" class="TextBoldRed">
							<asp:ValidationSummary ID="loginValSum" runat="server" HeaderText="Folgende Fehler wurden gefunden:"
								DisplayMode="List" />
						</td>
						<td align="left" class="Text12" valign="top">
						</td>
					</tr>
				</table>
				<table border="0">
					<tr>
						<td width="200" align="right" valign="bottom" class="Text12">
							Benutzername:</td>
						<td align="left" valign="bottom" class="TextBoldRed">
							<asp:TextBox ID="tx_login" runat="server" CssClass="text" Width="250px" TabIndex="1"></asp:TextBox>
							<asp:RequiredFieldValidator ID="loginVal" runat="server" ControlToValidate="tx_login"
								ErrorMessage="Bitte Benutzernamen angeben!">*
							</asp:RequiredFieldValidator>
							<asp:CustomValidator ID="loginValDB" runat="server" OnServerValidate="pwdReminder_ServerValidate"
								ErrorMessage="">*</asp:CustomValidator></td>
						<td width="150" align="left" valign="bottom">
							&nbsp;
						</td>
					</tr>
					<tr>
						<td width="200" align="right" valign="bottom" class="Text12">
							E-Mailadresse:</td>
						<td align="left" valign="bottom" class="TextBoldRed">
							<asp:TextBox ID="tx_pwd" runat="server" CssClass="text" Width="250px"
								TabIndex="2"></asp:TextBox>
							<asp:RequiredFieldValidator ID="pwdVal" runat="server" ControlToValidate="tx_pwd"
								ErrorMessage="Bitte E-Mailadresse angeben">*
							</asp:RequiredFieldValidator>
							</td>
						<td width="150" align="left" valign="bottom">
							&nbsp;
						</td>
					</tr>
				</table>
				<table border="0">
					<tr align="center">
					  <td width="200">&nbsp;</td>
						<td class="Text12">
							<asp:Button OnClick="bt_login_click" TabIndex="3" ID="btLogin" runat="server" CssClass="button"
								Text="Abschicken" Width="250px" />&nbsp;
						</td>
						<td width="150" align="left" valign="bottom">
							&nbsp;
						</td>
					</tr>
				</table>
				</asp:View>
          <asp:View ID="vwEnd" runat="server">
            
        <table width="100%" border="0">
					<tr>
						<td align="left" class="Text14">
							Kennworterinnerung
						</td>
					</tr>
					<tr>
						<td align="left" class="Text12">
							Ihr Kennwort wurde an Sie verschickt. Bitte &uuml;berpr&uuml;fen Sie auch Ihren Spam-Ordner.<br />
              <asp:HyperLink ID="hl_default" runat="server" NavigateUrl="~/default.aspx">Zur&uuml;ck zur Anmeldeseite</asp:HyperLink>
						</td>
					</tr>
				</table>
          </asp:View>
				</asp:MultiView>
      </div>
    </div>
</asp:Content>