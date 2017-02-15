<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="target-focus-item.aspx.cs" Inherits="WKS.DMS.WEB.Forms.target_focus_item" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <table>
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
                        <asp:ListItem Selected="True">9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
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
                        <asp:ListItem Selected="True">2016</asp:ListItem>
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
                <asp:Button ID="btnLoadData" runat="server" Text="Xem danh sách mặt hàng tập trung" Width="250px" 
                    OnClick="btnLoadData_Click" />
            </td>
        </tr>
    </table>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table>
        <tr>
            <td nowrap="nowrap">&nbsp;</td>
            <td>
                &nbsp;</td>
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
                                    <td></td>
                                    <td>Hàng hóa</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadComboBox ID="cbxItem" runat="server" DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled" Filter="Contains"  Width="400px">
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
                            RenderMode="Auto" GroupPanelPosition="Top" 
                            Height="200px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
                            <GroupingSettings ShowUnGroupButton="true" />
                            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                            <MasterTableView
                                AllowFilteringByColumn="true" TableLayout="Fixed"
                                DataKeyNames="row_id" CommandItemDisplay="Top"
                                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False"
                                    ShowRefreshButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" HeaderText="RowID" UniqueName="row_id">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Mã hàng hóa"
                                        UniqueName="item_code" FilterControlAltText="Filter group_code column"
                                        DataField="item_code">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="item_name"
                                        FilterControlAltText="Filter item_code column" HeaderText="Tên hàng hóa"
                                        UniqueName="item_name" >
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
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
