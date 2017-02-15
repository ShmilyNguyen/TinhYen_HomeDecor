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

namespace WKS.DMS.WEB.Forms
{
    public partial class ucThongBao : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindRepeater();
            }
        }

        private void BindRepeater()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * FROM dbo.cms_notice WHERE GETDATE() BETWEEN [from] AND [to] ORDER BY priority ASC";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                rptThongBao.DataSource = data;
                rptThongBao.DataBind();

            }catch(Exception e){

            }

           
        }



    }
}