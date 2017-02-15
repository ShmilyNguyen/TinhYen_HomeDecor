<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="saleout-return-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.saleout_return_edit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href="../../styles/grid.css" rel="stylesheet" />
    <style>
    





        /** Customize the demo canvas */
 
.demo-container label {
    padding-right: 10px;
    width: 185px;
    display: inline-block;
}
 
.demo-container .RadButton {
    margin-top: 20px;
}
 
 
/** Columns */
.rcbHeader ul,
.rcbFooter ul,
.rcbItem ul,
.rcbHovered ul,
.rcbDisabled ul {
    margin: 0;
    padding: 0;
    width: 100%;
    display: inline-block;
    list-style-type: none;
}
 
.exampleRadComboBox.RadComboBoxDropDown .rcbHeader {
    padding: 5px 27px 4px 7px;
}
 
.rcbScroll {
    overflow: scroll !important;
    overflow-x: hidden !important;
}
 
.col1{
    margin: 0;
    padding: 0 5px 0 0;
    width: 100px;
    line-height: 14px;
    float: left;
}
.col2{
    margin: 0;
    padding: 0 5px 0 0;
    width: 70px;
    line-height: 14px;
    float: left;
}
.col3 {
    margin: 0;
    padding: 0 5px 0 0;
    width: 600px;
    line-height: 14px;
    float: left;
}
 



