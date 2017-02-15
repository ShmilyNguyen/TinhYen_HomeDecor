<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="payment-row-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.payment_row_edit" %>

<div>
    <asp:HiddenField ID="hdf_row_id" runat="server" />
     <asp:HiddenField ID="hdf_saleout_id" runat="server" />
    <asp:HiddenField ID="hdf_doc_id" runat="server" />
     <asp:HiddenField ID="hdf_is_custom_debit" runat="server" />
</div>
<table>
    <tr>
        <td>Số chứng từ</td>
        <td>
            <asp:TextBox ID="txtSoChungTu" runat="server" Enabled="False"></asp:TextBox>
        </td>
        <td>Ngày</td>
        <td>
            <asp:TextBox ID="txtNgay" runat="server" Enabled="False"></asp:TextBox>
        </td>

         <td>Dư nợ</td>
        <td>
            <asp:TextBox ID="txtDuNo" runat="server" Enabled="False"></asp:TextBox>
        </td>


        <td>Thanh toán</td>
        <td>
            <asp:TextBox ID="txtThanhToan" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" Width="80px" CommandName="Update"/>
        </td>
        <td>
            <asp:Button ID="btnExit" runat="server" Text="Thoát" Width="80px" CommandName="Cancel"/>
        </td>
    </tr>
</table>
