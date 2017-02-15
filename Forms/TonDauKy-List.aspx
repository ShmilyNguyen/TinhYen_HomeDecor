<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="TonDauKy-List.aspx.cs" Inherits="WKS.DMS.WEB.Forms.TonDauKy_List" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Nhà phân phối</td>
            <td>
                    <telerik:RadComboBox ID="cbxNhaPhanPhoi" runat="server" DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="400px" OpenDropDownOnFocus="True"></telerik:RadComboBox>

            </td>
        </tr>
        <tr>
            <td>Tháng</td>
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
                    </asp:DropDownList>

                </td>
        </tr>
        <tr>
            <td>Năm</td>
            <td>
                    <asp:DropDownList ID="ddlNam" runat="server">
                        <asp:ListItem >2015</asp:ListItem>
                         <asp:ListItem >2016</asp:ListItem>
                         <asp:ListItem >2017</asp:ListItem>
                         <asp:ListItem >2018</asp:ListItem>
                         <asp:ListItem >2019</asp:ListItem>
                         <asp:ListItem >2020</asp:ListItem>
                    </asp:DropDownList>

                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnLoadData" runat="server" Text="Xem tồn đầu" Width="100px" OnClick="btnLoadData_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
        


        


        
      

<telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" 
            OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand" 
            OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" Height="500px" AutoGenerateColumns="False" Skin="Office2010Blue" PageSize="50">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView
                AllowFilteringByColumn="true" TableLayout="Fixed"
                DataKeyNames="item_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" ShowFooter="True">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn DataField="row_id" FilterControlAltText="Filter row_id column" HeaderText="row_id" UniqueName="row_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="store_id"
                        UniqueName="store_id" FilterControlAltText="Filter item_id column" DataField="store_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter item_code column" HeaderText="Nhà phân phối" UniqueName="store_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Mã hàng" UniqueName="item_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_id" Display="False" FilterControlAltText="Filter item_name column" HeaderText="item_id" SortExpression="Name" UniqueName="item_id">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter category_id1 column" UniqueName="item_name" HeaderText="Hàng hóa">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter qty_saleout column" HeaderText="Tồn đầu kỳ hàng bán" UniqueName="qty_saleout" DataField="qty_saleout" Aggregate="Sum"  DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter qty_promo column" HeaderText="Tồn đầu kỳ KM" UniqueName="qty_promo" DataField="qty_promo" Aggregate="Sum"  DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total_saleout" FilterControlAltText="Filter total_saleout column" HeaderText="GT Tồn đầu HB" UniqueName="total_saleout" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total_promo" FilterControlAltText="Filter total_promo column" HeaderText="GT Tồn đầu KM" UniqueName="total_promo" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="qty_total" FilterControlAltText="Filter qty_total column" HeaderText="Tổng SL" UniqueName="qty_total" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="value_total" FilterControlAltText="Filter value_total column" HeaderText="Tổng giá trị" UniqueName="value_total" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn FilterControlAltText="Filter note column" HeaderText="Lý do" UniqueName="note" DataField="note">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter adjusted_date column" HeaderText="Ngày điều chỉnh" UniqueName="adjusted_date" DataField="adjusted_date">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="adjust_type" FilterControlAltText="Filter adjust_type column" HeaderText="Loại điều chỉnh" UniqueName="adjust_type">
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn HeaderText="Cập nhật" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete Command Column" Text="Delete" UniqueName="DeleteColumn" Visible="False">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

                 <EditFormSettings UserControlName="TonDauKy-Edit.ascx" EditFormType="WebUserControl">
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

</asp:Content>
