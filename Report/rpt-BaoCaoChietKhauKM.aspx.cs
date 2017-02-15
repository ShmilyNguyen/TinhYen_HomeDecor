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
    public partial class rpt_BaoCaoChietKhauKM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rdpTuNgay.SelectedDate = DateTime.Now;
                rdpDenNgay.SelectedDate = DateTime.Now;
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
                    string storeProc = "[sp_rpt_BaoCaoChietKhauKM]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandTimeout = 60000; 

                        cmd.Parameters.AddWithValue("@user_id",Session["userid"]);
                        int b = clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value);
                        int c = clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value);

                        cmd.Parameters.AddWithValue("@from_date", clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value));
                        cmd.Parameters.AddWithValue("@to_date", clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value));



                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(data);
                        conn.Close();

                        grdData.DataSource = data;
                        grdData.DataBind();

                        grdData.ExpandAll();
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
                ASPxPivotGridExporter1.ExportXlsxToResponse("ChietKhauKM-" );
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