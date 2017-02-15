<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="saleout-edit-3.aspx.cs" Inherits="WKS.DMS.WEB.Forms.saleout_edit_3" %>

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
            <table>
                <tr>
                    <td class="auto-style1">
                        Số CT
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtSaleOut_Code" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style1">Ngày</td>
                    <td class="auto-style1">
                        <telerik:RadDatePicker ID="rdpNgayGiaoDich" runat="server" AutoPostBack="True" OnSelectedDateChanged="rdpNgayGiaoDich_SelectedDateChanged">
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
                    <td class="auto-style1">
                        &nbsp;Người lập</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtNguoiLap" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        &nbsp;Cập nhật</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtLastModified" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        NPP
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbxStore" OnClientFocus="OnClientFocusHandler" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td nowrap>Nhân viên</td>
                    <td>
                        <telerik:RadComboBox ID="cbxEmployee" runat="server" AutoPostBack="True" DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled" Filter="Contains" OnClientFocus="OnClientFocusHandler" OnSelectedIndexChanged="cbxEmployee_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        Tuyến-Thứ
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbxWeekDays" runat="server" AutoPostBack="True" DataTextField="route_name" DataValueField="route_id" DropDownAutoWidth="Enabled" Filter="Contains" OnClientFocus="OnClientFocusHandler" OnSelectedIndexChanged="cbxWeekDays_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td nowrap>
                        Khách hàng
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbxKhachHang" OnClientFocus="OnClientFocusHandler" runat="server"
                            DataTextField="customer_name" DataValueField="customer_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="350px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxKhachHang_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td nowrap>
                        Chính sách giá
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbxChinhSachGia" runat="server" AutoPostBack="True" DataTextField="item_price_code"
                            DataValueField="item_price_policy_id" DropDownAutoWidth="Enabled" Filter="Contains"
                            OnClientFocus="OnClientFocusHandler" OpenDropDownOnFocus="True" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td nowrap>Ghi chú </td>
                    <td colspan="7">
                        <asp:TextBox ID="txtGhiChu" runat="server" Width="500px"></asp:TextBox>
                        <asp:HiddenField ID="hdf_SaleOut_ID" runat="server" />
                        <asp:HiddenField ID="hdf_User_ID" runat="server" />
                    </td>
                </tr>
            </table>
        </telerik:RadAjaxPanel>
    </div>
    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart"  
            runat="server" CssClass="grid_wrapper">
            <div>
                <table>
                    <tr>
                        <td>
                            Loại xuất
                        </td>
                        <td>
                            Hàng hóa
                        </td>
                        <td>
                            Số lượng
                        </td>
                        <td>
                            CTKM
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="cbxLoaiXuat" runat="server" OnClientFocus="OnClientFocusHandler"
                                DropDownAutoWidth="Enabled" Filter="Contains" Width="100px" OpenDropDownOnFocus="True">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Selected="True" Text="Bán hàng" Value="HB" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cbxHangHoa" runat="server" OnClientFocus="OnClientFocusHandler"
                                DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="400px" OpenDropDownOnFocus="True"  EnableLoadOnDemand="true"
            HighlightTemplatedItems="true"  DropDownCssClass="exampleRadComboBox" AutoPostBack="True" OnSelectedIndexChanged="cbxHangHoa_SelectedIndexChanged" OnItemDataBound="cbxHangHoa_ItemDataBound">


                                <HeaderTemplate>
                                            <ul>
                                                <li class="col1">Mã hàng hóa</li>
                                                <li class="col2">Hàng bán</li>
                                                <li class="col2">KM</li>
                                                <li class="col2">Tồn kho</li>
                                                <li class="col3">Tên hàng hóa</li>
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <ul>
                                                <li class="col1">
                                                    <%# DataBinder.Eval(Container.DataItem, "item_code") %></li>
                                                
                                                <li class="col2">
                                                    <%# DataBinder.Eval(Container.DataItem, "qty_saleout") %></li>
                                                <li class="col2">
                                                    <%# DataBinder.Eval(Container.DataItem, "qty_promo") %></li>

                                                <li class="col2">
                                                    <%# DataBinder.Eval(Container.DataItem, "qty") %></li>
                                                <li class="col3">
                                                    <%# DataBinder.Eval(Container.DataItem, "item_name") %></li>
                                            </ul>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            A total of
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            items
                                        </FooterTemplate>


                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQty" runat="server" Width="50px" TextMode="Number"></asp:TextBox>
                        </td>
                        <td style="margin-left: 40px">
                            <telerik:RadComboBox ID="cbxCTKM" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="promo_name" DataValueField="promo_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="250px" OpenDropDownOnFocus="True"  EnableLoadOnDemand="true"
            HighlightTemplatedItems="true"  DropDownCssClass="exampleRadComboBox">



                                   <HeaderTemplate>
                                            <ul>
                                                <li class="colKM1">Mã CTKM</li>
                                                <li class="colKM2">Thời gian áp dụng</li>
                                                <li class="colKM3">Tên CTKM</li>
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <ul>
                                                <li class="colKM1">
                                                    <%# DataBinder.Eval(Container.DataItem, "promo_code") %></li>
                                                
                                                <li class="colKM2">
                                                    <%# DataBinder.Eval(Container.DataItem, "promo_date") %></li>
                                                <li class="colKM3">
                                                    <%# DataBinder.Eval(Container.DataItem, "promo_name") %></li>
                                            </ul>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            A total of
                                            <asp:Literal runat="server" ID="RadComboItemsCount1" />
                                            items
                                        </FooterTemplate>




                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;<asp:HiddenField ID="hdf_SLTonKho" runat="server" Value="0" />
