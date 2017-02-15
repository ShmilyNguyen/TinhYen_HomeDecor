using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace WKS.DMS.WEB.Print
{
    public partial class rpt_InPhieuXuat : System.Web.UI.Page
    {
        public string id = "";
       
        public decimal _GTCK = 0;
        public Int32 _ThanhTien = 0;
        public string _SoTienBangChu = "";




        public float _OntopDiscount = 0;
        public Int32 _GTChietKhauNPP = 0;

        public Int32 _GTChietKhauDongHang = 0;
        public Int32 _GTBan = 0;


        public string _ngay = "";
        public string _thang = "";
        public string _nam = "";

        public string _customer_id = "";
        public string _saleout_code = "";
        public string _customer_name = "";
        public string _address_full = "";
        public string _phone = "";



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
                                    SELECT ROW_NUMBER() OVER ( ORDER BY item_code DESC ) AS Stt ,
                                        A.*
                                 FROM   ( SELECT    saleout_code ,
                                                    customer_id ,
                                                    DAY(trans_date_gmt) AS ngay ,
                                                    MONTH(trans_date_gmt) AS thang ,
                                                    YEAR(trans_date_gmt) AS nam ,
                                                    item_code ,
                                                    item_name ,
                                                    item_price ,
                                                    '' AS unit_name ,
                                                    qty_saleout + qty_promo AS SLYeuCau ,
                                                    discount,
                                                    total_saleout AS ThanhTien,
                                                    b.store_name,
                                                    b.store_address,
                                                    b.phone
                                          FROM      dbo.v_SaleOut AS a
                                          LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
                                          WHERE     saleout_id = {0}
                                        ) AS A
                                 ORDER BY Stt ,
                                        SLYeuCau
                                                                                       ";

                sQuery = string.Format(sQuery, id);
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                

                rptChiTiet.DataSource = data;
                rptChiTiet.DataBind();


                _saleout_code = data.Rows[0]["saleout_code"].ToString();
                _customer_id = data.Rows[0]["customer_id"].ToString();
                _ngay = data.Rows[0]["ngay"].ToString();
                _thang = data.Rows[0]["thang"].ToString();
                _nam = data.Rows[0]["nam"].ToString();

                _tennhaphanphoi = data.Rows[0]["store_name"].ToString();
                _diachi = data.Rows[0]["store_address"].ToString();
                _dienthoai = data.Rows[0]["phone"].ToString();



                GetThanhTien();

                sQuery = @"  SELECT    customer_id ,
                                            customer_code ,
                                            customer_name ,
                                            '(' + phone + ')--' + ISNULL(add_number, '') + '-'
                                            + ISNULL(address, '') + '-' + ISNULL(province_name, '') + '-'
                                            + ISNULL(district_name, '') + '-' + ISNULL(ward_name, '') + '-'
                                            + ISNULL(street_name, '') AS address_full
                                  FROM      customer AS a
                                            LEFT JOIN dbo.geo_province AS b ON a.province_id = b.geo_province_id
                                            LEFT JOIN dbo.geo_district AS c ON a.district_id = c.geo_district_id
                                            LEFT JOIN dbo.geo_ward AS d ON a.ward_id = d.geo_ward_id
                                            LEFT JOIN dbo.geo_street AS e ON a.street_id = e.geo_street_id
                                  WHERE     a.customer_id = {0}";

                sQuery = string.Format(sQuery, _customer_id);
                DataTable data2 = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                _customer_name = data2.Rows[0]["customer_name"].ToString();
                _address_full = data2.Rows[0]["address_full"].ToString();

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
                string sQuery = @"[usp_saleout_gettotalvalues]";
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@saleout_id", id);
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                foreach (DataRow r in data.Rows)
                {
                    CultureInfo us = new CultureInfo("en-US");
                    _GTBan = Int32.Parse(r["GTBan"].ToString());
                    _GTChietKhauDongHang = Int32.Parse(r["GTChietKhauDongHang"].ToString());
                    _OntopDiscount = float.Parse(r["OntopDiscount"].ToString());
                    _GTChietKhauNPP = Int32.Parse(r["GTChietKhauNPP"].ToString());
                    _ThanhTien = Int32.Parse(r["GTThanhToan"].ToString());
                    _SoTienBangChu = clsCommon.DoiSoThanhChu1(_ThanhTien);
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}