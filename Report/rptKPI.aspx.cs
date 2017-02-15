using DevExpress.XtraPivotGrid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rptKPI : System.Web.UI.Page
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
                    string storeProc = "[sp_rpt_KPI]";
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
                ASPxPivotGridExporter1.ExportXlsxToResponse("KPI-" + ddlThang.Text +"-" +ddlNam.Text);
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
                //Order No
                if (e.DataField == fieldordervstarget)
                {
                    if ((decimal)e.GetFieldValue(fieldtargetorder) == 0)
                    {
                        e.Value = 0;
                    }
                    else
                    {
                        e.Value = (decimal)e.GetFieldValue(fieldorderno) / (decimal)e.GetFieldValue(fieldtargetorder);
                    }
                }

                //Saleout
                if (e.DataField == fieldsaleoutvstarget)
                {
                    if ((decimal)e.GetFieldValue(fieldtargetsaleout) == 0)
                    {
                        e.Value = 0;
                    }
                    else
                    {
                        e.Value = (decimal)e.GetFieldValue(fieldsaleout) / (decimal)e.GetFieldValue(fieldtargetsaleout);
                    }
                }

                //Order Focus
                if (e.DataField == fieldorderfocusvstarget)
                {
                    if ((decimal)e.GetFieldValue(fieldtargetfocusorder) == 0)
                    {
                        e.Value = 0;
                    }
                    else
                    {
                        e.Value = (decimal)e.GetFieldValue(fieldordernofocus) / (decimal)e.GetFieldValue(fieldtargetfocusorder);
                    }
                }

                //Saleout Focus

                if (e.DataField == fieldsaleoutfocus_vs_target)
                {
                    if ((decimal)e.GetFieldValue(fieldtargetfocussaleout) == 0)
                    {
                        e.Value = 0;
                    }
                    else
                    {
                        e.Value = (decimal)e.GetFieldValue(fieldsaleoutfocus) / (decimal)e.GetFieldValue(fieldtargetfocussaleout);
                    }
                }

                //Active Outlet

                if (e.DataField == fieldactiveoutletvstarget)
                {
                    if ((decimal)e.GetFieldValue(fieldtargetactiveoutlet) == 0)
                    {
                        e.Value = 0;
                    }
                    else
                    {
                        e.Value = (decimal)e.GetFieldValue(fieldactiveoutlet) / (decimal)e.GetFieldValue(fieldtargetactiveoutlet);
                    }
                }
            }
        }
    }
}