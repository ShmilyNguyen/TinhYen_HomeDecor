using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WKS.DMS.WEB.Libs;

namespace WKS.DMS.WEB.Forms.Route
{
    public partial class change_route : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {

            try
            {
                string sQuery = @"select store_id,store_name from store where is_Active=1";
                sQuery = string.Format(sQuery);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                cbxStore1.DataSource = tb;
                cbxStore1.DataBind();
                cbxStore1.Items.Insert(0, new RadComboBoxItem(null, "0"));

                cbxStore2.DataSource = tb;
                cbxStore2.DataBind();
                cbxStore2.Items.Insert(0, new RadComboBoxItem(null, "0"));

               
            }
            catch (Exception ex)
            {


            }






        }


        private void ShowNoResultFound(DataTable source, GridView gv)
        {
            source.Rows.Add(source.NewRow()); // create a new blank row to the DataTable
            // Bind the DataTable which contain a blank row to the GridView
            gv.DataSource = source;
            gv.DataBind();
            // Get the total number of columns in the GridView to know what the Column Span should be
            int columnsCount = gv.Columns.Count;
            gv.Rows[0].Cells.Clear();// clear all the cells in the row
            gv.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            gv.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

            //You can set the styles here
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[0].Font.Bold = true;
            //set No Results found to the new added cell
            gv.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
        }


        public void BindEmployee(string store_id, int index)
        {
            try
            {
                string sQuery = @"SELECT  employee_id ,
                                            ISNULL(employee_code, '') + '-' + ISNULL(employee_name, '') AS employee_name
                                    FROM    dbo.employee WHERE store_id={0}";

                sQuery = string.Format(sQuery, store_id);
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                if (index == 1)
                {
                     cbxEmployee1.DataSource = tb;
                    cbxEmployee1.DataBind();
                    cbxEmployee1.Items.Insert(0, new RadComboBoxItem(null, "0"));
                    BindGrid(store_id, "0", txtKeyword1.Text.Trim(),1);

                }

                if (index == 2)
                {
                    cbxEmployee2.DataSource = tb;
                    cbxEmployee2.DataBind();
                    cbxEmployee2.Items.Insert(0, new RadComboBoxItem(null, "0"));
                    BindGrid(store_id, "0", txtKeyword2.Text.Trim(), 2);
                }



            }
            catch (Exception ex)
            {


            }
        }


        public void BindGrid(string store_id,string employee_id,string keyword,int index)
        {
            try
            {
                string sQuery = @"";

                if (string.IsNullOrEmpty(employee_id) || employee_id=="0")
                {
                    sQuery = @"SELECT  *
                                    FROM    ( SELECT    store_id ,
                                                        customer_id ,employee_id,
                                                        customer_code ,
                                                        a.active ,
                                                        ISNULL(customer_code, '') + '-' + ISNULL(customer_name, '')
                                                        + +ISNULL(add_number, '') + '-' + ISNULL(address, '')
                                                        + '-' + ISNULL(province, '') + '-' + ISNULL(district, '')
                                                        + '-' + ISNULL(ward, '') + '-' + ISNULL(street, '') + '-('
                                                        + ISNULL(mobile, '') + ')' AS customer_name
                                              FROM      dbo.customer AS a
                                              WHERE     store_id = {1}
                                                        
                                            ) AS A
                                    WHERE   ( A.customer_code LIKE N'%{0}%'
                                              OR A.customer_name LIKE N'%{0}%'
                                              OR A.customer_name LIKE N'%{0}%'
                                            OR CHARINDEX(A.customer_code, '{0}', 0) > 0
                                            )";

                    sQuery = string.Format(sQuery, keyword, store_id);
                }else
                {
                    sQuery = @"SELECT  *
                                    FROM    ( SELECT    store_id ,
                                                        customer_id ,employee_id,
                                                        customer_code ,
                                                        a.active ,
                                                        ISNULL(customer_code, '') + '-' + ISNULL(customer_name, '')
                                                        + +ISNULL(add_number, '') + '-' + ISNULL(address, '')
                                                        + '-' + ISNULL(province, '') + '-' + ISNULL(district, '')
                                                        + '-' + ISNULL(ward, '') + '-' + ISNULL(street, '') + '-('
                                                        + ISNULL(mobile, '') + ')' AS customer_name
                                              FROM      dbo.customer AS a
                                              WHERE     store_id = {1}
                                                        AND employee_id = {2}
                                            ) AS A
                                    WHERE   ( A.customer_code LIKE N'%{0}%'
                                              OR A.customer_name LIKE N'%{0}%'
                                              OR A.customer_name LIKE N'%{0}%'
                                            OR CHARINDEX(A.customer_code, '{0}', 0) > 0
                                            )";

                    sQuery = string.Format(sQuery, keyword, store_id, employee_id);
                }

                
                DataTable tb = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                if (index == 1)
                {
                    gvData1.DataSource = tb;
                    gvData1.DataBind();
                }

                if (index == 2)
                {
                    gvData2.DataSource = tb;
                    gvData2.DataBind();
                }

            }
            catch (Exception ex)
            {


            }
        }

        protected void OnPaging1(object sender, GridViewPageEventArgs e)
        {
            BindGrid(cbxStore1.SelectedValue, cbxEmployee1.SelectedValue, txtKeyword1.Text.Trim(),1);
            gvData1.PageIndex = e.NewPageIndex;
            gvData1.DataBind();
        }

        protected void OnPaging2(object sender, GridViewPageEventArgs e)
        {
            BindGrid(cbxStore2.SelectedValue, cbxEmployee2.SelectedValue, txtKeyword2.Text.Trim(), 2);
            gvData2.PageIndex = e.NewPageIndex;
            gvData2.DataBind();
        }



