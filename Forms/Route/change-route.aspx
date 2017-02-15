<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="change-route.aspx.cs" Inherits="WKS.DMS.WEB.Forms.Route.change_route" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

    <h1>Luân chuyển khách hàng</h1>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <table>
        <tr>
            <td>Chuyển khách hàng từ</td>
            <td></td>
            <td>Chuyển khách sang</td>
        </tr>
        <tr>
            <td valign="top">
                <!--LEFT PANE-->

                <table>

                    <tr>

                        <td>
                            <telerik:RadComboBox ID="cbxStore1" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore1_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>

                    <tr>

                        <td>
                            <telerik:RadComboBox ID="cbxEmployee1" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxEmployee1_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>



                    <tr>

                        <td>
                           <i>Tìm kiếm Mã khách hàng, tên khách hàng, địa chỉ, điện thoại,
                               <br /> hoặc danh sách mã cần tìm. Ví dụ : 0001, 0002,0003,0004,...</i>
                            

                        </td>
                        <td>
                           

                        </td>
                    </tr>
                     <tr>

                        <td>
                           
                            <asp:TextBox ID="txtKeyword1" runat="server" Width="400px"></asp:TextBox>&nbsp;&nbsp;&nbsp; </td>
                        <td>
                            <asp:Button ID="btnSearch1" class="btn btn-success submit" runat="server" Text="Tìm kiếm" OnClick="btnSearch1_Click"  />

                        </td>
                    </tr>

                </table>

                <div style="overflow-x: auto; width: 100%">
                    <asp:GridView ID="gvData1" runat="server" Width="100%" class="table table-striped responsive-utilities jambo_table bulk_action"
                        AutoGenerateColumns="False" Font-Names="Tahoma"
                        Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                        HeaderStyle-BackColor="silver" AllowPaging="True" ShowFooter="True" OnPageIndexChanging="OnPaging1"
                        
                         PageSize="50" ShowHeaderWhenEmpty="True">
                        <Columns>
                            
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Eval("customer_code")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="200px" HeaderText="Khách hàng">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("customer_name")%>'></asp:Label>

                                </ItemTemplate>


                                <ItemStyle Width="200px" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Hoạt động">
                            <ItemTemplate>

                                 <asp:CheckBox ID="chckActive" runat="server" Checked='<%# Eval("active")%>' Enabled="false" />
                                
                            </ItemTemplate>
                            
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="lnkRemove" runat="server" CommandArgument='<%# Eval("store_id")+"," +Eval("customer_id")+"," +Eval("employee_id")+"," +Eval("active") %>'
                                        Text="Chuyển >>" OnClick="MoveData1"></asp:Button>
                                </ItemTemplate>


                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                        </Columns>
                        <AlternatingRowStyle BackColor="#C2D69B" />
                        <HeaderStyle BackColor="Silver" />
                    </asp:GridView>
                </div>

                <!--END LEFT PANE-->
            </td>
            <td></td>
            <td valign="top">


                 <!--RIGHT PANE-->

                <table>

                    <tr>

                        <td>
                            <telerik:RadComboBox ID="cbxStore2" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="store_name" DataValueField="store_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxStore2_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>

                    <tr>

                        <td>
                            <telerik:RadComboBox ID="cbxEmployee2" OnClientFocus="OnClientFocusHandler" runat="server"
                                DataTextField="employee_name" DataValueField="employee_id" DropDownAutoWidth="Enabled"
                                Filter="Contains" Width="200px" OpenDropDownOnFocus="True" AutoPostBack="True" OnSelectedIndexChanged="cbxEmployee2_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>



                     <tr>

                        <td>
                           <i>Tìm kiếm Mã khách hàng, tên khách hàng, địa chỉ, điện thoại,
                               <br /> hoặc danh sách mã cần tìm. Ví dụ : 0001, 0002,0003,0004,...</i>
                           </td>
                        <td>
                           

                        </td>
                    </tr>
                    <tr>

                        <td>
                          
                            <asp:TextBox ID="txtKeyword2" runat="server" Width="400px"></asp:TextBox>&nbsp;&nbsp;&nbsp; </td>
                        <td>
                            <asp:Button ID="btnSearch2" class="btn btn-success submit" runat="server" Text="Tìm kiếm" OnClick="btnSearch2_Click"  />

                        </td>
                    </tr>

                </table>

                <div style="overflow-x: auto; width: 100%">
                    <asp:GridView ID="gvData2" runat="server" Width="100%" class="table table-striped responsive-utilities jambo_table bulk_action"
                        AutoGenerateColumns="False" Font-Names="Tahoma"
                        Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                        HeaderStyle-BackColor="silver" AllowPaging="True" ShowFooter="True" OnPageIndexChanging="OnPaging2"
                        
                         PageSize="50" ShowHeaderWhenEmpty="True">
                        <Columns>
                            
                            <asp:TemplateField ItemStyle-Width="100px" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="lnkRemove" runat="server" CommandArgument='<%# Eval("store_id")+"," +Eval("customer_id")+"," +Eval("employee_id") +"," +Eval("active") %>'
                                        Text="<< Chuyển" ></asp:Button>
                                </ItemTemplate>


                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Eval("customer_code")%>'></asp:Label>
                                </ItemTemplate>


                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="200px" HeaderText="Khách hàng">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("customer_name")%>'></asp:Label>

                                </ItemTemplate>


                                <ItemStyle Width="200px" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Hoạt động">
                            <ItemTemplate>

                                 <asp:CheckBox ID="chckActive" runat="server" Checked='<%# Eval("active")%>' Enabled="false" />
                                
                            </ItemTemplate>
                            
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                            

                        </Columns>
                        <AlternatingRowStyle BackColor="#C2D69B" />
                        <HeaderStyle BackColor="Silver" />
                    </asp:GridView>
                </div>

                <!--END RIGHT PANE-->
            </td>
        </tr>
    </table>

        </ContentTemplate>
    </asp:UpdatePanel>
   

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <script type="text/javascript">

            function OnClientFocusHandler(sender, eventArgs) {
                if (!sender.get_dropDownVisible()) {
                    sender.showDropDown();
                }
            }







        </script>
    </telerik:RadCodeBlock>

</asp:Content>
