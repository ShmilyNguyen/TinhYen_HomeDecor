<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="payment-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.payment_list" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">



    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">

<asp:Button ID="Button1" runat="server" Text="Tạo mới" PostBackUrl="~/Forms/Payment/payment-edit-2.aspx"  />

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
                DataKeyNames="payment_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn DataField="payment_id" Display="False" FilterControlAltText="Filter payment_id column" HeaderText="payment_id" UniqueName="payment_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter store_name column" HeaderText="Nhà phân phối" UniqueName="store_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_id" FilterControlAltText="Filter store_id column" UniqueName="store_id" Visible="False" Display="False">
                    </telerik:GridBoundColumn>
                   
                       <telerik:GridBoundColumn FilterControlAltText="Filter customer_name column" HeaderText="Khách hàng" UniqueName="customer_name" DataField="customer_name" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn> 


                     <telerik:GridBoundColumn DataField="payment_date" 
                        FilterControlAltText="Filter item_name column" HeaderText="Ngày" 
                        SortExpression="Name" UniqueName="payment_date">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>


                    <telerik:GridBoundColumn DataField="payment_code" 
                        FilterControlAltText="Filter item_code column" HeaderText="Chứng từ thanh toán" 
                        UniqueName="payment_code">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="note" 
                        FilterControlAltText="Filter note column" HeaderText="Ghi chú" 
                        UniqueName="note">
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
