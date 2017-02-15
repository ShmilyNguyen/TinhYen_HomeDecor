<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="cms-list.aspx.cs" Inherits="WKS.DMS.WEB.CMS.cms_list" %>
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
                
                <td>
                         <asp:Button runat="server" id="btnCreateNew" Text="Tạo thông báo" 
                             PostBackUrl="~/CMS/cms-edit.aspx" />
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
                DataKeyNames="cms_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="customer_id" FilterControlAltText="Filter item_id column" 
                        DataField="cms_id" Display="false">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn HeaderText="Tiêu đề"
                        UniqueName="title" FilterControlAltText="Filter store_name column" 
                        DataField="title">
                    </telerik:GridBoundColumn>


                    <telerik:GridBoundColumn DataField="from" 
                        FilterControlAltText="Filter item_code column" HeaderText="Từ ngày" 
                        UniqueName="from">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="to" 
                        FilterControlAltText="Filter to column" HeaderText="Đến ngày" 
                        UniqueName="to">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="priority" 
                        FilterControlAltText="Filter to column" HeaderText="Ưu tiên" 
                        UniqueName="priority">
                    </telerik:GridBoundColumn>

                    
                    <telerik:GridButtonColumn CommandName="Update" Text="Edit" UniqueName="UpdateColumn" HeaderText="Update Command Column">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>


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

