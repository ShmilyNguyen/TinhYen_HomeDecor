using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;


namespace WKS.DMS.WEB.CMS
{
    public partial class cms_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();

                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    GenCode();
                }
                else
                {
                    ReloadData(id);
                }
            }
        }

        public void BindList()
        {
            try
            {
                rdpTuNgay.SelectedDate = DateTime.Now;
                rdpDenNgay.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
            }
        }

        public void GenCode()
        {
            try
            {
                string sQuery = @"select isnull(max(cms_id),0) + 1 from cms_notice";
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                txtID.Text = result;
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadData(string id)
        {
            try
            {
                string sQuery = "SELECT * from cms_notice where cms_id={0}";
                sQuery = string.Format(sQuery, id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string cms_id = r["cms_id"] == DBNull.Value ? "" : r["cms_id"].ToString();
                    string title = r["title"] == DBNull.Value ? "" : r["title"].ToString();
                    string body = r["body"] == DBNull.Value ? "" : r["body"].ToString();
                    string from = r["from"] == DBNull.Value ? "" : r["from"].ToString();
                    string to = r["to"] == DBNull.Value ? "" : r["to"].ToString();
                    string priority = r["priority"] == DBNull.Value ? "" : r["priority"].ToString();




                    txtID.Text = cms_id;
                    
                    txtTieuDe.Text = title;
                    txtNoiDung.Content = body;

                    rdpTuNgay.DbSelectedDate = from;
                    rdpDenNgay.DbSelectedDate = to;


                    ddlPriority.SelectedValue = priority;

                   

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void UpdateCMS()
        {
            try
            {
                string sQuery = @"";

                if (!string.IsNullOrEmpty(txtID.Text.Trim()))
                {


                    string storeProc = "[usp_InsertUpdate_CMS]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cms_id", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@title", txtTieuDe.Text.Trim());
                        cmd.Parameters.AddWithValue("@body", txtNoiDung.Content);
                        cmd.Parameters.AddWithValue("@from", rdpTuNgay.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@to", rdpDenNgay.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@priority", ddlPriority.SelectedValue);
                       
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        conn.Close();

                        Response.Redirect("cms-list.aspx");
                    }





                }
                

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(sQuery, conn);

                    cmd.CommandType = CommandType.Text;
                   

                    conn.Open();
                    string result = Convert.ToString(cmd.ExecuteNonQuery());

                    conn.Close();

                   
                    Response.Redirect("cms-list.aspx");
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void DeleteCMS()
        {
            try
            {
                string sQuery = @"";

                if (!string.IsNullOrEmpty(txtID.Text.Trim()))
                {
                    sQuery = @"delete from cms_notice where cms_id=" + txtID.Text.Trim();

                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(sQuery, conn);

                        cmd.CommandType = CommandType.Text;


                        conn.Open();
                        string result = Convert.ToString(cmd.ExecuteNonQuery());

                        conn.Close();


                        Response.Redirect("cms-list.aspx");
                    }
                }
               

                
            }
            catch (Exception ex)
            {
            }
        }



        

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            UpdateCMS();
            Response.Redirect("cms-list.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCMS();
            Response.Redirect("cms-list.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("cms-list.aspx");
        }

        
    }
}