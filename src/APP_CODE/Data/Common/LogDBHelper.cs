/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.03.06
 * 描    述： 日志内容登陆数据库
 * 修改日期： 2009.03.06
 * 版    本： 0.5.0
 ***********************************************/
using System;
using XBase.Model.Common;
using XBase.Data.DBHelper;
using System.Text;
using System.Data.SqlClient;

namespace XBase.Data.Common
{

    /// <summary>
    /// 类名：LogDBHelper
    /// 描述：日志内容登陆数据库
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/06
    /// 最后修改时间：2009/03/06
    /// </summary>
    ///
    public class LogDBHelper
    {

         #region 登陆日志内容
        /// <summary>
        /// 登陆日志内容
        /// </summary>
        /// <param name="model">日志内容</param>
        /// <returns>DataTable 部门信息</returns>
        public static bool InsertLog(LogInfoModel model)
        {
            //定义插入SQL变量
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.AppendLine("INSERT INTO officedba.ProcessLog ");
            sqlInsert.AppendLine("           (CompanyCD, UserID    ");
            sqlInsert.AppendLine("		   	  , ModuleID            ");
            sqlInsert.AppendLine("           , ObjectID, ObjectName");
            sqlInsert.AppendLine("           , Element, Remark)    ");
            sqlInsert.AppendLine("     VALUES                      ");
            sqlInsert.AppendLine("           (@CompanyCD           ");
            sqlInsert.AppendLine("           ,@UserID              ");
            sqlInsert.AppendLine("           ,@ModuleID            ");
            sqlInsert.AppendLine("           ,@ObjectID            ");
            sqlInsert.AppendLine("           ,@ObjectName          ");
            sqlInsert.AppendLine("           ,@Element             ");
            sqlInsert.AppendLine("           ,@Remark)             ");

            //设置参数
            SqlParameter[] param = new SqlParameter[7];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            //操作用户ID
            param[i++] = SqlHelper.GetParameter("@UserID", model.UserID);
            //操作模块ID
            param[i++] = SqlHelper.GetParameter("@ModuleID", model.ModuleID);
            //操作单据编号
            param[i++] = SqlHelper.GetParameter("@ObjectID", model.ObjectID);
            //操作对象
            param[i++] = SqlHelper.GetParameter("@ObjectName", model.ObjectName);
            //涉及关键元素
            param[i++] = SqlHelper.GetParameter("@Element", model.Element);
            //备注
            param[i++] = SqlHelper.GetParameter("@Remark", model.Remark);

            //执行插入
            return SqlHelper.ExecuteTransSql(sqlInsert.ToString(), param) > 0 ? true : false;
        }
        #endregion

    }
}
