using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace WKS.DMS.WEB.Libs
{
    public class clsPromotion

    {
      
        public static void TinhToanKM_ChietKhau(string saleout_id)
        {
            
            try
            {
                string sQuery = "";

                sQuery = "update saleout_detail set discount = 0 where saleout_id={0}";
                sQuery = string.Format(sQuery, saleout_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = @"SELECT  row_id ,item_id ,
                                    item_code ,
                                    promo_code ,
                                    promo_id ,
                                    qty_saleout ,
                                    qty_promo , total_saleout ,
                                    total_promo ,
                                    promo_type
                            FROM    dbo.v_SaleOut
                            WHERE   saleout_id = {0}
                                    AND (promo_type = 'CHIETKHAU' OR promo_type = 'LINE-DISCOUNT')
                                    AND saleout_type = 'HB'";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string item_id = r["item_id"].ToString();
                    string promo_id = r["promo_id"].ToString();

                    decimal qty_saleout = decimal.Parse(r["qty_saleout"].ToString());
                    decimal total_saleout = decimal.Parse(r["total_saleout"].ToString());

                    int row_id = int.Parse(r["row_id"].ToString());
                    decimal vol = 0;
                    float discount = 0;

                    //Lay chiet khau cua Chuong Trinh
                    sQuery = @"SELECT  *
                                FROM    dbo.promotion_item_discount
                                WHERE   promo_id = {0}
                                        AND item_id = {1}";

                    sQuery = string.Format(sQuery, promo_id, item_id);
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    //Kiem tra Hang hoa co tham gia Chiet Khau
                    if (tb.Rows.Count <= 0)
                    {
                        return;
                    }

                    vol = decimal.Parse(tb.Rows[0]["vol"].ToString());
                    discount = float.Parse(tb.Rows[0]["discount"].ToString());

                    string promo_type = tb.Rows[0]["promo_type"].ToString();

                    if (promo_type == "STOCK UOM")
                    {
                        if (qty_saleout >= vol)
                        {
                            sQuery = "update saleout_detail set discount={0} where row_id={1}";
                            sQuery = string.Format(sQuery, discount, row_id);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                        }
                    }

                    if (promo_type == "AMOUNT")
                    {
                        if (total_saleout >= vol)
                        {
                            sQuery = "update saleout_detail set discount={0} where row_id={1}";
                            sQuery = string.Format(sQuery, discount, row_id);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void TinhChietKhauDonHang_TheoLoaiKH(string saleout_id , int dist_channel_id )
        {
            try
            {
                
                

                string sQuery = "[sp_get_ontop_info_by_saleoutid]";

                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@saleout_id", saleout_id);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];

                if (data.Rows.Count > 0)
                {
                    DataRow r = data.Rows[0];
                    Int32 target_vol = Int32.Parse(r["target_vol"].ToString());
                    float ontopdiscount = float.Parse(r["ontopdiscount"].ToString());

                    bool isVAT = bool.Parse(r["isVAT"].ToString());
                    bool isOntop = bool.Parse(r["isOntop"].ToString());


                    //Tinh doanh so don hang
                    sQuery = @"SELECT  ISNULL(SUM(total_saleout),0)
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}";

                    sQuery = string.Format(sQuery, saleout_id);


                    String temp = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                    Double total_saleout = Double.Parse(SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());



                    //Kiem tra neu Doanh so don hang > = Doanh so chi tieu dat KM

                  
                    if (total_saleout >= target_vol   || ( (total_saleout * -1 ) > target_vol && dist_channel_id == 2))
                    {
                        //Chiet khau Ontop Don Hang
                        if (isOntop)
                        {
                            //Update CK vao don hang
                            sQuery = "update saleout_header set ontop_discount = {0} where saleout_id={1}";
                            sQuery = string.Format(sQuery, ontopdiscount, saleout_id);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                            //Update CK vao don hang
                            sQuery = "update saleout_detail set ontop_row_discount = {0} where saleout_id={1}";
                            sQuery = string.Format(sQuery, 0, saleout_id);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                        }
                        else
                        {

                            //Chiet khau Row_Ontop tung dong hang
                            //Update CK vao don hang
                            sQuery = "update saleout_detail set ontop_row_discount = {0} where saleout_id={1}";
                            sQuery = string.Format(sQuery, ontopdiscount, saleout_id);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                            sQuery = "update saleout_header set ontop_discount = {0} where saleout_id={1}";
                            sQuery = string.Format(sQuery, 0, saleout_id);
                            SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        }


                    }
                    else
                    {

                        //Update CK vao don hang
                        sQuery = "update saleout_header set ontop_discount = {0} where saleout_id={1}";
                        sQuery = string.Format(sQuery, 0, saleout_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                        //Update CK vao don hang
                        sQuery = "update saleout_detail set ontop_row_discount = {0} where saleout_id={1}";
                        sQuery = string.Format(sQuery, 0, saleout_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                    }
                }


            }
            catch (Exception ex)
            {
            }
        }


        public static void TinhToanKM_TangHang(string saleout_id)
        {
            try
            {
                string sQuery = "";

                #region Tang Hang 1 Level

                sQuery = "delete from saleout_detail where saleout_id={0} and saleout_type='KM'";
                sQuery = string.Format(sQuery, saleout_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = @"SELECT  item_id ,
                                        promo_id ,
                                        ISNULL(SUM(qty_saleout),0) AS qty_saleout
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}
                                        AND promo_type = 'TANGHANG'
                                        AND saleout_type = 'HB'
                                GROUP BY item_id ,
                                        promo_id";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string item_id = r["item_id"].ToString();
                    int promo_id = int.Parse(r["promo_id"].ToString());
                    int qty_saleout = int.Parse(r["qty_saleout"].ToString());
                    //int row_id = int.Parse(r["row_id"].ToString());

                    //Lay chiet khau cua Chuong Trinh
                    sQuery = @"SELECT  *
                                FROM    dbo.promotion_item_1
                                WHERE   promo_id = {0}
                                        AND item_id_src = {1}";

                    sQuery = string.Format(sQuery, promo_id, item_id);
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    //Kiem tra Hang hoa co tham gia Chiet Khau
                    if (tb.Rows.Count <= 0)
                    {
                        continue;
                    }

                    int item_id_des = 0;
                    int vol_des = 0;
                    int vol_src = 0;

                    item_id_des = int.Parse(tb.Rows[0]["item_id_des"].ToString());
                    vol_src = int.Parse(tb.Rows[0]["vol_src"].ToString());
                    vol_des = int.Parse(tb.Rows[0]["vol_des"].ToString());

                    int n = qty_saleout / vol_src;

                    if (n > 0)
                    {
                        string storeProc = "[usp_Insertsaleout_detail]";
                        int result = 0;
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(saleout_id))
                            {
                                cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                                cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(saleout_id));
                                cmd.Parameters.AddWithValue("@item_id", item_id_des);
                                cmd.Parameters.AddWithValue("@qty", vol_des * n);
                                cmd.Parameters.AddWithValue("@saleout_type", "KM");
                                cmd.Parameters.AddWithValue("@discount", 0);
                                cmd.Parameters.AddWithValue("@promo_id", promo_id);

                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();
                            }
                        }
                    }
                }

                #endregion Tang Hang 1 Level
            }
            catch (Exception ex)
            {
            }
        }

        public static void TinhToanKM_TangHang_Rule_1(string saleout_id )
        {
            try
            {
                string sQuery = "";

                #region Tang Hang 1 Level

                sQuery = "delete from saleout_detail where saleout_id={0} and saleout_type='KM'";
                sQuery = string.Format(sQuery, saleout_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = @"SELECT
                                        promo_id ,
                                        ISNULL(SUM(qty_saleout),0) AS qty_saleout,
                                        ISNULL(SUM(total_saleout),0) AS total_saleout
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}
                                        AND (promo_type = 'TANGHANG' OR promo_type = 'GWP/PWP' )
                                        AND saleout_type = 'HB'
                                GROUP BY  promo_id";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    int promo_id = int.Parse(r["promo_id"].ToString());
                    int qty_saleout = int.Parse(r["qty_saleout"].ToString());
                    decimal total_saleout = decimal.Parse(r["total_saleout"].ToString());


                    //Kiem tra so luong hang tham gia
                    sQuery = "SELECT * FROM dbo.promotion_rule_des1 WHERE promo_id={0}";
                    sQuery = string.Format(sQuery, promo_id);
                    DataTable tbRule = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    if (tbRule.Rows.Count > 0)
                    {
                        //Neu co dinh nghia hang tang
                        string promo_type = "";
                        decimal from_value = 0;
                        decimal to_value = 0;
                        int object_id = 0;
                        int step = 0;
                        int promo_qty = 0;

                        int nMulti = 0;

                        //Duyet qua tung Rule trong Don Hang
                        foreach (DataRow dr in tbRule.Rows)
                        {

                            //Lay thong tin Rule chi tiet
                            promo_type = (dr["promo_type"] ?? "").ToString();
                            from_value = decimal.Parse((dr["from_value"] ?? "0").ToString());
                            to_value = decimal.Parse((dr["to_value"] ?? "0").ToString());
                            object_id = int.Parse((dr["object_id"] ?? "0").ToString());
                            promo_qty = int.Parse((dr["promo_qty"] ?? "0").ToString());
                            step = int.Parse((dr["step"] ?? "0").ToString());

                            //Kiem tra RULE theo So Luong SKU theo tung LINE
                            if (promo_type == "STOCK UOM" || promo_type == "STOCKUOM")
                            {

                                if (qty_saleout >= from_value && qty_saleout <= to_value)
                                {

                                    if (step == 0)
                                    {
                                        TinhToanKM_TangHang_Insert(saleout_id.ToString(), promo_id.ToString(), object_id.ToString(), promo_qty, 1 );
                                    }
                                    else
                                    {
                                        nMulti = qty_saleout / step;
                                        if (nMulti > 0)
                                        {
                                            TinhToanKM_TangHang_Insert(saleout_id.ToString(), promo_id.ToString(), object_id.ToString(), promo_qty, nMulti);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }



                                }
                            }

                            //Kiem tra RULE theo Doanh so SKU theo tung LINE
                            if (promo_type == "AMOUNT")
                            {

                                if (total_saleout >= from_value && total_saleout <= to_value)
                                {

                                    if (step == 0)
                                    {
                                        TinhToanKM_TangHang_Insert(saleout_id.ToString(), promo_id.ToString(), object_id.ToString(), promo_qty, 1);
                                    }
                                    else
                                    {
                                        nMulti = (int)(total_saleout / step);
                                        if (nMulti > 0)
                                        {
                                            TinhToanKM_TangHang_Insert(saleout_id.ToString(), promo_id.ToString(), object_id.ToString(), promo_qty, nMulti);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }


                                }
                            }



                        }

                    }
                    else
                    {
                        //Khong co dinh nghia hang tang
                        continue;
                    }


                }

                #endregion Tang Hang 1 Level
            }
            catch (Exception ex)
            {
            }
        }
        public static double getGiaKM(string item_id)
        {
            double price = 0;
            string sQuery = @"select saleout_price from item_price where item_id  ={0} ";



            //----------------------
            //using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
            //{
            //    SqlCommand cmd = new SqlCommand(sQuery, conn);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    if (!string.IsNullOrEmpty(saleout_id))
            //    {
            //        cmd.Parameters.AddWithValue("@saleout_id", int.Parse(saleout_id));
            //        conn.Open();
            //      price = Convert.ToDouble(cmd.ExecuteScalar().ToString());
            //        conn.Close();
            //    }
            //}

            //using (SqlConnection myConnection = new SqlConnection(clsCommon.strCon))
            //{
            //    SqlCommand oCmd = new SqlCommand(sQuery, myConnection);
            //    oCmd.Parameters.AddWithValue("@item_id", saleout_id);
            //    myConnection.Open();
            //    using (SqlDataReader oReader = oCmd.ExecuteReader())
            //    {
            //        while (oReader.Read())
            //        {
            //           price = double.Parse(oReader["saleout_price"].ToString());
            //        }

            //        myConnection.Close();
            //    }
            //}



            //000000000000
            sQuery = string.Format(sQuery,item_id);

            DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Columns.Count > 0)
                {
                    DataRow r = ds.Tables[0].Rows[0];
                    price = double.Parse(r["saleout_price"].ToString());

                }
            }
            return price;
        }

        public static void TinhToanKM_TangHang_Insert(string saleout_id, string promo_id, string item_id_des, int vol_des, int nMulti )
        {
            try
            {
                //Tinh hang tang va so luong tang


                if (nMulti > 0)
                {

                    double price =   getGiaKM(item_id_des);
                    string storeProc = "[usp_Insertsaleout_detail]";
                   double result = 0;
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (!string.IsNullOrEmpty(saleout_id))
                        {
                            cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                            cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(saleout_id));
                            cmd.Parameters.AddWithValue("@item_id", item_id_des);
                            cmd.Parameters.AddWithValue("@qty", vol_des * nMulti);
                            cmd.Parameters.AddWithValue("@saleout_type", "KM");
                            cmd.Parameters.AddWithValue("@discount", 0);
                            cmd.Parameters.AddWithValue("@promo_id", promo_id);
                            cmd.Parameters.AddWithValue("@item_price", price);



                            conn.Open();
                            result = double.Parse((cmd.ExecuteScalar().ToString()));
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }





        public static void TinhToanKM_TangHang_Level2(string saleout_id)
        {
            try
            {
                string sQuery = "";

                #region Tang Hang 1 Level

                sQuery = @"SELECT  item_id ,
                                        promo_id ,
                                        ISNULL(SUM(qty_saleout),0) AS qty_saleout
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}
                                        AND promo_type = 'TANGHANG'
                                        AND saleout_type = 'HB'
                                GROUP BY item_id ,
                                        promo_id";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string item_id = r["item_id"].ToString();
                    int promo_id = int.Parse(r["promo_id"].ToString());
                    int qty_saleout = int.Parse(r["qty_saleout"].ToString());
                    //int row_id = int.Parse(r["row_id"].ToString());

                    //Lay chiet khau cua Chuong Trinh
                    sQuery = @"SELECT  *
                                FROM    dbo.promotion_item_1
                                WHERE   promo_id = {0}
                                        AND item_id_src = {1}";

                    sQuery = string.Format(sQuery, promo_id, item_id);
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    //Kiem tra Hang hoa co tham gia Chiet Khau
                    if (tb.Rows.Count <= 0)
                    {
                        continue;
                    }

                    int item_id_des = 0;
                    int vol_des = 0;
                    int vol_src = 0;

                    item_id_des = int.Parse(tb.Rows[0]["item_id_des2"].ToString());
                    vol_src = int.Parse(tb.Rows[0]["vol_src2"].ToString());
                    vol_des = int.Parse(tb.Rows[0]["vol_des2"].ToString());

                    int n = qty_saleout / vol_src;

                    if (n > 0)
                    {
                        #region Xóa CTKM đã tính ở Level 1

                        sQuery = "delete from saleout_detail where saleout_id={0}  and promo_id = {1} and saleout_type='KM'";
                        sQuery = string.Format(sQuery, saleout_id, promo_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        #endregion Xóa CTKM đã tính ở Level 1

                        string storeProc = "[usp_Insertsaleout_detail]";
                        int result = 0;
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(saleout_id))
                            {
                                cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                                cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(saleout_id));
                                cmd.Parameters.AddWithValue("@item_id", item_id_des);
                                cmd.Parameters.AddWithValue("@qty", vol_des * n);
                                cmd.Parameters.AddWithValue("@saleout_type", "KM");
                                cmd.Parameters.AddWithValue("@discount", 0);
                                cmd.Parameters.AddWithValue("@promo_id", promo_id);

                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();
                            }
                        }
                    }
                }

                #endregion Tang Hang 1 Level
            }
            catch (Exception ex)
            {
            }
        }

        public static void TinhToanKM_TangHang_Level3(string saleout_id)
        {
            try
            {
                string sQuery = "";

                #region Tang Hang 1 Level

                sQuery = @"SELECT  item_id ,
                                        promo_id ,
                                        ISNULL(SUM(qty_saleout),0) AS qty_saleout
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}
                                        AND promo_type = 'TANGHANG'
                                        AND saleout_type = 'HB'
                                GROUP BY item_id ,
                                        promo_id";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string item_id = r["item_id"].ToString();
                    int promo_id = int.Parse(r["promo_id"].ToString());
                    int qty_saleout = int.Parse(r["qty_saleout"].ToString());
                    //int row_id = int.Parse(r["row_id"].ToString());

                    //Lay chiet khau cua Chuong Trinh
                    sQuery = @"SELECT  *
                                FROM    dbo.promotion_item_1
                                WHERE   promo_id = {0}
                                        AND item_id_src = {1}";

                    sQuery = string.Format(sQuery, promo_id, item_id);
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    //Kiem tra Hang hoa co tham gia Chiet Khau
                    if (tb.Rows.Count <= 0)
                    {
                        continue;
                    }

                    int item_id_des = 0;
                    int vol_des = 0;
                    int vol_src = 0;

                    item_id_des = int.Parse(tb.Rows[0]["item_id_des3"].ToString());
                    vol_src = int.Parse(tb.Rows[0]["vol_src3"].ToString());
                    vol_des = int.Parse(tb.Rows[0]["vol_des3"].ToString());

                    int n = qty_saleout / vol_src;

                    if (n > 0)
                    {
                        #region Xóa CTKM đã tính ở Level 1

                        sQuery = "delete from saleout_detail where saleout_id={0}  and promo_id = {1} and saleout_type='KM'";
                        sQuery = string.Format(sQuery, saleout_id, promo_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        #endregion Xóa CTKM đã tính ở Level 1

                        string storeProc = "[usp_Insertsaleout_detail]";
                        int result = 0;
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(saleout_id))
                            {
                                cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                                cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(saleout_id));
                                cmd.Parameters.AddWithValue("@item_id", item_id_des);
                                cmd.Parameters.AddWithValue("@qty", vol_des * n);
                                cmd.Parameters.AddWithValue("@saleout_type", "KM");
                                cmd.Parameters.AddWithValue("@discount", 0);
                                cmd.Parameters.AddWithValue("@promo_id", promo_id);

                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();
                            }
                        }
                    }
                }

                #endregion Tang Hang 1 Level
            }
            catch (Exception ex)
            {
            }
        }

        public static void TinhToanKM_TangHang_Level4(string saleout_id)
        {
            try
            {
                string sQuery = "";

                #region Tang Hang 1 Level

                sQuery = @"SELECT  item_id ,
                                        promo_id ,
                                        ISNULL(SUM(qty_saleout),0) AS qty_saleout
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}
                                        AND promo_type = 'TANGHANG'
                                        AND saleout_type = 'HB'
                                GROUP BY item_id ,
                                        promo_id";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string item_id = r["item_id"].ToString();
                    int promo_id = int.Parse(r["promo_id"].ToString());
                    int qty_saleout = int.Parse(r["qty_saleout"].ToString());
                    //int row_id = int.Parse(r["row_id"].ToString());

                    //Lay chiet khau cua Chuong Trinh
                    sQuery = @"SELECT  *
                                FROM    dbo.promotion_item_1
                                WHERE   promo_id = {0}
                                        AND item_id_src = {1}";

                    sQuery = string.Format(sQuery, promo_id, item_id);
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    //Kiem tra Hang hoa co tham gia Chiet Khau
                    if (tb.Rows.Count <= 0)
                    {
                        continue;
                    }

                    int item_id_des = 0;
                    int vol_des = 0;
                    int vol_src = 0;

                    item_id_des = int.Parse(tb.Rows[0]["item_id_des4"].ToString());
                    vol_src = int.Parse(tb.Rows[0]["vol_src4"].ToString());
                    vol_des = int.Parse(tb.Rows[0]["vol_des4"].ToString());

                    int n = qty_saleout / vol_src;

                    if (n > 0)
                    {
                        #region Xóa CTKM đã tính ở Level 1

                        sQuery = "delete from saleout_detail where saleout_id={0}  and promo_id = {1} and saleout_type='KM'";
                        sQuery = string.Format(sQuery, saleout_id, promo_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        #endregion Xóa CTKM đã tính ở Level 1

                        string storeProc = "[usp_Insertsaleout_detail]";
                        int result = 0;
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(saleout_id))
                            {
                                cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                                cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(saleout_id));
                                cmd.Parameters.AddWithValue("@item_id", item_id_des);
                                cmd.Parameters.AddWithValue("@qty", vol_des * n);
                                cmd.Parameters.AddWithValue("@saleout_type", "KM");
                                cmd.Parameters.AddWithValue("@discount", 0);
                                cmd.Parameters.AddWithValue("@promo_id", promo_id);

                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();
                            }
                        }
                    }
                }

                #endregion Tang Hang 1 Level
            }
            catch (Exception ex)
            {
            }
        }

        public static void TinhToanKM_TangHang_Combo(string saleout_id)
        {
            try
            {
                string sQuery = "";

                #region Tang Hang 1 Level

                //sQuery = "delete from saleout_detail where saleout_id={0} and saleout_type='KM'";
                sQuery = string.Format(sQuery, saleout_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = @"SELECT  promo_id ,
                                    item_id ,
                                    SUM(qty) AS qty_saleout
                            FROM    dbo.v_SaleOut
                            WHERE   saleout_id = {0}
                                    AND saleout_type = 'HB'
                                    AND promo_id IN ( SELECT    promo_id
                                                        FROM      dbo.promotion_item_src )
                            GROUP BY promo_id ,
                                    item_id";

                sQuery = string.Format(sQuery, saleout_id);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string item_id = r["item_id"].ToString();
                    int promo_id = int.Parse(r["promo_id"].ToString());
                    int qty_saleout = int.Parse(r["qty_saleout"].ToString());

                    //Tính số Item trong Rule
                    sQuery = @"SELECT  *
                                FROM    dbo.promotion_item_src
                                WHERE   promo_id = {0}
                                        ";

                    sQuery = string.Format(sQuery, promo_id);

                    //Kiem tra so luong Item thuc te
                }

                #endregion Tang Hang 1 Level
            }
            catch (Exception ex)
            {
            }
        }
    }
}