using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_InBaoCaoTonKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                    ddlNam.SelectedValue = DateTime.Now.Year.ToString();
                }

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

                // Create a report.
                WKS.DMS.WEB.Report.rptFiles.rptPhieuInTonKho rpt = new WKS.DMS.WEB.Report.rptFiles.rptPhieuInTonKho();
                rpt.XmlDataPath = clsCommon.XMLPath + "/xmlInBaoCaoTonKho.xml";
                string sQuery = "";

                DataSet ds = new DataSet();

                string storeProc = "[usp_rpt_BaoCaoTonKho_By_Store]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@report_month", ddlThang.SelectedValue);
                    cmd.Parameters.AddWithValue("@report_year", ddlNam.SelectedValue);
                    cmd.Parameters.AddWithValue("@report_type", ddlReportType.SelectedValue);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    conn.Close();
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