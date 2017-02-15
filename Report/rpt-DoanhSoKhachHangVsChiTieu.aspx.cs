using DevExpress.XtraPivotGrid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;


namespace WKS.DMS.WEB.Report
{
    public partial class rpt_DoanhSoKhachHangVsChiTieu : System.Web.UI.Page
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
                    string storeProc = "[sp_rpt_BaoCaoDoanhSoKhachHang_vs_ChiTieu]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 60000;

                        cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                        cmd.Parameters.AddWithValue("@report_month", ddlThang.Text);
                        cmd.Parameters.AddWithValue("@report_year", ddlNam.Text);

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(data);
                        conn.Close();

                        grdData.DataSource = data;
                        grdData.DataBind();
                    }
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
                ASPxPivotGridExporter1.ExportXlsxToResponse("DoanhSoKhachHang_vs_ChiTieu_" + ddlThang.Text);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
        }

        protected void grdData_CustomCellValue(object sender, DevExpress.Web.ASPxPivotGrid.PivotCellValueEventArgs e)
        {
            if (e.RowValueType == PivotGridValueType.CustomTotal || e.RowValueType == PivotGridValueType.GrandTotal || e.RowValueType == PivotGridValueType.Total)
            {
              

                //Saleout
                if (e.DataField == fieldsalevstarget)
                {
                    if ((decimal)e.GetFieldValue(fieldtargetvalue) == 0)
                    {
                        e.Value = 0;
                    }
                    else
                    {
                        e.Value = (decimal)e.GetFieldValue(fieldThanhTien) / (decimal)e.GetFieldValue(fieldtargetvalue);
                    }
                }

               
            }
        }
    }
}