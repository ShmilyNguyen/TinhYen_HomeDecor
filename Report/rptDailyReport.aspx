<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rptDailyReport.aspx.cs" Inherits="WKS.DMS.WEB.Reports.rptDailyReport" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div>
        <table>
            <tr>
                <td style="color: #fff">Năm</td>
                <td>


                    <asp:DropDownList ID="ddlNam" runat="server">
                        <asp:ListItem>2015</asp:ListItem>
                        <asp:ListItem >2016</asp:ListItem>
                        <asp:ListItem Selected="True">2017</asp:ListItem>

                    </asp:DropDownList>

                </td>
                <td style="color: #fff">Tháng </td>
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
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem Selected="True">10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>

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


        <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grdData">
        </dx:ASPxPivotGridExporter>
        <dx:ASPxPivotGrid ID="grdData" runat="server" ClientIDMode="AutoID" OnCustomCellStyle="grdData_CustomCellStyle">
            <Fields>
                <dx:PivotGridField ID="fieldrsmname" Area="RowArea" AreaIndex="0" Caption="RSM" FieldName="rsm_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldasmname" Area="RowArea" AreaIndex="1" Caption="ASM" FieldName="asm_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="3" Caption="Nhà phân phối" FieldName="store_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldemployeename" Area="RowArea" AreaIndex="4" Caption="Nhân viên" FieldName="employee_name">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fielditemcode" Area="RowArea" AreaIndex="5" Caption="Mã hàng hóa" FieldName="item_code">
                </dx:PivotGridField>


                <dx:PivotGridField ID="fielditemname" Area="RowArea" AreaIndex="6" Caption="Hàng hóa" FieldName="item_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldngay" Area="ColumnArea" AreaIndex="0" Caption="Ngày" FieldName="ngay">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldtotalsaleout" Area="DataArea" AreaIndex="0" Caption="DS Bán" FieldName="total_saleout" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldsupname" Area="RowArea" AreaIndex="2" Caption="SUP" FieldName="sup_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtysaleout" Area="DataArea" AreaIndex="1" Caption="SL Bán" FieldName="qty_saleout" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldtotalpromo" Area="DataArea" AreaIndex="2" Caption="GT KM" FieldName="total_promo" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals" Visible="False">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtypromo" Area="DataArea" AreaIndex="2" Caption="SL KM" FieldName="qty_promo" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals" Visible="False">
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
