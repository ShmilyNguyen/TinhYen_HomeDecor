<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="Store-List.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Store_List" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" 
            OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand" 
            OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" 
            RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" 
            Height="800px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue" PageSize="50">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="store_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="id" FilterControlAltText="Filter item_id column" DataField="store_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_code" FilterControlAltText="Filter item_code column" HeaderText="Code" UniqueName="code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter item_name column" HeaderText="Name" SortExpression="Name" UniqueName="name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="location" FilterControlAltText="Filter location column" HeaderText="Tỉnh / TP" UniqueName="location">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="region" FilterControlAltText="Filter category_id1 column" UniqueName="region" HeaderText="Region">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="area" FilterControlAltText="Filter category_id2 column" UniqueName="area" HeaderText="Area">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="province" FilterControlAltText="Filter province column" UniqueName="province">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_address" FilterControlAltText="Filter store_address column" HeaderText="Địa chỉ" UniqueName="store_address">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="tax" FilterControlAltText="Filter tax column" HeaderText="MST" UniqueName="tax">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="phone" FilterControlAltText="Filter phone column" HeaderText="Điện thoại" UniqueName="phone">
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="prefix_code" FilterControlAltText="Filter prefix_code column" HeaderText="Mã tiền tố" UniqueName="prefix_code">
                    </telerik:GridBoundColumn>

                      <telerik:GridBoundColumn DataField="create_date" FilterControlAltText="Filter create_date column" HeaderText="Ngày tạo" UniqueName="create_date">
                    </telerik:GridBoundColumn>



                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="region_id" Display="False" FilterControlAltText="Filter region_id column" UniqueName="region_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="area_id" Display="False" FilterControlAltText="Filter area_id column" UniqueName="area_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="province_id" Display="False" FilterControlAltText="Filter province_id column" UniqueName="province_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="is_Active" 
                        FilterControlAltText="Filter active column" HeaderText="Hoạt động" 
                        UniqueName="active" Display="False">
                    </telerik:GridBoundColumn>
                </Columns>

                 <EditFormSettings UserControlName="Store-Edit.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
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