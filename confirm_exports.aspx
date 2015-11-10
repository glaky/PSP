<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirm_exports.aspx.cs" Inherits="PSP.confirm_exports" MasterPageFile="~/PSP.master"%>

<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
  <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b" ></asp:Label>&nbsp;|&nbsp;
  <asp:label id="lbTitel" runat="server" text="" cssclass="Text12b"></asp:label>&nbsp;
  <asp:Label ID="lbSurname" runat="server" Text="" CssClass="Text12b" ></asp:Label>&nbsp;
  <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b" ></asp:Label>
</asp:Content>
<asp:Content ID="ce_dks" ContentPlaceHolderID="cph_main" runat="Server">
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
    
    <table width="600px" border="0" class="frm_purple">
					<tr>
						<td align="left" class="Text12">
							Die von Ihnen gewählten Patienendaten wurden per E-Mail an Sie verschickt. Bitte &uuml;berpr&uuml;fen Sie auch Ihren Spam-Ordner.<br />
						</td>
					</tr>
				</table>
				      <table border="0" width="600px">
        <tr align="center">
          <td class="Text12" width="600px" align="center">
            <asp:Button ID="btCancel" runat="server" CssClass="button" Text="Zurück zur Übersicht" Width="190px"
              OnClick="bt_confirm_click" />&nbsp;
          </td>
        </tr>
      </table>
    </div>  
  </div>
</asp:Content>

