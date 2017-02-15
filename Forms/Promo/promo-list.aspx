<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.promo_list" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


        <table>
                <tr>
                    
                    <td>Từ khóa tìm kiếm : </td>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
                    <td></td>
                    <td>
                         <asp:Button ID="Button1" runat="server" Text="Tạo mới" 
         PostBackUrl="~/Forms/Promo/promo-edit.aspx" Width="100px"  />
                </td>
                </tr>
            </table>



        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" 
            RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" 
            Height="800px" ResolvedRenderMode="Classic" Skin="Office2010Blue" PageSize="50">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed"
                DataKeyNames="promo_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="promo_id" FilterControlAltText="Filter item_id column" 
                        DataField="promo_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="promo_code" 
                        FilterControlAltText="Filter item_code column" HeaderText="Code" 
                        UniqueName="promo_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="promo_name" 
                        FilterControlAltText="Filter item_name column" HeaderText="Name" 
                        SortExpression="Name" UniqueName="promo_name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="start_date_gmt" 
                        FilterControlAltText="Filter store_name column" HeaderText="Từ ngày" 
                        UniqueName="start_date_gmt">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="end_date_gmt" 
                        FilterControlAltText="Filter category_id1 column" UniqueName="end_date_gmt" 
                        HeaderText="Đến ngày">
                    </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="promo_status" FilterControlAltText="Filter promo_status column" HeaderText="Trạng thái" UniqueName="promo_status">
                    </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Update" Text="Edit" UniqueName="UpdateColumn" HeaderText="Update Command Column">
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
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("Button") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
