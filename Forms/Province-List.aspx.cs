using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;


namespace WKS.DMS.WEB.Forms
{
    public partial class Province_List : System.Web.UI.Page
    {
        public DataTable myData
        {
            get
            {
                DataTable data = Session["Data_Province"] as DataTable;

                data = GetData();

                return data;
            }
        }

        public DataTable GetData()
        {
            DataTable data = new DataTable();
            string sQuery = @"select * from geo_province";
            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

            return data;
        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();
                Session["Data_Province"] = data;
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
           


            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                    System.Web.UI.WebControls.TextBox txtID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtID");
                    System.Web.UI.WebControls.TextBox txtCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtCode");
                    System.Web.UI.WebControls.TextBox txtName = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtName");

                  
                


                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string id = parentItem["id"].Text;
                        string code = parentItem["code"].Text;
                        string name = parentItem["name"].Text;
                       

                        txtID.Text = id == "&nbsp;" ? "" : id;
                        txtID.Enabled = false;

                        txtCode.Text = code == "&nbsp;" ? "" : code;
                        txtCode.Enabled = false;


                        txtName.Text = name == "&nbsp;" ? "" : name;
                        


                    }
                   



                }
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
                       
                        bool is_Active = true;

                        string storeProc = "[usp_InsertUpdategeo_province]";
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (id > 0)
                                {
                                    cmd.Parameters.AddWithValue("@geo_province_id", id);
                                    cmd.Parameters.AddWithValue("@geo_province_code", code);
                                    cmd.Parameters.AddWithValue("@province_name", name);
                                    cmd.Parameters.AddWithValue("@is_Active", is_Active);
                                   


                                    conn.Open();
                                    result = Convert.ToInt32(cmd.ExecuteScalar());
                                    conn.Close();
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
                    string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["geo_province_id"].ToString();
                    string sQuery = "delete from [geo_province] where [geo_province_id]=" + id;
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