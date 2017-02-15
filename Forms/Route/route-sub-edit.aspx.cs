using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms.Route
{
    public partial class route_sub_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();

                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    GenCode();
                }
                else
                {
                    ReloadData(id);
                }
            }
        }

        public void BindList()
        {
            try
            {
                string sQuery = "";

//                sQuery = @"SELECT  a.* ,
//                                    b.employee_name ,
//                                    c.route_code ,
//                                    c.route_name ,
//                                    s.store_name ,
//                                    s.store_id ,
//                                    s.store_code
//                            FROM    dbo.route_sub_header AS a
//                                    LEFT JOIN dbo.store AS s ON a.store_id = s.store_id
//                                    LEFT JOIN dbo.employee AS b ON a.employee_id = b.employee_id
//                                    LEFT JOIN dbo.route AS c ON a.route_id = c.route_id
//                            WHERE   a.store_id IN ( SELECT  store_id
//                                                    FROM    dbo.fn_GetStore_By_UserID({0}) )
//                            ORDER BY route_code ,
//                                    route_sub_code";

                sQuery = @"select * from store where store_id IN ( SELECT  store_id
                                                    FROM    dbo.fn_GetStore_By_UserID({0}) )";


                sQuery = string.Format(sQuery, Session["userid"]);

                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = tb;
                cbxStore.DataBind();
                cbxStore.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }

        public void BindGrid()
        {
            try
            {
                string sQuery = "";

                sQuery = @"
SELECT  a.* ,
        cus.customer_code ,
        cus.customer_name ,
        '(' + phone + ')--' + ISNULL(add_number, '') + '-' + ISNULL(address,
                                                              '') + '-'
        + ISNULL(province_name, '') + '-' + ISNULL(district_name, '') + '-'
        + ISNULL(ward_name, '') + '-' + ISNULL(street_name, '') AS address
FROM    dbo.route_sub_detail AS a
        LEFT JOIN route_sub_header AS h ON a.route_sub_id = h.route_sub_id
        LEFT JOIN dbo.route AS r ON a.row_id = r.route_id
        LEFT JOIN dbo.customer AS cus ON a.customer_id = cus.customer_id
        LEFT JOIN dbo.geo_province AS b ON cus.province_id = b.geo_province_id
        LEFT JOIN dbo.geo_district AS c ON cus.district_id = c.geo_district_id
        LEFT JOIN dbo.geo_ward AS d ON cus.ward_id = d.geo_ward_id
        LEFT JOIN dbo.geo_street AS e ON cus.street_id = e.geo_street_id
WHERE   h.store_id = {0}
        AND h.route_id = {1}
        AND h.route_sub_id = {2}
        AND employee_id = {3}

ORDER BY a.visit_order ASC

                            ";

                sQuery = string.Format(sQuery, cbxStore.SelectedValue, cbxRoute.SelectedValue, txtID.Text, cbxEmployee.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadGrid1.DataSource = tb;
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        public void GenCode()
        {
            try
            {
                //Add New
                string ID = "";
                string Code = "";
                string UserID = Session["userid"].ToString();

                clsCodeMaster.GenCode("route-sub", UserID, out ID, out Code);
                txtID.Text = ID;
                //txtCode.Text = Code;
                //txtCode.Focus();

                cbxMaTuyenThu.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadData(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    string sQuery = "select * from route_sub_header where route_sub_id={0}";
                    sQuery = string.Format(sQuery, id);
                    string store_id = "";
                    string route_id = "";
                    string route_sub_id = "";
                    string route_sub_code = "";
                    string route_sub_name = "";
                    string employee_id = "";

                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    if (tb.Rows.Count > 0)
                    {
                        DataRow r = tb.Rows[0];
                        store_id = r["store_id"] == DBNull.Value ? "" : r["store_id"].ToString();
                        route_id = r["route_id"] == DBNull.Value ? "" : r["route_id"].ToString();
                        route_sub_id = r["route_sub_id"] == DBNull.Value ? "" : r["route_sub_id"].ToString();
                        route_sub_code = r["route_sub_code"] == DBNull.Value ? "" : r["route_sub_code"].ToString();
                        route_sub_name = r["route_sub_name"] == DBNull.Value ? "" : r["route_sub_name"].ToString();
                        employee_id = r["employee_id"] == DBNull.Value ? "" : r["employee_id"].ToString();

                        cbxStore.SelectedValue = store_id;
                        cbxStore_SelectedIndexChanged(null, null);
                        txtID.Text = route_sub_id;
                        cbxMaTuyenThu.Text = route_sub_code;
                        txtName.Text = route_sub_name;
                        cbxRoute.SelectedValue = route_id;
                        cbxEmployee.SelectedValue = employee_id;

                        BindGrid();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Update Data

            try
            {
                string sQuery = @"INSERT INTO dbo.route_sub_detail
                                        ( route_sub_id ,
                                          customer_id ,
                                          visit_order
                                        )
                                VALUES  ( {0} , -- route_sub_id - int
                                          {1} , -- customer_id - bigint
                                          {2}  -- visit_order - int
                                        )";
                sQuery = string.Format(sQuery, txtID.Text, cbxKhachHang.SelectedValue, txtVisitOrder.Text);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();

                string sQuery = @"DELETE  FROM dbo.route_sub_detail where row_id={0}";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string storeProc = "[usp_InsertUpdateroute_sub_header]";
                int result = 0;
                try
                {
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@route_sub_id", txtID.Text);
                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@route_id", cbxRoute.SelectedValue);
                        cmd.Parameters.AddWithValue("@route_sub_code", cbxMaTuyenThu.SelectedValue);
                        cmd.Parameters.AddWithValue("@route_sub_name", txtName.Text);
                        cmd.Parameters.AddWithValue("@day_of_week", 0);
                        cmd.Parameters.AddWithValue("@employee_id", cbxEmployee.SelectedValue);
                        cmd.Parameters.AddWithValue("@active", 1);

                        conn.Open();
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("route-sub-list.aspx");
        }

        protected void cbxStore_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string sQuery = @"[usp_Customer_List_All]";
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@store_id", cbxStore.SelectedValue);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                cbxKhachHang.DataSource = data;
                cbxKhachHang.DataBind();
                cbxKhachHang.Items.Insert(0, new RadComboBoxItem(null, null));

                sQuery = @"select * from route where store_id={0}";
                sQuery = string.Format(sQuery, cbxStore.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxRoute.DataSource = tb;
                cbxRoute.DataBind();
                cbxRoute.Items.Insert(0, new RadComboBoxItem(null, null));

                sQuery = @"select * from employee where store_id={0}";
                sQuery = string.Format(sQuery, cbxStore.SelectedValue);
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxEmployee.DataSource = tb;
                cbxEmployee.DataBind();
                cbxEmployee.Items.Insert(0, new RadComboBoxItem(null, null));

                BindGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;

                //Xoa table con
                string sQuery = @"DELETE  FROM dbo.route_sub_detail where route_sub_id={0}";
                sQuery = string.Format(sQuery, id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //Xoa table cha
                sQuery = @"DELETE  FROM dbo.route_sub_header where route_sub_id={0}";
                sQuery = string.Format(sQuery, id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                Response.Redirect("route-sub-list.aspx");
            }
            catch (Exception ex)
            {
            }
        }
    }
}