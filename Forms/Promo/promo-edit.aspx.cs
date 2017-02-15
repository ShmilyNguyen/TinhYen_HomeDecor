using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;
namespace WKS.DMS.WEB.Forms.Promo
{
    public partial class promo_edit : System.Web.UI.Page
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
                string sQuery = @"select isnull(max(promo_id),0) + 1 from promotion";
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
                string sQuery = "SELECT * from promotion where promo_id={0}";
                sQuery = string.Format(sQuery, id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string promo_id = r["promo_id"] == DBNull.Value ? "" : r["promo_id"].ToString();
                    string promo_code = r["promo_code"] == DBNull.Value ? "" : r["promo_code"].ToString();
                    string promo_name = r["promo_name"] == DBNull.Value ? "" : r["promo_name"].ToString();
                    string start_date_gmt = r["start_date_gmt"] == DBNull.Value ? "" : r["start_date_gmt"].ToString();
                    string end_date_gmt = r["end_date_gmt"] == DBNull.Value ? "" : r["end_date_gmt"].ToString();

                    
                    string is_active = r["is_active"] == DBNull.Value ? "" : r["is_active"].ToString();
                    string is_approved = r["is_approved"] == DBNull.Value ? "" : r["is_approved"].ToString();

                    string target_qty = r["target_qty"] == DBNull.Value ? "0" : r["target_qty"].ToString();
                    string target_vol = r["target_vol"] == DBNull.Value ? "0" : r["target_vol"].ToString();




                    txtID.Text = promo_id;
                    txtID.Enabled = false;

                    txtCode.Text = promo_code;
                    //txtCode.Enabled = false;

                    txtName.Text = promo_name;

                    rdpTuNgay.DbSelectedDate = start_date_gmt;
                    rdpDenNgay.DbSelectedDate = end_date_gmt;


                    string promo_type = r["promo_type"] == DBNull.Value ? "" : r["promo_type"].ToString();
                    string promo_level = r["promo_level"] == DBNull.Value ? "" : r["promo_level"].ToString();
                    string promo_apply = r["promo_apply"] == DBNull.Value ? "" : r["promo_apply"].ToString();
                    string promo_status = r["promo_status"] == DBNull.Value ? "" : r["promo_status"].ToString();
                    

                    ddlPromoType.SelectedValue = promo_type;
                    ddlPromoLevel.SelectedValue = promo_level;
                    ddlPromoApply.SelectedValue = promo_apply;
                    ddlPromoStatus.SelectedValue = promo_status;



                  

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string storeProc = "[usp_InsertUpdatepromotion]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@promo_id", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@promo_code", txtCode.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@promo_name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@start_date_gmt", rdpTuNgay.DbSelectedDate);
                    cmd.Parameters.AddWithValue("@end_date_gmt", rdpDenNgay.DbSelectedDate);
                    cmd.Parameters.AddWithValue("@start_date_numb", clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value));
                    cmd.Parameters.AddWithValue("@end_date_numb", clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value));
                    cmd.Parameters.AddWithValue("@total_qty", 0);
                    
                    cmd.Parameters.AddWithValue("@is_active", 1);

                    cmd.Parameters.AddWithValue("@is_publish", 0);
                    cmd.Parameters.AddWithValue("@is_approved", 0);


                    cmd.Parameters.AddWithValue("@promo_type", ddlPromoType.SelectedValue);
                    cmd.Parameters.AddWithValue("@promo_apply", ddlPromoApply.SelectedValue);
                    cmd.Parameters.AddWithValue("@promo_level", ddlPromoLevel.SelectedValue);
                    cmd.Parameters.AddWithValue("@promo_status", ddlPromoStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@target_vol", 0);
                    cmd.Parameters.AddWithValue("@target_qty", 0);


                    conn.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                   


                   
                }
            }
            catch (Exception ex)
            {
            }
        }






        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-list.aspx");
        }

    

      
        protected void btnRule1_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-rule-1.aspx?id=" + txtID.Text);
        }

        protected void btn_PhanBoNP_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-store.aspx?id=" + txtID.Text);
        }

        protected void btnRule_Discount_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-discount.aspx?id=" + txtID.Text);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string promo_id = txtID.Text;

                string sQuery = @"DELETE  FROM dbo.promotion
                                    WHERE   promo_id = {0}

                                    DELETE  FROM dbo.promotion_item_1
                                    WHERE   promo_id = {0}

                                    DELETE  FROM dbo.promotion_item_des
                                    WHERE   promo_id = {0}

                                    DELETE  FROM dbo.promotion_item_discount
                                    WHERE   promo_id = {0}

                                   
                                    DELETE  FROM dbo.promotion_rule_des1
                                    WHERE   promo_id = {0}

                                    DELETE  FROM dbo.promotion_rule_src1
                                    WHERE   promo_id = {0}

                                    DELETE  FROM dbo.promotion_store
                                    WHERE   promo_id = {0}";

                sQuery = string.Format(sQuery,promo_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                Response.Redirect("promo-list.aspx");

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}