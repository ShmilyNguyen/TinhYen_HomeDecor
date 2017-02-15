<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="sys-closingdays-extra.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Sys.sys_closingdays_extra" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table>
        <tr>
            <td nowrap="nowrap">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>

                <div>

                    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                        <script type="text/javascript">
                            function onRequestStart(sender, args) {
                                if (args.get_eventTarget().indexOf("Button") >= 0) {
                                    args.set_enableAjax(false);
                                }
                            }
                        </script>
                    </telerik:RadCodeBlock>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


                        <div>
                            <table>
                                <tr>
                                    <td style="margin-left: 20px">Chọn NPP   </td>
                                    <td style="margin-left: 20px">Xuất kho trước hiện tại</td>
                                    <td style="margin-left: 20px">Xuất kho sau hiện tại</td>
                                    <td style="margin-left: 20px">Trả hàng trước hiện tại</td>
                                    <td style="margin-left: 20px">Trả hàng sau hiện tại</td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>

                                </tr>

                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cbxStore" runat="server" DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="200px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_Saleout_before" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txt_Saleut_after" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txt_returnDate_before" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txt_returnDate_after" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="btnAdd1" runat="server" Text="Thêm" OnClick="btnAdd1_Click" />
                                    </td>
                                </tr>


                                <tr>
                                    <td>&nbsp;</td>
                                    <!--    <td>
                                        <asp:DropDownList ID="ddlstore" runat="server" Width="200px">
                                            <asp:ListItem></asp:ListItem>


                                        </asp:DropDownList>
                                    </td>-->
                                    <td>&nbsp;</td>

                                </tr>
                            </table>
                        </div>

                        <div>
                            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                                OnDeleteCommand="RadGrid1_DeleteCommand"
                                AllowPaging="True" AllowSorting="True"
                                RenderMode="Auto" GroupPanelPosition="Top"
                                Height="400px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
                                <GroupingSettings ShowUnGroupButton="true" />
                                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                                <MasterTableView
                                    AllowFilteringByColumn="true" TableLayout="Fixed"
                                    DataKeyNames="store_id" CommandItemDisplay="Top"
                                    InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False"
                                        ShowRefreshButton="False" />
                                    
                                    
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="store_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="r">
                                        </telerik:GridBoundColumn>
                                          
                                        <telerik:GridBoundColumn HeaderText="NPP"
                                            UniqueName="employee_name0" FilterControlAltText="Filter item_code column"
                                            DataField="store_name">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn HeaderText="Xuất kho trước hiện tại"
                                            UniqueName="employee_name1" FilterControlAltText="Filter item_code column"
                                            DataField="saleout_date_before">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Xuất kho sau hiện tại"
                                            UniqueName="employee_name2" FilterControlAltText="Filter item_code column"
                                            DataField="saleout_date_after">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Trả hàng trước hiện tại"
                                            UniqueName="employee_name3" FilterControlAltText="Filter item_code column"
                                            DataField="return_date_before">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Trả hàng sau hiện tại"
                                            UniqueName="employee_name4" FilterControlAltText="Filter item_code column"
                                            DataField="return_date_after">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Xóa">
                                            <HeaderStyle Width="70px" />
                                        </telerik:GridButtonColumn>
                                    </Columns>

                                    <EditFormSettings UserControlName="promo-row-edit-tanghang.ascx" EditFormType="WebUserControl">
                                        <EditColumn UniqueName="EditCommandColumn2">
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

                        </div>
                    </telerik:RadAjaxPanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <div>
                    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                        <script type="text/javascript">

                            function onRequestStart(sender, args) {
                                if (args.get_eventTarget().indexOf("Button") >= 0) {
                                    args.set_enableAjax(false);
                                }
                            }
                        </script>
                    </telerik:RadCodeBlock>
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
