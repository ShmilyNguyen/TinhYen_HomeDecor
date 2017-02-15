<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoMenu.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WKS.DMS.WEB.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <style type="text/css">
        
    </style>

    <div class="box">

        <table>
            <tr>
                <td style="color:#fff">TÀI KHOẢN</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="color:#fff">MẬT KHẨU
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <br />
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Đăng nhập" Width="100%" Height="30px" />
                    <br />
                     <br />
               <asp:Button ID="btnChangePass" runat="server" OnClick="btnLogin_Click" Text="Đổi mật khẩu" Width="100%" Height="30px"  PostBackUrl="~/change-password.aspx" />

                
                </td>
            </tr>
        </table>
    </div>
</asp:Content>