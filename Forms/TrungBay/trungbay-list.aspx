<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="trungbay-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.TrungBay.trungbay_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <asp:Repeater ID="rptTrungBay" runat="server">




        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>

            <div class="panel-group">
                <div class="panel panel-primary">
                    <div class="panel-heading"><%# Eval("email") %></div>
                    <div class="panel-body">
                        
                        <table border="1">
                            <tr>
                                <td>Cửa hàng :</td><td><%# Eval("cuahang") %></td>
                            </tr>
                            <tr>
                                <td>Địa chỉ :</td><td><%# Eval("diachi") %></td>
                            </tr>
                            <tr>
                                <td>Ngày :</td><td><%# Eval("ngay") %></td>
                            </tr>
                            <tr>
                                <td>Scheme :</td><td><%# Eval("scheme") %></td>
                            </tr>
                            <tr>
                                <td>Chấm điểm :</td><td><%# Eval("chamdiem") %></td>
                            </tr>
                            <tr>
                                <td>Đánh giá :</td><td><%# Eval("danhgia") %></td>
                            </tr>
                            <tr>
                                <td>Ghi chú :</td><td><%# Eval("ghichu") %></td>
                            </tr>

                            <tr>
                                <td>GPS MCP :</td><td><%# Eval("gps1") %></td>
                            </tr>

                            <tr>
                                <td>GPS Thực tế :</td><td><%# Eval("gps2") %></td>
                            </tr>
                        </table>

                        <table border="1">
                            <tr>
                                <td><img src='<%# "http://112.78.3.117/vietinfo/MobileUpload/" + Eval("img_ftp_uri1") %>' width="200px" height="150px"/></td>
                                 <td><img src='<%# "http://112.78.3.117/vietinfo/MobileUpload/" + Eval("img_ftp_uri2") %>'  width="200px" height="150px"/></td>
                                 <td><img src='<%# "http://112.78.3.117/vietinfo/MobileUpload/" + Eval("img_ftp_uri3") %>'  width="200px" height="150px"/></td>
                                
                            </tr>
                        </table>

                    </div>
                </div>
            </div>



        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>


</asp:Content>
