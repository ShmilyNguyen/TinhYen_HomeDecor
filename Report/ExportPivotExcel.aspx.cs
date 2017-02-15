using Aspose.Cells;
using Aspose.Cells.Pivot;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;


namespace WKS.DMS.WEB.Report
{
    public partial class ExportPivotExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
            }
        }

        protected void btnExportExcel1_Click(object sender, EventArgs e)
        {
            ExportExcel_BaoCaoBanHangTheoNgay();
        }


        private void ImportDataTable(Worksheet sheet, DataTable tbData)
        {

            // import vao excel
            sheet.Cells.ImportDataTable(tbData, true, "A1");
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();


        }

        protected void ExportExcel_BaoCaoBanHangTheoNgay()
        {
            try
            {
                DataTable data = new DataTable();
                string storeProc = "[sp_rpt_DailySaleReport_ExportExcel]";
                int result = 0;
                int b = clsCommon.ConvertDateToNumber(rdpTuNgay.SelectedDate.Value);
                int c = clsCommon.ConvertDateToNumber(rdpDenNgay.SelectedDate.Value);
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                    cmd.CommandTimeout = 60000;

                

                    cmd.Parameters.AddWithValue("@report_star", b);
                    cmd.Parameters.AddWithValue("@report_end", c);




                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();
                }



                string strTemplateFileName = "DSTheoNgay-Pivot";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");





                string strOutputFileName = "DoanhSoNhanVienTheoNgay-Pivot-" + Session["userid"] + "_" + b + "_" + c;


                File.Copy(Server.MapPath("Exports/Templates/") + strTemplateFileName + ".xlsx", Server.MapPath("Exports/Outputs/") + strOutputFileName + ".xlsx", true);

                ////Instantiate an instance of license and set the license file through its path
                //Aspose.Cells.License license = new Aspose.Cells.License();
                //license.SetLicense("Aspose.Cells.lic");

                //Open template
                //string path = MapPath("~");
                //path = path.Substring(0, path.LastIndexOf("\\"));
                //path += @"\DesktopModules\BaoCaoTongHop\Export\Excel\ThongKeLinhVuc.xls";
                string path = "";
                path += Server.MapPath("Exports/Outputs/") + strOutputFileName + ".xlsx";





                //Create a Workbook.
                //Open a file into the first book.
                Workbook excelWorkbook0 = new Workbook();
                excelWorkbook0.Open(path);

                ////Create another Workbook.
                //Workbook excelWorkbook1 = new Workbook();

                ////Copy the first sheet of the first book into second book.
                //excelWorkbook1.Worksheets[0].Name = "RawData";
                //excelWorkbook1.Worksheets[0].Copy(excelWorkbook0.Worksheets[0]);

                //excelWorkbook1.Worksheets.Add("Pivot");
                //excelWorkbook1.Worksheets["Pivot"].Copy(excelWorkbook0.Worksheets[1]);

                //Get the first worksheet in the workbook
                Worksheet sheet = excelWorkbook0.Worksheets[0];

                // dua du lieu vao sheet
                ImportDataTable(sheet, data);


                //Adding a new sheet
                Worksheet sheet2 = excelWorkbook0.Worksheets[excelWorkbook0.Worksheets.Add()];
                //Naming the sheet
                sheet2.Name = "PivotTable";
                excelWorkbook0.Worksheets.ActiveSheetIndex = 1;
                //Getting the pivottables collection in the sheet
                Aspose.Cells.Pivot.PivotTableCollection pivotTables = sheet2.PivotTables;
                //Adding a PivotTable to the worksheet
                int index = pivotTables.Add("=RawData!A1:V" + (data.Rows.Count + 1).ToString(), "A3", "PivotTable1");
                //Accessing the instance of the newly added PivotTable
                Aspose.Cells.Pivot.PivotTable pivotTable = pivotTables[index];
                //Showing the grand totals
                pivotTable.RowGrand = true;
                pivotTable.ColumnGrand = true;
                //Setting the PivotTable report is automatically formatted
                pivotTable.IsAutoFormat = true;
                //Setting the PivotTable autoformat type.
                //pivotTable.AutoFormatType = Aspose.Cells.Pivot.PivotTableAutoFormatType.Report1;
                pivotTable.PivotTableStyleType = Aspose.Cells.Pivot.PivotTableStyleType.PivotTableStyleMedium2;
                //Setting the PivotTable's Styles for Excel 2007/2010 formats e.g XLSX.
                //pivotTable.setPivotTableStyleType(PivotTableStyleType.PIVOT_TABLE_STYLE_LIGHT_1);

                //pivotTable.PivotTableStyleName = "Pivot Style Medium 2";
                //Draging the first field to the row area.
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "RSM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "ASM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Giám sát");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Nhà phân phối");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "NV Bán hàng");


                //Draging the fourth field to the column area.
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Column, "Ngày xuất kho");
                //pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Column, "Ngành hàng");
                //Draging the fifth field to the data area.






                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "SL Bán");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "SL KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT Bán");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT KM");

                
                //Setting the number format of the first data field
                

                pivotTable.ColumnFields.Add(pivotTable.DataField);

                pivotTable.DataFields[0].NumberFormat = "#,##0";
                pivotTable.DataFields[1].NumberFormat = "#,##0";
                pivotTable.DataFields[2].NumberFormat = "#,##0";
                pivotTable.DataFields[3].NumberFormat = "#,##0";


                excelWorkbook0.Save(path);

                //HttpContext.Current.Response.End();


                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strOutputFileName + ".xlsx");
                Response.TransmitFile(path);
                Response.End();


            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void ExportExcel_BaoCaoTonKho()
        {
            try
            {
                DataTable data = new DataTable();
                string storeProc = "[sp_rpt_BaoCaoTonKho_ExportExcel]";
                int result = 0;

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);

                    cmd.Parameters.AddWithValue("@report_month", int.Parse(ddlThang.Text));
                    cmd.Parameters.AddWithValue("@report_year", ddlNam.Text);




                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();
                }



                string strTemplateFileName = "BaoCaoTonKho-Pivot";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");





                string strOutputFileName = "BaoCaoTonKho-Pivot-" + Session["userid"] + "_" + ddlThang.SelectedValue + "_" + ddlNam.SelectedValue;


                File.Copy(Server.MapPath("Exports/Templates/") + strTemplateFileName + ".xlsx", Server.MapPath("Exports/Outputs/") + strOutputFileName + ".xlsx", true);

                ////Instantiate an instance of license and set the license file through its path
                //Aspose.Cells.License license = new Aspose.Cells.License();
                //license.SetLicense("Aspose.Cells.lic");

                //Open template
                //string path = MapPath("~");
                //path = path.Substring(0, path.LastIndexOf("\\"));
                //path += @"\DesktopModules\BaoCaoTongHop\Export\Excel\ThongKeLinhVuc.xls";
                string path = "";
                path += Server.MapPath("Exports/Outputs/") + strOutputFileName + ".xlsx";





                //Create a Workbook.
                //Open a file into the first book.
                Workbook excelWorkbook0 = new Workbook();
                excelWorkbook0.Open(path);

                ////Create another Workbook.
                //Workbook excelWorkbook1 = new Workbook();

                ////Copy the first sheet of the first book into second book.
                //excelWorkbook1.Worksheets[0].Name = "RawData";
                //excelWorkbook1.Worksheets[0].Copy(excelWorkbook0.Worksheets[0]);

                //excelWorkbook1.Worksheets.Add("Pivot");
                //excelWorkbook1.Worksheets["Pivot"].Copy(excelWorkbook0.Worksheets[1]);

                //Get the first worksheet in the workbook
                Worksheet sheet = excelWorkbook0.Worksheets[0];

                // dua du lieu vao sheet
                ImportDataTable(sheet, data);


                //Adding a new sheet
                Worksheet sheet2 = excelWorkbook0.Worksheets[excelWorkbook0.Worksheets.Add()];
                //Naming the sheet
                sheet2.Name = "PivotTable";
                excelWorkbook0.Worksheets.ActiveSheetIndex = 1;
                //Getting the pivottables collection in the sheet
                Aspose.Cells.Pivot.PivotTableCollection pivotTables = sheet2.PivotTables;
                //Adding a PivotTable to the worksheet
                int index = pivotTables.Add("=RawData!A1:X" + (data.Rows.Count + 1).ToString(), "A3", "PivotTable1");
                //Accessing the instance of the newly added PivotTable
                Aspose.Cells.Pivot.PivotTable pivotTable = pivotTables[index];
                //Showing the grand totals
                //pivotTable.RowGrand = true;
                //pivotTable.ColumnGrand = true;
                //Setting the PivotTable report is automatically formatted
                pivotTable.IsAutoFormat = true;
                //Setting the PivotTable autoformat type.
                //pivotTable.AutoFormatType = Aspose.Cells.Pivot.PivotTableAutoFormatType.Report1;
                pivotTable.PivotTableStyleType = Aspose.Cells.Pivot.PivotTableStyleType.PivotTableStyleMedium2;
                //Setting the PivotTable's Styles for Excel 2007/2010 formats e.g XLSX.
                //pivotTable.setPivotTableStyleType(PivotTableStyleType.PIVOT_TABLE_STYLE_LIGHT_1);

                //pivotTable.PivotTableStyleName = "Pivot Style Medium 2";
                //Draging the first field to the row area.
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Vùng");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Miền");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Khu vực");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Nhà phân phối");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Row, "Tên hàng");


                //Draging the fourth field to the column area.
                //pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Column, "Ngày xuất kho");
                //pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Column, "Ngành hàng");
                //Draging the fifth field to the data area.



                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Tồn đầu HB");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Tồn đầu KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Nhập HB");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Nhập KM");





                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Xuất bán");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Xuất KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "SL tồn cuối HB");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "SL tồn cuối KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Tổng SL tồn cuối");


                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT tồn đầu HB");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT tồn đầu KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT nhập HB");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT nhập KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT bán");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT tồn cuối HB");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "GT tồn cuối KM");
                pivotTable.AddFieldToArea(Aspose.Cells.Pivot.PivotFieldType.Data, "Tổng GT tồn cuối");

                //Setting the number format of the first data field


                //pivotTable.DataField.NumberFormat = "#,##0";
                pivotTable.ColumnFields.Add(pivotTable.DataField);

                pivotTable.DataFields[0].NumberFormat = "#,##0";
                pivotTable.DataFields[1].NumberFormat = "#,##0";
                pivotTable.DataFields[2].NumberFormat = "#,##0";
                pivotTable.DataFields[3].NumberFormat = "#,##0";
                pivotTable.DataFields[4].NumberFormat = "#,##0";
                pivotTable.DataFields[5].NumberFormat = "#,##0";
                pivotTable.DataFields[6].NumberFormat = "#,##0";
                pivotTable.DataFields[7].NumberFormat = "#,##0";
                pivotTable.DataFields[8].NumberFormat = "#,##0";



                pivotTable.DataFields[9].NumberFormat = "#,##0";
                pivotTable.DataFields[10].NumberFormat = "#,##0";
                pivotTable.DataFields[11].NumberFormat = "#,##0";
                pivotTable.DataFields[12].NumberFormat = "#,##0";
                pivotTable.DataFields[13].NumberFormat = "#,##0";
                pivotTable.DataFields[14].NumberFormat = "#,##0";
                pivotTable.DataFields[15].NumberFormat = "#,##0";
                pivotTable.DataFields[16].NumberFormat = "#,##0";
                pivotTable.DataFields[17].NumberFormat = "#,##0";

                excelWorkbook0.Save(path);

                //HttpContext.Current.Response.End();


                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strOutputFileName + ".xlsx");
                Response.TransmitFile(path);
                Response.End();


            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void btnExportExcel2_Click(object sender, EventArgs e)
        {
            ExportExcel_BaoCaoTonKho();
        }
    }
}