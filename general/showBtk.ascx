<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="showBtk.ascx.cs" Inherits="PSP.general.showBtk" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
  Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:UpdatePanel ID="upKontakte" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
  <ContentTemplate>
    <table width="600px" border="0" class="title">
      <tr>
        <td align="left" class="Text14">
          <asp:Label ID="lbOrder" runat="server" Text="Betreuungskontakte" Width="300px"></asp:Label>&nbsp;
        </td>
        <td align="right">
          <asp:HyperLink runat="server" ID="hl_btk" CssClass="images">
            <asp:Image ID="img_patient" ImageUrl="~/images/kontakt.png" ToolTip="Neuer Betreuungskontakt"
              AlternateText="Neuer Betreuungskontakt" runat="server" Style="height: 16px;width:16px"  />
          </asp:HyperLink>
          <asp:ImageButton ID="imgBtnKontakte" runat="server" ImageUrl="~/images/details.png"
            OnClick="imgBtn_Click" ToolTip="Betreuungskontakte anzeigen"  Style="height: 16px;width:16px" />
        </td>
        <td>
        </td>
      </tr>
    </table>
    <asp:MultiView ID="mvShowKontakte" runat="server">
      <asp:View ID="vwShowKontakteNo" runat="server">
      </asp:View>
      <asp:View ID="vwShowKontakte" runat="server">
        <asp:MultiView ID="mvKontakte" runat="server">
          <asp:View ID="vwNoKontakte" runat="server">
            <table border="0" width="600px" class="frmt_green_99CC99">
              <tr>
                <td class="frm_left" valign="middle" align="left" width="10px">
                </td>
                <td class="frm_left" valign="middle" align="left">
                  <asp:Label ID="Label1" runat="server" Text="Bisher keine Betreuungskontakte erfasst." Width="500px"></asp:Label>
                </td>
                <td class="frm_right" valign="middle" align="left" width="300px">
                  <asp:Label ID="Label2" runat="server" CssClass="text"></asp:Label>
                </td>
              </tr>
            </table>
          </asp:View>
          <asp:View ID="vwKontakte" runat="server">
            <asp:Repeater ID="rpDksKontakt" runat="server" DataSourceID="sqlDSDksKontakt" OnItemDataBound="rpDksKontakt_ItemDataDataBound">
              <HeaderTemplate>
              </HeaderTemplate>
              <ItemTemplate>
                <table border="0" width="600px" class="frm_green_CC9999">
                  <tr>
                    <td class="frm_left" valign="top" align="left" width="10px">
                    </td>
                    <td class="frm_left_white">
                      <asp:Label ID="Label100" runat="server" Text='<%# Eval("btkid") %>'></asp:Label>,&nbsp;
                      <asp:Label ID="lbDatum" runat="server" Text='<%# Eval("btkdate") %>'></asp:Label>,&nbsp;
                      <asp:Label ID="lbArt" runat="server" Text='<%# Eval("art") %>'></asp:Label>,&nbsp;
                      <asp:Label ID="lbAbge" runat="server" Text='<%# Eval("abgeschlossen") %>'></asp:Label>,&nbsp;
                    </td>
                    <td class="frm_left" valign="top" align="left" width="10px">
                      <asp:Image ID="imStatus" ImageUrl="~/images/o.png" runat="server" Style="height: 16px;width:16px" />
                    </td>
                    <td class="frm_left" valign="top" align="left" width="10px">
                      <asp:HyperLink ToolTip='<%# Eval("patid") %>' ID="hlEdit" runat="server" Target="_self"
                        NavigateUrl='<%# Eval("btkid") %>'>
                        <asp:Image ID="imView" ImageUrl="~/images/edit.png" runat="server" ToolTip="Kontakt bearbeiten" Style="height: 16px;width:16px" />
                      </asp:HyperLink>
                    </td>
                    <td width="10px">
                      &nbsp;
                    </td>
                  </tr>
                  </table>
                  <asp:MultiView ID="mvKntk" runat="server">
                    <asp:View ID="vwKntkNo" runat="server">
                    </asp:View>
                    <asp:View ID="vwKntk" runat="server">
                      <table border="0" width="600px" class="frmt_green_99CC99">
                        <tr>
                          <td class="frm_left" valign="top" align="left" width="10px">
                          </td>
                          <td class="frm_left_white" valign="top">
                            <asp:Label ID="Label3" runat="server" Text="Behandlungstatus" Width="250px"></asp:Label>
                          </td>
                          <td class="frm_leftn" valign="top" align="left">
                            <asp:Label ID="lbStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label><br />
                            <asp:Label ID="lbStatusDatum" runat="server" Text="Datum:&nbsp;"></asp:Label>
                            <asp:Label ID="lbStatusDatumV" runat="server" Text='<%# Eval("status_date") %>'></asp:Label><br />
                            <asp:Label ID="lbStatusG" runat="server" Text="Grund:&nbsp;"></asp:Label>
                            <asp:Label ID="lbStatusGV" runat="server" Text='<%# Eval("status_grund") %>'></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td class="frm_left" valign="middle" align="left" width="10px">
                          </td>
                          <td class="frm_left" valign="top">
                            Nebenwirkungen:
                          </td>
                          <td class="frm_leftn" valign="top" align="left">
                            <asp:Label ID="lbNw" runat="server" Text='<%# Eval("nw") %>'></asp:Label>
                            <br />
                            <asp:TextBox ID="tbNw" runat="server" TextMode="MultiLine" ReadOnly="true" Width="300px"
                              CssClass="text" Text='<%# Eval("nwtext") %>'></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td class="frm_left" valign="top" align="left" width="10px">
                          </td>
                          <td valign="top" class="frm_left">
                            Behandlungsverlauf:
                          </td>
                          <td class="frm_leftn" valign="middle" align="left">
                            <asp:TextBox ID="tbBhv" runat="server" TextMode="MultiLine" ReadOnly="true" Width="300px"
                              CssClass="text" Text='<%# Eval("bhv") %>'></asp:TextBox>
                          </td>
                        </tr>
                      </table>
                    </asp:View>
                  </asp:MultiView>
                  <table border="0" width="600px" class="frmt_green_99CC99">
                    <tr>
                      <td class="frm_left" valign="middle" align="left" width="10px">
                      </td>
                      <td valign="top" class="frm_left">
                        <asp:Label ID="Label9" runat="server" Text="Nächster Kontakt:" Width="250px"></asp:Label>
                      </td>
                      <td class="frm_leftn" valign="middle" align="left">
                        <asp:Label ID="lbNeko" runat="server" Text='<%# Eval("neko") %>' Width="300px"></asp:Label>
                      </td>
                    </tr>
                  </table>
              </ItemTemplate>
              <FooterTemplate>
                </table></FooterTemplate>
              <SeparatorTemplate>
              </SeparatorTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="sqlDSDksKontakt" runat="server" ConnectionString="<%$ ConnectionStrings:SqlDb %>"
              SelectCommand="SELECT [btkid], [patid],[btkdate], [art],[status],[status_grund], [status_date], [medikament],[nw],[nwtext], [bhv], [abgeschlossen],[neko] FROM [btk] WHERE ([patid] = @patid) ORDER BY [btkdate] DESC,[btkid] DESC">
              <SelectParameters>
                <asp:QueryStringParameter Name="patid" QueryStringField="patid" Type="Int32" />
              </SelectParameters>
            </asp:SqlDataSource>
          </asp:View>
        </asp:MultiView>
      </asp:View>
    </asp:MultiView>
  </ContentTemplate>
</asp:UpdatePanel>