using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WKS.DMS.WEB.Report.rptFiles
{
    public partial class rptPhieuInTonKho : DevExpress.XtraReports.UI.XtraReport
    {
       

       

        public rptPhieuInTonKho()
        {
            InitializeComponent();
        }

         public rptPhieuInTonKho(decimal totalThanhTien,decimal totalChietKhau,decimal totalThanhTienSauCKRow, decimal totalGTCKDH,decimal totalPhaiThu,string SoTienBangChu)
        {
            InitializeComponent();
         


            //xrGTCanThu.Text = totalPhaiThu.ToString();
            
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
