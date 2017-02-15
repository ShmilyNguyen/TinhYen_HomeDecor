<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="target-sellin-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.target_sellin_list" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Tháng</td>
            <td>
                    <asp:DropDownList ID="ddlThang" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>

                </td>
        </tr>
        <tr>
            <td>Năm</td>
            <td>
                    <asp:DropDownList ID="ddlNam" runat="server">
                        <asp:ListItem >2015</asp:ListItem>
                         <asp:ListItem >2016</asp:ListItem>
                         <asp:ListItem >2017</asp:ListItem>
                         <asp:ListItem >2018</asp:ListItem>
                         <asp:ListItem >2019</asp:ListItem>
                         <asp:ListItem >2020</asp:ListItem>
                    </asp:DropDownList>

                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnLoadData" runat="server" Text="Xem chỉ tiêu" Width="100px" 
                    OnClick="btnLoadData_Click" />
            </td>
        </tr>
    </table>

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
            Height="800px" ResolvedRenderMode="Classic" PageSize="50" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False"
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="row_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" ShowFooter="True">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn DataField="row_id" 
                        FilterControlAltText="Filter row_id column" HeaderText="Row ID" 
                        UniqueName="row_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="region_name" 
                        FilterControlAltText="Filter region_name column" HeaderText="Region" 
                        UniqueName="region_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="area_name" 
                        FilterControlAltText="Filter area_name column" HeaderText="Area" 
                        UniqueName="area_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="store_id"
                        UniqueName="store_id" FilterControlAltText="Filter item_id column" DataField="store_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter item_code column" HeaderText="Nhà phân phối" UniqueName="store_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="target_year" 
                        FilterControlAltText="Filter qty_saleout column" 
                        HeaderText="Năm" UniqueName="target_year" 
                       >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="target_month" 
                        FilterControlAltText="Filter qty_promo column" UniqueName="target_month" 
                        HeaderText="Tháng" >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter total_saleout column" 
                        HeaderText="Chỉ tiêu DS" UniqueName="target_sellin" DataField="target_sellin" 
                        Aggregate="Sum"  DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                </Columns>

                 <EditFormSettings UserControlName="target-sellin-edit.ascx" EditFormType="WebUserControl">
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
