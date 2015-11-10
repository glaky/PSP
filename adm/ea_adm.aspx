<%@ Page Title="" Language="C#" MasterPageFile="~/PSP.master" AutoEventWireup="true"
  CodeFile="ea_adm.aspx.cs" Inherits="adm_PSP" %>

<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
  <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b" ></asp:Label>&nbsp;|&nbsp;
  <asp:label id="lbTitel" runat="server" text="" cssclass="Text12b"></asp:label>&nbsp;
  <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b" ></asp:Label>&nbsp;
  <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b" ></asp:Label>
</asp:Content>
<asp:Content ID="menu_rez" ContentPlaceHolderID="cph_main" runat="Server">
  <div id="main">
    <!--#include file="~/menue/left_menu_adm.aspx"-->
    <div id="board">
      <table width="600px" border="0" class="title">
        <tr>
          <td align="left" class="Text14">
            <asp:Label ID="lbTitle" runat="server" Text="Neues Konto anlegen"></asp:Label>
          </td>
        </tr>
      </table>
        <br />
      <table width="600px" class="verror">
        <tr>
          <td align="right" class="Text12">
          </td>
          <td align="left" valign="top" class="Text12">
            <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="account" DisplayMode="BulletList"
              ID="accountValSum" runat="server" HeaderText="Es wurden fehlende oder fehlerhafte Eingaben entdeckt:" />
          </td>
        </tr>
        <tr>
          <td align="right" class="Text12" >
          </td>
          <td align="left" valign="top" class="Text12">
            <asp:ValidationSummary CssClass="TextBoldRed" ValidationGroup="account1" DisplayMode="BulletList"
              ID="accountValSum1" runat="server" HeaderText="Es wurden fehlende oder fehlerhafte Eingaben entdeckt:" />
          </td>
        </tr>
      </table>
        <br />
      <table width="600px" class="frm_green_99CC99">
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rvfName" ControlToValidate="tbName" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte einen Namen eingeben!">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                ID="rfvName1" ControlToValidate="tbName" runat="server" ValidationGroup="account1"
                ErrorMessage="Bitte einen Namen eingeben!">*</asp:RequiredFieldValidator>
          </td>
          <td class="Text12b" width="150px">
            Name
          </td>
          <td width="190px">
            <asp:TextBox ID="tbName" runat="server" CssClass="text"></asp:TextBox>
          </td>
          <td>
            &nbsp;
          </td>
        </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rfvSurname" ControlToValidate="tbSurname" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte einen Vornamen eingeben!">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                ID="rfvSurname1" ControlToValidate="tbSurname" runat="server" ValidationGroup="account1"
                ErrorMessage="Bitte einen Vornamen eingeben!">*</asp:RequiredFieldValidator>
          </td>
          <td class="Text12b" width="150px">
            Vorname
          </td>
          <td width="190px">
            <asp:TextBox ID="tbSurname" runat="server" CssClass="text"></asp:TextBox>
          </td>
          <td>
            &nbsp;
          </td>
        </tr>
        <tr>
              <td class="frm_left" valign="middle" align="left" width="10px">
              </td>
              <td class="Text12b" width="150px">
                Titel:
              </td>
              <td>
                <asp:DropDownList ID="ddl_title" runat="server" width="175px">
                </asp:DropDownList>
              </td>
              <td>
                &nbsp;
              </td>
            </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rfvAccount" ControlToValidate="tbAccount" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte einen Kontonamen eingeben!">*</asp:RequiredFieldValidator><asp:CustomValidator
                ID="cvKonto" runat="server" OnServerValidate="cvValKonto_ServerValidate" ValidationGroup="account" ErrorMessage="Es besteht bereits ein Konto mit diesem Kontonamen">*</asp:CustomValidator>
          </td>
          <td class="Text12b" width="150px">
            Kontoname
          </td>
          <td width="190px">
            <asp:TextBox ID="tbAccount" runat="server" CssClass="text"></asp:TextBox>
          </td>
          <td>
            <asp:Button ID="btAccount" CssClass="button" runat="server" Text="Kontonamen erstellen"
              Width="180px" ValidationGroup="account1" OnClick="btAccount_Click" />
            &nbsp;
          </td>
        </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rfvPwd" ControlToValidate="tbPwd" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte einen Passwort eingeben!">*</asp:RequiredFieldValidator><asp:CompareValidator
                ID="cmpVal_pwd" Operator="Equal" Type="String" ControlToCompare="tbPwdc" ControlToValidate="tbPwd"
                runat="server" ValidationGroup="account" ErrorMessage="Passwörter sind nicht indent!">*</asp:CompareValidator>
          </td>
          <td class="Text12b" width="150px">
            Passwort
          </td>
          <td width="190px">
            <asp:TextBox CssClass="text" TextMode="Password" ID="tbPwd" runat="server"></asp:TextBox>
          </td>
          <td>
            <asp:Button ID="btPwd" CssClass="button" runat="server" Text="Passwort erstellen"
              Width="180px" OnClick="btPwd_Click" />
            &nbsp;
          </td>
        </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rfvPwdC" ControlToValidate="tbPwdC" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte Passwort bestätigen!">*</asp:RequiredFieldValidator>
          </td>
          <td class="Text12b" width="150px">
            Passwortbestätigung
          </td>
          <td width="190px">
            <asp:TextBox TextMode="Password" CssClass="text" ID="tbPwdC" runat="server"></asp:TextBox>
          </td>
          <td>
            &nbsp;
          </td>
        </tr>
            <tr>
              <td class="frm_left" valign="middle" align="left" width="10px">
                <asp:CompareValidator ID="cv_role" Type="String" ControlToValidate="ddl_role" Operator="NotEqual"
                  ValueToCompare="-" ValidationGroup="account" ErrorMessage="Bitte eine Rolle wählen!"
                  runat="server">*</asp:CompareValidator>
              </td>
              <td class="Text12b" width="150px">
                Rolle:
              </td>
              <td width="190px">
                <asp:DropDownList ID="ddl_role" runat="server" Width="175px">
                </asp:DropDownList>
              </td>
              <td>
                &nbsp;
              </td>
            </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rfvPhone" ControlToValidate="tbPhone" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte eine Telefonnummer eingeben!">*</asp:RequiredFieldValidator>
          </td>
          <td class="Text12b" width="150px">
            Telefon
          </td>
          <td width="190px">
            <asp:TextBox ID="tbPhone" runat="server" CssClass="text"></asp:TextBox>
          </td>
          <td>
            &nbsp;
          </td>
        </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
          </td>
          <td class="Text12b" width="150px">
            Fax
          </td>
          <td width="190px">
            <asp:TextBox ID="tbFax" runat="server" CssClass="text"></asp:TextBox>
          </td>
          <td>
            &nbsp;
          </td>
        </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="tbPhone" runat="server"
              ValidationGroup="account" ErrorMessage="Bitte eine E-Mail Adresse eingeben!">*</asp:RequiredFieldValidator>
          </td>
          <td class="Text12b" width="150px">
            E-Mail
          </td>
          <td width="190px">
            <asp:TextBox ID="tbEmail" runat="server" CssClass="text"></asp:TextBox>
          </td>
          <td>
            &nbsp;
          </td>
        </tr>
        <tr>
          <td class="frm_left" valign="middle" align="left" width="10px">
            <asp:RequiredFieldValidator CssClass="TextBoldRed" runat="server" ID="rfvAktiv" ControlToValidate="rbAktiv"
              ValidationGroup="account" ErrorMessage="Aktiv: Bitte eine Option wählen" Width="10px">*</asp:RequiredFieldValidator>
          </td>
          <td class="frm_left" valign="middle" align="left" width="150px">
            <asp:Label ID="lbAktiv" runat="server" Text="Aktiv"></asp:Label>
          </td>
          <td class="frm_right" valign="middle" align="left" width="175px">
            <asp:RadioButtonList ID="rbAktiv" CssClass="Text12" runat="server" RepeatDirection="Horizontal">
              <asp:ListItem Text="Ja&nbsp;&nbsp;" Value="J"></asp:ListItem>
              <asp:ListItem Text="Nein&nbsp;&nbsp;" Value="N"></asp:ListItem>
            </asp:RadioButtonList>
          </td>
          <td class="frm_right" valign="middle" align="left">
            &nbsp;
          </td>
        </tr>
      </table>
      <table border="0" width="600px">
        <tr align="center">
          <td class="Text12" width="300px" align="right">
            <asp:Button OnClick="bt_save_click" ID="btSave" runat="server" CssClass="button"
              Text="Speichern" Width="190px" ValidationGroup="account" />&nbsp;
          </td>
          <td class="Text12" width="300px" align="left">
            <asp:Button ID="btCancel" runat="server" CssClass="button" Text="Abbrechen" Width="190px"
              OnClick="bt_cancel_click" />&nbsp;
          </td>
        </tr>
      </table>
    </div>
  </div>
</asp:Content>
