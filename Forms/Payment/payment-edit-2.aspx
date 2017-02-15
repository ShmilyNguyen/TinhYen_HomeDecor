<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="payment-edit-2.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Payment.payment_edit_2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:HiddenField ID="hdf_ID" runat="server" />
        
        <asp:HiddenField ID="hdf_UserID" runat="server" />
        <asp:HiddenField ID="hdf_GTThanhToan" runat="server" />
        <br />


        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>


    </div>

    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" ClientEvents-OnRequestStart="onRequestStart"
            runat="server" CssClass="grid_wrapper">
            <table>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>NPP
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbxStore" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td nowrap>Khách hàng </td>
                    <td>
                        <telerik:RadComboBox ID="cbxKhachHang" runat="server" AutoPostBack="True" DataTextField="customer_name" DataValueField="customer_id" DropDownAutoWidth="Enabled" Filter="Contains" OnSelectedIndexChanged="cbxKhachHang_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="350px">
                        </telerik:RadComboBox>
                    </td>
                    <td nowrap>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Số phiếu thu</td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>Ngày</td>
                    <td>
                        <telerik:RadDatePicker ID="rdpNgay" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td nowrap>Người lập</td>
                    <td>
                        <asp:TextBox ID="txtNguoiLap" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td nowrap>Loại chứng từ</td>
                    <td>
                        <telerik:RadComboBox ID="cbxPaymentType" runat="server" AutoPostBack="True" DataTextField="payment_type_name" DataValueField="payment_type_id" DropDownAutoWidth="Enabled" Filter="Contains" OnSelectedIndexChanged="cbxPaymentType_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Số CT</td>
                    <td>
                        <telerik:RadComboBox ID="cbxSoCT" runat="server" AutoPostBack="True" DataTextField="posted_code" DataValueField="posted_id" DropDownAutoWidth="Enabled" Enabled="False" Filter="Contains" OnSelectedIndexChanged="cbxSoCT_SelectedIndexChanged" OpenDropDownOnFocus="True" Width="350px">
                        </telerik:RadComboBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td nowrap>Công nợ hiện tại</td>
                    <td>
                        <asp:TextBox ID="txtCongNoHienTai" runat="server" ForeColor="Red" Width="200px" Font-Bold="True" Enabled="False">0</asp:TextBox>
                    </td>
                    <td>
                        Giá trị</td>
                    <td>
                        <asp:TextBox ID="txtGiaTri" runat="server" Font-Bold="True" ForeColor="Red" Width="200px" Enabled="False">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdf_GiaTri" runat="server" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Ghi chú </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtGhiChu" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="5">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Lưu chứng từ vào hệ thống" Width="180px" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click" Text="Thoát" Width="100px" />
                        &nbsp;&nbsp;
                       
                    </td>
                </tr>
            </table>

          

            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5" ClientEvents-OnRequestStart="onRequestStart" 
                AllowPaging="True" AllowSorting="True"
                RenderMode="Auto" GroupPanelPosition="Top"
                Height="800px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue" 
                 OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"

            OnItemCommand="RadGrid1_ItemCommand" 

                
                >
                <GroupingSettings ShowUnGroupButton="true" />
                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                <MasterTableView
                    AllowFilteringByColumn="true" TableLayout="Fixed"
                    DataKeyNames="row_id" CommandItemDisplay="Top"
                    InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Row ID"
                            UniqueName="row_id" FilterControlAltText="Filter row_id column" DataField="row_id" Display="false">
                        </telerik:GridBoundColumn>


                        <telerik:GridBoundColumn HeaderText="Is custom Debit"
                            UniqueName="is_custom_debit" FilterControlAltText="Filter is_custom_debit column" DataField="is_custom_debit" Display="false">
                        </telerik:GridBoundColumn>


                         <telerik:GridBoundColumn HeaderText="saleout_id"
                            UniqueName="saleout_id" FilterControlAltText="Filter saleout_id column" DataField="saleout_id" Display="false">
                        </telerik:GridBoundColumn>




                         <telerik:GridBoundColumn HeaderText="doc_id"
                            UniqueName="doc_id" FilterControlAltText="Filter saleout_id column" DataField="doc_id">
                        </telerik:GridBoundColumn>



                        <telerik:GridBoundColumn FilterControlAltText="Filter saleout_code column" HeaderText="Mã đơn hàng" UniqueName="saleout_code" DataField="saleout_code">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn
                            FilterControlAltText="Filter trans_date_gmt column" HeaderText="Ngày"
                            UniqueName="trans_date_gmt" DataField="trans_date_gmt">
                        </telerik:GridBoundColumn>

                     

                        <telerik:GridBoundColumn FilterControlAltText="Filter begin_balance column" HeaderText="Số dư hiện tại" UniqueName="begin_balance" DataField="begin_balance" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn> 
                        <telerik:GridBoundColumn FilterControlAltText="Filter payment_amt column" HeaderText="Thanh toán" UniqueName="payment_amt" DataField="payment_amt" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FilterControlAltText="Filter ending_balance column" UniqueName="ending_balance" HeaderText="Còn lại" DataField="ending_balance" DataFormatString="{0:###,###}">
                        </telerik:GridBoundColumn>

                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Xóa">
                            <HeaderStyle Width="70px" />
                        </telerik:GridButtonColumn>

                         <telerik:GridEditCommandColumn HeaderText="Cập nhật" UniqueName="EditColumn">
                            <HeaderStyle Width="70px" />
                        </telerik:GridEditCommandColumn>


                    </Columns>
                    <EditFormSettings UserControlName="payment-row-edit.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <PagerStyle PageButtonCount="5" />
                <FilterMenu RenderMode="Auto">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Auto">
                </HeaderContextMenu>
            </telerik:RadGrid>

              <div>
       
        <telerik:RadCodeBlock runat="server">
            <script type="text/javascript">
                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("Button") >= 0) {
                        args.set_enableAjax(false);
                    }
                }
            </script>
        </telerik:RadCodeBlock>
    </div>


        </telerik:RadAjaxPanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
    </div>

  
</asp:Content>