using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class promo_ontopdiscount_list : System.Web.UI.Page
    {

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
                string sQuery = @"[sp_promotion_ontop_discount_list]";

                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.StoredProcedure, sQuery, null).Tables[0];
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

        public void BindGrid()
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindList();
                BindGrid();
            }
        }

        public void BindList()
        {
            string sQuery = @"select * from customer_channel";
            DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

            RadComboBox1.DataSource = data;
            RadComboBox1.DataBind();

            RadComboBox1.Items.Insert(0, new RadComboBoxItem("All", "0"));
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            //Update Data

            try
            {
                string sQuery = @"DELETE FROM promotion_ontopdiscount where channel_id = {0}";

                sQuery = string.Format(sQuery, RadComboBox1.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                sQuery = @"INSERT  INTO dbo.promotion_ontopdiscount
                                            ( channel_id, target_vol, ontopdiscount, active,isVAT, isOntop )
                                    VALUES  ( {0}, -- channel_id - int
                                                {1},
                                              {2}, -- ontopdiscount - int
                                              'true',  -- active - bit
                                                '{3}',
'{4}'
                                              )";
                sQuery = string.Format(sQuery, RadComboBox1.SelectedValue, txtVol.Text, txtCK.Text, ddlLoaiGiaThanh.SelectedValue, ddlLoaiCK.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                RadGrid1.DataSource = this.myData;
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();

                string sQuery = @"DELETE  FROM dbo.promotion_ontopdiscount where row_id={0}
                                    ";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();
            }
            catch (Exception ex)
            {
                //throw;
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

       

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                //if (e.Item is GridDataItem)
                if (e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;

                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

                    //GridDataItem item = (GridDataItem)e.Item;


                    //GridItem parentItem = e.Item ;

                    System.Web.UI.WebControls.HiddenField hdf_row_id = (System.Web.UI.WebControls.HiddenField)MyUserControl.FindControl("hdf_row_id");
                    System.Web.UI.WebControls.TextBox txtVol = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtVol");
                    System.Web.UI.WebControls.TextBox txtDiscount = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtDiscount");


                    Telerik.Web.UI.RadComboBox cbxLoaiKhachHang = (Telerik.Web.UI.RadComboBox)MyUserControl.FindControl("cbxLoaiKhachHang");



                    System.Web.UI.WebControls.DropDownList ddlLoaiGiaThanh = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlLoaiGiaThanh");
                    System.Web.UI.WebControls.DropDownList ddlLoaiCK = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlLoaiCK");

                    string sQuery = @"select * from customer_channel";
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text,sQuery).Tables[0];
                   

                    cbxLoaiKhachHang.DataSource = tb;
                    cbxLoaiKhachHang.DataBind();
                   

                 


                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";


                        string id = parentItem["row_id"].Text;
                        string channel_id = parentItem["channel_id"].Text;
                        string isVAT = parentItem["isVAT"].Text;
                        string isOntop = parentItem["isOntop"].Text;




                        string target_vol = parentItem["target_vol"].Text;

                        string ontopdiscount = parentItem["ontopdiscount"].Text;
                       


                        hdf_row_id.Value = id == "&nbsp;" ? "" : id;
                        
                        cbxLoaiKhachHang.SelectedValue = channel_id;
                        cbxLoaiKhachHang.Enabled = false;
                        txtVol.Text = target_vol == "&nbsp;" ? "0" : target_vol;
                        txtDiscount.Text = ontopdiscount == "&nbsp;" ? "0" : ontopdiscount;


                     
                        if(isOntop.Equals("False"))
                        {
                            ddlLoaiCK.SelectedValue = "0";
                        }else
                        {
                            ddlLoaiCK.SelectedValue = "1";
                        }


                        if (isVAT.Equals("False"))
                        {
                            ddlLoaiGiaThanh.SelectedValue = "0";
                        }
                        else
                        {
                            ddlLoaiGiaThanh.SelectedValue = "1";
                        }


                    }
                    else
                    {
                        
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

                        int id = int.Parse((userControl.FindControl("hdf_row_id") as System.Web.UI.WebControls.HiddenField).Value);
                        string channel_id = (userControl.FindControl("cbxLoaiKhachHang") as Telerik.Web.UI.RadComboBox).SelectedValue;

                        string target_vol = (userControl.FindControl("txtVol") as System.Web.UI.WebControls.TextBox).Text.Trim();


                        string ontopdiscount = (userControl.FindControl("txtDiscount") as System.Web.UI.WebControls.TextBox).Text.Trim();

                      
                        string isVAT = (userControl.FindControl("ddlLoaiGiaThanh") as System.Web.UI.WebControls.DropDownList).SelectedValue;
                        string isOntop = ((userControl.FindControl("ddlLoaiCK") as System.Web.UI.WebControls.DropDownList).SelectedValue);

                        string sQuery = @"UPDATE  dbo.promotion_ontopdiscount
                                            SET     target_vol = {0} ,
                                                    ontopdiscount = {1} ,
                                                    isVAT = {2} ,
                                                    isOntop = {3}
                                            WHERE   row_id = {4}";
                        sQuery = string.Format(sQuery,target_vol,ontopdiscount,isVAT,isOntop,id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        Refresh_Data();
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