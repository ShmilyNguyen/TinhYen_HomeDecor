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
    public partial class target_saleout_list : System.Web.UI.Page
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
                string sQuery = "";

                sQuery = @"SELECT  a.store_id ,
                                                a.store_name + ' (' + ISNULL(a.location,'') + ')' AS store_name
                                        FROM    dbo.store AS a
                                                
                                        where store_id in (SELECT    store_id
                                                                        FROM      dbo.fn_GetStore_By_UserID(" + Session["userid"] + "))";
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxNhaPhanPhoi.DataSource = tb;
                cbxNhaPhanPhoi.DataBind();
                cbxNhaPhanPhoi.Items.Insert(0, new RadComboBoxItem(null, null));
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
                                            a.employee_id ,
                                            a.store_id ,
                                            a.employee_name ,
                                            b.target_saleout ,
                                            b.target_order,
                                            b.target_focus_saleout,
                                            b.target_focus_order,
                                            b.target_active_outlet,
                                            {0} AS target_month ,
                                            {1} AS target_year
                                    FROM    (select * from dbo.employee where position='SR') AS a
                                            LEFT JOIN ( SELECT  *
                                                        FROM    dbo.target_saleout
                                                        WHERE   target_month = {2}
                                                                AND target_year = {3}
                                                      ) AS b ON a.employee_id = b.employee_id
                                    WHERE   a.store_id = {4}";

                sQuery = string.Format(sQuery, ddlThang.SelectedValue, ddlNam.SelectedValue, ddlThang.SelectedValue, ddlNam.SelectedValue,cbxNhaPhanPhoi.SelectedValue);

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
                    TextBox txtNhanVien = (TextBox)MyUserControl.FindControl("txtNhanVien");
                    TextBox txtThang = (TextBox)MyUserControl.FindControl("txtThang");
                    TextBox txtNam = (TextBox)MyUserControl.FindControl("txtNam");

                    TextBox txtTargetSaleout = (TextBox)MyUserControl.FindControl("txtTargetSaleout");
                    TextBox txtTargetOrder = (TextBox)MyUserControl.FindControl("txtTargetOrder");
                    TextBox txtTargetFocusSaleout = (TextBox)MyUserControl.FindControl("txtTargetFocusSaleout");
                    TextBox txtTargetFocusOrder = (TextBox)MyUserControl.FindControl("txtTargetFocusOrder");
                    TextBox txtTargetActiveOutlet = (TextBox)MyUserControl.FindControl("txtTargetActiveOutlet");




                    HiddenField hdf_row_id = (HiddenField)MyUserControl.FindControl("hdf_row_id");
                    HiddenField hdf_store_id = (HiddenField)MyUserControl.FindControl("hdf_store_id");
                    HiddenField hdf_employee_id = (HiddenField)MyUserControl.FindControl("hdf_employee_id");


                    //Edit
                    if (parentItem != null)
                    {

                        string row_id = parentItem["row_id"].Text;
                        string store_name = cbxNhaPhanPhoi.Text;
                        string store_id = parentItem["store_id"].Text;
                        string employee_id = parentItem["employee_id"].Text;
                        string target_year = parentItem["target_year"].Text;
                        string target_month = parentItem["target_month"].Text;


                        string target_saleout = parentItem["target_saleout"].Text.Replace(",", "");
                        string target_order = parentItem["target_order"].Text.Replace(",", "");
                        string target_focus_saleout = parentItem["target_focus_saleout"].Text.Replace(",", "");
                        string target_focus_order = parentItem["target_focus_order"].Text.Replace(",", "");
                        string target_active_outlet = parentItem["target_active_outlet"].Text.Replace(",", "");


                        hdf_row_id.Value = row_id == "&nbsp;" ? "" : row_id;
                        hdf_store_id.Value = store_id == "&nbsp;" ? "" : store_id;
                        hdf_employee_id.Value = employee_id == "&nbsp;" ? "" : employee_id;


                        txtNhaPhanPhoi.Text = store_name == "&nbsp;" ? "" : store_name;
                        txtThang.Text = target_month == "&nbsp;" ? "0" : target_month;
                        txtNam.Text = target_year == "&nbsp;" ? "0" : target_year;

                        txtTargetSaleout.Text = target_saleout == "&nbsp;" ? "0" : target_saleout;
                        txtTargetOrder.Text = target_order == "&nbsp;" ? "0" : target_order;
                        txtTargetFocusSaleout.Text = target_focus_saleout == "&nbsp;" ? "0" : target_focus_saleout;
                        txtTargetFocusOrder.Text = target_focus_order == "&nbsp;" ? "0" : target_focus_order;
                        txtTargetActiveOutlet.Text = target_active_outlet == "&nbsp;" ? "0" : target_active_outlet;


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
                        string employee_id = (userControl.FindControl("hdf_employee_id") as System.Web.UI.WebControls.HiddenField).Value;
                        string target_month = (userControl.FindControl("txtThang") as System.Web.UI.WebControls.TextBox).Text;
                        string target_year = (userControl.FindControl("txtNam") as System.Web.UI.WebControls.TextBox).Text;

                        string target_saleout = (userControl.FindControl("txtTargetSaleout") as System.Web.UI.WebControls.TextBox).Text.Replace(",", "");
                        string target_order = (userControl.FindControl("txtTargetOrder") as System.Web.UI.WebControls.TextBox).Text.Replace(",", "");
                        string target_focus_saleout = (userControl.FindControl("txtTargetFocusSaleout") as System.Web.UI.WebControls.TextBox).Text.Replace(",", "");
                        string target_focus_order = (userControl.FindControl("txtTargetFocusOrder") as System.Web.UI.WebControls.TextBox).Text.Replace(",", "");
                        string target_active_outlet = (userControl.FindControl("txtTargetActiveOutlet") as System.Web.UI.WebControls.TextBox).Text.Replace(",", "");


                        string storeProc = "[usp_InsertUpdatetarget_saleout]";
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@row_id", row_id);
                                cmd.Parameters.AddWithValue("@store_id", store_id);
                                cmd.Parameters.AddWithValue("@employee_id", employee_id);
                                cmd.Parameters.AddWithValue("@target_month", target_month);
                                cmd.Parameters.AddWithValue("@target_year", target_year);

                                cmd.Parameters.AddWithValue("@target_saleout", target_saleout);
                                cmd.Parameters.AddWithValue("@target_order", target_order);
                                cmd.Parameters.AddWithValue("@target_focus_saleout", target_focus_saleout);
                                cmd.Parameters.AddWithValue("@target_focus_order", target_focus_order);
                                cmd.Parameters.AddWithValue("@target_active_outlet", target_active_outlet);

                                cmd.Parameters.AddWithValue("@working_day", 0);
                                cmd.Parameters.AddWithValue("@target_saleout_agv", 0);
                                cmd.Parameters.AddWithValue("@target_day", 0);


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