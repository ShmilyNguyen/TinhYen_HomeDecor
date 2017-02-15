<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-BaoCaoChietKhauKM.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_BaoCaoChietKhauKM" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="../../styles/grid.css" rel="stylesheet" />

     <style type="text/css">
div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div>
        <table>
            <tr>
                <td style="color:#fff">Xem báo cáo Từ ngày</td>
                <td>
                        <telerik:RadDatePicker ID="rdpTuNgay" runat="server">
                        </telerik:RadDatePicker>
                </td>
                <td style="color:#fff">
                    Đến ngày</td>
                <td>
                        <telerik:RadDatePicker ID="rdpDenNgay" runat="server">
                        </telerik:RadDatePicker>
                </td>
                <td style="margin-left: 160px">

                    <asp:Button ID="btnReload" runat="server" Text="Xem báo cáo" Width="100px" />
                </td>
                <td>

                    <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>

        <br />
        <br />

     <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grdData">
        </dx:ASPxPivotGridExporter>
        <dx:ASPxPivotGrid ID="grdData" runat="server" ClientIDMode="AutoID" OnCustomCellStyle="grdData_CustomCellStyle">
            <Fields>
                <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="0" Caption="Nhà phân phối" FieldName="store_name">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldchannelname" Area="RowArea" AreaIndex="1" Caption="Kênh KH" FieldName="channel_name">
                </dx:PivotGridField>

                 <dx:PivotGridField ID="fieldsaleout_code" Area="RowArea" AreaIndex="2" Caption="Số CT" FieldName="saleout_code">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldtrans_date_gmt" Area="RowArea" AreaIndex="3" Caption="Ngày" FieldName="trans_date_gmt">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldemployee_name" Area="RowArea" AreaIndex="4" Caption="Nhân viên" FieldName="employee_name">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldcustomer_name" Area="RowArea" AreaIndex="5" Caption="Khách hàng" FieldName="customer_name">
                </dx:PivotGridField>




                <dx:PivotGridField ID="fieldGTDonHang" Area="DataArea" AreaIndex="0" Caption="GT Đơn hàng" FieldName="GTDonHang" CellFormat-FormatString="###,###.##" CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0" GrandTotalCellFormat-FormatString="###,###.##" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric" TotalsVisibility="CustomTotals" TotalValueFormat-FormatString="###,###.##" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##" ValueFormat-FormatType="Numeric">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldGTCKLine" Area="DataArea" AreaIndex="1" 
                    Caption="Chiết khấu Line" FieldName="GTCKLine" CellFormat-FormatString="###,###.##" CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0" GrandTotalCellFormat-FormatString="###,###.##" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric" TotalsVisibility="CustomTotals" TotalValueFormat-FormatString="###,###.##" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##" ValueFormat-FormatType="Numeric">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldGTCKOntop" Area="DataArea" AreaIndex="2" 
                    Caption="Chiết khấu Ontop" FieldName="GTCKOntop" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
            </Fields>
            <OptionsView ShowHorizontalScrollBar="True" />
            <OptionsPager RowsPerPage="30">
            </OptionsPager>
            <OptionsLoadingPanel>
                <Image Url="~/App_Themes/Office2010Blue/PivotGrid/Loading.gif">
                </Image>
                <Style ImageSpacing="5px">
                </Style>
            </OptionsLoadingPanel>
            <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                <CustomizationFieldsBackground Url="~/App_Themes/Office2010Blue/PivotGrid/pcHBack.png">
                </CustomizationFieldsBackground>
                <LoadingPanel Url="~/App_Themes/Office2010Blue/PivotGrid/Loading.gif">
                </LoadingPanel>
            </Images>
            <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
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
            </StylesEditors>
        </dx:ASPxPivotGrid>
    </div>
</asp:Content>

