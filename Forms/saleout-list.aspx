<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="saleout-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.saleout_list" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/Forms/ucChotTonKho.ascx" TagPrefix="uc1" TagName="ucChotTonKho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucChotTonKho runat="server" id="ucChotTonKho" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
    </div>
    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">

            <table>
            <tr>
                <td  style="color:#fff" >Từ khóa tìm kiếm : </td>
                <td>
                    <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
               <td>
                    <asp:Button runat="server" Text="Tạo phiếu xuất kho" ID="btnNewOrder" />
                </td>
            </tr>
        </table>





            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                OnNeedDataSource="RadGrid1_NeedDataSource"
                OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                OnUpdateCommand="RadGrid1_UpdateCommand"
                OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                AllowPaging="True" AllowSorting="True" ShowGroupPanel="True"
                RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand"
                Height="800px" ResolvedRenderMode="Classic" Skin="Office2010Blue">
                <GroupingSettings ShowUnGroupButton="true" />
                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="true" TableLayout="Fixed"
                    DataKeyNames="saleout_id" CommandItemDisplay="Top"
                    InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" ShowGroupFooter="True" ShowFooter="True">
                    <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="id"
                            UniqueName="saleout_id" FilterControlAltText="Filter saleout_id column"
                            DataField="saleout_id" Display="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter trans_date_gmt column" HeaderText="Ngày" UniqueName="trans_date_gmt" DataField="trans_date_gmt">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter item_name column" HeaderText="NPP" UniqueName="store_name" SortExpression="Name">
                            <HeaderStyle Width="150px" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="saleout_code"
                            FilterControlAltText="Filter area_id column" HeaderText="Số CT"
                            UniqueName="saleout_code">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="employee_name" FilterControlAltText="Filter employee_name column" HeaderText="Nhân viên" UniqueName="employee_name" SortExpression="employee_name">
                            <HeaderStyle Width="150px" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="customer_code"
                            FilterControlAltText="Filter customer_code column" HeaderText="Code KH"
                            UniqueName="customer_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="customer_name"
                            FilterControlAltText="Filter category_id1 column" HeaderText="Khách hàng"
                            UniqueName="customer_name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="address_full"
                            FilterControlAltText="Filter address_full column" HeaderText="Địa chỉ"
                            UniqueName="address_full">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="total" FilterControlAltText="Filter category_id2 column" UniqueName="total" HeaderText="Thành tiền" DataFormatString="{0:###,###.##}" Aggregate="Sum">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="note" FilterControlAltText="Filter province_id column" UniqueName="note" HeaderText="Ghi chú" Display="False">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="order_status" FilterControlAltText="Filter order_status column" HeaderText="Tình trạng ĐH" UniqueName="order_status">
                        </telerik:GridBoundColumn>

                        <telerik:GridButtonColumn CommandName="Update" Text="Xem" UniqueName="UpdateColumn" HeaderText="Chỉnh sửa">
                            <HeaderStyle Width="70px" />
                        </telerik:GridButtonColumn>

                        <telerik:GridButtonColumn CommandName="RecordDebtAndConfirm" Text="Xác nhận" FilterControlAltText="Filter goDebtColumn column" HeaderText="Xác nhận" UniqueName="goDebtColumn">
                        </telerik:GridButtonColumn>

                        <telerik:GridButtonColumn Display="false" CommandName="Confirm" Text="Thu tiền" FilterControlAltText="Filter ConfirmColumn column" HeaderText="Thu tiền" UniqueName="ConfirmColumn">
                        </telerik:GridButtonColumn>
                    </Columns>

                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" FormatString="" HeaderText="Nhà phân phối" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" FormatString="" HeaderText="" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="ngaygiaodich" FieldName="ngaygiaodich" FormatString="" HeaderText="Ngày giao dịch" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldAlias="ngaygiaodich" FieldName="ngaygiaodich" SortOrder="Descending" />
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
        <telerik:RadCodeBlock runat="server">
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