        protected void cbxStore1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindEmployee(cbxStore1.SelectedValue,1);    
        }

        protected void cbxEmployee1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindGrid(cbxStore1.SelectedValue, cbxEmployee1.SelectedValue, txtKeyword1.Text.Trim(),1);
        }

        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            BindGrid(cbxStore1.SelectedValue, cbxEmployee1.SelectedValue, txtKeyword1.Text.Trim(),1);
        }

        

        protected void cbxStore2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindEmployee(cbxStore2.SelectedValue, 2);   
        }

        protected void cbxEmployee2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindGrid(cbxStore2.SelectedValue, cbxEmployee2.SelectedValue, txtKeyword2.Text.Trim(),2);
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            BindGrid(cbxStore2.SelectedValue, cbxEmployee2.SelectedValue, txtKeyword2.Text.Trim(),2);
        }


        protected void MoveData1(object sender, EventArgs e)
        {
            Button lnkRemove = (Button)sender;

            string[] commandArgs = lnkRemove.CommandArgument.ToString().Split(new char[] { ',' });
            string store_id1 = commandArgs[0];
            string customer_id1 = commandArgs[1];
            string employee_id1 = commandArgs[2];
            string active = commandArgs[3];


            string store_id2 = cbxStore2.SelectedValue;            
            string employee_id2 = cbxEmployee2.SelectedValue;


            if (string.IsNullOrEmpty(store_id2) || store_id2=="0")
            {
                RadWindowManager1.RadAlert("Vui lòng chọn NPP đích!", 330, 180, "Thông báo", null, null);
                return;
            }

            if (string.IsNullOrEmpty(employee_id2) || employee_id2 == "0")
            {
                RadWindowManager1.RadAlert("Vui lòng chọn Nhân viên đích!", 330, 180, "Thông báo", null, null);
                return;
            }


            if (active == "0" || active.ToUpper() == "FALSE")
            {
                RadWindowManager1.RadAlert("Không thể chuyển khách hàng này!", 330, 180, "Thông báo", null, null);
                return;
            }
            else
            {

                CloneData(store_id1, customer_id1, employee_id1, store_id2, employee_id2);
                BindGrid(cbxStore1.SelectedValue, cbxEmployee1.SelectedValue, txtKeyword1.Text.Trim(), 1);
                BindGrid(cbxStore2.SelectedValue, cbxEmployee2.SelectedValue, txtKeyword2.Text.Trim(), 2);

            }
            

        }


        public string GenID(string store_id)
        {
            try
            {
                //Add New
                string ID = "";
                string Code = "";
                string UserID = Session["userid"].ToString();

                clsCodeMaster.GenCode_WithStoreID(store_id, "customer", UserID, out ID, out Code);
                return ID;
               
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public void CloneData(string store_id1, string customer_id1,  string employee_id1 , string store_id2, string employee_id2)
        {
            try
            {



                string sQuery = @"UPDATE customer SET active = 0 where store_id={0} AND customer_id={1} AND employee_id={2} ";
                sQuery = string.Format(sQuery, store_id1, customer_id1, employee_id1);
                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                //Tao Code KH mới
                string new_customer_id = GenID(store_id2);
                sQuery = @"INSERT  dbo.customer
                            ( customer_id ,
                              customer_code ,
                              store_id ,
                              customer_name ,
                              mobile ,
                              phone ,
                              email ,
                              birthday ,
                              channel_id ,
                              market_id ,
                              route_id ,
                              route_code ,
                              address ,
                              add_number ,
                              province ,
                              district ,
                              ward ,
                              street ,
                              street_id ,
                              ward_id ,
                              district_id ,
                              province_id ,
                              active ,
                              max_debt_vol ,
                              current_debt_vol ,
                              personal_id ,
                              longitude ,
                              latitude ,
                              employee_code ,
                              visit_order ,
                              employee_id ,
                              created_date ,
                              last_modified, prev_store_id ,
                              prev_customer_id ,
                              prev_customer_code
                            )
                            SELECT  " + new_customer_id + @" ,
                                    customer_code ,
                                    "+store_id2+@" ,
                                    customer_name ,
                                    mobile ,
                                    phone ,
                                    email ,
                                    birthday ,
                                    channel_id ,
                                    market_id ,
                                    route_id ,
                                    route_code ,
                                    address ,
                                    add_number ,
                                    province ,
                                    district ,
                                    ward ,
                                    street ,
                                    street_id ,
                                    ward_id ,
                                    district_id ,
                                    province_id ,
                                    1 ,
                                    max_debt_vol ,
                                    current_debt_vol ,
                                    personal_id ,
                                    longitude ,
                                    latitude ,
                                    employee_code ,
                                    visit_order ,
                                    "+employee_id2+@" ,
                                    created_date ,
                                    last_modified,
                                    " + store_id1 + @"," + customer_id1 + @"," + employee_id1 + @"

                            FROM    dbo.customer
                            WHERE   store_id = " +store_id1+@"
                                    AND customer_id = "+customer_id1+@"
                                    AND employee_id = " + employee_id1;


                SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                BindGrid(cbxStore1.SelectedValue, cbxEmployee1.SelectedValue, txtKeyword1.Text.Trim(), 1);
                BindGrid(cbxStore2.SelectedValue, cbxEmployee2.SelectedValue, txtKeyword2.Text.Trim(), 2);


            }
            catch (Exception ex)
            {
                
                
            }
        }

    }
}