<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-sellin-mtd.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_sellin_mtd" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">



      <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>


      <div style="color:#fff" >
        Lưu ý : bạn không thể xuất báo cáo dữ liệu  60 ngày.
    </div>
    <div>
        <table>
            <tr>
                <td style="color:#fff" >Xem báo cáo Từ ngày</td>
                <td>
                        <telerik:RadDatePicker ID="rdpTuNgay" runat="server" EnableTyping="False">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" ReadOnly="True">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                </td>
                <td style="color:#fff" >
                    Đến ngày</td>
                <td>
                        <telerik:RadDatePicker ID="rdpDenNgay" runat="server" EnableTyping="False">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" ReadOnly="True">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
</DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                </td>
                <td style="margin-left: 160px">

                    &nbsp;</td>
                   <td style="margin-left: 160px">

                    <asp:Button ID="btnReload" runat="server" Text="Xem báo cáo" Width="100px" OnClick="btnReload_Click" />
                </td>
                <td>

                    <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel_Click" />
                </td>
            </tr>
        </table>
    </div>




        <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="ASPxPivotGrid">
        </dx:ASPxPivotGridExporter>
        <br />
        

        <dx:ASPxPivotGrid ID="ASPxPivotGrid" runat="server" ClientIDMode="AutoID" Width="100%"
        OnPreRender="ASPxPivotGrid_PreRender" 
        oncustomcellstyle="ASPxPivotGrid_CustomCellStyle" 
        oncustomcellvalue="ASPxPivotGrid_CustomCellValue">
        <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
            <TotalCellStyle Font-Bold="True">
            </TotalCellStyle>
            <GrandTotalCellStyle Font-Bold="False">
            </GrandTotalCellStyle>
            <CustomTotalCellStyle Font-Bold="False">
            </CustomTotalCellStyle>
            <LoadingPanel ImageSpacing="5px">
            </LoadingPanel>
        </Styles>
        <OptionsLoadingPanel ImagePosition="Top">
            <Image Url="~/App_Themes/Office2010Blue/PivotGrid/Loading.gif">
            </Image>
            <Style ImageSpacing="5px">
                
            </Style>
        </OptionsLoadingPanel>
        <OptionsPager CurrentPageNumberFormat="{0}">
        </OptionsPager>
        <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
            <CustomizationFieldsBackground Url="~/App_Themes/Office2010Blue/PivotGrid/pcHBack.png" />
            <LoadingPanel Url="~/App_Themes/Office2010Blue/PivotGrid/Loading.gif" />
        </Images>
        <OptionsView ShowDataHeaders="False" ShowHorizontalScrollBar="True" ShowGrandTotalsForSingleValues="True"
            ShowTotalsForSingleValues="True" ShowCustomTotalsForSingleValues="True" 
            ShowRowTotals="False"></OptionsView>
        <Fields>
            <dx:PivotGridField ID="fieldtransdategmt" AreaIndex="1" Caption="Ngày"
                FieldName="trans_date_gmt" Area="RowArea" CellFormat-FormatString="dd/MM/yyyy" 
                ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" 
                TotalsVisibility="CustomTotals">
                <CellStyle Wrap="False">
                </CellStyle>
                <HeaderStyle Wrap="False" />
                <ValueStyle Wrap="False">
                </ValueStyle>
                <CustomTotals>
                    <dx:PivotGridCustomTotal />
                </CustomTotals>
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsellincode" Area="RowArea" AreaIndex="2" Caption="Mã nhập kho"
                FieldName="sellin_code" TotalsVisibility="CustomTotals">
                <CustomTotals>
                    <dx:PivotGridCustomTotal />
                </CustomTotals>
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtotalsellin" Area="DataArea" AreaIndex="3" Caption="GT Nhập"
                FieldName="total_sellin" CellFormat-FormatString="###,###.###" CellFormat-FormatType="Numeric"
                EmptyCellText="0" EmptyValueText="0" GrandTotalCellFormat-FormatString="###,###.###"
                GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="###,###.###"
                TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="###,###.###"
                TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.###"
                ValueFormat-FormatType="Numeric">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="0" Caption="Nhà phân phối"
                FieldName="store_name" TotalsVisibility="CustomTotals">
                <CustomTotals>
                    <dx:PivotGridCustomTotal />
                </CustomTotals>
                <CustomTotals>
                    <dx:PivotGridCustomTotal />
                </CustomTotals>
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldareaname" AreaIndex="1" Caption="Area" 
                FieldName="area_name" TotalsVisibility="CustomTotals">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtotalpromo" Area="DataArea" AreaIndex="2" Caption="GT KM"
                FieldName="total_promo" CellFormat-FormatString="###,###.###" CellFormat-FormatType="Numeric"
                EmptyCellText="0" EmptyValueText="0" GrandTotalCellFormat-FormatString="###,###.###"
                GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="###,###.###"
                TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="###,###.###"
                TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.###"
                ValueFormat-FormatType="Numeric">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldregionname" AreaIndex="0" Caption="Region" FieldName="region_name" 
                TotalsVisibility="CustomTotals">
                <CustomTotals>
                    <dx:PivotGridCustomTotal />
                </CustomTotals>
            </dx:PivotGridField>
            <dx:PivotGridField ID="fielditemname" AreaIndex="4" Caption="Tên hàng hóa" 
                FieldName="item_name" TotalsVisibility="CustomTotals" Area="RowArea">
                <CustomTotals>
                    <dx:PivotGridCustomTotal />
                </CustomTotals>
            </dx:PivotGridField>
            <dx:PivotGridField ID="fielditemcode" AreaIndex="3" Caption="Mã hàng hóa" 
                FieldName="item_code" TotalsVisibility="CustomTotals" Area="RowArea">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqtysellin" Area="DataArea" AreaIndex="0" Caption="SL Nhập"
                FieldName="qty_sellin" CellFormat-FormatString="###,###.###" CellFormat-FormatType="Numeric"
                EmptyCellText="0" EmptyValueText="0" GrandTotalCellFormat-FormatString="###,###.###"
                GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="###,###.###"
                TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="###,###.###"
                TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.###"
                ValueFormat-FormatType="Numeric">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqtypromo" AreaIndex="1" FieldName="qty_promo" Area="DataArea" Caption="SL KM" CellFormat-FormatString="###,###.###" CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0" GrandTotalCellFormat-FormatString="###,###.###" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="###,###.###" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="###,###.###" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.###" ValueFormat-FormatType="Numeric">
            </dx:PivotGridField>
        </Fields>
        <OptionsView ShowDataHeaders="False" ShowRowTotals="False" />
        <OptionsPager RenderMode="Lightweight" RowsPerPage="15">
        </OptionsPager>
        <Styles>
            <TotalCellStyle Font-Bold="True">
            </TotalCellStyle>
            <GrandTotalCellStyle Font-Bold="False">
            </GrandTotalCellStyle>
            <CustomTotalCellStyle Font-Bold="False">
            </CustomTotalCellStyle>
        </Styles>
        <StylesPager>
            <PageNumber ForeColor="#3E4846">
            </PageNumber>
            <Summary ForeColor="#1E395B">
            </Summary>
        </StylesPager>
        <StylesEditors ButtonEditCellSpacing="0">
        </StylesEditors>
    </dx:ASPxPivotGrid>


    </div>
</asp:Content>
