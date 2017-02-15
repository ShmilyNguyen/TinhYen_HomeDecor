using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web.ASPxGridView;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_BaoCaoTongHopThuTien : System.Web.UI.Page
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
                    string storeProc = "[sp_rpt_BaoCaoTongHopThuTien]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@user_id", Session["userid"]);

                        cmd.Parameters.AddWithValue("@report_month", ddlThang.SelectedValue);
                        cmd.Parameters.AddWithValue("@report_year", 2015);



                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(data);
                        conn.Close();

                        grvData.DataSource = data;
                        grvData.DataBind();

                        grvData.ExpandAll();
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxGridViewExporter.WriteXlsxToResponse("BaoCaoTongHopThuTien-" + ddlThang.SelectedValue);
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