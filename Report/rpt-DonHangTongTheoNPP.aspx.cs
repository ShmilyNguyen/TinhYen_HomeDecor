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
    public partial class rpt_DonHangTongTheoNPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();
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

                int Ngay = clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value);

                if (cbxEmployee.SelectedValue=="")
                {
                    // Create a report. 
                    WKS.DMS.WEB.Report.rptFiles.rpt_DonHangTongTheoNPP rpt = new WKS.DMS.WEB.Report.rptFiles.rpt_DonHangTongTheoNPP();
                    rpt.XmlDataPath = clsCommon.XMLPath + "/xmlDonHangTongTheoNgay_NPP.xml";
                    string storeProc = "[sp_rpt_InPhieuXuatKho_TheoNgay_NPP]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@trans_date_numb", Ngay);


                        conn.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable data = new DataTable();
                        da.Fill(data);
                        conn.Close();


                        rpt.DataSource = data;

                        ASPxDocumentViewer1.Report = rpt;

                    }
                }
                else
                {
                    // Create a report. 
                    WKS.DMS.WEB.Report.rptFiles.rpt_DonHangTongTheoNhanVien rpt = new WKS.DMS.WEB.Report.rptFiles.rpt_DonHangTongTheoNhanVien();
                    rpt.XmlDataPath = clsCommon.XMLPath + "/xmlDonHangTongTheoNgay_NPP.xml";
                    string storeProc = "[sp_rpt_InPhieuXuatKho_TheoNgay_NhanVien]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@trans_date_numb", Ngay);
                        cmd.Parameters.AddWithValue("@employee_id", cbxEmployee.SelectedValue);

                        conn.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable data = new DataTable();
                        da.Fill(data);
                        conn.Close();

                        


                        rpt.DataSource = data;

                        ASPxDocumentViewer1.Report = rpt;

                    }
                }

              
               


                


                
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
                string sQuery = @"select * from employee where store_id={0}";
                sQuery = string.Format(sQuery, cbxStore.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxEmployee.DataSource = tb;
                cbxEmployee.DataBind();
                cbxEmployee.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }
    }
}