<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="route-sub-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Route.route_sub_edit" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">&nbsp;<table style="width:100%">
    <tr>
        <td>&nbsp;</td>
        <td style="margin-left: 40px">
            &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td style="margin-left: 40px">
            <asp:TextBox ID="txtID" runat="server" Width="400px" Visible="False"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td>Nhà phân phối</td>
        <td style="margin-left: 40px">
                     <telerik:radcombobox ID="cbxStore" runat="server"
                        OnClientFocus="OnClientFocusHandler" DataTextField="store_name"
                        DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains"
                        Width="400px" OpenDropDownOnFocus="True"
                       
                         xmlns:telerik="telerik.web.ui" autopostback="True" onselectedindexchanged="cbxStore_SelectedIndexChanged">
                      </telerik:radcombobox>
            </td>
    </tr>
    <tr>
        <td>Tuyến - Khu - vực</td>
        <td style="margin-left: 40px">
                     <telerik:radcombobox ID="cbxRoute" runat="server"
                        OnClientFocus="OnClientFocusHandler" DataTextField="route_code"
                        DataValueField="route_id" DropDownAutoWidth="Enabled" Filter="Contains"
                        Width="400px" OpenDropDownOnFocus="True"
                       
                         xmlns:telerik="telerik.web.ui">
                      </telerik:radcombobox>
            </td>
    </tr>
    <tr>
        <td>Mã tuyến - Thứ</td>
        <td>
                     <telerik:radcombobox ID="cbxMaTuyenThu" runat="server"
                        OnClientFocus="OnClientFocusHandler" DropDownAutoWidth="Enabled" Filter="Contains"
                        Width="200px" OpenDropDownOnFocus="True"
                       
                         xmlns:telerik="telerik.web.ui">
                         <Items>
                             <telerik:RadComboBoxItem runat="server" Text="T0002" Value="T0002" Selected="True"/>
                             <telerik:RadComboBoxItem runat="server" Text="T0003" Value="T0003" />
                             <telerik:RadComboBoxItem runat="server" Text="T0004" Value="T0004" />
                             <telerik:RadComboBoxItem runat="server" Text="T0005" Value="T0005" />
                             <telerik:RadComboBoxItem runat="server" Text="T0006" Value="T0006" />
                             <telerik:RadComboBoxItem runat="server" Text="T0007" Value="T0007" />
                             <telerik:RadComboBoxItem runat="server" Text="T0246" Value="T0246" />
                             <telerik:RadComboBoxItem runat="server" Text="T0357" Value="T0357" />
                         </Items>
                      </telerik:radcombobox>
            (Chữ hoa, không dấu). Ví dụ : T0002 - Thứ 2,T0003 - Thứ 3,T0004 - Thứ 4,...,T0246 - Thứ 2,4,6</td>
    </tr>
    <tr>
        <td>Tên tuyến - Thứ</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox>&nbsp;(Tuyến thứ 2, tuyến thứ 3,...)</td>
    </tr>

     <tr>
        <td>Nhân viên phụ trách</td>
        <td>
                     <telerik:radcombobox ID="cbxEmployee" runat="server"
                        OnClientFocus="OnClientFocusHandler" DataTextField="employee_name"
                        DataValueField="employee_id" DropDownAutoWidth="Enabled" Filter="Contains"
                        Width="400px" OpenDropDownOnFocus="True"
                        
                         xmlns:telerik="telerik.web.ui">
                      </telerik:radcombobox>
            </td>
    </tr>

    <tr>
        <td></td>
        <td>

            <asp:Button ID="btnSave" runat="server" Text="Lưu" CommandName="Update"
                Width="100px" onclick="btnSave_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnExit" runat="server" Text="Thoát" CommandName="Cancel"
                Width="100px" OnClick="btnExit_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hủy"
                Width="100px" OnClientClick="return confirm('Vui lòng xác nhận việc xóa Tuyến Bán Hàng ?');" OnClick="btnDelete_Click" />


        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<div>
    <strong>Danh sách khách hàng viếng thăm</strong></div>

    <div>
        <table>
            <tr>
                <td>Stt&nbsp; viếng thăm</td>
                <td>Khách hàng</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtVisitOrder" runat="server" TextMode="Number">1</asp:TextBox>
                </td>
                <td>
                        <telerik:RadComboBox ID="cbxKhachHang" OnClientFocus="OnClientFocusHandler" runat="server"
                            DataTextField="customer_name" DataValueField="customer_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="350px" OpenDropDownOnFocus="True">
                        </telerik:RadComboBox>
                        </td>
                <td>
            <asp:Button ID="btnAdd" runat="server" Text="Thêm" CommandName="Cancel"
                Width="100px" OnClick="btnAdd_Click" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

    <div>
        

          <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
           <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
           
            OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" 
            RenderMode="Auto" GroupPanelPosition="Top"
            Height="300px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="row_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                <Columns>
                    <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" UniqueName="row_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Stt viếng thăm"
                        UniqueName="visit_order" FilterControlAltText="Filter item_name column" DataField="visit_order" SortExpression="Name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="customer_code" FilterControlAltText="Filter customer_code column" HeaderText="Mã khách hàng" UniqueName="customer_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="customer_name" FilterControlAltText="Filter location column" HeaderText="Tên khách hàng" UniqueName="customer_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter address column" HeaderText="Địa chỉ" UniqueName="address" DataField="address">
                    </telerik:GridBoundColumn>
                
                     <telerik:GridBoundColumn FilterControlAltText="Filter phone column" HeaderText="Điện thoại" UniqueName="phone" DataField="phone">
                    </telerik:GridBoundColumn>
                
                     <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Xóa">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridButtonColumn>


                </Columns>

                 

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
    </telerik:RadAjaxPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("Button") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>

    </div>
    <div>

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
    <script type="text/javascript">

        function OnClientFocusHandler(sender, eventArgs) {
            if (!sender.get_dropDownVisible()) {
                sender.showDropDown();
            }
        }
    </script>
</telerik:RadCodeBlock>
    </div>
</asp:Content>