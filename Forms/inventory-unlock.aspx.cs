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


namespace WKS.DMS.WEB.Forms
{
    public partial class inventory_unlock : System.Web.UI.Page
    {
        public void BindList()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"select * from store  ";

                
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = data;
                cbxStore.DataBind();

            }
            catch (Exception ex)
            {


            }
        }




        public void BindGrid()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  a.row_id,a.user_id,a.store_id,b.employee_name,c.store_name
                                   
                                    FROM    dbo.inventory_closing_unlock AS a
                                            LEFT JOIN dbo.employee AS b ON a.user_id = b.employee_id
                                            LEFT JOIN dbo.store as c on a.store_id = c.store_id";

               
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadGrid1.DataSource = data;
                RadGrid1.DataBind();






            }
            catch (Exception ex)
            {


            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = (DateTime.Now.Month-1).ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();


                BindList();
                BindGrid();
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();





                string sQuery = @"UPDATE  dbo.inventory_closing_monthly
                                        SET     is_lock = 1
                                        FROM    dbo.inventory_closing_monthly
                                        WHERE   store_id = ( SELECT  [store_id]
                                                            FROM    dbo.inventory_closing_unlock
                                                            WHERE   row_id = {0}
                                                          )";

                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);



                 sQuery = @"DELETE  FROM dbo.inventory_closing_unlock where row_id={0}";
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


                string sQuery = @"INSERT INTO dbo.inventory_closing_unlock
                                            ( store_id )
                                    VALUES  ( 
                                              {0}  -- user_id - int
                                              )";
                sQuery = string.Format(sQuery, cbxStore.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                sQuery = @"DELETE FROM  dbo.inventory_closing_monthly
                                
                                WHERE   store_id = {0}
                                        AND data_month = {1}
                                        AND data_year = {2}";

                sQuery = string.Format(sQuery, cbxStore.SelectedValue, ddlThang.SelectedValue,ddlNam.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }


        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}