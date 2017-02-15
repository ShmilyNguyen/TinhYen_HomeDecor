<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoMenu.Master" AutoEventWireup="true" CodeBehind="change-password.aspx.cs" Inherits="WKS.DMS.WEB.change_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="box">
   
    <table>

        <tr>
            <td style="color:#fff" colspan="2"> Vui lòng nhập thông tin để thay đổi tài khoản đăng nhập</td>
            
        </tr>

        <tr>
            <td style="color:#fff">UserName : </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">Mật khẩu Cũ: </td>
            <td>
                <asp:TextBox ID="txtPassword_Old" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">Mật khẩu Mới: </td>
            <td>
                <asp:TextBox ID="txtPassword_New1" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">Nhập lại Mật khẩu Mới: </td>
            <td>
                <asp:TextBox ID="txtPassword_New2" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="color:#fff">Nhập mã bảo vệ</td>
            <td>
                <telerik:RadCaptcha ID="RadCaptcha1" Runat="server">
                </telerik:RadCaptcha>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnChangePass" runat="server" Text="Thay đổi  tài khoản" Width="150px" OnClick="btnChangePass_Click" />
            &nbsp;
                <asp:Button ID="btnGoBack" runat="server" Text="Quay về trang chủ" Width="150px" OnClick="btnGoBack_Click" />
            </td>
        </tr>

    </table>

    <div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
    </div>
        </div>

</asp:Content>
