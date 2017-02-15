<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-item-combo.aspx.cs" Inherits="WKS.DMS.WEB.Forms.promo_item_combo" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .RadGrid_Office2010Blue {
            border: 1px solid #8ba0bc;
            background: #fff;
            color: #384e73;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

            .RadGrid_Office2010Blue .rgMasterTable {
                font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
                line-height: 16px;
            }

        .RadGrid .rgMasterTable {
            border-collapse: separate;
            border-spacing: 0;
        }

        .RadGrid_Office2010Blue .rgCommandRow {
            background: #bdcbde 0 -2100px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
            color: #333;
        }

        .RadGrid_Office2010Blue thead .rgCommandCell {
            border-bottom: 1px solid #8ba0bc;
        }

        .RadGrid_Office2010Blue .rgCommandCell {
            border: 0;
            padding: 0;
        }

        .RadGrid_Office2010Blue .rgHeaderWrapper {
            border: solid #8ba0bc;
            border-width: 0 0 1px 1px;
            background: #bdcbde 0 -2300px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
        }

        .RadGrid_Office2010Blue .rgHeaderDiv {
            border-right-color: #9babc2;
        }

        .RadGrid_Office2010Blue .rgHeaderDiv {
            background: #d6e5f3 0 -8050px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
        }

        .RadGrid table.rgMasterTable tr .rgExpandCol {
            padding-left: 0;
            padding-right: 0;
            text-align: center;
        }

        .RadGrid_Office2010Blue .rgHeader:first-child {
            border-left-width: 0;
            padding-left: 8px;
        }

        .RadGrid .rgClipCells .rgHeader {
            overflow: hidden;
        }

        .RadGrid_Office2010Blue .rgHeader {
            color: #384e73;
        }

        .RadGrid_Office2010Blue .rgHeader {
            border: solid #8ba0bc;
            border-width: 0 0 1px 1px;
            background: #bdcbde 0 -2300px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
        }

        .RadGrid .rgHeader {
            padding-top: 5px;
            padding-bottom: 4px;
            text-align: left;
            font-weight: normal;
        }

        .RadGrid .rgHeader {
            padding-left: 7px;
            padding-right: 7px;
        }

        .RadGrid .rgHeader {
            cursor: default;
        }

        .RadGrid_Office2010Blue .rgFilterRow {
            background: #dae7f5;
        }
    .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table>
        <tr>
            <td nowrap="nowrap">ID</td>
            <td>
                <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="#FFCC66"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">Code</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" Width="400px" BackColor="#FFCC66" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">Tên CTKM</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="400px" BackColor="#FFCC66"
                    TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">&nbsp;</td>
            <td>
                <em>Định nghĩa hàng bán và hàng tặng hỗn hợp</em></td>
        </tr>
        <tr>
            <td nowrap="nowrap">&nbsp;</td>
            <td>
                Định nghĩa hàng bán&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>

                <div>

                    <telerik:RadCodeBlock runat="server">
                        <script type="text/javascript">
                            function onRequestStart(sender, args) {
                                if (args.get_eventTarget().indexOf("Button") >= 0) {
                                    args.set_enableAjax(false);
                                }
                            }
                        </script>
                    </telerik:RadCodeBlock>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


                        <div>
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Nhóm</td>
                                    <td>Hàng hóa</td>
                                    <td>Số lượng</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtGroup1" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBox1" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains"  Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty1" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd1" runat="server" Text="Thêm" OnClick="btnAdd1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                      <div>
                        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                            
                            
                            OnDeleteCommand="RadGrid1_DeleteCommand"
                           
                            AllowPaging="True" AllowSorting="True"
                            RenderMode="Auto" GroupPanelPosition="Top" 
                            Height="200px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
                            <GroupingSettings ShowUnGroupButton="true" />
                            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                            <MasterTableView
                                AllowFilteringByColumn="true" TableLayout="Fixed"
                                DataKeyNames="row_id" CommandItemDisplay="Top"
                                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False"
                                    ShowRefreshButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="row_id">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="group_code"
                                        FilterControlAltText="Filter group_code column" HeaderText="Group Code"
                                        UniqueName="group_code">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_id"
                                        FilterControlAltText="Filter item_code column" UniqueName="item_id"
                                        HeaderText="Item ID" Display="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_code"
                                        FilterControlAltText="Filter item_code column" HeaderText="Mã hàng hóa" UniqueName="item_code">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_name"
                                        FilterControlAltText="Filter item_name column" UniqueName="item_name"
                                        HeaderText="Tên hàng hóa" SortExpression="Name">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="qty"
                                        FilterControlAltText="Filter category_id1 column" UniqueName="qty" 
                                        HeaderText="Số lượng">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Xóa">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridButtonColumn>
                                </Columns>

                                <EditFormSettings UserControlName="promo-row-edit-tanghang.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="EditCommandColumn2">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            </ClientSettings>
                            <PagerStyle PageButtonCount="5" />
                            <FilterMenu RenderMode="Auto">
                            </FilterMenu>
                            <HeaderContextMenu RenderMode="Auto">
                            </HeaderContextMenu>
                        </telerik:RadGrid>

                          </div>
                    </telerik:RadAjaxPanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Định nghĩa hàng tặng</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <div>
                    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                        <script type="text/javascript">

                            function onRequestStart(sender, args) {
                                if (args.get_eventTarget().indexOf("Button") >= 0) {
                                    args.set_enableAjax(false);
                                }
                            }
                        </script>
                    </telerik:RadCodeBlock>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" ClientEvents-OnRequestStart="onRequestStart" CssClass="grid_wrapper">
                        <div>
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Nhóm</td>
                                    <td>Hàng hóa</td>
                                    <td>Số lượng</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtGroup2" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBox2" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains"  Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty2" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd2" runat="server" OnClick="btnAdd2_Click" Text="Thêm" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Height="200px" OnDeleteCommand="RadGrid2_DeleteCommand" PagerStyle-PageButtonCount="5" RenderMode="Auto" ResolvedRenderMode="Classic" Skin="Office2010Blue">
                                <GroupingSettings ShowUnGroupButton="true" />
                                <ExportSettings ExportOnlyData="true" IgnorePaging="true">
                                </ExportSettings>
                                <MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" DataKeyNames="row_id" InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" TableLayout="Fixed">
                                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="row_id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="group_code" FilterControlAltText="Filter group_code column" HeaderText="Group Code" UniqueName="group_code">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="item_id" Display="False" FilterControlAltText="Filter item_code column" HeaderText="Item ID" UniqueName="item_id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Mã hàng hóa" UniqueName="item_code">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column" HeaderText="Tên hàng hóa" SortExpression="Name" UniqueName="item_name">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="qty" FilterControlAltText="Filter category_id1 column" HeaderText="Số lượng" UniqueName="qty">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Delete" HeaderText="Xóa" Text="Delete" UniqueName="DeleteColumn">
                                            <HeaderStyle Width="70px" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="WebUserControl" UserControlName="promo-row-edit-tanghang.ascx">
                                        <EditColumn UniqueName="EditCommandColumn2">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings AllowColumnHide="true" AllowColumnsReorder="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <PagerStyle PageButtonCount="5" />
                                <FilterMenu RenderMode="Auto">
                                </FilterMenu>
                                <HeaderContextMenu RenderMode="Auto">
                                </HeaderContextMenu>
                            </telerik:RadGrid>
                        </div>
                    </telerik:RadAjaxPanel>
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnExit" runat="server" Text="Quay về"
                    Width="100px" OnClick="btnExit_Click" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
