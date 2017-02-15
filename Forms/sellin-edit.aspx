<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="sellin-edit.aspx.cs" Inherits="WKS.DMS.WEB.Forms.sellin_edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />

     
      <style>
    





        /** Customize the demo canvas */
 
.demo-container label {
    padding-right: 10px;
    width: 185px;
    display: inline-block;
}
 
.demo-container .RadButton {
    margin-top: 20px;
}
 
 
/** Columns */
.rcbHeader ul,
.rcbFooter ul,
.rcbItem ul,
.rcbHovered ul,
.rcbDisabled ul {
    margin: 0;
    padding: 0;
    width: 100%;
    display: inline-block;
    list-style-type: none;
}
 
.exampleRadComboBox.RadComboBoxDropDown .rcbHeader {
    padding: 5px 27px 4px 7px;
}
 
.rcbScroll {
    overflow: scroll !important;
    overflow-x: hidden !important;
}
 
.col1{
    margin: 0;
    padding: 0 5px 0 0;
    width: 100px;
    line-height: 14px;
    float: left;
}
.col2{
    margin: 0;
    padding: 0 5px 0 0;
    width: 70px;
    line-height: 14px;
    float: left;
}
.col3 {
    margin: 0;
    padding: 0 5px 0 0;
    width: 600px;
    line-height: 14px;
    float: left;
}
 



