using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Sys
{
    public partial class sys_user_price : System.Web.UI.Page
    {

        public void BindGrid()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  a.* ,
                                                b.employee_code ,
                                                b.employee_name ,
                                                c.description
                                        FROM    dbo.sys_user_price AS a
                                                LEFT JOIN dbo.employee AS b ON a.user_id = b.employee_id
                                                LEFT JOIN dbo.item_price_policy AS c ON a.price_type_id = c.item_price_policy_id";


                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadGrid1.DataSource = data;
                RadGrid1.DataBind();






            }
            catch (Exception ex)
            {


            }
        }

        public void BindControls()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"select * from employee where role='USER' AND position='DISTRIBUTOR' ";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxEmployee.DataSource = data;
                cbxEmployee.DataBind();

                sQuery = @"select * from item_price_policy";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxPrice.DataSource = data;
                cbxPrice.DataBind();

            }
            catch (Exception ex)
            {


            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindControls();
                BindGrid();
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();



                string sQuery = @"DELETE  FROM dbo.sys_user_price where row_id={0}
                                    ";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }



        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            //Update Data

            try
            {


                string sQuery = @"INSERT INTO dbo.sys_user_price
                                            ( user_id, price_type_id )
                                    VALUES  ( {0}, -- user_id - int
                                              {1}  
                                              )";
                sQuery = string.Format(sQuery, cbxEmployee.SelectedValue, cbxPrice.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}