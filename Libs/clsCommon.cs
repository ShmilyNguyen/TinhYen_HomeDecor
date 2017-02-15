using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using log4net;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Data;
using System.Data.OleDb;

namespace WKS.DMS.WEB
{
    public class clsCommon
    {
        public static string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["SQLCon"].ConnectionString;
        public static string UrlRoot = ConfigurationManager.AppSettings["URLRoot"];
        public static string XMLPath = ConfigurationManager.AppSettings["XMLPath"];
        public static string UploadPath = ConfigurationManager.AppSettings["UploadPath"];

        public static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region UserInfo

        public static string UserID;
        public static string UserName;
        public static string GroupID; //GroupID = 1 - Admin, GroupID = 0 System


        #endregion


        /// <summary>
        /// Gets the base part of the URL 'http://www."
        /// </summary>
        /// 

        

        public static string UrlRoot2
        {
            get
            {


                Uri url = HttpContext.Current.Request.Url;
                string newUrl;


                newUrl = string.Format("{0}{1}{2}", url.Scheme, Uri.SchemeDelimiter, url.Authority);


                return newUrl;
            }
        }


        /// <summary>
        /// Returns the Virtual Path.
        /// </summary>
        /// <remarks>The path will start and end with a /</remarks>
        /// <returns></returns>
        public static string VirtualPath
        {
            get
            {
                string path = HttpRuntime.AppDomainAppVirtualPath;


                if (!path.StartsWith("/"))
                {
                    path = "/" + path;
                }
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }


                return path;
            }
        }



        public static int ConvertDateToNumber(DateTime dateDate)
        {
            //int result = dateDate.Year * 10000000000 + dateDate.Month * 100000000 + dateDate.Day * 1000000 + dateDate.Hour * 10000 + dateDate.Minute * 100 + dateDate.Second;
            int result = dateDate.Year * 10000 + dateDate.Month * 100 + dateDate.Day;
            return result;
        }

        /// <summary>
        /// Contains the child folder portion of the path.
        /// </summary>
        public static string ApplicationFolder
        {
            get
            {
                string[] segments = HttpContext.Current.Request.Url.Segments;
                string newUrl = "";


                for (int x = 0; x < segments.Length - 1; x++)
                {
                    newUrl += segments[x];
                }


                string virtualPath = VirtualPath;


                if (newUrl.StartsWith(virtualPath))
                {
                    newUrl = newUrl.Substring(virtualPath.Length);
                }


                return newUrl;
            }
        }


        public static string ConvertToUnSign3(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "0";
            }
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ReFormatCode_NoSpace(string sCode)
        {
            sCode = ConvertToUnSign3(sCode);
            sCode = sCode.Replace(" ", "");
            sCode = sCode.Replace("  ", "");
            sCode = sCode.Replace("   ", "");
            sCode = sCode.ToUpper();
            return sCode;
        }


        public static void ShowMessageBox(string sMessage, System.Web.UI.HtmlControls.HtmlGenericControl objHTML)
        {
            string sMsg = "";
            sMsg = sMsg + "<script language='javascript'>";
            sMsg = sMsg + "alert('" + sMessage + "');";
            sMsg = sMsg + "</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "messagebox", "alert('" + sMessage + "');");
            objHTML.InnerHtml = sMsg;


        }


        public static string DoiSoThanhChu1(decimal number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return str + "đồng chẵn";
        }

        public static string DoiSoThanhChu2(double number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            double decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDouble(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return str + "đồng chẵn";
        }

        public void BulkCopy(string sFilePath, DataTable tblSrc, string SrcTableName, string DesTableName, string SrcColArr, string DesColArr, string sBulkType)
        {



            string SQLCon = "";


            //Create connection string to Excel work book
            string excelConnectionString =
            @"Provider=Microsoft.Jet.OLEDB.4.0;
            Data Source=" + sFilePath.Trim() + @";
            Extended Properties=""Excel 8.0;HDR=YES;""";

            //Create Connection to Excel work book
            OleDbConnection excelConnection =
            new OleDbConnection(excelConnectionString);

            //Create OleDbCommand to fetch data from Excel
            OleDbCommand cmd = new OleDbCommand
            ("Select [ID],[Name],[Location] from [Detail$]",
            excelConnection);

            excelConnection.Open();
            OleDbDataReader dReader;
            dReader = cmd.ExecuteReader();

            SqlBulkCopy sqlBulk = new SqlBulkCopy(SQLCon);
            sqlBulk.DestinationTableName = "DanhSachKhachHang";

            //sqlBulk.ColumnMappings.Add("ID", "ID");
            //sqlBulk.ColumnMappings.Add("Name", "Name");
            sqlBulk.WriteToServer(dReader);




        }

        public static void BulkCopyTable(string SQLCon, DataTable tblSrc, string DesTableName, int[] SrcColArr, string[] DesColArr)
        {

            SqlBulkCopy sqlBulk = new SqlBulkCopy(SQLCon);
            sqlBulk.DestinationTableName = DesTableName;
            int n = SrcColArr.Length;
            int i = 0;
            for (i = 0; i < n; i++)
            {
                sqlBulk.ColumnMappings.Add(SrcColArr[i], DesColArr[i]);
            }

            sqlBulk.WriteToServer(tblSrc);

        }

        public static void BulkCopyTable(string SQLCon, DataTable tblSrc, string DesTableName, string[] SrcColArr, string[] DesColArr)
        {

            SqlBulkCopy sqlBulk = new SqlBulkCopy(SQLCon);
            //sqlBulk.BulkCopyTimeout = 500;
            sqlBulk.DestinationTableName = DesTableName;
            int n = SrcColArr.Length;
            int i = 0;
            for (i = 0; i < n; i++)
            {
                sqlBulk.ColumnMappings.Add(SrcColArr[i], DesColArr[i]);
            }

            sqlBulk.WriteToServer(tblSrc);
            sqlBulk.Close();
            sqlBulk = null;


        }

        public static void BulkCopyTable(string SQLCon, DataTable tblSrc, string DesTableName, string[] SrcColArr, string[] DesColArr, int BatchSize)
        {

            SqlBulkCopy sqlBulk = new SqlBulkCopy(SQLCon);
            sqlBulk.BulkCopyTimeout = 600;
            sqlBulk.BatchSize = BatchSize;
            sqlBulk.DestinationTableName = DesTableName;
            int n = SrcColArr.Length;
            int i = 0;
            for (i = 0; i < n; i++)
            {
                sqlBulk.ColumnMappings.Add(SrcColArr[i], DesColArr[i]);
            }

            sqlBulk.WriteToServer(tblSrc);
            sqlBulk.Close();
            sqlBulk = null;


        }

        public static string GetChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

    }
}