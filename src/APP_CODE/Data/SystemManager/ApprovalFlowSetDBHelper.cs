using System;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SystemManager;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.SystemManager
{
    /// <summary>
    /// 类名：UserInfoDBHelper
    /// 描述：系统管理数据库层处理
    /// 
    /// 作者：陶春
    /// 创建时间：2009/03/27
    /// 最后修改时间：
    /// </summary>
    ///
    public class ApprovalFlowSetDBHelper
    {
        /// <summary>
        /// 根据分类标志获取分类信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetBillTypeByTypeFlag(string TypeFlag)
        {
            //SQL拼写
            if (TypeFlag != "0")
            {
                string sql = "select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=@TypeFlag and a.TypeLabel='0'";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
                return SqlHelper.ExecuteSql(sql, param);
            }
            else
            {
                string sql = "select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=@TypeFlag and a.TypeLabel='0'";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
                return SqlHelper.ExecuteSql(sql, param);
            }
        }
        /// <summary>
        /// 流程操作
        /// </summary>
        /// <param name="TypeFlag"></param>
        /// <returns></returns>
        public static DataTable GetBillFlowTypeByTypeFlag(string TypeFlag)
        {
            //SQL拼写
            if (TypeFlag != "0")
            {
                string sql = "select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=@TypeFlag and a.TypeLabel='0' and a.AuditFlag='1'";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
                return SqlHelper.ExecuteSql(sql, param);
            }
            else
            {
                string sql = "select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=@TypeFlag and a.TypeLabel='0'";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
                return SqlHelper.ExecuteSql(sql, param);
            }
        }
     
        public static DataTable GetCodePublicByTypeFlag(string TypeFlag)
        {
            string sql = "select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=@TypeFlag and a.TypeLabel='1'";
            if (TypeFlag == "5")
            {
                sql += " union all ";
                sql += " select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=1 and a.TypeCode=4 and a.TypeLabel='1'";
            }
            if (TypeFlag == "1")
            {
                sql += " union all ";
                sql += " select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=5 and a.TypeCode=6 and a.TypeLabel='1'";
            }
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
            return SqlHelper.ExecuteSql(sql, param);
        }

        /// <summary>
        /// 获取所有的单据类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBillTypeByTypeFlag()
        {
            //SQL拼写
            string sql = "select distinct a.TypeFlag,a.ModuleName,a.UsedStatus from pubdba.BillType as a ";
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 加载树信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBillTypeByType()
        {
            //SQL拼写
            string sql = "select distinct a.TypeFlag,a.ModuleName,a.UsedStatus from pubdba.BillType as a where a.TypeFlag!='0'and typelabel='0' ";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetBillTypeByType(string TypeFlag)
        {
            //SQL拼写
            string sql = "select a.TypeFlag,a.TypeCode,a.TypeName,a.ModuleName from pubdba.BillType as a  where a.TypeFlag=@TypeFlag and  typelabel='0'";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
            return SqlHelper.ExecuteSql(sql, param);
        }

        //public static DataTable GetBillTypeByType()
        //{
        //    //SQL拼写
        //    string sql = "select distinct a.TypeFlag,a.ModuleName,a.UsedStatus from pubdba.BillType as a,officedba.Flow as b where a.TypeFlag=b.BillTypeFlag and a.TypeCode=b.BillTypeCode ";
        //    return SqlHelper.ExecuteSql(sql);
        //}
        /// <summary>
        /// 获取需要操作的TypeCode
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBillTypeByTypeCode(string TypeFlag)
        {
            //SQL拼写
            string sql = "select distinct ID, TypeCode,TypeName,UsedStatus from pubdba.BillType where AuditFlag='1' and TypeFlag=@TypeFlag";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@TypeFlag", TypeFlag);
            return SqlHelper.ExecuteSql(sql, param);
        }

        ///
      



      }
  }


