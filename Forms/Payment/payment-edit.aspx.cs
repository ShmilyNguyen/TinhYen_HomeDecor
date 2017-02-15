using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class payment_edit : System.Web.UI.Page
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
                    Update_Header();
                    BindGrid();
                }
                else
                {
                    hdf_ID.Value = id;
                    ReloadData(id);
                }
            }
        }

        public void GenCode()
        {
            try
            {
                //Add New
                string ID = "";
                string Code = "";
                string UserID = Session["userid"].ToString();

                clsCodeMaster.GenCode("payment", UserID, out ID, out Code);
                hdf_ID.Value = ID;
                txtCode.Text = Code;

                hdf_UserID.Value = Session["userid"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public void Update_Header()
        {
            string storeProc = "[usp_InsertUpdatepayment_header]";

            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@payment_id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                        cmd.Parameters.AddWithValue("@payment_code", txtCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@payment_date", rdpNgay.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@payment_type_id", cbxPaymentType.SelectedValue);
                        cmd.Parameters.AddWithValue("@created_by", Session["userid"]);
                        cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);

                        cmd.Parameters.AddWithValue("@note", txtGhiChu.Text.Trim());

                        conn.Open();
                        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void BindList()
        {
            try
            {
                string sQuery = "";

                sQuery = @"SELECT  a.store_id ,
                                               store_name
                                        FROM    dbo.store AS a

                                                WHERE a.store_id  IN (
                                                                SELECT  store_id
                                                                FROM    dbo.fn_GetStore_By_UserID({0}) )

                                        ";

                sQuery = string.Format(sQuery, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = tb;
                cbxStore.DataBind();

                cbxStore_SelectedIndexChanged(null, null);

                sQuery = @"select * from payment_type";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxPaymentType.DataSource = tb;
                cbxPaymentType.DataBind();

                rdpNgay.DbSelectedDate = DateTime.Now;
                txtNguoiLap.Text = Session["username"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public void BindGrid()
        {
            try
            {
                string sQuery = "";

                sQuery = @"SELECT  b.saleout_code ,
                                    
                                     CONVERT(VARCHAR(20), b.trans_date_gmt , 101) AS trans_date_gmt ,
                                    b.total_amount ,
                                    a.payment_amt ,
                                    a.balance_amt ,
                                    a.row_id
                            FROM    dbo.payment_detail AS a
                                    LEFT JOIN dbo.saleout_header AS b ON a.order_id = b.saleout_id
                            WHERE   a.payment_id = {0}";

                sQuery = string.Format(sQuery, hdf_ID.Value);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadGrid1.DataSource = tb;
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
                string sQuery = @"SELECT  a.* ,
                                        b.employee_name
                                FROM    dbo.payment_header AS a
                                        LEFT JOIN dbo.employee AS b ON a.created_by = b.employee_id
                                WHERE   a.payment_id = {0}";
                sQuery = string.Format(sQuery, hdf_ID.Value);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string _id = (r["payment_id"] ?? "").ToString();
                    string _code = (r["payment_code"] ?? "").ToString();

                    string _note = (r["note"] ?? "").ToString();
                    string payment_date = (r["payment_date"] ?? "").ToString();
                    string _nguoilap = (r["employee_name"] ?? "").ToString();
                    string _payment_type_id = (r["payment_type_id"] ?? "").ToString();
                    string _store_id = (r["store_id"] ?? "").ToString();
                    string _customer_id = (r["customer_id"] ?? "").ToString();

                    hdf_ID.Value = _id;
                    txtCode.Text = _code;
                    rdpNgay.DbSelectedDate = payment_date;
                    txtNguoiLap.Text = _nguoilap;
                    txtGhiChu.Text = _note;

                    cbxStore.SelectedValue = _store_id;
                    cbxStore_SelectedIndexChanged(null, null);

                    cbxKhachHang.SelectedValue = _customer_id;
                    cbxKhachHang_SelectedIndexChanged(null, null);

                    cbxPaymentType.SelectedValue = _payment_type_id;

                    BindGrid();
                }
            }
            catch (Exception ex)
            {
            }
        }


     


        protected void cbxStore_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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
                cbxKhachHang_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxKhachHang_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string sQuery = @"SELECT  saleout_code ,
                                            saleout_id
                                    FROM    dbo.saleout_header
                                    WHERE   (is_complete_payment IS NULL
                                                OR is_complete_payment <> 1)
                                            AND is_confirmed = 1
                                            AND (is_complete_payment = 0 OR is_complete_payment is NULL)
                                            AND customer_id = {0}
                                            AND store_id = {1}
                                            AND store_id IN ( SELECT    store_id
                                                              FROM      dbo.fn_GetStore_By_UserID({2}) )";
                //SqlParameter[] arrSQLParam = new SqlParameter[1];
                //arrSQLParam[0] = new SqlParameter("@store_id", cbxStore.SelectedValue);
                sQuery = string.Format(sQuery, cbxKhachHang.SelectedValue, cbxStore.SelectedValue, Session["userid"]);
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxSoChungTu.DataSource = data;
                cbxSoChungTu.DataBind();
                cbxSoChungTu.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("payment-list.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "delete from payment_detail where payment_id={0}";
                sQuery = string.Format(sQuery, hdf_ID.Value);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "delete from payment_header where payment_id={0}";
                sQuery = string.Format(sQuery, hdf_ID.Value);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                btnExit_Click(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Header();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            try
            {

                Update_Header();

                string sQuery = "";

                decimal payment_amt = 0;
                decimal balance_amt = 0;
                decimal total_amt = 0;
               

                //Kiem tra don hang nay da co thanh toan nao hay chua

                sQuery = @"    IF EXISTS ( SELECT  order_id
                                        FROM    dbo.payment_detail
                                        WHERE   order_id = {0} ) 
                                BEGIN
                                    SELECT  1 AS result
        
                                END 
                            ELSE 
                                BEGIN 
                                    SELECT  0 AS result
                                END";

                sQuery = string.Format(sQuery,cbxSoChungTu.SelectedValue);
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();

                //Neu chua co dong thanh toan nao
                if (result == "0")
                {
                    sQuery = "select ISNULL(total_amount, 0) AS total_amount from saleout_header where saleout_id={0}";
                    sQuery = string.Format(sQuery, cbxSoChungTu.SelectedValue);
                    total_amt = decimal.Parse(SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());
                }
                else
                {
                    //Da co thanh toan don hang truoc day
                    //Lay gia tri thanh toan gan day nhat
                    sQuery = @"SELECT TOP 1
                                        balance_amt
                                FROM    dbo.payment_detail
                                WHERE   order_id = {0}
                                ORDER BY payment_id DESC";

                    sQuery = string.Format(sQuery, cbxSoChungTu.SelectedValue);
                    total_amt = decimal.Parse(SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());


                }

               
               

                payment_amt = decimal.Parse(txtAmount.Text);
              
                //Tinh thanh tien con lai
                balance_amt = total_amt - payment_amt;


                sQuery = @"INSERT INTO dbo.payment_detail
                                    ( payment_id ,
                                      order_id ,
                                      payment_amt ,
                                      balance_amt
                                    )
                            VALUES  ( {0} , -- payment_id - int
                                      {1} , -- order_id - int
                                      {2} , -- payment_amt - decimal
                                      {3}  -- balance_amt - decimal
                                    )";

                sQuery = string.Format(sQuery, hdf_ID.Value, cbxSoChungTu.SelectedValue, payment_amt, balance_amt);

                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();



                
                if (balance_amt <= 0)
                {
                    //Cap nhat trang thai cua don hang neu da thanh toan het
                    sQuery = "update payment_header set is_complete_payment = 1 where saleout_id={0}";
                    sQuery = string.Format(sQuery, cbxSoChungTu.SelectedValue);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                }
                else
                {
                    //Don hang chua hoan tat thanh toan
                     sQuery = "update payment_header set is_complete_payment = 0 where saleout_id={0}";
                    sQuery = string.Format(sQuery, cbxSoChungTu.SelectedValue);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                }
                

            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();
            string sQuery = "delete from [payment_detail] where [row_id]=" + id;
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(sQuery, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    result = Convert.ToInt32(cmd.ExecuteNonQuery());
                    conn.Close();
                    BindGrid();

                }
            }
            catch (Exception ex)
            {
            }
        }

       
    }
}