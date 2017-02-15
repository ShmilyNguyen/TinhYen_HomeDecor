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
    public partial class target_sellin_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();

                BindData();
            }
        }

        public void BindData()
        {
            try
            {
                
            }
            catch (Exception ex)
            {


            }
        }


        protected void btnLoadData_Click(object sender, EventArgs e)
        {

            RadGrid1_NeedDataSource(null, null);
            RadGrid1.DataBind();

        }



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

            try
            {



                string sQuery = @"SELECT  b.row_id ,
                                            a.store_id ,
                                            a.store_name ,
                                            c.region_name ,
                                            d.area_name ,
                                            {0} AS target_month ,
                                            {1} AS target_year ,
                                            ISNULL(b.target_sellin, 0) AS target_sellin
                                    FROM    dbo.store AS a
                                            LEFT JOIN ( SELECT  *
                                                        FROM    dbo.target_sellin
                                                        WHERE   target_month = {2}
                                                                AND target_year = {3}
                                                      ) AS b ON a.store_id = b.store_id
                                            LEFT JOIN dbo.region AS c ON a.region_id = c.region_id
                                            LEFT JOIN dbo.area AS d ON a.area_id = d.area_id";

                sQuery = string.Format(sQuery, ddlThang.SelectedValue, ddlNam.SelectedValue, ddlThang.SelectedValue, ddlNam.SelectedValue);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;
            }
            catch (Exception ex)
            {


            }

            return null;
        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();

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


                    TextBox txtNhaPhanPhoi = (TextBox)MyUserControl.FindControl("txtNhaPhanPhoi");
                    TextBox txtThang = (TextBox)MyUserControl.FindControl("txtThang");
                    TextBox txtNam = (TextBox)MyUserControl.FindControl("txtNam");
                    TextBox txtChiTieu = (TextBox)MyUserControl.FindControl("txtChiTieu");

                    HiddenField hdf_row_id = (HiddenField)MyUserControl.FindControl("hdf_row_id");
                    HiddenField hdf_store_id = (HiddenField)MyUserControl.FindControl("hdf_store_id");


                    //Edit
                    if (parentItem != null)
                    {

                        string row_id = parentItem["row_id"].Text;
                        string store_name = parentItem["store_name"].Text;
                        string store_id = parentItem["store_id"].Text;
                        string target_year = parentItem["target_year"].Text;
                        string target_month = parentItem["target_month"].Text;


                        string target_sellin = parentItem["target_sellin"].Text.Replace(",", "");
                        

                        hdf_row_id.Value = row_id == "&nbsp;" ? "" : row_id;
                        hdf_store_id.Value = store_id == "&nbsp;" ? "" : store_id;
                        txtNhaPhanPhoi.Text = store_name == "&nbsp;" ? "" : store_name;
                        txtThang.Text = target_month == "&nbsp;" ? "0" : target_month;
                        txtNam.Text = target_year == "&nbsp;" ? "0" : target_year;
                        txtChiTieu.Text = target_sellin == "&nbsp;" ? "0" : target_sellin;


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



                        string row_id = (userControl.FindControl("hdf_row_id") as System.Web.UI.WebControls.HiddenField).Value;
                        string store_id = (userControl.FindControl("hdf_store_id") as System.Web.UI.WebControls.HiddenField).Value;
                        string target_month = (userControl.FindControl("txtThang") as System.Web.UI.WebControls.TextBox).Text;
                        string target_year = (userControl.FindControl("txtNam") as System.Web.UI.WebControls.TextBox).Text;
                        string target_sellin = (userControl.FindControl("txtChiTieu") as System.Web.UI.WebControls.TextBox).Text.Replace(",","");


                        string storeProc = "[usp_InsertUpdatetarget_sellin]";
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@row_id", row_id);
                                cmd.Parameters.AddWithValue("@store_id", store_id);
                                cmd.Parameters.AddWithValue("@target_month", target_month);
                                cmd.Parameters.AddWithValue("@target_year", target_year);
                                cmd.Parameters.AddWithValue("@target_sellin", target_sellin);
                                


                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();
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

                }
            }
            catch (Exception ex)
            {


            }

        }
    }
}