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
    public partial class promo_store : System.Web.UI.Page
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


                sQuery = "select store_id,store_name from store";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                RadComboBox1.DataSource = tb;
                RadComboBox1.DataBind();


                sQuery = @"SELECT  customer_channel_id ,
                                    customer_channel_code + '-' + channel_name AS channel_name
                            FROM    dbo.customer_channel";
                tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];
                cbxChannel.DataSource = tb;
                cbxChannel.DataBind();
                cbxChannel.Items.Insert(0, new RadComboBoxItem("All Channel","0" ));


            }
            catch (Exception ex)
            {


            }

        }


        public DataTable GetData ()
        {
            try
            {
                DataTable data = new DataTable();
                string sQuery = @"SELECT  a.row_id ,
        a.promo_id ,
        b.store_id ,
        b.store_code ,
        b.store_name ,
        r.region_name ,
        e.area_name ,
        CASE a.channel_id
          WHEN 0 THEN 'All Channel'
          ELSE cn.channel_name
        END AS channel_name ,
        a.channel_id
FROM    dbo.promotion_store AS a
        LEFT JOIN dbo.customer_channel AS cn ON a.channel_id = cn.customer_channel_id
        LEFT JOIN dbo.store AS b ON a.store_id = b.store_id
        LEFT JOIN dbo.region AS r ON b.region_id = r.region_id
        LEFT JOIN dbo.area AS e ON b.area_id = e.area_id
                                    WHERE   promo_id = {0}
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDetail();
                //BindGrid();
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data

            try
            {
                string row_id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["row_id"].ToString();



                string sQuery = @"DELETE  FROM dbo.promotion_store where row_id={0}
                                    ";
                sQuery = string.Format(sQuery, row_id);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //BindGrid();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }



        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            //Update Data

            try
            {


                string sQuery = @"INSERT  INTO dbo.promotion_store
                                    ( store_id, promo_id,channel_id )
                            VALUES  ( {0}, -- store_id - int
                                      {1},  -- promo_id - int
                                        {2}
                                      )";
                sQuery = string.Format(sQuery, RadComboBox1.SelectedValue,txtID.Text,cbxChannel.SelectedValue);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //BindGrid();

                RadGrid1.DataSource = GetData();
                RadGrid1.DataBind();

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

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = GetData();
        }


    }
}