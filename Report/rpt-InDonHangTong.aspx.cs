using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_InDonHangTong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();
                rdpReportDate.SelectedDate = DateTime.Now;
                
            }
            BindData();

           
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            try
            {

                // Show the report's preview.               

                int report_date = clsCommon.ConvertDateToNumber(rdpReportDate.SelectedDate.Value);
               

                // Create a report. 
                WKS.DMS.WEB.Report.rptFiles.rptInPhieuXuatKho_All rpt = new WKS.DMS.WEB.Report.rptFiles.rptInPhieuXuatKho_All();
                string sQuery = "";

                Dataset_DATA ds = new Dataset_DATA();


                string storeProc = "[sp_rpt_InPhieuXuatKho_AIO_Header]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@print_date", report_date);        


                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    
                    da.Fill(ds,"template_order_header");
                    conn.Close();                 

                }


                string storeProc2 = "[sp_rpt_InPhieuXuatKho_AIO_Detail]";
                using (SqlConnection conn2 = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd2 = new SqlCommand(storeProc2, conn2);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd2.Parameters.AddWithValue("@print_date", report_date);


                    conn2.Open();

                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

                    da2.Fill(ds, "template_order_detail");
                    conn2.Close();

                }


                foreach (DataRow r in ds.Tables["template_order_header"].Rows)
                {
                    string sThanhTien = r["GTCanThu"].ToString();
                    r["ThanhChu"] = clsCommon.DoiSoThanhChu1(decimal.Parse(sThanhTien));
                    ds.AcceptChanges();
                }

                rpt.DataSource = ds;
                ASPxDocumentViewer1.Report = rpt;


              

            }
            catch (Exception ex)
            {


            }
        }

        public void BindList()
        {
            try
            {
                string sQuery = "";

                sQuery = @"SELECT  a.store_id ,
                                               store_name
                                        FROM    dbo.store AS a

                                                WHERE a.store_id  IN (
                                                                SELECT  store_id
                                                                FROM    dbo.fn_GetStore_By_UserID({0}) )

                                        ";

                sQuery = string.Format(sQuery, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = tb;
                cbxStore.DataBind();

                cbxStore_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void cbxStore_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
            }
        }
    }
}