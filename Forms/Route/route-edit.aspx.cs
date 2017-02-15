using System;
using System.Data;
using System.Web.UI;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms
{
    public partial class route_edit : System.Web.UI.Page
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

                sQuery = @"select * from store where store_id IN ( SELECT  store_id
                                                    FROM    dbo.fn_GetStore_By_UserID({0}) )";
                sQuery = string.Format(sQuery, Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxNhaPhanPhoi.DataSource = tb;
                cbxNhaPhanPhoi.DataBind();
                cbxNhaPhanPhoi.Items.Insert(0, new RadComboBoxItem(null, null));

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

                clsCodeMaster.GenCode("route", UserID, out ID, out Code);
                txtID.Text = ID;
                txtCode.Text = Code;
                txtCode.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadData(string id)
        {
            try
            {
                string sQuery = "SELECT * from route where route_id={0}";
                sQuery = string.Format(sQuery, id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string route_id = r["route_id"] == DBNull.Value ? "" : r["route_id"].ToString();
                    string route_code = r["route_code"] == DBNull.Value ? "" : r["route_code"].ToString();
                    string route_name = r["route_name"] == DBNull.Value ? "" : r["route_name"].ToString();
                    string store_id = r["store_id"] == DBNull.Value ? "" : r["store_id"].ToString();
                    



                    txtID.Text = route_id;
                    txtID.Enabled = false;

                    txtCode.Text = route_code;
                    //txtCode.Enabled = false;

                    txtName.Text = route_name;
                    cbxNhaPhanPhoi.SelectedValue = store_id;

                    cbxNhaPhanPhoi_SelectedIndexChanged(null, null);
                  
                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxNhaPhanPhoi_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                


                string route_id = txtID.Text.Trim();
                string route_code = txtCode.Text.Trim();
                string route_name = txtName.Text.Trim();
             
                string store_id = cbxNhaPhanPhoi.SelectedValue;



                string storeProc = "[usp_InsertUpdateroute]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@route_id", route_id);
                    cmd.Parameters.AddWithValue("@route_code", route_code);
                    cmd.Parameters.AddWithValue("@route_name", route_name);
                   
                    cmd.Parameters.AddWithValue("@store_id", store_id);
                   


                    conn.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();

                    Response.Redirect("route-list.aspx");
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("route-list.aspx");
        }




        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;

                string route_id = "";
                string route_sub_id = "";

                route_id = id;

                //Xoa Route Sub
                string sQuery = @"select route_sub_id from route_sub_header where route_id={0}";
                sQuery = string.Format(sQuery,route_id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    try
                    {
                        route_sub_id = r["route_sub_id"].ToString();

                        //Xoa table con
                        sQuery = @"DELETE  FROM dbo.route_sub_detail where route_sub_id={0}";
                        sQuery = string.Format(sQuery, route_sub_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                   

                }

                //Xoa table cha
                sQuery = @"DELETE  FROM dbo.route_sub_header where route_id={0}";
                sQuery = string.Format(sQuery, route_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //Xoa Route chinh

                sQuery = @"DELETE  FROM dbo.route where route_id={0}";
                sQuery = string.Format(sQuery, route_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

               

                Response.Redirect("route-list.aspx");


            }
            catch (Exception ex)
            {


            }
        }



    }
}