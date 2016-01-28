using System;
using XBase.Model.Office.SupplyChain;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

using System.IO;
namespace XBase.Data.Office.DefManager
{
    public class CustomModuleDBHelper
    {
        public static DataTable GetDataTableList(XBase.Common.UserInfoUtil UserInfo)
        {
            SqlParameter[] param = {
                                        new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)
                                    };
            param[0].Value = UserInfo.CompanyCD;
            DataTable dt = SqlHelper.ExecuteSql("select * from defdba.CustomModule where CompanyCD=@CompanyCD", param);
            if (dt != null && dt.Rows.Count > 0)
            {
                int dtnum = dt.Rows.Count;
                for (int i = 0; i < dtnum; i++)
                {
                    string[] arr = dt.Rows[i]["userdUserList"].ToString().Split(',');
                    if (dt.Rows[i]["userdUserList"].ToString().TrimEnd(',').Length < 1)
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = 0; j < arr.Length; j++)
                        {
                            if (UserInfo.EmployeeID.ToString() == arr[j])
                            {
                                break;
                            }

                            if (j == arr.Length - 1)
                            {
                                dt.Rows[i].Delete();
                            }
                        }
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }
    }
}
