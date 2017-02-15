<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="TrackingCustomerDebt.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.TrackingCustomerDebt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

      <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" >

           <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <asp:Label ID="lblId" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderNo" runat="server" Text="0"  Visible="false"></asp:Label>

    <div style="width:100%;background-color:white">
        <table>
        <tr>
            <td>Ngày thu tiền</td>
            <td>

                        <telerik:RadDatePicker ID="rdpNgayThuTien" runat="server" AutoPostBack="True" OnSelectedDateChanged="rdpNgayGiaoDich_SelectedDateChanged" EnableTyping="False">
                            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput AutoPostBack="True" DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="40%" ReadOnly="True">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>

            </td>
            <td>Diễn giải</td>
            <td>

                <asp:TextBox ID="txtDienGiai" runat="server" Width="350px"></asp:TextBox>

            </td>

        </tr>
        <tr>
            <td>Nhà phân phối</td>
            <td>

                        <telerik:RadComboBox ID="cbxStore" OnClientFocus="OnClientFocusHandler" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore_SelectedIndexChanged">
                        </telerik:RadComboBox>

            </td>
            <td>Khách hàng</td>
            <td>

                        <telerik:RadComboBox ID="cbxKhachHang" OnClientFocus="OnClientFocusHandler" runat="server"
                            DataTextField="customer_name" DataValueField="customer_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="350px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxKhachHang_SelectedIndexChanged">
                        </telerik:RadComboBox>

            </td>

        </tr>
        <tr>
            <td>Ngày đơn hàng</td>
            <td>

                        <telerik:RadDatePicker ID="rdpNgayGiaoDich" runat="server" AutoPostBack="True" OnSelectedDateChanged="rdpNgayGiaoDich_SelectedDateChanged" EnableTyping="False">
                            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput AutoPostBack="True" DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="40%" ReadOnly="True">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>

            </td>
            <td>Thu theo</td>
            <td>
                <asp:DropDownList ID="ddlThuTheo" runat="server">
                    <asp:ListItem Value="PXK">Phiếu xuất kho</asp:ListItem>
                </asp:DropDownList>
            </td>

        </tr>

         <tr>
            <td>Phiếu xuất kho</td>
            <td colspan="3">

                <telerik:RadComboBox ID="cbxPhieuXuatKho" Runat="server" Width="400px" OnSelectedIndexChanged="cbxPhieuXuatKho_SelectedIndexChanged" DataTextField="saleout_code" DataValueField="saleout_id" AutoPostBack="True">
                </telerik:RadComboBox>

            </td>

        </tr>

         <tr>
            <td>Tồng tiền</td>
            <td colspan="3">

                <telerik:RadNumericTextBox ID="txtTongTien" Runat="server" Width="200px" Enabled="False">
                </telerik:RadNumericTextBox>

            </td>

        </tr>

         <tr>
            <td>Tổng thanh toán</td>
            <td>

                <telerik:RadNumericTextBox ID="txtTongThanhToan" Runat="server" Width="200px">
                </telerik:RadNumericTextBox>

            </td>
            <td>&nbsp;</td>
            <td> &nbsp;</td>

        </tr>

         <tr>
            <td>&nbsp;</td>
            <td>

                <telerik:RadNumericTextBox ID="txtConLai" Runat="server" Width="200px" Visible="False">
                </telerik:RadNumericTextBox>

            </td>
            <td>&nbsp;</td>
            <td> &nbsp;</td>

        </tr>

         <tr>
            <td>&nbsp;</td>
            <td align="right">

                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Quay về đơn hàng" />
             </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Xác nhận thu tiền" OnClick="btnSave_Click" />
             </td>
            <td> &nbsp;</td>

        </tr>

         <tr>
            <td>&nbsp;</td>
            <td>

                &nbsp;</td>
            <td>&nbsp;</td>
            <td> &nbsp;</td>

        </tr>

    </table>

        <div id="grdList">


           

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
                            <telerik:GridBoundColumn HeaderText="Mã khách hàng"
                                UniqueName="customer_code" FilterControlAltText="Filter customer_code column" DataField="customer_code">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="customer_name" FilterControlAltText="Filter customer_name column" HeaderText="Tên khách hàng" UniqueName="customer_name" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="address_full" FilterControlAltText="Filter address_full column" HeaderText="Địa chỉ"  UniqueName="address_full">
                                
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="saleout_code" FilterControlAltText="Filter saleout_code column" HeaderText="Số chứng từ" UniqueName="saleout_code">
                            </telerik:GridBoundColumn>
                          
                              <telerik:GridBoundColumn DataField="trans_date_gmt" FilterControlAltText="Filter trans_date_gmt column" HeaderText="Ngày hóa đơn" UniqueName="trans_date_gmt">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="doc_date" FilterControlAltText="Filter doc_date column" HeaderText="Ngày thanh toán" UniqueName="doc_date">
                            </telerik:GridBoundColumn>


                             <telerik:GridBoundColumn  DataFormatString="{0:###,###}" DataField="total_amt" FilterControlAltText="Filter total_amt column" HeaderText="Tổng số tiền" UniqueName="total_amt">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn  DataFormatString="{0:###,###}" DataField="recieve_amt" FilterControlAltText="Filter recieve_amt column" HeaderText="Thanh toán" UniqueName="recieve_amt">
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn  DataFormatString="{0:###,###}" DataField="balance_amt" FilterControlAltText="Filter balance_amt column" HeaderText="Còn lại" UniqueName="balance_amt">
                            </telerik:GridBoundColumn>
                      
                                <telerik:GridCheckBoxColumn DataField="release" DataType="System.Boolean" FilterControlAltText="Filter release column" HeaderText="Trả hết" UniqueName="release">
                    </telerik:GridCheckBoxColumn>

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
