using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WKS.DMS.WEB
{
    public partial class MasterPageSingleMenu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {



                object objUserID = Session["userid"];

                if (objUserID == null)
                {
                    Response.Redirect(clsCommon.UrlRoot + "Login.aspx");
                }




               
                object objChannel_dist_is = Session["channel_dist_id"].ToString().Trim();

                if (objChannel_dist_is == null || objChannel_dist_is.Equals("") || objChannel_dist_is.Equals("0"))
                {
                    objChannel_dist_is = "1";

                }



                object objGroupID = Session["group_id"].ToString().Trim();
                object objPosition = Session["position"];
                object objRole = Session["role"];

                if (objPosition != null)
                {
                    string position = objPosition.ToString();
                    string role = objRole.ToString();

                    if (position.Equals("ADMIN") && objGroupID.Equals("1") || objGroupID.Equals("1"))
                    {
                        ASPxMenu1.Items.FindByName("menu_system").Visible = true;
                        ASPxMenu1.Items.FindByName("menu_danhmuc").Visible = true;
                        ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = true;
                        ASPxMenu1.Items.FindByName("menu_quanlyctkm").Visible = true;
                        ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = true;
                        ASPxMenu1.Items.FindByName("menu_tondau").Visible = true;
                       

                    }



                    else if (position.Equals("HR") && objGroupID.Equals("9") || objGroupID.Equals("9"))
                    {


                        if (position.Equals("HRCN"))
                        {
                            ASPxMenu1.Items.FindByName("menu_system").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_danhmuc").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_danhmuc_region").Visible = true;
                            ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_quanlyctkm").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_quanlytuyen").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_nhapkho").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_xuatkho").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_donhangduyet").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_phieuthu").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_congno").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_baocao").Visible = false;




                        }
                        else
                        {
                            ASPxMenu1.Items.FindByName("menu_system").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_danhmuc").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_danhmuc2").Visible = true;
                            ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_quanlyctkm").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_quanlytuyen").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_nhapkho").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_xuatkho").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_donhangduyet").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_phieuthu").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_congno").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                            ASPxMenu1.Items.FindByName("menu_baocao").Visible = false;


                        }

                    }





                    //else if (position.Contains("ADMIN-"))
                    //{

                    //    ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = true;
                    //    ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = true;


                    //}



                    else if (position == "SUP" || position == "ASM" || position == "RSM" || position == "ADMIN-REPORT")
                    {
                        ASPxMenu1.Items.FindByName("menu_quanlytuyen").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_nhapkho").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_xuatkho").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_donhangduyet").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_phieuthu").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_congno").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                        if (position.Contains("ADMIN-"))
                        {

                            ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = true;
                            ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = true;


                        }

                    }
                    else if (objChannel_dist_is.Equals("2"))
                    {
                        ASPxMenu1.Items.FindByName("menu_system").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_danhmuc").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_quanlyctkm").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_danhmuc_mt").Visible = true;


                    }

                    else
                    {
                        ASPxMenu1.Items.FindByName("menu_system").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_danhmuc").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_quanlychitieu").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_quanlyctkm").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_route_weekdays").Visible = false;
                        ASPxMenu1.Items.FindByName("menu_tondau").Visible = false;

                        //DevExpress.Web.ASPxMenu.MenuItem item = ASPxMenu1.Items.FindByName("menu_Reports_DangKyTrungBay");
                        //if (item != null)
                        //{
                        //    item.Enabled = false;
                        //}
                    }





                }
            }
            catch (Exception ex)
            {
            
            }


        }
    }
}