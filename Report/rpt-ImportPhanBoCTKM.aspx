<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-ImportPhanBoCTKM.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_ImportPhanBoCTKM" %>

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
        IMPORT PHÂN BỔ CTKM CHO NHÀ PHÂN PHỐI, KÊNH KHÁCH HÀNG</h1>
    </div>
    <div>
        <table>
            <tr>
                <td  style="color:#fff"  nowrap="nowrap">Bước 1 : Chọn CTKM</td>
                <td>
                    <telerik:RadComboBox ID="cbxPromo" runat="server"
                        DataTextField="promo_name" DataValueField="promo_id" DropDownAutoWidth="Enabled"
                        Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>

                <td>&nbsp;</td>
                <td>

                    <asp:Button ID="btnDownload" runat="server" Text="Tải file mẫu" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div>
        <br />
        <span class="auto-style1"  style="color:#fff" >Chú ý : Không sửa bất kỳ thông tin nào trên file Excel Template, chỉ được phép điền dữ liệu vào Sheet 'DATA' và những cột sau : Store_ID,Channel_ID !
        </span>
    </div>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <br />
        <div  style="color:#fff" >
            Bước 2 : Điều chỉnh dữ liệu
        </div>

        <div  style="color:#fff" >
            Bước 3 : Upload dữ liệu lên Server
        </div>
        <div>
            <%-- For the purpose of this demo the files are discarded.
                In order to store the uploaded files permanently set the TargetFolder property to a valid location. --%>

            <telerik:RadAsyncUpload ID="AsyncUpload1" runat="server" OnFileUploaded="AsyncUpload1_FileUploaded" AllowedFileExtensions="xls,xlsx"></telerik:RadAsyncUpload>
        </div>
    </telerik:RadAjaxPanel>
    <div  style="color:#fff" >
        Bước 4 : Submit Data
    </div>
    <div>
        <asp:Button ID="btnProcessData" runat="server" Text="Xử lý dữ liệu" OnClick="btnProcessData_Click" />
    </div>
    <div>
    </div>
</asp:Content>