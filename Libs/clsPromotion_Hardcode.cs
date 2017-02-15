using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WKS.DMS.WEB.Libs
{
    public class clsPromotion_Hardcode
    {
        #region Hardcode CTKm Hamper va Base




        public static bool KiemTraCoKM_Base_Hamper(string saleout_id)
        {
            //Kiem tra Don Hang co KM Base hoac Hamper 1021,1031 (72,73)
            string sQuery = @"IF EXISTS ( SELECT  promo_id
                                            FROM    dbo.saleout_detail
                                            WHERE   saleout_id = {0}
                                                    AND promo_id IN ( 957, 958, 1027,1028, 1030 ) AND saleout_type='HB' ) 
                                    SELECT  1 
                                ELSE 
                                    SELECT  0";
            sQuery = string.Format(sQuery, saleout_id);

            string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();

            //Neu khong co
            if (result.Contains("0"))
            {
                return false;
            }
            else
            {
                return true;
            }


        }


        public static bool KiemTraSanPhamKhongThuoc_Base_Hamper(string saleout_id)
        {
            //Kiem tra Don Hang co KM Base hoac Hamper 1021,1031 (72,73)
            string sQuery = @"IF EXISTS ( SELECT  item_id
            FROM    dbo.saleout_detail
            WHERE   saleout_id = {0}
                    AND saleout_type = 'HB'
                    AND item_id NOT IN (
                    SELECT  object_id
                    FROM    dbo.promotion_rule_src1 AS a
                    WHERE   promo_id IN ( 957, 958, 1027, 1028, 1030 )
                    UNION ALL
                    SELECT  item_id
                    FROM    dbo.item
                    WHERE   size IN (
                            SELECT  size
                            FROM    dbo.promotion_rule_src1 AS a
                            WHERE   promo_id IN ( 957, 958, 1027, 1028, 1030 ) ) )
                    AND item_id IS NOT NULL
                    AND item_id > 0 ) 
    SELECT  1 
ELSE 
    SELECT  0
    
    
  ";
            sQuery = string.Format(sQuery, saleout_id);

            string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();

            //Neu khong co
            if (result.Contains("0"))
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        #endregion

    }
}