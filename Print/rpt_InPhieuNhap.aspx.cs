using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Globalization;
namespace WKS.DMS.WEB.Print
{
    public partial class rpt_InPhieuNhap : System.Web.UI.Page
    {
        public string id = "";

        public string _sellin_code = "";
        public decimal _ThanhTien = 0;
        public string _SoTienBangChu = "";

        public string _ngay = "";
        public string _thang = "";
        public string _nam = "";

        public string _tennhaphanphoi = "";
        public string _diachi = "";
        public string _dienthoai = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            try
            {
                id = Request.QueryString["id"];
                string sQuery = @"
                                    SELECT  ROW_NUMBER() OVER ( ORDER BY item_code DESC ) AS Stt ,
                                            A.*
                                    FROM    ( SELECT    sellin_code ,
                                                        '' AS customer_id ,
                                                        DAY(trans_date_gmt) AS ngay ,
                                                        MONTH(trans_date_gmt) AS thang ,
                                                        YEAR(trans_date_gmt) AS nam ,
                                                        item_code ,
                                                        item_name ,
                                                        sellin_price AS item_price ,
                                                        '' AS unit_name ,
                                                        qty_sellin + qty_promo AS SLYeuCau ,
                                                        total_sellin AS ThanhTien ,
                                                        b.store_name ,
                                                        b.store_address ,
                                                        b.phone
                                              FROM      dbo.v_SellIn AS a
                                                        LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
                                              WHERE     sellin_id = {0}
                                            ) AS A
                                    ORDER BY Stt ,
                                            SLYeuCau
                                                       ";

                sQuery = string.Format(sQuery, id);
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];



                rptChiTiet.DataSource = data;
                rptChiTiet.DataBind();


              
                _ngay = data.Rows[0]["ngay"].ToString();
                _thang = data.Rows[0]["thang"].ToString();
                _nam = data.Rows[0]["nam"].ToString();

                _tennhaphanphoi = data.Rows[0]["store_name"].ToString();
                _diachi = data.Rows[0]["store_address"].ToString();
                _dienthoai = data.Rows[0]["phone"].ToString();


                GetThanhTien();

                _sellin_code = data.Rows[0]["sellin_code"].ToString();

            }
            catch (Exception ex)
            {

            }
        }

        public void GetThanhTien()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"[usp_sellin_gettotalvalues]";
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@sellin_id", id);
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                foreach (DataRow r in data.Rows)
                {
                    CultureInfo us = new CultureInfo("en-US");
                    //txtThanhTien.Text = r["GTBan"].ToString();
                    //txtGTCK.Text = r["GTChietKhauDongHang"].ToString();
                    //txtOntopDiscount.Text = r["OntopDiscount"].ToString();
                    //txtTotalOntopDiscount.Text = r["GTChietKhauNPP"].ToString();
                    _ThanhTien = Decimal.Parse(r["GTNhap"].ToString());
                    _SoTienBangChu = clsCommon.DoiSoThanhChu1(_ThanhTien);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}