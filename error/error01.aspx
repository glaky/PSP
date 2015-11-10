<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error01.aspx.cs" Inherits="PSP.error.error01" MasterPageFile="~/PSP.master" %>

<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
  <asp:Label ID="lbRole" runat="server" Text="Nicht angemeldet" CssClass="Text12b" ></asp:Label>
</asp:Content>
<asp:Content ID="login" ContentPlaceHolderID="cph_main" runat="Server">
  <div id="main">
    	<div id="menu">
      	<div class="menuitem_yellow">&nbsp;</div>
        <div class="menuitem_red">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
        <div class="menuitem_grey">&nbsp;</div>
      </div>
      <div id="board">
        <table width="100%" border="0">
					<tr>
						<td align="left" class="Text14">
							Sehr geehrter Benutzer!
						</td>
					</tr>
					<tr>
						<td align="left" class="Text12">
							Sie habe sich nicht ordnungsgemäß angemeldet oder Ihre Anmledung ist wegen einer Zeitüberschreitung ungültig<br />
								Bitte melden Sie sich
								<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">hier</asp:HyperLink> an.
						</td>
					</tr>
				</table>
    </div>
</asp:Content>