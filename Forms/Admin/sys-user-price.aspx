<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="sys-user-price.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Sys.sys_user_price" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table>
        <tr>
            <td>
                <em>Phân quyền xem dữ liệu cho User</em></td>
        </tr>
        <tr>
            <td>

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
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">


                        <div>
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>User</td>
                                    <td>Danh mục giá</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxEmployee" runat="server" DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled" Filter="Contains" OpenDropDownOnFocus="True" Width="200px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxPrice" runat="server" DataTextField="description" DataValueField="item_price_policy_id" DropDownAutoWidth="Enabled" Filter="Contains"  Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnAdd1" runat="server" Text="Thêm" OnClick="btnAdd1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                      <div>
                        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                            
                            
                            OnDeleteCommand="RadGrid1_DeleteCommand"
                           
                            AllowPaging="True" AllowSorting="True"
                            RenderMode="Auto" GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue" PageSize="200">
                            <GroupingSettings ShowUnGroupButton="true" />
                            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                            <MasterTableView
                                AllowFilteringByColumn="true" TableLayout="Fixed"
                                DataKeyNames="row_id" CommandItemDisplay="Top"
                                InsertItemPageIndexAction="ShowItemOnFirstPage">
                                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False"
                                    ShowRefreshButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="row_id">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="User Code"
                                        UniqueName="employee_code" FilterControlAltText="Filter group_code column"
                                        DataField="employee_code">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="employee_name"
                                        FilterControlAltText="Filter item_code column" HeaderText="User Name"
                                        UniqueName="employee_name" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="description"
                                        FilterControlAltText="Filter item_code column" UniqueName="description"
                                        HeaderText="Loại giá thành">
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
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
