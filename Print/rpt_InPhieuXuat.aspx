<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_InPhieuXuat.aspx.cs"
    Inherits="WKS.DMS.WEB.Print.rpt_InPhieuXuat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        html * {
            font-size: 10px;
            color: #000 !important;
            font-family: Tahoma;
        }

        .auto-style1 {
            font-weight: bold;
        }

        table {
            /*width: auto;*/
            margin-left: auto;
            margin-right: auto;
        }

        div.dash {
    border-top: 1px dashed #000;
    border-bottom: 1px dashed #000;
}
        div.dash {
    border-left: 0;
    border-right: 0;
    border-top: 1px dashed #000;
    border-bottom: 1px dashed #000;
   
}
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <table width="100%">
            <tr>
                <td>
                    <div>
                        <strong>Nhà phân phối :</strong> <%= _tennhaphanphoi %>
                        <br />
                        <strong>ĐC:</strong><%= _diachi %>
                        <br />
                        <strong>ĐT:</strong>  <%= _dienthoai %>
                    </div>
                    <div>
                        <div>
                            <strong>Khách hàng:</strong> <%= _customer_name %>
                        </div>
                        <div>
                            <strong>Địa chỉ :</strong> <%= _address_full %>
                        </div>
                    </div>
                </td>
                <td valign="top">
                    <div>
                        <table border="0" cellpadding="0" cellspacing="0" style="text-align: justify">
                            <tr>
                                <td colspan="2">
                                    <div>
                                        <strong>PHIẾU XUẤT KHO</strong>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Ngày <%= _ngay %> tháng <%= _thang %> năm <%= _nam %>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>
                                </td>
                                <td>Nợ:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Số : <%= _saleout_code %>
                                </td>
                                <td>Có :
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>

        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="style2">&nbsp; <b>Stt </b>
                    </td>
                    <td class="style2">&nbsp; <b>Hàng hóa </b>
                    </td>
                    <td class="style2">&nbsp; <b>Mã số </b>
                    </td>
                    <td class="style2">&nbsp; <b>Đơn vị tính </b>
                    </td>
                    <td class="style2"><b>&nbsp; Đơn giá </b>
                    </td>
                    <td class="auto-style1">SL</td>
                    <td class="auto-style1">CK(%)</td>
                    <td class="style2">&nbsp; <b>Thành tiền </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <div class="dash"></div>
                    </td>
                </tr>
                <asp:Repeater ID="rptChiTiet" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Stt")) %>
                            </td>
                            <td>
                                <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "item_name")) %>
                            </td>
                            <td>
                                <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "item_code")) %>
                            </td>
                            <td>
                                <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit_name")) %>
                            </td>
                            <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "item_price")) %>
                            </td>
                            <td>
                                <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SLYeuCau"))%>
                            </td>
                            <td>
                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discount"))%>
                            </td>
                            <td>
                                <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ThanhTien"))%>
                            </td>
                        </tr>
                        <tr>
                    <td colspan="8">
                        <div class="dash"></div>
                    </td>
                </tr>
                    </ItemTemplate>
                </asp:Repeater>

                </table>
        </div>

        <div>
            Tổng cộng:<b><%= _GTBan %></b>
        </div>
        <div>
            GT Chiết khấu Line Hàng:<b><%= _GTChietKhauDongHang %></b>
        </div>
        <div>
            Chiết khấu NPP (%):<b><%= _OntopDiscount %></b>
        </div>
        <div>
            GT Chiết khấu NPP:<b><%= _GTChietKhauNPP %></b>
        </div>

        <div>
            Khoản phải thu:<b><%= _ThanhTien %></b>
        </div>

        <div>
            Tổng số tiền (Viết bằng chữ):<b><%= _SoTienBangChu %></b>
        </div>
        <div>
            Số chứng từ gốc kèm theo:
        </div>
        <div style="text-align: right">
            Ngày.......Tháng.........Năm.........
        </div>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <b>Người lập phiếu
                        </b>
                    </td>
                    <td>
                        <b>Người nhận hàng
                        </b>
                    </td>
                    <td>
                        <b>Thủ kho
                        </b>
                    </td>
                    <td>
                        <b>Kế toán trưởng
                            <br />
                            (Hoặc bộ phận có nhu cầu nhập)
                        </b>
                    </td>
                    <td>
                        <b>Giám đốc
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <em>(Ký, Họ Tên) </em>
                    </td>
                    <td>
                        <em>(Ký, Họ Tên) </em>
                    </td>
                    </em>
                <td>
                    <em>(Ký, Họ Tên) </em>
                </td>
                    <td>
                        <em>(Ký, Họ Tên) </em>
                    </td>
                    <td>
                        <em>(Ký, Họ Tên,Đóng dấu) </em>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>