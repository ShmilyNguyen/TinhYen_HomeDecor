using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

using WKS.DMS.WEB.Libs;


namespace WKS.DMS.WEB.Forms
{
    public partial class Store_List : System.Web.UI.Page
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
            DataTable data = new DataTable();
            string sQuery = @"SELECT A.*,
                            B.region_name AS region,
                            
                            C.area_name AS area,
                            D.province_name as province, D.geo_province_id 

                              FROM store AS A
                                   LEFT JOIN region AS B ON A.region_id = B.region_id
                                   LEFT JOIN area AS C ON A.area_id = C.area_id
                                   LEFT JOIN geo_province AS D ON A.province_id = D.geo_province_id;";
            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

            return data;
        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();
                Session["Data_Store"] = data;
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
            try
            {
                
            }
            catch (Exception ex)
            {
                
                
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

            try
            {
               

               
                    if (e.Item is GridEditFormItem && e.Item.IsInEditMode ) 
                    {
                        UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                        GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                        System.Web.UI.WebControls.TextBox txtID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtID");
                        System.Web.UI.WebControls.TextBox txtCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtCode");
                        System.Web.UI.WebControls.TextBox txtName = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtName");


                        System.Web.UI.WebControls.DropDownList ddlRegion = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlRegion");
                        System.Web.UI.WebControls.DropDownList ddlArea = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlArea");
                        System.Web.UI.WebControls.DropDownList ddlProvince = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlProvince");


                        DevExpress.Web.ASPxEditors.ASPxCheckBox chckActive = (DevExpress.Web.ASPxEditors.ASPxCheckBox)MyUserControl.FindControl("chckActive");



                        System.Web.UI.WebControls.TextBox txtDiaChi = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtDiaChi");
                        System.Web.UI.WebControls.TextBox txtMaSoThue = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtMaSoThue");
                        System.Web.UI.WebControls.TextBox txtDienThoai = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtDienThoai");

                        System.Web.UI.WebControls.TextBox txtPrefixCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtPrefixCode");


                        DataTable tblRegion = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM region").Tables[0];
                        DataTable tblArea = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM area").Tables[0];
                        DataTable tblProvince = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM geo_province").Tables[0];


                        ddlRegion.DataSource = tblRegion;
                        ddlRegion.DataBind();
                        ddlRegion.Items.Insert(0, new ListItem(string.Empty, "0"));

                        ddlArea.DataSource = tblArea;
                        ddlArea.DataBind();
                        ddlArea.Items.Insert(0, new ListItem(string.Empty, "0"));


                        ddlProvince.DataSource = tblProvince;
                        ddlProvince.DataBind();
                        ddlProvince.Items.Insert(0, new ListItem(string.Empty, "0"));



                        //Edit
                        if (parentItem != null)
                        {
                            //btnSave.CommandName = "Update";

                            string id = parentItem["id"].Text;
                            string code = parentItem["code"].Text;
                            string name = parentItem["name"].Text;
                            
                            string region_id = parentItem["region_id"].Text;
                            string area_id = parentItem["area_id"].Text;
                            string province_id = parentItem["province_id"].Text;

                            string prefix_code = parentItem["prefix_code"].Text;



                            string active = parentItem["active"].Text;

                            txtID.Text = id == "&nbsp;" ? "" : id;
                            txtID.Enabled = false;

                            txtCode.Text = code == "&nbsp;" ? "" : code;
                            txtCode.Enabled = false;


                            txtName.Text = name == "&nbsp;" ? "" : name;

                            ddlRegion.SelectedValue = region_id == "&nbsp;" ? "" : region_id;
                            ddlArea.SelectedValue = area_id == "&nbsp;" ? "" : area_id;
                            ddlProvince.SelectedValue = province_id == "&nbsp;" ? "" : province_id;

                            chckActive.Checked = bool.Parse(active);


                            string store_address = parentItem["store_address"].Text;
                            txtDiaChi.Text = store_address == "&nbsp;" ? "" : store_address;

                            string tax = parentItem["tax"].Text;
                            txtMaSoThue.Text = tax == "&nbsp;" ? "" : tax;


                            string phone = parentItem["phone"].Text;
                            txtDienThoai.Text = phone == "&nbsp;" ? "" : phone;


                            txtPrefixCode.Text = prefix_code == "&nbsp;" ? "" : prefix_code;



                        }else
                        {
                            //Add New
                            string ID = "";
                            string Code = "";
                            string UserID = Session["userid"].ToString();

                            clsCodeMaster.GenCode("Store", UserID,out ID,out Code);
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
                        string name = (userControl.FindControl("txtName") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        
                        string prefix_code = (userControl.FindControl("txtPrefixCode") as System.Web.UI.WebControls.TextBox).Text.Trim();


                        int region_id = int.Parse((userControl.FindControl("ddlRegion") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                        int area_id = int.Parse((userControl.FindControl("ddlArea") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                        int province_id = int.Parse((userControl.FindControl("ddlProvince") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                        bool active = (userControl.FindControl("chckActive") as DevExpress.Web.ASPxEditors.ASPxCheckBox).Checked;



                        string store_address = (userControl.FindControl("txtDiaChi") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string phone = (userControl.FindControl("txtDienThoai") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string tax = (userControl.FindControl("txtMaSoThue") as System.Web.UI.WebControls.TextBox).Text.Trim();


                        string storeProc = "[usp_InsertUpdatestore]";
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (id > 0)
                                {
                                    cmd.Parameters.AddWithValue("@store_id", id);
                                    cmd.Parameters.AddWithValue("@store_code", code);
                                    cmd.Parameters.AddWithValue("@store_name", name);
                                    cmd.Parameters.AddWithValue("@location", "");
                                    cmd.Parameters.AddWithValue("@region_id", region_id);
                                    cmd.Parameters.AddWithValue("@area_id", area_id);
                                    cmd.Parameters.AddWithValue("@territory_id", 0);
                                    cmd.Parameters.AddWithValue("@province_id",province_id);
                                    cmd.Parameters.AddWithValue("@is_Active", active);

                                    cmd.Parameters.AddWithValue("@store_address", store_address);
                                    cmd.Parameters.AddWithValue("@tax", tax);
                                    cmd.Parameters.AddWithValue("@contact", "");
                                    cmd.Parameters.AddWithValue("@phone", phone);



                                    conn.Open();
                                    result = Convert.ToInt32(cmd.ExecuteScalar());
                                    conn.Close();

                                    //Update Prefix Code

                                    string sQuery = "update store set prefix_code=N'{0}' where store_id={1}";
                                    sQuery = string.Format(sQuery,prefix_code,id);
                                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                                }
                            }
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
                    string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["store_id"].ToString();
                    string sQuery = "delete from [store] where [store_id]=" + id;
                    int result = 0;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(sQuery, conn);
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            result = Convert.ToInt32(cmd.ExecuteNonQuery());
                            conn.Close();
                            Refresh_Data();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }
    }
}