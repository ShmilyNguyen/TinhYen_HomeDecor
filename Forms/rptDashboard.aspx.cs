using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WKS.DMS.WEB.Reports
{
    public partial class rptDashboard : System.Web.UI.Page
    {
        public string arrData1 = "";
        public string arrData2 = "";
        public string arrData3 = "";
        public string arrData4 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            arrData1 = @"['Region', 'Sales', 'Target'],
              ['NORTH1', 1000, 400],
              ['NORTH2', 1170, 460],
              ['CENTRAL', 660, 1120],
              ['SOUTH EAST', 1030, 540],
                ['HCM', 1030, 540],
                ['MEKONG', 1030, 540]
                ";

            arrData2 = @"['Year', 'Sales', 'Expenses'],
              ['2004', 1000, 400],
              ['2005', 1170, 460],
              ['2006', 660, 1120],
              ['2007', 1030, 540]";

            arrData3 = @"['Region', 'MTD Sale Value'],
                          ['NORTH1', 1000],
              ['NORTH2', 1170],
              ['CENTRAL', 660],
              ['SOUTH EAST', 1030],
                ['HCM', 1030],
                ['MEKONG', 1030]
                ";


            arrData4 = @"
['Month', 'NORTH1', 'NORTH2', 'CENTRAL', 'SOUTH EAST', 'HCM', 'MEKONG'],
          ['2004/05',  165,      938,         522,             998,           450,      614.6],
          ['2005/06',  135,      1120,        599,             1268,          288,      682],
          ['2006/07',  157,      1167,        587,             807,           397,      623],
          ['2007/08',  139,      1110,        615,             968,           215,      609.4],
          ['2008/09',  136,      691,         629,             1026,          366,      569.6]
";
        }
    }
}