<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="route-weekydays-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.Route.route_weekydays_edit" %>
 <table>
        <tr>
            <td>Route WeekDays ID</td>
            <td>
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>


            </td>
            <td>Route WeekDays Code</td>
            <td><asp:TextBox ID="txtCode" runat="server"></asp:TextBox></td>
            <td>Route WeekDays Name</td>
            <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>

             <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel"/></td>


        </tr>
    </table>