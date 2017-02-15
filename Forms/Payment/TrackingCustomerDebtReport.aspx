<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="TrackingCustomerDebtReport.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.TrackingCustomerDebtReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server">

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

        <div style="width: 100%; background-color: white">

            <div id="grdList">

                <h3>Theo dõi công nợ khách hàng</h3>

                <div>

                    <table>
                        <tr>
                            <td>Từ khóa tìm kiếm : </td>
                            <td>
                                <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
                            <td></td>
                        </tr>
                    </table>
                </div>

                <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                    OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                    OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                    AllowPaging="True" AllowSorting="True" RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" Height="800px" Skin="Office2010Blue">
                    <GroupingSettings ShowUnGroupButton="true" />
                    <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                    <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed"
                        DataKeyNames="doc_id" CommandItemDisplay="Top"
                        InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" ShowGroupFooter="True">
                        <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="doc_id" FilterControlAltText="Filter doc_id column" HeaderText="DocId" UniqueName="doc_id" Display="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter store_name column" HeaderText="Nhà phân phối" UniqueName="store_name">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Mã khách hàng"
                                UniqueName="customer_code" FilterControlAltText="Filter customer_code column" DataField="customer_code">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="customer_name" FilterControlAltText="Filter customer_name column" HeaderText="Tên khách hàng" UniqueName="customer_name">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="address_full" FilterControlAltText="Filter address_full column" HeaderText="Địa chỉ" UniqueName="address_full">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="saleout_code" FilterControlAltText="Filter saleout_code column" HeaderText="Số chứng từ" UniqueName="saleout_code">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="trans_date_gmt" FilterControlAltText="Filter trans_date_gmt column" HeaderText="Ngày hóa đơn" UniqueName="trans_date_gmt">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataFormatString="{0:###,###}" DataField="total_amt" FilterControlAltText="Filter total_amt column" HeaderText="Tổng số tiền" UniqueName="total_amt">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataFormatString="{0:###,###}" DataField="recieve_amt" FilterControlAltText="Filter recieve_amt column" HeaderText="Thanh toán" UniqueName="recieve_amt">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataFormatString="{0:###,###}" DataField="balance_amt" FilterControlAltText="Filter balance_amt column" HeaderText="Còn nợ" UniqueName="balance_amt">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataFormatString="{0:###,###}" DataField="tuoino" FilterControlAltText="Filter tuoino column" HeaderText="Số ngày nợ" UniqueName="tuoino">
                            </telerik:GridBoundColumn>

                            <telerik:GridButtonColumn CommandName="Update" Text="Thanh toán" UniqueName="UpdateColumn" HeaderText="Thanh toán">
                                <HeaderStyle Width="70px" />
                            </telerik:GridButtonColumn>
                        </Columns>
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

                <telerik:RadCodeBlock runat="server">
                    <script type="text/javascript">
                        function onRequestStart(sender, args) {
                            if (args.get_eventTarget().indexOf("Button") >= 0) {
                                args.set_enableAjax(false);
                            }
                        }

                        function OnClientFocusHandler(sender, eventArgs) {
                            if (!sender.get_dropDownVisible()) {
                                sender.showDropDown();
                            }
                        }
                    </script>
                </telerik:RadCodeBlock>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>