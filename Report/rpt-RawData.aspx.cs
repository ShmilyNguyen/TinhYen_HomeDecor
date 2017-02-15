using Aspose.Cells;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_RawData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                //ddlNam.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        private void ImportDataTable(Worksheet sheet, DataTable tbData)
        {
            // import vao excel
            sheet.Cells.ImportDataTable(tbData, false, "A2");
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();
        }

        private void ImportDataTable(Worksheet sheet, DataTable tbData, int startIndex)
        {
            startIndex = 2 + startIndex;
            // import vao excel
            sheet.Cells.ImportDataTable(tbData, false, "A" + startIndex.ToString());
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                DataTable data2 = new DataTable();

                //if (rdpTuNgay.SelectedDate.Value.AddDays(61) <= rdpDenNgay.SelectedDate.Value)
                //{
                //    RadWindowManager1.RadAlert("Số ngày vượt quá 60 ngày, Vui lòng chọn lại!", 330, 180, "Thông báo", null, null);
                //    return;
                //}

                int tuNgay = clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value);
                int denNgay = clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value);

                string storeProc = "[sp_rpt_RawData_FromDate_ToDate_v2]";
                int result = 0;

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                    cmd.Parameters.AddWithValue("@fromdate", tuNgay);
                    cmd.Parameters.AddWithValue("@todate", denNgay);

                    cmd.CommandTimeout = 60000;

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();
                }

                storeProc = "[sp_rpt_RawData_NgoaiHeThong_FromDate_ToDate_v2]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);

                    cmd.Parameters.AddWithValue("@fromdate", 0);
                    cmd.Parameters.AddWithValue("@todate", 0);

                    cmd.Parameters.AddWithValue("@thang", rdpTuNgay.SelectedDate.Value.Month);
                    cmd.Parameters.AddWithValue("@nam", rdpTuNgay.SelectedDate.Value.Year);

                    cmd.CommandTimeout = 60000;

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data2);
                    conn.Close();
                }

                string strTemplateFileName = "SecondarySales.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName = "SecondarySales_" + tuNgay + "_" + denNgay;

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
                ImportDataTable(sheet, data2, data.Rows.Count);

                //Save file and send to client browser using selected format
                excelWorkbook1.Save(strOutputFileName + ".xlsx", Aspose.Cells.SaveType.OpenInExcel, Aspose.Cells.FileFormatType.Excel2007Xlsx, HttpContext.Current.Response);

                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
            }
        }
    }
}