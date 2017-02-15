using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Telerik.Web.UI;


namespace WKS.DMS.WEB.Forms.Custom_Import
{
    public partial class import_khachhang : System.Web.UI.Page
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


                
                string _store_id = "";

               
                //_store_id = tb.Rows[0]["store_id"].ToString();

                string _manpp = tb.Rows[0]["Mã NPP"].ToString();


                string sQuery = "";

                sQuery = @"delete from custom_customer where [Mã NPP]=N'" + _manpp + "'";
                
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                string[] SrcColArr = new string[] {
                                                "[Dia Ban]",
 "[Mã Tinh]",
 "[Mã NPP]",
 "[Tên NVBH]",
 "[Mã KH]",
 "[Tên khách hàng]",
 "[Số nhà]",
 "[Đường/Phố]",
 "[Phường/xã]",
 "[Quận/ Huyện]",
 "[Tỉnh/Tp]",
 "[Địa chỉ đầy đủ]",
 "[Điện thoại]",
 "[Mảng CH]",
 "[Loại CH]",
 "[Vị trí]",
 "[Tần số]",
 "[Tuyến thứ]",
 "[Hoạt động]"

                                                            };
                string[] DesColArr = new string[] {
                                                        "[Dia Ban]",
 "[Mã Tinh]",
 "[Mã NPP]",
 "[Tên NVBH]",
 "[Mã KH]",
 "[Tên khách hàng]",
 "[Số nhà]",
 "[Đường/Phố]",
 "[Phường/xã]",
 "[Quận/ Huyện]",
 "[Tỉnh/Tp]",
 "[Địa chỉ đầy đủ]",
 "[Điện thoại]",
 "[Mảng CH]",
 "[Loại CH]",
 "[Vị trí]",
 "[Tần số]",
 "[Tuyến thứ]",
 "[Hoạt động]"
   };

                clsCommon.BulkCopyTable(clsCommon.strCon, tb, "custom_customer", SrcColArr, DesColArr, 5000);


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

                string strTemplateFileName = "custom_customer.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName = "custom_customer";

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
                path = path.Replace("\\Forms\\Custom_Import", "\\Report");

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