<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="customer-channel-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.customer_channel_edit" %>
<style type="text/css">
    .auto-style1 {
        height: 30px;
    }
</style>
<table>
    <tr>
        <td>ID</td>
        <td>
            <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="Silver" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Code</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" Width="400px"  BackColor="Silver"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Phân loại Khách hàng / cửa hàng</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox></td>
    </tr>

    <tr>
        <td>Kênh Khách hàng / cửa hàng</td>
        <td>
            <asp:DropDownList ID="ddlChannelDist" runat="server" DataTextField="channel_dist_name" DataValueField="channel_dist_id" Width="400px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style1"></td>
        <td class="auto-style1">
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" Width="100px"/>
            <asp:Button ID="Button2" runat="server" Text="Xóa" CommandName="Delete" Width="100px"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" Width="100px"/></td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

</table>
<div>
    
</div>