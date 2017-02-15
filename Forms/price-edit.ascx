<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="price-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.price_edit" %>

<asp:Label ID="lblRowId" runat="server" Text="0" Visible="false"></asp:Label>
<asp:Label ID="lblItemId" runat="server" Text="0" Visible="false"></asp:Label>

<table>

    <tr>
        <td>Hàng hóa</td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Width="400px" BackColor="Silver" Enabled="False">0</asp:TextBox>
        </td>
    </tr>

    <tr>
        <td>Giá mua (Trước VAT)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaMuaTruocVAT" runat="server" BackColor="Silver">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>VAT (%)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaMuaVAT" runat="server" BackColor="Silver">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>Giá mua (Sau VAT)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaMuaSauVAT" runat="server" BackColor="Silver">
            </telerik:RadNumericTextBox>
        </td>
    </tr>


    <tr>
        <td>Giá bán (Trước VAT)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaBanTruocVAT" runat="server" BackColor="Silver">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>VAT (%)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaBanVAT" runat="server" BackColor="Silver">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>Giá bán (Sau VAT)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaBanSauVAT" runat="server" BackColor="Silver">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

      <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" Width="100px" />
            <asp:Button ID="Button2" runat="server" Text="Xóa" CommandName="Delete" Width="100px" Visible="false" />
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" Width="100px" /></td>
    </tr>

    <tr>
        <td></td>
        <td>
            <telerik:RadNumericTextBox ID="txtDonGiaNhap" runat="server" BackColor="Silver" Visible="false">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td></td>
        <td>
            <telerik:RadNumericTextBox ID="txtDonGiaXuat" runat="server" BackColor="Silver" Visible="false">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaHoTro" runat="server" BackColor="Silver" Visible="False">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaBan1" runat="server" BackColor="Silver" Visible="False">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaBan2" runat="server" BackColor="Silver" Visible="False">
            </telerik:RadNumericTextBox>
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            <telerik:RadNumericTextBox ID="txtGiaBan3" runat="server" BackColor="Silver" Visible="False">
            </telerik:RadNumericTextBox>
        </td>
    </tr>
  

    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>

</table>
<div>
</div>
