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
    public partial class target_focus_item : System.Web.UI.Page
    {

        public void BindList()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"select item_id,item_code,item_code + '-' + item_name as item_name  from item";

                sQuery = string.Format(sQuery, ddlThang.SelectedValue, ddlNam.SelectedValue);
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxItem.DataSource = data;
                cbxItem.DataBind();

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
                string sQuery = @"SELECT  a.row_id,
                                            a.item_id ,
                                            b.item_code ,
                                            b.item_code + '-' + b.item_name as item_name 
                                    FROM    ( SELECT    *
                                              FROM      dbo.target_focus_item
                                              WHERE     target_month = {0}
                                                        AND target_year = {1}
                                            ) AS a
                                            LEFT JOIN item AS b ON a.item_id = b.item_id";

                sQuery = string.Format(sQuery, ddlThang.SelectedValue,ddlNam.SelectedValue);
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



                string sQuery = @"DELETE  FROM dbo.target_focus_item where row_id={0}";
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


                string sQuery = @"INSERT INTO dbo.target_focus_item
                                            ( item_id ,
                                              target_month ,
                                              target_year
                                            )
                                    VALUES  ( {0} , -- item_id - int
                                              {1} , -- target_month - int
                                              {2}  -- target_year - int
                                            )";
                sQuery = string.Format(sQuery, cbxItem.SelectedValue,ddlThang.SelectedValue,ddlNam.SelectedValue);
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