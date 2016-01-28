using System.Collections;
using System.Data;
using System.Data.SqlClient;


using XBase.Data.DBHelper;
using XBase.Data.Personal.Note;
using XBase.Model.Personal.Note;
using XBase.Common;
using System;
using System.Collections.Generic;

namespace XBase.Business.Personal.Note
{
    public class NoteBus
    {
        private readonly XBase.Data.Personal.Note.NoteDBHelper dal = new XBase.Data.Personal.Note.NoteDBHelper();
        public NoteBus()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.Personal.Note.NoteInfoModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.Personal.Note.NoteInfoModel model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public void Delete(string where)
        {
            dal.Delete(where);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.Personal.Note.NoteInfoModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetAllList()
        {
            return GetList("");
        }


        /// <summary>
        /// GetPageData
        /// </summary>    
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="orderExp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public int GetPageData(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return dal.GetPageData(out dt, where, fields, orderExp, pageindex, pagesize);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #region 获取当前人前一次日志 填写的可查看人员
        /// <summary>
        /// 获取当前人前一次日志 填写的可查看人员 
        /// 2010-5-25 add by hexw
        /// </summary>
        /// <param name="curEmployeeID">当前人ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public DataTable GetLastestCanViewUser(string curEmployeeID, string strCompanyCD)
        {
            return dal.GetLastestCanViewUser(curEmployeeID, strCompanyCD);
        }
        #endregion

        #endregion  成员方法


    }

}



/**********************************************
 * 类作用   日志表业务处理层
 * 创建人   xz
 * 创建时间 2010-5-26 14:23:04 
 ***********************************************/


namespace XBase.Business.Personal.Note
{
    /// <summary>
    /// 日志表业务类
    /// </summary>
    public class PersonalNoteBus
    {
        #region 默认方法
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return PersonalNoteDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>

        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(int iD)
        {
            return PersonalNoteDBHelper.SelectWithKey(iD);
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
            , PersonalNoteModel model)
        {
            return PersonalNoteDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, model);
        }

        /// <summary>
        /// 插入数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>插入数据是否成功:true成功,false不成功</returns>
        public static bool Insert(PersonalNoteModel model)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = PersonalNoteDBHelper.InsertCommand(model);

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
        public static bool Update(PersonalNoteModel model)
        {
            return PersonalNoteDBHelper.Update(model);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            return PersonalNoteDBHelper.Delete(iD);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(List<int> list)
        {
            ArrayList sqlList = new ArrayList();

            foreach (int id in list)
            {
                sqlList.Add(PersonalNoteReportDBHelper.DeleteCommandWithKey(id));
            }

            return SqlHelper.ExecuteTransWithArrayList(sqlList);
        }

        #endregion

        #region 自定义

        /// <summary>
        /// 判断今天是否已经写日志
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>true:存在;false:不存在</returns>
        public static bool ExisitTodayNote(PersonalNoteModel model)
        {
            return PersonalNoteDBHelper.ExisitTodayNote(model);
        }

        /// <summary>
        /// 根据日期获得汇报任务列表
        /// </summary>
        /// <param name="reportDate">时间</param>
        /// <param name="isNew">是否是新增</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public static DataTable GetAllReport(DateTime reportDate, bool isNew, UserInfoUtil userInfo)
        {
            return PersonalNoteDBHelper.GetAllReport(reportDate, isNew, userInfo);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">日志实体类</param>
        /// <param name="list">汇报集合</param>
        /// <returns></returns>
        public static bool SaveData(PersonalNoteModel model, IList<PersonalNoteReportModel> list)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = null;

            #region 日志

            if (model.ID.HasValue)
            {// 编号存在更新
                sqlList.Add(PersonalNoteDBHelper.UpdateCommand(model));
            }
            else
            {// 编号不存在保存
                cmd = PersonalNoteDBHelper.InsertCommand(model);
                sqlList.Add(cmd);
            }

            #endregion

            #region 汇报信息

            foreach (PersonalNoteReportModel item in list)
            {
                if (item.ID.HasValue)
                {
                    sqlList.Add(PersonalNoteReportDBHelper.UpdateCommand(item));
                }
                else
                {
                    sqlList.Add(PersonalNoteReportDBHelper.InsertCommand(item));
                }
            }

            #endregion

            // 保存数据
            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                if (!model.ID.HasValue)
                {
                    int i = 0;
                    if (int.TryParse(cmd.Parameters["@IndexID"].Value.ToString(), out i))
                    {
                        model.ID = i;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}



