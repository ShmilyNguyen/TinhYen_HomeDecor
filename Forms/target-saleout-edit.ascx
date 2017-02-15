<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="target-saleout-edit.ascx.cs" Inherits="WKS.DMS.WEB.Forms.target_saleout_edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table>
    <tr>
        <td>&nbsp;</td>
        <td>
                    <asp:HiddenField ID="hdf_row_id" runat="server" />
                    <asp:HiddenField ID="hdf_employee_id" runat="server" />
                    <asp:HiddenField ID="hdf_store_id" runat="server" />
        </td>
        <td>
                    &nbsp;</td>
        <td>
                    &nbsp;</td>
    </tr>
    <tr>
        <td>Nhà phân phối</td>
        <td>
                    <asp:TextBox ID="txtNhaPhanPhoi" runat="server" Enabled="False" Width="400px"></asp:TextBox>
        </td>
        <td>
                    Nhân viên</td>
        <td>
                    <asp:TextBox ID="txtNhanVien" runat="server" Enabled="False" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Tháng</td>
        <td>
                    <asp:TextBox ID="txtThang" runat="server" Enabled="False" Width="200px"></asp:TextBox>
        </td>
        <td>
                    Năm</td>
        <td>
                    <asp:TextBox ID="txtNam" runat="server" Enabled="False" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Chỉ tiêu doanh số </td>
        <td>
                    <asp:TextBox ID="txtTargetSaleout" runat="server" TextMode="Number" Width="200px">0</asp:TextBox>
        </td>
        <td>
                    Chỉ tiêu đơn hàng</td>
        <td>
                    <asp:TextBox ID="txtTargetOrder" runat="server" TextMode="Number" Width="200px">0</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Chỉ tiêu doanh số tập trung</td>
        <td>
                    <asp:TextBox ID="txtTargetFocusSaleout" runat="server" TextMode="Number" Width="200px">0</asp:TextBox>
        </td>
        <td>
                    Chỉ tiêu đơn hàng tập trung</td>
        <td>
                    <asp:TextBox ID="txtTargetFocusOrder" runat="server" TextMode="Number" Width="200px">0</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Chỉ tiêu cửa hiệu mua hàng</td>
        <td>
                    <asp:TextBox ID="txtTargetActiveOutlet" runat="server" TextMode="Number" Width="200px">0</asp:TextBox>
        </td>
        <td>
                    &nbsp;</td>
        <td>
                    &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
                    &nbsp;</td>
        <td>
                    &nbsp;</td>
        <td>
                    &nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Lưu" CommandName="Update" 
                Width="100px"/>
            <asp:Button ID="Button3" runat="server" Text="Thoát" CommandName="Cancel" 
                Width="100px"/></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