.colKM1{
    margin: 0;
    padding: 0 5px 0 0;
    width: 200px;
    line-height: 14px;
    float: left;
}
.colKM2{
    margin: 0;
    padding: 0 5px 0 0;
    width: 200px;
    line-height: 14px;
    float: left;
}
.colKM3 {
    margin: 0;
    padding: 0 5px 0 0;
    width: 400px;
    line-height: 14px;
    float: left;
}
 


 
/** Multiple rows and columns */
.multipleRowsColumns .rcbItem,
.multipleRowsColumns .rcbHovered {
    float: left;
    margin: 0 1px;
    min-height: 13px;
    overflow: hidden;
    padding: 2px 19px 2px 6px;
    width: 195px;
}
 
 
.results {
    display: block;
    margin-top: 20px;
}
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" ClientEvents-OnRequestStart="onRequestStart"
            runat="server" CssClass="grid_wrapper">

            <div>


                <asp:HiddenField ID="hdf_SaleOut_ID" runat="server" />
                <asp:HiddenField ID="hdf_User_ID" runat="server" />


            </div>
            <table>
                <tr>
                    <td class="auto-style1">
                        <strong>Số CT :</strong></td>
                    <td class="auto-style1">
                        <asp:Label ID="lblSoChungTu" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style1"><strong>Ngày :</strong></td>
                    <td class="auto-style1">
                        <asp:Label ID="lblNgay" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        &nbsp;<strong>Người lập :</strong></td>
                    <td class="auto-style1">
                        <asp:Label ID="lblNguoiLap" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        &nbsp;<strong>Cập nhật :</strong></td>
                    <td class="auto-style1">
                        <asp:Label ID="lblCapNhat" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <b>NPP :</b></td>
                    <td>
                        <asp:Label ID="lblNPP" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td nowrap><b>Nhân viên :</b></td>
                    <td>
                        <asp:Label ID="lblNhanVien" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <b>Tuyến-Thứ :</b></td>
                    <td>
                        <asp:Label ID="lblTuyenThu" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td nowrap>
                        <b>Khách hàng :</b></td>
                    <td colspan="3">
                        &nbsp;
                        <asp:Label ID="lblKhachHang" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td nowrap>
                        <b>Chính sách giá :&nbsp; </b>
                    </td>
                    <td>
                        <asp:Label ID="lblChinhSachGia" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td nowrap><b>&nbsp;Địa chỉ :</b></td>
                    <td colspan="7">
                        <asp:Label ID="lblDiaChiKhachHang" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td nowrap><strong>Ngày trả hàng</strong></td>
                    <td>
                        <telerik:RadDatePicker ID="rdpNgayTraDonHang" runat="server" >
                            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput AutoPostBack="True" DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="40%">
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
                    <td nowrap><b>Lý do trả hàng :</b></td>
                    <td colspan="7">
                        <asp:TextBox ID="txtLyDoTraHang" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="10" nowrap>&nbsp;</td>
                </tr>
            </table>
        </telerik:RadAjaxPanel>
    </div>
    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart"  
            runat="server" CssClass="grid_wrapper">
            <div>
            </div>
            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5" OnNeedDataSource="RadGrid1_NeedDataSource"
                OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand"
                OnDeleteCommand="RadGrid1_DeleteCommand" AllowPaging="True" AllowSorting="True"
                RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand"
                Height="250px" AutoGenerateColumns="False" ResolvedRenderMode="Classic" Skin="Office2010Blue" Enabled="False">
                <GroupingSettings ShowUnGroupButton="true" />
                <ExportSettings ExportOnlyData="true" IgnorePaging="true">
                </ExportSettings>
                <MasterTableView TableLayout="Fixed" DataKeyNames="row_id" CommandItemDisplay="Top"
                    InsertItemPageIndexAction="ShowItemOnFirstPage" ShowFooter="True" AllowNaturalSort="False"
                    AllowSorting="False">
                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="rowid" UniqueName="row_id" FilterControlAltText="Filter item_id column"
                            DataField="row_id" Display="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter saleout_type column" HeaderText="Loại xuất"
                            UniqueName="saleout_type" DataField="saleout_type">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_id" FilterControlAltText="Filter item_code column"
                            HeaderText="Item ID" UniqueName="item_id" Display="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column"
                            HeaderText="Code" UniqueName="item_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column"
                            UniqueName="name" HeaderText="Hàng hóa" SortExpression="Name">
                            <HeaderStyle Width="150px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_price" FilterControlAltText="Filter item_price column"
                            HeaderText="Đơn giá" UniqueName="item_price" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="qty" FilterControlAltText="Filter category_id1 column"
                            UniqueName="qty" HeaderText="SL" Aggregate="Sum" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter discount column" HeaderText="CK(%)"
                            UniqueName="discount" DataField="discount">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Display="False" FilterControlAltText="Filter promo_id column"
                            HeaderText="promo_id" UniqueName="promo_id">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter promo_code column" HeaderText="CTKM"
                            UniqueName="promo_code" DataField="promo_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GTChietKhauDongHang" FilterControlAltText="Filter total_discount column"
                            HeaderText="GT CK" UniqueName="total_discount" Aggregate="Sum" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GTBan" FilterControlAltText="Filter category_id2 column"
                            UniqueName="total" HeaderText="Thành tiền" Aggregate="Sum" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridEditCommandColumn HeaderText="Sửa" UniqueName="EditColumn" Visible="False">
                            <HeaderStyle Width="70px" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn"
                            HeaderText="Xóa" Visible="False">
                            <HeaderStyle Width="70px" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="saleout-row-edit-2.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
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
            <telerik:RadGrid ID="RadGrid2" runat="server" PagerStyle-PageButtonCount="5" AllowPaging="True"
                AllowSorting="True" RenderMode="Auto" GroupPanelPosition="Top" Height="200px"
                AutoGenerateColumns="False" ResolvedRenderMode="Classic" Skin="Office2010Blue" Enabled="False">
                <GroupingSettings ShowUnGroupButton="true" />
                <ExportSettings ExportOnlyData="true" IgnorePaging="true">
                </ExportSettings>
                <MasterTableView TableLayout="Fixed" DataKeyNames="row_id" CommandItemDisplay="Top"
                    InsertItemPageIndexAction="ShowItemOnFirstPage" ShowFooter="True" AllowNaturalSort="False"
                    AllowSorting="False">
                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="rowid" UniqueName="row_id" FilterControlAltText="Filter item_id column"
                            DataField="row_id" Display="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter saleout_type column" HeaderText="Loại xuất"
                            UniqueName="saleout_type" DataField="saleout_type">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_id" FilterControlAltText="Filter item_code column"
                            HeaderText="Item ID" UniqueName="item_id" Display="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column"
                            HeaderText="Code" UniqueName="item_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column"
                            UniqueName="name" HeaderText="Hàng hóa" SortExpression="Name">
                            <HeaderStyle Width="150px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="item_price" FilterControlAltText="Filter item_price column"
                            HeaderText="Đơn giá" UniqueName="item_price" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="qty" FilterControlAltText="Filter category_id1 column"
                            UniqueName="qty" HeaderText="SL" Aggregate="Sum" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter discount column" HeaderText="CK(%)"
                            UniqueName="discount" DataField="discount">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Display="False" FilterControlAltText="Filter promo_id column"
                            HeaderText="promo_id" UniqueName="promo_id">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter promo_code column" HeaderText="CTKM"
                            UniqueName="promo_code" DataField="promo_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GTChietKhauDongHang" FilterControlAltText="Filter total_discount column"
                            HeaderText="GT CK" UniqueName="total_discount">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GTBan" FilterControlAltText="Filter category_id2 column"
                            UniqueName="total" HeaderText="Thành tiền" Aggregate="Sum" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn Text="" UniqueName="DeleteColumn" HeaderText="Delete Command Column"
                            Visible="false">
                            <HeaderStyle Width="70px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn Text="" UniqueName="DeleteColumn" HeaderText="Delete Command Column"
                            Visible="false">
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
            <div>
                <table>
                    <tr>
                        <td>
                            Thành tiền
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtThanhTien" runat="server" Enabled="False">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            GT Chiết khấu KM
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtGTCK" runat="server" Enabled="False">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CK NPP(%)
                        </td>
                        <td>
                            <asp:TextBox ID="txtOntopDiscount" runat="server" Width="50px" Font-Bold="True" ForeColor="Red"
                                BackColor="#FFCC00" Enabled="False">0</asp:TextBox>
                        </td>
                        <td>
                            GT Chiết khấu NPP
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTotalOntopDiscount" runat="server" Enabled="False">
                                <NegativeStyle Resize="None" />
                                <NumberFormat ZeroPattern="n" />
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            GT Thanh toán
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtGTThanhToan" runat="server" Enabled="False">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
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
    <div>
       
        <asp:Button ID="btnSave" runat="server" Text="Lưu đơn hàng trả" Width="120px" OnClick="btnSave_Click" OnClientClick="return confirm('Vui lòng xác nhận việc trả Đơn Hàng ?');"/>
        &nbsp;&nbsp;&nbsp;

         <asp:Button ID="btnDelete" runat="server" Text="Xóa đơn hàng trả" Width="120px" OnClientClick="return confirm('Vui lòng xác nhận việc Xóa Đơn Hàng Trả này ?');" OnClick="btnDelete_Click"/>
        &nbsp;&nbsp;&nbsp;

        <asp:Button ID="btnExit" runat="server" Text="Thoát" Width="120px" OnClick="btnExit_Click" />
    </div>


    



    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <script type="text/javascript">
            function RowSelected(sender, args) {

                var grid = $find("<%=RadGrid1.ClientID %>");

                var totalAmount = 0;

                if (grid) {
                    var MasterTable = grid.get_masterTableView();
                    var Rows = MasterTable.get_dataItems();
                    for (var i = 0; i < Rows.length; i++) {
                        var row = Rows[i];
                        if (row.get_selected() == true) {

                            totalAmount = totalAmount + parseFloat(row.getDataKeyValue("ID"));
                        }
                    }
                }

                if ($("span[id$='myFooter']").length > 0) {
                    $("span[id$='myFooter']").get(0).innerHTML = "sum is : " + totalAmount;
                }
            }

            function OnClientFocusHandler(sender, eventArgs) {
                if (!sender.get_dropDownVisible()) {
                    sender.showDropDown();
                }
            }


          


          

        </script>
    </telerik:RadCodeBlock>
    <div>
       
    </div>
   
</asp:Content>
