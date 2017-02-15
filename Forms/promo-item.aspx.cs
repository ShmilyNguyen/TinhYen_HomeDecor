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
    public partial class promo_item : System.Web.UI.Page
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


        #region Tab Tang Hang

        public DataTable myData_TangHang
        {
            get
            {
                DataTable data = GetData2();

                return data;
            }
        }

        public DataTable GetData2()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  b.row_id ,
        a.item_id AS item_id_src ,
        a.item_code AS item_code_src ,
        a.item_name AS item_name_src ,
        c.item_id AS item_id_des ,
        c.item_code AS item_code_des ,
        c.item_name AS item_name_des ,
        ISNULL(b.vol_src, 0) AS vol_src ,
        ISNULL(b.vol_des, 0) AS vol_des ,
        ISNULL(b.vol_src2, 0) AS vol_src2 ,
        ISNULL(b.vol_des2, 0) AS vol_des2 ,
        ISNULL(b.vol_src3, 0) AS vol_src3 ,
        ISNULL(b.vol_des3, 0) AS vol_des3 ,
        ISNULL(b.vol_src4, 0) AS vol_src4 ,
        ISNULL(b.vol_des4, 0) AS vol_des4 ,
        c2.item_id AS item_id2 ,
        c2.item_name AS item_name2 ,
        c3.item_id AS item_id3 ,
        c3.item_name AS item_name3 ,
        c4.item_id AS item_id4 ,
        c4.item_name AS item_name4
