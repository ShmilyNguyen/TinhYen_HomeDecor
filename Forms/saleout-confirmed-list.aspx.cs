using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class saleout_confirmed_list : System.Web.UI.Page
    {
        public DataTable GetData()
        {
            DataTable data = new DataTable();
            string storeProc = "[sp_Saleout_Confirmed_Search_mt]";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                    cmd.Parameters.AddWithValue("@thang", ddlThang.SelectedValue);
                    cmd.Parameters.AddWithValue("@nam", ddlNam.SelectedValue);
                    cmd.Parameters.AddWithValue("@keyword", txtKeyword.Text.Trim());

                    cmd.CommandTimeout = 60000;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();

                    return data;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
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
            Refresh_Data();
            RadGrid1.DataSource = GetData();
            RadGrid1.DataBind();
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = GetData();
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
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
            if (e.CommandName == "View")
            {
                //   string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["saleout_id"].ToString();
                // here 
                // Response.Redirect("SaleOut-Edit-2.aspx?id=" + id);


                string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["saleout_id"].ToString();
                //Response.Redirect(_saleout_edit_url + "?id=" + id);

                //Kenh GT

                String distID = Session["channel_dist_id"].ToString();

                if (Session["channel_dist_id"].ToString().Equals("1"))
                {
                    Response.Redirect(clsCommon.UrlRoot + "Forms/saleout-edit-2.aspx" + "?id=" + id);
                }
                // Kenh MT 
                if (Session["channel_dist_id"].ToString().Equals("2"))
                {

                    Response.Redirect(clsCommon.UrlRoot + "Forms/saleout-edit-mt.aspx" + "?id=" + id);
                }



            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();
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