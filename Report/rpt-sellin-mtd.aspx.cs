﻿using System;
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
using DevExpress.XtraPivotGrid;


namespace WKS.DMS.WEB.Report
{
    public partial class rpt_sellin_mtd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                //ddlNam.SelectedValue = DateTime.Now.Year.ToString();
            }

            BindData();
        }

        public void BindData()
        {
            try
            {
                DataTable data = new DataTable();

                if (rdpTuNgay.SelectedDate.Value.AddDays(61) <= rdpDenNgay.SelectedDate.Value)
                {
                    RadWindowManager1.RadAlert("Số ngày vượt quá 60 ngày, Vui lòng chọn lại!", 330, 180, "Thông báo", null, null);
                    return;
                }

                int TuNgay = clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value);
                int DenNgay = clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value);

                try
                {
                    string storeProc = "[sp_rpt_BaoCaoDoanhSoNhap_FromDate_ToDate]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                        cmd.Parameters.AddWithValue("@fromdate", TuNgay);
                        cmd.Parameters.AddWithValue("@todate", DenNgay);

                        cmd.CommandTimeout = 60000;

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(data);
                        conn.Close();

                        ASPxPivotGrid.DataSource = data;
                        ASPxPivotGrid.DataBind();
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


        protected void ASPxPivotGrid_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
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
                int TuNgay = clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value);
                int DenNgay = clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value);

                ASPxPivotGridExporter1.ExportXlsxToResponse("BaoCaoDoanhSoNhap-" + TuNgay.ToString() + "-" + DenNgay.ToString());
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {

        }

        protected void ASPxPivotGrid_CustomCellValue(object sender, DevExpress.Web.ASPxPivotGrid.PivotCellValueEventArgs e)
        {
            if (e.RowValueType == PivotGridValueType.CustomTotal)
            {


              




            }
        }

       

        protected void ASPxPivotGrid_PreRender(object sender, EventArgs e)
        {

        }
    }
}