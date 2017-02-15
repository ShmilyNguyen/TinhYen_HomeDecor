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
    public partial class HangHoa_List : System.Web.UI.Page
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
            string sQuery = @"SELECT I.*,
                               C1.category_name AS category_name1,
                               C2.category_name AS category_name2,
                               C3.category_name AS category_name3,
                               C4.category_name AS category_name4,
                               C5.category_name AS category_name5,
                               U.unit_name
                          FROM item AS I
                               LEFT JOIN item_category AS C1 ON I.category_id1 = C1.item_category_id
                               LEFT JOIN item_category AS C2 ON I.category_id2 = C2.item_category_id
                               LEFT JOIN item_category AS C3 ON I.category_id3 = C3.item_category_id
                               LEFT JOIN item_category AS C4 ON I.category_id4 = C4.item_category_id
                               LEFT JOIN item_category AS C5 ON I.category_id5 = C5.item_category_id
                               LEFT JOIN unit AS U ON I.unit_id = U.unit_id

   WHERE   i.item_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR i.item_code LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR C1.category_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR C2.category_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR C3.category_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR C4.category_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR C5.category_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'
                            OR U.unit_name LIKE N'%" + txtKeyword.Text.Trim() + @"%'

                    ";


            data = SqlHelper.ExecuteDataset(clsCommon.strCon,CommandType.Text,sQuery).Tables[0];

            return data;
        }

        public void Refresh_Data()
        {
            try
            {
                DataTable data = GetData();
                Session["Data_Item"] = data;
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
            //Hashtable table = new Hashtable();
            //(e.Item as GridEditableItem).ExtractValues(table);

            //DataRow row = this.Sellers.Rows.Find((e.Item as GridEditableItem).GetDataKeyValue("ID"));

            //foreach (string key in table.Keys)
            //{
            //    row[key] = table[key] ?? DBNull.Value;
            //}
            //DateTime date;
            //if (DateTime.TryParse((row["BirthDate"].ToString()), out date))
            //{
            //    row["Age"] = DateTime.Now.Year - date.Year;
            //}
            //else
            //{
            //    row["Age"] = 0;
            //}
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Hashtable table = new Hashtable();
            //(e.Item as GridEditableItem).ExtractValues(table);
            //DataRow row = this.Sellers.NewRow();

            //foreach (string key in table.Keys)
            //{
            //    if (table[key] != null)
            //    {
            //        row[key] = table[key];
            //    }
            //}
            //row["ID"] = new Random().Next(int.MaxValue);
            //DateTime date;
            //if (DateTime.TryParse((row["BirthDate"].ToString()), out date))
            //{
            //    row["Age"] = DateTime.Now.Date.Year - date.Year;
            //}
            //this.Sellers.Rows.InsertAt(row, 0);
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //this.Sellers.Rows.Remove(this.Sellers.Rows.Find((e.Item as GridEditableItem).GetDataKeyValue("ID")));
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //RadComboBox comboBox = e.Item.FindControl("RCB_City") as RadComboBox;
            //if (comboBox != null)
            //{
            //    if (!(e.Item.DataItem is GridInsertionObject))
            //    {
            //        comboBox.SelectedValue = (e.Item.DataItem as DataRowView)["City"].ToString();
            //    }
            //    comboBox.DataTextField = string.Empty;
            //    comboBox.DataSource = this.GetCities();
            //    comboBox.DataBind();
            //    if (this.RadGrid1.ResolvedRenderMode == RenderMode.Mobile)
            //    {
            //        (e.Item.FindControl("TB_Age") as WebControl).Enabled = false;
            //    }
            //    else
            //    {
            //        ((e.Item as GridEditableItem)["Age"].Controls[0] as WebControl).Enabled = false;
            //    }
            //}


            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                    System.Web.UI.WebControls.TextBox txtItem_ID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtItem_ID");
                    System.Web.UI.WebControls.TextBox txtItem_Code = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtItem_Code");
                    System.Web.UI.WebControls.TextBox txtItem_Name = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtItem_Name");

                    System.Web.UI.WebControls.DropDownList ddlUnit = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlUnit");

                    System.Web.UI.WebControls.DropDownList ddlCategory1 = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlCategory1");
                    System.Web.UI.WebControls.DropDownList ddlCategory2 = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlCategory2");
                    System.Web.UI.WebControls.DropDownList ddlCategory3 = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlCategory3");
                    System.Web.UI.WebControls.DropDownList ddlCategory4 = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlCategory4");
                    System.Web.UI.WebControls.DropDownList ddlCategory5 = (System.Web.UI.WebControls.DropDownList)MyUserControl.FindControl("ddlCategory5");
                    //string GhiChu = parentItem["GhiChu"].Text;



                    System.Web.UI.WebControls.TextBox txtQuyCach1 = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtQuyCach1");
                    System.Web.UI.WebControls.TextBox txtQuyCach2 = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtQuyCach2");

                    System.Web.UI.WebControls.TextBox txtSize = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtSize");
                    System.Web.UI.WebControls.TextBox txtWeight = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtWeight");
                    System.Web.UI.WebControls.TextBox txtVol = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtVol");



                    System.Web.UI.WebControls.Button btnSave = (System.Web.UI.WebControls.Button)MyUserControl.FindControl("btnSave");


                    DataTable tbCategory = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM item_category").Tables[0];
                    DataTable tbUnit = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, "SELECT * FROM unit").Tables[0];


                    ddlUnit.DataSource = tbUnit;
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, new ListItem(string.Empty, "0"));

                    ddlCategory1.DataSource = tbCategory;
                    ddlCategory1.DataBind();
                    ddlCategory1.Items.Insert(0, new ListItem(string.Empty, "0"));

                    ddlCategory2.DataSource = tbCategory;
                    ddlCategory2.DataBind();
                    ddlCategory2.Items.Insert(0, new ListItem(string.Empty, "0"));

                    ddlCategory3.DataSource = tbCategory;
                    ddlCategory3.DataBind();
                    ddlCategory3.Items.Insert(0, new ListItem(string.Empty, "0"));

                    ddlCategory4.DataSource = tbCategory;
                    ddlCategory4.DataBind();
                    ddlCategory4.Items.Insert(0, new ListItem(string.Empty, "0"));

                    ddlCategory5.DataSource = tbCategory;
                    ddlCategory5.DataBind();
                    ddlCategory5.Items.Insert(0, new ListItem(string.Empty,  "0"));

                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string item_id = parentItem["item_id"].Text;
                        string item_code = parentItem["item_code"].Text;
                        string item_name = parentItem["item_name"].Text;
                        string unit_id = parentItem["unit_id"].Text;

                        string category_id1 = parentItem["category_id1"].Text;
                        string category_id2 = parentItem["category_id2"].Text;
                        string category_id3 = parentItem["category_id3"].Text;
                        string category_id4 = parentItem["category_id4"].Text;
                        string category_id5 = parentItem["category_id5"].Text;

                        string specification_1 = parentItem["specification_1"].Text;
                        string specification_2 = parentItem["specification_2"].Text;


                        string size = parentItem["size"].Text;
                        string weight = parentItem["weight"].Text;
                        string volume = parentItem["volume"].Text;



                        txtItem_ID.Text = item_id == "&nbsp;" ? "" : item_id;
                        txtItem_ID.Enabled = false;

                        txtItem_Code.Text = item_code == "&nbsp;" ? "" : item_code;
                        txtItem_Code.Enabled = false;

                        txtItem_Name.Text = item_name == "&nbsp;" ? "" : item_name;

                        

                     
                        ddlUnit.SelectedValue = unit_id == "&nbsp;" ? "" : unit_id;                       
                      
                        ddlCategory1.SelectedValue = category_id1 == "&nbsp;" ? "" : category_id1;
                       
                        ddlCategory2.SelectedValue = category_id2 == "&nbsp;" ? "" : category_id2;
                      
                        ddlCategory3.SelectedValue = category_id3 == "&nbsp;" ? "" : category_id3;
                        
                        ddlCategory4.SelectedValue = category_id4 == "&nbsp;" ? "" : category_id4;
                       
                        ddlCategory5.SelectedValue = category_id5 == "&nbsp;" ? "" : category_id5;

                        txtQuyCach1.Text = specification_1 == "&nbsp;" ? "" : specification_1;
                        txtQuyCach2.Text = specification_2 == "&nbsp;" ? "0" : specification_2;

                        txtSize.Text = size == "&nbsp;" ? "" : size;
                        txtWeight.Text = weight == "&nbsp;" ? "" : weight;
                        txtVol.Text = volume == "&nbsp;" ? "" : volume;



                    }else
                    {
                        //Add New
                        string ID = "";
                        string Code = "";
                        string UserID = Session["userid"].ToString();

                        clsCodeMaster.GenCode("Item", UserID, out ID, out Code);
                        txtItem_ID.Text = ID;
                        txtItem_Code.Text = Code;
                        txtItem_Code.Focus();
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


                    int item_id = int.Parse((userControl.FindControl("txtItem_ID") as System.Web.UI.WebControls.TextBox).Text.Trim());
                    string item_code = (userControl.FindControl("txtItem_Code") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string item_name = (userControl.FindControl("txtItem_Name") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    int brand_id = 0;
                    int unit_id = int.Parse((userControl.FindControl("ddlUnit") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                    string barcode = "NULL";
                    float min_stock_vol = 0;
                    float max_stock_vol = 0;
                    int manufacturer_id = 0;
                    string tags = "NULL";
                    string description = "NULL";
                    int item_type_id = 0;
                    bool is_follow_inventory = true;
                    bool is_component = true;
                    bool is_Active = true;

                    string specification_1 = (userControl.FindControl("txtQuyCach1") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string specification_2 = (userControl.FindControl("txtQuyCach2") as System.Web.UI.WebControls.TextBox).Text.Trim();


                    string Size = (userControl.FindControl("txtSize") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string Vol = (userControl.FindControl("txtVol") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string Weight = (userControl.FindControl("txtWeight") as System.Web.UI.WebControls.TextBox).Text.Trim();


                    int category_id1 = int.Parse((userControl.FindControl("ddlCategory1") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                    int category_id2 = int.Parse((userControl.FindControl("ddlCategory2") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                    int category_id3 = int.Parse((userControl.FindControl("ddlCategory3") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                    int category_id4 = int.Parse((userControl.FindControl("ddlCategory4") as System.Web.UI.WebControls.DropDownList).SelectedValue);
                    int category_id5 = int.Parse((userControl.FindControl("ddlCategory5") as System.Web.UI.WebControls.DropDownList).SelectedValue);

                    string storeProc = "[usp_InsertUpdateitem]";
                    int result = 0;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                        {
                            SqlCommand cmd = new SqlCommand(storeProc, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (item_id > 0)
                            {
                                cmd.Parameters.AddWithValue("@item_id", item_id);
                                cmd.Parameters.AddWithValue("@item_code", item_code);
                                cmd.Parameters.AddWithValue("@item_name", item_name);
                                cmd.Parameters.AddWithValue("@brand_id", brand_id);
                                cmd.Parameters.AddWithValue("@category_id1", category_id1);
                                cmd.Parameters.AddWithValue("@category_id2", category_id2);
                                cmd.Parameters.AddWithValue("@category_id3", category_id3);
                                cmd.Parameters.AddWithValue("@category_id4", category_id4);
                                cmd.Parameters.AddWithValue("@category_id5", category_id5);
                                cmd.Parameters.AddWithValue("@unit_id", unit_id);
                                cmd.Parameters.AddWithValue("@barcode", barcode);
                                cmd.Parameters.AddWithValue("@min_stock_vol", min_stock_vol);
                                cmd.Parameters.AddWithValue("@max_stock_vol", max_stock_vol);
                                cmd.Parameters.AddWithValue("@manufacturer_id", manufacturer_id);
                                cmd.Parameters.AddWithValue("@tags", tags);
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@item_type_id", item_type_id);
                                cmd.Parameters.AddWithValue("@is_follow_inventory", is_follow_inventory);
                                cmd.Parameters.AddWithValue("@is_component", is_component);
                                cmd.Parameters.AddWithValue("@is_Active", is_Active);
                                cmd.Parameters.AddWithValue("@specification_1", specification_1);
                                cmd.Parameters.AddWithValue("@specification_2", specification_2);
                                cmd.Parameters.AddWithValue("@package_1", "");
                                cmd.Parameters.AddWithValue("@package_2", 0);
                                cmd.Parameters.AddWithValue("@weight", Weight);
                                cmd.Parameters.AddWithValue("@volume", Vol);
                                cmd.Parameters.AddWithValue("@size", Size);


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
                catch (Exception ex)
                {


                    //throw;
                }


            }


            if (e.CommandName == "Delete")
            {
                //string item_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["item_id"].ToString();


                ////int item_id = int.Parse((userControl.FindControl("txtItem_ID") as System.Web.UI.WebControls.TextBox).Text.Trim());

                //string sQuery = "delete from item where item_id=" + item_id;
                //int result = 0;
                //try
                //{
                //    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                //    {
                //        SqlCommand cmd = new SqlCommand(sQuery, conn);
                //        cmd.CommandType = CommandType.StoredProcedure;                     

                //        conn.Open();
                //        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                //        conn.Close();
                //        Refresh_Data();
                //    }
                //}
                //catch (Exception ex)
                //{
                //}
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
    }
}