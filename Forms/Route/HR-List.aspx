<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="HR-List.aspx.cs" Inherits="WKS.DMS.WEB.Forms.HR_List" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">



        <table>
            <tr>
                <td style="color:#fff">Từ khóa tìm kiếm : </td>
                <td>
                    <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>


        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource"
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand"
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            OnItemCommand="RadGrid1_ItemCommand"
            AllowPaging="True" AllowSorting="True" ShowGroupPanel="True"
            RenderMode="Auto" GroupPanelPosition="Top"
            Height="800px" ResolvedRenderMode="Classic" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed"
                DataKeyNames="employee_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID"
                        UniqueName="id" FilterControlAltText="Filter item_id column" DataField="employee_id" Display="false">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="store_name" FilterControlAltText="Filter province column" UniqueName="store_name" HeaderText="NPP">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="employee_code" FilterControlAltText="Filter item_code column" HeaderText="Code" UniqueName="code">
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="hr_code" FilterControlAltText="Filter hr_code column" HeaderText="HR Code" UniqueName="HRCode">
                    </telerik:GridBoundColumn>


                    <telerik:GridBoundColumn DataField="employee_name" FilterControlAltText="Filter item_name column" HeaderText="Name" SortExpression="Name" UniqueName="name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="position" FilterControlAltText="Filter category_id1 column" UniqueName="position" HeaderText="Chức vụ">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="role" FilterControlAltText="Filter category_id2 column" UniqueName="role" HeaderText="Nhóm quyền">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QuanLyCapTren" FilterControlAltText="Filter quanlycaptren column" HeaderText="Cấp trên" UniqueName="quanlycaptren">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="store_id" FilterControlAltText="Filter store_id column" UniqueName="store_id" Display="false">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="birthday" FilterControlAltText="Filter birthday column" UniqueName="birthday">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="personal_id" FilterControlAltText="Filter personal_id column" HeaderText="CMND" UniqueName="personal_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="phone" FilterControlAltText="Filter phone column" HeaderText="Phone" UniqueName="phone">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="email" FilterControlAltText="Filter email column" HeaderText="Email" UniqueName="email">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="group_name" FilterControlAltText="Filter group_name column" HeaderText="Group" UniqueName="group_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="group_id" FilterControlAltText="Filter group_name column" HeaderText="Group" UniqueName="group_id" Display="false" >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="parent_id"
                        FilterControlAltText="Filter parent_id column" UniqueName="parent_id"
                        Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="username"
                        FilterControlAltText="Filter username column" UniqueName="username" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="password"
                        FilterControlAltText="Filter password column" UniqueName="password" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="active"  Display="false" 
                        FilterControlAltText="Filter active column" HeaderText="Active"
                        UniqueName="active">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="active" DataType="System.Boolean" FilterControlAltText="Filter chckActive column" HeaderText="Hoạt động" UniqueName="chckActive">
                    </telerik:GridCheckBoxColumn>


                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                </Columns>

                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" FormatString="" HeaderText="NPP" />
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldAlias="store_name" FieldName="store_name" SortOrder="Descending" />
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>

                <EditFormSettings UserControlName="Employee-Edit.ascx" EditFormType="WebUserControl">
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