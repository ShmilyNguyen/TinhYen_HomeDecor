<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="ExportPivotExcel.aspx.cs" Inherits="WKS.DMS.WEB.Report.ExportPivotExcel" %>


<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

     <div >
        <table >
            <tr> 
                <td>
                    <asp:DropDownList ID="ddlThang" runat="server" Visible="false">
                         <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem >2</asp:ListItem>
                         <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem >4</asp:ListItem>
                         <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem >6</asp:ListItem>
                         <asp:ListItem >7</asp:ListItem>
                         <asp:ListItem >8</asp:ListItem>
                        <asp:ListItem Selected="True">9</asp:ListItem>
                         <asp:ListItem >10</asp:ListItem>
                         <asp:ListItem >11</asp:ListItem>
                         <asp:ListItem >12</asp:ListItem>

                    </asp:DropDownList>

                </td>

                <td>
                    <asp:DropDownList ID="ddlNam" runat="server" Visible="false">

                        
                     
                         <asp:ListItem >2015</asp:ListItem>
                        <asp:ListItem Selected="True">2016</asp:ListItem>
                      
                    </asp:DropDownList>

                </td>

                <td style="margin-left: 160px">

                   

                </td>
                <td>

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

                    <asp:Button ID="btnExportExcel1" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>




    </div>


     


</asp:Content>
