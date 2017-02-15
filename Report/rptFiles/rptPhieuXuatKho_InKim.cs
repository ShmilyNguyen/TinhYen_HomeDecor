using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace WKS.DMS.WEB.Report.rptFiles
{
    public partial class rptPhieuXuatKho_InKim : DevExpress.XtraReports.UI.XtraReport
    {
        Int64 totalThanhTien = 0;
        Int64 totalChietKhau = 0;
        Int64 totalThanhTienSauCKRow = 0;
        Int64 totalGTCKDH = 0;

        public rptPhieuXuatKho_InKim()
        {
            InitializeComponent();
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
