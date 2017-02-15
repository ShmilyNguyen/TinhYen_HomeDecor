using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class TrackingCustomerDebt : System.Web.UI.Page
    {
        public static bool release = false;
        public static double gttruocthanhtoan = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rdpNgayGiaoDich.DbSelectedDate = DateTime.Now;
                rdpNgayThuTien.DbSelectedDate = DateTime.Now;

                BindList();

                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                }
                else
                {
                    ReloadData(id);
                }

                txtTongThanhToan.Focus();
            }
        }

        public void BindList()
        {
            try
            {
                string sQuery = @"SELECT  a.store_id ,
                                               store_name
                                        FROM    dbo.store AS a

                                                WHERE a.store_id  IN (
                                                                SELECT  store_id
                                                                FROM    dbo.fn_GetStore_By_UserID({0}) )

                                        ";

                sQuery = string.Format(sQuery, Session["userid"]);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = data;
                cbxStore.DataBind();
                cbxStore.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
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
                string store_id = "0";
                string customer_id = "0";
                DateTime trans_date_gmt = DateTime.Now;
                string order_id = "0";

                string sQuery = "SELECT * FROM saleout_header where saleout_id in ( SELECT reference_id FROM TrackingCustomerDebt where doc_id=" + id + ")";
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    store_id = r["store_id"].ToString();
                    customer_id = r["customer_id"].ToString();
                    order_id = r["saleout_id"].ToString();
                    trans_date_gmt = DateTime.Parse(r["trans_date_gmt"].ToString());

                    cbxStore.SelectedValue = store_id;
                    cbxStore_SelectedIndexChanged(null, null);

                    cbxKhachHang.SelectedValue = customer_id;
                    cbxKhachHang_SelectedIndexChanged(null, null);

                    rdpNgayGiaoDich.DbSelectedDate = trans_date_gmt;
                    rdpNgayGiaoDich_SelectedDateChanged(null, null);

                    cbxPhieuXuatKho.SelectedValue = order_id;
                    cbxPhieuXuatKho_SelectedIndexChanged(null, null);
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

                double tongtien = (double)txtTongTien.Value;
                double tongthanhtoan = (double)txtTongThanhToan.Value;

                if(tongthanhtoan > tongtien)
                {
                    //Don hang da tra het
                    RadWindowManager1.RadAlert("Giá trị thanh toán lớn hơn Tổng Tiền cần thanh toán, Vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (string.IsNullOrEmpty(cbxPhieuXuatKho.SelectedValue))
                {
                    //Don hang da tra het
                    RadWindowManager1.RadAlert("Vui lòng chọn phiếu xuất !", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (cbxPhieuXuatKho.SelectedValue == "0")
                {
                    //Don hang da tra het
                    RadWindowManager1.RadAlert("Vui lòng chọn phiếu xuất !", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (release || gttruocthanhtoan == 0)
                {
                    //Don hang da tra het
                    RadWindowManager1.RadAlert("Đơn hàng này đã Thanh toán!", 330, 180, "Thông báo", null, null);
                    return;
                }

                double conlai = (double)gttruocthanhtoan - (double)txtTongThanhToan.Value;
                if (conlai <= 0)
                {
                    release = true;
                }

                string storeProc = "[sp_InsertUpdateTrackingCustomerDebt]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doc_id", lblId.Text);
                    cmd.Parameters.AddWithValue("@doc_date", rdpNgayThuTien.DbSelectedDate);
                    cmd.Parameters.AddWithValue("@doc_code", "");
                    cmd.Parameters.AddWithValue("@doc_type", "GHICO");
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@reference_id", cbxPhieuXuatKho.SelectedValue);
                    cmd.Parameters.AddWithValue("@total_amt", gttruocthanhtoan);
                    cmd.Parameters.AddWithValue("@recieve_amt", txtTongThanhToan.Value);
                    cmd.Parameters.AddWithValue("@balance_amt", conlai);
                    cmd.Parameters.AddWithValue("@release", release);
                    cmd.Parameters.AddWithValue("@note", txtDienGiai.Text);
                    cmd.Parameters.AddWithValue("@created_by", Session["userid"].ToString());

                    cmd.Parameters.AddWithValue("@order_no", lblOrderNo.Text);

                    conn.Open();
                    string result = Convert.ToString(cmd.ExecuteNonQuery());

                    //if(!string.IsNullOrEmpty(result))
                    //{
                    //    txtID.Text = result;
                    //}

                    conn.Close();




                    //Don hang da release , xac nhan don hang
                    if (release)
                    {
                        //Xac nhan don hang sau khi cong no = 0
                        int result_confirm = WKS.DMS.WEB.Libs.clsProcessOrder.Order_Confirmed(cbxPhieuXuatKho.SelectedValue, Session["userid"].ToString());
                        if (result_confirm == 1)
                        {
                            //ReloadGrid();

                            RadWindowManager1.RadAlert("Đơn hàng đã được xác nhận !", 330, 180, "Thông báo", null, null);
                        }

                        /*if (result_confirm == 0)
                        {
                            RadWindowManager1.RadAlert("Số lượng hàng tồn kho không đủ, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                        }

                        if (result_confirm == 2)
                        {
                            RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin nhân viên, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                        }


                        if (result_confirm == 3)
                        {
                            RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin khách hàng, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                        }

                        if (result_confirm == 4)
                        {
                            RadWindowManager1.RadAlert("Ngày xuất kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                        }*/

                    }

                    ReloadGrid();

                    cbxStore.SelectedIndex = -1;
                    cbxKhachHang.SelectedIndex = -1;
                    cbxPhieuXuatKho.SelectedIndex = -1;
                    lblId.Text = "0";
                    lblOrderNo.Text = "0";
                    txtDienGiai.Text = "";
                    txtKeyword.Text = "";
                    txtTongTien.Value = 0;
                    txtTongThanhToan.Value = 0;
                    txtConLai.Value = 0;


                    


                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = "";

                string sQuery = @"delete from [TrackingCustomerDebt] where doc_id={0}";

                sQuery = string.Format(sQuery, id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
            }
            catch (Exception ex)
            {
                throw;
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
            string store_id = "0";
            string customer_id = "";

            if (string.IsNullOrEmpty(cbxStore.SelectedValue))
            {
                store_id = "0";
            }
            else
            {
                store_id = cbxStore.SelectedValue;
            }

            if (string.IsNullOrEmpty(cbxKhachHang.SelectedValue))
            {
                customer_id = "0";
            }
            else
            {
                customer_id = cbxKhachHang.SelectedValue;
            }

            DataTable data = new DataTable();
            string sQuery = @"SELECT  a.* ,
                                b.store_name ,
                                c.customer_code ,
                                c.customer_name ,
                                ISNULL(add_number, '') + '-' + ISNULL(address, '') + '-'
                                + ISNULL(province, '') + '-' + ISNULL(district, '') + '-'
                                + ISNULL(ward, '') + '-' + ISNULL(street, '') AS address_full ,
                                d.saleout_code ,
                                d.trans_date_gmt
                        FROM    dbo.TrackingCustomerDebt AS a
                                LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
                                LEFT JOIN dbo.customer AS c ON a.customer_id = c.customer_id
                                LEFT JOIN dbo.saleout_header AS d ON a.reference_id = d.saleout_id
                        WHERE   a.store_id = {0}
                                AND a.customer_id = {1}
                                AND d.trans_date_numb = {3}
                                AND ( customer_code LIKE N'%{2}%'
                                      OR customer_name LIKE N'%{2}%'
                                      OR store_name LIKE N'%{2}%'
                                      OR add_number LIKE N'%{2}%'
                                      OR address LIKE N'%{2}%'
                                      OR province LIKE N'%{2}%'
                                      OR district LIKE N'%{2}%'
                                      OR ward LIKE N'%{2}%'
                                      OR street LIKE N'%{2}%'
                                    )
                        ORDER BY doc_date DESC";

            sQuery = string.Format(sQuery, store_id, customer_id, txtKeyword.Text.Trim(), clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
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

        public void Bind_Customer_All()
        {
            try
            {
                string sQuery = @"[usp_Customer_List_All]";
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@store_id", cbxStore.SelectedValue);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                cbxKhachHang.DataSource = data;
                cbxKhachHang.DataBind();

                cbxKhachHang.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxStore_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Bind_Customer_All();
            }
            catch (Exception ex)
            {
            }
        }

        public void GetSoChungTu()
        {
            //Lay danh sach don hang
            try
            {
                string sQuery = @"SELECT  saleout_id ,
                                            saleout_code
                                    FROM    dbo.saleout_header
                                    WHERE   store_id = {0}
                                            AND customer_id = {1}
                                            AND trans_date_numb = {2}";

                sQuery = string.Format(sQuery, cbxStore.SelectedValue, cbxKhachHang.SelectedValue, clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxPhieuXuatKho.DataSource = data;
                cbxPhieuXuatKho.DataBind();

                cbxPhieuXuatKho.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }

        protected void rdpNgayGiaoDich_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            GetSoChungTu();
        }

        public void GetCongNo(string store_id, string customer_id, string order_id)
        {
            try
            {
                string sQuery = @"SELECT * FROM TrackingCustomerDebt WHERE   store_id = {0}
                                            AND customer_id = {1}
                                            AND reference_id = {2}";
                sQuery = string.Format(sQuery, store_id, customer_id, order_id);
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                if (data.Rows.Count <= 1)
                {
                    //Dong dau tien - chua co phat sinh thanh toan
                    sQuery = @"SELECT  order_no ,
                                            total_amt ,
                                            recieve_amt ,
                                            balance_amt ,
                                            ISNULL(release, 0) AS release
                                    FROM    dbo.TrackingCustomerDebt
                                    WHERE   store_id = {0}
                                            AND customer_id = {1}
                                            AND reference_id = {2}
                                            AND order_no = ( SELECT MAX(order_no)
                                                             FROM   TrackingCustomerDebt
                                                             WHERE  store_id = {0}
                                                                    AND customer_id = {1}
                                                                    AND reference_id = {2}
                                                           )";
                    sQuery = string.Format(sQuery, store_id, customer_id, order_id);

                    data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    foreach (DataRow r in data.Rows)
                    {
                        int order_no = int.Parse(r["order_no"].ToString());
                        order_no = order_no + 1;
                        lblOrderNo.Text = order_no.ToString();

                        txtTongTien.Value = double.Parse(r["total_amt"].ToString());
                        gttruocthanhtoan = (double)txtTongTien.Value;

                        //Thiet lap gia tri mac dinh thanh toan
                        txtTongThanhToan.Value = (double)txtTongTien.Value;


                        release = bool.Parse(r["release"].ToString());


                        
                    }
                }
                else
                {
                    //Dong dau tien - Da co thanh toan
                    sQuery = @"SELECT  order_no ,
                                            total_amt ,
                                            recieve_amt ,
                                            balance_amt ,
                                            ISNULL(release, 0) AS release
                                    FROM    dbo.TrackingCustomerDebt
                                    WHERE   store_id = {0}
                                            AND customer_id = {1}
                                            AND reference_id = {2}
                                            AND order_no = ( SELECT MAX(order_no)
                                                             FROM   TrackingCustomerDebt
                                                             WHERE  store_id = {0}
                                                                    AND customer_id = {1}
                                                                    AND reference_id = {2}
                                                           )";
                    sQuery = string.Format(sQuery, store_id, customer_id, order_id);

                    data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    foreach (DataRow r in data.Rows)
                    {
                        int order_no = int.Parse(r["order_no"].ToString());
                        order_no = order_no + 1;
                        lblOrderNo.Text = order_no.ToString();

                        txtTongTien.Value = double.Parse(r["balance_amt"].ToString());
                        gttruocthanhtoan = (double)txtTongTien.Value;

                        //Thiet lap gia tri mac dinh thanh toan
                        txtTongThanhToan.Value = (double)txtTongTien.Value;


                        release = bool.Parse(r["release"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxPhieuXuatKho_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //Lay thong tin cong no hien tai cua don hang
                GetCongNo(cbxStore.SelectedValue, cbxKhachHang.SelectedValue, cbxPhieuXuatKho.SelectedValue);
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxKhachHang_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetSoChungTu();
            ReloadGrid();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Forms/saleout-list.aspx");
        }
    }
}