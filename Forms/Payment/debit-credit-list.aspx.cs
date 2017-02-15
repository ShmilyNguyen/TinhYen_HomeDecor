using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class debit_credit_list : System.Web.UI.Page
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
            string sQuery = @"  SELECT  a.doc_id AS posted_id ,
        a.doc_code AS posted_code ,
        a.doc_date AS posted_date ,a.original_doc_amount AS posted_amount,
        b.employee_name ,
        CASE WHEN a.doc_type = 'DM' THEN N'Nợ'
             WHEN a.doc_type = 'CM' THEN N'Có'
        END AS NoCo ,
        s.store_name ,
        c.customer_code ,
        c.customer_name
FROM    dbo.ARDoc AS a
        LEFT JOIN dbo.employee AS b ON a.created_by = b.employee_id
        LEFT JOIN dbo.store AS s ON a.store_id = s.store_id
        LEFT JOIN dbo.customer AS c ON a.customer_id = c.customer_id
WHERE   a.doc_type IN ( 'DM', 'CM' )
        AND a.customer_id > 0
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
            RadGrid1.DataSource = this.myData;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["posted_id"].ToString();
            Response.Redirect("debit-credit-edit.aspx?id=" + id);
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