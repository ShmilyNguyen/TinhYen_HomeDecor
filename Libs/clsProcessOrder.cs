using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace WKS.DMS.WEB.Libs
{
    public static class clsProcessOrder
    {
        public static bool Check_Valid_LockDate(string _NgayGiaoDich, string _store_id)
        {
            try
            {
                string flag1 = "";
                string flag2 = "";
                string flag3 = "";

                string storeProc = "[sp_sys_config_check_lockdate_saleout]";
                string storeProc3 = "[sp_sys_config_check_lockdate_saleout_extra]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd1 = new SqlCommand(storeProc, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    SqlCommand cmd3 = new SqlCommand(storeProc3, conn);
                    cmd3.CommandType = CommandType.StoredProcedure;

                    cmd1.Parameters.AddWithValue("@trans_date_gmt", _NgayGiaoDich);

                    cmd3.Parameters.AddWithValue("@trans_date_gmt", _NgayGiaoDich);
                    cmd3.Parameters.AddWithValue("@store_id",_store_id);


                    conn.Open();
                    flag1 = Convert.ToString(cmd1.ExecuteScalar());
                    flag3 = Convert.ToString(cmd3.ExecuteScalar());
                    conn.Close();
                }

                storeProc = "[sp_sys_config_check_lockdate_inventory_closing]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd2 = new SqlCommand(storeProc, conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@trans_date_gmt", _NgayGiaoDich);
                    cmd2.Parameters.AddWithValue("@store_id", _store_id);

                    conn.Open();
                    flag2 = Convert.ToString(cmd2.ExecuteScalar());
                    conn.Close();
                }

                if (bool.Parse(flag1) && bool.Parse(flag2) || bool.Parse(flag3))
                {
                    return true;
                }
                

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // 0 - Khong hop le- Ton kho khong du
        // 1 - Hop le
        // 2 - Thieu ten nhan vien
        // 3 - Thieu ten khach hang
        // 4 - Ngay xuat kho vuot qua pham vi cho phep
        public static int Order_Confirmed(string order_id, string user_id)
        {
            try
            {

                string sQuery = "";

                sQuery = @"SELECT  store_id ,
                                    ISNULL(employee_id,0) as employee_id,
                                    ISNULL(customer_id,0) as customer_id,
                                    MONTH(trans_date_gmt) AS Thang ,
                                    YEAR(trans_date_gmt) AS Nam,
                                    trans_date_gmt
                            FROM    dbo.saleout_header
                            where saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                DataTable dt = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                string Thang = "0";
                string Nam = "0";
                string store_id = "0";
                string employee_id = "";
                string customer_id = "";
                string trans_date_gmt = "";

                System.Diagnostics.Debug.WriteLine("Execute query ! ");
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    Thang = r["Thang"].ToString();
                    Nam = r["Nam"].ToString();
                    store_id = r["store_id"].ToString();
                    employee_id = r["employee_id"].ToString();
                    customer_id = r["customer_id"].ToString();
                    trans_date_gmt = r["trans_date_gmt"].ToString();
                }
                else
                {
                    return 0;
                }

                if (employee_id == "0")
                {
                    return 2;
                }

                if (customer_id == "0")
                {
                    return 3;
                }

                if (!Check_Valid_LockDate(trans_date_gmt, store_id))
                {
                    return 4;
                }

                // 
                sQuery = @"[sp_rpt_XacNhanDonHang_TinhTonKho]";
                SqlParameter[] arrSQLParam = new SqlParameter[3];
                arrSQLParam[0] = new SqlParameter("@user_id", user_id);
                arrSQLParam[1] = new SqlParameter("@report_month", Thang);
                arrSQLParam[2] = new SqlParameter("@report_year", Nam);

                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam);

                sQuery = @"
                            SELECT  a.item_id ,
                                    a.qty_total ,
                                    b.qty
                            FROM    ( SELECT    *
                                      FROM      dbo.tmp_inventory_closing
                                      WHERE     user_id = {0}
                                                AND store_id = {1}
                                                AND item_id IN ( SELECT DISTINCT
                                                                        item_id
                                                                 FROM   dbo.saleout_detail
                                                                 WHERE  saleout_id = {2} )
                                    ) AS a
                                    LEFT JOIN ( SELECT  item_id ,
                                                        SUM(qty) AS qty
                                                FROM    dbo.saleout_detail
                                                WHERE   saleout_id = {3}
                                                GROUP BY item_id
                                              ) AS b ON a.item_id = b.item_id
                            WHERE   a.qty_total < b.qty";

                sQuery = string.Format(sQuery, user_id, store_id, order_id, order_id);

                dt = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

              
                //Neu khong co san pham nao het ton kho
                if (dt.Rows.Count == 0)
                {
                    sQuery = @"update saleout_header set is_confirmed = 1,is_complete_payment = 0 where saleout_id={0}";
                    sQuery = string.Format(sQuery, order_id);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return 0;
        }

        public static int Order_Confirmed_By_Store(string store_id, string order_id, string user_id)
        {
            try
            {
                string sQuery = "";

                sQuery = @"SELECT  store_id ,
                                    ISNULL(employee_id,0) as employee_id,
                                    ISNULL(customer_id,0) as customer_id,
                                    MONTH(trans_date_gmt) AS Thang ,
                                    YEAR(trans_date_gmt) AS Nam
                            FROM    dbo.saleout_header
                            where saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                DataTable dt = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                string Thang = "0";
                string Nam = "0";

                string employee_id = "";
                string customer_id = "";

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    Thang = r["Thang"].ToString();
                    Nam = r["Nam"].ToString();
                    store_id = r["store_id"].ToString();
                    employee_id = r["employee_id"].ToString();
                    customer_id = r["customer_id"].ToString();
                }
                else
                {
                    return 0;
                }

                if (employee_id == "0")
                {
                    return 2;
                }

                if (customer_id == "0")
                {
                    return 3;
                }

                sQuery = @"[sp_rpt_XacNhanDonHang_TinhTonKho_Theo_NPP]";
                SqlParameter[] arrSQLParam = new SqlParameter[4];
                arrSQLParam[0] = new SqlParameter("@user_id", user_id);
                arrSQLParam[1] = new SqlParameter("@store_id", store_id);
                arrSQLParam[2] = new SqlParameter("@report_month", Thang);
                arrSQLParam[3] = new SqlParameter("@report_year", Nam);

                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam);

                sQuery = @"
                            SELECT  a.item_id ,
                                    a.qty_total ,
                                    b.qty
                            FROM    ( SELECT    *
                                      FROM      dbo.tmp_inventory_closing
                                      WHERE     user_id = {0}
                                                AND store_id = {1}
                                                AND item_id IN ( SELECT DISTINCT
                                                                        item_id
                                                                 FROM   dbo.saleout_detail
                                                                 WHERE  saleout_id = {2} )
                                    ) AS a
                                    LEFT JOIN ( SELECT  item_id ,
                                                        SUM(qty) AS qty
                                                FROM    dbo.saleout_detail
                                                WHERE   saleout_id = {3}
                                                GROUP BY item_id
                                              ) AS b ON a.item_id = b.item_id
                            WHERE   a.qty_total < b.qty";

                sQuery = string.Format(sQuery, user_id, store_id, order_id, order_id);

                dt = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                //Neu khong co san pham nao het ton kho
                if (dt.Rows.Count == 0)
                {
                    sQuery = @"update saleout_header set is_confirmed = 1,is_complete_payment = 0 where saleout_id={0}";
                    sQuery = string.Format(sQuery, order_id);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return 0;
        }

        public static void Order_Reset_Promo(string store_id, string order_id)
        {
            try
            {
                string sQuery = "";

                sQuery = @"DELETE  dbo.saleout_detail
                                    WHERE   saleout_id = {0}";
                sQuery = string.Format(sQuery, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool Order_CheckExist(string codename, string code)
        {
            try
            {
                string sQuery = @"[sp_Sys_Check_Code_Exist]";
                SqlParameter[] arrSQLParam = new SqlParameter[2];
                arrSQLParam[0] = new SqlParameter("@codename", codename);
                arrSQLParam[1] = new SqlParameter("@code", code);

                object result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam);
                bool b = (bool)(result);
                if (b)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }

            return true;
        }

        public static bool Order_Returned(string order_id, string user_id)
        {
            try
            {
                string return_order_id = "";
                string return_order_code = "";

                #region GenCode

                string ID = "";
                string Code = "";

                clsCodeMaster.GenCode("trahang", user_id, out ID, out Code);
                return_order_id = ID;
                return_order_code = Code;

                #endregion GenCode

                string sQuery = @"INSERT  INTO dbo.saleout_header
                                                ( saleout_id ,
                                                  saleout_code ,
                                                  trans_date_gmt ,
                                                  trans_date_numb ,
                                                  store_id ,
                                                  route_id ,
                                                  employee_id ,
                                                  customer_id ,
                                                  note ,
                                                  created_by ,
                                                  last_modified ,
                                                  ontop_discount ,
                                                  ontop_discount_code ,
                                                  item_price_policy_id ,
                                                  is_confirmed ,
                                                  is_returned ,
                                                  parent_id ,
                                                  is_returned_order ,
                                                  returned_date_gmt,
                                                    is_locked,
                                                total_amount,current_balance
                                                )
                                                SELECT   {1},--saleout_id
                                                         '{2}',--saleout_code
                                                        '{4}',--trans_date_gmt
                                                        {5},--trans_date_numb
                                                        store_id ,
                                                        route_id ,
                                                        employee_id ,
                                                        customer_id ,
                                                        note ,
                                                        created_by ,
                                                        last_modified ,
                                                        ontop_discount ,
                                                        ontop_discount_code ,
                                                        item_price_policy_id ,
                                                        1,--is_confirmed
                                                        0 ,--is_returned
                                                        {3} ,--parent_id
                                                        1 ,--is_returned_order
                                                        GETDATE(), --returned_date_gmt
                                                        1, --is_locked

                                                        -1 * ISNULL(total_amount,0),
                                                        -1 * ISNULL(current_balance,0)
                                                FROM    dbo.saleout_header
                                                WHERE   saleout_id = {0}";
                sQuery = string.Format(sQuery, order_id, return_order_id, return_order_code, order_id, DateTime.Now, clsCommon.ConvertDateToNumber(DateTime.Now));
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "select * from saleout_detail where saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string saleout_id = return_order_id;
                    string saleout_type = (r["saleout_type"] ?? "").ToString();
                    string item_id = (r["item_id"] ?? "").ToString();
                    string qty = (r["qty"] ?? "0").ToString();
                    string promo_id = (r["promo_id"] ?? "").ToString();
                    string discount = (r["discount"] ?? "0").ToString();

                    if (string.IsNullOrEmpty(promo_id))
                    {
                        promo_id = "NULL";
                    }
                    sQuery = @"INSERT  INTO dbo.saleout_detail
                                            ( saleout_id ,
                                              saleout_type ,
                                              item_id ,
                                              qty ,
                                              promo_id,
                                                discount

                                            )
                                    VALUES  ( {0} , -- saleout_id - int
                                              '{1}' , -- saleout_type - varchar(50)
                                              {2} , -- item_id - int
                                              -{3} , -- qty - int
                                              {4} , -- promo_id - int
                                              {5}  -- discount - float

                                            )";

                    sQuery = string.Format(sQuery, saleout_id, saleout_type, item_id, qty, promo_id, discount);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                }

                sQuery = "update saleout_header set is_returned=1 , is_locked= 1, is_confirmed = 1 where saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //Update ARDoc

                sQuery = "update ARDoc set current_doc_balance = 0, release = 1 where reference_id = {0}";
                sQuery = string.Format(sQuery, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //Cap nhat cong no don hang tra
                //Order_Returned_Payment(return_order_id, user_id);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public static bool Order_Returned(string order_id, string user_id, DateTime return_date, string LyDoTraHang)
        {
            try
            {
                string return_order_id = "";
                string return_order_code = "";

                #region Xoa don hang tra cu
                //Xoa don hang tra cu don hang cu
                string sQuery = "SELECT ISNULL(saleout_id,'') AS parent_id from saleout_header where saleout_code like '%TH-%' AND parent_id={0}";
                sQuery = string.Format(sQuery, order_id);
                object saleout_id_old = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery);

                if(saleout_id_old != null)
                {
                    var _saleout_id = saleout_id_old.ToString();
                    if (!string.IsNullOrEmpty(_saleout_id))
                    {
                        sQuery = "delete from saleout_detail where saleout_id={0}";
                        sQuery = string.Format(sQuery, _saleout_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        sQuery = "delete from saleout_header where saleout_id={0}";
                        sQuery = string.Format(sQuery, _saleout_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                    }
                }
                

                #endregion

                #region GenCode

                string ID = "";
                string Code = "";

                clsCodeMaster.GenCode("trahang", user_id, out ID, out Code);
                return_order_id = ID;
                return_order_code = Code;

                #endregion GenCode

                sQuery = @"INSERT  INTO dbo.saleout_header
                                                ( saleout_id ,
                                                  saleout_code ,
                                                  trans_date_gmt ,
                                                  trans_date_numb ,
                                                  store_id ,
                                                  route_id ,
                                                  employee_id ,
                                                  customer_id ,
                                                  note ,
                                                  created_by ,
                                                  last_modified ,
                                                  ontop_discount ,
                                                  ontop_discount_code ,
                                                  item_price_policy_id ,
                                                  is_confirmed ,
                                                  is_returned ,
                                                  parent_id ,
                                                  is_returned_order ,
                                                  returned_date_gmt,
                                                    is_locked,
                                                total_amount,current_balance , price_group_id, status_id
                                                )
                                                SELECT   {1},--saleout_id
                                                         '{2}',--saleout_code
                                                        '{4}',--trans_date_gmt
                                                        {5},--trans_date_numb
                                                        store_id ,
                                                        route_id ,
                                                        employee_id ,
                                                        customer_id ,
                                                        N'{6}' ,
                                                        created_by ,
                                                        last_modified ,
                                                        ontop_discount ,
                                                        ontop_discount_code ,
                                                        item_price_policy_id ,
                                                        1,--is_confirmed
                                                        0 ,--is_returned
                                                        {3} ,--parent_id
                                                        1 ,--is_returned_order
                                                        GETDATE(), --returned_date_gmt
                                                        1, --is_locked

                                                        -1 * ISNULL(total_amount,0),
                                                        -1 * ISNULL(current_balance,0),

                                                            price_group_id,
                                                            status_id

                                                FROM    dbo.saleout_header
                                                WHERE   saleout_id = {0}";
                sQuery = string.Format(sQuery, order_id, return_order_id, return_order_code, order_id, return_date, clsCommon.ConvertDateToNumber(return_date), LyDoTraHang);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = @"SELECT  ISNULL(saleout_type, '') AS saleout_type ,
                                    ISNULL(item_id, '0') AS item_id ,
                                    ISNULL(qty, 0) AS qty ,
                                    promo_id ,
                                    ISNULL(discount, 0) AS discount ,
                                    ISNULL(ontop_row_discount, 0) AS ontop_row_discount ,
                                    ISNULL(item_price, 0) AS item_price,
                                    ISNULL(tax_rate, 0) AS tax_rate  
                            FROM    saleout_detail
                            WHERE   saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string saleout_id = return_order_id;
                    string saleout_type = (r["saleout_type"] ?? "").ToString();
                    string item_id = (r["item_id"] ?? "").ToString();
                    string qty = (r["qty"] ?? "0").ToString();
                    string promo_id = (r["promo_id"] ?? "").ToString();
                    string discount = (r["discount"] ?? "0").ToString();

                    string ontop_row_discount = (r["ontop_row_discount"] ?? "0").ToString();
                    string item_price = (r["item_price"] ?? "0").ToString();
                    string tax_rate = (r["tax_rate"] ?? "0").ToString();

                    if (string.IsNullOrEmpty(promo_id))
                    {
                        promo_id = "NULL";
                    }
                    sQuery = @"INSERT  INTO dbo.saleout_detail
                                            ( saleout_id ,
                                              saleout_type ,
                                              item_id ,
                                              qty ,
                                              promo_id,
                                                discount,  ontop_row_discount ,
                                                item_price, tax_rate 

                                            )
                                        VALUES  ( {0} , -- saleout_id - int
                                                  '{1}' , -- saleout_type - varchar(50)
                                                  {2} , -- item_id - int
                                                  -{3} , -- qty - int
                                                  {4} , -- promo_id - int
                                                  {5},  -- discount - float
                                                {6},
                                                {7}, 
                                                {8}
                                            )";

                    sQuery = string.Format(sQuery, saleout_id, saleout_type, item_id, qty, promo_id, discount, ontop_row_discount, item_price,tax_rate);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                }

                //Khoa don hang da tra
                sQuery = "update saleout_header set is_returned=1,is_locked=1  where saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                //Update ARDoc

                sQuery = "update ARDoc set current_doc_balance = 0, release = 1 where reference_id = {0}";
                sQuery = string.Format(sQuery, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

              //  tai day hả ? k phải r a :D 

                sQuery = "delete from TrackingCustomerDebt where reference_id = {0}";
                sQuery = string.Format(sQuery, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                //Cap nhat cong no don hang tra
                //Order_Returned_Payment(return_order_id, user_id);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public static bool Order_UpdateAmount(string order_id, decimal amount)
        {
            try
            {
                string sQuery = "update saleout_header set total_amount={0}, current_balance={1} where saleout_id={2}";
                sQuery = string.Format(sQuery, amount, amount, order_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool Order_Returned_Payment(string order_id, string user_id)
        {
            try
            {
                string sQuery = "";

                //Tao Code Payment

                //Add New
                string ID = "";
                string Code = "";
                string parent_id = "";

                sQuery = "select * from saleout_header where saleout_id={0}";
                sQuery = string.Format(sQuery, order_id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                decimal current_balance = 0;
                decimal total_amount = 0;

                if (tb.Rows.Count > 0)
                {
                    DataRow r = tb.Rows[0];
                    clsCodeMaster.GenCode("payment-credit-memo", user_id, out ID, out Code);

                    //Tao phieu thu am

                    string storeProc = "[usp_InsertUpdatepayment_header]";

                    int result = 0;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            string id = ID;
                            if (!string.IsNullOrEmpty(id))
                            {
                                cmd.Parameters.AddWithValue("@payment_id", int.Parse(ID));
                                cmd.Parameters.AddWithValue("@store_id", r["store_id"]);
                                cmd.Parameters.AddWithValue("@customer_id", r["customer_id"]);
                                cmd.Parameters.AddWithValue("@payment_code", Code);
                                cmd.Parameters.AddWithValue("@payment_date", DateTime.Now);
                                //Kieu Pyament la Credit Memo
                                cmd.Parameters.AddWithValue("@payment_type_id", 2);
                                cmd.Parameters.AddWithValue("@created_by", r["created_by"]);
                                cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);

                                cmd.Parameters.AddWithValue("@note", DBNull.Value);

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    parent_id = r["parent_id"].ToString();

                    current_balance = decimal.Parse(r["current_balance"].ToString());

                    total_amount = decimal.Parse(r["total_amount"].ToString());

                    sQuery = @"INSERT INTO dbo.payment_detail
                                                ( payment_id ,
                                                  order_id ,
                                                  begin_balance ,
                                                  payment_amt ,
                                                  ending_balance
                                                )
                                        VALUES  ( {0} , -- payment_id - int
                                                  {1} , -- order_id - int
                                                  {2} , -- begin_balance - decimal
                                                  {3} , -- payment_amt - decimal
                                                  {4}  -- ending_balance - decimal
                                                )";

                    sQuery = string.Format(sQuery, ID, order_id, total_amount, -1 * total_amount, 0);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    sQuery = @"update saleout_header set current_balance = {0} where saleout_id={1}";
                    sQuery = string.Format(sQuery, 0, order_id);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    sQuery = @"update saleout_header set current_balance = {0} where saleout_id={1}";
                    sQuery = string.Format(sQuery, 0, parent_id);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public static string Order_GetUrlForm()
        {
            try
            {
                string sQuery = @"IF EXISTS ( SELECT  param_value
                                                FROM    dbo.sys_config
                                                WHERE   param_key = 'AUTO_CONFIRM' )
                                        SELECT  ISNULL(param_value, 'false')
                                        FROM    dbo.sys_config
                                        WHERE   param_key = 'AUTO_CONFIRM'
                                    ELSE
                                        SELECT  'false' ";

                bool ret = bool.Parse(SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());
                if (ret == true)
                {
                    return clsCommon.UrlRoot + "Forms/saleout-edit-3.aspx";
                }
                else
                {
                    return clsCommon.UrlRoot + "Forms/saleout-edit-2.aspx";
                }

                return "";
            }
            catch (Exception ex)
            {
                return "#";
                throw;
            }
        }
    }
}