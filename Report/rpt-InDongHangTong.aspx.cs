using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_InDongHangTong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();
                BindNhanVien();
                
            }

            BindData();
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            //BindData();
        }

        public void BindData()
        {
            try
            {
                // Show the report's preview.

                int Ngay = clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value);
                Dataset_DATA ds = new Dataset_DATA();
                // Create a report.
                WKS.DMS.WEB.Report.rptFiles.rptInPhieuXuatKhoTong rpt = new WKS.DMS.WEB.Report.rptFiles.rptInPhieuXuatKhoTong();
                //rpt.XmlDataPath = clsCommon.XMLPath + "/xmlInPhieuXuatKhoTong.xml";

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    #region Header

                    string storeProc = "[sp_rpt_InPhieuXuatKhoTong_header]";
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@employee_id", cbxEmployee.SelectedValue);
                    cmd.Parameters.AddWithValue("@trans_date1", rdpNgayGiaoDich.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("@trans_date2", Ngay);

                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds, "xuatkho0_header");

                    #endregion Header

                    conn.Close();
                }

                using (SqlConnection conn1 = new SqlConnection(clsCommon.strCon))
                {
                    #region Header

                    string storeProc1 = "[sp_rpt_InPhieuXuatKhoTong_detail_1]";
                    SqlCommand cmd1 = new SqlCommand(storeProc1, conn1);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    cmd1.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd1.Parameters.AddWithValue("@employee_id", cbxEmployee.SelectedValue);
                    cmd1.Parameters.AddWithValue("@trans_date1", rdpNgayGiaoDich.SelectedDate.Value);
                    cmd1.Parameters.AddWithValue("@trans_date2", Ngay);

                    conn1.Open();

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(ds, "xuatkho0_detail_1");

                    #endregion Header

                    conn1.Close();
                }

                using (SqlConnection conn2 = new SqlConnection(clsCommon.strCon))
                {
                    #region Header

                    string storeProc2 = "[sp_rpt_InPhieuXuatKhoTong_detail_2]";
                    SqlCommand cmd2 = new SqlCommand(storeProc2, conn2);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd2.Parameters.AddWithValue("@employee_id", cbxEmployee.SelectedValue);
                    cmd2.Parameters.AddWithValue("@trans_date1", rdpNgayGiaoDich.SelectedDate.Value);
                    cmd2.Parameters.AddWithValue("@trans_date2", Ngay);

                    conn2.Open();

                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

                    da2.Fill(ds, "xuatkho0_detail_2");

                    #endregion Header

                    conn2.Close();
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

                //cbxStore_SelectedIndexChanged(null, null);


               

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void BindNhanVien()
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