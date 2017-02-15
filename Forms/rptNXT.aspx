<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rptNXT.aspx.cs" Inherits="WKS.DMS.WEB.Forms.rptNXT" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server">
</dx:ASPxPivotGridExporter>
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID">
        <Fields>
            <dx:PivotGridField ID="field" Area="RowArea" AreaIndex="0" Caption="Region">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field1" Area="RowArea" AreaIndex="1" Caption="Area">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field2" Area="RowArea" AreaIndex="2" Caption="Nhà phân phối">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field3" Area="RowArea" AreaIndex="3" Caption="Hàng hóa">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field4" Area="DataArea" AreaIndex="0" Caption="Tồn đầu HB">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field5" Area="DataArea" AreaIndex="1" Caption="Tồn đầu KM">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field6" Area="DataArea" AreaIndex="2" Caption="Nhập HB">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field7" Area="DataArea" AreaIndex="3" Caption="Nhập KM">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field8" Area="DataArea" AreaIndex="4" Caption="Xuất HB">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field9" Area="DataArea" AreaIndex="5" Caption="Xuất KM">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field10" Area="DataArea" AreaIndex="6" Caption="Tồn cuối HB">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field11" Area="DataArea" AreaIndex="7" Caption="Tồn cuối KM">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field12" Area="DataArea" AreaIndex="8" Caption="GT Tồn cuối HB">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field13" Area="DataArea" AreaIndex="9" Caption="GT Tồn cuối KM">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>

</asp:Content>
