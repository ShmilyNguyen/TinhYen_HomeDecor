using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Web;

namespace WKS.DMS.WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //if (txtUserName.Text.Equals("admin") && txtPassword.Text.Equals("admin@123"))
            //{
            //    Session["userid"] = 1;
            //    Session["username"] = txtUserName.Text.ToLower();
            //    Response.Redirect("/Default.aspx");
            //}

            try
            {
                string username = txtUserName.Text.Trim();
                string password = txtPassword.Text.Trim();
                string sQuery = @"";
                string sQuery2 = @"";


                sQuery = @"SELECT  ISNULL(employee_id,'') as employee_id,
                                ISNULL(position,'') as position,
                                ISNULL(role,'') as role,
                                ISNULL(username,'') as username ,
                                ISNULL(b.channel_dist_id, 0) AS channel_dist_id,
								group_id , 
                                b.store_id, 
                                a.parent_id
                               
								

                            FROM   ( ( SELECT    ISNULL(employee_id, '') AS employee_id ,
                                                ISNULL(position, '') AS position ,
                                                ISNULL(role, '') AS role ,
                                                ISNULL(username, '') AS username ,
                                                ISNULL(store_id, 0) AS store_id,
												ISNULL(group_id,'') as group_id,
                                                parent_id
                                    

                                      FROM      employee WHERE username='{0}' and password='{1}'
                                    ) AS a
                                    LEFT JOIN dbo.store AS b ON a.store_id = b.store_id ) 
						";

                



                        sQuery = string.Format(sQuery,username.ToLower(),password.ToLower());
                DataSet ds = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow r = ds.Tables[0].Rows[0];
                        Session["userid"] = r["employee_id"].ToString();
                        Session["position"] = r["position"].ToString();
                        Session["role"] = r["role"].ToString();
                        Session["username"] = txtUserName.Text.ToLower();
                        Session["channel_dist_id"] = r["channel_dist_id"].ToString();
                        Session["group_id"] = r["group_id"].ToString();
                        Session["store_id"] = r["store_id"].ToString();
                        string store_id = r["store_id"].ToString();
                        Session["parent_id"] = r["parent_id"].ToString();
                       


                        //get channel_dist_id from store_id
                        sQuery2 = @"select  channel_dist_id from dbo.[store] where store_id = '{0}' ";
                        sQuery2 = string.Format(sQuery2, r["store_id"].ToString().Trim());
                        DataSet ds2 = SqlHelper.ExecuteDataset(clsCommon.strCon, CommandType.Text, sQuery2);
                        if (ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                DataRow r2 = ds2.Tables[0].Rows[0];
                                Session["channel_dist_id"] = r2["channel_dist_id"].ToString();

                            }
                        }

                        clsCommon.logger.Info(username + " - " + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + " : " + Request.ServerVariables["URL"]);


                        clsCommon.logger.Error(Session["username"] +  "-" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]  + "-" + Request.ServerVariables["URL"] +  "-" + "Login" );


                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                clsCommon.logger.Error(Session["username"] +  "-" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]  + "-" + Request.ServerVariables["URL"] +  "-" + ex.Message );
            }
        }
    }
}