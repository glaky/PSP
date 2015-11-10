<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="showPatBrief.ascx.cs" Inherits="TecfiCare.general.WebUserControl1" %>
<table border="0" width="600px" class="frm_green_99CC99">
    <tr>
        <td class="frm_left" valign="middle" align="left" width="10px">
            &nbsp;
        </td>
        <td class="frm_left_white" valign="middle" align="left">
            <asp:Label ID="lbPNameV" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <asp:Label ID="lbPVornameV" runat="server" CssClass="Text12"></asp:Label>&nbsp;&nbsp;*
            <asp:Label ID="lbGebdatV" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <asp:Label ID="lbGeschlechtV" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <br />
            <asp:Label ID="lbPlzOrt" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <asp:Label ID="lbAdresseV" runat="server" CssClass="Text12"></asp:Label>
            <br />
            <asp:Label CssClass="Text12" ID="Label57" runat="server" Text="Telefon: "></asp:Label>
            <asp:Label ID="lbFestnetz" runat="server" CssClass="Text12"></asp:Label>
            <asp:Label CssClass="Text12" ID="Label59" runat="server" Text=" | E-Mail: "></asp:Label>
            <asp:Label ID="lbEmail" runat="server" CssClass="Text12"></asp:Label>
            <br />
            <asp:Label ID="lbTheStart" runat="server" CssClass="Text12"></asp:Label><br />
            <asp:Label ID="lbErrei" runat="server" CssClass="Text12"></asp:Label><br />
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>