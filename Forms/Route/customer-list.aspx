<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="customer-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.customer_list" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    <div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">

<div>


        <table>
            <tr>
                <td style="color:#fff">Từ khóa tìm kiếm : </td>
                <td>
                    <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
                <td>
                         <asp:Button runat="server" id="btnCreateNew" Text="Tạo mới KH" 
                             PostBackUrl="~/Forms/Route/customer-edit.aspx" />
                </td>
            </tr>
        </table>

    </div>

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
            <MasterTableView TableLayout="Fixed"
                DataKeyNames="customer_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="customer_id" FilterControlAltText="Filter item_id column" 
                        DataField="customer_id" Display="false">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn HeaderText="Nhà phân phối"
                        UniqueName="store_name" FilterControlAltText="Filter store_name column" 
                        DataField="store_name">
                    </telerik:GridBoundColumn>


                    <telerik:GridBoundColumn DataField="customer_code" 
                        FilterControlAltText="Filter item_code column" HeaderText="Code" 
                        UniqueName="customer_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="customer_name" 
                        FilterControlAltText="Filter sellin_id column" HeaderText="Khách hàng" 
                        UniqueName="customer_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="add_number" 
                        FilterControlAltText="Filter add_number column" HeaderText="Số nhà" 
                        UniqueName="add_number">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="address" 
                        FilterControlAltText="Filter sellin_code column" HeaderText="Địa chỉ" 
                        UniqueName="address">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="phone" 
                        FilterControlAltText="Filter phone column" HeaderText="Điện thoại" 
                        UniqueName="phone">
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="created_date" 
                        FilterControlAltText="Filter created_date column" HeaderText="Ngày tạo" 
                        UniqueName="created_date">
                    </telerik:GridBoundColumn>

                 
                    
                    <telerik:GridCheckBoxColumn DataField="active" DataType="System.Boolean" FilterControlAltText="Filter active column" HeaderText="Hoạt động" UniqueName="active">
                    </telerik:GridCheckBoxColumn>
                    
                    <telerik:GridButtonColumn CommandName="Update" Text="Edit" UniqueName="UpdateColumn" HeaderText="Update Command Column">
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


                 <EditFormSettings UserControlName="Store-Edit.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>

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
    </div>
</asp:Content>

