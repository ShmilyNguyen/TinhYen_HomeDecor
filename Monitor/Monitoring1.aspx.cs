using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;

namespace WKS.DMS.WEB.Monitor
{
    public partial class Monitoring1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            DataTable data = new DataTable();
            string sQuery = @"select * from store";
            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
            foreach (DataRow r in data.Rows)
            {
                string store_id = r["store_id"].ToString();

                sQuery = @"
                            DECLARE @thang INT
                            DECLARE @nam INT
                            DECLARE @store_id INT
                            DECLARE @closing_time DATETIME

                            SET @thang = 10
                            SET @nam = 2016
                            SET @store_id = "+store_id+@"

                            SELECT  @closing_time = MAX(closing_date)
                            FROM    dbo.inventory_closing_monthly
                            WHERE   data_month = @thang
                                    AND data_year = @nam
                                    AND store_id = @store_id

                            SELECT  saleout_code
                            FROM    dbo.v_SaleOut
                            WHERE   data_month = @thang
                                    AND data_year = @nam
                                    AND store_id = @store_id
                                    AND last_modified > @closing_time
                            ";

                DataTable tb = new DataTable();
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r2 in tb.Rows)
                {
                    Label1.Text = Label1.Text + "," + r2["saleout_code"].ToString();
                }

            }
        }
    }
}