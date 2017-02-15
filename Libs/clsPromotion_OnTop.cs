using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace WKS.DMS.WEB.Libs
{
    public class clsPromotion_OnTop
    {
      
        public static int GetCustomerChannel(string saleoutid)
        {

            try
            {
                string sQuery = @"SELECT  channel_id
                                FROM    dbo.customer
                                WHERE   customer_id IN ( SELECT customer_id
                                                         FROM   dbo.saleout_header
                                                         WHERE  saleout_id = {0} )";

                sQuery = string.Format(sQuery, saleoutid);
                object result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery);
                int b = (int)(result);

                return b;
                
            }
            catch (Exception ex)
            {

                return 0;
            }
            
            return 0;
        }

        public static bool CheckCustomerInRule(string promo_id, string store_id, string customer_id)
        {
            try
            {

                string sQuery = @"IF EXISTS(SELECT customer_id FROM promotion_customer WHERE promo_id={0} AND store_id={1} AND customer_id={2}) 
                                             SELECT 'TRUE' 
                                          ELSE 
                                             SELECT 'FALSE'";

                sQuery = string.Format(sQuery, promo_id, store_id, customer_id);
                object result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery);
                bool b = bool.Parse(result.ToString());

                return b;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        

        public static void TinhToanKM_TangHang_Insert(string saleout_id, string promo_id, string item_id_des, int vol_des, int nMulti)
        {
            try
            {
                //Tinh hang tang va so luong tang


                if (nMulti > 0)
                {
                    string storeProc = "[usp_Insertsaleout_detail]";
                    int result = 0;
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;


                        double price = clsPromotion.getGiaKM(item_id_des);


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
                            //result = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }


        public static bool CheckCustomerChannelInRule(string promo_id, string store_id, string channel_id)
        {
            try
            {

                string sQuery = @"IF EXISTS(SELECT  channel_id
                                                    FROM    dbo.promotion_store
                                                    WHERE   store_id = {0}
                                                            AND channel_id = {1}
		                                                    AND promo_id = {2}) 
                                             SELECT 'TRUE' 
                                          ELSE 
                                             SELECT 'FALSE'";

                sQuery = string.Format(sQuery, store_id, channel_id,promo_id);
                object result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery);
                bool b = bool.Parse(result.ToString());

                return b;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        //Don hang tren 1 Trieu, tang hang
        public static void TinhToanKM_TangHang_Ontop(string store_id, string saleout_id, string customer_id, int ngaygiaodich)
        {
            try
            {
                //Xoa CTKM Ontop Hiện tại
                string sQuery = "";




                //Kiem tra Khach hang co thuoc nhom LP,WSP,SP hay khong
                //if (GetCustomerChannel(saleout_id) != 1 && 
                //    GetCustomerChannel(saleout_id) != 2 && 
                //    GetCustomerChannel(saleout_id) != 4 &&
                //    GetCustomerChannel(saleout_id) != 9)
                //{
                //    return;
                //}




                //Lay danh sach các CTKM Ontop Áp dụng cho NPP

                sQuery = @"SELECT  promo_id
                        FROM    dbo.promotion
                        WHERE   promo_type = 'ONTOP'
                                AND promo_status = 'APPROVED'
                                AND promo_id IN ( SELECT    promo_id
                                                  FROM      dbo.promotion_store
                                                  WHERE     store_id = {0}


                                                    UNION ALL
                                                    SELECT promo_id
                                                    FROM promotion_customer
                                                    WHERE store_id={1}

            
                                                )
                                AND {2} BETWEEN start_date_numb
                                             AND     end_date_numb
						                          ";

                sQuery = string.Format(sQuery, store_id, store_id, ngaygiaodich);
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);
                string promo_id = "";
                //Duyet qua tung CTKM Ontop
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    promo_id = r["promo_id"].ToString();

                   //Kiem tra Khach hang có nam torng danh sach cho phep hay khong doi voi CTKM 101
                    if (promo_id=="101")
                    {
                        if (!CheckCustomerInRule(promo_id,store_id,customer_id))
                        {
                            return;
                        }
                    }

                    
                    //Kiem tra khach hang co nam trong nhom KH duoc phep tham gia KM hay khong
                    string channel_id = GetCustomerChannel(saleout_id).ToString();
                    if (!CheckCustomerChannelInRule(promo_id,store_id,channel_id))
                    {
                        continue;
                    }


                    //Tinh thanh tien don hang

                    #region TinhThanhTien
                    decimal thanhtien = 0;

                    sQuery = @"SELECT   ISNULL(SUM(GTBan), 0)
                                FROM    dbo.v_SaleOut
                                WHERE   saleout_id = {0}
                                        AND saleout_type = 'HB'
                                        AND item_id IN ( SELECT item_id
                                                         FROM   dbo.promotion_rule_src2
                                                         WHERE  promo_id = {1} )";

                    sQuery = string.Format(sQuery, saleout_id, promo_id);
                    object result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery);
                    thanhtien = decimal.Parse(result.ToString());


                    if (thanhtien <= 0)
                    {
                        continue;
                    }

                    #endregion


                   


                    //Kiem tra so luong hang tham gia
                    sQuery = "SELECT * FROM dbo.promotion_rule_des2 WHERE promo_id={0}";
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



                            //Kiem tra RULE theo Doanh so SKU theo tung LINE
                            if (promo_type == "AMOUNT")
                            {
                                if (thanhtien >= from_value && thanhtien <= to_value)
                                {

                                    if (step == 0)
                                    {
                                        TinhToanKM_TangHang_Insert(saleout_id.ToString(), promo_id.ToString(), object_id.ToString(), promo_qty, 1);
                                    }
                                    else
                                    {
                                        nMulti = (int)(thanhtien / step);
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

            }
            catch (Exception ex)
            {


            }


        }
        

       

       
    }
}