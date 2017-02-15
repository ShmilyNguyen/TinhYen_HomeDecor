<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="payment-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.payment_edit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:HiddenField ID="hdf_ID" runat="server" />
        <asp:HiddenField ID="hdf_UserID" runat="server" />
    </div>

    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" ClientEvents-OnRequestStart="onRequestStart"
            runat="server" CssClass="grid_wrapper">
            <table>
                <tr>
                    <td class="auto-style1">Số CT
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style1">Ngày</td>
                    <td class="auto-style1">
                        <telerik:RadDatePicker ID="rdpNgay" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td class="auto-style1">&nbsp;Người lập</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtNguoiLap" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>NPP
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbxStore" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td>Khách hàng </td>
                    <td>
                        <telerik:RadComboBox ID="cbxKhachHang" runat="server" AutoPostBack="True" DataTextField="customer_name" DataValueField="customer_id" DropDownAutoWidth="Enabled" Filter="Contains" OnSelectedIndexChanged="cbxKhachHang_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="350px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Loại chứng từ</td>
                    <td>
                        <telerik:RadComboBox ID="cbxPaymentType" runat="server" AutoPostBack="True" DataTextField="payment_type_name" DataValueField="payment_type_id" DropDownAutoWidth="Enabled" Filter="Contains" OnSelectedIndexChanged="cbxStore_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Công nợ hiện tại</td>
                    <td>
                        <asp:TextBox ID="txtCongNoHienTai" runat="server" Width="200px" Enabled="False" Font-Bold="True" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Ghi chú </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtGhiChu" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="5">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Lưu" Width="100px" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click" Text="Thoát" Width="100px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Vui lòng xác nhận việc xóa Chứng từ thanh toán này?');" Text="Hủy chứng từ" Width="100px" />
                    </td>
                </tr>
            </table>

            <table>
                <tr>
                    <td>Số chứng từ</td>
                    <td>Thanh toán</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cbxSoChungTu" runat="server" DataTextField="saleout_code" DataValueField="saleout_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="400px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnAdd1" runat="server" Text="Thêm" OnClick="btnAdd1_Click" />
                    </td>
                </tr>
            </table>

            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                AllowPaging="True" AllowSorting="True"
                RenderMode="Auto" GroupPanelPosition="Top"
                Height="800px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue" OnDeleteCommand="RadGrid1_DeleteCommand">
                <GroupingSettings ShowUnGroupButton="true" />
                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                <MasterTableView
                    AllowFilteringByColumn="true" TableLayout="Fixed"
                    DataKeyNames="row_id" CommandItemDisplay="Top"
                    InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Row ID"
                            UniqueName="row_id" FilterControlAltText="Filter row_id column" DataField="row_id" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter saleout_code column" HeaderText="Mã đơn hàng" UniqueName="saleout_code" DataField="saleout_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn
                            FilterControlAltText="Filter trans_date_gmt column" HeaderText="Ngày"
                            UniqueName="trans_date_gmt" DataField="trans_date_gmt">
                        </telerik:GridBoundColumn>

                     

                        <telerik:GridBoundColumn FilterControlAltText="Filter total_amt column" HeaderText="Thành tiền" UniqueName="total_amount" DataField="total_amount" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn FilterControlAltText="Filter payment_amt column" HeaderText="Thanh toán" UniqueName="payment_amt" DataField="payment_amt" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter balance_amt column" UniqueName="balance_amt" HeaderText="Còn lại" DataField="balance_amt" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>

                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Xóa">
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
        </telerik:RadAjaxPanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
    </div>

    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
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