.colKM1{
    margin: 0;
    padding: 0 5px 0 0;
    width: 200px;
    line-height: 14px;
    float: left;
}
.colKM2{
    margin: 0;
    padding: 0 5px 0 0;
    width: 200px;
    line-height: 14px;
    float: left;
}
.colKM3 {
    margin: 0;
    padding: 0 5px 0 0;
    width: 400px;
    line-height: 14px;
    float: left;
}
 


 
/** Multiple rows and columns */
.multipleRowsColumns .rcbItem,
.multipleRowsColumns .rcbHovered {
    float: left;
    margin: 0 1px;
    min-height: 13px;
    overflow: hidden;
    padding: 2px 19px 2px 6px;
    width: 195px;
}
 
 
.results {
    display: block;
    margin-top: 20px;
}
          .auto-style1 {
              width: 181px;
          }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">



    <div>
    <table>
        <tr>
            <td style="color:#fff">Mã NK</td>
            <td >
                <asp:TextBox ID="txtsellin_code" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td   style="color:#fff" >
                Số CT</td>
            <td >
                <asp:TextBox ID="txtSoChungTu" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td   style="color:#fff" >Ngày giao dịch</td>
            <td class="auto-style1" >
                <telerik:RadDatePicker ID="rdpNgayGiaoDich" Runat="server" OnSelectedDateChanged="rdpNgayGiaoDich_SelectedDateChanged">
                </telerik:RadDatePicker>
            </td>
            <td   style="color:#fff" >&nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color:#fff" nowrap>Nhà phân phối</td>
            <td>
                    <telerik:RadComboBox ID="rddlNhaPhanPhoi" runat="server" DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled" Filter="Contains" OnClientFocus="OnClientFocusHandler" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="rddlNhaPhanPhoi_SelectedIndexChanged">
                    </telerik:RadComboBox>
            </td>
            <td style="color:#fff" nowrap>
                Lần cập nhật</td>
            <td>
                <asp:TextBox ID="txtLastModified" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td style="color:#fff" nowrap>Người lập</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtNguoiLap" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

         <tr>
            <td  style="color:#fff" nowrap>Ghi chú</td>
            <td colspan="7">
                <asp:TextBox ID="txtGhiChu" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td  style="color:#fff" nowrap>&nbsp;</td>
            <td>
                        <telerik:RadComboBox ID="cbxBoGia" runat="server" AutoPostBack="True" DataTextField="price_group_code" DataValueField="price_group_id" DropDownAutoWidth="Enabled" Filter="Contains" OnClientFocus="OnClientFocusHandler" OpenDropDownOnFocus="True" Width="300px" OnSelectedIndexChanged="cbxBoGia_SelectedIndexChanged" Visible="False">
                        </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="auto-style1">
                <asp:HiddenField ID="hdf_Sellin_ID" runat="server" />
                <asp:HiddenField ID="hdf_User_ID" runat="server" />
            </td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        </div>
    
    <div>
         <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
             <div>
        <table>     
            <tr>
                <td  style="color:#fff" >Loại nhập</td>
                <td  style="color:#fff" >Hàng hóa</td>
                <td  style="color:#fff" >Số lượng</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cbxLoaiNhap" runat="server" DropDownAutoWidth="Enabled" Filter="Contains" OnClientFocus="OnClientFocusHandler" OpenDropDownOnFocus="True" Width="100px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Selected="True" Text="Hàng bán" Value="HB" />
                            <telerik:RadComboBoxItem runat="server" Text="Hàng KM" Value="KM" />
                        </Items>
                    </telerik:RadComboBox>

                </td>
                <td>
                    <telerik:RadComboBox ID="cbxHangHoa" runat="server" OnClientFocus="OnClientFocusHandler"
                                DataTextField="item_name" DataValueField="item_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="400px" OpenDropDownOnFocus="True"  EnableLoadOnDemand="true"
            HighlightTemplatedItems="true"  DropDownCssClass="exampleRadComboBox" AutoPostBack="True"  OnItemDataBound="cbxHangHoa_ItemDataBound">


                                <HeaderTemplate>
                                            <ul>
                                                <li class="col1">Mã hàng hóa</li>
                                               
                                                <li class="col3">Tên hàng hóa</li>
                                                <li class="col2">Đơn giá</li>
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <ul>
                                                <li class="col1">
                                                    <%# DataBinder.Eval(Container.DataItem, "item_code") %></li>
                                                
                                             
                                                <li class="col3">
                                                    <%# DataBinder.Eval(Container.DataItem, "item_name") %></li>
                                                <li class="col2">
                                                    <%# DataBinder.Eval(Container.DataItem, "item_price") %></li>
                                            </ul>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            A total of
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            items
                                        </FooterTemplate>


                            </telerik:RadComboBox>
                </td>
                <td>
                <asp:TextBox ID="txtQty" runat="server" Width="50px" TextMode="Number">0</asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddRow" runat="server" Text="Thêm" OnClick="btnAddRow_Click" />
                </td>
            </tr>
        </table>
        </div>

        <telerik:RadGrid ID="RadGrid1" runat="server" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource" 
            OnItemCreated="RadGrid1_ItemCreated" 
            OnItemDataBound="RadGrid1_ItemDataBound"
            OnUpdateCommand="RadGrid1_UpdateCommand" 
            OnInsertCommand="RadGrid1_InsertCommand" 
            OnDeleteCommand="RadGrid1_DeleteCommand"
            OnItemCommand="RadGrid1_ItemCommand"
            AllowPaging="True" AllowSorting="True" RenderMode="Auto" GroupPanelPosition="Top"  Height="400px" Skin="Office2010Blue">
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed"
                DataKeyNames="row_id" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage" ShowFooter="True" AllowNaturalSort="False" AllowSorting="False">
                <CommandItemSettings AddNewRecordText="Thêm mới" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="rowid"
                        UniqueName="row_id" FilterControlAltText="Filter item_id column" DataField="row_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sellin_type" FilterControlAltText="Filter sellin_type column" HeaderText="Loại nhập" UniqueName="sellin_type">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_id" FilterControlAltText="Filter item_code column" HeaderText="Item ID" UniqueName="item_id" Display="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_code" FilterControlAltText="Filter item_code column" HeaderText="Code" UniqueName="item_code">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="item_name" FilterControlAltText="Filter item_name column" UniqueName="name" HeaderText="Name" SortExpression="Name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="qty" FilterControlAltText="Filter category_id1 column" UniqueName="qty" HeaderText="SL" Aggregate="Sum" DataFormatString="{0:###,###}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total" FilterControlAltText="Filter category_id2 column" UniqueName="total" HeaderText="Total" Aggregate="Sum" DataFormatString="{0:###,###}" >
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn HeaderText="Edit Command Column" UniqueName="EditColumn">
                        <HeaderStyle Width="70px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete Command Column">
                        <HeaderStyle Width="70px" />
                    </telerik:GridButtonColumn>
                </Columns>

                 <EditFormSettings UserControlName="SellIn-Row-Edit.ascx" EditFormType="WebUserControl">
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
    <div>
        <asp:Button ID="btnDeleteOrder" runat="server" Text="Hủy đơn hàng"  OnClientClick="return confirm('Vui lòng xác nhận việc xóa đơn hàng ?');" OnClick="btnDeleteOrder_Click"/>
        &nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="Lưu đơn hàng" OnClick="btnSave_Click" />
        
        &nbsp;&nbsp;
        <input type="button" value="In đơn hàng" onclick="printFrame();" style="width:120px" />&nbsp;&nbsp;&nbsp;
       
        <asp:Button ID="btnNext" runat="server" Text="Đơn kế tiếp" OnClick="btnNext_Click"  />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExit" runat="server" Text="Thoát" OnClick="btnExit_Click" />
    </div>

     <%--Vung in don hang--%>
    <div style="display:none">
        <iframe style="width: 100%; height: 100%;" id="Iframe1" name="Iframe1" src= "#"
                    runat="server"></iframe>


    

    </div>

  <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>

 <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("Button") >= 0) {
                    args.set_enableAjax(false);
                }
            }

            function OnClientFocusHandler(sender, eventArgs) {
                if (!sender.get_dropDownVisible()) {
                    sender.showDropDown();
                }
            }

            //id is the  id of the iframe
            function printFrame() {
                id = '<%= Iframe1.ClientID %>';
                var frm = document.getElementById(id).contentWindow;
                frm.focus();// focus on contentWindow is needed on some ie versions
                frm.print();
                return false;
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
