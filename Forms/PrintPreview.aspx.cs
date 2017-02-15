using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WKS.DMS.WEB.Forms
{
    public partial class PrintPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BindData_PhieuXuatKho();
        }



        public void BindData_PhieuXuatKho()
        {

            String distID = Request.QueryString["p5"].Trim();
            if (distID.Equals("1")){


                try
                {

                    string _pKey = "";
                    string _saleout_id = "";
                    decimal _totalThanhTien = 0;
                    decimal _totalChietKhau = 0;
                    decimal _totalThanhTienSauCKRow = 0;
                    decimal _totalGTCKDH = 0;
                    decimal _totalPhaiThu = 0;
                    string _SoTienBangChu = "";
                    string _OnTopDiscoount = "0";
                    string _TotalOntopDiscount = "0";



                    _pKey = Request.QueryString["p0"];
                    _saleout_id = Request.QueryString["p1"];
                    _totalChietKhau = decimal.Parse(Request.QueryString["p2"]);
                    _totalGTCKDH = decimal.Parse(Request.QueryString["p3"]);
                    _totalPhaiThu = decimal.Parse(Request.QueryString["p4"]);
                    _SoTienBangChu = clsCommon.DoiSoThanhChu1(_totalPhaiThu);


                    Report.rptFiles.rptPhieuXuatKho_InLaser report = new Report.rptFiles.rptPhieuXuatKho_InLaser(_totalThanhTien, _totalChietKhau, _totalThanhTienSauCKRow, _totalGTCKDH, _totalPhaiThu, _SoTienBangChu);

                    report.XmlDataPath = clsCommon.XMLPath + "/xmlPhieuXuatKho.xml";

                    string storeProc = "[sp_rpt_InPhieuXuatKho]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@saleout_id", _saleout_id);
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable data = new DataTable();
                        da.Fill(data);
                        conn.Close();
                        report.DataSource = data;
                        ASPxDocumentViewer1.Report = report;



                    }




                }
                catch (Exception ex)
                {


                }
               


                }
            // redirect to MT form : 
            else if (distID.Equals("2"))
            {


                try
                {

                    string _pKey = "";
                    string _saleout_id = "";
                    decimal _totalThanhTien = 0;
                    decimal _totalChietKhau = 0;
                    decimal _totalThanhTienSauCKRow = 0;
                    decimal _totalGTCKDH = 0;
                    decimal _VAT = 0; 
                    decimal _totalPhaiThu = 0;
                    string _SoTienBangChu = "";
                    string _OnTopDiscoount = "0";
                    string _TotalOntopDiscount = "0";



                    _pKey = Request.QueryString["p0"];
                    _saleout_id = Request.QueryString["p1"];
                    _totalChietKhau = decimal.Parse(Request.QueryString["p2"]);
                    _totalGTCKDH = decimal.Parse(Request.QueryString["p3"]);
                    _totalPhaiThu = decimal.Parse(Request.QueryString["p4"]);
                    _SoTienBangChu = clsCommon.DoiSoThanhChu1(_totalPhaiThu);
                    _VAT = decimal.Parse(Request.QueryString["p6"]);


                    Report.rptFiles.rptPhieuXuatKho_InLaser_mt report = new Report.rptFiles.rptPhieuXuatKho_InLaser_mt(_totalThanhTien, _totalChietKhau, _totalThanhTienSauCKRow, _totalGTCKDH, _totalPhaiThu, _VAT, _SoTienBangChu);

                    report.XmlDataPath = clsCommon.XMLPath + "/xmlPhieuXuatKho_mt.xml";

                    string storeProc = "[sp_rpt_InPhieuXuatKho_mt]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@saleout_id", _saleout_id);
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable data = new DataTable();
                        da.Fill(data);
                        conn.Close();
                        report.DataSource = data;
                        ASPxDocumentViewer1.Report = report;



                    }




                }
                catch (Exception ex)
                {


                }



            }


        }


        }


}