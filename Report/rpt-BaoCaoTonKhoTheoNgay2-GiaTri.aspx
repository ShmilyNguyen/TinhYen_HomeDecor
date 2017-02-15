<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-BaoCaoTonKhoTheoNgay2-GiaTri.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_BaoCaoTonKhoTheoNgay2_GiaTri " %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div>
        <table>
            <tr>
                <td style="color:#fff">Năm</td>
                <td>
                     
                   <asp:DropDownList ID="ddlNam" runat="server">
                        <asp:ListItem >2015</asp:ListItem>
                        <asp:ListItem >2016</asp:ListItem>
                       <asp:ListItem >2017</asp:ListItem>
                       <asp:ListItem >2018</asp:ListItem>
                       <asp:ListItem >2019</asp:ListItem>
                       <asp:ListItem >2020</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="color:#fff">Tháng </td>
                <td>
                    <asp:DropDownList ID="ddlThang" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>

                </td>

                 <td  style="color:#fff">Nhà phân phối</td>
            <td>
                        <telerik:RadComboBox ID="cbxStore" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" >
                        </telerik:RadComboBox>
                    </td>

                <td style="margin-left: 160px">

                    &nbsp;</td>
                <td>

                    <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel_Click" />

                </td>
            </tr>
        </table>
    </div>
    <div>


     



    </div>


</asp:Content>

