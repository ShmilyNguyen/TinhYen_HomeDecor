using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class promo_discount : System.Web.UI.Page
    {

        public void BindDetail()
        {
            try
            {
                string id = Request.QueryString["id"];
                string sQuery = "select promo_id,promo_code,promo_name from promotion where promo_id={0}";
                sQuery = string.Format(sQuery, id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                DataRow r = tb.Rows[0];
                txtID.Text = r["promo_id"].ToString();
                txtCode.Text = r["promo_code"].ToString();
                txtName.Text = r["promo_name"].ToString();
            }
            catch (Exception ex)
            {


            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDetail();
            }
        }

        #region Tab ChietKhau

        public DataTable myData_ChietKhau
        {
            get
            {
                DataTable data = GetData();

                return data;
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



       

        public DataTable GetData()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  b.row_id ,
                                        a.item_id ,
                                        a.item_code ,
                                        a.item_name ,
                                        b.promo_type,
                                        ISNULL(b.vol, 0) AS vol ,
                                        ISNULL(b.discount, 0) AS discount
                                FROM    dbo.item AS a
                                        LEFT JOIN ( SELECT  *
                                                    FROM    dbo.promotion_item_discount
                                                    WHERE   promo_id = {0}
                                                  ) AS b ON a.item_id = b.item_id
                                ";

                sQuery = string.Format(sQuery, txtID.Text.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.myData_ChietKhau;
        }


        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();

                string sQuery = "delete from promotion_item_discount where row_id={0}";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                

            }
            catch (Exception ex)
            {
                //throw;
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


                    TextBox txtTenHangHoa = (TextBox)MyUserControl.FindControl("txtTenHangHoa");
                    TextBox txtMaHangHoa = (TextBox)MyUserControl.FindControl("txtMaHangHoa");

                    TextBox txtVol = (TextBox)MyUserControl.FindControl("txtVol");
                    TextBox txtDiscount = (TextBox)MyUserControl.FindControl("txtDiscount");

                    RadComboBox cbxPromoType = (RadComboBox)MyUserControl.FindControl("cbxPromoType");

                    HiddenField hdf_row_id = (HiddenField)MyUserControl.FindControl("hdf_row_id");
                    HiddenField hdf_promo_id = (HiddenField)MyUserControl.FindControl("hdf_promo_id");
                    HiddenField hdf_item_id = (HiddenField)MyUserControl.FindControl("hdf_item_id");


                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string row_id = parentItem["row_id"].Text;
                        string promo_id = txtID.Text.Trim();
                        string item_id = parentItem["item_id"].Text;

                        string item_code = parentItem["item_code"].Text;
                        string item_name = parentItem["item_name"].Text;
                        string vol = parentItem["vol"].Text;
                        string discount = parentItem["discount"].Text;
                        string promo_type = parentItem["promo_type"].Text;



                        hdf_row_id.Value = row_id == "&nbsp;" ? "" : row_id;
                        hdf_item_id.Value = item_id == "&nbsp;" ? "" : item_id;
                        hdf_promo_id.Value = promo_id == "&nbsp;" ? "" : promo_id;


                        txtMaHangHoa.Text = item_code == "&nbsp;" ? "" : item_code;

                        txtTenHangHoa.Text = item_name == "&nbsp;" ? "" : item_name;
                        txtVol.Text = vol == "&nbsp;" ? "0" : vol;
                        txtDiscount.Text = discount == "&nbsp;" ? "0" : discount;

                        cbxPromoType.SelectedValue = promo_type;

                    }

                }
            }
            catch (Exception ex)
            {
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



                        string row_id = (userControl.FindControl("hdf_row_id") as System.Web.UI.WebControls.HiddenField).Value;
                        string item_id = (userControl.FindControl("hdf_item_id") as System.Web.UI.WebControls.HiddenField).Value;
                        string promo_id = (userControl.FindControl("hdf_promo_id") as System.Web.UI.WebControls.HiddenField).Value;


                        string vol = (userControl.FindControl("txtVol") as System.Web.UI.WebControls.TextBox).Text;
                        string discount = (userControl.FindControl("txtDiscount") as System.Web.UI.WebControls.TextBox).Text;

                        string promo_type = (userControl.FindControl("cbxPromoType") as RadComboBox).SelectedValue;

                        string storeProc = "[usp_InsertUpdatepromotion_item_discount]";
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@row_id", row_id);
                                cmd.Parameters.AddWithValue("@promo_id", promo_id);
                                cmd.Parameters.AddWithValue("@item_id", item_id);
                                cmd.Parameters.AddWithValue("@vol", vol);
                                cmd.Parameters.AddWithValue("@discount", discount);
                                cmd.Parameters.AddWithValue("@promo_type", promo_type);



                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();

                                //Refresh_Data();
                                
                            }
                        }
                        catch (Exception ex)
                        {
                        }

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

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            
        }
        #endregion Tab ChietKhau

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("promo-edit.aspx");
            }
            else
            {
                Response.Redirect("promo-edit.aspx?id=" + id);
            }
            
        }

      
    }
}