using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;

namespace XBase.Business.Office.SupplyChain
{
    public class TableExtFieldsBus
    {
        public static bool Add(TableExtFields tableExtFields, out string strMsg, string CustomValues)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = TableExtFieldsDBHelper.Add(tableExtFields, out strMsg, CustomValues);
                //设置操作成功标识
                remark = "成功";

            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, "2081306");
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                strMsg = "保存失败，请联系系统管理员！";
            }
            //SellLogCommon.InsertLog(tableExtFields.EFDesc, "2081306", "officedba.TableExtFields", remark, "添加");
            return isSuc;
        }

        public static bool Update(TableExtFields tableExtFields, out string strMsg, string CustomValues)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = TableExtFieldsDBHelper.Update(tableExtFields, out strMsg, CustomValues);
                //设置操作成功标识
                remark = "成功";

            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, "2081306");
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                strMsg = "保存失败，请联系系统管理员！";
            }
            //SellLogCommon.InsertLog(tableExtFields.EFDesc, "2081306", "officedba.TableExtFields", remark, "更新");
            return isSuc;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable Getlist(string FunctionModule,string FormType,string CompanyCD, string BranchID, string EFDesc, string ModelNo, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return TableExtFieldsDBHelper.Getlist(FunctionModule, FormType, CompanyCD, BranchID, EFDesc, ModelNo, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 初始化商品档案页面获取所有字段
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllList(string CompanyCD, string BranchID, string TableName)
        {
            return TableExtFieldsDBHelper.GetAllList(CompanyCD, BranchID, TableName);
        }

        /// <summary>
        /// 编辑查看
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDataByNo(string CompanyCD, string BranchID, string ModelNo)
        {
            return TableExtFieldsDBHelper.GetDataByNo(CompanyCD, BranchID, ModelNo);
        }
        /// <summary>
        /// 编辑查看
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable IsHaveRepeatNo(string CompanyCD, string BranchID, string ModelNo)
        {
            return TableExtFieldsDBHelper.IsHaveRepeatNo(CompanyCD, BranchID, ModelNo);
        }
        /// <summary>
        /// 编辑查看
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable IsHaveAttr(string CompanyCD, string BranchID, string TableName)
        {
            return TableExtFieldsDBHelper.IsHaveAttr(CompanyCD, BranchID, TableName);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strCompanyCD"></param>
        /// <param name="IDS">id列表</param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool Delete(string strCompanyCD, string IDS, string BranchID, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strFieldText = "";
            strMsg = "";
            try
            {
                isSucc = TableExtFieldsDBHelper.Delete(strCompanyCD, IDS, BranchID, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, "2081306");
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                strMsg = "删除失败，请联系系统管理员！";
            }
            string[] orderNoS = null;
            orderNoS = IDS.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], "2081306", "officedba.TableExtFields", remark, "删除");
            }


            return isSucc;
        }
    }
}
