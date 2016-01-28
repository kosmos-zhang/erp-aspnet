using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.Office.DefManager;
using XBase.Model.Office.DefManager;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Business.Office.DefManager
{
    public class DefFormBus
    {

        /// <summary>
        /// 添加自定义表单
        /// </summary>
        /// <returns></returns>
        public static int AddTable(CustomTableModel model, List<StructTable> sonModel, out string strMsg)
        {
            return DefFormDBHelper.AddTable(model, sonModel, out strMsg);
        }

        /// <summary>
        /// 修改自定义表单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sonModel"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool EditTable(CustomTableModel model, List<StructTable> sonModel, out string strMsg)
        {
            return DefFormDBHelper.EditTable(model, sonModel, out strMsg);
        }

        /// <summary>
        /// 删除自定义表单
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="tableName">物理表名</param>
        /// <param name="CompanyCD">机构代码</param>
        public static void DelTable(string id, string tableName, string CompanyCD)
        {
            DefFormDBHelper.DelTable(id, tableName, CompanyCD);
        }

        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <param name="AliasTableName"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetTableList(string AliasTableName, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return DefFormDBHelper.GetTableList(AliasTableName, CompanyCD, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 根据ID自定义表单明细
        /// </summary>
        /// <param name="TableId"></param>
        /// <returns></returns>
        public static DataSet GetTableById(string TableId)
        {
            return DefFormDBHelper.GetTableById(TableId);
        }

        /// <summary>
        /// 初始化表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool UpdateStruct(IList<StructTable> list)
        {
            return DefFormDBHelper.UpdateStruct(list);
        }

        /// <summary>
        /// 获得字典表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDictionary(string CompanyCD)
        {
            return DefFormDBHelper.GetDictionary(CompanyCD);
        }

        /// <summary>
        /// 获得绑定数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetBindData(string sql)
        {
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 表数据初始化
        /// </summary>
        /// <param name="TableID"></param>
        public static void InitTable(string TableID, string CompanyCD)
        {
            DefFormDBHelper.InitTable(TableID, CompanyCD);
        }

        /// <summary>
        /// 根据表名判断表中是否存在数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool ExistHasData(string tableName)
        {
            return DefFormDBHelper.ExistHasData(tableName);
        }

        public static void AddReguler(string Relation, int ColumnID, int TableId, int summaryFlag)
        {
            DefFormDBHelper.AddReguler(Relation, ColumnID, TableId, summaryFlag);
        }

        /// <summary>
        /// 更新表达式
        /// </summary>
        public static int ModReguler(string Relation, int ColumnID, int summaryFlag)
        {
            return DefFormDBHelper.ModReguler(Relation, ColumnID, summaryFlag);
        }

        public static int CreateMenu(string tableid,string userlist)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            return DefFormDBHelper.CreateMenu(tableid, userInfo,userlist);
        }

        /// <summary>
        /// 获得表达式
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetRegular(int ColumnID)
        {
            return DefFormDBHelper.GetRegular(ColumnID);
        }

        /// <summary>
        /// 获取从属表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetParentDT(string CompanyCD)
        {
            return DefFormDBHelper.GetParentDT(CompanyCD);
        }

    }
}
