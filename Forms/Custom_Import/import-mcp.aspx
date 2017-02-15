<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="import-mcp.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Custom_Import.import_mcp" %>
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

     <div>
        <h1>
        IMPORT DỮ LIỆU MCP</h1>
    </div>


     <div>
            <table>
                <tr>
                    <td nowrap="nowrap">Bước 1 : Tải file mẫu</td>
                    <td>
                        
                    </td>

                    <td></td>
                    <td>
                       
                    </td>
                    <td>
                     
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
            <span class="auto-style1">Chú ý : Không sửa tên cột dữ liệu
            

        </span>
            

        </div>


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
       <br />
        <div>
            Bước 2 : Điều chỉnh dữ liệu
        </div>

          <div>
        Bước 3 : Chọn Nhà phân phối và Nhân viên để Import MCP
        </div>
        <div>
             <table>

                    <tr>

                        <td>
                            <telerik:RadComboBox ID="cbxStore1" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore1_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>

                    <tr>

                        <td>
                            <telerik:RadComboBox ID="cbxEmployee1" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" >
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                 </table>
        </div>

        
        <div>
            Bước 4 : Upload dữ liệu lên Server
        </div>
        <div>
            <%-- For the purpose of this demo the files are discarded.
                In order to store the uploaded files permanently set the TargetFolder property to a valid location. --%>

            <telerik:RadAsyncUpload ID="AsyncUpload1" runat="server" OnFileUploaded="AsyncUpload1_FileUploaded" AllowedFileExtensions="xls,xlsx"></telerik:RadAsyncUpload>
        </div>
       
     

    </telerik:RadAjaxPanel>
     <div>
            Bước 5 : Submit Data
        </div>
     <div>
            <asp:Button ID="btnProcessData" runat="server" Text="Xử lý dữ liệu" />
        </div>
    <div>

    </div>

    
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <script type="text/javascript">

            function OnClientFocusHandler(sender, eventArgs) {
                if (!sender.get_dropDownVisible()) {
                    sender.showDropDown();
                }
            }







        </script>
    </telerik:RadCodeBlock>


</asp:Content>