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

namespace WKS.DMS.WEB.Reports
{
    public partial class rpt_BaoCaoCongNo : System.Web.UI.Page
    {
        int keyActive = 1;




        protected void Page_Load(object sender, EventArgs e)
        {

            String groupID = Session["group_id"].ToString();
            String distID = Session["channel_dist_id"].ToString();
            string priceGroup = price_group.SelectedValue.Trim();
            if (distID.Equals("1") || (priceGroup.Equals("GT") && groupID.Equals("1")))
            {
                keyActive = 1;

            }
            if (distID.Equals("2") ||(priceGroup.Equals("MT") && groupID.Equals("1")))
            {
                keyActive = 2;
            }

            if (!groupID.Equals("1")   ){
                price_group.Visible = false;
            }

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
                    string a = keyActive.ToString();


                    string storeProc = "[sp_rpt_BaoCaoCongNo]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Thang thoi gian chay query len, dang bi timeout
                        cmd.CommandTimeout = 60000;
                        
                        cmd.Parameters.AddWithValue("@user_id", int.Parse( Session["userid"].ToString()));
                        //   cmd.Parameters.AddWithValue("@report_type", ddlReportType.SelectedValue.Trim());

                        cmd.Parameters.AddWithValue("@month", ddlThang.Text);
                        cmd.Parameters.AddWithValue("@year", ddlNam.Text);



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
                ASPxPivotGridExporter1.ExportXlsxToResponse("TonKho-" + ddlThang.Text);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            BindData();
        }



    }
}