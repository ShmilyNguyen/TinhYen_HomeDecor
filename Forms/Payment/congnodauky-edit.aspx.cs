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
    public partial class congnodauky_edit : System.Web.UI.Page
    {
        string _store_id = "";
        string _customer_id = "";
        string _posted_amount = "";


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
            

                hdf_UserID.Value = Session["userid"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public void Update_Header()
        {
            string storeProc = "[usp_InsertUpdate_congnodauky]";

         double result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string id = hdf_ID.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(id));
                        cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
                        cmd.Parameters.AddWithValue("@init_balance",int.Parse(txtGiaTri.Text.ToString()));
                        cmd.Parameters.AddWithValue("@customer_id",cbxKhachHang.SelectedValue);




                        conn.Open();
                        result = double.Parse(cmd.ExecuteNonQuery().ToString());
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
                               
           
            }
            catch (Exception ex)
            {
            }
        }  
       


        public void ReloadData(string id)
        {
            try
            {
                string sQuery = @"select b.id, a.customer_id, a.customer_name , c.store_name, b.init_balance, c.store_id
                    from  customer as a
                        left join customer_init_balance as b
                            on a.customer_id = b.customer_id
                       join store as c
                            on c.store_id = b.store_id
                    WHERE a.customer_id > 0
                            AND b.id ={0}    AND a.store_id IN ( SELECT  store_id
                            FROM    dbo.fn_GetStore_By_UserID({1}))";
                sQuery = string.Format(sQuery,id , Session["userid"]);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    string _id = (r["id"] ?? "").ToString();

                     

                     _store_id = (r["store_id"] ?? "").ToString();

                     _customer_id = (r["customer_id"] ?? "").ToString();


                  _posted_amount = (r["init_balance"] ?? "0").ToString();



                    hdf_ID.Value = _id;
                    cbxStore.SelectedValue = _store_id;
                    cbxStore_SelectedIndexChanged(null, null);

                    cbxKhachHang.SelectedValue = _customer_id;
               //     cbxKhachHang_SelectedIndexChanged(null, null);
                    txtGiaTri.Text = _posted_amount;

 
                    
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
            Response.Redirect("congnodauky-list.aspx");
        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Header();

              //  UpdateARDoc();

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
                 
                    cmd.Parameters.AddWithValue("@doc_type", doc_type);
                    cmd.Parameters.AddWithValue("doc_balance ", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@current_doc_balance", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@original_doc_amount", txtGiaTri.Text);
                    cmd.Parameters.AddWithValue("@release ", 0);
                    cmd.Parameters.AddWithValue("@reference_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@customer_id", cbxKhachHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@store_id", cbxStore.SelectedValue);
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
                string sQuery = "delete from customer_init_balance where  id={0}";
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