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
using Aspose.Cells;
using System.Web;

namespace WKS.DMS.WEB
{
    public partial class rpt_DanhSachKhachHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();  
            }

            //BindData();

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

                if (Session["role"].ToString().Equals("ADMIN"))
                {
                    cbxStore.Items.Insert(0, new RadComboBoxItem("Tất cả nhà phân phối", "0")); 
                }

                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void BindData()
        {
            try
            {
                DataTable data = new DataTable();

                try
                {
                    string storeProc = "[sp_rpt_DanhSachKhachHang]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@user_id", Session["userid"]);




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

        private void ImportDataTable(Worksheet sheet, DataTable tbData)
        {

            // import vao excel
            sheet.Cells.ImportDataTable(tbData, false, "A5");
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();
            sheet.Cells["B1"].PutValue(cbxStore.SelectedItem.Text);
           
        }


        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                string storeProc = "[sp_rpt_DanhSachKhachHang_2]";
                int result = 0;

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                  




                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();
                }



                string strTemplateFileName = "DanhSachKhachHang.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");



                string strOutputFileName = "DanhSachKhachHang_" + cbxStore.SelectedItem.Text;



                ////Instantiate an instance of license and set the license file through its path
                //Aspose.Cells.License license = new Aspose.Cells.License();
                //license.SetLicense("Aspose.Cells.lic");

                //Open template
                //string path = MapPath("~");
                //path = path.Substring(0, path.LastIndexOf("\\"));
                //path += @"\DesktopModules\BaoCaoTongHop\Export\Excel\ThongKeLinhVuc.xls";
                string path = "";
                path += Server.MapPath("Exports/Templates/") + strTemplateFileName;





                //Create a Workbook.
                //Open a file into the first book.
                Workbook excelWorkbook0 = new Workbook();
                excelWorkbook0.Open(path);

                //Create another Workbook.
                Workbook excelWorkbook1 = new Workbook();

                //Copy the first sheet of the first book into second book.
                excelWorkbook1.Worksheets[0].Copy(excelWorkbook0.Worksheets[0]);

                //Get the first worksheet in the workbook
                Worksheet sheet = excelWorkbook1.Worksheets[0];

                // dua du lieu vao sheet
                ImportDataTable(sheet, data);

                //Save file and send to client browser using selected format
                excelWorkbook1.Save(strOutputFileName + ".xlsx", Aspose.Cells.SaveType.OpenInExcel, Aspose.Cells.FileFormatType.Excel2007Xlsx, HttpContext.Current.Response);

                HttpContext.Current.Response.End();



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
    }
}