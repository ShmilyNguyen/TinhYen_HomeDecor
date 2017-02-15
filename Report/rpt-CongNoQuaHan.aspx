<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rpt-CongNoQuaHan.aspx.cs" Inherits="WKS.DMS.WEB.Report.rpt_CongNoQuaHan" %>


<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid.Export" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


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

        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" GridViewID="grvData" PaperKind="A4">
    </dx:ASPxGridViewExporter>



        <dx:ASPxGridView ID="grvData" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" Width="100%" EnableTheming="True" Theme="Office2010Blue">
            <GroupSummary>
                <dx:ASPxSummaryItem FieldName="ending_balance" SummaryType="Sum" />
            </GroupSummary>
            <Columns>
                <dx:GridViewDataTextColumn Caption="Nhà phân phối" FieldName="store_name" VisibleIndex="0" Width="200px" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Nhân viên" FieldName="employee_name" VisibleIndex="1" Width="200px" GroupIndex="1" SortIndex="1" SortOrder="Ascending">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Khách hàng" FieldName="customer_name"  VisibleIndex="2" Width="250px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Ngày HD" FieldName="payment_date" VisibleIndex="3">
                    <PropertiesDateEdit DisplayFormatString="" Spacing="0">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                 
                <dx:GridViewDataTextColumn Caption="Công nợ hiện tại" FieldName="ending_balance" VisibleIndex="4" UnboundType="Decimal">
                    
                    <PropertiesTextEdit DisplayFormatString="###,###.##">
                    </PropertiesTextEdit>
                    
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Số ngày quá hạn" VisibleIndex="6" FieldName="SoNgayQuaHan">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Ngày hiện tại" FieldName="NgayHienTai" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
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