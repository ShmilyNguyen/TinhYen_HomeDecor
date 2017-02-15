<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-BaoCaoCongNo.aspx.cs" Inherits="WKS.DMS.WEB.Reports.rpt_BaoCaoCongNo" %>

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
                
                <td style="color:#fff" >

                 
                
                <td>
                    
                     <asp:DropDownList ID="price_group" runat="server" Visible ="false" >
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

                    <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" Width="100px" OnClick="btnExportExcel_Click"  Visible="false"/>
                </td>
            </tr>
        </table>
    </div>
    <div>

        <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grdData">
        </dx:ASPxPivotGridExporter>
        <dx:ASPxPivotGrid ID="grdData" runat="server"
            OnCustomCellStyle="grdData_CustomCellStyle" ClientIDMode="AutoID"
            Theme="Office2010Blue" Width="100%">
            <Fields >
                <dx:PivotGridField ID="fieldregionname" Area="RowArea" AreaIndex="0" Caption="Region" FieldName="region_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldareaname" Area="RowArea" AreaIndex="1" Caption="Area" FieldName="area_name">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldstorename" Area="RowArea" AreaIndex="2" Caption="Nhà phân phối" FieldName="store_name">
                </dx:PivotGridField>

              
                <dx:PivotGridField ID="fieldcongnogoidau" Area="DataArea" AreaIndex="1" Caption="Công nợ gối đầu" FieldName="init_balance" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldcongnodauky" Area="DataArea" AreaIndex="2" Caption="Công nợ đầu kỳ" FieldName="congnodauky" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldcongnophatsinh" Area="DataArea" AreaIndex="3" Caption="Công nợ phát sinh" FieldName="total_amt" CellFormat-FormatString="###,###.##"
                    CellFormat-FormatType="Numeric" EmptyCellText="0" EmptyValueText="0"
                    GrandTotalCellFormat-FormatString="###,###.##"
                    GrandTotalCellFormat-FormatType="Numeric"
                    TotalCellFormat-FormatString="###,###.##" TotalCellFormat-FormatType="Numeric"
                    TotalValueFormat-FormatString="###,###.##"
                    TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="###,###.##"
                    ValueFormat-FormatType="Numeric" TotalsVisibility="CustomTotals">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldcongnotong" Area="DataArea" AreaIndex="4" Caption="Công nợ tổng " FieldName="tongcongno" CellFormat-FormatString="###,###.##"
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