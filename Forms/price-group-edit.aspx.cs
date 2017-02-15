using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Microsoft.ApplicationBlocks.Data;
using Telerik.Web.UI;
using System.Collections.Generic;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms
{
    public partial class price_group_edit : System.Web.UI.Page
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
                string sQuery = "select * from customer_channel";
                
                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                lcbCustomerType.DataSource = data;
                lcbCustomerType.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        public void GenCode()
        {
            try
            {
                string sQuery = @"select isnull(max(price_group_id),0) + 1 from price_group";
                string result = SqlHelper.ExecuteScalar(clsCommon.strCon, CommandType.Text, sQuery).ToString();
                txtID.Text = result;
            }
            catch (Exception ex)
            {
            }
        }

        public void ReloadData(string id)

        {
            try
            {
                string sQuery = "SELECT * from price_group where price_group_id={0}";
                sQuery = string.Format(sQuery, id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                foreach (DataRow r in tb.Rows)
                {
                    
                    string name = r["price_group_name"] == DBNull.Value ? "" : r["price_group_name"].ToString();
                    string code = r["price_group_code"] == DBNull.Value ? "" : r["price_group_code"].ToString();

                    string note = r["note"] == DBNull.Value ? "" : r["note"].ToString();

                    string is_active = r["is_active"] == DBNull.Value ? "" : r["is_active"].ToString();
               
                    string fromdate = r["fromdate"] == DBNull.Value ? "0" : r["fromdate"].ToString();
                    string todate = r["todate"] == DBNull.Value ? "0" : r["todate"].ToString();

                    txtID.Text = id;
                    txtName.Text = name;
                    txtCode.Text = code;
                    txtNote.Text = note;

                    txtFromDate.Text = fromdate;
                    txtToDate.Text = todate;

                    txtID.Enabled = false;

                    chckActive.Checked = bool.Parse(is_active);

                    //Lay danh dach loai khach hang ap dung chuong trinh
                    sQuery = "select * from price_group_alloc where price_group_id ={0}";
                    sQuery = string.Format(sQuery,id);
                    DataTable tb2 = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                    foreach (DataRow r2 in tb2.Rows)
                    {
                        IList<RadListBoxItem> collection = lcbCustomerType.Items;
                        foreach (RadListBoxItem item in collection)
                        {
                           if(item.Value == r2["customer_type_id"].ToString())
                            {
                                item.Checked = true;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string storeProc = "[usp_InsertUpdateprice_group]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@price_group_id", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@price_group_code", txtCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@price_group_name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@note", txtNote.Text.Trim());
                    cmd.Parameters.AddWithValue("@is_active", chckActive.Checked);

                    cmd.Parameters.AddWithValue("@fromdate", txtFromDate.Text);
                    cmd.Parameters.AddWithValue("@todate", txtToDate.Text);



                    conn.Open();
                    string result = Convert.ToString(cmd.ExecuteNonQuery());

                    //if(!string.IsNullOrEmpty(result))
                    //{
                    //    txtID.Text = result;
                    //}
                   

                    conn.Close();


                    //Cap nhat phan bo gia cho loai khach hang
                    string sQuery = @"delete from price_group_alloc where price_group_id={0}";
                
                    sQuery = string.Format(sQuery, txtID.Text.Trim());
                    SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                    IList<RadListBoxItem> collection = lcbCustomerType.CheckedItems;
                    foreach (RadListBoxItem item in collection)
                    {
                        sQuery = @"INSERT INTO dbo.price_group_alloc
                                                ( price_group_id ,
                                                  customer_type_id
                                                )
                                        VALUES  ( {0} , -- price_group_id - int
                                                  {1}  -- customer_type_id - int
                                                )";
                        sQuery = string.Format(sQuery,txtID.Text.Trim(),item.Value);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }






        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("price-group-list.aspx");
        }




       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;

                string sQuery = @"delete from price_group where price_group_id={0}";

                sQuery = string.Format(sQuery, id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                Response.Redirect("price-group-list.aspx");

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        #region bang gia

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
            string sQuery = @"SELECT  ISNULL(b.item_price_id, 0) AS item_price_id ,
                                            a.item_id ,
                                            a.item_code ,
                                            a.item_name ,
                                            ISNULL(price1, 0) AS price1 ,
                                            ISNULL(price2, 0) AS price2 ,
                                            ISNULL(price3, 0) AS price3 ,
                                            ISNULL(support_price, 0) AS support_price ,

                                            ISNULL(sellin_price, 0) AS sellin_price ,
                                            ISNULL(saleout_price, 0) AS saleout_price ,
                                            ISNULL(retail_price, 0) AS retail_price ,

                                            ISNULL(giamua_truocvat, 0) AS giamua_truocvat ,
                                            ISNULL(giamua_vat, 0) AS giamua_vat ,
                                            ISNULL(giamua_sauvat, 0) AS giamua_sauvat ,

                                            ISNULL(giaban_truocvat, 0) AS giaban_truocvat ,
                                            ISNULL(giaban_vat, 0) AS giaban_vat ,
                                            ISNULL(giaban_sauvat, 0) AS giaban_sauvat ,



                                            ISNULL(b.is_Active, 0) AS is_Active ,
                                            ISNULL(price_group_id, 0) AS price_group_id
                                    FROM    dbo.item AS a
                                            LEFT JOIN ( SELECT  *
                                                        FROM    dbo.item_price
                                                        WHERE   price_group_id = {0}
                                                      ) AS b ON a.item_id = b.item_id
                                    WHERE   a.item_name LIKE N'%{1}%'
                                            OR a.item_code LIKE N'%{1}%'
                                            OR b.fromdate LIKE N'%{1}%'
                                            OR b.todate LIKE N'%{1}%'
                                    ORDER BY item_name";

            sQuery = string.Format(sQuery,txtID.Text,txtKeyword.Text.Trim());
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


                    System.Web.UI.WebControls.Label lblRowId = (System.Web.UI.WebControls.Label)MyUserControl.FindControl("lblRowId");
                    System.Web.UI.WebControls.Label lblItemId = (System.Web.UI.WebControls.Label)MyUserControl.FindControl("lblItemId");

                    System.Web.UI.WebControls.TextBox txtName = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtName");




                    RadNumericTextBox txtDonGiaNhap = (RadNumericTextBox)MyUserControl.FindControl("txtDonGiaNhap");
                    RadNumericTextBox txtDonGiaXuat = (RadNumericTextBox)MyUserControl.FindControl("txtDonGiaXuat");
                    RadNumericTextBox txtGiaHoTro = (RadNumericTextBox)MyUserControl.FindControl("txtGiaHoTro");

                    RadNumericTextBox txtGiaMuaTruocVAT = (RadNumericTextBox)MyUserControl.FindControl("txtGiaMuaTruocVAT");
                    RadNumericTextBox txtGiaMuaVAT = (RadNumericTextBox)MyUserControl.FindControl("txtGiaMuaVAT");
                    RadNumericTextBox txtGiaMuaSauVAT = (RadNumericTextBox)MyUserControl.FindControl("txtGiaMuaSauVAT");


                    RadNumericTextBox txtGiaBanTruocVAT = (RadNumericTextBox)MyUserControl.FindControl("txtGiaBanTruocVAT");
                    RadNumericTextBox txtGiaBanVAT = (RadNumericTextBox)MyUserControl.FindControl("txtGiaBanVAT");
                    RadNumericTextBox txtGiaBanSauVAT = (RadNumericTextBox)MyUserControl.FindControl("txtGiaBanSauVAT");

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
                       
                        string sellin_price = parentItem["sellin_price"].Text;
                        string saleout_price = parentItem["saleout_price"].Text;
                        string support_price = parentItem["support_price"].Text;

                        string price1 = parentItem["price1"].Text;
                        string price2 = parentItem["price2"].Text;
                        string price3 = parentItem["price3"].Text;




                        string giamua_truocvat = parentItem["giamua_truocvat"].Text;
                        string giamua_vat = parentItem["giamua_vat"].Text;
                        string giamua_sauvat = parentItem["giamua_sauvat"].Text;

                        string giaban_truocvat = parentItem["giaban_truocvat"].Text;
                        string giaban_vat = parentItem["giaban_vat"].Text;
                        string giaban_sauvat = parentItem["giaban_sauvat"].Text;


                        lblRowId.Text = item_price_id == "&nbsp;" ? "0" : item_price_id;
                        lblItemId.Text = item_id == "&nbsp;" ? "0" : item_id;



                        txtName.Text = name;



                        txtDonGiaNhap.Text = sellin_price == "&nbsp;" ? "0" : sellin_price;
                        txtDonGiaXuat.Text = saleout_price == "&nbsp;" ? "0" : saleout_price;
                        txtGiaHoTro.Text = support_price == "&nbsp;" ? "0" : support_price;

                        txtGiaBan1.Text = price1 == "&nbsp;" ? "0" : price1;
                        txtGiaBan2.Text = price2 == "&nbsp;" ? "0" : price2;
                        txtGiaBan3.Text = price3 == "&nbsp;" ? "0" : price3;




                        txtGiaMuaTruocVAT.Text = giamua_truocvat == "&nbsp;" ? "0" : giamua_truocvat;
                        txtGiaMuaVAT.Text = giamua_vat == "&nbsp;" ? "0" : giamua_vat;
                        txtGiaMuaSauVAT.Text = giamua_sauvat == "&nbsp;" ? "0" : giamua_sauvat;


                        txtGiaBanTruocVAT.Text = giaban_truocvat == "&nbsp;" ? "0" : giaban_truocvat;
                        txtGiaBanVAT.Text = giaban_vat == "&nbsp;" ? "0" : giaban_vat;
                        txtGiaBanSauVAT.Text = giaban_sauvat == "&nbsp;" ? "0" : giaban_sauvat;


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

                    int item_price_id = int.Parse((userControl.FindControl("lblRowId") as System.Web.UI.WebControls.Label).Text);

                    //Neu la ID moi, lay MAX ID
                    if (item_price_id == 0)
                    {
                        //Add New
                        string ID = "";
                        string Code = "";
                        string UserID = Session["userid"].ToString();

                        clsCodeMaster.GenCode("item-price", UserID, out ID, out Code);
                        item_price_id = int.Parse(ID);
                    }


                    int item_id = int.Parse( (userControl.FindControl("lblItemId") as System.Web.UI.WebControls.Label).Text);


                    float sellin_price = 0;
                    string strsellin_price = (userControl.FindControl("txtDonGiaNhap") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strsellin_price))
                    {
                        sellin_price = float.Parse(strsellin_price);
                    }




                    float saleout_price = 0;
                    string strsaleout_price = (userControl.FindControl("txtDonGiaXuat") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strsaleout_price))
                    {
                        saleout_price = float.Parse(strsaleout_price);
                    }





                    float giahotro = 0;
                    string strgiahotro = (userControl.FindControl("txtGiaHoTro") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strgiahotro))
                    {
                        giahotro = float.Parse(strgiahotro);
                    }




                    decimal price1 = 0;
                    string strprice1 = (userControl.FindControl("txtGiaBan1") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strprice1))
                    {
                        price1 = decimal.Parse(strprice1);
                    }


                    decimal price2 = 0;
                    string strprice2 = (userControl.FindControl("txtGiaBan2") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strprice2))
                    {
                        price2 = decimal.Parse(strprice2);
                    }

                    decimal price3 = 0;
                    string strprice3 = (userControl.FindControl("txtGiaBan3") as RadNumericTextBox).Text.Trim();

                    if (!string.IsNullOrEmpty(strprice3))
                    {
                        price3 = decimal.Parse(strprice3);
                    }






                    decimal giamua_truocvat = decimal.Parse((userControl.FindControl("txtGiaMuaTruocVAT") as RadNumericTextBox).Text.Trim());
                    decimal giamua_vat = decimal.Parse((userControl.FindControl("txtGiaMuaVAT") as RadNumericTextBox).Text.Trim());
                    decimal giamua_sauvat = decimal.Parse((userControl.FindControl("txtGiaMuaSauVAT") as RadNumericTextBox).Text.Trim());


                    decimal giaban_truocvat = decimal.Parse((userControl.FindControl("txtGiaBanTruocVAT") as RadNumericTextBox).Text.Trim());
                    decimal giaban_vat = decimal.Parse((userControl.FindControl("txtGiaBanVAT") as RadNumericTextBox).Text.Trim());
                    decimal giaban_sauvat = decimal.Parse((userControl.FindControl("txtGiaBanSauVAT") as RadNumericTextBox).Text.Trim());





                    string storeProc = "[usp_InsertUpdateitem_price]";
                    double result = 0;
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
                                cmd.Parameters.AddWithValue("@price_group_id", txtID.Text.Trim());
                                cmd.Parameters.AddWithValue("@fromdate", 0);
                                cmd.Parameters.AddWithValue("@todate", 0);
                                cmd.Parameters.AddWithValue("@price1", price1);
                                cmd.Parameters.AddWithValue("@price2", price2);
                                cmd.Parameters.AddWithValue("@price3", price3);

                                cmd.Parameters.AddWithValue("@sellin_price", sellin_price);
                                cmd.Parameters.AddWithValue("@saleout_price", saleout_price);
                                cmd.Parameters.AddWithValue("@support_price", giahotro);


                                cmd.Parameters.AddWithValue("@giamua_truocvat", giamua_truocvat);
                                cmd.Parameters.AddWithValue("@giamua_vat", giamua_vat);
                                cmd.Parameters.AddWithValue("@giamua_sauvat", giamua_sauvat);

                                cmd.Parameters.AddWithValue("@giaban_truocvat", giaban_truocvat);
                                cmd.Parameters.AddWithValue("@giaban_vat", giaban_vat);
                                cmd.Parameters.AddWithValue("@giaban_sauvat", giaban_sauvat);





                                cmd.Parameters.AddWithValue("@is_Active", 1);

                                conn.Open();
                                result = double.Parse((cmd.ExecuteNonQuery().ToString()));
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
        
        #endregion
    }
}
