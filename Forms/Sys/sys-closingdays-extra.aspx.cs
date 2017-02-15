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
    public partial class sys_closingdays_extra : System.Web.UI.Page
    {
        public void BindList()
        {
            try
            {
              
                DataTable data = new DataTable();
                string sQuery = @"select * from store";

                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = data;
                cbxStore.DataBind();

            }
            catch (Exception ex)
            {
                throw;
            }
           
        }




        public void BindGrid()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @" SELECT * from  dbo.[sys_config_extra] as a left join dbo.[store]   on a.store_id = store.store_id  ";


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
                string store_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["store_id"].ToString();

                string sQuery = @"DELETE  FROM dbo.sys_config_extra where store_id={0}";
                sQuery = string.Format(sQuery, store_id);
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
                int saleout_date_before = int.Parse(this.txt_Saleout_before.Text.ToString().Trim());
                int saleout_date_after = int.Parse(this.txt_Saleut_after.Text.ToString().Trim());
                int return_date_before = int.Parse(this.txt_returnDate_before.Text.ToString().Trim());
                int return_date_afrer = int.Parse(this.txt_returnDate_after.Text.ToString().Trim());

                string sQuery = @"INSERT INTO dbo.sys_config_extra
                                            ( store_id , saleout_date_before , saleout_date_after ,
                                             return_date_before , return_date_after )  
                                    VALUES  ( {0} , {1}, {2} , {3} , {4}   )";

                sQuery = string.Format(sQuery, cbxStore.SelectedValue, saleout_date_before, saleout_date_after, return_date_before, return_date_afrer);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                txt_returnDate_after.Text = "";
                txt_returnDate_before.Text = "";
                txt_Saleout_before.Text = "";
                txt_Saleut_after.Text = "";

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