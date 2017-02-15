using DevExpress.XtraPrinting;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class saleout_return_edit : System.Web.UI.Page
    {
        string store_id = "";
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
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * from v_SaleOut where SaleOut_id='" + hdf_SaleOut_ID.Value + "' and saleout_type='HB'";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;
            }
            catch (Exception)
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

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.myData;
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
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.Enabled = false;

                rdpNgayTraDonHang.DbSelectedDate = DateTime.Now;
                string id = Request.QueryString["id"].ToString();
                ReloadData(id);
            }
        }

        public void ReloadData(string id)
        {
            try
            {
                string sQuery = "SELECT TOP 1 * from v_SaleOut where SaleOut_id=" + id;
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string SaleOut_id = (r["SaleOut_id"] ?? "").ToString();
                    string SaleOut_code = (r["SaleOut_code"] ?? "").ToString();
                    string store_name = (r["store_name"] ?? "").ToString();
                    string route_code = (r["route_code"] ?? "").ToString();
                     store_id = (r["store_id"] ?? "").ToString();

                    string employee_name = (r["employee_name"] ?? "").ToString();

                    string customer_name = (r["customer_name"] ?? "").ToString();

                    string address = (r["address"] ?? "").ToString();

                    string trans_date_gmt = (r["trans_date_gmt"] ?? "").ToString();
                    string trans_date_numb = (r["trans_date_numb"] ?? "").ToString();
                    string created_by = (r["created_by"] ?? "").ToString();

                    string created_by_name = (r["created_by_name"] ?? "").ToString();

                    string last_modified = (r["last_modified"] ?? "").ToString();

                    string item_price_code = (r["item_price_code"] ?? "").ToString();

                    hdf_SaleOut_ID.Value = SaleOut_id;
                    hdf_User_ID.Value = Session["userid"].ToString();

                    lblSoChungTu.Text = SaleOut_code;
                    lblNgay.Text = trans_date_gmt;
                    lblNPP.Text = store_name;
                    lblTuyenThu.Text = route_code;
                    lblNhanVien.Text = employee_name;
                    lblKhachHang.Text = customer_name;
                    lblDiaChiKhachHang.Text = address;

                    ReloadGrid();

                    if (SaleOut_code.Contains("TH-"))
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadGrid()
        {
            Refresh_Data();
            RadGrid1.DataSource = this.myData;
            RadGrid1.DataBind();

            Reload_XuatKM();

            UpdateThanhTien();
        }

        public void Reload_XuatKM()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * from v_SaleOut where SaleOut_id='" + hdf_SaleOut_ID.Value + "' and saleout_type='KM'";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                RadGrid2.DataSource = data;
                RadGrid2.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateThanhTien()
        {
            string congthuc_tinhthanhtien = "CKONTOP_SAU_CKLINEHANG";

            try
            {
                string sQuery = "";
                if (congthuc_tinhthanhtien == "CKONTOP_SAU_CKLINEHANG")
                {
                    sQuery = @"[usp_saleout_gettotalvalues]";
                }

                DataTable data = new DataTable();
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@saleout_id", hdf_SaleOut_ID.Value);
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                foreach (DataRow r in data.Rows)
                {
                    CultureInfo us = new CultureInfo("en-US");
                    txtThanhTien.Text = r["GTBan"].ToString();
                    txtGTCK.Text = r["GTChietKhauDongHang"].ToString();
                    txtOntopDiscount.Text = r["OntopDiscount"].ToString();
                    txtTotalOntopDiscount.Text = r["GTChietKhauNPP"].ToString();
                    txtGTThanhToan.Text = r["GTThanhToan"].ToString();
                }

                //Update thanh tien vao Don Hang
                //Libs.clsProcessOrder.Order_UpdateAmount(hdf_SaleOut_ID.Value, decimal.Parse(txtGTThanhToan.Text));
            }
            catch (Exception ex)
            {
            }
        }


        public bool Check_Valid_LockDate()
        {
            try
            {
                string id = Request.QueryString["id"].ToString();
                ReloadData(id);



                string storeProc = "[sp_sys_config_check_lockdate_return_order]";
                string storeProc2 = "[sp_sys_config_check_lockdate_return_order_extra]";
                string storeProc3 = "[sp_sys_config_check_lockdate_return_order_by_PG]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    SqlCommand cmd2 = new SqlCommand(storeProc2, conn);
                    SqlCommand cmd3 = new SqlCommand(storeProc3, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@trans_date_gmt", rdpNgayTraDonHang.DbSelectedDate);

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@trans_date_gmt", rdpNgayTraDonHang.DbSelectedDate);
                    cmd2.Parameters.AddWithValue("@store_id", store_id);

                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@saleout_id", id );
                    cmd3.Parameters.AddWithValue("@trans_date_gmt", rdpNgayTraDonHang.DbSelectedDate);

                    conn.Open();
                    string flag = Convert.ToString(cmd.ExecuteScalar());
                    string flag2 = Convert.ToString(cmd2.ExecuteScalar());
                    string flag3 = Convert.ToString(cmd3.ExecuteScalar());
                    conn.Close();


                    if(bool.Parse(flag) || bool.Parse(flag2))
                    {

                        return true;
                    }
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool KiemTraDonHang_DaTra(string order_id)
        {
            try
            {
                string sQuery = @"IF EXISTS ( SELECT  parent_id
                                                FROM    dbo.saleout_header
                                                WHERE   is_returned_order = 1
                                                        AND parent_id = {0} ) 
                                        SELECT  'true' 
                                    ELSE 
                                        SELECT  'false'";
                sQuery = string.Format(sQuery,order_id);
                bool ret = bool.Parse( SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());


                return ret;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiem tra co nhap thong tin
                if (string.IsNullOrEmpty(txtLyDoTraHang.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập lý do trả hàng !", 330, 180, "Thông báo", null, null);
                    return;
                }


                if (rdpNgayTraDonHang.SelectedDate == null )
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập ngày trả hàng !", 330, 180, "Thông báo", null, null);
                    return;
                }


                

                
                //Kiem tra gioi han ngay cho phep tra don hang
                if (!Check_Valid_LockDate())
                {
                    RadWindowManager1.RadAlert("Ngày trả hàng vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                    return;
                }

                //Kiem tra don hang nay da duoc tra hay chua
                //Tam thoi khong kiem tra
                //if (KiemTraDonHang_DaTra(hdf_SaleOut_ID.Value.ToString()))
                //{
                //    RadWindowManager1.RadAlert("Đơn hàng đã trả , Bạn không thể trả lần thứ 2 , vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                //    return;
                //}

                //Tra don hang

                bool ret = Libs.clsProcessOrder.Order_Returned(hdf_SaleOut_ID.Value.ToString(), hdf_User_ID.Value.ToString(), rdpNgayTraDonHang.SelectedDate.Value, txtLyDoTraHang.Text.Trim());

                if (ret)
                {

                  

                    RadWindowManager1.RadAlert("Trả đơn hàng thành công, vui lòng tạo đơn hàng mới để điều chỉnh !", 330, 180, "Thông báo", null, null);
                    //Response.Redirect("saleout-edit-2.aspx");
                    btnSave.Enabled = false;
                }
                else
                {
                    RadWindowManager1.RadAlert("Trả đơn hàng bị lỗi, vui lòng liên hệ IT để được hỗ trợ !", 330, 180, "Thông báo", null, null);
                }
            }
            catch (Exception ex)
            {
                RadWindowManager1.RadAlert("Trả đơn hàng bị lỗi, vui lòng liên hệ IT để được hỗ trợ !", 330, 180, "Thông báo", null, null);
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                
                Response.Redirect("saleout-confirmed-list.aspx");
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"];

                string sQuery = "delete from saleout_detail where saleout_id={0}";
                sQuery = string.Format(sQuery,id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "delete from saleout_header where saleout_id={0}";
                sQuery = string.Format(sQuery, id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                Response.Redirect("saleout-confirmed-list.aspx");

            }
            catch (Exception  ex)
            {

                
            }
        }
    }
}