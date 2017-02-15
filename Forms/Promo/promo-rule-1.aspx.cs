using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Promo
{
    public partial class promo_rule_1 : System.Web.UI.Page
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

        public void BindList()
        {
            try
            {
                string sQuery = "select item_id,item_code , item_code +'-' + item_name as item_name from item";

                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxObject.DataSource = tb;
                cbxObject.DataBind();
                cbxObject.Items.Insert(0, new RadComboBoxItem("All Item", "0"));
                

                cbxItem.DataSource = tb;
                cbxItem.DataBind();
                cbxItem.Items.Insert(0, new RadComboBoxItem(null,null));


                sQuery = @"SELECT  item_category_code
                            FROM    dbo.item_category
                            WHERE   category_level = 1";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxCategory1.DataSource = tb;
                cbxCategory1.DataBind();
                cbxCategory1.Items.Insert(0, new RadComboBoxItem(null, null));

                sQuery = @"SELECT  item_category_code
                            FROM    dbo.item_category
                            WHERE   category_level = 2";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxCategory2.DataSource = tb;
                cbxCategory2.DataBind();
                cbxCategory2.Items.Insert(0, new RadComboBoxItem(null, null));

                sQuery = @"SELECT  item_category_code
                            FROM    dbo.item_category
                            WHERE   category_level = 3";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxCategory3.DataSource = tb;
                cbxCategory3.DataBind();
                cbxCategory3.Items.Insert(0, new RadComboBoxItem(null, null));

                sQuery = @"SELECT DISTINCT size as object_size
                            FROM dbo.item
                            WHERE size IS NOT NULL";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxSize.DataSource = tb;
                cbxSize.DataBind();
                cbxSize.Items.Insert(0, new RadComboBoxItem(null, null));
            }
            catch (Exception ex)
            {
            }
        }


        public DataTable GetData1()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  a.* ,
                                            CASE WHEN a.object_id = 0 THEN 'All Item'
                                                 WHEN a.object_id <> 0 THEN b.item_name
                                            END AS object_name
                                    FROM    dbo.promotion_rule_src1 AS a
                                            LEFT JOIN dbo.item AS b ON a.object_id = b.item_id
                                    WHERE   promo_id = {0}";

                sQuery = string.Format(sQuery, txtID.Text.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;

            }
            catch (Exception ex) 
            { 
            }

            return null;

        }


        public DataTable GetData2()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  a.* ,
                                    b.item_name AS object_name
                            FROM    dbo.promotion_rule_des1 AS a
                                    LEFT JOIN dbo.item AS b ON a.object_id = b.item_id
                            WHERE   promo_id = {0}";

                sQuery = string.Format(sQuery, txtID.Text.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                return data;

            }
            catch (Exception ex)
            {
            }

            return null;

        }


       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDetail();
                BindList();

                
            }
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            try
            {

                //if (cbxItem.SelectedValue=="0" && cbxCategory1.SelectedValue == "" && cbxCategory2.SelectedValue == "" && cbxCategory3.SelectedValue == "" && cbxSize.SelectedValue=="")
                //{
                //    RadWindowManager1.RadAlert("Vui lòng chọn một trong các điều kiện lọc : Category, Size !", 330, 180, "Thông báo", null, null);
                //    return;
                //}


                string sQuery = @"INSERT INTO dbo.promotion_rule_src1
                                            ( promo_id ,
                                              ref_id ,
                                              object_id ,
                                              category1 ,
                                              category2 ,
                                              category3 ,
                                              size ,
                                              operator
                                            )
                                    VALUES  ( {0} , -- promo_id - int
                                              {1} , -- ref_id - int
                                              {2} , -- object_id - int
                                              N'{3}' , -- category1 - nvarchar(100)
                                              N'{4}' , -- category2 - nvarchar(100)
                                              N'{5}' , -- category3 - nvarchar(100)
                                              N'{6}' , -- size - nvarchar(100)
                                              N'{7}'  -- operator - nvarchar(100)
                                            )";
                sQuery = string.Format(sQuery, txtID.Text.Trim(), txtRefID1.Text.Trim(), cbxObject.SelectedValue, cbxCategory1.SelectedValue, cbxCategory2.SelectedValue, cbxCategory3.SelectedValue, cbxSize.SelectedValue, cbxOperator.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //BindGrid();
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();

                string sQuery = @"DELETE  FROM promotion_rule_src1 where row_id={0} ";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //BindGrid();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                string sQuery = @"INSERT INTO dbo.promotion_rule_des1
                                            ( promo_id ,
                                              ref_id ,
                                              promo_type ,
                                              from_value ,
                                              to_value ,
                                              line_discount ,
                                              object_id ,
                                              promo_qty ,
                                              step
                                            )
                                    VALUES  ( {0} , -- promo_id - int
                                              {1} , -- ref_id - int
                                              '{2}' , -- promo_type - varchar(50)
                                              {3} , -- from_value - decimal
                                              {4} , -- to_value - decimal
                                              {5} , -- line_discount - float
                                              {6} , -- object_id - int
                                              {7} , -- promo_qty - float
                                              {8}  -- step - int
                                            )";
                sQuery = string.Format(sQuery, txtID.Text.Trim(), txtRefID2.Text.Trim(), cbxPromoType.SelectedValue,txtFrom.Text,txtTo.Text,"0",cbxItem.SelectedValue,txtPromoQty.Text,txtStep.Text );
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //BindGrid();
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
                string sQuery = @"DELETE  FROM promotion_rule_des1 where row_id={0} ";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //BindGrid();
            }
            catch (Exception ex)
            {
                //throw;
            }
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

        protected void cbxItem_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = GetData1();
        }

        protected void RadGrid2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid2.DataSource = GetData2();
        }
    }
}