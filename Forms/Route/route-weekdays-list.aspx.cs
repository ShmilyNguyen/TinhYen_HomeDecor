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

namespace WKS.DMS.WEB.Forms.Route
{
    public partial class route_weekdays_list : System.Web.UI.Page
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


            string sQuery = @"select * from route_weekdays";


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

            try
            {

                if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
                {
                    UserControl MyUserControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;


                    System.Web.UI.WebControls.TextBox txtID = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtID");
                    System.Web.UI.WebControls.TextBox txtCode = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtCode");
                    System.Web.UI.WebControls.TextBox txtName = (System.Web.UI.WebControls.TextBox)MyUserControl.FindControl("txtName");


                 


                    //Edit
                    if (parentItem != null)
                    {
                        //btnSave.CommandName = "Update";

                        string id = parentItem["route_id"].Text;
                        string code = parentItem["route_code"].Text;
                        string name = parentItem["route_name"].Text;

                       

                        txtID.Text = id == "&nbsp;" ? "" : id;
                       

                        txtCode.Text = code == "&nbsp;" ? "" : code;
                        

                        txtName.Text = name == "&nbsp;" ? "" : name;

                      


                    }
                    else
                    {
                        //Add New
                        string ID = "";
                        string Code = "";
                        
                        txtID.Text = ID;
                        txtCode.Text = Code;
                        txtCode.Focus();
                    }





                }
            }
            catch (Exception ex)
            {


            }


        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHeaderItem headerItem = e.Item as GridHeaderItem;
            if (headerItem != null)
            {
                headerItem["EditColumn"].Text = string.Empty;

            }
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


                        int id = int.Parse((userControl.FindControl("txtID") as System.Web.UI.WebControls.TextBox).Text.Trim());
                        string code = (userControl.FindControl("txtCode") as System.Web.UI.WebControls.TextBox).Text.Trim();
                        string name = (userControl.FindControl("txtName") as System.Web.UI.WebControls.TextBox).Text.Trim();


                        string sQuery = @"delete from route_weekdays where route_id = {0}";
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);

                        sQuery = @"INSERT INTO dbo.route_weekdays
                                            ( route_id, route_code, route_name )
                                    VALUES  ( {0}, -- route_id - int
                                              '{1}', -- route_code - varchar(50)
                                              N'{2}'  -- route_name - nvarchar(50)
                                              )";

                        sQuery = string.Format(sQuery,id,code,name);
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