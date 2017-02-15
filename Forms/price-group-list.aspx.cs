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
    public partial class price_group_list : System.Web.UI.Page
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

            string channel_dist_id = Session["channel_dist_id"].ToString();



            DataTable data = new DataTable();
            string sQuery2 = @"SELECT * FROM dbo.price_group 
                                    WHERE channel_dist_id = '2' and  ( price_group_name LIKE N'%{0}%' 
                                    OR note LIKE N'%{0}%'
                                    OR fromdate LIKE N'%{0}%'
                                    OR todate LIKE N'%{0}%' ) ";
            if (channel_dist_id.Equals("2"))
            {
                sQuery2 = string.Format(sQuery2, txtKeyword.Text.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery2).Tables[0];
            }
            else
            {

                string sQuery = @"SELECT * FROM dbo.price_group 
                                    WHERE price_group_name LIKE N'%{0}%' 
                                    OR note LIKE N'%{0}%'
                                    OR fromdate LIKE N'%{0}%'
                                    OR todate LIKE N'%{0}%'";

                sQuery = string.Format(sQuery, txtKeyword.Text.Trim());

                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
            }
            return data;
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



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RadGrid1.DataSource = GetData();
                RadGrid1.DataBind();

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
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["price_group_id"].ToString();
            Response.Redirect("price-group-edit.aspx?id=" + id);
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {




        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
        }



        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {







        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("price-group-edit.aspx");
        }
    }
}