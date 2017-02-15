using System;
using System.Web.UI;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;

namespace WKS.DMS.WEB.Forms
{
    public partial class user_region : System.Web.UI.Page
    {
         

        public void BindList()
        {
            try
            {
                int userID = int.Parse(Session["userid"].ToString());
                string sQuery = " select employee_name, employee_id from employee where group_id <> 1 and  (group_id = 9  or position like 'HR' )";

                DataTable data = new DataTable();
                sQuery = string.Format(sQuery, cbxHr.SelectedValue.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                cbxHr.DataSource = data;
                cbxHr.DataBind();

                }
            
            catch (Exception ex)
            {

            }
        }


        public void loadManaged()
        {

            try
            {


                string regionName = cbxHr.SelectedValue.Trim();
                string sQuery = "select region_name , region_id  from region where region_name not like '{0}' ";
                DataTable data = new DataTable();
                sQuery = string.Format(sQuery, cbxHr.SelectedValue.Trim());
                data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];



                cbxHr_management.DataSource = data;
                cbxHr_management.DataBind();





            }
            catch (Exception e)
            {


            }

        }

        public void BindGrid()
        {

            int group_id = int.Parse(Session["group_id"].ToString());

            if (group_id != 9)
            {
                try
                {
                    // int parent_id = int.Parse(Session["userid"].ToString());
                    string sQuery = "select c.employee_id , c.employee_name, b.region_name from user_region as a left join region as b on a.region_id = b.region_id left join employee as c on a.user_id = c.employee_id  ";


                    DataTable data = new DataTable();
                    data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    RadGrid1.DataSource = data;
                    RadGrid1.DataBind();

                }
                catch (Exception ex)
                {

                }
            }
            else
            {

                try
                {
                    int parent_id = int.Parse(Session["userid"].ToString());
                    string sQuery = "select c.employee_id , c.employee_name, b.region_name from user_region as a left join region as b on a.region_id = b.region_id left join employee as c on a.user_id = c.employee_id where (   employee_id = '{0}')";
                    sQuery = string.Format(sQuery , parent_id); 


                    DataTable data = new DataTable();
                    data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

                    RadGrid1.DataSource = data;
                    RadGrid1.DataBind();

                }
                catch (Exception ex)
                {

                }



            }








        }


        protected void Page_Load(object sender, EventArgs e)
        {
            int groupID = int.Parse(Session["group_id"].ToString());
            if (groupID == 9)
            {
                cbxHr_management.Visible = false;
                cbxHr.Visible = false;
                btnAdd1.Visible = false;




            }


            if (!Page.IsPostBack)
            {

                BindList();
                BindGrid();
                loadManaged();
            }

        }
        private void HookOnFocus(Control CurrentControl)
        {
            //checks if control is one of TextBox, DropDownList, ListBox or Button
            if ((CurrentControl is System.Web.UI.WebControls.TextBox) ||
                (CurrentControl is System.Web.UI.WebControls.DropDownList) ||
                (CurrentControl is System.Web.UI.WebControls.ListBox) ||
                (CurrentControl is System.Web.UI.WebControls.Button))
                //adds a script which saves active control on receiving focus in the hidden field __LASTFOCUS.
                (CurrentControl as System.Web.UI.WebControls.WebControl).Attributes.Add(
                    "onfocus",
                    "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {}");

            //checks if the control has children
            if (CurrentControl.HasControls())
                //if yes do them all recursively
                foreach (Control CurrentChildControl in CurrentControl.Controls)
                    HookOnFocus(CurrentChildControl);
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //Update Data
            int groupID = int.Parse(Session["group_id"].ToString());
            if (groupID != 9)
            {
                try
                {
                    //  int parentID = int.Parse(Session["userid"].ToString());
                    int employee_id = int.Parse((e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["employee_id"].ToString());
                    string regionId = cbxHr_management.SelectedValue;
                    SqlDataReader rdr = null;
                    string storeProc = "[delete_sub_hr_region]";
                    storeProc = string.Format(storeProc, employee_id);




                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@employee_id", employee_id);
                        cmd.Parameters.AddWithValue("@region_id", regionId);

                        rdr = cmd.ExecuteReader();
                        rdr.Read();

                        rdr.Close();
                        conn.Close();
                    }
                    BindGrid();
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
            else
            {
                BindGrid();
            }
        }



        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            //Update Data

            //string flag1 = "";
            //string storeProcs = "[check_isExist_parent_region]";

            //using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
            //{
            //    SqlCommand cmd1 = new SqlCommand(storeProcs, conn);


            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.AddWithValue("@region_name", cbxHr_management.SelectedValue.Trim());



            //    conn.Open();
            //    flag1 = Convert.ToString(cmd1.ExecuteScalar());


            //    conn.Close();
            //}

            //if (bool.Parse(flag1))
            //{




                try
                {
                    string regionId = cbxHr_management.SelectedValue;


                    string employeeId = cbxHr.SelectedValue;


                    string region_name = (cbxHr_management.SelectedValue);

                    string storeProc = "[insert_sub_hr_region]";
                    SqlDataReader rdr = null;


                    using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(storeProc, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@employee_id", employeeId);
                        cmd.Parameters.AddWithValue("@region_id", regionId);



                        rdr = cmd.ExecuteReader();
                        rdr.Read();

                        rdr.Close();
                        conn.Close();



                    }
                    BindGrid();

                }
                catch (Exception ex)
                {
                }
            }
        
        // RadComboBoxSelectedIndexChangedEventArgs

        protected void cbxHr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string value = cbxHr.SelectedValue.Trim();


            if (cbxHr.SelectedValue == "0")
            {
                BindList();
            }



            if (cbxHr.SelectedValue == "")
            {
                BindList();
            }

            if (cbxHr.SelectedValue != "0")
            {

                loadManaged();
                BindGrid();

            }
        }


        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            BindList();
            BindGrid();
            loadManaged();
        }
    }
}

