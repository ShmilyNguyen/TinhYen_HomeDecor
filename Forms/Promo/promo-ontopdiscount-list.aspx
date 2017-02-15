<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="promo-ontopdiscount-list.aspx.cs" Inherits="WKS.DMS.WEB.Forms.promo_ontopdiscount_list" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../../styles/grid.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div>
    </div>

    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">



            <table>
                <tr>
                    <td>Kênh khách hàng</td>
                    <td>Giá trị đạt KM</td>

                    <td>Chiết khấu</td>
                    <td>Sau VAT / Trước VAT</td>

                    <td>CK Ontop / CK theo dòng</td>

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="RadComboBox1" runat="server" DataTextField="channel_name" DataValueField="customer_channel_id" DropDownAutoWidth="Enabled" Filter="Contains" Width="400px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVol" runat="server"></asp:TextBox>
                    </td>

                    <td>
                        <asp:TextBox ID="txtCK" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                          <asp:DropDownList ID="ddlLoaiGiaThanh" runat="server" >
                <asp:ListItem Value="1">Giá sau VAT</asp:ListItem>
                <asp:ListItem Value="0">Giá trước VAT</asp:ListItem>
                    </asp:DropDownList>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLoaiCK" runat="server" >
            <asp:ListItem Value="1">CK Ontop</asp:ListItem>
            <asp:ListItem Value="0">CK dòng hàng</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd1" runat="server" Text="Thêm" OnClick="btnAdd1_Click" />
                    </td>
                </tr>
            </table>

            <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
                OnDeleteCommand="RadGrid1_DeleteCommand"


                  OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand"
           
           OnItemCommand="RadGrid1_ItemCommand" 

                AllowPaging="True" AllowSorting="True"
                RenderMode="Auto" GroupPanelPosition="Top"
                Height="800px" ResolvedRenderMode="Classic" AutoGenerateColumns="False" Skin="Office2010Blue">
                <GroupingSettings ShowUnGroupButton="true" />
                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                <MasterTableView
                    AllowFilteringByColumn="true" TableLayout="Fixed"
                    DataKeyNames="row_id" CommandItemDisplay="Top"
                    InsertItemPageIndexAction="ShowItemOnFirstPage" PageSize="50">
                    <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="row_id" Display="False" FilterControlAltText="Filter row_id column" UniqueName="row_id">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Channel ID" Display="false"
                            UniqueName="channel_id" FilterControlAltText="Filter item_id column" DataField="channel_id">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="channel_name" FilterControlAltText="Filter item_name column" HeaderText="Channel Name" UniqueName="channel_name" SortExpression="Name">
                            <HeaderStyle Width="150px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="target_vol" FilterControlAltText="Filter target_vol column" HeaderText="Giá trị đạt KM" UniqueName="target_vol">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ontopdiscount" FilterControlAltText="Filter location column" HeaderText="Chiết khấu" UniqueName="ontopdiscount">
                        </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn DataField="isVAT" FilterControlAltText="Filter isVAT column" HeaderText="isVAT" UniqueName="isVAT" Display="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="isOntop" FilterControlAltText="Filter isOntop column" HeaderText="isOntop" UniqueName="isOntop" Display="false">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="LoaiGia" FilterControlAltText="Filter LoaiGia column" HeaderText="Sau VAT / Trước VAT" UniqueName="LoaiGia">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="LoaiCKOntop" FilterControlAltText="Filter LoaiCKOntop column" HeaderText="CK Ontop / CK theo dòng" UniqueName="LoaiCKOntop">
                        </telerik:GridBoundColumn>


                       

                            <telerik:GridEditCommandColumn HeaderText="Edit" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>

                    </Columns>

                      <EditFormSettings UserControlName="promo-ontopdiscount-edit.ascx" EditFormType="WebUserControl">
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

    </div>

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
