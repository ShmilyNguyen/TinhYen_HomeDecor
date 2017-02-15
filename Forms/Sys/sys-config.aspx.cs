using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms.Sys
{
    public partial class sys_config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

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
                string sQuery = @"select * from sys_config";

                DataTable data = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery).Tables[0];

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

                    TextBox txtParam = (TextBox)MyUserControl.FindControl("txtParam");
                    TextBox txtValue = (TextBox)MyUserControl.FindControl("txtValue");
                    TextBox txtNote = (TextBox)MyUserControl.FindControl("txtNote");

                    HiddenField hdf_id = (HiddenField)MyUserControl.FindControl("hdf_id");

                    //Edit
                    if (parentItem != null)
                    {
                        string id = parentItem["id"].Text;

                        string param_key = parentItem["param_key"].Text;
                        string param_value = parentItem["param_value"].Text;
                        string note = parentItem["note"].Text;

                        hdf_id.Value = id == "&nbsp;" ? "" : id;

                        txtParam.Text = param_key == "&nbsp;" ? "" : param_key;
                        txtValue.Text = param_value == "&nbsp;" ? "" : param_value;
                        txtNote.Text = note == "&nbsp;" ? "" : note;
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

                        string id = (userControl.FindControl("hdf_id") as System.Web.UI.WebControls.HiddenField).Value;

                        string param_key = (userControl.FindControl("txtParam") as System.Web.UI.WebControls.TextBox).Text;
                        string param_value = (userControl.FindControl("txtValue") as System.Web.UI.WebControls.TextBox).Text;
                        string note = (userControl.FindControl("txtNote") as System.Web.UI.WebControls.TextBox).Text;

                        string sQuery = "update sys_config set param_value=N'{0}' where id={1}";
                        sQuery = string.Format(sQuery, param_value, id);
                        int result = 0;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                            {
                                SqlCommand cmd = new SqlCommand(sQuery, conn);
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
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
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}