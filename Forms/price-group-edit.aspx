<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="price-group-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.price_group_edit" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div style="width: 100%; background-color: white">
        &nbsp;
    <table>
        <tr>
            <td>ID</td>
            <td>

                <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="#FFCC66"
                    Enabled="False"></asp:TextBox></td>
            <td rowspan="6" valign="top">

                <telerik:RadListBox RenderMode="Lightweight" ID="lcbCustomerType" runat="server" CheckBoxes="True" ShowCheckAll="True" Width="400px"
                    Height="150px" DataTextField="channel_name" DataValueField="customer_channel_id">
                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                </telerik:RadListBox>
            </td>
        </tr>

        <tr>
            <td>Mã nhóm giá</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" Width="400px"></asp:TextBox></td>
        </tr>

        <tr>
            <td>Tên nhóm giá</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Ghi chú</td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Từ ngày</td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="400px" TextMode="Number"></asp:TextBox></td>
        </tr>

        <tr>
            <td>Đến ngày</td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" Width="400px" TextMode="Number"></asp:TextBox></td>
        </tr>

        <tr>
            <td>Hoạt động</td>
            <td>
                <asp:CheckBox ID="chckActive" runat="server" />
            </td>
            <td></td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>

                <asp:Button ID="btnSave" runat="server" Text="Lưu"
                    Width="100px" OnClick="btnSave_Click" />
                &nbsp;&nbsp;
            <asp:Button ID="btnExit" runat="server" Text="Thoát"
                Width="100px" OnClick="btnExit_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Xóa"
                Width="100px" OnClick="btnDelete_Click" OnClientClick="return confirm('Vui lòng xác nhận việc xóa dữ liệu  này?');" />

                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>

        <div>

            <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">

                <div>

                    <table>
                        <tr>
                            <td>Từ khóa tìm kiếm : </td>
                            <td>
                                <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
                            <td></td>
                        </tr>
                    </table>
                </div>

                <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                    OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                    OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                    AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" Height="800px" Skin="Office2010Blue">
                    <GroupingSettings ShowUnGroupButton="true" />
                    <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="true" TableLayout="Fixed"
                        DataKeyNames="item_id" CommandItemDisplay="Top"
                        InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20" ShowGroupFooter="True">
                        <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="item_price_id" FilterControlAltText="Filter item_price_id column" HeaderText="item price id" UniqueName="item_price_id" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Mã hàng hóa"
                                UniqueName="item_id" FilterControlAltText="Filter item_id column" DataField="item_id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Code hàng hóa" UniqueName="item_code" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column" HeaderText="Tên hàng hóa" SortExpression="Name" UniqueName="item_name">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="sellin_price" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter category2 column" HeaderText="Đơn giá nhập" UniqueName="sellin_price">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter category3 column" DataFormatString="{0:###,###.##}" HeaderText="Đơn giá xuất" UniqueName="saleout_price" DataField="saleout_price">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Display="false" DataField="support_price" FilterControlAltText="Filter support_price column" HeaderText="Giá hỗ trợ" UniqueName="support_price">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn Display="false" DataField="price1" FilterControlAltText="Filter price1 column" HeaderText="Giá bán 1" UniqueName="price1">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn Display="false" DataField="price2" FilterControlAltText="Filter price2 column" HeaderText="Giá bán 2" UniqueName="price2">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn Display="false" DataField="price3" FilterControlAltText="Filter price3 column" HeaderText="Giá bán 3" UniqueName="price3">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="giamua_truocvat" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter giamua_truocvat column" HeaderText="Giá mua trước VAT" UniqueName="giamua_truocvat">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="giamua_vat" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter giamua_vat column" HeaderText="VAT(%)" UniqueName="giamua_vat">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="giamua_sauvat" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter giamua_sauvat column" HeaderText="Giá mua sau VAT" UniqueName="giamua_sauvat">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="giaban_truocvat" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter giaban_truocvat column" HeaderText="Giá ban trước VAT" UniqueName="giaban_truocvat">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="giaban_vat" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter giaban_vat column" HeaderText="VAT(%)" UniqueName="giaban_vat">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="giaban_sauvat" DataFormatString="{0:###,###.##}" FilterControlAltText="Filter giaban_sauvat column" HeaderText="Giá ban sau VAT" UniqueName="giaban_sauvat">
                            </telerik:GridBoundColumn>

                            <telerik:GridEditCommandColumn UniqueName="EditColumn" HeaderText="Edit Command Column">
                                <HeaderStyle Width="70px" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete Command Column" Visible="False">
                                <HeaderStyle Width="70px" />
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings UserControlName="price-edit.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true" AllowDragToGroup="true">
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
    </div>
</asp:Content>