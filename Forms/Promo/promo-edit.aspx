<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Promo.promo_edit" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">&nbsp;<table>
    <tr>
        <td>ID</td>
        <td>
            <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="#FFCC66"
                Enabled="False"></asp:TextBox></td>
        <td>
            <em>&nbsp;(Mã tự động tăng)</em>&nbsp;</td>
    </tr>
    <tr>
        <td>Code</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" Width="400px"  BackColor="#FFCC66"></asp:TextBox></td>
        <td>
            <em>(Mã CTKM - Tiếng Việt, chữ hoa, không dấu)</em></td>
    </tr>
    <tr>
        <td>Tên CTKM</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"  BackColor="#FFCC66" 
                TextMode="MultiLine"></asp:TextBox></td>
        <td><em>(Tên CTKM - Chú thích chi tiết CTKM)</em></td>
    </tr>

     <tr>
        <td>Từ ngày</td>
        <td>
                <telerik:RadDatePicker ID="rdpTuNgay" Runat="server">
                </telerik:RadDatePicker>
            </td>
        <td>
                &nbsp;</td>
    </tr>
    <tr>
        <td>Đến ngày</td>
        <td>
                <telerik:RadDatePicker ID="rdpDenNgay" Runat="server">
                </telerik:RadDatePicker>
        </td>
        <td>
                &nbsp;</td>
    </tr>

    <tr>
        <td>Loại KM</td>
        <td>
            <asp:DropDownList ID="ddlPromoType" runat="server" Width="400px"
                style="margin-bottom: 0px">
                <asp:ListItem>GWP/PWP</asp:ListItem>
                <asp:ListItem>LINE-DISCOUNT</asp:ListItem>
                
               
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>Cấp KM Áp Dụng</td>
        <td>
            <asp:DropDownList ID="ddlPromoLevel" runat="server" Width="400px"
                style="margin-bottom: 0px">
                <asp:ListItem>LINE</asp:ListItem>
                
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>Đối tượng áp dụng</td>
        <td>
            <asp:DropDownList ID="ddlPromoApply" runat="server" Width="400px"
                style="margin-bottom: 0px">
                <asp:ListItem>CATEGORY</asp:ListItem>
                <asp:ListItem>SIZE</asp:ListItem>
                <asp:ListItem>ITEM</asp:ListItem>
               
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>Trạng thái</td>
        <td>
            <asp:DropDownList ID="ddlPromoStatus" runat="server" Width="400px"
                style="margin-bottom: 0px">
                <asp:ListItem>PENDING</asp:ListItem>
                <asp:ListItem>APPROVED</asp:ListItem>
                <asp:ListItem>REJECTED</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" 
                Width="100px" onclick="btnSave_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnExit" runat="server" Text="Thoát" 
                Width="100px" OnClick="btnExit_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hủy CTKM" 
                Width="100px" onclick="btnDelete_Click"  OnClientClick="return confirm('Vui lòng xác nhận việc xóa Chương trình khuyến mãi này?');" />
            </td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>Định nghĩa chiết khấu (Line Discount)&nbsp;</td>
        <td>
            <asp:Button ID="btnRule_Discount" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btnRule_Discount_Click" /></td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>Định nghĩa chương trình (GWP / PWP)</td>
        <td>
            <asp:Button ID="btnRule1" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btnRule1_Click" /></td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>Phân bổ nhà phân phối</td>
        <td>
            <asp:Button ID="btn_PhanBoNP" runat="server" Text="Định nghĩa" 
                Width="100px" OnClick="btn_PhanBoNP_Click" /></td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<div>
</div>


<div>


</div>

<div>
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