<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sys-config-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.Sys.sys_config_edit" %>
<div>
    <asp:HiddenField ID="hdf_id" runat="server" />
</div>

<table>
    <tr>
        <td>Key</td>
        <td>
            <asp:TextBox ID="txtParam" runat="server" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>Value</td>
        <td>

            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>

        </td>

        <td>

            Note</td>

        <td>

            <asp:TextBox ID="txtNote" runat="server" Width="300px" Enabled="False"></asp:TextBox>

        </td>

        <td>

            &nbsp;</td>

        <td>

             <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" 
                Width="100px"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" 
                Width="100px"/>

        </td>

    </tr>
    </table>
