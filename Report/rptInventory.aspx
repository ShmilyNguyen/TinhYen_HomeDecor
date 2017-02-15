<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rptInventory.aspx.cs" Inherits="WKS.DMS.WEB.Reports.rptInventory" %>

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
                        <asp:ListItem >2016</asp:ListItem>
                          <asp:ListItem Selected="True">2017</asp:ListItem>

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
                
                <td style="color:#fff" >

                    Tồn kho theo giá</td>
                
                <td>
                    
                     <asp:DropDownList ID="price_group" runat="server" >
                        <asp:ListItem>GT</asp:ListItem>
                        <asp:ListItem>MT</asp:ListItem>
                    </asp:DropDownList>

                </td>

                <td>
                   
                    <asp:HiddenField  runat="server" />
                    <asp:DropDownList ID="ddlReportType" runat="server" Visible="false">
                      
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
        <dx:ASPxPivotGrid ID="grdData" runat="server"
            OnCustomCellStyle="grdData_CustomCellStyle" ClientIDMode="AutoID"
            Theme="Office2010Blue">
            <Fields>
                <dx:PivotGridField ID="fieldregionname" Area="RowArea" AreaIndex="0" Caption="Region" FieldName="region_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldareaname" Area="RowArea" AreaIndex="1" Caption="Area" FieldName="area_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="2" Caption="Nhà phân phối" FieldName="store_name">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fielditemcode" Area="RowArea" AreaIndex="3" Caption="Mã hàng hóa" FieldName="item_code">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fielditemname" Area="RowArea" AreaIndex="4" Caption="Hàng hóa" FieldName="item_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtysaleoutbegin" Area="DataArea" AreaIndex="0" Caption="Tồn đầu HB" FieldName="SLTonDauHangBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtypromobegin" Area="DataArea" AreaIndex="1" Caption="Tồn đầu KM" FieldName="SLTonDauKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtysellin" Area="DataArea" AreaIndex="2" Caption="Nhập HB" FieldName="SLMuaVaoHangBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtypromosellin" Area="DataArea" AreaIndex="3" Caption="Nhập KM" FieldName="SLMuaVaoKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtysaleout" Area="DataArea" AreaIndex="4" Caption="Xuất bán" FieldName="SLBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtypromosaleout" Area="DataArea" AreaIndex="5" Caption="Xuất KM" FieldName="SLKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtysaleout6" Area="DataArea" AreaIndex="6" Caption="Xuất bán tạm" Visible ="False" FieldName="SLBan_tmp" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldqtypromosaleout7" Area="DataArea" AreaIndex="7" Caption="Xuất KM tạm" Visible ="False" FieldName="SLKM_tmp" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldSLTonCuoiThucTeHangBan" Area="DataArea" AreaIndex="8" Caption="SL tồn cuối HB" FieldName="SLTonCuoiThucTe_HangBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldSLTonCuoiThucTeHangKM" Area="DataArea" AreaIndex="9" Caption="SL tồn cuối KM" FieldName="SLTonCuoiThucTe_HangKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout8" Area="DataArea" AreaIndex="10" Caption="SL Tồn cuối" FieldName="SLTonCuoiThucTe" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout9" Area="DataArea" AreaIndex="11" Caption="SL Tồn cuối tạm" Visible ="False" FieldName="SLTonCuoi_tmp" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout10" Area="DataArea" AreaIndex="12" Caption="GT Tồn đầu HB" FieldName="GTTonDauHangBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout11" Area="DataArea" AreaIndex="13" Caption="GT Tồn đầu KM" FieldName="GTTonDauKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout12" Area="DataArea" AreaIndex="14" Caption="GT Nhập HB" FieldName="GTMuaVaoHangBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout13" Area="DataArea" AreaIndex="15" Caption="GT Nhập KM" FieldName="GTMuaVaoKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout14" Area="DataArea" AreaIndex="16" Caption="GT Bán" FieldName="GTBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout15" Area="DataArea" AreaIndex="17" Caption="GT KM" FieldName="GTKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout16" Area="DataArea" AreaIndex="18" Caption="GT Bán tạm" Visible ="False"  FieldName="GTBan_tmp" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout17" Area="DataArea" AreaIndex="19" Caption="GT KM tạm" Visible ="False"  FieldName="GTKM_tmp" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldGTTonCuoiHangBan" Area="DataArea" AreaIndex="20" Caption="GT tồn cuối HB" FieldName="GTTonCuoi_HangBan" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldGTTonCuoiHangKM" Area="DataArea" AreaIndex="21" Caption="GT tồn cuối KM" FieldName="GTTonCuoi_HangKM" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout18" Area="DataArea" AreaIndex="22" Caption="GT tồn cuối" FieldName="GTTonCuoi" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>

                <dx:PivotGridField ID="fieldqtypromosaleout19" Area="DataArea" AreaIndex="23" Caption="GT tồn cuối tạm" Visible ="False"  FieldName="GTTonCuoi_tmp" CellFormat-FormatString="###,###.##"
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
            <OptionsPager RowsPerPage="50">
            </OptionsPager>
        </dx:ASPxPivotGrid>
    </div>
</asp:Content>