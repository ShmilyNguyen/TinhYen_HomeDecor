using Aspose.Cells;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WKS.DMS.WEB.Report
{
    public partial class rpt_BaoCaoTonKhoTheoNgay2_GiaTri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();
                BindList();
            }
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

        private void ImportDataTable(Worksheet sheet, DataTable tbData)
        {

            // import vao excel
            sheet.Cells.ImportDataTable(tbData, false, "A11");
            //Autofit all the columns in the sheet
            sheet.AutoFitColumns();

            //Putting some values to cells
            //sheet.Cells["A1"].PutValue("1st Value");
            //sheet.Cells["A2"].PutValue("2nd Value");
            //sheet.Cells["A3"].PutValue("Sum");
            //sheet.Cells["B1"].PutValue(125.56);
            //sheet.Cells["B2"].PutValue(23.93);


            int rowN = 0; ;
            string cell1 = "";

            for (int i = 0; i < tbData.Rows.Count; i++)
            {
                rowN = 11 + i;
                //Adding a simple formula to "B3" cell
                sheet.Cells["J" + rowN].Formula = "=(F" + rowN + " + G" + rowN + " - H" + rowN + " - I" + rowN + ")" + " * E" + rowN;
                sheet.Cells["N" + rowN].Formula = "=((J" + rowN + " / E" + rowN + ")" + " + K" + rowN + " - L" + rowN + " - M" + rowN + ")" + " * E" + rowN;
                sheet.Cells["R" + rowN].Formula = "=((N" + rowN + " / E" + rowN + ")" + " + O" + rowN + " - P" + rowN + " - Q" + rowN + ")" + " * E" + rowN;
                sheet.Cells["V" + rowN].Formula = "=((R" + rowN + " / E" + rowN + ")" + " + S" + rowN + " - T" + rowN + " - U" + rowN + ")" + " * E" + rowN;
                sheet.Cells["Z" + rowN].Formula = "=((V" + rowN + " / E" + rowN + ")" + " + W" + rowN + " - X" + rowN + " - Y" + rowN + ")" + " * E" + rowN;
                sheet.Cells["AD" + rowN].Formula = "=((Z" + rowN + " / E" + rowN + ")" + " + AA" + rowN + " - AB" + rowN + " - AC" + rowN + ")" + " * E" + rowN;
                sheet.Cells["AH" + rowN].Formula = "=((AD" + rowN + " / E" + rowN + ")" + " + AE" + rowN + " - AF" + rowN + " - AG" + rowN + ")" + " * E" + rowN;
                sheet.Cells["AL" + rowN].Formula = "=((AH" + rowN + " / E" + rowN + ")" + " + AI" + rowN + " - AJ" + rowN + " - AK" + rowN + ")" + " * E" + rowN;
                sheet.Cells["AP" + rowN].Formula = "=((AL" + rowN + " / E" + rowN + ")" + " + AM" + rowN + " - AN" + rowN + " - AO" + rowN + ")" + " * E" + rowN;
                sheet.Cells["AT" + rowN].Formula = "=((AP" + rowN + " / E" + rowN + ")" + " + AQ" + rowN + " - AR" + rowN + " - AS" + rowN + ")" + " * E" + rowN;


                sheet.Cells["AX" + rowN].Formula = "=((AT" + rowN + " / E" + rowN + ")" + " + AU" + rowN + " - AV" + rowN + " - AW" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BB" + rowN].Formula = "=((AX" + rowN + " / E" + rowN + ")" + " + AY" + rowN + " - AZ" + rowN + " - BA" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BF" + rowN].Formula = "=((BB" + rowN + " / E" + rowN + ")" + " + BC" + rowN + " - BD" + rowN + " - BE" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BJ" + rowN].Formula = "=((BF" + rowN + " / E" + rowN + ")" + " + BG" + rowN + " - BH" + rowN + " - BI" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BN" + rowN].Formula = "=((BJ" + rowN + " / E" + rowN + ")" + " + BK" + rowN + " - BL" + rowN + " - BM" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BR" + rowN].Formula = "=((BN" + rowN + " / E" + rowN + ")" + " + BO" + rowN + " - BP" + rowN + " -BQ" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BV" + rowN].Formula = "=((BR" + rowN + " / E" + rowN + ")" + " + BS" + rowN + " - BT" + rowN + " - BU" + rowN + ")" + " * E" + rowN;
                sheet.Cells["BZ" + rowN].Formula = "=((BV" + rowN + " / E" + rowN + ")" + " + BW" + rowN + " - BX" + rowN + " - BY" + rowN + ")" + " * E" + rowN;
                sheet.Cells["CD" + rowN].Formula = "=((BZ" + rowN + " / E" + rowN + ")" + " + CA" + rowN + " - CB" + rowN + " - CC" + rowN + ")" + " * E" + rowN;
                sheet.Cells["CH" + rowN].Formula = "=((CD" + rowN + " / E" + rowN + ")" + " + CE" + rowN + " - CF" + rowN + " - CG" + rowN + ")" + " * E" + rowN;

                sheet.Cells["CL" + rowN].Formula = "=((CH" + rowN + " / E" + rowN + ")" + " + CI" + rowN + " - CJ" + rowN + " - CK" + rowN + ")" + " * E" + rowN;
                sheet.Cells["CP" + rowN].Formula = "=((CL" + rowN + " / E" + rowN + ")" + " + CM" + rowN + " - CN" + rowN + " - CO" + rowN + ")" + " * E" + rowN;
                sheet.Cells["CT" + rowN].Formula = "=((CP" + rowN + " / E" + rowN + ")" + " + CQ" + rowN + " - CR" + rowN + " - CS" + rowN + ")" + " * E" + rowN;
                sheet.Cells["CX" + rowN].Formula = "=((CT" + rowN + " / E" + rowN + ")" + " + CU" + rowN + " - CV" + rowN + " - CW" + rowN + ")" + " * E" + rowN;
                sheet.Cells["DB" + rowN].Formula = "=((CX" + rowN + " / E" + rowN + ")" + " + CY" + rowN + " - CZ" + rowN + " - DA" + rowN + ")" + " * E" + rowN;
                sheet.Cells["DF" + rowN].Formula = "=((DB" + rowN + " / E" + rowN + ")" + " + DC" + rowN + " - DD" + rowN + " - DE" + rowN + ")" + " * E" + rowN;
                sheet.Cells["DJ" + rowN].Formula = "=((DF" + rowN + " / E" + rowN + ")" + " + DG" + rowN + " - DH" + rowN + " - DI" + rowN + ")" + " * E" + rowN;
                sheet.Cells["DN" + rowN].Formula = "=((DJ" + rowN + " / E" + rowN + ")" + " + DK" + rowN + " - DL" + rowN + " - DM" + rowN + ")" + " * E" + rowN;
                sheet.Cells["DR" + rowN].Formula = "=((DN" + rowN + " / E" + rowN + ")" + " + DO" + rowN + " - DP" + rowN + " - DQ" + rowN + ")" + " * E" + rowN;
                sheet.Cells["DV" + rowN].Formula = "=((DR" + rowN + " / E" + rowN + ")" + " + DS" + rowN + " - DT" + rowN + " - DU" + rowN + ")" + " * E" + rowN;

                sheet.Cells["DZ" + rowN].Formula = "=((DV" + rowN + " / E" + rowN + ")" + " + DW" + rowN + " - DX" + rowN + " - DY" + rowN + ")" + " * E" + rowN;


                //Cot Total

                sheet.Cells["ED" + rowN].Formula = "=F" + rowN + " + EA" + rowN + " - EB" + rowN + " - EC" + rowN;
                sheet.Cells["EE" + rowN].Formula = "=ED" + rowN + " * E" + rowN;
                sheet.Cells["EF" + rowN].Formula = "=EB" + rowN + " * E" + rowN;

            }
        }



        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                string storeProc = "[sp_rpt_BaoCaoDoanhThuTonKhoTheoNgay2]";
                int result = 0;

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@data_month", ddlThang.SelectedValue);
                    cmd.Parameters.AddWithValue("@data_year", ddlNam.SelectedValue);




                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();
                }



                string strTemplateFileName = "BaoCaoTonKhoNgayTheoGiaTri.xls";
                string strSrcPathReport = Server.MapPath("Exports/Templates/");
                string strDesPathReport = Server.MapPath("Exports/Outputs/");

                string srcFilePath = Server.MapPath("Exports/Templates/");
                string desFilePath = Server.MapPath("Exports/Outputs/");



                string strOutputFileName = "BaoCaoTonKhoNgayTheoGiaTri" + cbxStore.SelectedItem.Text + "_" + ddlThang.SelectedValue + "_" + ddlNam.SelectedValue;



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
    }
}