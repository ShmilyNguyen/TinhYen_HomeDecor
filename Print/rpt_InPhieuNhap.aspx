<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_InPhieuNhap.aspx.cs" Inherits="WKS.DMS.WEB.Print.rpt_InPhieuNhap" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        
        html *
        {
            font-size: 10px;
            color: #000 !important;
            font-family: Tahoma;
        }
      
        .style1
        {
            font-weight: bold;
            text-align: center;
        }
        .style2
        {
            text-align: center;
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

        .auto-style1 {
            height: 12px;
        }

        </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td><div>
        <%= _tennhaphanphoi %>
        <br />
         ĐC:<%= _diachi %> 
                    <br />ĐT:  <%= _dienthoai %>
    </div>


<div>
        <div>
            Họ và tên người giao: KHO</div>
        <div>
            Theo hóa đơn 
            số............ngày.......tháng.......năm.........của.......................... </div>
        <div>
            Nhập tại kho: Thành phẩm </div>
        <div>
            Địa điểm:</div>
    </div>
                </td>
                <td valign="top">
                    <div>
                        <table border="0" cellpadding="0" cellspacing="0" style="text-align: justify">
                            <tr>
                                <td colspan="2" class="auto-style1">
                                    <div>
                                        <strong>PHIẾU NHẬP KHO</strong>
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
                                <td>Số : <%= _sellin_code %>
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
                <td class="style2">
                    <strong>Stt</strong></td>
                <td class="style2">
                    &nbsp; <b>Tên, nhãn hiệu, quy cách,
                        <br />
                        phẩm chất vật tư, dụng cụ sản phẩm, hàng hóa </b>
                </td>
                <td class="style2">
                    &nbsp; <b>Mã số </b>
                </td>
                <td class="style2">
                    &nbsp; <b>ĐVT </b>
                </td>
                <td class="style2">
                    &nbsp; <b>Đơn giá </b>
                </td>
                <td class="style1">
                    SL</td>
               
              
                <td class="style2">
                    <b>&nbsp; Thành tiền </b>
                </td>
            </tr>
          <tr>
                        <td colspan="7" >
                            
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
                        <td>
                            <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "item_price")) %>
                        </td>
                       
                        <td><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SLYeuCau"))%>
                           
                        </td>
                        <td>
                           <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ThanhTien"))%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" >
                            
                         <div class="dash"></div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    TỔNG CỘNG
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <%= _ThanhTien %></td>
                <td>
                     </td>
                <td>
                   
                </td>
            </tr>
        </table>
    </div>
   

    <div>
        Tổng số tiền (Viết bằng chữ):<b><%= _SoTienBangChu %></b>
    </div>
    <div>
        Số chứng từ gốc kèm theo:
    </div>
    <div style="text-align:right">
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
                    <b>Người giao hàng
                </b>
                </td>
                <td>
                    <b>Thủ kho
                </b>
                </td>
                <td>
                    <b>Kế toán trưởng <br />(Hoặc bộ phận có nhu cầu nhập)
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
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
   
    </form>
</body>
</html>