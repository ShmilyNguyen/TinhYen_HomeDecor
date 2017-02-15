<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="SellIn-List.aspx.cs" Inherits="WKS.DMS.WEB.Forms.SellIn_List" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
    </div>
    <div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


        <table>
                <tr>
                    <td  style="color:#fff" >Năm
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNam" runat="server">
                            <asp:ListItem>2015</asp:ListItem>
                             <asp:ListItem>2016</asp:ListItem>
                             <asp:ListItem>2017</asp:ListItem>
                             <asp:ListItem>2018</asp:ListItem>
                             <asp:ListItem>2019</asp:ListItem>
                             <asp:ListItem>2020</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td  style="color:#fff" >Tháng
                    </td>
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
                    <td  style="color:#fff" >Từ khóa tìm kiếm : </td>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
                    <td></td>
                    <td>
                         <asp:Button runat="server" id="btnCreatOrder" Text="Tạo phiếu nhập kho" PostBackUrl="~/Forms/sellin-edit.aspx" />
                </td>
                </tr>
            </table>



        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
           OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" 
            RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand" 
            Height="800px" ResolvedRenderMode="Classic" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed"
                DataKeyNames="sellin_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Stt"
                        UniqueName="Stt" FilterControlAltText="Filter item_id column" 
                        DataField="stt" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="trans_date_gmt" FilterControlAltText="Filter item_code column" HeaderText="Ngày" UniqueName="Ngay">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sellin_id" 
                        FilterControlAltText="Filter sellin_id column" HeaderText="id" 
                        UniqueName="sellin_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sellin_code" FilterControlAltText="Filter sellin_code column" HeaderText="Code" UniqueName="sellin_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sellin_code" FilterControlAltText="Filter area_id column" HeaderText="Số CT" UniqueName="MaDonHang" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter item_name column" UniqueName="NhaPhanPhoi" HeaderText="NPP" SortExpression="Name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="note" FilterControlAltText="Filter category_id1 column" UniqueName="note" HeaderText="Ghi chú">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total" FilterControlAltText="Filter category_id2 column" UniqueName="ThanhTien" HeaderText="Thành tiền" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="note" Display="False" FilterControlAltText="Filter province_id column" HeaderText="Ghi chú" UniqueName="GhiChu">
                    </telerik:GridBoundColumn>
                    
                    <telerik:GridButtonColumn CommandName="Update" Text="Xem" UniqueName="UpdateColumn" HeaderText="Cập nhật">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

                 <GroupByExpressions>
                     <telerik:GridGroupByExpression>
                         <SelectFields>
                             <telerik:GridGroupByField FieldAlias="trans_date_gmt" FieldName="trans_date_gmt" FormatString="" HeaderText="Ngày giao dịch" />
                         </SelectFields>
                         <GroupByFields>
                             <telerik:GridGroupByField FieldAlias="trans_date_gmt" FieldName="trans_date_gmt" SortOrder="Descending" />
                         </GroupByFields>
                     </telerik:GridGroupByExpression>
                </GroupByExpressions>

                 <EditFormSettings UserControlName="Store-Edit.ascx" EditFormType="WebUserControl">
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
</asp:Content>
