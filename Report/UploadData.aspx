<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="UploadData.aspx.cs" Inherits="WKS.DMS.WEB.Forms.UploadData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <div>
            <table>
                <tr>
                    <td  style="color:#fff"  nowrap="nowrap">Bước 1 : Chọn nhà phân phối</td>
                    <td>
                        <telerik:RadComboBox ID="cbxStore" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>

                    <td style="color:#fff" >Tháng</td>
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
                        </asp:DropDownList></td>
                    <td  style="color:#fff" >Năm
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNam" runat="server">
                            <asp:ListItem Selected="True">2015</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>

        <div>
            <table>
                <tr>
                    <td nowrap="nowrap">Bước 2 : Tải file dữ liệu mẫu </td>
                    <td>
                        <asp:DropDownList ID="ddlReportName" runat="server" Width="300px">
                            <asp:ListItem Value="chitieudoanhsokhachhang">Chỉ tiêu doanh số của Khách Hàng</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnDownload" runat="server" Text="Tải file mẫu" OnClick="btnDownload_Click" /></td>
                </tr>
            </table>
        </div>
        <div>
            Bước 3 : Xử lý dữ liệu
        </div>


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Height="200px" Width="300px">
       
        <div>
            Bước 4 : Upload dữ liệu lên Server
        </div>
        <div>
            <%-- For the purpose of this demo the files are discarded.
                In order to store the uploaded files permanently set the TargetFolder property to a valid location. --%>

            <telerik:RadAsyncUpload ID="AsyncUpload1" runat="server" OnFileUploaded="AsyncUpload1_FileUploaded"></telerik:RadAsyncUpload>
            <telerik:RadProgressArea ID="RadProgressArea1" runat="server"></telerik:RadProgressArea>
        </div>
       
       
    </telerik:RadAjaxPanel>
     <div>
            Bước 5 : Submit Data
        </div>
     <div>
            <asp:Button ID="btnProcessData" runat="server" Text="Xử lý dữ liệu" />
        </div>
</asp:Content>