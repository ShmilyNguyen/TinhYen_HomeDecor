<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category-Edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.Category_Edit" %>
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
        <td>Name</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Level</td>
        <td>
            <asp:TextBox ID="txtLevel" runat="server" TextMode="Number"></asp:TextBox>
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