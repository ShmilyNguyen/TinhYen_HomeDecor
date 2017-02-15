using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms.Custom_Import
{
    public partial class import_mcp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();
            }

           

        }


        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            
        }



        

        private void BindList()
        {

            try
            {
                string sQuery = @"select store_id,store_name from store where is_Active=1";
                sQuery = string.Format(sQuery);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                cbxStore1.DataSource = tb;
                cbxStore1.DataBind();
                cbxStore1.Items.Insert(0, new RadComboBoxItem(null, "0"));



            }
            catch (Exception ex)
            {


            }






        }

        protected void cbxStore1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string sQuery = @"SELECT  employee_id ,
                                            ISNULL(employee_code, '') + '-' + ISNULL(employee_name, '') AS employee_name
                                    FROM    dbo.employee WHERE store_id={0}";

                sQuery = string.Format(sQuery, cbxStore1.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                cbxEmployee1.DataSource = tb;
                cbxEmployee1.DataBind();
                cbxEmployee1.Items.Insert(0, new RadComboBoxItem(null, "0"));
                

            }
            catch (Exception ex)
            {
                
                
            }
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
                    BulkCopyData(tb,cbxStore1.SelectedValue,cbxEmployee1.SelectedValue);
                    //RadWindowManager1.RadAlert("Import Thành Công!", 330, 180, "Thông báo", null, null);
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


        public string GenID(string store_id)
        {
            try
            {
                //Add New
                string ID = "";
                string Code = "";
                string UserID = Session["userid"].ToString();

                clsCodeMaster.GenCode_WithStoreID(store_id, "customer", UserID, out ID, out Code);
                return ID;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void BulkCopyData(DataTable tb,string store_id, string employee_id)
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

                foreach (DataRow r in tb.Rows)
                {
                    string customer_code = (r["customer_code"] ?? "").ToString();
                    string customer_name = (r["customer_name"] ?? "").ToString();
                    
                    string route_id = (r["route_id"] ?? "").ToString();
                    string phone = (r["phone"] ?? "").ToString();
                    string mobile = (r["mobile"] ?? "").ToString();

                    string address = (r["address"] ?? "").ToString();
                    string add_number = (r["add_number"] ?? "").ToString();
                    string province = (r["province"] ?? "").ToString();
                    string district = (r["district"] ?? "").ToString();
                    string ward = (r["ward"] ?? "").ToString();
                    string street = (r["street"] ?? "").ToString();

                    string email = (r["email"] ?? "").ToString();
                    string channel_id = (r["channel_id"] ?? "").ToString();

                    if (string.IsNullOrEmpty(channel_id) || channel_id=="")
                    {
                        channel_id = "0";
                    }
                    

                    string sQuery = "DELETE FROM customer WHERE store_id={0} AND customer_code='{1}'";
                    sQuery = string.Format(sQuery,store_id,customer_code);
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    string new_customer_id = GenID(store_id);
                    sQuery = @"INSERT  INTO dbo.customer
                                        ( customer_id ,
                                          customer_code ,
                                          store_id ,
                                          customer_name ,
                                          mobile ,
                                          phone ,
                                          email ,
                                          birthday ,
                                          channel_id ,
                                          route_id ,
                                          address ,
                                          add_number ,
                                          province ,
                                          district ,
                                          ward ,
                                          street ,
                                          active ,
                                          employee_id ,
                                          created_date 

                                        )
                                VALUES  ( " + new_customer_id + @" , -- customer_id - bigint
                                          '" +customer_code+@"' , -- customer_code - varchar(50)
                                          " + store_id + @" , -- store_id - int
                                          N'" + customer_name + @"' , -- customer_name - nvarchar(100)
                                          '" + mobile + @"' , -- mobile - varchar(50)
                                          '" + phone + @"' , -- phone - varchar(50)
                                          '" + email + @"' , -- email - varchar(50)
                                          GETDATE() , -- birthday - date
                                          " + channel_id + @" , -- channel_id - int
                                          " + route_id + @" , -- route_id - int
                                         
                                          N'" + address + @"' , -- address - nvarchar(200)
                                          N'" + add_number + @"' , -- add_number - nvarchar(100)
                                          N'" + province + @"' , -- province - nvarchar(100)
                                          N'" + district + @"' , -- district - nvarchar(100)
                                          N'" + ward + @"' , -- ward - nvarchar(100)
                                          N'" + street + @"' , -- street - nvarchar(100)
                                          1 , -- active - bit
                                          " + employee_id + @" , -- employee_id - int
                                          GETDATE()  -- created_date - datetime
        
                                        )";


                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                }


                RadWindowManager1.RadAlert("Import thành công !", 330, 180, "Thông báo", null, null);

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

                string strTemplateFileName = "mcp_template.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");

                string strOutputFileName = "mcp_template";

             
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