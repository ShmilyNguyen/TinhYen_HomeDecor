using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WKS.DMS.WEB.Libs
{
    public class clsCodeMaster
    {
        public static void GenCode(string CodeName,string UserID,out string ID,out string Code)
        {

            try
            {
                ID = "";
                Code = "";

                string storeProc = "usp_Sys_Gen_Code";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(UserID))
                    {
                        SqlParameter p_CodeName = new SqlParameter("@CodeName", SqlDbType.VarChar);
                        p_CodeName.Size = 20;
                        p_CodeName.Value = CodeName;
                        p_CodeName.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(p_CodeName);


                        SqlParameter p_UserID = new SqlParameter("@UserID", SqlDbType.BigInt);
                        p_UserID.Value = UserID;
                        p_UserID.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(p_UserID);

                        SqlParameter p_Code = new SqlParameter("@Code", SqlDbType.VarChar);
                        p_Code.Size = 50;
                        p_Code.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p_Code);

                        SqlParameter p_ID = new SqlParameter("@ID", SqlDbType.BigInt);                        
                        p_ID.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p_ID);


                        conn.Open();
                        cmd.ExecuteNonQuery();                        
                        conn.Close();

                        ID = cmd.Parameters["@ID"].Value.ToString();
                        Code = cmd.Parameters["@Code"].Value.ToString();



                        return;
                    }
                }
            }
            catch (Exception ex)
            {
               
 
               
            }

            ID = "";
            Code = "";
        }

        public static void GenCode_WithStoreID(string store_id,string CodeName, string UserID, out string ID, out string Code)
        {

            try
            {
                ID = "";
                Code = "";

                string storeProc = "usp_Sys_Gen_Code_With_StoreID";
                using (SqlConnection conn = new SqlConnection(clsCommon.strCon))
                {
                    SqlCommand cmd = new SqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(UserID))
                    {

                        SqlParameter p_store_id = new SqlParameter("@store_id", SqlDbType.Int);
                        p_store_id.Value = store_id;
                        p_store_id.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(p_store_id);


                        SqlParameter p_CodeName = new SqlParameter("@CodeName", SqlDbType.VarChar);
                        p_CodeName.Size = 20;
                        p_CodeName.Value = CodeName;
                        p_CodeName.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(p_CodeName);


                        SqlParameter p_UserID = new SqlParameter("@UserID", SqlDbType.BigInt);
                        p_UserID.Value = UserID;
                        p_UserID.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(p_UserID);

                        SqlParameter p_Code = new SqlParameter("@Code", SqlDbType.VarChar);
                        p_Code.Size = 50;
                        p_Code.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p_Code);

                        SqlParameter p_ID = new SqlParameter("@ID", SqlDbType.BigInt);
                        p_ID.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p_ID);


                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        ID = cmd.Parameters["@ID"].Value.ToString();
                        Code = cmd.Parameters["@Code"].Value.ToString();



                        return;
                    }
                }
            }
            catch (Exception ex)
            {



            }

            ID = "";
            Code = "";
        }

    }
}