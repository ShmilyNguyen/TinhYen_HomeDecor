<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-rule-1.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Promo.promo_rule_1" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
    <style type="text/css">
.RadGrid_Office2010Blue{border:1px solid #8ba0bc;background:#fff;color:#384e73;font:normal 12px "Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}
        .RadGrid_Office2010Blue {
            border: 1px solid #8ba0bc;
            background: #fff;
            color: #384e73;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

            .RadGrid_Office2010Blue .rgMasterTable{font:normal 12px "Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadGrid .rgMasterTable{border-collapse:separate;border-spacing:0}

        .RadGrid .rgMasterTable {
            border-collapse: separate;
            border-spacing: 0;
        }

            .RadGrid_Office2010Blue .rgMasterTable {
                font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
                line-height: 16px;
            }

        .RadGrid_Office2010Blue .rgCommandRow{background:#bdcbde 0 -2100px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');color:#333}

        .RadGrid_Office2010Blue .rgCommandRow {
            background: #bdcbde 0 -2100px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
            color: #333;
        }

        .RadGrid_Office2010Blue thead .rgCommandCell{border-bottom:1px solid #8ba0bc}

        .RadGrid_Office2010Blue thead .rgCommandCell {
            border-bottom: 1px solid #8ba0bc;
        }

        .RadGrid_Office2010Blue .rgCommandCell{border:0;padding:0}

        .RadGrid_Office2010Blue .rgCommandCell {
            border: 0;
            padding: 0;
        }

        .RadGrid_Office2010Blue .rgHeaderWrapper{border:solid #8ba0bc;border-width:0 0 1px 1px;background:#bdcbde 0 -2300px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png')}

        .RadGrid_Office2010Blue .rgHeaderWrapper {
            border: solid #8ba0bc;
            border-width: 0 0 1px 1px;
            background: #bdcbde 0 -2300px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
        }

        .RadGrid_Office2010Blue .rgHeaderDiv{border-right-color:#9babc2}.RadGrid_Office2010Blue .rgHeaderDiv{background:#d6e5f3 0 -8050px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png')}

        .RadGrid_Office2010Blue .rgHeaderDiv {
            background: #d6e5f3 0 -8050px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
        }

        .RadGrid_Office2010Blue .rgHeaderDiv {
            border-right-color: #9babc2;
        }

        .RadGrid table.rgMasterTable tr .rgExpandCol{padding-left:0;padding-right:0;text-align:center}

        .RadGrid table.rgMasterTable tr .rgExpandCol {
            padding-left: 0;
            padding-right: 0;
            text-align: center;
        }

        .RadGrid_Office2010Blue .rgHeader:first-child{border-left-width:0;padding-left:8px}.RadGrid .rgClipCells .rgHeader{overflow:hidden}

        .RadGrid .rgClipCells .rgHeader {
            overflow: hidden;
        }

        .RadGrid_Office2010Blue .rgHeader:first-child {
            border-left-width: 0;
            padding-left: 8px;
        }

        .RadGrid_Office2010Blue .rgHeader{color:#384e73}.RadGrid_Office2010Blue .rgHeader{border:solid #8ba0bc;border-width:0 0 1px 1px;background:#bdcbde 0 -2300px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png')}.RadGrid .rgHeader{padding-top:5px;padding-bottom:4px;text-align:left;font-weight:normal}.RadGrid .rgHeader{padding-left:7px;padding-right:7px}.RadGrid .rgHeader{cursor:default}

        .RadGrid .rgHeader {
            cursor: default;
        }

        .RadGrid .rgHeader {
            padding-left: 7px;
            padding-right: 7px;
        }

        .RadGrid .rgHeader {
            padding-top: 5px;
            padding-bottom: 4px;
            text-align: left;
            font-weight: normal;
        }

        .RadGrid_Office2010Blue .rgHeader {
            border: solid #8ba0bc;
            border-width: 0 0 1px 1px;
            background: #bdcbde 0 -2300px repeat-x url('mvwres://Telerik.Web.UI.Skins, Version=2015.1.225.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2010Blue.Grid.sprite.png');
        }

        .RadGrid_Office2010Blue .rgHeader {
            color: #384e73;
        }

        .RadGrid_Office2010Blue .rgFilterRow{background:#dae7f5}

        .RadGrid_Office2010Blue .rgFilterRow {
            background: #dae7f5;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div>
       

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>

    </div>
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
                <strong>Định nghĩa điều kiện tham gia CTKM&nbsp;</strong></td>
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
                                    <td>Tham chiếu</td>
                                    <td>Đối tượng&nbsp;</td>
                                    <td>Category 1</td>
                                    <td>Category 2</td>
                                    <td>Category 3</td>
                                    <td>Size</td>
                                    <td>And / Or</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtRefID1" runat="server" Width="50px">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxObject" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains"  Width="300px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxCategory1" runat="server" DataTextField="item_category_code" DataValueField="item_category_code" DropDownAutoWidth="Enabled" Filter="Contains" Width="150px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxCategory2" runat="server" DataTextField="item_category_code" DataValueField="item_category_code" DropDownAutoWidth="Enabled" Filter="Contains" Width="150px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxCategory3" runat="server" DataTextField="item_category_code" DataValueField="item_category_code" DropDownAutoWidth="Enabled" Filter="Contains" Width="150px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxSize" runat="server" DataTextField="object_size" DataValueField="object_size" DropDownAutoWidth="Enabled" Filter="Contains" Width="80px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxOperator" runat="server" DropDownAutoWidth="Enabled" Filter="Contains" Width="80px">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="OR" Value="OR" />
                                                <telerik:RadComboBoxItem runat="server" Text="AND" Value="AND" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd1" runat="server" Text="Thêm" OnClick="btnAdd1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                      <div>

                          <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Height="200px" OnDeleteCommand="RadGrid1_DeleteCommand" PagerStyle-PageButtonCount="5" RenderMode="Auto" ResolvedRenderMode="Classic" Skin="Office2010Blue" OnNeedDataSource="RadGrid1_NeedDataSource">
                              <GroupingSettings ShowUnGroupButton="true" />
                              <ExportSettings ExportOnlyData="true" IgnorePaging="true">
                              </ExportSettings>
                              <MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" DataKeyNames="row_id" InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" TableLayout="Fixed">
                                  <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                                  <Columns>
                                      <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="row_id">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="ref_id" FilterControlAltText="Filter group_code column" HeaderText="Số tham chiếu" UniqueName="ref_id">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="item_id" Display="False" FilterControlAltText="Filter item_code column" HeaderText="object id" UniqueName="object_id">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="object_name" FilterControlAltText="Filter object_name column" HeaderText="Đối tượng" UniqueName="object_name">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="category1" FilterControlAltText="Filter item_code column" HeaderText="Category 1" UniqueName="category1">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn FilterControlAltText="Filter category2 column" HeaderText="Category 2" UniqueName="category2" DataField="category2">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn FilterControlAltText="Filter category3 column" HeaderText="Category 3" UniqueName="category3" DataField="category3">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="size" FilterControlAltText="Filter item_name column" HeaderText="Size" SortExpression="Name" UniqueName="size">
                                          <HeaderStyle Width="150px" />
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="operator" FilterControlAltText="Filter category_id1 column" HeaderText="AND / OR" UniqueName="operator">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridButtonColumn CommandName="Delete" HeaderText="Xóa" Text="Delete" UniqueName="DeleteColumn">
                                          <HeaderStyle Width="70px" />
                                      </telerik:GridButtonColumn>
                                  </Columns>
                                 
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
            <td>&nbsp;</td>
            <td><strong>Định nghĩa hàng tặng</strong></td>
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
                                    <td>Tham chiếu</td>
                                    <td>GT/SL</td>
                                    <td>Từ(GT/SL)</td>
                                    <td>Đến(GT/SL)</td>
                                    <td>Hàng KM</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>SL KM</td>
                                    <td>Nhân tố</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRefID2" runat="server" Width="50px" TextMode="Number">1</asp:TextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxPromoType" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="STOCK UOM" Value="STOCK UOM" />
                                                <telerik:RadComboBoxItem runat="server" Text="AMOUNT" Value="AMOUNT" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFrom" runat="server" TextMode="Number" Width="150px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTo" runat="server" TextMode="Number" Width="150px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxItem" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="300px" OnSelectedIndexChanged="cbxItem_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtPromoQty" runat="server" TextMode="Number" Width="50px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStep" runat="server" TextMode="Number" Width="150px">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd2" runat="server" OnClick="btnAdd2_Click" Text="Thêm" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Height="400px" OnDeleteCommand="RadGrid2_DeleteCommand" PagerStyle-PageButtonCount="5" RenderMode="Auto" ResolvedRenderMode="Classic" Skin="Office2010Blue" OnNeedDataSource="RadGrid2_NeedDataSource">
                                <GroupingSettings ShowUnGroupButton="true" />
                                <ExportSettings ExportOnlyData="true" IgnorePaging="true">
                                </ExportSettings>
                                <MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" DataKeyNames="row_id" InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" TableLayout="Fixed">
                                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="row_id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ref_id" FilterControlAltText="Filter group_code column" HeaderText="Số tham chiếu" UniqueName="ref_id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="object_id" Display="False" FilterControlAltText="Filter item_code column" HeaderText="object id" UniqueName="object_id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="promo_type" FilterControlAltText="Filter item_code column" HeaderText="GT/SL" UniqueName="promo_type">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="from_value" FilterControlAltText="Filter category2 column" HeaderText="Từ (SL/GT)" UniqueName="from_value">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="to_value" FilterControlAltText="Filter category3 column" HeaderText="Đến (SL/GT)" UniqueName="to_value">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="object_name" FilterControlAltText="Filter object_name column" HeaderText="Đối tượng" UniqueName="object_name">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="line_discount" FilterControlAltText="Filter item_name column" HeaderText="Chiết khấu" UniqueName="line_discount" SortExpression="Name" Display="False">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="promo_qty" FilterControlAltText="Filter category_id1 column" HeaderText="SL KM" UniqueName="promo_qty">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="step" FilterControlAltText="Filter step column" HeaderText="Nhân tố" UniqueName="step">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Delete" HeaderText="Xóa" Text="Delete" UniqueName="DeleteColumn">
                                            <HeaderStyle Width="70px" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    
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

