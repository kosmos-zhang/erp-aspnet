using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.DefManager;
using XBase.Model.Office.DefManager;

namespace XBase.Business.Office.DefManager
{
    public class CreateTableViewModelBus
    {
        #region 根据公司编码获取自定义格式表
        /// <summary>
        /// 根据公司编码获取自定义格式表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetDefTableNameList(string strCompanyCD)
        {
            return CreateTableViewModelDBHelper.GetDefTableNameList(strCompanyCD);
        }
        #endregion

        #region 根据表ID获取表字段列表
        /// <summary>
        /// 根据表ID获取表字段列表
        /// </summary>
        /// <param name="tbID">表ID</param>
        /// <returns></returns>
        public static DataTable GetTableFieldsList(string tbID)
        {
            return CreateTableViewModelDBHelper.GetTableFieldsList(tbID);
        }
        #endregion

        #region 保存模板
        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="tbModel">ModuleTableModel模板实体</param>
        /// <param name="strMsg"></param>
        public static int SaveTableModel(ModuleTableModel tbModel, out string strMsg)
        {
           return  CreateTableViewModelDBHelper.SaveTableModel(tbModel, out strMsg);
        }
        #endregion

        #region 修改模板
        /// <summary>
        ///  修改模板
        /// </summary>
        /// <param name="tbModel">ModuleTableModel模板实体</param>
        /// <param name="strMsg"></param>
        public static bool UpdateTableModel(ModuleTableModel tbModel, out string strMsg)
        {
            return CreateTableViewModelDBHelper.UpdateTableModel(tbModel, out strMsg);
        }
        #endregion

        #region 根据条件获取模板列表
        /// <summary>
        /// 根据条件获取模板列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetTableViewModelList(ModuleTableModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return CreateTableViewModelDBHelper.GetTableViewModelList(model, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据模板ID获取模板信息
        /// <summary>
        /// 根据模板ID获取模板信息
        /// </summary>
        /// <param name="tbModuleID">模板ID</param>
        /// <returns>datata模板信息</returns>
        public static DataTable GetTBModuleInfo(string tbModuleID)
        {
            return CreateTableViewModelDBHelper.GetTBModuleInfo(tbModuleID);
        }
        #endregion

        #region 根据公司和表ID获取其子表列表(启用状态下的)
        /// <summary>
        /// 根据公司和表ID获取其子表列表(启用状态下的)
        /// </summary>
        /// <param name="tbID">表ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubTableNameList(string tbID, string strCompanyCD)
        {
            return CreateTableViewModelDBHelper.GetSubTableNameList(tbID, strCompanyCD);
        }
        #endregion

        #region 获取可查看菜单人员
        /// <summary>
        /// 获取可查看菜单人员
        /// </summary>
        /// <param name="proptype">菜单列表中的指定串和表ID组成的字符串</param>
        /// <returns>可查看菜单人员ID串和名字串的datatable</returns>
        public static DataTable GetCanViewMenuUser(string proptype)
        {
            return CreateTableViewModelDBHelper.GetCanViewMenuUser(proptype);
        }
        #endregion

        public static string GetTableTotalFlag(string companyCD,string tablename)
        {
            return CreateTableViewModelDBHelper.GetTableTotalFlag(companyCD, tablename);
        }

        #region 删除模板
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="tbIDStr"></param>
        /// <returns></returns>
        public static bool DelTableModule(string tbIDStr, out string strMsg, out string strFieldText)
        {
            return CreateTableViewModelDBHelper.DelTableModule(tbIDStr, out strMsg, out strFieldText);
        }
        #endregion
    }
}
