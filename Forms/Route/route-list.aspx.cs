using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class route_list : System.Web.UI.Page
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
            string sQuery = @"SELECT  a.route_id ,
                                        a.route_code ,
                                        a.route_name ,
                                        c.store_id ,
                                        c.store_name
                                FROM    dbo.route AS a
                                        LEFT JOIN dbo.store AS c ON a.store_id = c.store_id
                                WHERE   a.store_id IN ( SELECT  store_id
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
            RadGrid1.DataSource = this.myData;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["route_id"].ToString();
            Response.Redirect("route-edit.aspx?id=" + id);
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