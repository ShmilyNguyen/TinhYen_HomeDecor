<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-RawData.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_RawData" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="color:#fff" >
        Lưu ý :  xuất báo cáo dữ liệu thô theo từng tháng.
    </div>
    <div>
        <table>
            <tr>
              
                    <asp:DropDownList ID="ddlThang" runat="server" Visible="false">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
               
                </td>
                <td style="margin-left: 160px">
                    <asp:DropDownList ID="ddlNam" runat="server" Visible="false">
                        <asp:ListItem>2016</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;</td>
                
            </tr>
        </table>
    </div>

      <div>
        <table>
            <tr>
                <td style="color:#fff">Xem báo cáo Từ ngày</td>
                <td>
                        <telerik:RadDatePicker ID="rdpTuNgay" runat="server">
                        </telerik:RadDatePicker>
                </td>
                <td style="color:#fff">
                    Đến ngày</td>
                <td>
                        <telerik:RadDatePicker ID="rdpDenNgay" runat="server">
                        </telerik:RadDatePicker>
                </td>
                
                <td>
                 <asp:Button ID="Button1" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel_Click" />
                </td>
            </tr>
        </table>
    </div>


   
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

</asp:Content>

