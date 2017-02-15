<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="target-sellin-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.target_sellin_edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table>
    <tr>
        <td>Nhà phân phối</td>
        <td>
                    <asp:TextBox ID="txtNhaPhanPhoi" runat="server" Enabled="False" Width="400px"></asp:TextBox>
        </td>
        <td>
                    <asp:HiddenField ID="hdf_row_id" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Năm</td>
        <td>
                    <asp:TextBox ID="txtNam" runat="server" Enabled="False" Width="200px"></asp:TextBox>
        </td>
        <td>
                    <asp:HiddenField ID="hdf_store_id" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Tháng</td>
        <td>
                    <asp:TextBox ID="txtThang" runat="server" Enabled="False" Width="200px"></asp:TextBox>
        </td>
        <td>
                    &nbsp;</td>
    </tr>
    <tr>
        <td>Chỉ tiêu doanh số nhập</td>
        <td>
                    <asp:TextBox ID="txtChiTieu" runat="server" TextMode="Number" Width="200px">0</asp:TextBox>
        </td>
        <td>
                    &nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" 
                Width="100px"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" 
                Width="100px"/></td>
        <td>
            &nbsp;</td>
    </tr>
</table>
