<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="saleout-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.saleout_edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
    <table>
        <tr>
            <td class="auto-style1">Số CT</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtSaleOut_Code" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style1">Ngày</td>
            <td class="auto-style1">
                <telerik:RadDatePicker ID="rdpNgayGiaoDich" Runat="server">
                </telerik:RadDatePicker>
            </td>
            <td class="auto-style1">Người lập</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtNguoiLap" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style1">
                Cập nhật</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtLastModified" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>NPP</td>
            <td>
                    <telerik:RadComboBox ID="cbxNhaPhanPhoi" OnClientFocus="OnClientFocusHandler" runat="server" DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="200px" OpenDropDownOnFocus="True"></telerik:RadComboBox>

            </td>
            <td>Nhân viên</td>
            <td>
                    <telerik:RadComboBox ID="cbxNhanVien" OnClientFocus="OnClientFocusHandler" runat="server" DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="200px" OpenDropDownOnFocus="True"></telerik:RadComboBox>

            </td>
            <td>
                Ghi chú</td>
            <td>
                <asp:TextBox ID="txtGhiChu" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:HiddenField ID="hdf_SaleOut_ID" runat="server" />
                <asp:HiddenField ID="hdf_User_ID" runat="server" />
            </td>
        </tr>
        </table>
        </div>
    
    <div>
         <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


             <div>
        <table>     
            <tr>
                <td>Loại xuất</td>
                <td>Hàng hóa</td>
                <td>Số lượng</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cbxLoaiXuat" runat="server" OnClientFocus="OnClientFocusHandler" DropDownAutoWidth="Enabled" Filter="Contains" Width="100px" OpenDropDownOnFocus="True">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Selected="True" Text="Bán hàng" Value="HB" />
                            <telerik:RadComboBoxItem runat="server" Text="Khuyến mãi" Value="KM" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="cbxHangHoa" runat="server" OnClientFocus="OnClientFocusHandler" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="400px" OpenDropDownOnFocus="True">
                    </telerik:RadComboBox>
                </td>
                <td>
                <asp:TextBox ID="txtQty" runat="server" Width="50px" TextMode="Number">0</asp:TextBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="cbxCTKM" OnClientFocus="OnClientFocusHandler" runat="server" DataTextField="promo_name" DataValueField="promo_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="250px" OpenDropDownOnFocus="True" Visible="False">
                    </telerik:RadComboBox>
                </td>
                <td>
                <asp:TextBox ID="txtCK" runat="server" Width="50px" TextMode="Number" Visible="False">0</asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddRow" runat="server" Text="Thêm" OnClick="btnAddRow_Click" />
                </td>
            </tr>
        </table>
        </div>



        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" Height="400px" AutoGenerateColumns="False">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView TableLayout="Fixed"
                DataKeyNames="row_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" ShowFooter="True" AllowNaturalSort="False" AllowSorting="False">
                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="rowid"
                        UniqueName="row_id" FilterControlAltText="Filter item_id column" DataField="row_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter saleout_type column" HeaderText="Loại xuất" UniqueName="saleout_type" DataField="saleout_type">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_id" FilterControlAltText="Filter item_code column" HeaderText="Item ID" UniqueName="item_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Code" UniqueName="item_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column" UniqueName="name" HeaderText="Name" SortExpression="Name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="qty" FilterControlAltText="Filter category_id1 column" UniqueName="qty" HeaderText="Qty" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter discount column" HeaderText="CK" UniqueName="discount" DataField="discount">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Display="False" FilterControlAltText="Filter promo_id column" HeaderText="promo_id" UniqueName="promo_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter promo_code column" HeaderText="CTKM" UniqueName="promo_code" DataField="promo_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total_discount" FilterControlAltText="Filter total_discount column" HeaderText="Total Discount" UniqueName="total_discount">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total" FilterControlAltText="Filter category_id2 column" UniqueName="total" HeaderText="Total" Aggregate="Sum" DataFormatString="{0:###,###}" >
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete Command Column">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

                 <EditFormSettings UserControlName="saleout-row-edit.ascx" EditFormType="WebUserControl">
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

<div>
        <table >
            <tr>
                <td>Thành tiền</td>
                <td>
                    <telerik:RadNumericTextBox ID="txtThanhTien" Runat="server">
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    GT Chiết khấu KM</td>
                <td>
                    <telerik:RadNumericTextBox ID="txtGTCK" Runat="server">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>CK NPP(%)</td>
                <td>
                <asp:TextBox ID="txtOntopDiscount" runat="server" Width="50px" Font-Bold="True" ForeColor="Red" Enabled="False">0</asp:TextBox>
                </td>
                <td>
                    GT Thanh Toán</td>
                <td>
                    <telerik:RadNumericTextBox ID="txtTotalOntopDiscount" Runat="server">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>GT Thanh toán</td>
                <td>
                    <telerik:RadNumericTextBox ID="txtGTThanhToan" Runat="server">
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
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
        <asp:Button ID="btnDeleteOrder" runat="server" Text="Hủy đơn hàng" OnClientClick="return confirm('Vui lòng xác nhận việc xóa đơn hàng ?');" OnClick="btnDeleteOrder_Click"/>
        <asp:Button ID="btnSave" runat="server" Text="Lưu đơn hàng" OnClick="btnSave_Click" />

        <asp:Button ID="btnNext" runat="server" Text="Đơn kế tiếp"  OnClick="btnNext_Click"/>


        <asp:Button ID="btnExit" runat="server" Text="Thoát" OnClick="btnExit_Click" />
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




</asp:Content>
