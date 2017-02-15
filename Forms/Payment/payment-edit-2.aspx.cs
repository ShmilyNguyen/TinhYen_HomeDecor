using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class payment_edit_2 : System.Web.UI.Page
    {
        public DataTable myData_Temp
        {
            get
            {
                DataTable data = GetData_Temp();

                return data;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                BindList();
                Delete_TempData();
                string id = Request.QueryString["id"];

                if (string.IsNullOrEmpty(id))
                {
                    GenCode();
                    Update_Header();
                    BindGrid_Temp();
                    
                }
                else
                {
                    hdf_ID.Value = id;
                    ReloadData(id);
                    Disable_Controls(true);
                }
            }
        }


        public void Disable_Controls(bool is_readonly)
        {
            try
            {

                if (is_readonly)
                {

                    

                    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;

                }

                
            }
            catch (Exception ex)
            {


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

                clsCodeMaster.GenCode("ardoc-pa", UserID, out ID, out Code);
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

        //public void BindGrid()
        //{
        //    RadGrid1.DataSource = GetData();
        //    RadGrid1.DataBind();
        //}

        public void BindGrid_Temp()
        {
            RadGrid1.DataSource = GetData_Temp();
            RadGrid1.DataBind();
        }

        public void Update_DuNo()
        {
            try
            {
                string sQuery = "";

                sQuery = @"SELECT  SUM(current_doc_balance) AS current_doc_balance
                            FROM    dbo.ARDoc
                            WHERE   doc_type IN ( 'IN', 'DM' )
                                    AND current_doc_balance <> 0
                                    AND store_id = {0}
                                    AND customer_id = {1}";
                sQuery = string.Format(sQuery, cbxStore.SelectedValue, cbxKhachHang.SelectedValue);
                string sCongNoHienTai = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                txtCongNoHienTai.Text = sCongNoHienTai;


//                sQuery = @"SELECT  SUM(( CASE WHEN debit_credit = 0 THEN 1
//                                               WHEN debit_credit IS NULL THEN 1
//                                               ELSE -1
//                                          END ) * ISNULL(posted_amount, 0)) AS posted_amount
//                            FROM    dbo.debit_credit
//
//                            WHERE   is_completed <> 1
//                                    AND store_id = {0}
//                                    AND customer_id = {1}                                  
//                                    AND store_id IN ( SELECT    store_id
//                                                      FROM      dbo.fn_GetStore_By_UserID({2}) )
//                                                      ";
//                sQuery = string.Format(sQuery, cbxStore.SelectedValue, cbxKhachHang.SelectedValue, Session["userid"]);
//                string sNoCo = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();            





                sQuery = @"SELECT  ISNULL(SUM(payment_amt),0) AS payment_amt
                            FROM    dbo.payment_detail_temp where user_id={0}";


                sQuery = string.Format(sQuery,  Session["userid"]);
                string sThanhToan = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                txtGiaTri.Text = sThanhToan;



                decimal SoDuConLai = decimal.Parse(sCongNoHienTai) - decimal.Parse(sThanhToan);

                txtCongNoHienTai.Text = SoDuConLai.ToString();



                




                string sGiaTriCongNo = Get_Posted_Amount(cbxSoCT.SelectedValue);

                //Gia tri thanh toan con lai
                if (sThanhToan =="0")
                {
                    cbxSoCT_SelectedIndexChanged(null, null);
                }
                else
                {
                    decimal GTThanhToanConLai = decimal.Parse(sGiaTriCongNo) - decimal.Parse(sThanhToan);
                    txtGiaTri.Text = GTThanhToanConLai.ToString();
                }


            }
            catch (Exception ex)
            {
                
                
            }
        }


        

        public DataTable GetData_Temp()
        {
            try
            {
                string sQuery = "";

//                sQuery = @"SELECT  payment_id ,
//                                    saleout_code ,
//                                    saleout_id ,
//                                    CONVERT(VARCHAR(20), trans_date_gmt, 101) AS trans_date_gmt ,
//                                    ISNULL( current_balance,0) as begin_balance ,
//                                    ISNULL(payment_amt, 0) AS payment_amt ,
//                                    ISNULL(ending_balance, 0) AS ending_balance ,
//                                    b.row_id
//                            FROM    dbo.saleout_header AS a
//                                    LEFT JOIN ( SELECT  *
//                                                FROM    dbo.payment_detail_temp
//                                                WHERE   payment_id = {2}
//                                              ) AS b ON a.saleout_id = b.order_id
//                            WHERE   store_id = {0}
//                                    AND customer_id = {1}
//                                   
//                                    --AND ( current_balance <> 0
//                                          --OR current_balance IS NULL
//                                        --)
//                                    AND store_id IN ( SELECT    store_id
//                                                      FROM      dbo.fn_GetStore_By_UserID({3}) )
//                                                      ";


//                sQuery = @"SELECT  payment_id ,
//        saleout_code ,
//        a.saleout_id ,
//        a.is_custom_debit ,
//        CONVERT(VARCHAR(20), trans_date_gmt, 101) AS trans_date_gmt ,
//        ISNULL(current_balance, 0) AS begin_balance ,
//        ISNULL(payment_amt, 0) AS payment_amt ,
//        ISNULL(ending_balance, 0) AS ending_balance ,
//        b.row_id
//FROM    ( SELECT    saleout_code ,
//                    saleout_id ,
//                    trans_date_gmt ,
//                    current_balance ,
//                    0 AS is_custom_debit
//          FROM      dbo.saleout_header
//          WHERE     is_confirmed = 1
//                    AND is_returned <> 1
//                    AND current_balance <> 0
//                    AND store_id = {0}
//                    AND customer_id = {1}
//                    AND store_id IN ( SELECT    store_id
//                                      FROM      dbo.fn_GetStore_By_UserID({2}) )
//          UNION ALL
//          SELECT    posted_code AS saleout_code ,
//                    posted_id AS saleout_id ,
//                    posted_date AS trans_date_gmt ,
//                    current_balance ,
//                    1 AS is_custom_debit
//          FROM      dbo.debit_credit
//          WHERE     debit_credit = 0
//                    AND is_completed <> 1
//                    AND current_balance <> 0
//                    AND store_id IN ( SELECT    store_id
//                                      FROM      dbo.fn_GetStore_By_UserID({3}) )
//        ) AS a
//        LEFT JOIN ( SELECT  *
//                    FROM    dbo.payment_detail_temp
//                    WHERE   payment_id = {4}
//                  ) AS b ON a.saleout_id = b.order_id
//        LEFT JOIN ( SELECT  saleout_id ,
//                            COUNT(row_id) AS row_count
//                    FROM    dbo.saleout_detail
//                    GROUP BY saleout_id
//                  ) AS c ON a.saleout_id = c.saleout_id
//";



                sQuery = @"SELECT   a.doc_id ,
a.saleout_id,
        a.saleout_code ,
        a.is_custom_debit ,
        CONVERT(VARCHAR(20), trans_date_gmt, 101) AS trans_date_gmt ,
        ISNULL(a.current_balance, 0) AS begin_balance ,
        ISNULL(b.payment_amt, 0) AS payment_amt ,
        ISNULL(b.ending_balance, 0) AS ending_balance ,
        b.row_id
FROM    ( 

--Don Hang
          SELECT    a1.doc_id AS doc_id ,
                    b1.saleout_id,
                    b1.saleout_code AS saleout_code ,
                    b1.trans_date_gmt ,
                    a1.doc_type ,
                    0 AS is_custom_debit,
                    a1.current_doc_balance AS current_balance ,
                    a1.customer_id ,
                    a1.store_id
          FROM      dbo.ARDoc AS a1
                    LEFT JOIN dbo.saleout_header AS b1 ON a1.reference_id = b1.saleout_id
          WHERE     doc_type = 'IN'
                    AND a1.current_doc_balance <> 0
                    AND a1.store_id = {0}
                    AND a1.customer_id = {1}
                    
--Debit
          UNION ALL
          SELECT    a1.doc_id AS doc_id ,
                    NULL as saleout_id,
                    a1.doc_code AS saleout_code ,
                    a1.doc_date AS trans_date_gmt ,
                    a1.doc_type ,
                    1 AS is_custom_debit,
                    a1.current_doc_balance AS current_balance ,
                    a1.customer_id ,
                    a1.store_id
          FROM      dbo.ARDoc AS a1
          WHERE     doc_type = 'DM'
                    AND a1.current_doc_balance <> 0
                    AND a1.store_id = {0}
                    AND a1.customer_id = {1}
        ) AS a
        LEFT JOIN ( SELECT  *
                    FROM    dbo.payment_detail_temp
                    WHERE   user_id = {2}
                  ) AS b ON a.doc_id = b.payment_id";

                sQuery = string.Format(sQuery, cbxStore.SelectedValue, cbxKhachHang.SelectedValue, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                return tb;
            }
            catch (Exception ex)
            {
            }

            return null;
        }


        

        public void Delete_TempData()
        {
            try
            {
                string sQuery = "";
                sQuery = @"delete from payment_detail_temp where user_id={0}";
                sQuery = string.Format(sQuery, Session["userid"]);
               SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                
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

                    BindGrid_Temp();
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
                BindGrid_Temp();
                Update_DuNo();
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

        /*
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {




                Update_Header();

                if (cbxPaymentType.SelectedValue=="2")
                {
                    if (txtGiaTri.Text != "0")
                    {
                        RadWindowManager1.RadAlert("Số tiền thanh toán phải được phân bổ hết, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                        return;
                    }
                }


                //Don hang da hoan tat thanh toan
                string sQuery = "";

                sQuery = "select * from payment_detail_temp where user_id={0}";
                sQuery = string.Format(sQuery, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                if (tb.Rows.Count > 0)
                {
                    sQuery = "delete from payment_detail where payment_id={0}";
                    sQuery = string.Format(sQuery, hdf_ID.Value);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    sQuery = @"INSERT  INTO dbo.payment_detail
                                    ( payment_id ,
                                      order_id ,
                                      begin_balance ,
                                      payment_amt ,
                                      ending_balance,
                                        is_custom_debit
                                    )
                                    SELECT  payment_id ,
                                            order_id ,
                                            begin_balance ,
                                            payment_amt ,
                                            ending_balance,
                                            is_custom_debit
                                    FROM    dbo.payment_detail_temp
                                    WHERE   payment_id = {0}";
                    sQuery = string.Format(sQuery, hdf_ID.Value);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    //Cap nhat Current Balance cho Saleout Header

                    sQuery = @"UPDATE  dbo.saleout_header
                            SET     current_balance = ISNULL(b.ending_balance, 0)
                            FROM    dbo.saleout_header AS a
                                    LEFT JOIN ( SELECT  *
                                                FROM    dbo.payment_detail_temp
                                                WHERE   user_id = {0}
                                                        and is_custom_debit = 0
                                              ) AS b ON a.saleout_id = b.order_id
                            WHERE   saleout_id IN ( SELECT  order_id
                                                    FROM    dbo.payment_detail_temp
                                                    WHERE   user_id = {1} and is_custom_debit = 0 )";

                    sQuery = string.Format(sQuery, Session["userid"], Session["userid"]);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);




                    //Cap nhat Current Balance cho debit_credit

                    sQuery = @"UPDATE  dbo.debit_credit
                                    SET     current_balance = ISNULL(b.ending_balance, 0)
                                    FROM    dbo.debit_credit AS a
                                            LEFT JOIN ( SELECT  *
                                                        FROM    dbo.payment_detail_temp
                                                        WHERE   user_id = {0}
                                                                AND is_custom_debit = 1
                                                      ) AS b ON a.posted_id = b.order_id
                                    WHERE   posted_id IN ( SELECT   order_id
                                                           FROM     dbo.payment_detail_temp
                                                           WHERE    user_id = {1}
                                                                    AND is_custom_debit = 1 )";

                    sQuery = string.Format(sQuery, Session["userid"], Session["userid"]);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                    //Cap nhat trang thai cho Debit_Credit

                    if (cbxPaymentType.SelectedValue =="2")
                    {
                        if (txtGiaTri.Text =="0")
                        {
                            sQuery = @"update debit_credit set is_completed= 1 where posted_id={0}";
                            sQuery = string.Format(sQuery, cbxSoCT.SelectedValue);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                        }
                    }
                   

                    
                    //Cap nhat trang thai Complete Payment cho tung don hang
                    
                    sQuery = @"UPDATE  dbo.saleout_header
                                    SET     is_complete_payment = 1
                                    WHERE   saleout_id IN ( SELECT  order_id
                                                            FROM    dbo.payment_detail
                                                            WHERE   ending_balance = 0 and is_custom_debit = 0)";                    
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);



                    //Cap nhat is_complete
                    sQuery = @"UPDATE  dbo.debit_credit
                                    SET     is_completed = 1
                                    WHERE   posted_id IN ( SELECT   order_id
                                                           FROM     dbo.payment_detail
                                                           WHERE    ending_balance = 0
                                                                    AND is_custom_debit = 1 )";
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);



                }


                

                Response.Redirect("payment-edit-2.aspx");
            }
            catch (Exception ex)
            {
            }
        }


        */


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                Update_Header();

                if (cbxPaymentType.SelectedValue == "2")
                {
                    if (txtGiaTri.Text != "0")
                    {
                        RadWindowManager1.RadAlert("Số tiền thanh toán phải được phân bổ hết, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                        return;
                    }
                }

                if (cbxPaymentType.SelectedValue == "1")
                {
                    if (txtGiaTri.Text == "0" || txtGiaTri.Text =="")
                    {
                        RadWindowManager1.RadAlert("Vui lòng điền tổng Số tiền thanh toán vào ô Giá Trị!", 330, 180, "Thông báo", null, null);
                        return;
                    }
                }


                UpdateARDoc();

                string sQuery = "select * from payment_detail_temp where user_id={0}";
                sQuery = string.Format(sQuery, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                if (tb.Rows.Count > 0)
                {
                    sQuery = "delete from payment_detail where payment_id={0}";
                    sQuery = string.Format(sQuery, hdf_ID.Value);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    sQuery = @"INSERT  INTO dbo.payment_detail
                                    ( payment_id ,
                                      order_id ,
                                      begin_balance ,
                                      payment_amt ,
                                      ending_balance,
                                        is_custom_debit
                                    )
                                    SELECT  payment_id ,
                                            order_id ,
                                            begin_balance ,
                                            payment_amt ,
                                            ending_balance,
                                            is_custom_debit
                                    FROM    dbo.payment_detail_temp
                                    WHERE   user_id = {0}";
                    sQuery = string.Format(sQuery, Session["userid"]);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    //Cap nhat Current Balance cho Saleout Header

//                    sQuery = @"UPDATE  dbo.saleout_header
//                            SET     current_balance = ISNULL(b.ending_balance, 0)
//                            FROM    dbo.saleout_header AS a
//                                    LEFT JOIN ( SELECT  *
//                                                FROM    dbo.payment_detail_temp
//                                                WHERE   user_id = {0}
//                                                        and is_custom_debit = 0
//                                              ) AS b ON a.saleout_id = b.order_id
//                            WHERE   saleout_id IN ( SELECT  order_id
//                                                    FROM    dbo.payment_detail_temp
//                                                    WHERE   user_id = {1} and is_custom_debit = 0 )";


                    sQuery = @"UPDATE  dbo.ARDoc
                                SET     current_doc_balance = ISNULL(b.ending_balance, 0)
                                FROM    dbo.ARDoc AS a
                                        LEFT JOIN ( SELECT  *
                                                    FROM    dbo.payment_detail_temp
                                                    WHERE   user_id = {0}
                                                  ) AS b ON a.doc_id = b.payment_id


                                WHERE   a.doc_id IN ( SELECT    payment_id
                                                      FROM      dbo.payment_detail_temp
                                                      WHERE     user_id = {0} )";

                    sQuery = string.Format(sQuery, Session["userid"]);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                    if (cbxPaymentType.SelectedValue == "2")
                    {
                        if (txtGiaTri.Text == "0")
                        {
                            //Cap nhat is_complete
                            sQuery = @"UPDATE  dbo.ARDoc
                                        SET     release = 1,
                                                current_doc_balance = 0 
                                        WHERE   doc_id ={0}";
                            sQuery = string.Format(sQuery,cbxSoCT.SelectedValue);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                        }
                        else
                        {
                            //Cap nhat is_complete
                            sQuery = @"UPDATE  dbo.ARDoc
                                        SET     release = 0,
                                                current_doc_balance = {0}
                                        WHERE   doc_id ={1}";

                            sQuery = string.Format(sQuery,txtGiaTri.Text,cbxSoCT.SelectedValue);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                        }
                    }
                    else
                    {
                        //Cap nhat is_complete
                        sQuery = @"UPDATE  dbo.ARDoc
                                    SET     release = 1
                                    WHERE   doc_id IN ( SELECT   payment_id
                                                           FROM     dbo.payment_detail
                                                           WHERE    ending_balance = 0
                                                                    )";
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                    }
                    



                }




                Response.Redirect("payment-edit-2.aspx");

                
            }
            catch (Exception ex)
            {
            }
        }


        public void UpdateARDoc()
        {
            try
            {
                //Insert
                //GenCode
                string _id = hdf_ID.Value;
                string _code = txtCode.Text;      

              

                string storeProc = "[usp_InsertUpdateARDoc]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doc_id", _id);
                    cmd.Parameters.AddWithValue("@doc_code", _code);
                    cmd.Parameters.AddWithValue("@doc_date", rdpNgay.DbSelectedDate);
                    cmd.Parameters.AddWithValue("@doc_type", "PA");
                    cmd.Parameters.AddWithValue("doc_balance ", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@current_doc_balance", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@original_doc_amount", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@release ", 0);
                    cmd.Parameters.AddWithValue("@reference_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@note", txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@created_by", Session["userid"]);
                    cmd.Parameters.AddWithValue("@last_modified", DateTime.Now); 


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


            }
            catch (Exception ex)
            {


            }
        }



        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();
            string sQuery = "delete from [payment_detail_temp] where [row_id]=" + id;
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
                    BindGrid_Temp();
                    Update_DuNo();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.myData_Temp;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

                    System.Web.UI.WebControls.TextBox _txtSoChungTu = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtSoChungTu");
                    System.Web.UI.WebControls.TextBox _txtNgay = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtNgay");
                    System.Web.UI.WebControls.TextBox _txtDuNo = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtDuNo");
                    System.Web.UI.WebControls.TextBox _txtThanhToan = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtThanhToan");

                    HiddenField hdf_row_id = (HiddenField)MyUserControl.FindControl("hdf_row_id");
                    HiddenField hdf_saleout_id = (HiddenField)MyUserControl.FindControl("hdf_saleout_id");
                    HiddenField hdf_is_custom_debit = (HiddenField)MyUserControl.FindControl("hdf_is_custom_debit");


                    HiddenField hdf_doc_id = (HiddenField)MyUserControl.FindControl("hdf_doc_id");

                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                       
                        
                        string row_id = parentItem["row_id"].Text;

                        string saleout_id = parentItem["saleout_id"].Text;
                        hdf_saleout_id.Value = saleout_id == "&nbsp;" ? "" : saleout_id;


                        string is_custom_debit = parentItem["is_custom_debit"].Text;
                        hdf_row_id.Value = row_id;
                        hdf_is_custom_debit.Value = is_custom_debit;


                        string doc_id = parentItem["doc_id"].Text;
                        hdf_doc_id.Value = doc_id;



                        string saleout_code = parentItem["saleout_code"].Text;
                        string trans_date_gmt = parentItem["trans_date_gmt"].Text;

                        string current_balance = parentItem["begin_balance"].Text.Replace(",", "");
                        string payment_amt = parentItem["payment_amt"].Text.Replace(",", "");




                        _txtSoChungTu.Text = saleout_code == "&nbsp;" ? "" : saleout_code;
                        _txtNgay.Text = trans_date_gmt == "&nbsp;" ? "" : trans_date_gmt;
                        _txtDuNo.Text = current_balance == "&nbsp;" ? "0" : current_balance;
                        _txtThanhToan.Text = payment_amt == "&nbsp;" ? "0" : payment_amt;



                    }
                    else
                    {
                       
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    //Update Data


                    

                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);



                    string row_id = (userControl.FindControl("hdf_row_id") as System.Web.UI.WebControls.HiddenField).Value;
                    string saleout_id = (userControl.FindControl("hdf_saleout_id") as System.Web.UI.WebControls.HiddenField).Value;


                    string is_custom_debit = (userControl.FindControl("hdf_is_custom_debit") as System.Web.UI.WebControls.HiddenField).Value;


                    if (row_id == "&nbsp;")
                    {
                        row_id = "";
                    }
	

                    string doc_id = (userControl.FindControl("hdf_doc_id") as System.Web.UI.WebControls.HiddenField).Value;
                    string thanhtoan = (userControl.FindControl("txtThanhToan") as System.Web.UI.WebControls.TextBox).Text.Trim();




                    string sQuery = "";

                    decimal payment_amt = 0;
                    decimal balance_amt = 0;
                    decimal begin_balance = 0;
                    decimal ending_balance = 0;


                    //Kiem tra don hang nay da co thanh toan nao hay chua


                    ////Tinh gia tri 
                    //if (is_custom_debit == "0")
                    //{
                    //    sQuery = @"select ISNULL(current_balance,0) as current_balance from saleout_header where saleout_id={0}";
                    //}

                    //if (is_custom_debit == "1")
                    //{
                    //    sQuery = @"select ISNULL(current_balance,0) as current_balance from debit_credit where posted_id={0}";
                    //}

                    sQuery = @"select ISNULL(current_doc_balance,0) as current_doc_balance from ARDoc where doc_id={0}";


                    sQuery = string.Format(sQuery, doc_id);

                    
                    begin_balance = decimal.Parse(SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());

                    
                    
                    payment_amt = decimal.Parse(thanhtoan);
                    ending_balance =  begin_balance - payment_amt;

                    string storeProc = "usp_InsertUpdatepayment_detail_temp";


                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;


                        if (string.IsNullOrEmpty(row_id))
                        {
                            cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                            cmd.Parameters.AddWithValue("@payment_id", doc_id);
                            cmd.Parameters.AddWithValue("@order_id", saleout_id);

                            cmd.Parameters.AddWithValue("@begin_balance", begin_balance);
                            cmd.Parameters.AddWithValue("@payment_amt", payment_amt);
                            cmd.Parameters.AddWithValue("@ending_balance", ending_balance);
                            cmd.Parameters.AddWithValue("@user_id", Session["userid"]);

                            cmd.Parameters.AddWithValue("@is_custom_debit", is_custom_debit);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@row_id", row_id);
                            cmd.Parameters.AddWithValue("@payment_id", doc_id);
                            cmd.Parameters.AddWithValue("@order_id", doc_id);

                            cmd.Parameters.AddWithValue("@begin_balance", begin_balance);
                            cmd.Parameters.AddWithValue("@payment_amt", payment_amt);
                            cmd.Parameters.AddWithValue("@ending_balance", ending_balance);
                            cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                            cmd.Parameters.AddWithValue("@is_custom_debit", is_custom_debit);
                        }




                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }


                    BindGrid_Temp();
                    Update_DuNo();

                }
                           

            }
            catch (Exception ex)
            {


            }
        }

        protected void cbxPaymentType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cbxPaymentType.SelectedValue =="2")
                {
                    cbxSoCT.Enabled = true;
                    //txtGiaTri.Enabled = false;
                    txtGiaTri.Text = "0";
                    //Load DS so chung tu


                    string sQuery = @"SELECT  doc_id AS posted_id ,
                                            doc_code AS posted_code
                                    FROM    dbo.ARDoc
                                    WHERE   doc_type = 'CM'
                                            AND (release = 0 OR release is null)
                                            AND current_doc_balance <> 0
                                            AND store_id = {0}
                                            AND customer_id = {1}
                                            ";
                    sQuery = string.Format(sQuery,cbxStore.SelectedValue ,cbxKhachHang.SelectedValue);
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    cbxSoCT.DataSource = tb;
                    cbxSoCT.DataBind();
                    cbxSoCT.Items.Insert(0, new RadComboBoxItem(null, null));
                }
                else
                {
                    cbxSoCT.Enabled = false;
                    //txtGiaTri.Enabled = true;
                    txtGiaTri.Text = "0";

                }
            }
            catch (Exception ex)
            {
                
                
            }
        }


        public string Get_Posted_Amount(string posted_id)
        {
            try
            {
                //string sQuery = @"select isnull(posted_amount,0) as posted_amount from debit_credit where posted_id={0}";
                string sQuery = @"select isnull(current_doc_balance,0) as posted_amount from ARDoc where doc_id={0}";

                sQuery = string.Format(sQuery, posted_id);
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                return result;
            }
            catch (Exception ex)
            {
                return "";
                
            }
            return "";
        }
        protected void cbxSoCT_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                //Lay Gia tri

                string result = Get_Posted_Amount(cbxSoCT.SelectedValue);
                txtGiaTri.Text = result;
                hdf_GTThanhToan.Value = result;

            }
            catch (Exception ex)
            {
                
                
            }
        }
    }
}