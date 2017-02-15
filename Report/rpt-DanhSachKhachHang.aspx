<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-DanhSachKhachHang.aspx.cs" Inherits="WKS.DMS.WEB.rpt_DanhSachKhachHang" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid.Export" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="../../styles/grid.css" rel="stylesheet" />

     <style type="text/css">
div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <table>
        <tr>
            <td style="color:#fff" >Nhà phân phối</td>
            <td><telerik:RadComboBox ID="cbxStore" runat="server"
                            DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                            Filter="Contains" Width="200px" OpenDropDownOnFocus="True"  >
                        </telerik:RadComboBox></td>
            <td>
                 <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel_Click" />
            </td>
        </tr>
    </table>
    <div>
       
    </div>
    <div>

        <br />
        <br />

        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" GridViewID="grvData" PaperKind="A4">
    </dx:ASPxGridViewExporter>



        <dx:ASPxGridView Visible="false" ID="grvData" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" Width="100%" EnableTheming="True" Theme="Office2010Blue">
           
            <Columns>
                <dx:GridViewDataTextColumn Caption="Nhà phân phối" FieldName="store_name" VisibleIndex="0" Width="200px" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Nhân viên" FieldName="employee_name" VisibleIndex="1" Width="200px" GroupIndex="1" SortIndex="0" SortOrder="Ascending">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                 <dx:GridViewDataTextColumn Caption="Tuyến - Thứ" FieldName="route_code" VisibleIndex="2" Width="200px" GroupIndex="2" SortIndex="0" SortOrder="Ascending">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dx:GridViewDataTextColumn>


                <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="customer_code"  VisibleIndex="4" Width="250px">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Khách hàng" FieldName="customer_name"  VisibleIndex="5" Width="250px">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="address_full" VisibleIndex="6" UnboundType="Decimal">
                    
                   
                    
                </dx:GridViewDataTextColumn>

                
                <dx:GridViewDataCheckColumn Caption="Hoạt động" FieldName="active" UnboundType="Boolean" VisibleIndex="7">
                </dx:GridViewDataCheckColumn>

                
            </Columns>
            <SettingsPager PageSize="50">
            </SettingsPager>
            <Settings ShowGroupPanel="True" ShowFooter="True" />
            <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                </LoadingPanelOnStatusBar>
                <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                </LoadingPanel>
            </Images>
            <ImagesFilterControl>
                <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                </LoadingPanel>
            </ImagesFilterControl>
            <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                </Header>
                <LoadingPanel ImageSpacing="5px">
                </LoadingPanel>
            </Styles>
            <StylesPager>
                <PageNumber ForeColor="#3E4846">
                </PageNumber>
                <Summary ForeColor="#1E395B">
                </Summary>
            </StylesPager>
            <StylesEditors ButtonEditCellSpacing="0">
                <ProgressBar Height="21px">
                </ProgressBar>
            </StylesEditors>
        </dx:ASPxGridView>
    </div>
</asp:Content>

