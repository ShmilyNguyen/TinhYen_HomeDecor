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
    public partial class sys_account_store : System.Web.UI.Page
    {
      

        public void BindGrid()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  a.* ,
                                            b.store_code ,
                                            b.store_name ,
                                            c.employee_code ,
                                            c.employee_name
                                    FROM    dbo.sys_user_store AS a
                                            LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
                                            LEFT JOIN dbo.employee AS c ON a.user_id = c.employee_id";

                
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
                string sQuery = @"select * from employee 
                                        where 
                                        --role='USER' AND
                                         (position='DISTRIBUTOR' OR position='SUP'  OR position='ASM'  OR position='RSM'  OR position='GM' )"; 
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxEmployee.DataSource = data;
                cbxEmployee.DataBind();

                sQuery = @"select * from store";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = data;
                cbxStore.DataBind();
                
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



                string sQuery = @"DELETE  FROM dbo.sys_user_store where row_id={0}
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


                string sQuery = @"INSERT INTO dbo.sys_user_store
                                            ( user_id, store_id )
                                    VALUES  ( {0}, -- user_id - int
                                              {1}  -- store_id -int
                                              )";
                sQuery = string.Format(sQuery, cbxEmployee.SelectedValue,cbxStore.SelectedValue);
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