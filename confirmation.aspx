<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoMenu.Master" AutoEventWireup="true" CodeBehind="confirmation.aspx.cs" Inherits="WKS.DMS.WEB.confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div style="width: 100%; text-align: center">
        <br />
        <br />
        <br />

        <div>

            <asp:Label ID="lblMessage" runat="server" Font-Size="Large" Text="Label"></asp:Label>
        </div>
        <br />
        <br />
        <div style="width: 100%; text-align: center">

            <asp:Button ID="btnGoBack" runat="server" OnClick="btnGoBack_Click" Text="Quay về trang chủ" />
        </div>
    </div>
</asp:Content>