using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using WKS.DMS.WEB.Libs;


namespace WKS.DMS.WEB.Forms.TrungBay
{

    


    public partial class trungbay_list : System.Web.UI.Page
    {

        public void GetData()
        {
            DataTable data = new DataTable();
            string sQuery = @"SELECT  email ,
        cuahang ,
        diachi ,
        ngay ,
        img_ftp_uri1 ,
        img_ftp_uri2 ,
        img_ftp_uri3 ,
        chamdiem ,
        danhgia ,
        ghichu ,
        scheme ,
        lat1 ,
        lng1 ,
        lat2 ,
        lng2,
		ISNULL(lat1,'') + ',' + ISNULL(lng1,'') AS gps1,
		ISNULL(lat2,'') + ',' + ISNULL(lng2,'') AS gps2
FROM    dbo.trungbay_list";
            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

            rptTrungBay.DataSource = data;
            rptTrungBay.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetData();
            }

        }

       

    }

}