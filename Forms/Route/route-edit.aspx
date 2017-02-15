<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="route-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.route_edit" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">&nbsp;<table>
    <tr>
        <td>ID</td>
        <td style="margin-left: 40px">
            <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="Silver"></asp:TextBox>(Mã tự động tăng)</td>
    </tr>
    <tr>
        <td>Mã tuyến</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" Width="400px"  BackColor="Silver"></asp:TextBox>(Chữ hoa, không dấu)</td>
    </tr>
    <tr>
        <td>Tên tuyến</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox></td>
    </tr>

     <tr>
        <td>Nhà phân phối</td>
        <td>
                     <telerik:radcombobox ID="cbxNhaPhanPhoi" runat="server"
                        OnClientFocus="OnClientFocusHandler" DataTextField="store_name"
                        DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains"
                        Width="400px" OpenDropDownOnFocus="True"
                        onselectedindexchanged="cbxNhaPhanPhoi_SelectedIndexChanged"
                         xmlns:telerik="telerik.web.ui" autopostback="True">
                      </telerik:radcombobox>
            </td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" CommandName="Update"
                Width="100px" onclick="btnSave_Click" />
            <asp:Button ID="btnExit" runat="server" Text="Thoát" CommandName="Cancel"
                Width="100px" OnClick="btnExit_Click" />

              <asp:Button ID="btnDelete" runat="server" Text="Hủy"
                Width="100px" OnClientClick="return confirm('Vui lòng xác nhận việc xóa Tuyến Bán Hàng và các Tuyến Thứ thuộc Tuyến Bán Hàng ?');" OnClick="btnDelete_Click" />

       

            


        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<div>
</div>

    <div>

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
    <script type="text/javascript">

        function OnClientFocusHandler(sender, eventArgs) {
            if (!sender.get_dropDownVisible()) {
                sender.showDropDown();
            }
        }
    </script>
</telerik:RadCodeBlock>
    </div>
</asp:Content>