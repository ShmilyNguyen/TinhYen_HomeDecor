<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="HangHoa-List.aspx.cs" Inherits="WKS.DMS.WEB.Forms.HangHoa_List" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/grid.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


    <div>


        <table>
            <tr>
                <td>Từ khóa tìm kiếm : </td>
                <td>
                    <asp:TextBox ID="txtKeyword" runat="server" Width="300px"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click"  /></td>
                <td>
                         
                </td>
            </tr>
        </table>

    </div>


    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource"
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand"
            OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="True"
            RenderMode="Auto" GroupPanelPosition="Top" OnItemCommand="RadGrid1_ItemCommand"
            Height="800px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView TableLayout="Fixed"
                DataKeyNames="item_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="20">
                <CommandItemSettings ShowExportToCsvButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" AddNewRecordText="Thêm mới" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Mã hàng hóa"
                        UniqueName="item_id" FilterControlAltText="Filter item_id column" DataField="item_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Code hàng hóa" UniqueName="item_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column" HeaderText="Tên hàng hóa" SortExpression="Name" UniqueName="item_name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="unit_name" FilterControlAltText="Filter item_unit column" HeaderText="DVT" UniqueName="unit_name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter category1 column" HeaderText="Category 1" UniqueName="category_name1" DataField="category_name1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter category2 column" HeaderText="Category 2" UniqueName="category_name2" DataField="category_name2">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter category3 column" HeaderText="Category 3" UniqueName="category_name3" DataField="category_name3">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter category4 column" HeaderText="Category 4" UniqueName="category_name4" DataField="category_name4" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn FilterControlAltText="Filter category5 column" HeaderText="Category 5" UniqueName="category_name5" DataField="category_name5" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="specification_1"
                        FilterControlAltText="Filter specification_1 column" HeaderText="Quy cách 1"
                        UniqueName="specification_1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="specification_2"
                        FilterControlAltText="Filter specification_2 column" HeaderText="Quy cách 2"
                        UniqueName="specification_2">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="size" FilterControlAltText="Filter size column" HeaderText="Size" UniqueName="size">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="weight" FilterControlAltText="Filter weight column" HeaderText="Weight" UniqueName="weight">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="volume" FilterControlAltText="Filter volume column" HeaderText="Volume" UniqueName="volume">
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn UniqueName="EditColumn" HeaderText="Edit Command Column">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete Command Column">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="unit_id" Display="False" FilterControlAltText="Filter unit_id column" UniqueName="unit_id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="category_id1" Display="False" FilterControlAltText="Filter category_id1 column" UniqueName="category_id1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="category_id2" Display="False" FilterControlAltText="Filter category_id2 column" UniqueName="category_id2">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="category_id3" Display="False" FilterControlAltText="Filter category_id3 column" UniqueName="category_id3">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="category_id4" Display="False" FilterControlAltText="Filter category_id4 column" UniqueName="category_id4">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="category_id5" Display="False" FilterControlAltText="Filter category_id5 column" UniqueName="category_id5">
                    </telerik:GridBoundColumn>
                </Columns>

                <EditFormSettings UserControlName="HangHoa-Edit.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
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