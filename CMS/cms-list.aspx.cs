using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.CMS
{
    public partial class cms_list : System.Web.UI.Page
    {
        public DataTable GetData()
        {
            DataTable data = new DataTable();
            string storeProc = "select * from cms_notice";
            //Show All
            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
            {
                SqlCommand cmd = new SqlCommand(storeProc, conn);
                cmd.CommandType = CommandType.Text;

             
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                conn.Close();

                return data;
            }
        }

        public void ShowHideControls()
        {
            try
            {
                string position = Session["position"].ToString();
                if (position.Contains("ADMIN"))
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public void ReloadGrid()
        {
            RadGrid1.DataSource = GetData();
            RadGrid1.DataBind();
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = GetData();
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cms_id"].ToString();
            Response.Redirect("cms-edit.aspx?id=" + id);
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
            //GridHeaderItem headerItem = e.Item as GridHeaderItem;
            //if (headerItem != null)
            //{
            //    headerItem["EditColumn"].Text = string.Empty;

            //}
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowHideControls();
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            ReloadGrid();
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
    }
}