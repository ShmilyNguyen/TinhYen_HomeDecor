<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucChotTonKho.ascx.cs" Inherits="WKS.DMS.WEB.Forms.ucChotTonKho" %>


<asp:Panel ID="Panel1" runat="server">

    <div style="background-color: lightgray">
        <div>
            Để xuất kho được của tháng tiếp theo , vui lòng xác nhận việc chốt kho tháng hiện tại :
        </div>
        <div>
            Vui lòng xác nhận chốt tồn kho NPP
        <asp:DropDownList ID="ddlNPP" runat="server" DataTextField="store_name" DataValueField="store_id" Width="200px">
        </asp:DropDownList>
            &nbsp;tháng
        <asp:DropDownList ID="ddlThang" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem Value="6"></asp:ListItem>
            <asp:ListItem Value="7"></asp:ListItem>
            <asp:ListItem Value="8"></asp:ListItem>
            <asp:ListItem Value="9"></asp:ListItem>
            <asp:ListItem Value="10"></asp:ListItem>
            <asp:ListItem Value="11"></asp:ListItem>
            <asp:ListItem Value="12"></asp:ListItem>
        </asp:DropDownList>
            &nbsp;năm
        <asp:DropDownList ID="ddlNam" runat="server">
            <asp:ListItem Value="2015"></asp:ListItem>
            <asp:ListItem Value="2016"></asp:ListItem>
        </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnClosingStock" runat="server" OnClick="btnClosingStock_Click" Text="Chốt kho" OnClientClick="return confirm('Vui lòng xác nhận việc CHỐT TỒN KHO !');" />
        </div>
    </div>
    <div>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
    </div>
    <br />
</asp:Panel>