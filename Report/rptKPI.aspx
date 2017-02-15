<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rptKPI.aspx.cs" Inherits="WKS.DMS.WEB.Report.rptKPI" %>

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
                <td style="color:#fff" >Năm</td>
                <td>
                    <asp:DropDownList ID="ddlNam" runat="server">
                        <asp:ListItem >2015</asp:ListItem>
                        <asp:ListItem Selected="True">2016</asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td style="color:#fff" >Tháng </td>
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

                    <asp:Button ID="btnReload" runat="server" Text="Xem báo cáo" Width="100px" OnClick="btnReload_Click" />
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
        <dx:ASPxPivotGrid ID="grdData" runat="server" ClientIDMode="AutoID" OnCustomCellStyle="grdData_CustomCellStyle" OnCustomCellValue="grdData_CustomCellValue">
            <Fields>
                <dx:PivotGridField ID="fieldregionname" Area="RowArea" AreaIndex="0"
                    Caption="Region" FieldName="region_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="1"
                    Caption="Nhà phân phối" FieldName="store_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldemployeename" Area="RowArea" AreaIndex="3"
                    Caption="Nhân viên" FieldName="employee_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldoutlet" Area="DataArea" AreaIndex="0"
                    Caption="Outlet" FieldName="outlet"
                    CellFormat-FormatString="###,###.##" CellFormat-FormatType="Numeric"
                    EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalsVisibility="CustomTotals" TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldactiveoutlet" Area="DataArea" AreaIndex="1"
                    Caption="Active Outlet" FieldName="activeoutlet">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldtargetactiveoutlet" Area="DataArea" Caption="Chỉ tiêu" AreaIndex="2" CellFormat-FormatString="###,###.##" CellFormat-FormatType="Numeric"
                    EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalsVisibility="CustomTotals" TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" FieldName="target_active_outlet">
                </dx:PivotGridField>


                <dx:PivotGridField ID="fieldactiveoutletvstarget" Area="DataArea" Caption="% Đạt" AreaIndex="3" CellFormat-FormatString="p"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="p"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="p" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="p"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="p"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals" FieldName="activeoutlet_vs_target">
                </dx:PivotGridField>



               

                <dx:PivotGridField ID="fieldsaleout" Area="DataArea" AreaIndex="4"
                    Caption="Doanh số" FieldName="saleout" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>


                 <dx:PivotGridField ID="fieldtargetsaleout" Area="DataArea" AreaIndex="5"
                    Caption="Chỉ tiêu" FieldName="target_saleout"
                    CellFormat-FormatString="###,###.##" CellFormat-FormatType="Numeric"
                    EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalsVisibility="CustomTotals" TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric">
                </dx:PivotGridField>


                <dx:PivotGridField ID="fieldsaleoutvstarget" Area="DataArea" AreaIndex="6"
                    Caption="% Đạt" FieldName="saleout_vs_target" CellFormat-FormatString="p"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="p"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="p" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="p"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="p"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldorderno" Area="DataArea" AreaIndex="7" Caption="Số đơn hàng" FieldName="orderno" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldtargetorder" Area="DataArea" AreaIndex="8" Caption="Chỉ tiêu" FieldName="target_order" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldordervstarget" Area="DataArea" AreaIndex="9" Caption="% Đạt" FieldName="order_vs_target" CellFormat-FormatString="p"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="p"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="p" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="p"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="p"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldsaleoutfocus" Area="DataArea" AreaIndex="10" Caption="DS Tập trung" FieldName="saleout_focus" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldtargetfocussaleout" Area="DataArea" AreaIndex="11" Caption="Chỉ tiêu" FieldName="target_focus_saleout" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldsaleoutfocus_vs_target" Area="DataArea" AreaIndex="12" Caption="% Đạt" FieldName="saleoutfocus_vs_target" CellFormat-FormatString="p"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="p"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="p" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="p"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="p"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldordernofocus" Area="DataArea" AreaIndex="13" Caption="Đơn hàng tập trung" FieldName="orderno_focus" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldtargetfocusorder" Area="DataArea" AreaIndex="14" Caption="Chỉ tiêu" FieldName="target_focus_order" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldorderfocusvstarget" Area="DataArea" AreaIndex="15" Caption="% Đạt" FieldName="orderfocus_vs_target" CellFormat-FormatString="p"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="p"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="p" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="p"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="p"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                
                <dx:PivotGridField ID="fieldemployeecode" Area="RowArea" AreaIndex="2" Caption="Mã NV" FieldName="employee_code">
                </dx:PivotGridField>
                
            </Fields>
            <OptionsView ShowHorizontalScrollBar="True" ShowDataHeaders="False" />
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