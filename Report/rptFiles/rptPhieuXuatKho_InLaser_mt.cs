using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WKS.DMS.WEB.Report.rptFiles
{
    public partial class rptPhieuXuatKho_InLaser_mt : DevExpress.XtraReports.UI.XtraReport
    {

        decimal _totalThanhTien = 0;
        decimal _totalChietKhau = 0;
        decimal _totalThanhTienSauCKRow = 0;
        decimal _totalGTCKDH = 0;
        decimal _VAT = 0;
        decimal _GTVAT = 0;
        decimal _totalPhaiThu = 0;
        string _SoTienBangChu = "";

        public rptPhieuXuatKho_InLaser_mt()
        {
            InitializeComponent();
        }

        public rptPhieuXuatKho_InLaser_mt(decimal totalThanhTien,decimal totalChietKhau,decimal totalThanhTienSauCKRow, decimal totalGTCKDH,decimal totalPhaiThu,decimal gtVAT,string SoTienBangChu)
        {
            InitializeComponent();

            _totalThanhTien = totalThanhTien;
            _totalChietKhau = totalChietKhau;
            _totalThanhTienSauCKRow = totalThanhTienSauCKRow;
            _totalGTCKDH = totalGTCKDH;
            _totalPhaiThu = totalPhaiThu;
            _SoTienBangChu = SoTienBangChu;
            _GTVAT = gtVAT;


            //xrGTCanThu.Text = totalPhaiThu.ToString();
            xrGTCanThu.Text = String.Format("{0:#,###.##}", totalPhaiThu);
            xrThanhChu.Text = SoTienBangChu;
           
        //    xrRow_GTCK.Text = _totalChietKhau.ToString();


        }

        public rptPhieuXuatKho_InLaser_mt(decimal _totalThanhTien, decimal _totalChietKhau, decimal _totalThanhTienSauCKRow, decimal _totalGTCKDH, decimal _totalPhaiThu, string _SoTienBangChu, decimal _GTVAT)
        {
            this._totalThanhTien = _totalThanhTien;
            this._totalChietKhau = _totalChietKhau;
            this._totalThanhTienSauCKRow = _totalThanhTienSauCKRow;
            this._totalGTCKDH = _totalGTCKDH;
            this._totalPhaiThu = _totalPhaiThu;
            this._SoTienBangChu = _SoTienBangChu;
            this._GTVAT = _GTVAT;


        }

        private void xrThanhTien_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

           


        }

        private void xrThanhTien_SummaryRowChanged(object sender, EventArgs e)
        {
            

        }




        private void xrGTVAT_TextChanged(object sender, EventArgs e)
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
