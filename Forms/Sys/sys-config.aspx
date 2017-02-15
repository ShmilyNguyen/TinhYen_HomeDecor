<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="sys-config.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Sys.sys_config" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource"
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand"
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True"
            RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand"
            Height="500px" ResolvedRenderMode="Classic" Skin="Office2010Blue" PageSize="50">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False"
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage"
                ShowFooter="True">
                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="id" FilterControlAltText="Filter row_id column"
                        DataField="id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="param_key"
                        FilterControlAltText="Filter item_id column" HeaderText="Param Key"
                        UniqueName="param_key" >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="param_value"
                        FilterControlAltText="Filter item_code column" HeaderText="Param Value"
                        UniqueName="param_value">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter qty_saleout column"
                        HeaderText="Note" UniqueName="note" DataField="note">
                    </telerik:GridBoundColumn>


                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                </Columns>

                <EditFormSettings UserControlName="sys-config-edit.ascx"
                    EditFormType="WebUserControl">
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
</asp:Content>