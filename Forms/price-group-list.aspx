<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="price-group-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.price_group_list" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


        <div>


            <table>
                <tr>
                    <td>Từ khóa tìm kiếm : </td>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="Tạo mới" OnClick="btnAddNew_Click"  />
                    </td>
                </tr>
            </table>

        </div>


        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" Height="800px" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False"
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="price_group_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" ShowGroupFooter="True">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn DataField="price_group_id" FilterControlAltText="Filter price_group_id column" HeaderText="Nhóm giá ID" UniqueName="price_group_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Tên nhóm giá"
                        UniqueName="price_group_name" FilterControlAltText="Filter price_group_name" DataField="price_group_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="fromdate" FilterControlAltText="Filter fromdate column" HeaderText="Từ ngày" UniqueName="fromdate">
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="todate" FilterControlAltText="Filter todate column" HeaderText="Đến ngày" UniqueName="todate">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="note" FilterControlAltText="Filter note column" HeaderText="Ghi chú" SortExpression="note" UniqueName="note">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                   

                    <telerik:GridBoundColumn DataField="is_active" FilterControlAltText="Filter is_active column" HeaderText="Hoạt động" UniqueName="is_active">
                    </telerik:GridBoundColumn>


                    
                        <telerik:GridButtonColumn CommandName="Update" Text="Edit" UniqueName="UpdateColumn" HeaderText="Edit">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

               

            </MasterTableView>
            <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true" AllowDragToGroup="true">
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
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("Button") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>


