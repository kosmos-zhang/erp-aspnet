using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Decision
{
    public class DataMySubscribe
    {
        private static readonly XBase.Data.Decision.DataMySubscribe dal = new XBase.Data.Decision.DataMySubscribe();
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public  DataTable GetDataMySubscribeAll(string CompanyCD)
        {   
            DataTable dt=null;
            try
            {
                dt = dal.GetDataMySubscribleALL(CompanyCD).Tables[0];
            }
            catch
            {

            }
            return dt;
        }


        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public IList<XBase.Model.Decision.DataMySubscribe> GetDataMySubscribeListbyCond(string cond, string orderExp)
        {
            return dal.GetDataMySubscribleListbyCond(cond, orderExp);
        }


        public int CheckRecord(int Id)
        {
            return dal.CheckRecord(Id);
        }
        public  XBase.Model.Decision.DataMySubscribe GetDataMySubscribeById(string Id)
        {
            IList<XBase.Model.Decision.DataMySubscribe> _list = GetDataMySubscribeListbyCond("[Id]=" + Id, "[Id]");
            return _list[0];
        }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddDataMySubscribe(XBase.Model.Decision.DataMySubscribe entity,out int Id)
        {
            return dal.AddDataMySubscribe(entity,out Id);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelDataMySubscrible(int id)
        {
            return dal.DelDataMySubscrible(id);
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ModDataMySubscrible(XBase.Model.Decision.DataMySubscribe entity)
        {
            return dal.ModDataMySubscrible(entity);
        }

        public static DataTable GetDataName(string CompanyCD)
        {
            return dal.GetGetDataName(CompanyCD);
        }

        /// <summary>
        /// 根据Id获取产品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DataTable GetProductById(int Id)
        {
            return dal.GetProductById(Id);
        }

        /// <summary>
        /// 根据Id获取部门信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DataTable GetDeptById(int Id)
        {
            return dal.GetDeptById(Id);
        }

        /// <summary>
        /// 获取数据订阅的分页数据
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="queryCondition"></param>
        /// <param name="sortExp"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        public static int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            return dal.GetPageData(out tb, PageSize, PageIndex, queryCondition, sortExp, fieldList);
        }
    }
}
