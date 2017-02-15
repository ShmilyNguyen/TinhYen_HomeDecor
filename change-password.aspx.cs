using System;
using System.Data;
using System.Data.SqlClient;

namespace WKS.DMS.WEB
{
    public partial class change_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RadCaptcha1.IsValid)
                {
                    RadWindowManager1.RadAlert("Mã bảo vệ không đúng, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập Username !", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword_Old.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập Password cũ !", 330, 180, "Thông báo", null, null);
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword_New1.Text.Trim()) || string.IsNullOrEmpty(txtPassword_New2.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Vui lòng nhập Password mới !", 330, 180, "Thông báo", null, null);
                    return;
                }

                string sQuery = "";
                //Kiem tra tai khoan co ton tai hay ko
                sQuery = @" IF EXISTS ( SELECT employee_id
                                         FROM   dbo.employee
                                         WHERE  username = N'{0}'
                                                AND password = N'{1}' )
                                SELECT  'true' AS result
                             ELSE
                                SELECT  'false' AS result";
                sQuery = string.Format(sQuery, txtUserName.Text.Trim(), txtPassword_Old.Text.Trim());
                string flag = "0";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(sQuery, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    flag = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();
                }
                if (!bool.Parse(flag))
                {
                    RadWindowManager1.RadAlert("Tài khoản cũ không đúng !", 330, 180, "Thông báo", null, null);
                    return;
                }


                //Kiem tra 2 mat khau co trung nhau
                if (string.IsNullOrEmpty(txtPassword_New1.Text.Trim()) || string.IsNullOrEmpty(txtPassword_New2.Text.Trim()))
                {
                    RadWindowManager1.RadAlert("Mật khẩu mới bị trống, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                    return;
                }


                if (!txtPassword_New1.Text.Equals(txtPassword_New2.Text))
                {
                    RadWindowManager1.RadAlert("Mật khẩu mới không khớp nhau, vui lòng kiểm tra lại !", 330, 180, "Thông báo", null, null);
                    return;
                }

                //Tien hanh cap nhat thong tin tai khoan moi

                sQuery = @"UPDATE  dbo.employee
                            SET     password = N'{0}'
                            WHERE   username = N'{1}'";

                sQuery = string.Format(sQuery, txtPassword_New1.Text.Trim(),txtUserName.Text.Trim());
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(sQuery, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                //changedpass

                Response.Redirect("confirmation.aspx?p=changedpass");

            }
            catch (Exception ex)
            {
                RadWindowManager1.RadAlert("Không thể đổi thông tin tài khoản!", 330, 180, "Thông báo", null, null);
            }
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(clsCommon.UrlRoot + "/login.aspx");
        }
    }
}