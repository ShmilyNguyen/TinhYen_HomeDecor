using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class ucChotTonKho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int dayofmonth = DateTime.Now.Day;



                //Danh cho User
                if (KiemTraNgayMoFormChotKho(dayofmonth)
                    )
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }


                //Danh cho Admin
                if (Session["role"].ToString().Equals("ADMIN"))
                {
                    if (KiemTraNgayMoFormChotKho(dayofmonth)
                       )
                    {
                        Panel1.Visible = true;
                    }
                    else
                    {
                        Panel1.Visible = false;
                    }   
                }


                //Kiem tra Unlock
                if (CheckUnlock(Session["userid"].ToString()))
                {
                    Panel1.Visible = true;
                }


                BindData();
            }
        }

        public bool KiemTraNgayMoFormChotKho(int ngay)
        {
            string sQuery = @"IF EXISTS ( SELECT  dayofmonth
                                                    FROM    dbo.inventory_dayofclosing
                                                    WHERE   dayofmonth = {0} ) 
                                            SELECT  'true'
                                        ELSE 
                                            SELECT  'false'";

            sQuery = string.Format(sQuery, ngay);

            bool ret = bool.Parse(SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());

            return ret;
        }


        public bool CheckUnlock(string user_id)
        {
            string sQuery = @"IF EXISTS ( SELECT  [user_id]
            FROM    dbo.inventory_closing_unlock
                                    WHERE   [user_id] = {0} ) 
                            SELECT  'true'
                        ELSE 
                            SELECT  'false'";

            sQuery = string.Format(sQuery,user_id);

            bool ret = bool.Parse( SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString());

            return ret;
        }

        public void BindData()
        {
            try
            {
                if (DateTime.Now.Month == 1)
                {
                    ddlNam.SelectedValue = (DateTime.Now.Year -1).ToString();
                }
                else
                {
                    ddlNam.SelectedValue = DateTime.Now.Year.ToString();
                }

                int dayofmonth = DateTime.Now.Day;

                if (dayofmonth == 30 || dayofmonth == 31)
                {
                    ddlThang.SelectedValue = (DateTime.Now.Month).ToString();
                }

                if (KiemTraNgayMoFormChotKho(dayofmonth)
                    )
                {
                    if (DateTime.Now.Month == 1)
                    {
                        ddlThang.SelectedValue = "12";
                    }
                    else
                    {
                        ddlThang.SelectedValue = (DateTime.Now.Month - 1).ToString();
                    }
                }

                string sQuery = @"SELECT  a.store_id ,
                                               store_name
                                        FROM    dbo.store AS a

                                                WHERE a.store_id  IN (
                                                                SELECT  store_id
                                                                FROM    dbo.fn_GetStore_By_UserID({0}) )


                                AND a.store_id NOT IN ( SELECT  store_id
                                                        FROM    dbo.inventory_closing_monthly
                                                        WHERE   data_month = {1}
                                                                AND data_year = {2} )


                                                                        ";

                sQuery = string.Format(sQuery, Session["userid"], ddlThang.SelectedValue, ddlNam.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                ddlNPP.DataSource = tb;
                ddlNPP.DataBind();


            }
            catch (Exception ex)
            {
            }
        }

        

        protected void btnClosingStock_Click(object sender, EventArgs e)
        {
            try
            {
                //Tinh ton kho
                int Nam = 0;
                int NamKeTiep = 0;
                int ThangHienTai = 0;
                int ThangKeTiep = 0;
                int store_id = 0;
                int user_id = 0;

                
               
                ThangHienTai = int.Parse(ddlThang.Text);

                if (ThangHienTai ==12)
                {
                    ThangKeTiep =  1;
                    Nam = int.Parse(ddlNam.Text) ;
                    NamKeTiep = Nam + 1;
                }
                else
                {
                    ThangKeTiep = ThangHienTai + 1;
                    Nam = int.Parse(ddlNam.Text);
                    NamKeTiep = Nam;
                }
                

                store_id = int.Parse(ddlNPP.SelectedValue.ToString());
                user_id = int.Parse(Session["userid"].ToString());

                //Kiem tra Thang, Nam User dang chon co dung hay khong


                if (ThangHienTai == 12 && ThangKeTiep ==1)
                {
                    goto jump_here;
                }

                int sys_month = DateTime.Now.Month;
                if (ThangHienTai > sys_month)
                {
                    RadWindowManager1.RadAlert("Bạn không được phép chốt kho trước tháng hiện tại !", 330, 180, "Thông báo", null, null);
                    return;
                }
                if (ThangHienTai == sys_month && DateTime.Now.Day <= 30)
                {
                    RadWindowManager1.RadAlert("Bạn chưa được phép chốt kho cho tháng " + ThangHienTai + " !", 330, 180, "Thông báo", null, null);
                    return;
                }

            jump_here:


                //Kiem tra NPP da Confirm het don hang hay chua

                string sQuery = @" SELECT COUNT(saleout_id) AS TotalOrder
                                     FROM   dbo.v_SaleOut
                                     WHERE  GTBan <> 0 AND data_month = " + ThangHienTai + @"
                                            AND data_year = " + Nam + @"
                                            AND is_confirmed = 0
                                            AND store_id = " + ddlNPP.SelectedValue;

                string totalOrder = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                if (totalOrder != "0")
                {
                    RadWindowManager1.RadAlert("Vui lòng duyệt hết đơn hàng trước khi chốt kho !", 330, 180, "Thông báo", null, null);
                    return;
                }





                //Kiem tra thang hien tai da chot ton kho hay chua
                sQuery = "";

                sQuery = @"IF ( EXISTS ( SELECT    store_id
                                          FROM      dbo.inventory_closing_monthly
                                          WHERE     store_id = {0}
                                                    AND data_month = {1}
                                                    AND data_year = {2}
                                                    AND is_lock = 1 ) )
                                SELECT  '1'
                            ELSE
                                SELECT  '0'";

                sQuery = string.Format(sQuery, ddlNPP.SelectedValue, ddlThang.SelectedValue, ddlNam.SelectedValue);
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                if (result == "1")
                {
                    RadWindowManager1.RadAlert("Đơn hàng đã chốt, Bạn không thể chốt lại , vui lòng liên hệ Admin công ty để được hỗ trợ!", 330, 180, "Thông báo", null, null);
                }

                if (result == "0")
                {
                    int ret = Closing_Inventory(user_id, ThangHienTai, Nam, ThangKeTiep,NamKeTiep, store_id);
                    if (ret == 0)
                    {
                        //Chot ton kho
                        sQuery = "delete from inventory_closing_monthly where store_id= {0} and data_month={1} and data_year={2}";
                        sQuery = string.Format(sQuery, ddlNPP.SelectedValue, ddlThang.SelectedValue, ddlNam.SelectedValue);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        sQuery = @"INSERT  INTO dbo.inventory_closing_monthly
                                        ( store_id ,
                                          user_id ,
                                          closing_date ,
                                          data_month ,
                                          data_year ,
                                          is_lock
                                        )
                                VALUES  ( {0} , -- store_id - int
                                          {1} , -- user_id - int
                                          GETDATE() , -- closing_date - datetime
                                          {2} , -- data_month - int
                                          {3} , -- data_year - int
                                          1  -- is_lock - bit
                                        )
                                ";


                        sQuery = string.Format(sQuery, ddlNPP.SelectedValue, Session["userid"], ddlThang.SelectedValue, ddlNam.SelectedValue);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);



                        sQuery = "delete from inventory_closing_unlock where store_id= {0} ";
                        sQuery = string.Format(sQuery, ddlNPP.SelectedValue);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);



                        RadWindowManager1.RadAlert("Chốt kho tháng " + ThangHienTai + "/ " + Nam + " thành công , Bạn có thể nhập kho cho tháng " + ThangKeTiep + "/ " + NamKeTiep + " !", 330, 180, "Thông báo", null, null);
                    }

                    if (ret == 2)
                    {
                        RadWindowManager1.RadAlert("Chốt kho tháng " + ThangHienTai + "/ " + Nam + " thất bại ! Báo cáo tồn kho bị âm, vui lòng kiểm tra lại tồn kho trước kho chốt kho!", 330, 180, "Thông báo", null, null);
                    }


                    if (ret == 3)
                    {
                        RadWindowManager1.RadAlert("Chốt kho tháng " + ThangHienTai + "/ " + Nam + "bị lỗi !!! vui lòng liên hệ Admin để được hỗ trợ !", 330, 180, "Thông báo", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static int Closing_Inventory(int UserID, int Thang, int Nam, int ThangKeTiep, int NamKeTiep, int store_id)
        {
            try
            {
                string sQuery = "delete from inventory_closing where month={0} and year={1} and store_id={2}";

                sQuery = string.Format(sQuery, ThangKeTiep, NamKeTiep, store_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                DataTable data = new DataTable();

                try
                {
                    string storeProc = "[usp_rpt_BaoCaoTonKho_By_Store]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@user_id", UserID);
                        cmd.Parameters.AddWithValue("@store_id", store_id);
                        cmd.Parameters.AddWithValue("@report_month", Thang);
                        cmd.Parameters.AddWithValue("@report_year", Nam);
                        cmd.Parameters.AddWithValue("@report_type", "GIABAN");

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(data);
                        conn.Close();


                        //Kiem tra ton kho am
                        foreach (DataRow r in data.Rows)
                        {
                            int qty_saleout = int.Parse((r["SLTonCuoiThucTe_HangBan"] ?? "0").ToString());

                            int qty_promo = int.Parse((r["SLTonCuoiThucTe_HangKM"] ?? "0").ToString());

                            if (qty_saleout + qty_promo < 0)
                            {
                                return 2;
                            }
                        }


                        foreach (DataRow r in data.Rows)
                        {
                            string _store_id = (r["store_id"] ?? "").ToString();

                            if (int.Parse(_store_id) == store_id)
                            {
                                string item_id = (r["item_id"] ?? "").ToString();
                                int qty_saleout = int.Parse((r["SLTonCuoiThucTe_HangBan"] ?? "0").ToString());

                                int qty_promo = int.Parse((r["SLTonCuoiThucTe_HangKM"] ?? "0").ToString());

                                decimal total_saleout = Decimal.Parse((r["GTTonCuoi_HangBan"] ?? "0").ToString());

                                decimal total_promo = Decimal.Parse((r["GTTonCuoi_HangKM"] ?? "0").ToString());

                                sQuery = @"INSERT INTO dbo.inventory_closing
                                                        ( store_id ,
                                                          item_id ,
                                                          qty_saleout ,
                                                          qty_promo ,
                                                          day ,
                                                          month ,
                                                          year ,
                                                          total_saleout ,
                                                          total_promo ,
                                                          adjusted_date ,
                                                          modified_by ,
                                                          note ,
                                                          adjust_type
                                                        )
                                                VALUES  ( " + _store_id + @" , -- store_id - int
                                                          " + item_id + @" , -- item_id - int
                                                          " + qty_saleout + @" , -- qty_saleout - int
                                                          " + qty_promo + @" , -- qty_promo - int
                                                          0 , -- day - int
                                                          " + ThangKeTiep + @" , -- month - int
                                                          " + NamKeTiep + @" , -- year - int
                                                          " + total_saleout + @" , -- total_saleout - decimal
                                                          " + total_promo + @" , -- total_promo - decimal
                                                          NULL , -- adjusted_date - date
                                                          " + UserID + @" , -- modified_by - int
                                                          N'' , -- note - nvarchar(50)
                                                          ''  -- adjust_type - varchar(20)
                                                        )";

                                //sQuery = string.Format(sQuery, store_id, item_id, qty_saleout, qty_promo, 0, ThangKeTiep, Nam, total_saleout, total_promo);
                                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                            }
                        }
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    return 3;
                }
            }
            catch (Exception ex)
            {
                return 3;
            }
        }
    }
}