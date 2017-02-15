<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TonDauKy-Edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.TonDauKy_Edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table>
    <tr>
        <td>&nbsp;</td>
        <td>
                    <asp:HiddenField ID="hdf_row_id" runat="server" />
                </td>
    </tr>
    <tr>
        <td>Hàng hóa</td>
        <td>
                    <telerik:RadComboBox ID="cbxHangHoa" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="400px" OpenDropDownOnFocus="True">
                    </telerik:RadComboBox>
                </td>
    </tr>
    <tr>
        <td>Tồn đầu hàng bán</td>
        <td>
                    <telerik:RadNumericTextBox ID="txtQty_SaleOut" Runat="server">
                    </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td>GT Tồn đầu HB</td>
        <td>
                    <telerik:RadNumericTextBox ID="txtTotal_SaleOut" Runat="server">
                    </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td>Tồn đầu khuyến mãi</td>
        <td>
                    <telerik:RadNumericTextBox ID="txtQty_Promo" Runat="server">
                    </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td>GT Tồn đầu KM</td>
        <td>
                    <telerik:RadNumericTextBox ID="txtTotal_Promo" Runat="server">
                    </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td>Lý do điều chỉnh</td>
        <td>
                    <asp:TextBox ID="txtNote" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Loại điều chỉnh</td>
        <td>
                    <asp:TextBox ID="txtAdjust_Type" runat="server" Enabled="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" Width="100px"/>
            <asp:Button ID="Button2" runat="server" Text="Xóa" CommandName="Delete" Visible ="False"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" Width="100px"/></td>
    </tr>
</table>
