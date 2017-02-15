<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HangHoa-Edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.HangHoa_Edit" %>
<style type="text/css">

a {
  background: transparent;
}

    </style>
<table>
    <tr>
        <td>Hàng hóa ID</td>
        <td>
            <asp:TextBox ID="txtItem_ID" runat="server" Width="400px" BackColor="Silver" Enabled="False">0</asp:TextBox>(Mã tự động tăng)</td>
    </tr>
    <tr>
        <td>Hàng hóa Code</td>
        <td>
            <asp:TextBox ID="txtItem_Code" runat="server" Width="400px"  BackColor="Silver"></asp:TextBox>(Chữ hoa, không dấu)</td>
    </tr>
    <tr>
        <td>Tên hàng hóa</td>
        <td><asp:TextBox ID="txtItem_Name" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Đơn vị tính</td>
        <td>
            <asp:DropDownList ID="ddlUnit" runat="server" Width="400px" DataTextField="unit_name" DataValueField="unit_id"></asp:DropDownList></td>
    </tr>

     <tr>
        <td>Phân loại ngành hàng 1</td>
        <td>
            <asp:DropDownList ID="ddlCategory1" runat="server" Width="400px" DataTextField="category_name" DataValueField="item_category_id"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Phân loại ngành hàng 2</td>
        <td>
            <asp:DropDownList ID="ddlCategory2" runat="server" Width="400px" DataTextField="category_name" DataValueField="item_category_id"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Phân loại ngành hàng 3</td>
        <td>
            <asp:DropDownList ID="ddlCategory3" runat="server" Width="400px" DataTextField="category_name" DataValueField="item_category_id"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Phân loại ngành hàng 4</td>
        <td>
            <asp:DropDownList ID="ddlCategory4" runat="server" Width="400px" DataTextField="category_name" DataValueField="item_category_id"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Phân loại ngành hàng 5</td>
        <td>
            <asp:DropDownList ID="ddlCategory5" runat="server" Width="400px" DataTextField="category_name" DataValueField="item_category_id"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Quy cách 1 (Thùng, Lốc)</td>
        <td>
            <asp:TextBox ID="txtQuyCach1" runat="server">6x6</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Quy cách 2 (Số lượng)</td>
        <td>
            <asp:TextBox ID="txtQuyCach2" runat="server" TextMode="Number">0</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Size</td>
        <td>
            <asp:TextBox ID="txtSize" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Weight</td>
        <td>
            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Vol</td>
        <td>
            <asp:TextBox ID="txtVol" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" 
                Width="100px"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" 
               Width="100px"/></td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

</table>