FROM    dbo.item AS a
        LEFT JOIN ( SELECT  *
                    FROM    dbo.promotion_item_1
                    WHERE   promo_id = {0}
                  ) AS b ON a.item_id = b.item_id_src
        LEFT JOIN dbo.item AS c ON b.item_id_des = c.item_id
        LEFT JOIN dbo.item AS c2 ON b.item_id_des2 = c2.item_id
        LEFT JOIN dbo.item AS c3 ON b.item_id_des3 = c3.item_id
        LEFT JOIN dbo.item AS c4 ON b.item_id_des4 = c4.item_id
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

        protected void RadGrid2_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid2.DataSource = this.myData_TangHang;
        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                    TextBox txtTenHangHoa = (TextBox)MyUserControl.FindControl("txtTenHangHoa");
                    TextBox txtMaHangHoa = (TextBox)MyUserControl.FindControl("txtMaHangHoa");

                    TextBox txtVol_Src = (TextBox)MyUserControl.FindControl("txtVol_Src");
                    TextBox txtVol_Des = (TextBox)MyUserControl.FindControl("txtVol_Des");


                    TextBox txtVol_Src2 = (TextBox)MyUserControl.FindControl("txtVol_Src2");
                    TextBox txtVol_Des2 = (TextBox)MyUserControl.FindControl("txtVol_Des2");
                    
                    TextBox txtVol_Src3 = (TextBox)MyUserControl.FindControl("txtVol_Src3");
                    TextBox txtVol_Des3 = (TextBox)MyUserControl.FindControl("txtVol_Des3");

                    TextBox txtVol_Src4 = (TextBox)MyUserControl.FindControl("txtVol_Src4");
                    TextBox txtVol_Des4 = (TextBox)MyUserControl.FindControl("txtVol_Des4");



                    HiddenField hdf_row_id = (HiddenField)MyUserControl.FindControl("hdf_row_id");
                    HiddenField hdf_promo_id = (HiddenField)MyUserControl.FindControl("hdf_promo_id");
                    HiddenField hdf_item_id_src = (HiddenField)MyUserControl.FindControl("hdf_item_id_src");
                    HiddenField hdf_item_id_des = (HiddenField)MyUserControl.FindControl("hdf_item_id_des");

                    RadComboBox cbxHangHoa_Des = (RadComboBox)MyUserControl.FindControl("cbxHangHoa_Des");
                    RadComboBox cbxHangHoa_Des2 = (RadComboBox)MyUserControl.FindControl("cbxHangHoa_Des2");
                    RadComboBox cbxHangHoa_Des3 = (RadComboBox)MyUserControl.FindControl("cbxHangHoa_Des3");
                    RadComboBox cbxHangHoa_Des4 = (RadComboBox)MyUserControl.FindControl("cbxHangHoa_Des4");

                    string sQuery = "select * from item";
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    cbxHangHoa_Des.DataSource = tb;
                    cbxHangHoa_Des.DataBind();


                    cbxHangHoa_Des2.DataSource = tb;
                    cbxHangHoa_Des2.DataBind();

                    cbxHangHoa_Des3.DataSource = tb;
                    cbxHangHoa_Des3.DataBind();


                    cbxHangHoa_Des4.DataSource = tb;
                    cbxHangHoa_Des4.DataBind();


                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string row_id = parentItem["row_id"].Text;
                        string promo_id = txtID.Text.Trim();
                        string item_id_src = parentItem["item_id_src"].Text;
                        string item_id_des = parentItem["item_id_des"].Text;

                        string item_id_des2 = parentItem["item_id_des2"].Text;
                        string item_id_des3 = parentItem["item_id_des3"].Text;
                        string item_id_des4 = parentItem["item_id_des4"].Text;


                        string item_code_src = parentItem["item_code_src"].Text;
                        string item_name_src = parentItem["item_name_src"].Text;

                        string vol_src = parentItem["vol_src"].Text;
                        string vol_des = parentItem["vol_des"].Text;

                        string vol_src2 = parentItem["vol_src2"].Text;
                        string vol_des2 = parentItem["vol_des2"].Text;

                        string vol_src3 = parentItem["vol_src3"].Text;
                        string vol_des3 = parentItem["vol_des3"].Text;

                        string vol_src4 = parentItem["vol_src4"].Text;
                        string vol_des4 = parentItem["vol_des4"].Text;



                        hdf_row_id.Value = row_id == "&nbsp;" ? "" : row_id;
                        hdf_item_id_src.Value = item_id_src == "&nbsp;" ? "" : item_id_src;
                        hdf_item_id_des.Value = item_id_des == "&nbsp;" ? "" : item_id_des;
                        hdf_promo_id.Value = promo_id == "&nbsp;" ? "" : promo_id;


                        txtMaHangHoa.Text = item_code_src == "&nbsp;" ? "" : item_code_src;

                        txtTenHangHoa.Text = item_name_src == "&nbsp;" ? "" : item_name_src;

                        txtVol_Src.Text = vol_src == "&nbsp;" ? "0" : vol_src;
                        txtVol_Des.Text = vol_des == "&nbsp;" ? "0" : vol_des;

                        txtVol_Src2.Text = vol_src2 == "&nbsp;" ? "0" : vol_src2;
                        txtVol_Des2.Text = vol_des2 == "&nbsp;" ? "0" : vol_des2;

                        txtVol_Src3.Text = vol_src3 == "&nbsp;" ? "0" : vol_src3;
                        txtVol_Des3.Text = vol_des3 == "&nbsp;" ? "0" : vol_des3;

                        txtVol_Src4.Text = vol_src4 == "&nbsp;" ? "0" : vol_src4;
                        txtVol_Des4.Text = vol_des4 == "&nbsp;" ? "0" : vol_des4;



                        cbxHangHoa_Des.SelectedValue = item_id_des;

                        cbxHangHoa_Des2.SelectedValue = item_id_des2 == "&nbsp;" ? "" : item_id_des2;
                        cbxHangHoa_Des3.SelectedValue = item_id_des3 == "&nbsp;" ? "" : item_id_des3;
                        cbxHangHoa_Des4.SelectedValue = item_id_des4 == "&nbsp;" ? "" : item_id_des4;


                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
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
                        string item_id_src = (userControl.FindControl("hdf_item_id_src") as System.Web.UI.WebControls.HiddenField).Value;
                        string promo_id = (userControl.FindControl("hdf_promo_id") as System.Web.UI.WebControls.HiddenField).Value;


                        string vol_src = (userControl.FindControl("txtVol_Src") as System.Web.UI.WebControls.TextBox).Text;
                        string vol_des = (userControl.FindControl("txtVol_Des") as System.Web.UI.WebControls.TextBox).Text;

                        string item_id_des = (userControl.FindControl("cbxHangHoa_Des") as RadComboBox).SelectedValue;


                        string storeProc = "[usp_InsertUpdatepromotion_item_1]";
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(storeProc, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@row_id", row_id);
                                cmd.Parameters.AddWithValue("@promo_id", promo_id);
                                cmd.Parameters.AddWithValue("@item_id_src", item_id_src);
                                cmd.Parameters.AddWithValue("@vol_src", vol_src);
                                cmd.Parameters.AddWithValue("@item_id_des", item_id_des);
                                cmd.Parameters.AddWithValue("@vol_des", vol_des);


                                conn.Open();
                                result = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();



                                string vol_src2 = (userControl.FindControl("txtVol_Src2") as System.Web.UI.WebControls.TextBox).Text;
                                string vol_des2 = (userControl.FindControl("txtVol_Des2") as System.Web.UI.WebControls.TextBox).Text;

                                string vol_src3 = (userControl.FindControl("txtVol_Src3") as System.Web.UI.WebControls.TextBox).Text;
                                string vol_des3 = (userControl.FindControl("txtVol_Des3") as System.Web.UI.WebControls.TextBox).Text;

                                string vol_src4 = (userControl.FindControl("txtVol_Src4") as System.Web.UI.WebControls.TextBox).Text;
                                string vol_des4 = (userControl.FindControl("txtVol_Des4") as System.Web.UI.WebControls.TextBox).Text;


                                string item_id_des2 = (userControl.FindControl("cbxHangHoa_Des2") as RadComboBox).SelectedValue;
                                string item_id_des3 = (userControl.FindControl("cbxHangHoa_Des3") as RadComboBox).SelectedValue;
                                string item_id_des4 = (userControl.FindControl("cbxHangHoa_Des4") as RadComboBox).SelectedValue;


                                string sQuery = @"UPDATE  dbo.promotion_item_1
                                                SET     vol_src2 = {0} ,
                                                        vol_des2 = {1} ,
                                                        item_id_des2={2},

                                                        vol_src3 = {3} ,
                                                        vol_des3 = {4} ,
                                                         item_id_des3={5},

                                                        vol_src4 = {6} ,
                                                        vol_des4 = {7},
                                                         item_id_des4={8}

                                                WHERE   row_id = {9}";

                                sQuery = string.Format(sQuery,vol_src2,vol_des2,item_id_des2,vol_src3,vol_des3,item_id_des3,vol_src4,vol_des4,item_id_des4,row_id);
                                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


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


        protected void RadGrid2_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();

                string sQuery = "delete from promotion_item_1 where row_id={0}";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);


            }
            catch (Exception ex)
            {
                //throw;
            }
        }


        #endregion Tab TangHang



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