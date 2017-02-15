using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class sellin_edit : System.Web.UI.Page
    {
        public string _id = "";
        public string _urlPrint = "#";

        public DataTable myData
        {
            get
            {
                DataTable data = GetData();

                return data;
            }
        }


        public void Load_Item_List()
        {
            try
            {
                string storeProc = "[sp_HangHoa_List_SellIn_v2]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon)) {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

               
                    cmd.Parameters.AddWithValue("@trans_date_numb", clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
                    cmd.Parameters.AddWithValue("@price_group_id", cbxBoGia.SelectedValue);


                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable();
                    da.Fill(data);
                    conn.Close();

                    cbxHangHoa.DataSource = data;
                    cbxHangHoa.DataBind();
                    cbxHangHoa.Items.Insert(0, new RadComboBoxItem(null, null));
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void cbxHangHoa_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
               
                e.Item.Attributes["item_price"] = ((DataRowView)e.Item.DataItem)["item_price"].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GetBoGia(int trans_date_numb)
        {
            try
            {

                string channel_dist_id = "0";

                string sQuery = @"SELECT  ISNULL(channel_dist_id, 0) AS channel_dist_id
                                FROM    dbo.store
                                WHERE   store_id = {0}";

                sQuery = string.Format(sQuery,rddlNhaPhanPhoi.SelectedValue);
                channel_dist_id = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery, null).ToString();

                sQuery = @" SELECT  price_group_id ,
                                            price_group_code +'-'+ price_group_name AS price_group_code ,
                                            price_group_name
                                    FROM    dbo.price_group
                                    WHERE   {0} BETWEEN fromdate AND todate AND channel_dist_id={1} AND is_active = 1";



                sQuery = string.Format(sQuery,trans_date_numb, channel_dist_id);
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery,null).Tables[0];
                cbxBoGia.DataSource = data;
                cbxBoGia.DataBind();



            }
            catch (Exception)
            {


            }
        }



        public DataTable GetData()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * from v_SellIn where sellin_id='" + hdf_Sellin_ID.Value + "'";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();
                Session["Data_Sellin_Detail"] = data;
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.myData;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();
            string sQuery = "delete from [sellin_detail] where [row_id]=" + id;
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(sQuery, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    result = Convert.ToInt32(cmd.ExecuteNonQuery());
                    conn.Close();
                    ReloadGrid();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

                    System.Web.UI.WebControls.TextBox txtRowID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtRowID");
                    System.Web.UI.WebControls.TextBox txtItemID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtItemID");
                    System.Web.UI.WebControls.TextBox txtCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtCode");
                    System.Web.UI.WebControls.TextBox txtName = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtName");
                    System.Web.UI.WebControls.TextBox txtQty = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtQty");

                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string row_id = parentItem["row_id"].Text;
                        string item_id = parentItem["item_id"].Text;
                        string item_code = parentItem["item_code"].Text;
                        string name = parentItem["name"].Text;
                        string qty = parentItem["qty"].Text;

                        txtRowID.Text = row_id == "&nbsp;" ? "" : row_id;
                        txtRowID.Enabled = false;

                        txtItemID.Text = item_id == "&nbsp;" ? "" : item_id;
                        txtItemID.Enabled = false;

                        txtCode.Text = item_code == "&nbsp;" ? "" : item_code;
                        txtCode.Enabled = false;

                        txtName.Text = name == "&nbsp;" ? "" : name;

                        txtQty.Text = qty == "&nbsp;" ? "0" : qty;

                        string sellin_type = parentItem["sellin_type"].Text;
                        cbxLoaiNhap.SelectedValue = sellin_type;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHeaderItem headerItem = e.Item as GridHeaderItem;
            if (headerItem != null)
            {
                headerItem["EditColumn"].Text = string.Empty;
                headerItem["DeleteColumn"].Text = string.Empty;
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    //Update Data

                    try
                    {
                        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                        string row_id = ((userControl.FindControl("txtRowID") as System.Web.UI.WebControls.TextBox).Text.Trim());
                        string item_id = ((userControl.FindControl("txtItemID") as System.Web.UI.WebControls.TextBox).Text.Trim());

                        string sellin_type = (userControl.FindControl("cbxLoaiNhap") as RadComboBox).SelectedValue;

                        string qty = (userControl.FindControl("txtQty") as System.Web.UI.WebControls.TextBox).Text.Trim();

                        string storeProc = "[usp_InsertUpdatesellin_detail]";
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(row_id))
                            {
                                cmd.Parameters.AddWithValue("@row_id", int.Parse(row_id));
                                cmd.Parameters.AddWithValue("@sellin_id", int.Parse(hdf_Sellin_ID.Value));
                                cmd.Parameters.AddWithValue("@sellin_type", sellin_type);
                                cmd.Parameters.AddWithValue("@item_id", int.Parse(item_id));
                                cmd.Parameters.AddWithValue("@qty", int.Parse(qty));
                                cmd.Parameters.AddWithValue("@item_price",cbxHangHoa.SelectedItem.Attributes["item_price"].ToString());

                                conn.Open();
                                int result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();
                            }
                        }

                        Refresh_Data();
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                }

                //if (e.CommandName == "Delete_Row")
                //{
                //    string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();
                //    string sQuery = "delete from [store] where [store_id]=" + id;
                //    int result = 0;
                //    try
                //    {
                //        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                //        {
                //            SqlCommand cmd = new SqlCommand(sQuery, conn);
                //            cmd.CommandType = CommandType.Text;

                //            conn.Open();
                //            result = Convert.ToInt32(cmd.ExecuteNonQuery());
                //            conn.Close();
                //            Refresh_Data();
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Disable_Controls();

                BindList();
                

            string  id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    BindControls();
                    GetBoGia(clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
                    Load_Item_List();
                    Gen_SellIn_Code();
                    Update_Header();
                }
                else
                {
                    ReloadData(id);
                    if (!Check_Valid_LockDate())
                    {
                        Disable_Controls();
                    }
                }
            }

            _id = hdf_Sellin_ID.Value;

            _urlPrint = "/Print/rpt_InPhieuNhap.aspx?id=" + _id;

            Iframe1.Attributes["src"] = _urlPrint;
        }

        public bool Check_Valid_LockDate()
        {
            string store_id = "";
            string flag = "";
            string flag2 = "";

            string query = "select * from dbo.sellin_header where sellin_id ='{0}' ";
            query = string.Format(query, _id);

            DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, query).Tables[0];
            foreach (DataRow r in tb.Rows)
            {
                 store_id = r["store_id"].ToString();
            }


                try
            {
                string storeProc = "[sp_sys_config_check_lockdate_sellin]";
                string storeProcExtra = "[sp_sys_config_check_lockdate_sellin_extra]";

                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    SqlCommand cmd2 = new SqlCommand(storeProcExtra, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);
                    cmd2.Parameters.AddWithValue("@store_id", store_id);

                    conn.Open();
                    flag = Convert.ToString(cmd.ExecuteScalar());
                    flag2 = Convert.ToString(cmd2.ExecuteScalar());


                    conn.Close();

                 

                  
                }
                if (bool.Parse(flag) || bool.Parse(flag2))
                {

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Disable_Controls()
        {
            try
            {
                string position = Session["position"].ToString();
                if (position.Contains("ADMIN"))
                {
                    btnAddRow.Enabled = true;
                    btnDeleteOrder.Enabled = true;
                    btnSave.Enabled = true;
                    RadGrid1.MasterTableView.Columns[7].Visible = true;
                    RadGrid1.MasterTableView.Columns[8].Visible = true;
                }
                else
                {
                    btnAddRow.Enabled = false;
                    btnDeleteOrder.Enabled = false;
                    btnSave.Enabled = false;

                    RadGrid1.MasterTableView.Columns[7].Visible = false;
                    RadGrid1.MasterTableView.Columns[8].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Gen_SellIn_Code()
        {
            try
            {
                string storeProc = "usp_Sys_Gen_ID";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(hdf_User_ID.Value))
                    {
                        cmd.Parameters.AddWithValue("@UserID", hdf_User_ID.Value);
                        cmd.Parameters.AddWithValue("@Prefix", "NK");

                        conn.Open();
                        string result = Convert.ToString(cmd.ExecuteScalar());
                        txtsellin_code.Text = result;
                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BindList()
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
          

            rddlNhaPhanPhoi.DataSource = tb;
            rddlNhaPhanPhoi.DataBind();

            rddlNhaPhanPhoi_SelectedIndexChanged(null, null);
            //Load_Item_List();
        }

        public void BindControls()
        {
            try
            {
                txtLastModified.Text = DateTime.Now.ToString();
                rdpNgayGiaoDich.DbSelectedDate = DateTime.Now;
                txtNguoiLap.Text = Session["username"].ToString();

                string sQuery = @"SELECT ISNULL( MAX( sellin_id ),0 ) + 1   FROM dbo.sellin_header;";
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();

                hdf_Sellin_ID.Value = result;

                hdf_User_ID.Value = Session["userid"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadGrid()
        {
            Refresh_Data();
            RadGrid1.DataSource = this.myData;
            RadGrid1.DataBind();
        }

        public void ReloadData(string id)
        {
            try
            {
                string sQuery = "SELECT TOP 1 * from v_SellIn where sellin_id=" + id;
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string sellin_id = (r["sellin_id"] ?? "").ToString();
                    string sellin_code = (r["sellin_code"] ?? "").ToString();
                    string store_id = (r["store_id"] ?? "").ToString();

                    System.Diagnostics.Debug.WriteLine("store_id : " + store_id);
                    string note = (r["note"] ?? "").ToString();
                    string trans_date_gmt = (r["trans_date_gmt"] ?? "").ToString();
                    string trans_date_numb = (r["trans_date_numb"] ?? "").ToString();
                    string created_by = (r["created_by"] ?? "").ToString();
                    string last_modified = (r["last_modified"] ?? "").ToString();


                    string price_group_id = (r["price_group_id"] ?? "").ToString();


                    hdf_Sellin_ID.Value = sellin_id;
                    txtsellin_code.Text = sellin_code;
                    rddlNhaPhanPhoi.SelectedValue = store_id;
                    rdpNgayGiaoDich.DbSelectedDate = trans_date_gmt;
                    txtNguoiLap.Text = created_by;
                    hdf_User_ID.Value = created_by;
                    txtLastModified.Text = last_modified;
                    txtGhiChu.Text = note;

                    GetBoGia(clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
                    cbxBoGia.SelectedValue = price_group_id;

                    ReloadGrid();
                }
            }
            catch (Exception)
            {
            }
        }

        public void Update_Header()
        {
            string storeProc = "[usp_InsertUpdatesellin_header]";
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_Sellin_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@sellin_id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@sellin_code", txtsellin_code.Text);
                        cmd.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@trans_date_numb", clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
                        cmd.Parameters.AddWithValue("@store_id", int.Parse(rddlNhaPhanPhoi.SelectedValue));
                        cmd.Parameters.AddWithValue("@employee_id", int.Parse(hdf_User_ID.Value));
                        cmd.Parameters.AddWithValue("@note", txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@created_by", hdf_User_ID.Value);
                        cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@price_group_id", cbxBoGia.SelectedValue);

                        conn.Open();
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            Refresh_Data();
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            string storeProc = "[usp_Insertsellin_detail]";
            int result = 0;
            string channel_dist_id = Session["channel_dist_id"].ToString().Trim();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_Sellin_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                        cmd.Parameters.AddWithValue("@sellin_id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@sellin_type", cbxLoaiNhap.SelectedValue);
                        cmd.Parameters.AddWithValue("@item_id", int.Parse(cbxHangHoa.SelectedValue));
                        cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text.Trim()));
                        // cmd.Parameters.AddWithValue("@item_price", cbxHangHoa.SelectedItem.Attributes["item_price"].ToString());
                        cmd.Parameters.AddWithValue("@channel_dist_id", channel_dist_id);
                        cmd.Parameters.AddWithValue("@price_group_id", cbxBoGia.SelectedValue);
                         

                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            ReloadGrid();
            txtQty.Text = "0";
            cbxHangHoa.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string position = Session["position"].ToString();
            if (!Check_Valid_LockDate())
            {
                if (!position.Contains("ADMIN"))
                {
                    RadWindowManager1.RadAlert("Ngày nhập kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                    return;
                }
            }

            Update_Header();
            ReloadGrid();
        }

        protected void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "delete from sellin_detail where sellin_id=" + hdf_Sellin_ID.Value;
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "delete from sellin_header where sellin_id=" + hdf_Sellin_ID.Value;
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                Response.Redirect("Sellin-List.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sellin-List.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("sellin-edit.aspx");
        }

        protected void cbxBoGia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetBoGia(clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
            Load_Item_List();
        }

        protected void rdpNgayGiaoDich_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            GetBoGia(clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
        }

        protected void rddlNhaPhanPhoi_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                GetBoGia(clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));
                Load_Item_List();
            }
            catch (Exception ex)
            {

               
            }
           
        }
    }
}