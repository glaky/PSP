<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_adm.aspx.cs" Inherits="PSP.general.menu_adm"
    MasterPageFile="~/PSP.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
    <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
    <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="menu_rez" ContentPlaceHolderID="cph_main" runat="Server">
  <div id="main">
    	<asp:MultiView ID="mvMenu" runat="server">
            <asp:View ID="vwNrs" runat="server">
                <!--#include file="~/menue/left_menu_nrs.aspx"-->
            </asp:View>
            <asp:View ID="vwSec" runat="server">
                <!--#include file="~/menue/left_menu_sec.aspx"-->
            </asp:View>
            <asp:View ID="vwAdm" runat="server">
                <!--#include file="~/menue/left_menu_adm.aspx"-->
            </asp:View>
        </asp:MultiView>
      <div id="board">
        <table width="650px" border="0" class="title">
					<tr>
						<td align="left" class="Text14">
							Übersicht Benutzerkonten 
						</td>
					</tr>
				</table>
        <br />
        <table width="650px" border="0" class="frm">
          <tr>
            <td width="40px">
              <asp:Label CssClass="Text12b" ID="lbFName" runat="server" Text="Name:&nbsp;"></asp:Label>
            </td>
            <td width="180px">
              <asp:TextBox CssClass="text" Text="" ID="txFName" runat="server"></asp:TextBox>
              <asp:TextBox Visible="false" ID="txFNameV" runat="server"></asp:TextBox>
            </td>
            <td width="40px">
              <asp:Label CssClass="Text12b" ID="lbFRole" runat="server" Text="Rolle:&nbsp;"></asp:Label>
            </td>
            <td width="100px">
              <asp:DropDownList ID="ddlFRole" runat="server">
                <asp:ListItem Selected="True">Alle</asp:ListItem>
                <asp:ListItem>Administrator</asp:ListItem>
                <asp:ListItem>Medizinische Assistenz</asp:ListItem>
                <asp:ListItem>Nurse Service</asp:ListItem>
                <asp:ListItem>Service Center</asp:ListItem>
                <%-- <asp:ListItem>MS-Coach</asp:ListItem> --%> 
              </asp:DropDownList>
              <asp:TextBox Visible="false" ID="txFRoleV" runat="server"></asp:TextBox>
            </td>
            <td>
              <asp:Button ID="btFilter" runat="server" Text="Filter anwenden" CssClass="button" />
            </td>
          </tr>
        </table>
        <br />
        <asp:GridView ID="gv_acclist" runat="server" AutoGenerateColumns="False" 
          DataSourceID="SqlDataSource" AllowPaging="True" AllowSorting="True" PageSize="30"
          CellPadding="4" GridLines="None" EmptyDataText="Anzahl der gefundenen Konten mit diesem Filter: 0" 
          ForeColor="#333333" OnRowDataBound="gv_acclist_rdb" Width="700px" BorderColor="#535d65" BorderWidth="1px" BorderStyle="Solid">
          <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
          <Columns>
            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
            <asp:BoundField DataField="forename" HeaderText="Vorname" 
              SortExpression="forename" />
            <asp:BoundField DataField="email" HeaderText="E-Mail" SortExpression="email" />
            <asp:TemplateField HeaderText="Rolle" SortExpression="role">
            <ItemTemplate>
              <asp:Label runat="server" ID="lbDelQ" Visible="false" Text='<%# Eval("role")%>'></asp:Label> 
              <asp:Label ID="lbRolle" runat="server" Text='<%# Eval("role")%>'></asp:Label> 
            </ItemTemplate>
          </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" SortExpression="status" ItemStyle-Width="15px">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
              <asp:Image ID="img_status" runat="server" width="16px" height="16px" ImageUrl='<%# "~/images/" + Eval("status") +  ".png"%>' />
            </ItemTemplate>
          </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="15px">
              <ItemTemplate>
                <asp:HyperLink  runat="server" ID="hl_account" CssClass="images" NavigateUrl='<%# Eval("ID", "ea_adm.aspx?ID={0}&reason=edit") %>'>
                    <asp:Image ID="img_edit" ImageUrl="~/images/edit.png" width="16px" height="16px" ToolTip="Kontodaten bearbeiten" runat="server" />
                </asp:HyperLink>
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
          <RowStyle CssClass="gv_row" />
          <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
          <HeaderStyle BackColor="#f5a300" Font-Bold="True" ForeColor="White" />
          <AlternatingRowStyle BackColor="#DDDDDD" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource" runat="server" 
          ConnectionString="<%$ ConnectionStrings:SqlDb %>" 
          SelectCommand="SELECT [id], [account], [name], [forename], [role], [phone], [email], [fax], [status] FROM [accounts] WHERE ([name] LIKE @filter_name AND [role] like @filter_role AND NOT([status] LIKE 'A%')) ORDER BY [name], [forename], [role]">
          <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="ID" Type="Int32" />
            <asp:ControlParameter ControlID="txFNameV" Name="filter_name" PropertyName="Text" />
            <asp:ControlParameter ControlID="txFRoleV" Name="filter_role" PropertyName="Text" />
          </SelectParameters>
        </asp:SqlDataSource>
      </div>
    </div>
</asp:Content>
