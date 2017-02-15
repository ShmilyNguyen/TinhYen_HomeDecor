using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_ImportPhanBoCTKM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                   
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

                sQuery = @"select promo_id, promo_code +'-'+ promo_name as promo_name from promotion";

                sQuery = string.Format(sQuery, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxPromo.DataSource = tb;
                cbxPromo.DataBind();
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
                    DataTable tb = ImportData(_SavePath, "DATA");
                    if (BulkCopyData(tb))
                    {
                        RadWindowManager1.RadAlert("Import Thành Công!", 330, 180, "Thông báo", null, null);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("Import Lỗi, Vui lòng kiểm tra lại file Template !", 330, 180, "Thông báo", null, null);
                    }

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
            catch (Exception ex)
            {
                return null;
            }

            //MessageBox.Show(dataTable.Rows.Count.ToString());

            return dataTable;
        }

        public bool BulkCopyData(DataTable tb)
        {
            try
            {
                if (tb == null)
                {
                    return false;
                }

                if (tb.Rows.Count == 0)
                {
                    return false;
                }


                string promo_id = "";
                string store_id = "";
                string channel_id = "";


                string sQuery = "";

                foreach (DataRow r in tb.Rows)
                {
                    promo_id = r["promo_id"] == DBNull.Value ? "" : r["promo_id"].ToString();
                    store_id = r["store_id"] == DBNull.Value ? "" : r["store_id"].ToString();
                    channel_id = r["channel_id"] == DBNull.Value ? "" : r["channel_id"].ToString();



                    if (string.IsNullOrEmpty(promo_id))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(store_id))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(channel_id))
                    {
                        return false;
                    }    


                    sQuery = @"delete from promotion_store where promo_id={0} and store_id={1}";
                    sQuery = string.Format(sQuery, promo_id,store_id);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    sQuery = @"INSERT INTO dbo.promotion_store
                                            ( store_id, promo_id, channel_id )
                                    VALUES  ( {0}, -- store_id - int
                                              {1}, -- promo_id - int
                                              {2}  -- channel_id - int
                                              )";

                    sQuery = string.Format(sQuery,store_id,promo_id,channel_id);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                }



                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string _reporttypename = "";
                string _promo_name = "";

                _promo_name = cbxPromo.Text;

                string strTemplateFileName = "RouteTemplate.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName = "PhanBoCTKM_"  + "(" + _promo_name + ")";

                DataSet data = ExportTemplateData(_reporttypename);

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
                excelWorkbook1.Worksheets.Add();
                excelWorkbook1.Worksheets[1].Copy(excelWorkbook0.Worksheets[1]);
                excelWorkbook1.Worksheets.Add();
                excelWorkbook1.Worksheets[2].Copy(excelWorkbook0.Worksheets[2]);

                //Get the first worksheet in the workbook
                Worksheet sheet_DATA = excelWorkbook1.Worksheets[0];
                Worksheet sheet_STORE = excelWorkbook1.Worksheets[1];
                Worksheet sheet_CHANNEL = excelWorkbook1.Worksheets[2];

                sheet_DATA.Name = "DATA";
                sheet_STORE.Name = "STORE";
                sheet_CHANNEL.Name = "CHANNEL";

                // dua du lieu vao sheet
                ImportDataTable(sheet_DATA, data.Tables["DATA"]);
                ImportDataTable(sheet_STORE, data.Tables["STORE"]);
                ImportDataTable(sheet_CHANNEL, data.Tables["CHANNEL"]);

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

        public DataSet ExportTemplateData(string ReportTypeName)
        {
            try
            {
                string promo_id = "";

                promo_id = cbxPromo.SelectedValue;

                DataSet ds = new DataSet();

                string sQuery = "";

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    sQuery = @"SELECT  a.promo_id ,
                                    a.promo_code ,
                                    a.promo_name ,
                                    a.start_date_numb ,
                                    a.end_date_numb ,
                                    r1.region_name ,
                                    r2.area_name ,                                   
                                    c.store_name ,
                                    c.store_id ,
                                    1 AS 'channel_id'
                            FROM    dbo.promotion AS a ,
                                    dbo.store AS c ,
                                    dbo.region AS r1 ,
                                    dbo.area AS r2
                            WHERE   promo_id = " + promo_id+@"
                                    AND c.region_id = r1.region_id
                                    AND c.area_id = r2.area_id
                            ORDER BY r1.region_name ,
                                    r2.area_name ,
                                    store_id";

                   

                    SqlCommand cmd = new SqlCommand(sQuery, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60000;

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "DATA");
                    conn.Close();

                    sQuery = @"select store_id,store_code,store_name from store";
                    
                    SqlCommand cmd1 = new SqlCommand(sQuery, conn);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandTimeout = 60000;
                    conn.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(ds, "STORE");
                    conn.Close();

                    sQuery = @"select * from customer_channel";
                    
                    SqlCommand cmd2 = new SqlCommand(sQuery, conn);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandTimeout = 60000;
                    conn.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(ds, "CHANNEL");
                    conn.Close();

                }

                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnProcessData_Click(object sender, EventArgs e)
        {

        }
    }
}