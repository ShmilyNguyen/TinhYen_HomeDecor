<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="debit-credit-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.debit_credit_edit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 4px;
        }
    </style>
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
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
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
                    <td nowrap>Khách hàng </td>
                    <td>
                        <telerik:RadComboBox ID="cbxKhachHang" runat="server" AutoPostBack="True" DataTextField="customer_name" DataValueField="customer_id" DropDownAutoWidth="Enabled" Filter="Contains" OnSelectedIndexChanged="cbxKhachHang_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="350px">
                        </telerik:RadComboBox>
                    </td>
                    <td nowrap>Loại chứng từ</td>
                    <td>
                        <telerik:RadComboBox ID="cbxPaymentType" runat="server" AutoPostBack="True" DataTextField="payment_type_name" DataValueField="payment_type_id" DropDownAutoWidth="Enabled" Filter="Contains" OnSelectedIndexChanged="cbxPaymentType_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="200px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Debit Memo" Value="0" />
                                <telerik:RadComboBoxItem runat="server" Text="Credit Memo" Value="1" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td nowrap>Số chứng từ</td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>Ngày</td>
                    <td>
                        <telerik:RadDatePicker ID="rdpNgay" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td>Người lập</td>
                    <td>
                        <asp:TextBox ID="txtNguoiLap" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Giá trị</td>
                    <td>
                        <asp:TextBox ID="txtGiaTri" runat="server" ForeColor="Red" Width="200px" Font-Bold="True">0</asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
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
                        &nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Hủy" Width="100px" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click" Text="Thoát" Width="100px" />
                        &nbsp;&nbsp;
                       
                    </td>
                </tr>
            </table>

          

              <div>
       
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


        </telerik:RadAjaxPanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
    </div>

  
</asp:Content>