using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms
{
    public partial class customer_edit : System.Web.UI.Page
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


                sQuery = @"SELECT DISTINCT province_name FROM geo_data ORDER BY province_name";
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxProvince.DataSource = tb;
                cbxProvince.DataBind();
                cbxProvince.Items.Insert(0, new RadComboBoxItem(null, null));


                //sQuery = @"select * from geo_province";
                //DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                //cbxProvince.DataSource = tb;
                //cbxProvince.DataBind();
                //cbxProvince.Items.Insert(0, new RadComboBoxItem(null, null));

                //sQuery = @"select * from geo_district";
                //tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                //cbxDistrict.DataSource = tb;
                //cbxDistrict.DataBind();
                //cbxDistrict.Items.Insert(0, new RadComboBoxItem(null, null));

                //sQuery = @"select * from geo_ward";
                //tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                //cbxWard.DataSource = tb;
                //cbxWard.DataBind();
                //cbxWard.Items.Insert(0, new RadComboBoxItem(null, null));

                //sQuery = @"select * from geo_street";
                //tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                //cbxStreet.DataSource = tb;
                //cbxStreet.DataBind();
                //cbxStreet.Items.Insert(0, new RadComboBoxItem(null, null));

                sQuery = @"select * from store where store_id IN ( SELECT  store_id
                                                    FROM    dbo.fn_GetStore_By_UserID({0}) )";
                sQuery = string.Format(sQuery, Session["userid"]);

                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxNhaPhanPhoi.DataSource = tb;
                cbxNhaPhanPhoi.DataBind();
                //cbxNhaPhanPhoi.Items.Insert(0, new RadComboBoxItem(null, null));
                cbxNhaPhanPhoi_SelectedIndexChanged(null, null);



              

                sQuery = @"SELECT  * FROM distribute_channel";


                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxDistChannel.DataSource = tb;
                cbxDistChannel.DataBind();
                cbxDistChannel.Items.Insert(0, new RadComboBoxItem(null, null));



                sQuery = @"SELECT  route_id ,
                                route_code + '-' + route_name AS route_name
                        FROM    route_weekdays";

                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxWeekDays.DataSource = tb;
                cbxWeekDays.DataBind();
                cbxWeekDays.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {

            }

        }

        public void BindCustomerChannel()
        {
            try
            {

                string sQuery = @"SELECT  customer_channel_id ,
                                    customer_channel_code + '-' + channel_name AS channel_name
                            FROM    dbo.customer_channel
                            WHERE   channel_dist_id IN ( SELECT channel_dist_id
                                                         FROM   dbo.store
                                                         WHERE  store_id = {0} )";


                sQuery = string.Format(sQuery,cbxNhaPhanPhoi.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxChannel.DataSource = tb;
                cbxChannel.DataBind();
                cbxChannel.Items.Insert(0, new RadComboBoxItem(null, null));
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

                clsCodeMaster.GenCode_WithStoreID(cbxNhaPhanPhoi.SelectedValue, "customer", UserID, out ID, out Code);
                txtID.Text = ID;
                txtCode.Text = Code;
                txtCode.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String distID = Session["channel_dist_id"].ToString();
                string customer_id = txtID.Text.Trim();
                string customer_code = txtCode.Text.Trim();
                string store_id = cbxNhaPhanPhoi.SelectedValue;
                string customer_name = txtTenKH.Text.Trim();
                string mobile = txtDienThoai.Text.Trim();
                string phone = txtDienThoai.Text.Trim();
                string email = "";
                string birthday = "";
                string channel_id = cbxChannel.SelectedValue;
                string distribute_channel_id = cbxDistChannel.SelectedValue;


                string market_id = cbxMarket.SelectedValue;

                string route_id = cbxWeekDays.SelectedValue;

                string visit_order = txtSttViengTham.Text;

                string address = txtDiaChiDayDu.Text.Trim();
                string add_number = txtSoNha.Text.Trim();
                string province = cbxProvince.SelectedValue;
                string district = cbxDistrict.SelectedValue;
                string ward = cbxWard.SelectedValue;
                string street = cbxStreet.SelectedValue;

                string street_id = "0";
                string ward_id = "0";
                string district_id = "0";
                string province_id = "0";


                bool active = chckActive.Checked;
                decimal max_debt_vol = 0;
                decimal current_debt_vol = 0;
                string personal_id = "";
                float longitude = 0;
                float latitude = 0;
                string employee_code = "";

                string employee_id = cbxEmployee.SelectedValue;

                //Kiem tra du lieu nhap vao

                if (string.IsNullOrEmpty(txtTenKH.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập tên khách hàng", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (string.IsNullOrEmpty(txtDiaChiDayDu.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập địa chỉ đầy đủ", 330, 180, "Thông báo", null, null);
                    return;
                }


                if (string.IsNullOrEmpty(cbxProvince.SelectedValue))
                {
                    RadWindowManager1.RadAlert("Vui lòng chọn thông tin Tỉnh - Thành phố", 330, 180, "Thông báo", null, null);
                    return;
                }

                //if (string.IsNullOrEmpty(cbxDistrict.SelectedValue))
                //{
                //    RadWindowManager1.RadAlert("Vui lòng chọn thông tin Quận - Huyện", 330, 180, "Thông báo", null, null);
                //    return;
                //}


                //if (string.IsNullOrEmpty(cbxWard.SelectedValue))
                //{
                //    RadWindowManager1.RadAlert("Vui lòng chọn thông tin Xã - Phường", 330, 180, "Thông báo", null, null);
                //    return;
                //}



                if (string.IsNullOrEmpty(employee_id))
                {
                    RadWindowManager1.RadAlert("Vui lòng chọn nhân viên bán hàng", 330, 180, "Thông báo", null, null);
                        return;
                }

                //if (string.IsNullOrEmpty(distribute_channel_id))
                //{
                //    RadWindowManager1.RadAlert("Vui lòng chọn kênh phân phối", 330, 180, "Thông báo", null, null);
                //    return;
                //}


                if (string.IsNullOrEmpty(channel_id))
                {
                    RadWindowManager1.RadAlert("Vui lòng chọn kênh khách hàng", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (string.IsNullOrEmpty(route_id))
                {
                    RadWindowManager1.RadAlert("Vui lòng tuyến  bán hàng", 330, 180, "Thông báo", null, null);
                    return;
                }


                #region Kiem ra viec nhap LP

                object objPosition = Session["position"];
                string position = objPosition.ToString();
                string channel_name = cbxChannel.Text;
             


                if ((channel_name != "LP-LP" && channel_name != "") || !distID.Equals("1") )
                {
                    //if (!position.Contains("ADMIN") )
                    //{
                    //    RadWindowManager1.RadAlert("Bạn chỉ có quyền tạo khách hàng thuộc nhóm LP-LP!", 330, 180, "Thông báo", null, null);
                    //    return;
                    //}
                   

                }
                #endregion




                string storeProc = "[usp_InsertUpdatecustomer]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", customer_id);
                    cmd.Parameters.AddWithValue("@customer_code", customer_code);
                    cmd.Parameters.AddWithValue("@store_id", store_id);
                    cmd.Parameters.AddWithValue("@customer_name", customer_name);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@birthday", birthday);
                    cmd.Parameters.AddWithValue("@channel_id", channel_id);
                    cmd.Parameters.AddWithValue("@distribute_channel_id", distribute_channel_id);
                    cmd.Parameters.AddWithValue("@market_id", market_id);
                    cmd.Parameters.AddWithValue("@route_id", route_id);
                    cmd.Parameters.AddWithValue("@route_code", "");
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@add_number", add_number);
                    cmd.Parameters.AddWithValue("@province", province);
                    cmd.Parameters.AddWithValue("@district", district);
                    cmd.Parameters.AddWithValue("@ward", ward);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@street_id", street_id);
                    cmd.Parameters.AddWithValue("@ward_id", ward_id);
                    cmd.Parameters.AddWithValue("@district_id", district_id);
                    cmd.Parameters.AddWithValue("@province_id", province_id);
                    cmd.Parameters.AddWithValue("@active", active);
                    cmd.Parameters.AddWithValue("@max_debt_vol", max_debt_vol);
                    cmd.Parameters.AddWithValue("@current_debt_vol", current_debt_vol);
                    cmd.Parameters.AddWithValue("@personal_id", personal_id);
                    cmd.Parameters.AddWithValue("@longitude", longitude);
                    cmd.Parameters.AddWithValue("@latitude", latitude);
                    cmd.Parameters.AddWithValue("@employee_id", employee_id);
                    cmd.Parameters.AddWithValue("@employee_code", employee_code);
                    cmd.Parameters.AddWithValue("@visit_order", visit_order);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    Response.Redirect("customer-list.aspx");
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadData(string id)
        {
            try
            {
                string sQuery = "SELECT * from customer where customer_id={0}";
                sQuery = string.Format(sQuery, id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string customer_id = r["customer_id"] == DBNull.Value ? "" : r["customer_id"].ToString();
                    string customer_code = r["customer_code"] == DBNull.Value ? "" : r["customer_code"].ToString();
                    string store_id = r["store_id"] == DBNull.Value ? "" : r["store_id"].ToString();
                    string customer_name = r["customer_name"] == DBNull.Value ? "" : r["customer_name"].ToString();
                    string mobile = r["mobile"] == DBNull.Value ? "" : r["mobile"].ToString();
                    string phone = r["phone"] == DBNull.Value ? "" : r["phone"].ToString();
                    string email = r["email"] == DBNull.Value ? "" : r["email"].ToString();
                    string birthday = r["birthday"] == DBNull.Value ? "" : r["birthday"].ToString();
                    string channel_id = r["channel_id"] == DBNull.Value ? "" : r["channel_id"].ToString();
                    string distribute_channel_id = r["distribute_channel_id"] == DBNull.Value ? "" : r["distribute_channel_id"].ToString();


                    string market_id = r["market_id"] == DBNull.Value ? "" : r["market_id"].ToString();
                    string route_id = r["route_id"] == DBNull.Value ? "" : r["route_id"].ToString();
                    string address = r["address"] == DBNull.Value ? "" : r["address"].ToString();
                    string add_number = r["add_number"] == DBNull.Value ? "" : r["add_number"].ToString();

                    string province = r["province"] == DBNull.Value ? "" : r["province"].ToString();
                    string district = r["district"] == DBNull.Value ? "" : r["district"].ToString();
                    string ward = r["ward"] == DBNull.Value ? "" : r["ward"].ToString();                    
                    string street = r["street"] == DBNull.Value ? "" : r["street"].ToString();


                    string street_id = r["street_id"] == DBNull.Value ? "" : r["street_id"].ToString();
                    string ward_id = r["ward_id"] == DBNull.Value ? "" : r["ward_id"].ToString();
                    string district_id = r["district_id"] == DBNull.Value ? "" : r["district_id"].ToString();
                    string province_id = r["province_id"] == DBNull.Value ? "" : r["province_id"].ToString();

                    string active = r["active"] == DBNull.Value ? "" : r["active"].ToString();
                    string max_debt_vol = r["max_debt_vol"] == DBNull.Value ? "" : r["max_debt_vol"].ToString();
                    string current_debt_vol = r["current_debt_vol"] == DBNull.Value ? "" : r["current_debt_vol"].ToString();
                    string personal_id = r["personal_id"] == DBNull.Value ? "" : r["personal_id"].ToString();
                    string longitude = r["longitude"] == DBNull.Value ? "" : r["longitude"].ToString();
                    string latitude = r["latitude"] == DBNull.Value ? "" : r["latitude"].ToString();
                    string employee_code = r["employee_code"] == DBNull.Value ? "" : r["employee_code"].ToString();
                    string employee_id = r["employee_id"] == DBNull.Value ? "" : r["employee_id"].ToString();

                    string visit_order = r["visit_order"] == DBNull.Value ? "0" : r["visit_order"].ToString();



                    string created_date = r["created_date"] == DBNull.Value ? "" : r["created_date"].ToString();


                    txtID.Text = customer_id;
                    txtID.Enabled = false;


                    object objPosition = Session["position"];
                    string position = objPosition.ToString();

                    if (position.Equals("ADMIN"))
                    {
                        txtCode.Text = customer_code;
                        txtCode.Enabled = true;
                    }else
                    {
                        txtCode.Text = customer_code;
                        txtCode.Enabled = false;
                    }

                    

                    txtTenKH.Text = customer_name;
                    txtDienThoai.Text = phone;

                    txtNgayTao.Text = created_date;

                    cbxNhaPhanPhoi.SelectedValue = store_id;
                    cbxNhaPhanPhoi_SelectedIndexChanged(null, null);
                    cbxEmployee.SelectedValue = employee_id;
                    cbxNhaPhanPhoi.Enabled = false;


                    cbxProvince.SelectedValue = province;
                    cbxProvince_SelectedIndexChanged(null, null);
                    cbxDistrict.SelectedValue = district;
                    cbxDistrict_SelectedIndexChanged(null, null);
                    cbxWard.SelectedValue = ward;
                    cbxWard_SelectedIndexChanged(null, null);
                    cbxStreet.SelectedValue = street;



                    cbxMarket.SelectedValue = market_id;
                    cbxChannel.SelectedValue = channel_id;
                    cbxDistChannel.SelectedValue = distribute_channel_id;

                    cbxWeekDays.SelectedValue = route_id;
                    txtSoNha.Text = add_number;
                    txtDiaChiDayDu.Text = address;

                    chckActive.Checked = bool.Parse(active);
                    txtSttViengTham.Text = visit_order;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxNhaPhanPhoi_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    GenCode();
                }

                string sQuery = @"select employee_id, employee_code + '-' + employee_name as employee_name from employee where store_id={0} and position = 'SR'";
                sQuery = string.Format(sQuery, cbxNhaPhanPhoi.SelectedValue);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxEmployee.DataSource = tb;
                cbxEmployee.DataBind();
                //cbxEmployee.Items.Insert(0, new RadComboBoxItem("Chọn quản nhân viên bán hàng...", string.Empty));
                //cbxEmployee.Items.Insert(0, new RadComboBoxItem(null, null));

                //cbxRoute.Items.Insert(0, new RadComboBoxItem(null, null));

                BindCustomerChannel();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("customer-list.aspx");
        }

        protected void cbxProvince_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string sQuery = @"SELECT DISTINCT district_name FROM geo_data where province_name=N'"+cbxProvince.SelectedValue+"' ORDER BY district_name";
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxDistrict.DataSource = tb;
                cbxDistrict.DataBind();
                cbxDistrict.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
                
                
            }
        }

        protected void cbxDistrict_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string sQuery = @"SELECT DISTINCT ward_name FROM geo_data where province_name=N'" + cbxProvince.SelectedValue + "' and district_name=N'" + cbxDistrict.SelectedValue + "' ORDER BY ward_name";
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxWard.DataSource = tb;
                cbxWard.DataBind();
                cbxWard.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {


            }
        }

        protected void cbxWard_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {


            }
        }
    }
}