&nbsp;</td>
                        <td style="margin-left: 80px">
                            <asp:Button ID="btnAddRow" runat="server" Text="Thêm" OnClick="btnAddRow_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnTinhToanKM" runat="server" OnClick="btnTinhToanKM_Click" Text="Tính toán KM" />
                        </td>
                    </tr>
                </table>
            </div>
            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5" OnNeedDataSource="RadGrid1_NeedDataSource"
                OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand"
                OnDeleteCommand="RadGrid1_DeleteCommand" AllowPaging="True" AllowSorting="True"
                RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand"
                Height="250px" AutoGenerateColumns="False" ResolvedRenderMode="Classic" Skin="Office2010Blue">
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
                        <telerik:GridEditCommandColumn HeaderText="Sửa" UniqueName="EditColumn">
                            <HeaderStyle Width="70px" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn"
                            HeaderText="Xóa">
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
                AutoGenerateColumns="False" ResolvedRenderMode="Classic" Skin="Office2010Blue">
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
                                BackColor="#FFCC00">0</asp:TextBox>
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
       
        <asp:Button ID="btnSave" runat="server" Text="Lưu đơn hàng" OnClick="btnSave_Click" Width="120px" />
        &nbsp;&nbsp;&nbsp;
        
        



        <asp:Button ID="btnInDonHang" runat="server" Text="In đơn hàng" OnClick="btnInDonHang_Click" Width="120px" OnClientClick="var originalTarget = document.forms[0].target; document.forms[0].target = '_blank'; setTimeout(function () { document.forms[0].target = originalTarget; }, 3000);"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNext" runat="server" Text="Đơn kế tiếp" OnClick="btnNext_Click" Width="120px" />
       
        &nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnDeleteOrder" runat="server" Text="Hủy đơn hàng" OnClientClick="return confirm('Vui lòng xác nhận việc HỦY ĐƠN HÀNG !');"
            OnClick="btnDeleteOrder_Click" Width="120px" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnReturnOrder" runat="server" Text="Trả đơn hàng" OnClick="tbnReturnOrder_Click" OnClientClick="return confirm('Bạn sẽ được chuyển đến Form trả đơn hàng, vui lòng chọn NGÀY TRẢ và LÝ DO trả hàng !');" Width="120px"  Enabled="False"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExit" runat="server" Text="Thoát" OnClick="btnExit_Click" Width="120px" />
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
