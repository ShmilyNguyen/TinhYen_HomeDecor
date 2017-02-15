<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WKS.DMS.WEB.Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/Forms/ucChotTonKho.ascx" TagPrefix="uc1" TagName="ucChotTonKho" %>
<%@ Register Src="~/Forms/ucThongBao.ascx" TagPrefix="uc1" TagName="ucThongBao" %>



<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="styles/default.css" rel="stylesheet" />
    
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>


    <style>
        .jumbotron {
            background-color:white;
            opacity: 01;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
                        <uc1:ucChotTonKho runat="server" ID="ucChotTonKho" />


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
 <telerik:radpagelayout runat="server" id="radpagelayout2">
        <rows>
            <telerik:layoutrow>
                <columns>
                    <telerik:layoutcolumn cssclass="jumbotron">
                        <uc1:ucThongBao runat="server" id="ucThongBao" />

                    </telerik:layoutcolumn>
                </columns>
            </telerik:layoutrow>
         
        </rows>
    </telerik:radpagelayout>
</asp:Content>
