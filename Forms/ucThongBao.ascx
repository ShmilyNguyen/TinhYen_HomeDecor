<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucThongBao.ascx.cs" Inherits="WKS.DMS.WEB.Forms.ucThongBao" %>


<telerik:RadPageLayout runat="server" ID="radpagelayout1">
    <Rows>
        <telerik:LayoutRow>
            <Columns>
                <telerik:LayoutColumn CssClass="jumbotron">
                    <%-- <h1>Thông báo chốt số liệu :</h1>
                        <p><strong>Chốt số liệu tháng 10/2015 : </strong>Số liệu đã chốt, bắt đầu nhập liệu cho tháng 11/2015, các trường hợp phát sinh, vui lòng liên hệ Admin của công ty để xử lý. Xin cám ơn.</p>--%>




                    <asp:Repeater ID="rptThongBao" runat="server">




                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <div class="panel-group">
                                <div class="panel panel-primary">
                                    <div class="panel-heading"><%# Eval("title") %></div>
                                    <div class="panel-body"><%# Eval("body") %></div>
                                </div>
                            </div>



                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>


                </telerik:LayoutColumn>
            </Columns>
        </telerik:LayoutRow>
        <%-- <telerik:layoutrow>
                <columns>
                    <telerik:layoutcolumn hiddenmd="true" hiddensm="true" hiddenxs="true">

                        <h3>h3 text, font size 24 px </h3>
                        ut aliquam elit eget quam tincidunt, et aliquam libero congue. phasellus aliquet sed quam vitae dictum. aliquam erat volutpat. morbi accumsan a mi quis pretium. 

                    </telerik:layoutcolumn>
                </columns>
            </telerik:layoutrow>--%>
    </Rows>
</telerik:RadPageLayout>

