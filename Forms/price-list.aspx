<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="price-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.price_list" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


        <div>


                        <table>
                            <tr>
                                <td>Từ khóa tìm kiếm : </td>
                                <td>
                                    <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click"/></td>
                                <td></td>
                            </tr>
                        </table>

                    </div>


        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" Height="800px" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False"
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="item_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" ShowGroupFooter="True">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn DataField="item_price_id" FilterControlAltText="Filter item_price_id column" HeaderText="item price id" UniqueName="item_price_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Mã hàng hóa"
                        UniqueName="item_id" FilterControlAltText="Filter item_id column" DataField="item_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Code hàng hóa" UniqueName="item_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column" HeaderText="Tên hàng hóa" SortExpression="Name" UniqueName="item_name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="fromdate" FilterControlAltText="Filter fromdate column" HeaderText="Từ ngày" UniqueName="fromdate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="todate" FilterControlAltText="Filter todate column" HeaderText="Đến ngày" UniqueName="todate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sellin_price" FilterControlAltText="Filter category2 column" HeaderText="Đơn giá nhập" UniqueName="sellin_price">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter category3 column" HeaderText="Đơn giá xuất" UniqueName="saleout_price" DataField="saleout_price">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="support_price" FilterControlAltText="Filter support_price column" HeaderText="Giá hỗ trợ" UniqueName="support_price">
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="price1" FilterControlAltText="Filter price1 column" HeaderText="Giá bán 1" UniqueName="price1">
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="price2" FilterControlAltText="Filter price2 column" HeaderText="Giá bán 2" UniqueName="price2">
                    </telerik:GridBoundColumn>


                     <telerik:GridBoundColumn DataField="price3" FilterControlAltText="Filter price3 column" HeaderText="Giá bán 3" UniqueName="price3">
                    </telerik:GridBoundColumn>



                    <telerik:GridEditCommandColumn UniqueName="EditColumn" HeaderText="Edit Command Column">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete Command Column" Visible="False">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

                 <EditFormSettings UserControlName="price-edit.ascx" EditFormType="WebUserControl">
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


