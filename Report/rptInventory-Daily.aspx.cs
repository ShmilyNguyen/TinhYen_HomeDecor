using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;


namespace WKS.DMS.WEB.Report
{
    public partial class rptInventory_Daily : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();
                ddlNgay.SelectedValue = DateTime.Now.Day.ToString();
            }

            BindData();
        }

        public void BindData()
        {
            try
            {
                DataTable data = new DataTable();

                try
                {
                    string storeProc = "[usp_rpt_BaoCaoTonKho_TheoNgay]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                        cmd.Parameters.AddWithValue("@report_day", ddlNgay.Text);
                        cmd.Parameters.AddWithValue("@report_month", ddlThang.Text);
                        cmd.Parameters.AddWithValue("@report_year", ddlNam.Text);
                        cmd.Parameters.AddWithValue("@report_type", ddlReportType.SelectedValue);


                        cmd.CommandTimeout = 60000;
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(data);
                        conn.Close();

                        grdData.DataSource = data;
                        grdData.DataBind();
                    }

                    //string s = string.Empty;
                    //using (var reader = File.OpenText(Path.Combine(ConfigurationManager.AppSettings["R0001Folder"], string.Format("Ton_Kho_{0:yyyyMM}.txt", DateTime.Today))))
                    //{
                    //    s = reader.ReadToEnd();
                    //}
                    //data = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(DataTable));

                    //grdData.DataSource = data;
                    //grdData.DataBind();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
            }


        }


        protected void grdData_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
        {
            if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.CustomTotal || e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total || e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
            {
                e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                e.CellStyle.Font.Bold = true;
                e.CellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPivotGridExporter1.ExportXlsxToResponse("TonKho-" + ddlThang.Text);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {

        }

    }
}