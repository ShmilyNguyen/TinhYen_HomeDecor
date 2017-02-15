<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="customer-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.customer_edit" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href="../../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

     <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart"  
            runat="server" CssClass="grid_wrapper">

    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="color:#fff">
                ID
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server" BackColor="Silver" Enabled="False"></asp:TextBox>
                (Mã tự động tăng)</td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">
                Nhà phân phối</td>
            <td>
                    <telerik:RadComboBox ID="cbxNhaPhanPhoi" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="store_name" 
                        DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True" 
                        onselectedindexchanged="cbxNhaPhanPhoi_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color:#fff">
                Mã khách hàng</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" BackColor="Silver" Width="400px" Enabled="False"></asp:TextBox>
                (Chữ hoa, không dấu)</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color:#fff">
                Tên KH
            </td>
            <td>
                <asp:TextBox ID="txtTenKH" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">
                Điện thoại
            </td>
            <td>
                <asp:TextBox ID="txtDienThoai" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">
                Địa chỉ đầy đủ
            </td>
            <td>
                <asp:TextBox ID="txtDiaChiDayDu" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>

       

        <tr>
            <td style="color:#fff">
                Tỉnh - Thành phố
            </td>
            <td>
                    <telerik:RadComboBox ID="cbxProvince" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="province_name" 
                        DataValueField="province_name" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxProvince_SelectedIndexChanged">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="color:#fff">
                Quận - Huyện
            </td>
            <td>
                    <telerik:RadComboBox ID="cbxDistrict" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="district_name" 
                        DataValueField="district_name" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxDistrict_SelectedIndexChanged">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>

        <tr>
            <td style="color:#fff">
                Xã - Phường</td>
            <td>
                    <telerik:RadComboBox ID="cbxWard" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="ward_name" 
                        DataValueField="ward_name" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxWard_SelectedIndexChanged">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
                Đường- chợ</td>
            <td>
                    <telerik:RadComboBox ID="cbxStreet" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="street_name" 
                        DataValueField="geo_street_id" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
                Số nhà</td>
            <td>
                <asp:TextBox ID="txtSoNha" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
               </td>
            <td>
                    <telerik:RadComboBox ID="cbxDistChannel" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="distribute_channel_name" 
                        DataValueField="distribute_channel_id" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True" Visible ="false">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
                Kênh khách hàng</td>
            <td>
                    <telerik:RadComboBox ID="cbxChannel" runat="server" 
                        OnClientFocus="OnClientFocusHandler" DataTextField="channel_name" 
                        DataValueField="customer_channel_id" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
                Market</td>
            <td>
                    <telerik:RadComboBox ID="cbxMarket" runat="server" OnClientFocus="OnClientFocusHandler" 
                       DataTextField="market_name" 
                        DataValueField="customer_market_id" DropDownAutoWidth="Enabled" Filter="Contains" 
                        Width="400px" OpenDropDownOnFocus="True">
                    </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
                Nhân viên</td>
            <td>
                     <asp:DropDownList ID="cbxEmployee" runat="server" DataTextField="employee_name" DataValueField="employee_id" Width="200px">
                     </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td style="color:#fff">
                Tuyến bán hàng</td>
            <td>
                     <telerik:radcombobox ID="cbxWeekDays" runat="server"
                        OnClientFocus="OnClientFocusHandler" DropDownAutoWidth="Enabled" Filter="Contains"
                        Width="200px" OpenDropDownOnFocus="True"
                       
                        DataTextField="route_name" DataValueField="route_id">
                      </telerik:radcombobox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
            <td>
                <dx:ASPxCheckBox ID="chckActive" runat="server" Text="Còn hoạt động ?">
                </dx:ASPxCheckBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>



        <tr>
            <td style="color:#fff">
               Ngày tạo</td>
            <td>
                   <asp:TextBox ID="txtNgayTao" runat="server"  Enabled="False"></asp:TextBox> 
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>



        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Lưu" Width="100px" 
                    onclick="btnSave_Click" />
                <asp:Button ID="btnExit" runat="server" Text="Thoát" Width="100px" OnClick="btnExit_Click" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
            <td>
            <asp:TextBox ID="txtSttViengTham" runat="server" TextMode="Number" Visible="False">1</asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>





    </table>


    

          </telerik:RadAjaxPanel>

<div>
    
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
    <script type="text/javascript">
       
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("Button") >= 0) {
                args.set_enableAjax(false);
            }
        }

        function OnClientFocusHandler(sender, eventArgs) {
            if (!sender.get_dropDownVisible()) {
                sender.showDropDown();
            }
        }

    </script>
</telerik:RadCodeBlock>
    
    </div>
</asp:Content>
