using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Report
{
    public partial class rpt_ImportTuyenBanHang : System.Web.UI.Page
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

              
                string store_id = "";
                string customer_id = "";
                
                
                string customer_code = "";
                string customer_name = "";
                string mobile = "";
                string phone = "";
                string email = "";
                string channel_id = "";
                string route_id = "";

                string address = "";
                string add_number = "";
                string province = "";
                string district = "";
                string ward = "";
                string street = "";
                string employee_id = "";

                string sQuery = "";

                foreach (DataRow r in tb.Rows)
                {
                    store_id = r["store_id"] == DBNull.Value ? "" : r["store_id"].ToString();
                    customer_id = r["customer_id"] == DBNull.Value ? "" : r["customer_id"].ToString();
                    customer_code = r["customer_code"] == DBNull.Value ? "" : r["customer_code"].ToString();
                    customer_name = r["customer_name"] == DBNull.Value ? "" : r["customer_name"].ToString();

                    channel_id = r["channel_id"] == DBNull.Value ? "" : r["channel_id"].ToString();
                    route_id = r["route_id"] == DBNull.Value ? "" : r["route_id"].ToString();
                    employee_id = r["employee_id"] == DBNull.Value ? "" : r["employee_id"].ToString();
                    province = r["province"] == DBNull.Value ? "" : r["province"].ToString();
                    district = r["district"] == DBNull.Value ? "" : r["district"].ToString();
                    ward = r["ward"] == DBNull.Value ? "" : r["ward"].ToString();
                    street = r["street"] == DBNull.Value ? "" : r["street"].ToString();
                    address = r["address"] == DBNull.Value ? "" : r["address"].ToString();
                    add_number = r["add_number"] == DBNull.Value ? "" : r["add_number"].ToString();
                    email = r["email"] == DBNull.Value ? "" : r["email"].ToString();
                    phone = r["phone"] == DBNull.Value ? "" : r["phone"].ToString();
                    mobile = r["mobile"] == DBNull.Value ? "" : r["mobile"].ToString();

                    if (string.IsNullOrEmpty(employee_id))
                    {
                        employee_id = "NULL";
                    }

                    if (string.IsNullOrEmpty(store_id))
                    {
                        return false;
                    }


                    if (string.IsNullOrEmpty(customer_code))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(channel_id))
                    {
                        channel_id = "0";
                    }

                    if (string.IsNullOrEmpty(route_id))
                    {
                        route_id = "0";
                    }


                    sQuery = @"update customer set 
                                customer_code=N'"+customer_code.Trim()+@"',
                                customer_name=N'" + customer_name.Trim() + @"',
                                employee_id=" + employee_id + @",
                                channel_id=" +channel_id+@",
                                route_id="+route_id+@",
                                address=N'" + address.Trim() + @"',
                                add_number=N'" + add_number.Trim() + @"',
                                province=N'" + province.Trim() + @"',
                                district=N'" + district.Trim() + @"',
                                ward=N'" + ward.Trim() + @"',
                                street=N'" + street.Trim() + @"',
                                email=N'" + email.Trim() + @"',
                                phone=N'" + phone.Trim() + @"',
                                mobile=N'" + mobile.Trim() + @"'

                                where store_id=" +store_id+@"
                                and customer_id=" + customer_id;

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
                string _storename = "";

                _storename = cbxStore.Text;

                string strTemplateFileName = "RouteTemplate.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName = "TuyenBanHang_" + ddlThang.Text + "_" + ddlNam.Text + "(" + _storename + ")";

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
                Worksheet sheet_EMPLOYEE = excelWorkbook1.Worksheets[1];
                Worksheet sheet_GEOGRAPHY = excelWorkbook1.Worksheets[2];

                sheet_DATA.Name = "DATA";
                sheet_EMPLOYEE.Name = "EMPLOYEE";
                sheet_GEOGRAPHY.Name = "GEOGRAPHY";

                // dua du lieu vao sheet
                ImportDataTable(sheet_DATA, data.Tables["DATA"]);
                ImportDataTable(sheet_EMPLOYEE, data.Tables["EMPLOYEE"]);
                ImportDataTable(sheet_GEOGRAPHY, data.Tables["GEOGRAPHY"]);

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
                string _filename = "";
                string _storename = "";
                string _thang = "";
                string _nam = "";
                string _store_id = "";

                _storename = cbxStore.Text;
                _thang = ddlThang.Text;
                _nam = ddlNam.Text;
                _store_id = cbxStore.SelectedValue;

                DataSet ds = new DataSet();

                string sQuery = "";

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    sQuery = @"SELECT  a.store_id ,
                                    b.store_name ,
                                    a.customer_id ,
                                    customer_code ,
                                    customer_name ,
                                    mobile ,
                                    a.phone ,
                                    a.email ,

                                    a.channel_id ,
                                    route_id ,
                                    address ,
                                    add_number ,
                                    province ,
                                    district ,
                                    ward ,
                                    street ,
                                    a.employee_id ,
                                    employee_name
                            FROM    dbo.customer AS a
                                    LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
                                    LEFT JOIN dbo.employee AS c ON a.employee_id = c.employee_id
                            WHERE   a.store_id = {0}";

                    sQuery = string.Format(sQuery, _store_id);

                    SqlCommand cmd = new SqlCommand(sQuery, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60000;

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "DATA");
                    conn.Close();

                    sQuery = @"select employee_id,employee_code,employee_name from employee where  store_id = {0}";
                    sQuery = string.Format(sQuery, _store_id);
                    SqlCommand cmd1 = new SqlCommand(sQuery, conn);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandTimeout = 60000;
                    conn.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(ds, "EMPLOYEE");
                    conn.Close();

                    sQuery = @"select * from geo_data";
                    sQuery = string.Format(sQuery, _store_id);
                    SqlCommand cmd2 = new SqlCommand(sQuery, conn);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandTimeout = 60000;
                    conn.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(ds, "GEOGRAPHY");
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