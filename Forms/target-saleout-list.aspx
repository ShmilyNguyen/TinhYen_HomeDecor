<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="target-saleout-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.target_saleout_list" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Nhà phân phối</td>
            <td>
                <telerik:RadComboBox ID="cbxNhaPhanPhoi" runat="server" DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="400px" OpenDropDownOnFocus="True"></telerik:RadComboBox>
            </td>
        </tr>
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
            Height="800px" ResolvedRenderMode="Classic" Skin="Office2010Blue" PageSize="50">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False"
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="row_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage"
                ShowFooter="True">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Row ID"
                        UniqueName="row_id" FilterControlAltText="Filter row_id column"
                        DataField="row_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="employee_id"
                        FilterControlAltText="Filter item_id column" HeaderText="employee_id"
                        UniqueName="employee_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="employee_name"
                        FilterControlAltText="Filter item_code column" HeaderText="Nhân viên"
                        UniqueName="employee_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter qty_saleout column"
                        HeaderText="Năm" UniqueName="target_year" DataField="target_year">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="target_month"
                        FilterControlAltText="Filter qty_promo column" HeaderText="Tháng"
                        UniqueName="target_month">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter target_order column" HeaderText="Chỉ tiêu DH" UniqueName="target_order"  DataField="target_order" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="target_saleout"
                        FilterControlAltText="Filter total_saleout column" HeaderText="Chỉ tiêu DS"
                        UniqueName="target_saleout" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter target_focus_order column" HeaderText="Chỉ tiêu DH tập trung" UniqueName="target_focus_order"  DataField="target_focus_order" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn FilterControlAltText="Filter target_focus_saleout column" HeaderText="Chỉ tiêu DS tập trung" UniqueName="target_focus_saleout"  DataField="target_focus_saleout"  DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn FilterControlAltText="Filter target_active_outlet column" HeaderText="Cửa hiệu mua hàng" UniqueName="target_active_outlet"  DataField="target_active_outlet"  DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>


                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="store_id" Display="False"
                        FilterControlAltText="Filter store_id column" HeaderText="store_id"
                        UniqueName="store_id">
                    </telerik:GridBoundColumn>
                </Columns>

                <EditFormSettings UserControlName="target-saleout-edit.ascx"
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