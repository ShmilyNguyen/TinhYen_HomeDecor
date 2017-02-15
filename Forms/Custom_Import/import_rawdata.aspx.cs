using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Custom_Import
{
    public partial class import_rawdata : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();
            }

            BindData();

        }


        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            BindData();
        }



        public void BindList()
        {
            try
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void BindData()
        {

        }

        protected void AsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                foreach (UploadedFile f in AsyncUpload1.UploadedFiles)
                {
                    string _SavePath = clsCommon.UploadPath + f.GetName();
                    f.SaveAs(_SavePath, true);
                    DataTable tb = ImportData(_SavePath, "Sheet1");
                    BulkCopyData(tb);
                    RadWindowManager1.RadAlert("Import Thành Công!", 330, 180, "Thông báo", null, null);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable ImportData(string _vsPath, string _sheetname)
        {
            //string _vsPath = "";
            Workbook workbook = new Workbook();
            workbook.Open(_vsPath);

            Cells cells = workbook.Worksheets[_sheetname].Cells;
            System.Data.DataTable dataTable = new DataTable();


            //dataTable.Columns.Add("[Tháng]", typeof(string));
            //dataTable.Columns.Add("Năm", typeof(string));
            //dataTable.Columns.Add("[Mã Xuất Kho]", typeof(string));
            //dataTable.Columns.Add("[Loại Xuất]", typeof(string));
            //dataTable.Columns.Add("[Ngày Xuất Kho (Date)]", typeof(string));
            //dataTable.Columns.Add("[NPP ID]", typeof(string));
            //dataTable.Columns.Add("[Khách hàng ID]", typeof(string));
            //dataTable.Columns.Add("parent_id", typeof(string));
            //dataTable.Columns.Add("[Mã khách hàng]", typeof(string));
            //dataTable.Columns.Add("[Mã khách hàng toàn quốc]", typeof(string));
            //dataTable.Columns.Add("[Tên khách hàng]", typeof(string));
            //dataTable.Columns.Add("[Địa chỉ]", typeof(string));
            //dataTable.Columns.Add("[Số nhà]", typeof(string));
            //dataTable.Columns.Add("[Tỉnh - Thành phố]", typeof(string));
            //dataTable.Columns.Add("[Quận - Huyện]", typeof(string));
            //dataTable.Columns.Add("[Xã - Phường]", typeof(string));
            //dataTable.Columns.Add("[Đường - chợ]", typeof(string));
            //dataTable.Columns.Add("Kenh", typeof(string));
            //dataTable.Columns.Add("Tuyến", typeof(string));
            //dataTable.Columns.Add("employee_id", typeof(string));
            //dataTable.Columns.Add("[Nhân viên]", typeof(string));
            //dataTable.Columns.Add("[Ghi chú]", typeof(string));
           
            //dataTable.Columns.Add("created_by_name", typeof(string));
            //dataTable.Columns.Add("[Mã hàng hóa]", typeof(string));
            //dataTable.Columns.Add("[Tên hàng hóa]", typeof(string));
            //dataTable.Columns.Add("ĐVT", typeof(string));
            //dataTable.Columns.Add("[Loại giá]", typeof(string));
            //dataTable.Columns.Add("item_price", typeof(string));
            //dataTable.Columns.Add("[Số lượng]", typeof(string));
            //dataTable.Columns.Add("[Chiết khấu]", typeof(string));
            //dataTable.Columns.Add("[Chiết khấu Ontop]", typeof(string));
            //dataTable.Columns.Add("ontop_discount_code", typeof(string));
            //dataTable.Columns.Add("[Mã CTKM]", typeof(string));
            //dataTable.Columns.Add("[Tên CTKM]", typeof(string));
            //dataTable.Columns.Add("promo_type", typeof(string));
            //dataTable.Columns.Add("[Nhà phân phối]", typeof(string));
            //dataTable.Columns.Add("region_name", typeof(string));
            //dataTable.Columns.Add("region_code", typeof(string));
            //dataTable.Columns.Add("area_name", typeof(string));
            //dataTable.Columns.Add("area_code", typeof(string));
            //dataTable.Columns.Add("SUP", typeof(string));
            //dataTable.Columns.Add("ASM", typeof(string));
            //dataTable.Columns.Add("RSM", typeof(string));
            //dataTable.Columns.Add("[SL Bán]", typeof(string));
            //dataTable.Columns.Add("[SL Khuyến mãi]", typeof(string));
            //dataTable.Columns.Add("Total", typeof(string));
            //dataTable.Columns.Add("[GT Bán]", typeof(string));
            //dataTable.Columns.Add("[GT Khuyến mãi]", typeof(string));
            //dataTable.Columns.Add("[GTCK dòng hàng]", typeof(string));
            //dataTable.Columns.Add("[GTCK đơn hàng]", typeof(string));
            //dataTable.Columns.Add("[Thành tiền]", typeof(string));

            


            try
            {


                dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);

                for (int i = 0; i <= cells.MaxDataColumn; i++)
                {
                    try
                    {

                        dataTable.Columns[i].ColumnName = cells[i].Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        Response.Write(ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            //MessageBox.Show(dataTable.Rows.Count.ToString());

            return dataTable;
        }


        public void BulkCopyData(DataTable tb)
        {
            try
            {
                if (tb == null)
                {
                    return;
                }

                if (tb.Rows.Count == 0)
                {
                    return;
                }


                string _thang = "";
                string _nam = "";
                string _nhaphanphoi = "";
                string _store_id = "";

                _thang = tb.Rows[0]["Tháng"].ToString();
                _nam = tb.Rows[0]["Năm"].ToString();
                _nhaphanphoi = tb.Rows[0]["Nhà phân phối"].ToString();
                //_store_id = tb.Rows[0]["store_id"].ToString();

                if (string.IsNullOrEmpty(_thang) || string.IsNullOrEmpty(_nam) || string.IsNullOrEmpty(_nhaphanphoi))
                {

                    return;

                }


                string sQuery = "";

                sQuery = @"delete from custom_rawdata where [Năm]={0} and [Tháng]={1} and [Nhà phân phối] =N'{2}'";
                sQuery = string.Format(sQuery, _nam, _thang,_nhaphanphoi);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                string[] SrcColArr = new string[] {
                                                "[Tháng]",
"[Năm]",
"[Mã Xuất Kho]",
"[Loại Xuất]",
"[Ngày Xuất Kho (Date)]",
"[NPP ID]",
"[Khách hàng ID]",
"[parent_id]",
"[Mã khách hàng]",
"[Mã khách hàng toàn quốc]",
"[Tên khách hàng]",
"[Địa chỉ]",
"[Số nhà]",
"[Tỉnh - Thành phố]",
"[Quận - Huyện]",
"[Xã - Phường]",
"[Đường - chợ]",
"[Kênh]",
"[Tuyến]",
"[employee_code]",
"[Nhân viên]",
"[Ghi chú]",

"[created_by_name]",
"[Mã hàng hóa]",
"[Tên hàng hóa]",
"[ĐVT]",
"[Loại giá]",
"[item_price]",
"[Số lượng]",
"[Chiết khấu]",
"[Chiết khấu Ontop]",
"[ontop_discount_code]",
"[Mã CTKM]",
"[Tên CTKM]",
"[promo_type]",
"[Nhà phân phối]",
"[region_name]",
"[region_code]",
"[area_name]",
"[area_code]",
"[SUP]",
"[ASM]",
"[RSM]",
"[SL Bán]",
"[SL Khuyến mãi]",
"[Total]",
"[GT Bán]",
"[GT Khuyến mãi]",

"[GTCK dòng hàng]",
"[GTCK đơn hàng]",
"[Thành tiền]"

                                                            };
                string[] DesColArr = new string[] {
                                                        "[Tháng]",
"[Năm]",
"[Mã Xuất Kho]",
"[Loại Xuất]",
"[Ngày Xuất Kho (Date)]",
"[NPP ID]",
"[Khách hàng ID]",
"[parent_id]",
"[Mã khách hàng]",
"[Mã khách hàng toàn quốc]",
"[Tên khách hàng]",
"[Địa chỉ]",
"[Số nhà]",
"[Tỉnh - Thành phố]",
"[Quận - Huyện]",
"[Xã - Phường]",
"[Đường - chợ]",
"[Kênh]",
"[Tuyến]",
"[employee_code]",
"[Nhân viên]",
"[Ghi chú]",

"[created_by_name]",
"[Mã hàng hóa]",
"[Tên hàng hóa]",
"[ĐVT]",
"[Loại giá]",
"[item_price]",
"[Số lượng]",
"[Chiết khấu]",
"[Chiết khấu Ontop]",
"[ontop_discount_code]",
"[Mã CTKM]",
"[Tên CTKM]",
"[promo_type]",
"[Nhà phân phối]",
"[region_name]",
"[region_code]",
"[area_name]",
"[area_code]",
"[SUP]",
"[ASM]",
"[RSM]",
"[SL Bán]",
"[SL Khuyến mãi]",
"[Total]",
"[GT Bán]",
"[GT Khuyến mãi]",

"[GTCK dòng hàng]",
"[GTCK đơn hàng]",
"[Thành tiền]"
   };

                clsCommon.BulkCopyTable(clsCommon.strCon, tb, "custom_rawdata", SrcColArr, DesColArr, 5000);


            }
            catch (Exception ex)
            {
                RadWindowManager1.RadAlert("Import Lỗi, Vui lòng kiểm tra lại file Template !", 330, 180, "Thông báo", null, null);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string _reporttypename = "";
                string _storename = "";

                //_storename = cbxStore.Text;

                DataTable data = new DataTable();

                string strTemplateFileName = "custom_rawdata.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName = "custom_rawdata_" + ddlThang.Text + "_" + ddlNam.Text;

                //data = ExportTemplateData(_reporttypename);

                ////Instantiate an instance of license and set the license file through its path
                //Aspose.Cells.License license = new Aspose.Cells.License();
                //license.SetLicense("Aspose.Cells.lic");

                //Open template
                //string path = MapPath("~");
                //path = path.Substring(0, path.LastIndexOf("\\"));
                //path += @"\DesktopModules\BaoCaoTongHop\Export\Excel\ThongKeLinhVuc.xls";
                string path = "";
                path += Server.MapPath("Exports/Templates/") + strTemplateFileName;
                path = path.Replace("\\Forms\\Custom_Import","\\Report");

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

        private void ImportDataTable(Worksheet sheet, DataTable tbData)
        {
            // import vao excel
            sheet.Cells.ImportDataTable(tbData, true, "A1");
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();
        }

        

    }
}