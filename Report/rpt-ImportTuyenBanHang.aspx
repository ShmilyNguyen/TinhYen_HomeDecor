<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-ImportTuyenBanHang.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_ImportTuyenBanHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <div  style="color:#fff" >
        <h1>
        IMPORT TUYẾN BÁN HÀNG</h1>
    </div>

     <div>
            <table>
                <tr>
                    <td style="color:#fff"  nowrap="nowrap">Bước 1 : Chọn nhà phân phối</td>
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
                    <td style="color:#fff" >Năm
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlNam" runat="server">
                        <asp:ListItem >2015</asp:ListItem>
                        <asp:ListItem Selected="True">2016</asp:ListItem>

                    </asp:DropDownList>
                    </td>
                    <td>

                        &nbsp;</td>
                    <td>

                        <asp:Button ID="btnDownload" runat="server" Text="Tải file mẫu" OnClick="btnDownload_Click" />

                    </td>
                </tr>
            </table>
        </div>

        <div>
            <br />
            <span class="auto-style1"  style="color:#fff" >Chú ý : Không sửa bất kỳ thông tin nào trên file Excel Template, chỉ được phép điền dữ liệu vào Sheet 'DATA' và những cột sau : Customer_Code, Route_ID, Channel_ID, Province, District, Ward, Street, Address, Add_Number, Phone, Mobile, Email !
            

        </span>
            

        </div>


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
       <br />
         <div style="color:#fff" >
            Bước 2 : Điều chỉnh dữ liệu
        </div>

        <div style="color:#fff" >
            Bước 3 : Upload dữ liệu lên Server
        </div>
        <div>
            <%-- For the purpose of this demo the files are discarded.
                In order to store the uploaded files permanently set the TargetFolder property to a valid location. --%>

            <telerik:RadAsyncUpload ID="AsyncUpload1" runat="server" OnFileUploaded="AsyncUpload1_FileUploaded" AllowedFileExtensions="xls,xlsx"></telerik:RadAsyncUpload>
        </div>
       
       
    </telerik:RadAjaxPanel>
     <div style="color:#fff" >
            Bước 4 : Submit Data
        </div>
     <div>
            <asp:Button ID="btnProcessData" runat="server" Text="Xử lý dữ liệu" OnClick="btnProcessData_Click" />
        </div>
    <div>

    </div>
</asp:Content>