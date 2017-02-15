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
    public partial class sys_closingdays : System.Web.UI.Page
    {
            public void BindList()
        {
            try
            {
               

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
                string sQuery = @"SELECT * from inventory_dayofclosing";

               
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

                string sQuery = @"DELETE  FROM dbo.inventory_dayofclosing where row_id={0}";
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


                string sQuery = @"INSERT INTO dbo.inventory_dayofclosing
                                            ( dayofmonth )
                                    VALUES  ( {0}  -- dayofmonth - int
                                              )";
                sQuery = string.Format(sQuery, ddlNgay.SelectedValue);
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