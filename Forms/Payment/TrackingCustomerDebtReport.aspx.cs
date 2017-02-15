using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class TrackingCustomerDebtReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        public void ReloadGrid()
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

        public void ReloadData(string id)

        {
            try
            {
            }
            catch (Exception ex)
            {
            }
        }

        #region bang gia

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
            string sQuery = @"[sp_trackingcustomerdebt_list]";
            SqlParameter[] arrSQLParam = new SqlParameter[2];
            arrSQLParam[0] = new SqlParameter("@user_id", Session["userid"]);
            arrSQLParam[1] = new SqlParameter("@keyword", txtKeyword.Text.Trim());

            DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];

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
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["doc_id"].ToString();
            Response.Redirect("TrackingCustomerDebt.aspx?id=" + id);
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

        #endregion bang gia
    }
}