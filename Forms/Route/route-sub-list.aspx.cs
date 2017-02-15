using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;


namespace WKS.DMS.WEB.Forms.Route
{
    public partial class route_sub_list : System.Web.UI.Page
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
            //string id = Request.QueryString["id"];
            DataTable data = new DataTable();
            string sQuery = @"SELECT  a.* ,
                                        b.employee_name ,
                                        c.route_code ,
                                        c.route_name,
                                        s.store_name,
                                        s.store_id,
                                        s.store_code
                                FROM    dbo.route_sub_header AS a
		                                LEFT JOIN dbo.store AS s ON a.store_id = s.store_id
                                        LEFT JOIN dbo.employee AS b ON a.employee_id = b.employee_id
                                        LEFT JOIN dbo.route AS c ON a.route_id = c.route_id
                                WHERE a.store_id IN ( SELECT  store_id
                                                        FROM    dbo.fn_GetStore_By_UserID({0}) )
                                ORDER BY route_code ,
                                        route_sub_code";

            //sQuery = string.Format(sQuery, id);
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
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["route_sub_id"].ToString();
            Response.Redirect("route-sub-edit.aspx?id=" + id);
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