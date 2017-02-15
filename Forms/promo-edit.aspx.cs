using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
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

                    string promo_type = r["promo_type"] == DBNull.Value ? "" : r["promo_type"].ToString();
                    string is_active = r["is_active"] == DBNull.Value ? "" : r["is_active"].ToString();
                    string is_approved = r["is_approved"] == DBNull.Value ? "" : r["is_approved"].ToString();

                    string target_qty = r["target_qty"] == DBNull.Value ? "0" : r["target_qty"].ToString();
                    string target_vol = r["target_vol"] == DBNull.Value ? "0" : r["target_vol"].ToString();


                    txtID.Text = promo_id;
                    txtID.Enabled = false;

                    txtCode.Text = promo_code;
                    txtCode.Enabled = false;

                    txtName.Text = promo_name;

                    rdpTuNgay.DbSelectedDate = start_date_gmt;
                    rdpDenNgay.DbSelectedDate = end_date_gmt;

                    ddlPromoType.SelectedValue = promo_type;
                    chckActive.Checked = bool.Parse(is_active);
                    chckApproved.Checked = bool.Parse(is_approved);

                    txtQty.Text = target_qty;
                    txtVol.Text = target_vol;

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
                    cmd.Parameters.AddWithValue("@promo_type", ddlPromoType.SelectedValue);
                    cmd.Parameters.AddWithValue("@is_active", chckActive.Checked);

                    conn.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    string sQuery = @"UPDATE  dbo.promotion
                                        SET     is_active = '{0}' ,
                                                is_approved = '{1}'
                                        WHERE   promo_id = {2}";
                    sQuery = string.Format(sQuery,chckActive.Checked,chckApproved.Checked,txtID.Text.Trim());
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);




                    sQuery = @"UPDATE  dbo.promotion
                                SET     target_qty = {0} ,
                                        target_vol = {1}
                                WHERE   promo_id = {2}";
                    sQuery = string.Format(sQuery,txtQty.Text,txtVol.Text,txtID.Text);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    Response.Redirect("promo-list.aspx");
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

        protected void btnRule_ChietKhau_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-discount.aspx?id=" + txtID.Text);
        }

        protected void btn_TangHang_1Level_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-item.aspx?id=" + txtID.Text);
        }

        protected void btn_TangHang_MultiLevel_Click(object sender, EventArgs e)
        {
            Response.Redirect("promo-item-combo.aspx?id=" + txtID.Text);
        }

        protected void btn_PhanBoNP_Click(object sender, EventArgs e)
        {

            Response.Redirect("promo-store.aspx?id=" + txtID.Text);

        }

       
    
    
    }


}