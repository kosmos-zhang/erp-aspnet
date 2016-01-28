
/**********************************************
 * 类作用   工作日志汇报明细表业务处理层
 * 创建人   xz
 * 创建时间 2010-7-2 15:08:55 
 ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Model.Personal.Note;
using XBase.Data.Personal.Note;


namespace XBase.Business.Personal.Note
{
    /// <summary>
    /// 工作日志汇报明细表业务类
    /// </summary>
    public class PersonalNoteReportBus
    {
        #region 默认方法
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">流水号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return PersonalNoteReportDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="noteNo">日志编号</param>
        /// <param name="reportID">汇报流水号(任务流水号,日程流水号)</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string noteNo, int reportID)
        {
            return PersonalNoteReportDBHelper.SelectWithKey(noteNo, reportID);
        }


        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , PersonalNoteReportModel model)
        {
            return PersonalNoteReportDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, model);
        }

        /// <summary>
        /// 插入数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>插入数据是否成功:true成功,false不成功</returns>
        public static bool Insert(PersonalNoteReportModel model)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = PersonalNoteReportDBHelper.InsertCommand(model);

            sqlList.Add(cmd);

            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                int i = 0;
                if (int.TryParse(cmd.Parameters["@IndexID"].Value.ToString(), out i))
                {
                    model.ID = i;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(PersonalNoteReportModel model)
        {
            return PersonalNoteReportDBHelper.Update(model);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            return PersonalNoteReportDBHelper.Delete(iD);
        }

        #endregion

        #region 自定义
        /// <summary>
        /// 根据日志编号查询数据记录
        /// </summary>
        /// <param name="noteNo">企业代码</param>
        /// <param name="reportID">日志编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithNoteNo(string CompanyCD, string noteNo)
        {
            return PersonalNoteReportDBHelper.SelectWithNoteNo(CompanyCD, noteNo);
        }

        /// <summary>
        /// 根据任务ID获得汇报信息
        /// </summary>
        /// <param name="CompanyCD">企业代码</param>
        /// <param name="ReportID">任务ID</param>
        /// <param name="ReportType">报表类别</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable GetTaskReport(string CompanyCD, int ReportID, int ReportType)
        {
            return PersonalNoteReportDBHelper.GetTaskReport(CompanyCD, ReportID, ReportType);
        }
        #endregion

    }
}



