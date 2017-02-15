using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace WKS.DMS.WEB.Reports
{
    public partial class rptDailyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();
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
                    string storeProc = "[usp_rpt_DailySaleReport]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        System.Diagnostics.Debug.WriteLine("id : " + Session["userid"]);
                        cmd.Parameters.AddWithValue("@user_id",(string) Session["userid"]);

                        cmd.Parameters.AddWithValue("@report_month", int.Parse(ddlThang.Text));
                        cmd.Parameters.AddWithValue("@report_year", ddlNam.SelectedValue);

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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPivotGridExporter1.ExportXlsxToResponse("DoanhSoTheoNgay-" + ddlThang.Text);
            }
            catch (Exception ex)
            {
                throw;
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
    }
}