﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="route-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.route_list" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">

        <asp:Button ID="Button1" runat="server" Text="Tạo mới" PostBackUrl="~/Forms/Route/route-edit.aspx" />

        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource"
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand"
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True"
            RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand"
            Height="800px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="route_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter store_name column" HeaderText="Nhà phân phối" UniqueName="store_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_id" FilterControlAltText="Filter store_id column" UniqueName="store_id" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="route_id" FilterControlAltText="Filter item_id column"
                        DataField="route_id" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="route_code"
                        FilterControlAltText="Filter item_code column" HeaderText="Code"
                        UniqueName="route_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="route_name"
                        FilterControlAltText="Filter item_name column" HeaderText="Name"
                        SortExpression="Name" UniqueName="route_name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn CommandName="Update" HeaderText="Cập nhật" Text="Xem" UniqueName="UpdateColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" FormatString="" HeaderText="NPP" />
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" SortOrder="Descending" />
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
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