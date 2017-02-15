using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Telerik.Web.UI;


namespace WKS.DMS.WEB.Report
{
    public partial class rpt_ChiTieuDoanhSoKhachHang : System.Web.UI.Page
    {
       

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                    ddlNam.SelectedValue = DateTime.Now.Year.ToString();
                }

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
                dataTable = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);

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
            catch (Exception)
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
                string _store_id = "";

                _thang = tb.Rows[0]["Thang"].ToString();
                _nam = tb.Rows[0]["Nam"].ToString();
                _store_id = tb.Rows[0]["store_id"].ToString();

                if (string.IsNullOrEmpty(_thang) || string.IsNullOrEmpty(_nam) || string.IsNullOrEmpty(_store_id))
                {

                    return;
                }


                string sQuery = "";

                sQuery = @"delete from target_customer where store_id={0} and data_year={1} and data_month={2}";
                sQuery = string.Format(sQuery,_store_id,_nam,_thang);
               SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                string[] SrcColArr = new string[] {
                                                    "Nam" ,
                                                        "Thang" ,
                                                      
                                                      "store_id" ,
                                                      "customer_id" ,
                                                      "CHỈ TIÊU"
                                                            };
                string[] DesColArr = new string[] {
                                                        "data_year" ,
                                                        "data_month" ,
                                                        "store_id" ,
                                                        "customer_id" ,
                                                        "target_value"   };

                clsCommon.BulkCopyTable(clsCommon.strCon, tb, "target_customer", SrcColArr, DesColArr, 5000);


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
                
                _storename = cbxStore.Text;

                DataTable data = new DataTable();

                string strTemplateFileName = "TemplateFile.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName =   "ChiTieuDoanhSoKhachHang_" + ddlThang.Text + "_" + ddlNam.Text + "(" + _storename + ")";

                data = ExportTemplateData(_reporttypename);

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

        private void ImportDataTable(Worksheet sheet, DataTable tbData)
        {
            // import vao excel
            sheet.Cells.ImportDataTable(tbData, true, "A1");
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();
        }

        public DataTable ExportTemplateData(string ReportTypeName)
        {
            try
            {
                string _filename = "";
                string _storename = "";
                string _thang = "";
                string _nam = "";
                string _store_id = "";

                
                _storename = cbxStore.Text;
                _thang = ddlThang.Text;
                _nam = ddlNam.Text;
                _store_id = cbxStore.SelectedValue;

                string sQuery = "";

                sQuery = @"SELECT  '{0}' AS Nam ,
                                        '{1}' AS Thang ,
                                        c.store_id ,
                                        s.store_name ,
                                        customer_id ,
                                        customer_code ,
                                        customer_name ,
                                        [address] ,
                                        0 AS 'CHỈ TIÊU'
                                FROM    customer AS c
                                        LEFT JOIN dbo.store AS s ON c.store_id = s.store_id
                                WHERE   c.store_id = {2}";

                sQuery = string.Format(sQuery, _nam, _thang, _store_id);

                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                return tb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}