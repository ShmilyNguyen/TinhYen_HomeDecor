<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-DoanhSoKhachHangVsChiTieu.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_DoanhSoKhachHangVsChiTieu" %>
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
               
                <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="0"
                    Caption="Nhà phân phối" FieldName="store_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldemployeename" Area="RowArea" AreaIndex="1"
                    Caption="Nhân viên" FieldName="employee_name">
                </dx:PivotGridField>
                 <dx:PivotGridField ID="fieldcustomer_name" Area="RowArea" AreaIndex="3"
                    Caption="Khách Hàng" FieldName="customer_name">
                </dx:PivotGridField>
                
             
                <dx:PivotGridField ID="fieldaddress_full" Area="RowArea" AreaIndex="4"
                    Caption="Địa chỉ" FieldName="address_full">
                </dx:PivotGridField>



               

                <dx:PivotGridField ID="fieldThanhTien" Area="DataArea" AreaIndex="0"
                    Caption="Doanh số" FieldName="ThanhTien" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>


                 <dx:PivotGridField ID="fieldtargetvalue" Area="DataArea" AreaIndex="1"
                    Caption="Chỉ tiêu" FieldName="target_value"
                    CellFormat-FormatString="###,###.##" CellFormat-FormatType="Numeric"
                    EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalsVisibility="CustomTotals" TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric">
                </dx:PivotGridField>


                <dx:PivotGridField ID="fieldsalevstarget" Area="DataArea" AreaIndex="2"
                    Caption="% Đạt" FieldName="sale_vs_target" CellFormat-FormatString="p"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="p"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="p" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="p"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="p"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                
                
                <dx:PivotGridField ID="fieldcustomercode" Area="RowArea" AreaIndex="2" Caption="Code" FieldName="customer_code">
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
