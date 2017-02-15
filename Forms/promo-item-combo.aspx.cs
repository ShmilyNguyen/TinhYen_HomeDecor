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
    public partial class promo_item_combo : System.Web.UI.Page
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


                sQuery = "select item_id,item_code,item_name from item";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadComboBox1.DataSource = tb;
                RadComboBox1.DataBind();
                RadComboBox2.DataSource = tb;
                RadComboBox2.DataBind();



            }
            catch (Exception ex)
            {


            }

        }


        public void BindGrid()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"
                                     SELECT  a.* ,
                                            b.item_name,b.item_code 
                                    FROM    dbo.promotion_item_src AS a
                                            LEFT JOIN dbo.item AS b ON a.item_id = b.item_id
                                    where a.promo_id = {0}
                                                                                                        ";

                sQuery = string.Format(sQuery, txtID.Text.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadGrid1.DataSource = data;
                RadGrid1.DataBind();



                sQuery = @"
                                     SELECT  a.* ,
                                            b.item_name,b.item_code 
                                    FROM    dbo.promotion_item_des AS a
                                            LEFT JOIN dbo.item AS b ON a.item_id = b.item_id
                                    where a.promo_id = {0}
                                                                                                        ";

                sQuery = string.Format(sQuery, txtID.Text.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadGrid2.DataSource = data;
                RadGrid2.DataBind();



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
                BindGrid();
            }
        }


        #region Tab Tang Hang


      

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();



                string sQuery = @"DELETE  FROM dbo.promotion_item_src where row_id={0}
                                    ";
                sQuery = string.Format(sQuery,row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }



        protected void RadGrid2_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();



                string sQuery = @"DELETE  FROM dbo.promotion_item_des where row_id={0}
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

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            //Update Data

            try
            {


                string sQuery = @"INSERT INTO dbo.promotion_item_src
                                            ( promo_level ,
                                              group_code ,
                                              promo_id ,
                                              item_id ,
                                              qty
                                            )
                                    VALUES  ( {0} , -- promo_level - int
                                              '{1}' , -- group_code - varchar(50)
                                              {2} , -- promo_id - int
                                              {3} , -- item_id - int
                                              {4}  -- qty - int
                                            )";
                sQuery = string.Format(sQuery, 0,txtGroup1.Text,txtID.Text,RadComboBox1.SelectedValue,txtQty1.Text);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }


        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            //Update Data

            try
            {


                string sQuery = @"INSERT INTO dbo.promotion_item_des
                                            ( promo_level ,
                                              group_code ,
                                              promo_id ,
                                              item_id ,
                                              qty
                                            )
                                    VALUES  ( {0} , -- promo_level - int
                                              '{1}' , -- group_code - varchar(50)
                                              {2} , -- promo_id - int
                                              {3} , -- item_id - int
                                              {4}  -- qty - int
                                            )";
                sQuery = string.Format(sQuery, 0, txtGroup2.Text, txtID.Text, RadComboBox2.SelectedValue, txtQty2.Text);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }



    }
}