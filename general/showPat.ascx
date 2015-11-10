<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="showPat.ascx.cs" Inherits="TecfiCare.general.showPat" %>
<table width="600px" border="0" class="title">
    <tr>
        <td align="left" class="Text14">
            <asp:Label ID="lbTitle" runat="server" Text="Personaldaten: " Width="300px"></asp:Label>&nbsp;
        </td>
        <td align="right">
            <asp:HyperLink runat="server" ID="hl_pers" CssClass="images">
                <asp:Image ID="img_pers" ImageUrl="~/images/edit.png" ToolTip="Personaldaten bearbeiten"
                    AlternateText="Personaldaten bearbeiten" runat="server"  Style="height: 16px;width:16px" />
            </asp:HyperLink>
        </td>
        <td>
        </td>
    </tr>
</table>
<table border="0" width="600px" class="frmb_green_99CC99">
    <tr>
        <td class="frm_leftn" valign="middle" align="left" width="10px">
            &nbsp;
        </td>
        <td class="frm_leftn" valign="middle" align="left">
            <asp:Label ID="lbConsent" runat="server" CssClass="Text12" Style="font-weight:bold"></asp:Label>,&nbsp;
            <asp:Label ID="lbForm" runat="server" CssClass="Text12" Style="font-weight:bold"></asp:Label><br />
            <asp:Label ID="lbPNameV" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <asp:Label ID="lbPVornameV" runat="server" CssClass="Text12"></asp:Label>&nbsp;&nbsp;*
            <asp:Label ID="lbGebdatV" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <asp:Label ID="lbGeschlechtV" runat="server" CssClass="Text12"></asp:Label>,&nbsp;
            <br />
            <asp:Label ID="lbPlzOrt" runat="server" CssClass="Text12"></asp:Label>
            ,
            <asp:Label ID="lbAdresse" runat="server" CssClass="Text12"></asp:Label>
            <br />
            Telefon:
            <asp:Label ID="lbFestnetz" runat="server" CssClass="Text12"></asp:Label>
            <br />
            E-Mail:
            <asp:Label ID="lbEmail" runat="server" CssClass="Text12"></asp:Label>
            <br />
            <asp:Label ID="lbProcdate" runat="server" CssClass="Text12"></asp:Label>

</td>
    </tr>
</table>
<table border="0" width="600px" class="frmt_green_99CC99">
    <tr>
        <td class="frm_leftn" valign="middle" align="left" width="10px">
            &nbsp;
        </td>
        <td class="frm_leftn" valign="middle" align="left">
            <asp:Label ID="lbDiagnose" runat="server" CssClass="text" ></asp:Label><br />
            <asp:Label ID="lbZentrum" runat="server" CssClass="text"></asp:Label><br />
            <asp:Label ID="lbMedikament" runat="server" CssClass="text"></asp:Label><br />
            <asp:Label ID="lbThestart" runat="server" CssClass="text"></asp:Label><br />
            <asp:Label ID="lbVorthe" runat="server" CssClass="text"></asp:Label><br />
            <asp:Label ID="lbAnonym" runat="server" CssClass="text"></asp:Label><br />
            <asp:Label ID="lbErrei" runat="server" CssClass="text"></asp:Label><br />
            <asp:Label ID="lbIntervall" runat="server" CssClass="text"></asp:Label><br />
        </td>
    </tr>
</table>