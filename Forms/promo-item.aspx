<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-item.aspx.cs" Inherits="WKS.DMS.WEB.Forms.promo_item" %>

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
                <em>Định nghĩa hàng bán và hàng tặng cùng loại</em></td>
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
                    <telerik:RadAjaxPanel ID="RadAjaxPanel2" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
                        <telerik:RadGrid ID="RadGrid2" runat="server" PagerStyle-PageButtonCount="5"
                            OnNeedDataSource="RadGrid2_NeedDataSource"
                            OnItemDataBound="RadGrid2_ItemDataBound"
                            OnDeleteCommand="RadGrid2_DeleteCommand"
                            OnItemCommand="RadGrid2_ItemCommand"
                            AllowPaging="True" AllowSorting="True"
                            RenderMode="Auto" GroupPanelPosition="Top" 
                            Height="500px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
                            <GroupingSettings ShowUnGroupButton="true" />
                            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                            <MasterTableView
                                AllowFilteringByColumn="true" TableLayout="Fixed"
                                DataKeyNames="row_id" CommandItemDisplay="Top"
                                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False"
                                    ShowRefreshButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="row_id"
                                        UniqueName="row_id" FilterControlAltText="Filter item_id column"
                                        DataField="row_id" Display="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_id_src"
                                        FilterControlAltText="Filter item_code column" HeaderText="tem_id_src"
                                        UniqueName="item_id_src" Display="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_code_src"
                                        FilterControlAltText="Filter item_code column" UniqueName="item_code_src"
                                        HeaderText="Item Code Src">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_name_src"
                                        FilterControlAltText="Filter item_name column" HeaderText="Item Name Src"
                                        SortExpression="Name" UniqueName="item_name_src">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_src"
                                        FilterControlAltText="Filter category_id1 column" UniqueName="vol_src"
                                        HeaderText="Vol Src">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_id_des"
                                        FilterControlAltText="Filter item_id_des column" UniqueName="item_id_des"
                                        HeaderText="Item ID Des" Display="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_code_des"
                                        FilterControlAltText="Filter item_code_des column" HeaderText="Item Code Des"
                                        UniqueName="item_code_des">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_name_des"
                                        FilterControlAltText="Filter item_name_des column" HeaderText="Item Name Des"
                                        UniqueName="item_name_des">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_des"
                                        FilterControlAltText="Filter category_id2 column" HeaderText="Vol Des"
                                        UniqueName="vol_des">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_src2" FilterControlAltText="Filter vol_src2 column" HeaderText="Vol Src 2" UniqueName="vol_src2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_des2" FilterControlAltText="Filter vol_des2 column" HeaderText="Vol Des 2" UniqueName="vol_des2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_src3" FilterControlAltText="Filter vol_src3 column" HeaderText="Vol Src 3" UniqueName="vol_src3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_des3" FilterControlAltText="Filter vol_des3 column" HeaderText="Vol Des 3" UniqueName="vol_des3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_src4" FilterControlAltText="Filter vol_src4 column" HeaderText="Vol Src 4" UniqueName="vol_src4">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vol_des4" FilterControlAltText="Filter vol_des4 column" HeaderText="Vol Des 4" UniqueName="vol_des4">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridEditCommandColumn HeaderText="Sửa" UniqueName="EditColumn">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn CommandName="Delete" HeaderText="Xóa" Text="Delete" UniqueName="DeleteColumn">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="item_id_des2" Display="False" FilterControlAltText="Filter item_id_des2 column" UniqueName="item_id_des2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_id_des3" Display="False" FilterControlAltText="Filter item_id_des3 column" UniqueName="item_id_des3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_id_des4" Display="False" FilterControlAltText="Filter item_id_des4 column" UniqueName="item_id_des4">
                                    </telerik:GridBoundColumn>
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
                    </telerik:RadAjaxPanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
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