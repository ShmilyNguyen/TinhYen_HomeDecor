using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace WKS.DMS.WEB.Forms
{
    public partial class saleout_edit : System.Web.UI.Page
    {
        public DataTable myData
        {
            get
            {
                DataTable data = Session["Data_SaleOut_Detail"] as DataTable;

                object is_reload = Session["is_reload"];

                if (data == null)
                {
                    data = GetData();
                    Session["Data_SaleOut_Detail"] = data;
                }

                return data;
            }
        }

        public DataTable GetData()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT * from v_SaleOut where SaleOut_id='" + hdf_SaleOut_ID.Value + "'";
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
                Session["Data_SaleOut_Detail"] = data;
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
                        string discount = (userControl.FindControl("txtDiscount") as System.Web.UI.WebControls.TextBox).Text.Trim();



                        string storeProc = "[usp_InsertUpdateSaleOut_detail]";
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(row_id))
                            {
                                cmd.Parameters.AddWithValue("@row_id", int.Parse(row_id));
                                cmd.Parameters.AddWithValue("@SaleOut_id", int.Parse(hdf_SaleOut_ID.Value.Trim()));
                                cmd.Parameters.AddWithValue("@item_id", int.Parse(item_id));
                                cmd.Parameters.AddWithValue("@qty", int.Parse(qty));                              
                                cmd.Parameters.AddWithValue("@saleout_type", saleout_type);
                                cmd.Parameters.AddWithValue("@discount", float.Parse(discount));
                                cmd.Parameters.AddWithValue("@promo_id", DBNull.Value);




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



                

               
            }
            catch (Exception ex)
            {


            }

        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();


                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    BindControls();
                    Gen_SaleOut_Code();                    
                    Update_Header();
                }
                else
                {
                    ReloadData(id);
                }



            }

            cbxNhaPhanPhoi.Focus();
        }



        public void Gen_SaleOut_Code()
        {
            //DateTime dt = DateTime.Now;
            //string Code = dt.Year.ToString();

            //if (dt.Month < 10)
            //{
            //    Code = Code + "0" + dt.Month.ToString();
            //}
            //else
            //{
            //    Code = Code + dt.Month.ToString();
            //}

            //if (dt.Day < 10)
            //{
            //    Code = Code + "0" + dt.Day.ToString();
            //}
            //else
            //{
            //    Code = Code + dt.Day.ToString();
            //}

            //Code = Code + "." + dt.Minute.ToString() + dt.Second.ToString() + dt.Millisecond.ToString();
            //txtSaleOut_Code.Text = Code;

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
                        cmd.Parameters.AddWithValue("@Prefix", "XK");

                        conn.Open();
                        string result = Convert.ToString(cmd.ExecuteScalar());
                        txtSaleOut_Code.Text = result;
                        conn.Close();
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void BindList()
        {
            cbxLoaiXuat.SelectedIndex = 0;

            string sQuery = "";

            sQuery = @"SELECT  a.store_id ,
                                                a.store_name + ' (' + b.region_name + ')' AS store_name
                                        FROM    dbo.store AS a
                                                LEFT JOIN dbo.region AS b ON a.region_id = b.region_id
                                        where store_id in (SELECT    store_id
                                                                        FROM      dbo.fn_GetStore_By_UserID(" + Session["userid"] + "))";

            DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
            cbxNhaPhanPhoi.DataSource = tb;
            cbxNhaPhanPhoi.DataBind();
            //cbxNhaPhanPhoi.Items.Insert(0, new RadComboBoxItem(null, null));


            sQuery = @"SELECT  *
                        FROM    dbo.employee
                        WHERE   store_id IN (SELECT    store_id
                                                FROM      dbo.fn_GetStore_By_UserID(" + Session["userid"] + "))";
            tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
            cbxNhanVien.DataSource = tb;
            cbxNhanVien.DataBind();
            //cbxNhanVien.Items.Insert(0, new RadComboBoxItem(null, null));


            sQuery = "select * from item";
            tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
            cbxHangHoa.DataSource = tb;
            cbxHangHoa.DataBind();
            cbxHangHoa.SelectedIndex = -1;
            //cbxHangHoa.Items.Insert(0, new RadComboBoxItem(null, null));

            sQuery = "select * from promotion";
            tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
            cbxCTKM.DataSource = tb;
            cbxCTKM.DataBind();
            cbxCTKM.SelectedIndex = -1;
            cbxCTKM.Items.Insert(0, new RadComboBoxItem(null, null));

        }

        public void BindControls()
        {
            try
            {
                txtLastModified.Text = DateTime.Now.ToString();
                rdpNgayGiaoDich.DbSelectedDate = DateTime.Now;
                txtNguoiLap.Text = Session["username"].ToString();

                string sQuery = @"SELECT ISNULL( MAX( SaleOut_id ),0 ) + 1   FROM dbo.SaleOut_header;";
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();


                hdf_SaleOut_ID.Value = result;

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
                    string employee_id = (r["employee_id"] ?? "").ToString();
                    string note = (r["note"] ?? "").ToString();
                    string trans_date_gmt = (r["trans_date_gmt"] ?? "").ToString();
                    string trans_date_numb = (r["trans_date_numb"] ?? "").ToString();
                    string created_by = (r["created_by"] ?? "").ToString();
                    string last_modified = (r["last_modified"] ?? "").ToString();

                    hdf_SaleOut_ID.Value = SaleOut_id;
                    txtSaleOut_Code.Text = SaleOut_code;
                    cbxNhaPhanPhoi.SelectedValue = store_id;
                    rdpNgayGiaoDich.DbSelectedDate = trans_date_gmt;
                    cbxNhanVien.SelectedValue = employee_id;
                    txtNguoiLap.Text = created_by;
                    hdf_User_ID.Value = created_by;
                    txtLastModified.Text = last_modified;
                    txtGhiChu.Text = note;

                    ReloadGrid();

                }
            }
            catch (Exception ex)
            {

            }

        }

        public void Update_Header()
        {
            string storeProc = "[usp_InsertUpdateSaleOut_header]";
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
                        cmd.Parameters.AddWithValue("@store_id", int.Parse(cbxNhaPhanPhoi.SelectedValue));
                        cmd.Parameters.AddWithValue("@employee_id", cbxNhanVien.SelectedValue);
                        cmd.Parameters.AddWithValue("@note", txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@created_by", hdf_User_ID.Value);
                        cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ontop_discount", float.Parse(txtOntopDiscount.Text.Trim()));
                        cmd.Parameters.AddWithValue("@ontop_discount_code", DBNull.Value);


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
                        cmd.Parameters.AddWithValue("@discount", float.Parse(txtCK.Text.Trim()));

                        if (string.IsNullOrEmpty(cbxCTKM.SelectedValue))
                        {
                            cmd.Parameters.AddWithValue("@promo_id", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@promo_id", int.Parse(cbxCTKM.SelectedValue));
                        }
                       
                        



                        conn.Open();
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            ReloadGrid();

            cbxLoaiXuat.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Update_Header();
            ReloadGrid();
            
        }

        protected void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "delete from saleout_detail where saleout_id=" + hdf_SaleOut_ID.Value;
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = "delete from saleout_header where saleout_id=" + hdf_SaleOut_ID.Value;
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


                Response.Redirect("saleout-list.aspx");
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("saleout-list.aspx");
        }
    
        public void UpdateThanhTien()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"[usp_saleout_gettotalvalues]";
                SqlParameter[] arrSQLParam = new SqlParameter[1];
                arrSQLParam[0] = new SqlParameter("@saleout_id", hdf_SaleOut_ID.Value);
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery,arrSQLParam).Tables[0];
                foreach (DataRow r in data.Rows)
                {
                    CultureInfo us = new CultureInfo("en-US");
                    txtThanhTien.Text = r["ThanhTien"].ToString();
                    txtGTCK.Text = r["GTChietKhau"].ToString();
                    txtOntopDiscount.Text = r["OntopDiscount"].ToString();
                    txtTotalOntopDiscount.Text = r["TotalOntopDiscount"].ToString();
                    txtGTThanhToan.Text = r["GTThanhToan"].ToString(); 
                }
            }

            catch (Exception ex)
            {
                
                
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("saleout-edit.aspx");
        }
    
    }
}