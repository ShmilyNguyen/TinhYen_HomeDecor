using DevExpress.XtraPrinting;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms
{
    public partial class saleout_edit_3 : System.Web.UI.Page
    {
        public static string _saleout_edit_url = "";

        public string _id = "";
        public string _urlPrint = "#";
        public static bool _flag_only_update_header = false;
        public static bool _is_returned_order = false;
        public static bool _is_confirmed = false;

        public int OnTopDiscount
        {
            get
            {
                return this.ViewState["OnTopDiscount"] == null ? 0 : (int)this.ViewState["OnTopDiscount"];
            }
            set
            {
                this.ViewState["OnTopDiscount"] = value;
            }
        }

        public DataTable myData
        {
            get
            {
                DataTable data = GetData();

                return data;
            }
        }

        public DataTable GetData()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * from v_SaleOut where SaleOut_id='" + hdf_SaleOut_ID.Value + "' and saleout_type='HB'";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public void Reload_XuatKM()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * from v_SaleOut where SaleOut_id='" + hdf_SaleOut_ID.Value + "' and saleout_type='KM'";
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                RadGrid2.DataSource = data;
                RadGrid2.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();
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
            string sQuery = "delete from [SaleOut_detail] where [row_id]=" + id;
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

                    RadComboBox cbxCTKM = (RadComboBox)MyUserControl.FindControl("cbxCTKM");
                    string sQuery = "select * from promotion";
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    cbxCTKM.DataSource = tb;
                    cbxCTKM.DataBind();
                    cbxCTKM.Items.Insert(0, new RadComboBoxItem(null, null));

                    System.Web.UI.WebControls.TextBox txtDiscount = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtDiscount");

                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string row_id = parentItem["row_id"].Text;
                        string item_id = parentItem["item_id"].Text;
                        string item_code = parentItem["item_code"].Text;
                        string name = parentItem["name"].Text;
                        string qty = parentItem["qty"].Text;
                        string discount = parentItem["discount"].Text;
                        string promo_id = parentItem["promo_id"].Text;

                        string saleout_type = parentItem["saleout_type"].Text;

                        txtRowID.Text = row_id == "&nbsp;" ? "" : row_id;
                        txtRowID.Enabled = false;

                        txtItemID.Text = item_id == "&nbsp;" ? "" : item_id;
                        txtItemID.Enabled = false;

                        txtCode.Text = item_code == "&nbsp;" ? "" : item_code;
                        txtCode.Enabled = false;

                        txtName.Text = name == "&nbsp;" ? "" : name;

                        txtQty.Text = qty == "&nbsp;" ? "0" : qty;

                        txtDiscount.Text = discount == "&nbsp;" ? "0" : discount;

                        cbxLoaiXuat.SelectedValue = saleout_type;
                        cbxCTKM.SelectedValue = promo_id;
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

                        string qty = (userControl.FindControl("txtQty") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string saleout_type = (userControl.FindControl("cbxLoaiXuat") as RadComboBox).SelectedValue;
                        string promo_id = (userControl.FindControl("cbxCTKM") as RadComboBox).SelectedValue;

                        string sQuery = "";
                        sQuery = "update saleout_detail set qty = {0} where row_id = {1}";
                        sQuery = string.Format(sQuery, qty, row_id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        Refresh_Data();

                        UpdateThanhTien();

                        TinhToanKM();

                        Reload_XuatKM();
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// This script sets a focus to the control with a name to which
        /// REQUEST_LASTFOCUS was replaced. Setting focus heppens after the page
        /// (or update panel) was rendered. To delay setting focus the function
        /// window.setTimeout() will be used.
        /// </summary>
        private const string SCRIPT_DOFOCUS =
              @"window.setTimeout('DoFocus()', 1);
            function DoFocus()
            {
                try {
                    document.getElementById('REQUEST_LASTFOCUS').focus();
                } catch (ex) {}
            }";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (RadAjaxPanel1.IsAjaxRequest || RadAjaxPanel2.IsAjaxRequest)
            //{
            //   txtOntopDiscount.Text = OnTopDiscount.ToString();
            //}

            if (!Page.IsPostBack)
            {
                
                _saleout_edit_url = DMS.WEB.Libs.clsProcessOrder.Order_GetUrlForm();

                BindList();

                string id = Request.QueryString["id"];

                if (string.IsNullOrEmpty(id))
                {
                    BindControls();
                    //Gen_SaleOut_Code();
                    GenCode();
                    Update_Header(true);

                    Load_Item_List();
                    Reload_XuatKM();

                    cbxEmployee.Focus();
                }
                else
                {
                    ReloadData(id);
                    Load_Item_List();

                    if (!Check_Valid_LockDate())
                    {
                        Disable_Controls(true, false, false, false, false);
                    }
                }

                _id = hdf_SaleOut_ID.Value;
            }
        }

        /// <summary>
        /// This function goes recursively all child controls and sets
        /// onfocus attribute if the control has one of defined types.
        /// </summary>
        /// <param name="CurrentControl">the control to hook.</param>
        private void HookOnFocus(Control CurrentControl)
        {
            //checks if control is one of TextBox, DropDownList, ListBox or Button
            if ((CurrentControl is System.Web.UI.WebControls.TextBox) ||
                (CurrentControl is System.Web.UI.WebControls.DropDownList) ||
                (CurrentControl is System.Web.UI.WebControls.ListBox) ||
                (CurrentControl is System.Web.UI.WebControls.Button))
                //adds a script which saves active control on receiving focus in the hidden field __LASTFOCUS.
                (CurrentControl as System.Web.UI.WebControls.WebControl).Attributes.Add(
                    "onfocus",
                    "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {}");

            //checks if the control has children
            if (CurrentControl.HasControls())
                //if yes do them all recursively
                foreach (Control CurrentChildControl in CurrentControl.Controls)
                    HookOnFocus(CurrentChildControl);
        }

        public void GenCode()
        {
            try
            {
                //Add New
                string _id = "";
                string _code = "";
                string _userid = Session["userid"].ToString();

                clsCodeMaster.GenCode("xuatkho", _userid, out _id, out _code);
                hdf_SaleOut_ID.Value = _id;
                txtSaleOut_Code.Text = _code;
            }
            catch (Exception ex)
            {
            }
        }

        public void Load_Item_List()
        {
            try
            {
                string storeProc = "[sp_HangHoa_List]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);

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

        public void Load_Promo_List()
        {
            try
            {
                string storeProc = "[sp_CTKM_List]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@item_id", cbxHangHoa.SelectedValue);
                    cmd.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);
                    cmd.Parameters.AddWithValue("@saleout_id", hdf_SaleOut_ID.Value);

                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable();
                    da.Fill(data);
                    conn.Close();

                    cbxCTKM.DataSource = data;
                    cbxCTKM.DataBind();
                    cbxCTKM.Items.Insert(0, new RadComboBoxItem(null, null));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void BindList()
        {
            try
            {
                cbxLoaiXuat.SelectedIndex = 0;

                string sQuery = "";

                sQuery = @"SELECT  route_id ,
                                route_code + '-' + route_name AS route_name
                        FROM    route_weekdays";

                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxWeekDays.DataSource = tb;
                cbxWeekDays.DataBind();
                cbxWeekDays.Items.Insert(0, new RadComboBoxItem("Tất cả", "0"));

                sQuery = @"SELECT  a.store_id ,
                                               store_name
                                        FROM    dbo.store AS a

                                                WHERE a.store_id  IN (
                                                                SELECT  store_id
                                                                FROM    dbo.fn_GetStore_By_UserID({0}) )

                                        ";

                sQuery = string.Format(sQuery, Session["userid"]);
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxStore.DataSource = tb;
                cbxStore.DataBind();

                try
                {
                    cbxStore.SelectedIndex = 0;
                    cbxStore_SelectedIndexChanged(null, null);
                }
                catch (Exception ex)
                {
                }

                string storeProc = "[sp_Price_Policy_List]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);

                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable();
                    da.Fill(data);
                    conn.Close();

                    cbxChinhSachGia.DataSource = data;
                    cbxChinhSachGia.DataBind();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BindControls()
        {
            try
            {
                txtLastModified.Text = DateTime.Now.ToString();
                rdpNgayGiaoDich.DbSelectedDate = DateTime.Now;
                txtNguoiLap.Text = Session["username"].ToString();

                //string sQuery = @"SELECT ISNULL( MAX( SaleOut_id ),0 ) + 1   FROM dbo.SaleOut_header;";
                //string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();

                //hdf_SaleOut_ID.Value = result;

                hdf_User_ID.Value = Session["userid"].ToString();
            }
            catch (Exception ex)
            {
            }
        }


        public void Disable_Controls(bool is_readonly, bool is_confirmed, bool is_locked, bool is_returned, bool is_returned_order)
        {
            try
            {
                if (is_readonly)
                {
                    txtSaleOut_Code.Enabled = false;
                    rdpNgayGiaoDich.Enabled = false;
                    //txtNguoiLap.Enabled = false;
                    //txtLastModified.Enabled = false;
                    cbxStore.Enabled = false;
                    cbxEmployee.Enabled = false;
                    cbxWeekDays.Enabled = false;
                    cbxKhachHang.Enabled = false;
                    cbxChinhSachGia.Enabled = false;
                    txtGhiChu.Enabled = false;
                    txtOntopDiscount.Enabled = false;
                    btnAddRow.Enabled = false;
                    btnTinhToanKM.Enabled = false;

                    cbxLoaiXuat.Enabled = false;
                    cbxCTKM.Enabled = false;
                    cbxHangHoa.Enabled = false;
                    txtQty.Enabled = false;

                    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                }

                if (is_confirmed)
                {
                    btnDeleteOrder.Enabled = false;
                    btnSave.Enabled = false;
                    btnTinhToanKM.Enabled = false;
                    btnAddRow.Enabled = false;
                    btnReturnOrder.Enabled = true;
                }

                if (is_locked)
                {
                    btnDeleteOrder.Enabled = false;
                    btnSave.Enabled = false;
                    btnTinhToanKM.Enabled = false;
                    btnReturnOrder.Enabled = false;
                    btnAddRow.Enabled = false;
                }

                if (is_returned || is_returned_order)
                {
                    btnReturnOrder.Enabled = false;
                }
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

            Reload_XuatKM();

            UpdateThanhTien();
        }

        public void ReloadData(string id)
        {
            try
            {
                string sQuery = "SELECT TOP 1 * from v_SaleOut where SaleOut_id=" + id;
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string SaleOut_id = (r["SaleOut_id"] ?? "").ToString();
                    string SaleOut_code = (r["SaleOut_code"] ?? "").ToString();
                    string store_id = (r["store_id"] ?? "").ToString();
                    string route_id = (r["route_id"] ?? "").ToString();

                    string employee_id = (r["employee_id"] ?? "").ToString();

                    string customer_id = (r["customer_id"] ?? "").ToString();
                    string note = (r["note"] ?? "").ToString();
                    string trans_date_gmt = (r["trans_date_gmt"] ?? "").ToString();
                    string trans_date_numb = (r["trans_date_numb"] ?? "").ToString();
                    string created_by = (r["created_by"] ?? "").ToString();

                    string created_by_name = (r["created_by_name"] ?? "").ToString();

                    string last_modified = (r["last_modified"] ?? "").ToString();

                    string item_price_policy_id = (r["item_price_policy_id"] ?? "").ToString();

                    bool is_confirmed = (bool)((r["is_confirmed"]));
                    bool is_locked = (bool)((r["is_locked"]));
                    bool is_returned = (bool)((r["is_returned"]));
                    bool is_returned_order = (bool)((r["is_returned_order"]));

                    _is_returned_order = is_returned_order;
                    _is_confirmed = is_confirmed;

                    //             is_locked ,
                    //is_returned ,
                    //is_returned_order ,

                    hdf_SaleOut_ID.Value = SaleOut_id;
                    txtSaleOut_Code.Text = SaleOut_code;

                    cbxStore.SelectedValue = store_id;
                    cbxStore_SelectedIndexChanged(null, null);

                    cbxEmployee.SelectedValue = employee_id;
                    cbxEmployee_SelectedIndexChanged(null, null);

                    cbxWeekDays.SelectedValue = route_id;
                    cbxWeekDays_SelectedIndexChanged(null, null);

                    rdpNgayGiaoDich.DbSelectedDate = trans_date_gmt;

                    txtNguoiLap.Text = created_by_name;
                    hdf_User_ID.Value = created_by;
                    txtLastModified.Text = last_modified;
                    txtGhiChu.Text = note;

                    cbxKhachHang.SelectedValue = customer_id;

                    cbxChinhSachGia.SelectedValue = item_price_policy_id;

                    ReloadGrid();

                    //Reload_XuatKM();

                    //Disable cac control

                    bool is_readonly = false;
                    if (is_confirmed || is_locked || is_returned || is_returned_order)
                    {
                        is_readonly = true;
                    }
                    Disable_Controls(is_readonly, is_confirmed, is_locked, is_returned, is_returned_order);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Update_Header(bool isNew)
        {
            string storeProc = "";

            if (isNew)
            {
                storeProc = "[usp_Insertsaleout_header]";
            }
            else
            {
                storeProc = "[usp_InsertUpdatesaleout_header]";
            }

            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_SaleOut_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@SaleOut_code", txtSaleOut_Code.Text);
                        cmd.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@trans_date_numb", clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value));

                        cmd.Parameters.AddWithValue("@employee_id", cbxEmployee.SelectedValue);

                        cmd.Parameters.AddWithValue("@store_id", int.Parse(cbxStore.Text == "" ? "0" : cbxStore.SelectedValue));
                        cmd.Parameters.AddWithValue("@route_id", cbxWeekDays.SelectedValue);
                        cmd.Parameters.AddWithValue("@customer_id", Int64.Parse(cbxKhachHang.Text == "" ? "0" : cbxKhachHang.SelectedValue));
                        cmd.Parameters.AddWithValue("@item_price_policy_id", cbxChinhSachGia.SelectedValue);
                        cmd.Parameters.AddWithValue("@note", txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@created_by", hdf_User_ID.Value);
                        cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ontop_discount", float.Parse(txtOntopDiscount.Text.Trim()));
                        cmd.Parameters.AddWithValue("@ontop_discount_code", DBNull.Value);

                        cmd.Parameters.AddWithValue("@is_confirmed", false);
                        cmd.Parameters.AddWithValue("@is_returned", false);
                        cmd.Parameters.AddWithValue("@parent_id", DBNull.Value);

                        conn.Open();
                        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            Refresh_Data();
        }

        public bool CheckValidData()
        {
            try
            {
                if (cbxEmployee.SelectedValue == "" || cbxEmployee.SelectedValue == "0")
                {
                    RadWindowManager1.RadAlert("Vui lòng chọn nhân viên!", 330, 180, "Thông báo", null, null);
                    return false;
                }

                if (cbxKhachHang.SelectedValue == "" || cbxKhachHang.SelectedValue == "0")
                {
                    RadWindowManager1.RadAlert("Vui lòng chọn khách hàng!", 330, 180, "Thông báo", null, null);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckValidRow()
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập số lượng bán!", 330, 180, "Thông báo", null, null);
                    return false;
                }

                string inv_qty = this.cbxHangHoa.SelectedItem.Attributes["qty"];
                string qty = txtQty.Text.Trim();

                if (int.Parse(inv_qty) < int.Parse(qty))
                {
                    RadWindowManager1.RadAlert("Hết hàng ,vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            if (!CheckValidData())
            {
                return;
            }

            if (!CheckValidRow())
            {
                return;
            }

            string storeProc = "[usp_Insertsaleout_detail]";
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_SaleOut_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                        cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@item_id", int.Parse(cbxHangHoa.SelectedValue));
                        cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text.Trim()));
                        cmd.Parameters.AddWithValue("@saleout_type", cbxLoaiXuat.SelectedValue);
                        cmd.Parameters.AddWithValue("@discount", 0);

                        if (string.IsNullOrEmpty(cbxCTKM.SelectedValue))
                        {
                            cmd.Parameters.AddWithValue("@promo_id", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@promo_id", int.Parse(cbxCTKM.SelectedValue));
                        }

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

            //cbxCTKM.SelectedIndex = 0;
            //cbxHangHoa.SelectedIndex = 0;

            cbxCTKM.Items.Clear();
            cbxCTKM.Text = "";
            cbxCTKM.SelectedIndex = -1;
            cbxHangHoa.SelectedIndex = 0;
            txtQty.Text = "";
            cbxLoaiXuat.Focus();
        }

        public bool Check_Valid_LockDate()
        {
            try
            {

                string flag1 = "";
                string flag2 = "";

                string storeProc = "[sp_sys_config_check_lockdate_saleout]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd1 = new SqlCommand(storeProc, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    cmd1.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);

                    conn.Open();
                    flag1 = Convert.ToString(cmd1.ExecuteScalar());
                    conn.Close();


                }

                storeProc = "[sp_sys_config_check_lockdate_inventory_closing]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd2 = new SqlCommand(storeProc, conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@trans_date_gmt", rdpNgayGiaoDich.DbSelectedDate);
                    cmd2.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);

                    conn.Open();
                    flag2 = Convert.ToString(cmd2.ExecuteScalar());
                    conn.Close();


                }

                if (bool.Parse(flag1) && bool.Parse(flag2))
                {
                    return true;
                }
                else
                {
                    return false;
                }



                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Trong truong hop don hang da duoc Confirm thi khong cho phep Luu thong tin don hang lai nua
            if (_is_confirmed)
            {
                return;
            }

            if (!CheckValidData())
            {
                return;
            }

            string position = Session["position"].ToString();
            if (!Check_Valid_LockDate())
            {
                RadWindowManager1.RadAlert("Ngày xuất kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                return;
            }

            Update_Header(false);
            TinhToanKM();
            UpdateThanhTien();
            ReloadGrid();
            Reload_XuatKM();
            UpdateARDoc();

            #region Xac nhan don hang

            string id = hdf_SaleOut_ID.Value;
            int result = WKS.DMS.WEB.Libs.clsProcessOrder.Order_Confirmed_By_Store(cbxStore.SelectedValue, id, Session["userid"].ToString());

            if (result == 1)
            {
                //RadWindowManager1.RadAlert("Đơn hàng đã được Lưu và Xác nhận thành công !", 330, 180, "Thông báo", null, null);
            }
            

            if (result == 2)
            {
                RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin nhân viên, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
            }

            if (result == 3)
            {
                RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin khách hàng, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
            }

            if (result == 0)
            {
                RadWindowManager1.RadAlert("Đơn hàng không thể xác nhận do hàng tồn kho không đủ, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                
            }

            #endregion Xac nhan don hang
        }

        public void UpdateARDoc()
        {
            try
            {
                //Kiem tra saleout_id trong ARDoc
                string sQuery = "";

                sQuery = @"IF EXISTS ( SELECT  reference_id
                                        FROM    dbo.ARDoc
                                        WHERE   reference_id = {0} )
                                SELECT  1 AS result
                            ELSE
                                SELECT  0 AS result";

                sQuery = string.Format(sQuery, hdf_SaleOut_ID.Value);
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();

                if (result == "1")
                {
                    //Update
                    sQuery = @"UPDATE  dbo.ARDoc
                                SET     original_doc_amount = {0}
                                WHERE   reference_id = {1}
                                        AND doc_type = 'IN'";

                    sQuery = string.Format(sQuery, txtGTThanhToan.Text, hdf_SaleOut_ID.Value);

                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                }
                else
                {
                    //Insert
                    //GenCode
                    string _id = "";
                    string _code = "";
                    string _userid = Session["userid"].ToString();

                    clsCodeMaster.GenCode("ardoc-in", _userid, out _id, out _code);

                    string storeProc = "[usp_InsertUpdateARDoc]";
                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@doc_id", _id);
                        cmd.Parameters.AddWithValue("@doc_code", _code);
                        cmd.Parameters.AddWithValue("@doc_date", rdpNgayGiaoDich.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@doc_type", "IN");
                        cmd.Parameters.AddWithValue("doc_balance ", txtGTThanhToan.Text);
                        cmd.Parameters.AddWithValue("@current_doc_balance", txtGTThanhToan.Text);
                        cmd.Parameters.AddWithValue("@original_doc_amount", txtGTThanhToan.Text);
                        cmd.Parameters.AddWithValue("@release ", 0);
                        cmd.Parameters.AddWithValue("@reference_id", hdf_SaleOut_ID.Value);
                        cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@note", "");
                        cmd.Parameters.AddWithValue("@created_by", Session["userid"]);
                        cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "delete from saleout_detail where saleout_id=" + hdf_SaleOut_ID.Value;
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "delete from saleout_header where saleout_id=" + hdf_SaleOut_ID.Value;
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "delete from ARDoc where reference_id = {0}";
                sQuery = string.Format(sQuery, hdf_SaleOut_ID.Value);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                Response.Redirect("saleout-list.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
            Response.Redirect("saleout-list.aspx");
        }

        public void UpdateThanhTien()
        {
            string congthuc_tinhthanhtien = "CKONTOP_SAU_CKLINEHANG";

            try
            {
                string sQuery = "";
                if (congthuc_tinhthanhtien == "CKONTOP_SAU_CKLINEHANG")
                {
                    sQuery = @"[usp_saleout_gettotalvalues]";
                }

                DataTable data = new DataTable();
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@saleout_id", hdf_SaleOut_ID.Value);
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                foreach (DataRow r in data.Rows)
                {
                    CultureInfo us = new CultureInfo("en-US");
                    txtThanhTien.Text = r["GTBan"].ToString();
                    txtGTCK.Text = r["GTChietKhauDongHang"].ToString();
                    txtOntopDiscount.Text = r["OntopDiscount"].ToString();
                    txtTotalOntopDiscount.Text = r["GTChietKhauNPP"].ToString();
                    txtGTThanhToan.Text = r["GTThanhToan"].ToString();
                }

                //Update thanh tien vao Don Hang
                Libs.clsProcessOrder.Order_UpdateAmount(hdf_SaleOut_ID.Value, decimal.Parse(txtGTThanhToan.Text));
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave_Click(null, null);
                Response.Redirect(_saleout_edit_url);
            }
            catch (Exception ex)
            {
            }
        }

        public void Bind_CustomerByRouteWeekDays()
        {
            try
            {
                string sQuery = @"[usp_Customer_List_By_RouteWeekDays]";
                SqlParameter[] arrSQLParam = new SqlParameter[3];
                arrSQLParam[0] = new SqlParameter("@store_id", cbxStore.SelectedValue);
                arrSQLParam[1] = new SqlParameter("@employee_id", cbxEmployee.SelectedValue);
                arrSQLParam[2] = new SqlParameter("@route_id", cbxWeekDays.SelectedValue);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                cbxKhachHang.DataSource = data;
                cbxKhachHang.DataBind();

                cbxKhachHang.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }

        public void Bind_CustomerBy_Employee()
        {
            try
            {
                string sQuery = @"[usp_Customer_List_By_Employee]";
                SqlParameter[] arrSQLParam = new SqlParameter[2];
                arrSQLParam[0] = new SqlParameter("@store_id", cbxStore.SelectedValue);
                arrSQLParam[1] = new SqlParameter("@employee_id", cbxEmployee.SelectedValue);

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];
                cbxKhachHang.DataSource = data;
                cbxKhachHang.DataBind();

                cbxKhachHang.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }

        public void Bind_Customer_All()
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
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnTinhToanKM_Click(object sender, EventArgs e)
        {
            if (!CheckValidData())
            {
                return;
            }

            //btnSave_Click(null, null);
            Update_Header(false);
            TinhToanKM();
            //TinhChietKhauNhomKhachHang();

            ReloadGrid();
            Reload_XuatKM();
        }

        public void CTKM_Hardcode()
        {
            try
            {
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.Host);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.Authority);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.Port);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.AbsolutePath);
                //Response.Write("<br/> " + HttpContext.Current.Request.ApplicationPath);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.AbsoluteUri);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.PathAndQuery);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.Query);
                //Response.Write("<br/> " + HttpContext.Current.Request.Url.Fragment);

                string url = HttpContext.Current.Request.Url.Host;
                if (url.Contains("thuanviet.wikisoft.vn") || url.Contains("localhost"))
                {
                    if (cbxStore.SelectedValue == "7" || cbxStore.SelectedValue == "12" || cbxStore.SelectedValue == "13" || cbxStore.SelectedValue == "14" || cbxStore.SelectedValue == "15")
                    {
                        int NgayGiaoDich = clsCommon.ConvertDateToNumber(rdpNgayGiaoDich.SelectedDate.Value);
                        decimal ThanhTien = decimal.Parse(txtGTThanhToan.Text.Replace(",", ""));
                        if (NgayGiaoDich >= 20151009 && NgayGiaoDich <= 20151031)
                        {
                            if (ThanhTien >= 500000)
                            {
                                int nMulti = (int)(ThanhTien / 500000);
                                if (nMulti >= 1)
                                {
                                    string saleout_id = hdf_SaleOut_ID.Value;
                                    string promo_id = "40";
                                    string item_id = "38";
                                    int vol = 1;

                                    clsPromotion.TinhToanKM_TangHang_Insert(saleout_id, promo_id, item_id, vol, nMulti);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void TinhToanKM()
        {
            int item_price = int.Parse(cbxHangHoa.SelectedItem.Attributes["item_price"].ToString());
            try
            {
                Libs.clsPromotion.TinhChietKhauDonHang_TheoLoaiKH(hdf_SaleOut_ID.Value , int.Parse(Session["channel_dist_id"].ToString()));
                Libs.clsPromotion.TinhToanKM_ChietKhau(hdf_SaleOut_ID.Value);
                Libs.clsPromotion.TinhToanKM_TangHang_Rule_1(hdf_SaleOut_ID.Value  );
                //Libs.clsPromotion.TinhToanKM_TangHang_Level2(hdf_SaleOut_ID.Value);
                //Libs.clsPromotion.TinhToanKM_TangHang_Level3(hdf_SaleOut_ID.Value);
                //Libs.clsPromotion.TinhToanKM_TangHang_Level4(hdf_SaleOut_ID.Value);

                CTKM_Hardcode();
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxHangHoa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Load_Promo_List();
                //txtQty.Focus();
            }
            catch (Exception ex)
            {
                hdf_SLTonKho.Value = "0";
            }
        }

        protected void cbxKhachHang_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Update_Header(false);
        }

        #region Combobox Change

        protected void cbxStore_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string sQuery = @"select * from employee where store_id={0} and position ='SR'";
                sQuery = string.Format(sQuery, cbxStore.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxEmployee.DataSource = tb;
                cbxEmployee.DataBind();
                cbxEmployee.Items.Insert(0, new RadComboBoxItem(null, null));

                Load_Item_List();

                RadAjaxPanel2.ResponseScripts.Add(String.Format("$find('{0}').ajaxRequest();", RadAjaxPanel1.ClientID));
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxEmployee_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Bind_CustomerBy_Employee();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion Combobox Change

        protected void tbnReturnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiem tra don hang da Confirm hay chua
                if (_is_confirmed)
                {
                    Response.Redirect("saleout-return-edit.aspx?id=" + hdf_SaleOut_ID.Value);
                }
                else
                {
                    RadWindowManager1.RadAlert("Đơn hàng chưa xác nhận, bạn không thể trả đơn hàng !", 330, 180, "Thông báo", null, null);
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnInDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _totalThanhTien = 0;
                decimal _totalChietKhau = 0;
                decimal _totalThanhTienSauCKRow = 0;
                decimal _totalGTCKDH = 0;
                decimal _totalPhaiThu = 0;
                string _SoTienBangChu = "";

                _totalChietKhau = decimal.Parse(txtGTCK.Text);
                _totalGTCKDH = decimal.Parse(txtTotalOntopDiscount.Text);
                _totalPhaiThu = decimal.Parse(txtGTThanhToan.Text);
                _SoTienBangChu = clsCommon.DoiSoThanhChu1(_totalPhaiThu);

                string _url = "PrintPreview.aspx?p0={0}&&p1={1}&&p2={2}&&p3={3}&&p4={4}";
                _url = string.Format(_url, "saleout", hdf_SaleOut_ID.Value, _totalChietKhau, _totalGTCKDH, _totalPhaiThu);
                Response.Redirect(_url);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnTinhLaiCongNo_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateARDoc();
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxWeekDays_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cbxWeekDays.SelectedValue == "0")
            {
                Bind_CustomerBy_Employee();
            }

            if (cbxWeekDays.SelectedValue == "")
            {
                Bind_CustomerBy_Employee();
            }

            if (cbxWeekDays.SelectedValue != "0")
            {
                Bind_CustomerByRouteWeekDays();
            }
        }

        protected void cbxHangHoa_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                e.Item.Attributes["qty"] = ((DataRowView)e.Item.DataItem)["qty"].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void rdpNgayGiaoDich_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                Load_Item_List();
                RadAjaxPanel2.ResponseScripts.Add(String.Format("$find('{0}').ajaxRequest();", RadAjaxPanel1.ClientID));
            }
            catch (Exception ex)
            {
            }
        }
    }
}