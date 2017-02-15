<%@ Page Language="C#" Title="" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="user_region.aspx.cs" Inherits="WKS.DMS.WEB.Forms.user_region" %>


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
                                    <td></td>
                                    <td> Danh sách các HR  sẽ quản lý</td>
                                    <td> Danh sách các vùng </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxHr" runat="server" DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="200px"
                                            OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxHr_SelectedIndexChanged" >
                                        </telerik:RadComboBox>
                                    </td>

                                    <td>
                                        <telerik:RadComboBox ID="cbxHr_management" runat="server" DataTextField="region_name" DataValueField="region_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="200px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;</td>
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
                                RenderMode="Auto" GroupPanelPosition="Top"
                                Height="400px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
                                <GroupingSettings ShowUnGroupButton="true" />
                                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                                <MasterTableView
                                    AllowFilteringByColumn="true" TableLayout="Fixed"
                                    DataKeyNames="employee_id" CommandItemDisplay="Top"
                                    InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False"
                                        ShowRefreshButton="False" />
                                    <Columns>
                                       
                                        <telerik:GridBoundColumn HeaderText="HR quản lý vùng"
                                            UniqueName="HR_vung" FilterControlAltText="Filter region_name column"
                                            DataField="employee_name">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Vùng HR sẽ quản lý"
                                            UniqueName="HR_se_quan_ly" FilterControlAltText="Filter region_name column"
                                            DataField="region_name">
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
