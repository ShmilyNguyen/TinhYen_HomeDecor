using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WKS.DMS.WEB.Report.rptFiles
{
    public partial class rptPhieuGiaoHang_InLaser : DevExpress.XtraReports.UI.XtraReport
    {

        decimal _totalThanhTien = 0;
        decimal _totalChietKhau = 0;
        decimal _totalThanhTienSauCKRow = 0;
        decimal _totalGTCKDH = 0;
        decimal _totalPhaiThu = 0;
        string _SoTienBangChu = "";

        public rptPhieuGiaoHang_InLaser()
        {
            InitializeComponent();
        }

        public rptPhieuGiaoHang_InLaser(decimal totalThanhTien,decimal totalChietKhau,decimal totalThanhTienSauCKRow, decimal totalGTCKDH,decimal totalPhaiThu,string SoTienBangChu)
        {
            InitializeComponent();

            _totalThanhTien = totalThanhTien;
            _totalChietKhau = totalChietKhau;
            _totalThanhTienSauCKRow = totalThanhTienSauCKRow;
            _totalGTCKDH = totalGTCKDH;
            _totalPhaiThu = totalPhaiThu;
            _SoTienBangChu = SoTienBangChu;


            

        }


       

       


        private void xrThanhTien_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

           


        }

        private void xrThanhTien_SummaryRowChanged(object sender, EventArgs e)
        {
            

        }





        private void xrCKDH_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void xrGTCKDH_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void xrGTCK_SummaryRowChanged(object sender, EventArgs e)
        {

        }

        private void xrGTCK_TextChanged(object sender, EventArgs e)
        {

        }

        private void xrThanhTien_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
