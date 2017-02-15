<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="route-sub-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Route.route_sub_list" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">




    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


  <asp:Button ID="Button1" runat="server" Text="Tạo mới" PostBackUrl="~/Forms/Route/route-sub-edit.aspx"  />

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
                DataKeyNames="route_sub_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn DataField="route_sub_id" FilterControlAltText="Filter item_name column" HeaderText="Route Sub ID" UniqueName="route_sub_id" Display="False" SortExpression="Name"  >
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="route_code" 
                        FilterControlAltText="Filter item_code column" HeaderText="Code" 
                        UniqueName="route_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="route_sub_code" 
                        FilterControlAltText="Filter route_sub_code column" HeaderText="Route Sub Code" UniqueName="route_sub_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="route_sub_name" FilterControlAltText="Filter column column" HeaderText="Route Sub Name" UniqueName="route_sub_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="employee_name" FilterControlAltText="Filter employee_name column" HeaderText="Nhân viên phụ trách" UniqueName="employee_name">
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
                              <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" FormatString="" HeaderText="" />
                          </GroupByFields>
                      </telerik:GridGroupByExpression>
                     <telerik:GridGroupByExpression>
                         <SelectFields>
                             <telerik:GridGroupByField FieldAlias="route_code" FieldName="route_code" FormatString="" HeaderText="Route Code" />
                         </SelectFields>
                         <GroupByFields>
                             <telerik:GridGroupByField FieldAlias="route_code" FieldName="route_code" SortOrder="Descending" />
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
