using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace WKS.DMS.WEB.Forms
{
    public partial class saleout_list : System.Web.UI.Page
    {
        public static string _saleout_edit_url = "";

        public DataTable GetData()
        {
            DataTable data = new DataTable();
            string storeProc = "[sp_Saleout_Search]";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@user_id", Session["userid"]);
                    cmd.Parameters.AddWithValue("@keyword", txtKeyword.Text.Trim());
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);
                    conn.Close();

                    return data;
                }
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

        public void ReloadGrid()
        {
            Refresh_Data();
            RadGrid1.DataSource = GetData();
            RadGrid1.DataBind();
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = GetData();
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["saleout_id"].ToString();
            //Response.Redirect(_saleout_edit_url + "?id=" + id);

            //Kenh GT
         
            if (Session["channel_dist_id"].ToString().Equals("1"))
            {
                Response.Redirect( clsCommon.UrlRoot + "Forms/saleout-edit-2.aspx" + "?id=" + id  );
            }
            // Kenh MT 
            if (Session["channel_dist_id"].ToString().Equals("2"))
            {
               
                Response.Redirect( clsCommon.UrlRoot + "Forms/saleout-edit-mt.aspx" + "?id=" + id );
            }


        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //GridHeaderItem headerItem = e.Item as GridHeaderItem;
            //if (headerItem != null)
            //{
            //    headerItem["EditColumn"].Text = string.Empty;

            //}
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["saleout_id"].ToString();
                string doc_id = "0";

                //Lay DocID
                string storeProc = "[sp_getdocid_by_saleoutid]";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@saleout_id", id);

                    conn.Open();
                    doc_id = Convert.ToString(cmd.ExecuteScalar());

                    conn.Close();
                }

                if (!string.IsNullOrEmpty(doc_id))
                {
                    Response.Redirect("Payment/TrackingCustomerDebt.aspx?id=" + doc_id);
                }
                else
                {
                    RadWindowManager1.RadAlert("Đơn hàng này không có phát sinh công nợ, Vui lòng liên hệ IT để được hỗ trợ !", 330, 180, "Thông báo", null, null);
                }

                /*int result = WKS.DMS.WEB.Libs.clsProcessOrder.Order_Confirmed(id, Session["userid"].ToString());
                if (result == 1)
                {
                    ReloadGrid();

                    RadWindowManager1.RadAlert("Đơn hàng đã được xác nhận !", 330, 180, "Thông báo", null, null);
                }

                if (result == 0)
                {
                    RadWindowManager1.RadAlert("Số lượng hàng tồn kho không đủ, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                }

                if (result == 2)
                {
                    RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin nhân viên, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                }

                if (result == 3)
                {
                    RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin khách hàng, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                }

                if (result == 4)
                {
                    RadWindowManager1.RadAlert("Ngày xuất kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                }*/

                // RadWindowManager1.RadAlert("Ngày xuất kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
            }

            //Ghi nhan cong no va XAC NHAN DON HANG
            if (e.CommandName == "RecordDebtAndConfirm")
            {
                string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["saleout_id"].ToString();

                //Ham tra nguyen don hang, sau khi tra xong, se return ket qua kieu Int 
                int result = WKS.DMS.WEB.Libs.clsProcessOrder.Order_Confirmed(id, Session["userid"].ToString());
                System.Diagnostics.Debug.WriteLine("this is result : " + result);
                
               
                //=1 tra nguyen don thanh cong
                if (result == 1)
                {
                    try
                    {
                        //Cap nhat lại trang thai don hang sau khi qua phase 2
                        string sQuery = "update saleout_header set status_id = 0 where saleout_id={0}";
                        sQuery = string.Format(sQuery, id);
                        SqlHelper.ExecuteNonQuery(clsCommon.strCon, CommandType.Text, sQuery);
                        ReloadGrid();

                        RadWindowManager1.RadAlert("Đơn hàng đã được xác nhận !", 330, 180, "Thông báo", null, null);
                    }
                    catch (Exception ex)
                    {
                        RadWindowManager1.RadAlert("Cập nhật trạng thái đơn hàng bị lỗi, Vui lòng liên hệ IT để hỗ trợ !", 330, 180, "Thông báo", null, null);
                    }
                }

                //Hang ton kho ko du
                if (result == 0)
                {
                    RadWindowManager1.RadAlert("Số lượng hàng tồn kho không đủ, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                }

                // Don hang tra thieu thong tin nhan vien
                if (result == 2)
                {
                    RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin nhân viên, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                }

                if (result == 3)
                {
                    RadWindowManager1.RadAlert("Đơn hàng thiếu thông tin khách hàng, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                }

                if (result == 4)
                {
                    RadWindowManager1.RadAlert("Ngày xuất kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
                }

                // RadWindowManager1.RadAlert("Ngày xuất kho vượt quá thời gian cho phép, vui lòng xem lại!", 330, 180, "Thông báo", null, null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //_saleout_edit_url = DMS.WEB.Libs.clsProcessOrder.Order_GetUrlForm();

                //Kenh GT
                if(Session["channel_dist_id"].ToString().Equals("1"))
                {
                    btnNewOrder.PostBackUrl = clsCommon.UrlRoot + "Forms/saleout-edit-2.aspx";
                }

                if (Session["channel_dist_id"].ToString().Equals("2"))
                {
                    btnNewOrder.PostBackUrl = clsCommon.UrlRoot + "Forms/saleout-edit-mt.aspx";
                }

            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            ReloadGrid();
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