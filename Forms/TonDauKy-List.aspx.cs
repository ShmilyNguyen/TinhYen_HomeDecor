using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;


namespace WKS.DMS.WEB.Forms
{
    public partial class TonDauKy_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();


                BindData();
            }
        }

        public void BindData()
        {
            try
            {
                  string sQuery = "";

                  sQuery = @"SELECT  a.store_id ,
                                                a.store_name + ' (' + ISNULL(a.location,'') + ')' AS store_name
                                        FROM    dbo.store AS a
                                        WHERE a.store_id  IN (
                                                                SELECT  store_id
                                                                FROM    dbo.fn_GetStore_By_UserID({0}) )
                                                

                                        ";

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

      
        protected void btnLoadData_Click(object sender, EventArgs e)
        {

            RadGrid1_NeedDataSource(null,null);
            RadGrid1.DataBind();
    
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

                string store_id = cbxNhaPhanPhoi.SelectedValue;
                string month = ddlThang.SelectedValue;
                string year = ddlNam.SelectedValue;
                string user_id = Session["userid"].ToString();

                DataTable data = new DataTable();
                string sQuery = @"[sp_Inventory_Closing_List]";
                SqlParameter[] arrSQLParam = new SqlParameter[4];
                arrSQLParam[0] = new SqlParameter("@store_id",store_id );
                arrSQLParam[1] = new SqlParameter("@month",month );
                arrSQLParam[2] = new SqlParameter("@year",year );
                arrSQLParam[3] = new SqlParameter("@user_id", user_id);

                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, arrSQLParam).Tables[0];

                return data;
            }
            catch (Exception ex)
            {

               
            }

            return null;
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

        protected void RadGrid2_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
        }


        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {



            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                    RadNumericTextBox txtQty_SaleOut = (RadNumericTextBox)MyUserControl.FindControl("txtQty_SaleOut");
                    RadNumericTextBox txtQty_Promo = (RadNumericTextBox)MyUserControl.FindControl("txtQty_Promo");
                    RadNumericTextBox txtTotal_SaleOut = (RadNumericTextBox)MyUserControl.FindControl("txtTotal_SaleOut");
                    RadNumericTextBox txtTotal_Promo = (RadNumericTextBox)MyUserControl.FindControl("txtTotal_Promo");


                    TextBox txtNote = (TextBox)MyUserControl.FindControl("txtNote");
                    TextBox txtAdjust_Type = (TextBox)MyUserControl.FindControl("txtAdjust_Type");


                    HiddenField hdf_row_id = (HiddenField)MyUserControl.FindControl("hdf_row_id");


                    
                    RadComboBox cbxHangHoa = (RadComboBox)MyUserControl.FindControl("cbxHangHoa");

                    string sQuery = @"select * from item";
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    cbxHangHoa.DataSource = tb;
                    cbxHangHoa.DataBind();
                    cbxHangHoa.Items.Insert(0, new RadComboBoxItem(null, null));


                    //Edit
                    if (parentItem != null)
                    {


                        string store_id = parentItem["store_id"].Text;
                        string item_id = parentItem["item_id"].Text;

                        string qty_saleout = parentItem["qty_saleout"].Text.Replace(",", "");
                        string qty_promo = parentItem["qty_promo"].Text.Replace(",", "");

                        string total_saleout = parentItem["total_saleout"].Text.Replace(",","");
                        string total_promo = parentItem["total_promo"].Text.Replace(",", "");


                        string row_id = parentItem["row_id"].Text;
                        hdf_row_id.Value = row_id;


                        txtQty_SaleOut.Text = qty_saleout == "&nbsp;" ? "0" : qty_saleout;
                        txtQty_Promo.Text = qty_promo == "&nbsp;" ? "0" : qty_promo;
                        txtTotal_SaleOut.Text = total_saleout == "&nbsp;" ? "0" : total_saleout;
                        txtTotal_Promo.Text = total_promo == "&nbsp;" ? "0" : total_promo;

                        cbxHangHoa.SelectedValue = item_id;
                        cbxHangHoa.Enabled = false;

                        string adjust_type = parentItem["adjust_type"].Text;
                        adjust_type = adjust_type == "&nbsp;" ? "" : adjust_type;
                        txtAdjust_Type.Text = adjust_type;


                        string note = parentItem["note"].Text;
                        note = note == "&nbsp;" ? "" : note;


                    }else
                    {
                        //Add New
                        txtAdjust_Type.Text = "MANUAL";

                    }




                }
            }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHeaderItem headerItem = e.Item as GridHeaderItem;
            if (headerItem != null)
            {
                headerItem["EditColumn"].Text = string.Empty;

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


                        string row_id =(((userControl.FindControl("hdf_row_id") as HiddenField).Value));

                        int qty_saleout = int.Parse((userControl.FindControl("txtQty_SaleOut") as RadNumericTextBox).Text.Replace(".",""));
                        int qty_promo = int.Parse((userControl.FindControl("txtQty_Promo") as RadNumericTextBox).Text.Replace(".", ""));
                        double total_saleout = double.Parse((userControl.FindControl("txtTotal_SaleOut") as RadNumericTextBox).Text.Replace(".", ""));
                        double total_promo = double.Parse((userControl.FindControl("txtTotal_Promo") as RadNumericTextBox).Text.Replace(".", ""));
                        int store_id = int.Parse(cbxNhaPhanPhoi.SelectedValue);
                        int item_id = int.Parse((userControl.FindControl("cbxHangHoa") as RadComboBox).SelectedValue);

                        string note = ((userControl.FindControl("txtNote") as TextBox).Text.Trim());
                        string adjust_type = ((userControl.FindControl("txtAdjust_Type") as TextBox).Text.Trim());


                        string storeProc = "[usp_InsertUpdateinventory_closing]";
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);

                            cmd.CommandType = CommandType.StoredProcedure;

                            if (!string.IsNullOrEmpty(row_id) && row_id != "&nbsp;")
                            {
                                cmd.Parameters.AddWithValue("@row_id", int.Parse(row_id));
                            }else
                            {
                                cmd.Parameters.AddWithValue("@row_id", DBNull.Value);
                            }
                           

                            cmd.Parameters.AddWithValue("@store_id", store_id);
                            cmd.Parameters.AddWithValue("@item_id", item_id);

                            cmd.Parameters.AddWithValue("@qty_saleout", qty_saleout);
                            cmd.Parameters.AddWithValue("@qty_promo", qty_promo);
                            cmd.Parameters.AddWithValue("@total_saleout", total_saleout);
                            cmd.Parameters.AddWithValue("@total_promo", total_promo);

                            cmd.Parameters.AddWithValue("@day", 0);
                            cmd.Parameters.AddWithValue("@month", int.Parse(ddlThang.SelectedValue));
                            cmd.Parameters.AddWithValue("@year", int.Parse(ddlNam.SelectedValue));

                            cmd.Parameters.AddWithValue("@adjusted_date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@modified_by",int.Parse(Session["userid"].ToString()));
                            cmd.Parameters.AddWithValue("@note", note);
                            cmd.Parameters.AddWithValue("@adjust_type", adjust_type);



                            conn.Open();
                            string result = Convert.ToString(cmd.ExecuteScalar());

                            conn.Close();


                        }

                    }
                    catch (Exception ex)
                    {


                        //throw;
                    }


                }


                if (e.CommandName == "Delete")
                {
                    
                }
            }
            catch (Exception ex)
            {


            }

        }
        
    }
}