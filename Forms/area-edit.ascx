<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="area-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.area_edit" %>
<table>
    <tr>
        <td>ID</td>
        <td>
            <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="#FFCC66" Enabled="False">0</asp:TextBox></td>
    </tr>
    <tr>
        <td>Code</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" Width="400px"  BackColor="#FFCC66"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Name</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"  BackColor="#FFCC66"></asp:TextBox></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update"/>
            <asp:Button ID="Button2" runat="server" Text="Xóa" CommandName="Delete"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel"/></td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

</table>
<div>
    
</div>