using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class congnodauky_list : System.Web.UI.Page
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
            DataTable data = new DataTable();
            string sQuery = @"select b.id, a.customer_id, a.customer_name , c.store_name, b.init_balance
                    from  customer as a 
	                      join customer_init_balance as b 
		                    on a.customer_id = b.customer_id
	                    left join store as c 
	                    	on c.store_id = b.store_id
                    WHERE   a.customer_id > 0
                            AND a.store_id IN ( SELECT  store_id
                            FROM    dbo.fn_GetStore_By_UserID({0}) )";

            sQuery = string.Format(sQuery, Session["userid"]);
            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

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

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource =this.myData;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id"].ToString();
            Response.Redirect("congnodauky-edit.aspx?id=" + id);
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
    }
}