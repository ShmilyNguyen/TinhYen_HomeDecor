<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.promo_edit" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">&nbsp;<table>
    <tr>
        <td>ID</td>
        <td>
            <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="#FFCC66"
                Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Code</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" Width="400px"  BackColor="#FFCC66"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Tên CTKM</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"  BackColor="#FFCC66" 
                TextMode="MultiLine"></asp:TextBox></td>
    </tr>

     <tr>
        <td>Từ ngày</td>
        <td>
                <telerik:RadDatePicker ID="rdpTuNgay" Runat="server">
                </telerik:RadDatePicker>
            </td>
    </tr>
    <tr>
        <td>Đến ngày</td>
        <td>
                <telerik:RadDatePicker ID="rdpDenNgay" Runat="server">
                </telerik:RadDatePicker>
        </td>
    </tr>

    <tr>
        <td>Loại KM</td>
        <td>
            <asp:DropDownList ID="ddlPromoType" runat="server" Width="400px"
                DataTextField="region_name" DataValueField="region_id"
                style="margin-bottom: 0px">
                <asp:ListItem>TANGHANG</asp:ListItem>
                <asp:ListItem>CHIETKHAU</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
                &nbsp;</td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
                <dx:ASPxCheckBox ID="chckActive" runat="server" Text="Còn hoạt động ?">
                </dx:ASPxCheckBox>
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
                <dx:ASPxCheckBox ID="chckApproved" runat="server" Text="Đã được duyệt ?">
                </dx:ASPxCheckBox>
        </td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" 
                Width="100px" onclick="btnSave_Click" />
            <asp:Button ID="btnExit" runat="server" Text="Thoát" 
                Width="100px" OnClick="btnExit_Click" /></td>
    </tr>

    <tr>
        <td>Định nghĩa chiết khấu</td>
        <td>
            <asp:Button ID="btnRule_ChietKhau" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btnRule_ChietKhau_Click" /></td>
    </tr>

    <tr>
        <td>Định nghĩa mua và tặng hàng cùng loại (Multi Level)</td>
        <td>
            <asp:Button ID="btn_TangHang_1Level" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btn_TangHang_1Level_Click" /></td>
    </tr>

    <tr>
        <td>Định nghĩa mua và tặng hàng hỗn hợp</td>
        <td>
            <asp:Button ID="btn_TangHang_MultiLevel" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btn_TangHang_MultiLevel_Click" /></td>
    </tr>

    <tr>
        <td>Phân bổ nhà phân phối</td>
        <td>
            <asp:Button ID="btn_PhanBoNP" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btn_PhanBoNP_Click" /></td>
    </tr>
</table>
<div>
</div>


<div>


                <asp:TextBox ID="txtVol" runat="server" TextMode="Number" Visible="False">0</asp:TextBox>


</div>

<div>
                <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Visible="False">0</asp:TextBox>
</div>

<div>

</div>
    <div>

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
    <script type="text/javascript">

        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("Button") >= 0) {
                args.set_enableAjax(false);
            }
        }


        function OnClientFocusHandler(sender, eventArgs) {
            if (!sender.get_dropDownVisible()) {
                sender.showDropDown();
            }
        }
    </script>
</telerik:RadCodeBlock>
    </div>
</asp:Content>