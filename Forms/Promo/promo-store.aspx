<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-store.aspx.cs" Inherits="WKS.DMS.WEB.Forms.promo_store" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table>
        <tr>
            <td nowrap="nowrap">ID</td>
            <td>
                <asp:TextBox ID="txtID" runat="server" Width="400px" BackColor="#FFCC66"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">Code</td>
            <td>
                <asp:TextBox ID="txtCode" runat="server" Width="400px" BackColor="#FFCC66" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">Tên CTKM</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="400px" BackColor="#FFCC66"
                    TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td nowrap="nowrap">&nbsp;</td>
            <td>
                <em>Phân bổ CTKM cho nhà phân phối </em></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
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
                                    <td></td>
                                    <td>Nhà phân phối</td>
                                    <td>Nhóm khách hàng áp dụng</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBox1" runat="server" DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains"  Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxChannel" runat="server" DataTextField="channel_name" DataValueField="customer_channel_id" DropDownAutoWidth="Enabled" Filter="Contains"  OpenDropDownOnFocus="True" Width="200px">
                                        </telerik:RadComboBox>
                                    </td>
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
                            Height="500px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue" OnNeedDataSource="RadGrid1_NeedDataSource" PageSize="50">
                            <GroupingSettings ShowUnGroupButton="true" CaseSensitive="False" />
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
                                    <telerik:GridBoundColumn HeaderText="Region"
                                        UniqueName="region_name" FilterControlAltText="Filter group_code column"
                                        DataField="region_name">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="area_name"
                                        FilterControlAltText="Filter item_code column" HeaderText="Area"
                                        UniqueName="area_name" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="store_id"
                                        FilterControlAltText="Filter store_id column" UniqueName="store_id"
                                        HeaderText="Store ID" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="store_name"
                                        FilterControlAltText="Filter item_code column" HeaderText="Nhà phân phối" UniqueName="store_name">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="channel_name" FilterControlAltText="Filter channel_name column" HeaderText="Channel" UniqueName="channel_name">
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
            <td></td>
            <td>
                <asp:Button ID="btnExit" runat="server" Text="Quay về"
                    Width="100px" OnClick="btnExit_Click" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
