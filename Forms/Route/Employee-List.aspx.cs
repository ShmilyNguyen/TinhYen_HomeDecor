using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms
{
    public partial class Employee_List : System.Web.UI.Page
    {
        public DataTable myData
        {
            get
            {
                DataTable data = GetData();

                return data;
            }
        }

        public DataTable GetData()
        {
//            DataTable data = new DataTable();

//            string sQuery = "";

//            if (Session["role"].ToString().Contains("ADMIN"))
//            {
//                sQuery = @"SELECT  A.* ,
//                                    B.store_name ,
//                                    C.employee_name AS QuanLyCapTren
//                            FROM    employee AS A
//                                    LEFT JOIN store AS B ON A.store_id = B.store_id
//                                    LEFT JOIN dbo.employee AS C ON A.parent_id = C.employee_id";
//            }
//            else
//            {
//                sQuery = @"SELECT  A.* ,
//                                    B.store_name ,
//                                    C.employee_name AS QuanLyCapTren
//                            FROM    employee AS A
//                                    LEFT JOIN store AS B ON A.store_id = B.store_id
//                                    LEFT JOIN dbo.employee AS C ON A.parent_id = C.employee_id
//                                WHERE   a.store_id IN ( SELECT  store_id
//                                                        FROM    dbo.fn_GetStore_By_UserID({0}) )";
//            }

//            sQuery = string.Format(sQuery, Session["userid"]);
//            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

//            return data;



            DataTable data = new DataTable();
            string storeProc = "[sp_employee_search]";
            //Show All
            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
            {
                SqlCommand cmd = new SqlCommand(storeProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@keyword", txtKeyword.Text.Trim());
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                conn.Close();

                return data;
            }


        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();
                //Session["Data_Employee"] = data;
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.myData;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                //if (e.Item is GridDataItem)
                if (e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

                    //GridDataItem item = (GridDataItem)e.Item;


                    //GridItem parentItem = e.Item ;

                    System.Web.UI.WebControls.TextBox txtID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtID");
                    System.Web.UI.WebControls.TextBox txtCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtCode");

                    System.Web.UI.WebControls.TextBox txtHRCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtHRCode");


                    System.Web.UI.WebControls.TextBox txtName = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtName");

                    System.Web.UI.WebControls.TextBox txtUsername = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtUsername");
                    System.Web.UI.WebControls.TextBox txtPassword = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtPassword");
                    System.Web.UI.WebControls.TextBox txtEmail = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtEmail");
                    System.Web.UI.WebControls.TextBox txtPhone = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtPhone");

                    Telerik.Web.UI.RadComboBox ddlStore = (Telerik.Web.UI.RadComboBox)MyUserControl.FindControl("ddlStore");
                    Telerik.Web.UI.RadComboBox ddlParentID = (Telerik.Web.UI.RadComboBox)MyUserControl.FindControl("ddlParentID");

                    Telerik.Web.UI.RadComboBox cbxGroup_Name = (Telerik.Web.UI.RadComboBox)MyUserControl.FindControl("cbxGroup_Name");
                    Telerik.Web.UI.RadComboBox cbxGroup_id = (Telerik.Web.UI.RadComboBox)MyUserControl.FindControl("cbxGroup_id");


                    DevExpress.Web.ASPxEditors.ASPxCheckBox chckActive = (DevExpress.Web.ASPxEditors.ASPxCheckBox)MyUserControl.FindControl("chckActive");

                    System.Web.UI.WebControls.DropDownList ddlPosition = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlPosition");
                    System.Web.UI.WebControls.DropDownList ddlRole = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlRole");

                    RadDatePicker rdpNgaySinh = (RadDatePicker)MyUserControl.FindControl("rdpNgaySinh");
                    rdpNgaySinh.SelectedDate = DateTime.Now;

                    DataTable tbStore = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM store left join dbo.[group] as b on group_id = b.group_id").Tables[0];
                    DataTable tbEmploy = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM employee").Tables[0];

                    DataTable tblGruop = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM [group]").Tables[0];

                    cbxGroup_Name.DataSource = tblGruop;
                    cbxGroup_Name.DataBind();
                    cbxGroup_Name.Items.Insert(0, new RadComboBoxItem("Chọn Goup.....", "0"));


                    ddlStore.DataSource = tbStore;
                    ddlStore.DataBind();
                    ddlStore.Items.Insert(0, new RadComboBoxItem("Chọn nhà phân phối...", "0"));

                    ddlParentID.DataSource = tbEmploy;
                    ddlParentID.DataBind();
                    ddlParentID.Items.Insert(0, new RadComboBoxItem("Chọn quản lý cấp trên...", "0"));



                    //Neu la Admin moi cho phep tao tai khoan dang nhap

                    if (Session["role"].ToString().Contains("ADMIN"))
                    {
                        txtUsername.Enabled = true;
                        txtPassword.Enabled = true;
                    }
                    else
                    {
                        txtUsername.Enabled = false;
                        txtPassword.Enabled = false;
                    }

                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        
                        string id = parentItem["id"].Text;
                        string code = parentItem["code"].Text;
                        string hrcode = parentItem["HRCode"].Text;

                        string group_id =(parentItem["group_id"].Text);
                        

                        string name = parentItem["name"].Text;

                        string position = parentItem["position"].Text;
                        string role = parentItem["role"].Text;

                        string email = parentItem["email"].Text;
                        string username = parentItem["username"].Text;
                        string password = parentItem["password"].Text;

                        string parent_id = parentItem["parent_id"].Text;
                        string store_id = parentItem["store_id"].Text;
                        string phone = parentItem["phone"].Text;
                        string birthday = parentItem["birthday"].Text;
                        string active = parentItem["active"].Text;
                        string group_name = parentItem["group_name"].Text;




                        txtID.Text = id == "&nbsp;" ? "" : id;
                        txtID.Enabled = false;

                        txtCode.Text = code == "&nbsp;" ? "" : code;
                        txtCode.Enabled = false;


                        txtHRCode.Text = hrcode == "&nbsp;" ? "" : hrcode;


                        txtName.Text = name == "&nbsp;" ? "" : name;

                        ddlParentID.SelectedValue = parent_id == "&nbsp;" ? "" : parent_id;
                        ddlStore.SelectedValue = store_id == "&nbsp;" ? "" : store_id;

                        txtUsername.Text = username == "&nbsp;" ? "" : username;
                        txtPassword.Text = password == "&nbsp;" ? "" : password;
                        txtEmail.Text = email == "&nbsp;" ? "" : email;
                        txtPhone.Text = phone == "&nbsp;" ? "" : phone;

                        rdpNgaySinh.DbSelectedDate = birthday;

                        ddlPosition.SelectedValue = position;
                        ddlRole.SelectedValue = role;

                        cbxGroup_Name.SelectedValue = group_id;
                        
                        active =  active == "&nbsp;" ? "false" : active;
                        chckActive.Checked = bool.Parse(active);
                    }
                    else
                    {
                        //Add New
                        string ID = "";
                        string Code = "";
                        string UserID = Session["userid"].ToString();

                        clsCodeMaster.GenCode("employee", UserID, out ID, out Code);
                        txtID.Text = ID;
                        txtCode.Text = Code;
                        txtCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHeaderItem headerItem = e.Item as GridHeaderItem;
            if (headerItem != null)
            {
                headerItem["EditColumn"].Text = string.Empty;
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    //Update Data

                    try
                    {
                        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                      




                        int id = int.Parse((userControl.FindControl("txtID") as System.Web.UI.WebControls.TextBox).Text.Trim());
                        string code = (userControl.FindControl("txtCode") as System.Web.UI.WebControls.TextBox).Text.Trim();

                        string hrcode = (userControl.FindControl("txtHRCode") as System.Web.UI.WebControls.TextBox).Text.Trim();


                        string name = (userControl.FindControl("txtName") as System.Web.UI.WebControls.TextBox).Text.Trim();

                        string position = (userControl.FindControl("ddlPosition") as System.Web.UI.WebControls.DropDownList).SelectedValue;

                        int parent_id = int.Parse((userControl.FindControl("ddlParentID") as Telerik.Web.UI.RadComboBox).SelectedValue);

                        int store_id = int.Parse((userControl.FindControl("ddlStore") as Telerik.Web.UI.RadComboBox).SelectedValue);
                        string role = ((userControl.FindControl("ddlRole") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                       int  group_id = int.Parse((userControl.FindControl("cbxGroup_name") as Telerik.Web.UI.RadComboBox).SelectedValue);

                       //   DataTable tbEmploy = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT group_name FROM store left join dbo.[group] as b on group_id = b.group_id").Tables[0];

                       




                        DateTime birthday = DateTime.Parse((userControl.FindControl("rdpNgaySinh") as RadDatePicker).DbSelectedDate.ToString());
                        string cmnd = (userControl.FindControl("txtCMND") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string email = (userControl.FindControl("txtEmail") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string phone = (userControl.FindControl("txtPhone") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string username = (userControl.FindControl("txtUsername") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string password = (userControl.FindControl("txtPassword") as System.Web.UI.WebControls.TextBox).Text.Trim();
                       

                        bool active = (userControl.FindControl("chckActive") as DevExpress.Web.ASPxEditors.ASPxCheckBox).Checked;

                        string storeProc = "[usp_InsertUpdateemployee]";

                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (id > 0)
                                {
                                    cmd.Parameters.AddWithValue("@employee_id", id);
                                    cmd.Parameters.AddWithValue("@store_id", store_id);
                                    cmd.Parameters.AddWithValue("@employee_code", code);
                                    cmd.Parameters.AddWithValue("@employee_name", name);
                                    cmd.Parameters.AddWithValue("@full_name", name);
                                    cmd.Parameters.AddWithValue("@birthday", birthday);
                                    cmd.Parameters.AddWithValue("@personal_id", cmnd);
                                    cmd.Parameters.AddWithValue("@phone", phone);
                                    cmd.Parameters.AddWithValue("@email", email);
                                    cmd.Parameters.AddWithValue("@active", active);
                                    cmd.Parameters.AddWithValue("@position", position);
                                    cmd.Parameters.AddWithValue("@parent_id", parent_id);
                                    cmd.Parameters.AddWithValue("@username", username);
                                    cmd.Parameters.AddWithValue("@password", password);
                                    cmd.Parameters.AddWithValue("@role", role);
                                    cmd.Parameters.AddWithValue("@group_id", group_id);

                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }

                            string sQuery ="update employee set hr_code=N'"+hrcode+"' WHERE employee_id=" + id;
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        }
                        catch (Exception ex)
                        {
                        }

                        Refresh_Data();
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                }

                if (e.CommandName == "Delete")
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RadGrid1.DataSource = GetData();
                RadGrid1.DataBind();
                
            }
            catch (Exception ex)
            {
                
                
            }
        }
    }
}