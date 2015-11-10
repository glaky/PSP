<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="overviewPat.aspx.cs" Inherits="PSP.general.overviewPat" MasterPageFile="~/PSP.master" %>

<asp:Content ID="status" ContentPlaceHolderID="cph_status" runat="Server">
    <asp:Label ID="lbRole" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;|&nbsp;
  <asp:Label ID="lbTitel" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
  <asp:Label ID="lbForename" runat="server" Text="" CssClass="Text12b"></asp:Label>&nbsp;
  <asp:Label ID="lbName" runat="server" Text="" CssClass="Text12b"></asp:Label>
</asp:Content>
<asp:Content ID="overviewPat_content" ContentPlaceHolderID="cph_main" runat="Server">

    <script type="text/javascript" language="javascript">
  <!--
    function SelectAll(CheckBox) {
        TotalChkBx = parseInt('<%=this.gv_patlist.Rows.Count%>');
        var TargetBaseControl = document.getElementById('<%=this.gv_patlist.ClientID%>');
        var TargetChildControl = "cbSingle";
        var Inputs = TargetBaseControl.getElementsByTagName('input');
        for (var iCount = 0; iCount < Inputs.length; ++iCount) {
            if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[iCount].checked = CheckBox.checked;
        }
    }

    function SelectDeSelectHeader(CheckBox) {
        TotalChkBx = parseInt('<%= this.gv_patlist.Rows.Count %>');
          var TargetBaseControl = document.getElementById('<%= this.gv_patlist.ClientID %>');
          var TargetChildControl = "cbSingle";
          var TargetHeaderControl = "cbAll";
          var Inputs = TargetBaseControl.getElementsByTagName("input");
          var flag = false;
          var HeaderCheckBox;
          for (var iCount = 0; iCount < Inputs.length; ++iCount) {
              if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) >= 0)
                  HeaderCheckBox = Inputs[iCount];
              if (Inputs[iCount] != CheckBox && Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) == -1) {
                  if (CheckBox.checked) {
                      if (!Inputs[iCount].checked) {
                          flag = false;
                          HeaderCheckBox.checked = false;
                          return;
                      }
                      else
                          flag = true;
                  }
                  else if (!CheckBox.checked)
                      HeaderCheckBox.checked = false;
              }
          }
          if (flag)
              HeaderCheckBox.checked = CheckBox.checked
      }
      --> 
    </script>

    <div id="main">
        <asp:MultiView ID="mvMenu" runat="server">
            <asp:View ID="vwNrs" runat="server">
                <!--#include file="~/menue/left_menu_nrs.aspx"-->
            </asp:View>
            <asp:View ID="vwSec" runat="server">
                <!--#include file="~/menue/left_menu_sec.aspx"-->
            </asp:View>
            <asp:View ID="vwAss" runat="server">
                <!--#include file="~/menue/left_menu_ass.aspx"-->
            </asp:View>
        </asp:MultiView>
        <div id="board">
            <table width="730px" border="0" class="title">
                <tr>
                    <td align="left" class="Text14">Übersicht Patienten&nbsp;
                    </td>
                    <td align="right" class="Text10">
                        <asp:Label CssClass="Text12" ID="lb_countPat" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table width="730px" border="0" class="frmb_white">
                <tr>
                    <td width="10px">&nbsp;
                    </td>
                    <td>
                        <asp:Label CssClass="Text12b" ID="lbFName" runat="server" Text="Name:&nbsp;" Width="90px"></asp:Label>
                        <asp:TextBox CssClass="texts" Text="" ID="txFName" runat="server" Width="145px"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txFNameV" runat="server"></asp:TextBox>
                    </td>
                    <td width="70px">
                        <asp:Label CssClass="Text12b" ID="lbFID" runat="server" Text="ID:&nbsp;" Width="20px"></asp:Label>
                        <asp:TextBox CssClass="textss" Text="" ID="txFID" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txFIDV" runat="server"></asp:TextBox>
                    </td>
                    <td width="80px">
                        <asp:Label CssClass="Text12b" ID="lbFPlz" runat="server" Text="Plz:&nbsp;" Width="25px"></asp:Label>
                        <asp:TextBox CssClass="textss" Text="" ID="txFPlz" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txFPlzV" runat="server"></asp:TextBox>
                    </td>
                    <td width="140px">
                        <asp:Label CssClass="Text12b" ID="lbFOrt" runat="server" Text="Ort:&nbsp;" Width="25px"></asp:Label>
                        <asp:TextBox CssClass="textss" Text="" ID="txFOrt" runat="server" Width="100px"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txFOrtV" runat="server"></asp:TextBox>
                    </td>
                    <td width="150px">&nbsp;
                    </td>
                </tr>
            </table>
            <table width="730px" border="0" class="frmtb_white">
                <tr>
                    <td width="10px">&nbsp;
                    </td>
                    <td width="90px">
                        <asp:CheckBox ID="cbNurse" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbNurse" runat="server" Text="Nurse Service" Width="80px"></asp:Label>
                        <asp:TextBox Visible="false" ID="txFNrsSecV" runat="server"></asp:TextBox>
                    </td>
                    <td width="110px">
                        <asp:CheckBox ID="cbService" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbService" runat="server" Text="Service Center" Width="100px"></asp:Label>
                    </td>
                    <td width="15px">&nbsp;</td>
                    <td width="65px">
                        <asp:CheckBox ID="cbLfd" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbFLfd" runat="server" Text="Laufend" Width="55px"></asp:Label>
                        <asp:TextBox Visible="false" ID="txFLfdV" runat="server"></asp:TextBox>
                    </td>
                    <td width="85px">
                        <asp:CheckBox ID="cbAbge" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbFAbge" runat="server" Text="Abgebrochen" Width="75px"></asp:Label>
                    </td>
                    <td width="200px">&nbsp;
                    </td>
                </tr>
            </table>
            <table width="730px" border="0" class="frmtb_white">
                <tr>
                    <td width="15px">&nbsp;
                    </td>
                    <td valign="bottom">
                        <asp:Label ID="lbIntAbs" CssClass="Text12b" Width="90px" runat="server" Text="Anrufintervall:"></asp:Label>
                        <asp:DropDownList ID="ddl_intabs" runat="server" Width="115px">
                        </asp:DropDownList>
                        <asp:TextBox Visible="false" ID="txFIntabsV" runat="server"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txIntervallV" runat="server" Text="3000-12-31"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txIntervallL" Text="1900-01-01" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td width="10px">&nbsp;
                    </td>
                    <td width="90px">
                        <asp:CheckBox ID="cbPlegridy" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbPlegridy" runat="server" Text="Plegridy" Width="70px"></asp:Label>
                        <asp:TextBox Visible="false" ID="txFMedikamentV" runat="server"></asp:TextBox>
                    </td>
                    <td width="90x">
                        <asp:CheckBox ID="cbTecfidera" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbTecfidera" runat="server" Text="Tecfidera" Width="70px"></asp:Label>
                    </td>
                    <td width="90px">
                        <asp:CheckBox ID="cbAvonex" Checked="true" runat="server" /><asp:Label CssClass="Text12b"
                            ID="lbAvonex" runat="server" Text="Avonex" Width="70px"></asp:Label>
                    </td>
                    <td width="149px">&nbsp;
                    </td>
                </tr>
            </table>
            <table width="730px" border="0" class="frmtb_white">
                <tr>
                    <td width="10px">&nbsp;
                    </td>
                    <td valign="bottom">
                        <asp:CheckBox ID="cbIntervall" Checked="false" runat="server" Text="nur mit anstehenden Anrufen für das gewählte Intervall"
                            CssClass="Text12b" />
                    </td>
                    <td>
                        <asp:CheckBox ID="cbWechsel" Checked="true" runat="server" Text="nur mit anstehendem Wechsel zu Service Center" CssClass="Text12b"  />
                        <asp:TextBox Visible="false" ID="txWechselV" runat="server" Text="3000-12-31"></asp:TextBox>
                        <asp:TextBox Visible="false" ID="txWechselL" Text="1900-01-01" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td width="50px">&nbsp;
                    </td>
                </tr>
            </table>
            <table width="730px" border="0" class="frmt_white">
                <tr>
                    <td width="10px">&nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="cbAllOnOnePage" runat="server" OnCheckedChanged="cbAllOnOnePage_CheckedChanged" />
                        <asp:Label CssClass="Text12b" ID="Label1" runat="server" Text="Alle Patienten auf einer Seite anzeigen" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btFilter" runat="server" Text="Filter anwenden" CssClass="btFilter"
                            Width="450px" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gv_patlist" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataSourceID="SqlDB" CellPadding="4" GridLines="None"
                EmptyDataText="Anzahl Patienten mit diesem Filter: 0" ForeColor="#333333" BorderColor="#535D65"
                BorderWidth="1px" BorderStyle="Solid" Width="730px" OnRowDataBound="gv_dkskontakt_rdb"
                PageSize="30" DataKeyNames="patid">
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbAll" runat="server" onclick="SelectAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSingle" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="Text12b" />
                        <ItemStyle Width="10px" CssClass="Text11b"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="lnkSort" runat="server" CommandName="Sort" CommandArgument="patid"
                                Text="ID" ForeColor="White" CssClass="Text12b"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbPatID" runat="server" Text='<%# Eval("patid") %>' CssClass="Text11b"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="Text12b" Width="10px" />
                        <ItemStyle CssClass="Text11b" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="NameSort" runat="server" CommandName="Sort" CommandArgument="name"
                                Text="Name" ForeColor="White" CssClass="Text12b"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HyperLink CssClass="gv_hl" runat="server" ID="hl_details" NavigateUrl='<%# Eval("patid", "detailPat.aspx?patid={0}") %>'>
                                <asp:Label ID="lbName" runat="server" Text='<%# Eval("name") %>' CssClass="gv_hv"
                                    Font-Size="11px"></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="Text12b" />
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Vorname" HeaderText="Vorname" SortExpression="vorname">
                        <HeaderStyle CssClass="Text12b" Width="40px" />
                        <ItemStyle HorizontalAlign="Left" CssClass="Text11b" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="zustaendigkeit" HeaderText="Zustaendigkeit" SortExpression="zustaendigkeit">
                        <HeaderStyle CssClass="Text12b" Width="40px" />
                        <ItemStyle HorizontalAlign="Left" CssClass="Text11b" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tel" HeaderText="Telefon" SortExpression="tel">
                        <HeaderStyle CssClass="Text12b" Width="120px" />
                        <ItemStyle HorizontalAlign="Left" CssClass="Text11b" Width="95px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="intervall" HeaderText="Anrufintervall" SortExpression="intervall">
                        <HeaderStyle CssClass="Text12b" Width="90px" />
                        <ItemStyle HorizontalAlign="Left" CssClass="Text11b" Width="90px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Stat" SortExpression="status">
                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                        <HeaderStyle Width="10px" CssClass="Text12b" />
                        <ItemTemplate>
                            <asp:Image Width="16px" Height="16px" ID="img_status" runat="server" ToolTip='<%# Eval("status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Medikament" SortExpression="medikament">
                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                        <HeaderStyle Width="10px" CssClass="Text12b" />
                        <ItemTemplate>
                            <asp:Image Width="16px" Height="16px" ID="img_medikament" runat="server" ToolTip='<%# Eval("medikament") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="gv_row" />
                <PagerStyle BackColor="#f5a300" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle BackColor="#708090" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DDDDDD" />
            </asp:GridView>
            <asp:SqlDataSource OnSelected="sqldb_selected" ID="SqlDB" runat="server" ConnectionString="<%$ ConnectionStrings:SqlDb %>"
                SelectCommand="SELECT [patid],[gendate],[name], [vorname], [geschlecht], [gebdat], [tel], [email],[leko], [intervall], [status], [plz], [ort], [medikament], [zustaendigkeit], [consget] FROM [patdaten] WHERE [status] Like @filter_lfd AND [plz] LIKE @filter_plz AND [ort] LIKE @filter_ort AND [name] LIKE @filter_name AND [patid] like @filter_patid AND [intervall] LIKE @filter_intabs AND [medikament] like @filter_medikament AND [zustaendigkeit] like @filter_zustaendigkeit AND ([leko] between @filter_intervall_limit AND @filter_intervall) AND ([consget] between @filter_wechsel_bottom AND @filter_wechsel_top) ORDER BY [name], [vorname], [patid]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="PatID" Type="Int32" />
                    <asp:ControlParameter ControlID="txFNameV" Name="filter_name" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFIDV" Name="filter_patid" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txIntervallV" Name="filter_intervall" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txIntervallL" Name="filter_intervall_limit" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFPlzV" Name="filter_plz" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFOrtV" Name="filter_ort" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFIntabsV" Name="filter_intabs" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFLfdV" Name="filter_lfd" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFMedikamentV" Name="filter_medikament" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txFNrsSecV" Name="filter_zustaendigkeit" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txWechselV" Name="filter_wechsel_top" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txWechselL" Name="filter_wechsel_bottom" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <table border="0" width="730px" class="frm_white">
                <tr align="center">
                    <td align="left" style="width: 200px">
                        <asp:CheckBox ID="cbExport" runat="server" Text="Nur Übersicht exportieren" />
                    </td>
                    <td class="Text12" align="right">
                        <asp:Button OnClick="bt_export_click" ID="btSave" runat="server" CssClass="button"
                            Text="Auf dieser Seite ausgewählte Patientdaten exportieren" ToolTip="Datenexport für Offline-Verfügbarkeit"
                            Width="450px" ValidationGroup="patient" />&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
