using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms.Payment
{
    public partial class debit_credit_edit : System.Web.UI.Page
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
                    Update_Header();
                    

                }
                else
                {
                    hdf_ID.Value = id;
                    ReloadData(id);
                    Disable_Controls(true);
                }
            }
        }


        public void Disable_Controls(bool is_readonly)
        {
            try
            {

                if (is_readonly)
                {



                    //RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                    //RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;

                }


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


                if (cbxPaymentType.SelectedValue =="0")
                {
                    clsCodeMaster.GenCode("ardoc-dm", UserID, out ID, out Code);
                }

                if (cbxPaymentType.SelectedValue == "1")
                {
                    clsCodeMaster.GenCode("ardoc-cm", UserID, out ID, out Code);
                }
                
                hdf_ID.Value = ID;
                txtCode.Text = Code;

                hdf_UserID.Value = Session["userid"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public void Update_Header()
        {
            string storeProc = "[usp_InsertUpdatedebit_credit]";

            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@posted_id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                        cmd.Parameters.AddWithValue("@posted_code", txtCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@posted_date", rdpNgay.DbSelectedDate);
                        cmd.Parameters.AddWithValue("@debit_credit", cbxPaymentType.SelectedValue);
                        cmd.Parameters.AddWithValue("@created_by", Session["userid"]);
                        cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@note", txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@posted_amount", txtGiaTri.Text.Trim());
                        cmd.Parameters.AddWithValue("@is_completed", "0");

                        cmd.Parameters.AddWithValue("@posted_type", "");
                        cmd.Parameters.AddWithValue("@current_balance", txtGiaTri.Text.Trim());
                        


                        conn.Open();
                        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                        conn.Close();


                    }
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
                cbxStore.DataSource = tb;
                cbxStore.DataBind();

                cbxStore_SelectedIndexChanged(null, null);
                               
                rdpNgay.DbSelectedDate = DateTime.Now;
                txtNguoiLap.Text = Session["username"].ToString();
            }
            catch (Exception ex)
            {
            }
        }  
       


        public void ReloadData(string id)
        {
            try
            {
                string sQuery = @"SELECT  a.doc_id AS posted_id ,
                                    a.doc_code AS posted_code ,
                                    a.note ,
                                    a.doc_date AS posted_date ,
                                    CASE WHEN doc_type = 'DM' THEN 0
                                         WHEN doc_type = 'CM' THEN 1
                                    END AS debit_credit ,
                                    a.release AS is_completed ,
                                    a.store_id ,
                                    a.customer_id ,
                                    a.original_doc_amount AS posted_amount ,
                                    b.employee_name
                            FROM    dbo.ARDoc AS a
                                    LEFT JOIN dbo.employee AS b ON a.created_by = b.employee_id
                            WHERE   doc_type IN ( 'DM', 'CM' )
                                    AND a.doc_id = {0}";
                sQuery = string.Format(sQuery, hdf_ID.Value);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string _id = (r["posted_id"] ?? "").ToString();
                    string _code = (r["posted_code"] ?? "").ToString();

                    string _note = (r["note"] ?? "").ToString();
                    string _posted_date = (r["posted_date"] ?? "").ToString();
                    string _nguoilap = (r["employee_name"] ?? "").ToString();
                    string _debit_credit = (r["debit_credit"] ?? "").ToString();

                    string _is_completed = (r["is_completed"] ?? "").ToString();


                    string _store_id = (r["store_id"] ?? "").ToString();


                    string _customer_id = (r["customer_id"] ?? "").ToString();


                    string _posted_amount = (r["posted_amount"] ?? "0").ToString();



                    hdf_ID.Value = _id;
                    txtCode.Text = _code;
                    rdpNgay.DbSelectedDate = _posted_date;
                    txtNguoiLap.Text = _nguoilap;
                    txtGhiChu.Text = _note;

                    cbxStore.SelectedValue = _store_id;
                    cbxStore_SelectedIndexChanged(null, null);

                    cbxKhachHang.SelectedValue = _customer_id;
                    cbxKhachHang_SelectedIndexChanged(null, null);

                    if (_debit_credit.ToLower() == "true")
                    {
                        _debit_credit = "1";
                    }
                    else
                    {
                        _debit_credit = "0";
                    }
                    cbxPaymentType.SelectedValue = _debit_credit;

                    txtGiaTri.Text = _posted_amount;


                    if (_is_completed.ToLower() =="true")
                    {
                        cbxPaymentType.Enabled = false;
                        txtCode.Enabled = false;
                        txtGhiChu.Enabled = false;
                        txtGiaTri.Enabled = false;
                        cbxKhachHang.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
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
                cbxKhachHang_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxKhachHang_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                
                
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("debit-credit-list.aspx");
        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Header();

                UpdateARDoc();

                btnExit_Click(null, null);
            }
            catch (Exception ex)
            {
            }
        }


        public void UpdateARDoc()
        {
            try
            {
                //Insert
                //GenCode
                string _id = hdf_ID.Value;
                string _code = txtCode.Text;



                string storeProc = "[usp_InsertUpdateARDoc]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    string doc_type = "";

                    if (cbxPaymentType.SelectedValue == "0")
                    {
                        doc_type = "DM";
                    }

                    if (cbxPaymentType.SelectedValue == "1")
                    {
                        doc_type = "CM";
                    }



                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doc_id", _id);
                    cmd.Parameters.AddWithValue("@doc_code", _code);
                    cmd.Parameters.AddWithValue("@doc_date", rdpNgay.DbSelectedDate);
                    cmd.Parameters.AddWithValue("@doc_type", doc_type);
                    cmd.Parameters.AddWithValue("doc_balance ", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@current_doc_balance", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@original_doc_amount", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@release ", 0);
                    cmd.Parameters.AddWithValue("@reference_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@note", txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@created_by", Session["userid"]);
                    cmd.Parameters.AddWithValue("@last_modified", DateTime.Now);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


            }
            catch (Exception ex)
            {


            }
        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "delete from debit_credit where posted_id={0}";
                sQuery = string.Format(sQuery, hdf_ID.Value);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);



                btnExit_Click(null, null);
            }
            catch (Exception ex)
            {
            }
        }

        protected void cbxPaymentType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GenCode();
        }

        

        
    }
}