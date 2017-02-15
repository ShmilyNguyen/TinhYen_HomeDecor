using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class payment_list : System.Web.UI.Page
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
            string sQuery = @" SELECT  b.store_code ,
        b.store_name ,
        a.doc_id as payment_id,
        a.doc_code ,
        CONVERT(VARCHAR(20), a.doc_date, 101) AS payment_date ,
        a.note ,
        c.customer_name
FROM    dbo.ARDoc AS a
        LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
        LEFT JOIN dbo.customer AS c ON a.customer_id = c.customer_id
        LEFT JOIN ( SELECT  payment_id ,
                            COUNT(payment_id) AS rowcout
                    FROM    dbo.payment_detail
                    GROUP BY payment_id
                  ) AS xx ON a.doc_id = xx.payment_id
WHERE   a.store_id IN ( SELECT  store_id
                        FROM    dbo.fn_GetStore_By_UserID(999) )
        AND xx.rowcout > 0
                        
                        
";

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
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["payment_id"].ToString();
            Response.Redirect("payment-edit-2.aspx?id=" + id);
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