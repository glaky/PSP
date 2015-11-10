<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="showOrder.ascx.cs" Inherits="PSP.general.showOrder" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:UpdatePanel ID="upOrder" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <contenttemplate>
     <table width="600px" border="0" class="title">
        <tr>
        <td align="left" class="Text14">
            <asp:Label ID="lbOrder" runat="server" Text="Bestellungen" Width="300px"></asp:Label>&nbsp;
        </td>
        <td align="right">
        <asp:HyperLink runat="server" ID="hl_order" CssClass="images" >
            <asp:Image ID="img_order" ImageUrl="~/images/order.png" ToolTip="Neue Bestellung"
                    AlternateText="Neue Bestellung" runat="server"  Style="height: 16px;width:16px" />
        </asp:HyperLink>
            <asp:ImageButton ID="imgBtnOrder" runat="server" ImageUrl="~/images/details.png"
            OnClick="imgBtn_Click" ToolTip="Bestellungen anzeigen"  Style="height: 16px;width:16px"/>
      </td>
      <td>
      </td>
    </tr>
  </table>
  
      <asp:MultiView ID="mvShowOrder" runat="server">
        <asp:View ID="vwShowOrderNo" runat="server">
        </asp:View>
        <asp:View ID="vwShowOrder" runat="server">
          <asp:MultiView ID="mvOrder" runat="server">
            <asp:View ID="vwNoOrder" runat="server">
              <table border="0" width="600px" class="frmt_green_99CC99">
                <tr>
                  <td class="frm_left" valign="middle" align="left" width="10px">
                  </td>
                  <td class="frm_left" valign="middle" align="left">
                    Bisher keine Bestellungen erfasst.
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="vwOrder" runat="server">
              <asp:Repeater ID="rpBst" runat="server" DataSourceID="sqlDSBst" OnItemDataBound="rpBst_ItemDataDataBound">
                <HeaderTemplate>
                  <table border="0" width="600px" class="frmt_green_99CC99">
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                    <td class="frm_left" valign="middle" align="left" width="10px">
                    </td>
                    <td class="frm_left_white">
                                            <asp:Label ID="lbDatum" runat="server" Text='<%# Eval("procdate") %>'></asp:Label>&nbsp;
                      <asp:HyperLink ToolTip="Druckansicht (PDF)" ID="hlPrint" runat="server" Target="_blank"
                        NavigateUrl='<%# Eval("pdf") %>'>&nbsp;&nbsp;
                        <asp:Image ID="imPrint" ImageUrl="~/images/cdr.png" runat="server" Style="height: 16px;width:16px"  />
                      </asp:HyperLink>
                    </td>
                    <td class="frm_left" valign="middle" align="left">
                    </td>
                  </tr>
                  <tr>
                    <td class="frm_left" valign="middle" align="left" width="10px">
                    </td>
                    <td colspan="2">
                      <hr />
                    </td>
                  </tr>
                </ItemTemplate>
                <FooterTemplate>
                  </table></FooterTemplate>
                <SeparatorTemplate>
                </SeparatorTemplate>
              </asp:Repeater>
              <asp:SqlDataSource ID="sqlDSBst" runat="server" ConnectionString="<%$ ConnectionStrings:SqlDb %>"
                SelectCommand="SELECT [ordid], [patid], [procdate], [item1], [pdf] FROM [orders] WHERE ([patid] = @patid) ORDER BY [procdate] DESC">
                <SelectParameters>
                  <asp:QueryStringParameter DefaultValue="0" Name="patid" QueryStringField="patid"
                    Type="Int32" />
                </SelectParameters>
              </asp:SqlDataSource>
            </asp:View>
          </asp:MultiView>
        </asp:View>
      </asp:MultiView>
    </contenttemplate>
</asp:UpdatePanel>