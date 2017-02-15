using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using WKS.DMS.WEB.Libs;


namespace WKS.DMS.WEB.Forms
{
    public partial class price_list : System.Web.UI.Page
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
            DataTable data = new DataTable();
            string sQuery = @"SELECT  A.item_price_id ,
                                        A.item_id ,
                                        B.item_name ,
                                        B.item_code ,
                                        A.fromdate ,
                                        A.todate ,
                                        ISNULL(sellin_price, 0) AS sellin_price ,
                                        ISNULL(saleout_price, 0) AS saleout_price ,
                                        ISNULL(support_price, 0) support_price ,
                                        ISNULL(price1, 0) AS price1 ,
                                        ISNULL(price2, 0) AS price2 ,
                                        ISNULL(price3, 0) AS price3
                                FROM    dbo.item_price AS A
                                        LEFT JOIN item AS B ON A.item_id = B.item_id
                               WHERE   B.item_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                                    OR B.item_code LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                                    OR A.fromdate LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                                    OR A.todate LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                             ORDER BY item_name";

            data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

            return data;
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



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RadGrid1.DataSource = GetData();
                RadGrid1.DataBind();

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
           
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {




            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                    System.Web.UI.WebControls.TextBox txtID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtID");

                    RadComboBox cbxHangHoa = (RadComboBox)MyUserControl.FindControl("cbxHangHoa");

                    string sQuery = @"select * from item";
                    DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    cbxHangHoa.DataSource = tb;
                    cbxHangHoa.DataBind();


                    TextBox txtTuNgay = (TextBox)MyUserControl.FindControl("txtTuNgay");
                    TextBox txtDenNgay = (TextBox)MyUserControl.FindControl("txtDenNgay");
                    RadNumericTextBox txtDonGiaNhap = (RadNumericTextBox)MyUserControl.FindControl("txtDonGiaNhap");
                    RadNumericTextBox txtDonGiaXuat = (RadNumericTextBox)MyUserControl.FindControl("txtDonGiaXuat");
                    RadNumericTextBox txtGiaHoTro = (RadNumericTextBox)MyUserControl.FindControl("txtGiaHoTro");

                    RadNumericTextBox txtGiaBan1 = (RadNumericTextBox)MyUserControl.FindControl("txtGiaBan1");
                    RadNumericTextBox txtGiaBan2 = (RadNumericTextBox)MyUserControl.FindControl("txtGiaBan2");
                    RadNumericTextBox txtGiaBan3 = (RadNumericTextBox)MyUserControl.FindControl("txtGiaBan3");


                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string item_price_id = parentItem["item_price_id"].Text;
                        string item_id = parentItem["item_id"].Text;
                       
                        string name = parentItem["item_name"].Text;
                        string fromdate = parentItem["fromdate"].Text;
                        string todate = parentItem["todate"].Text;
                        string sellin_price = parentItem["sellin_price"].Text;
                        string saleout_price = parentItem["saleout_price"].Text;
                        string support_price = parentItem["support_price"].Text;

                        string price1 = parentItem["price1"].Text;
                        string price2 = parentItem["price2"].Text;
                        string price3 = parentItem["price3"].Text;

                        txtID.Text = item_price_id == "&nbsp;" ? "0" : item_price_id;
                        txtID.Enabled = false;


                        cbxHangHoa.SelectedValue = item_id;


                        txtTuNgay.Text = fromdate == "&nbsp;" ? "0" : fromdate;
                        txtDenNgay.Text = todate == "&nbsp;" ? "0" : todate;

                        txtDonGiaNhap.Text = sellin_price == "&nbsp;" ? "0" : sellin_price;
                        txtDonGiaXuat.Text = saleout_price == "&nbsp;" ? "0" : saleout_price;
                        txtGiaHoTro.Text = support_price == "&nbsp;" ? "0" : support_price;

                        txtGiaBan1.Text = price1 == "&nbsp;" ? "0" : price1;
                        txtGiaBan2.Text = price2 == "&nbsp;" ? "0" : price2;
                        txtGiaBan3.Text = price3 == "&nbsp;" ? "0" : price3;


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
                headerItem["DeleteColumn"].Text = string.Empty;
            }
        }

       

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {




            if (e.CommandName == "Update")
            {
                //Update Data

                try
                {


                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                    int item_price_id = int.Parse((userControl.FindControl("txtID") as System.Web.UI.WebControls.TextBox).Text.Trim());

                    //Neu la ID moi, lay MAX ID
                    if (item_price_id==0)
                    {
                        //Add New
                        string ID = "";
                        string Code = "";
                        string UserID = Session["userid"].ToString();

                        clsCodeMaster.GenCode("item-price", UserID, out ID, out Code);
                        item_price_id = int.Parse(ID);
                    }


                    int item_id = int.Parse((userControl.FindControl("cbxHangHoa") as RadComboBox).SelectedValue);



                    string strtungay = (userControl.FindControl("txtTuNgay") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    int fromdate = 0;
                    if (!string.IsNullOrEmpty(strtungay))
                    {
                       fromdate =  int.Parse(strtungay);
                    }

                    
                    string strdenngay = (userControl.FindControl("txtDenNgay") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    int todate = 0;
                    if (!string.IsNullOrEmpty(strdenngay))
                    {
                        todate = int.Parse(strdenngay);
                    }



                    double sellin_price =0;
                    string strsellin_price = (userControl.FindControl("txtDonGiaNhap") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strsellin_price))
                    {
                        sellin_price = double.Parse(strsellin_price);
                    }



                   
                    double saleout_price = 0;
                    string strsaleout_price = (userControl.FindControl("txtDonGiaXuat") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strsaleout_price))
                    {
                        saleout_price = double.Parse(strsaleout_price);
                    }


                   
                   

                    double giahotro = 0;
                    string strgiahotro = (userControl.FindControl("txtGiaHoTro") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strgiahotro))
                    {
                        giahotro = double.Parse(strgiahotro);
                    }



                    
                    double price1 = 0;
                    string strprice1 = (userControl.FindControl("txtGiaBan1") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strprice1))
                    {
                        price1 = double.Parse(strprice1);
                    }


                    double price2 = 0;
                    string strprice2 = (userControl.FindControl("txtGiaBan2") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strprice2))
                    {
                        price2 = double.Parse(strprice2);
                    }

                    double price3 = 0;
                    string strprice3 = (userControl.FindControl("txtGiaBan3") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strprice3))
                    {
                        price3 = double.Parse(strprice3);
                    }
                  




                    string storeProc = "[usp_InsertUpdateitem_price]";
                    int result = 0;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (item_id > 0)
                            {
                                cmd.Parameters.AddWithValue("@item_price_id", item_price_id);
                                cmd.Parameters.AddWithValue("@item_price_code", DBNull.Value);
                                cmd.Parameters.AddWithValue("@item_id", item_id);
                                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                                cmd.Parameters.AddWithValue("@todate", todate);
                                cmd.Parameters.AddWithValue("@price1", price1);
                                cmd.Parameters.AddWithValue("@price2", price2);
                                cmd.Parameters.AddWithValue("@price3", price3);
                                cmd.Parameters.AddWithValue("@sellin_price", sellin_price);
                                cmd.Parameters.AddWithValue("@saleout_price", saleout_price);
                                cmd.Parameters.AddWithValue("@support_price", giahotro);                               
                                cmd.Parameters.AddWithValue("@is_Active", 1);

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
                catch (Exception ex)
                {


                    //throw;
                }


            }


            

        }
    }
}