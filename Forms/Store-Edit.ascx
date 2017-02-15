<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Store-Edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.Store_Edit" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<style type="text/css">
    .auto-style1 {
        height: 26px;
    }
</style>
<table>
    <tr>
        <td>&nbsp;</td>
        <td>
            Thông tin chi tiết Nhà Phân Phối</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>ID</td>
        <td>
            <asp:TextBox ID="txtID" runat="server" BackColor="#CCCCCC" Enabled="False"></asp:TextBox></td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Mã NPP</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" Width="500px" BackColor="Silver"></asp:TextBox>(Dữ liệu bắt buộc)</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Tên nhà phân phối</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="500px" BackColor="Silver"></asp:TextBox>(Dữ liệu bắt buộc)</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Vùng</td>
        <td>
            <asp:DropDownList ID="ddlRegion" runat="server" Width="500px" DataTextField="region_name" DataValueField="region_id"></asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td>Khu vực</td>
        <td>
            <asp:DropDownList ID="ddlArea" runat="server" Width="500px" DataTextField="area_name" DataValueField="area_id"></asp:DropDownList></td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td>Tỉnh</td>
        <td>
            <asp:DropDownList ID="ddlProvince" runat="server" Width="500px" DataTextField="province_name" DataValueField="geo_province_id"></asp:DropDownList></td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td>Địa chỉ&nbsp;</td>
        <td>
            <asp:TextBox ID="txtDiaChi" runat="server" Width="500px" BackColor="Silver"></asp:TextBox>
            (Dữ liệu bắt buộc)</td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td>Mã số thuế</td>
        <td>
            <asp:TextBox ID="txtMaSoThue" runat="server" BackColor="Silver"></asp:TextBox>
            (Dữ liệu bắt buộc)</td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td>Điện thoại</td>
        <td>
            <asp:TextBox ID="txtDienThoai" runat="server" BackColor="Silver"></asp:TextBox>
            (Dữ liệu bắt buộc)</td>
        <td>
            &nbsp;</td>
    </tr>

     <tr>
        <td class="auto-style1">Tiền tố mã hệ thống</td>
        <td class="auto-style1">
            <asp:TextBox ID="txtPrefixCode" runat="server" ></asp:TextBox></td>
        <td class="auto-style1">
            </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <dx:ASPxCheckBox ID="chckActive" runat="server" Text="Còn hoạt động ?">
            </dx:ASPxCheckBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" Width="100px"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" Width="100px"/></td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

</table>
<div>
    
</div>