using System;
using System.Data;
using System.Text;

namespace XBase.Business.KnowledgeCenter
{
	/// <summary>
	/// 业务
	/// </summary>
	public class KnowledgeWarehouse
	{
		private static readonly XBase.Data.KnowledgeCenter.KnowledgeWarehouse  dal = new XBase.Data.KnowledgeCenter.KnowledgeWarehouse();
		public bool Create(XBase.Model.KnowledgeCenter.KnowledgeWarehouse entity)
		{
			return dal.Create(entity);
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.KnowledgeWarehouse entity)
		{
			return dal.UpdateByID(entity);
		}


		public int DeleteByID(int iD)
		{
			return dal.DeleteByID(iD);
		}


		public XBase.Model.KnowledgeCenter.KnowledgeWarehouse GetEntityByID(int iD)
		{
			return dal.GetEntityByID(iD);
		}


		public DataSet Select()
		{
			return dal.Select();
		}


		public DataSet SelectWhereOrderedEx(string Where,string OrderExp,string Fields)
		{
			return dal.SelectWhereOrderedEx(Where,OrderExp,Fields);
		}


		public DataSet SelectWhereOrderedTopNEx(string Where,string OrderExp,int Num,string Fields)
		{
			return dal.SelectWhereOrderedTopNEx(Where,OrderExp,Num,Fields);
		}


		public int GetCount(string where)
		{
			return dal.GetCount(where);
		}

         /// <summary>
        /// 获取知识库的分页数据
        /// </summary>
        /// <param name="tb">输出的数据表</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="queryCondition">查询条件</param>
        /// <param name="sortExp">排序表达式如： ID ASC</param>
        /// <param name="fieldList">查询的字段列表</param>
        /// <returns>记录总数</returns>
        public static int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            return dal.GetPageData(out tb, PageSize, PageIndex, queryCondition, sortExp, fieldList);
        }


        /// <summary>
        /// 获取知识库的分页数据2
        /// </summary>
        /// <param name="tb">输出的数据表</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="queryCondition">查询条件</param>
        /// <param name="sortExp">排序表达式如： ID ASC</param>
        /// <param name="fieldList">查询的字段列表</param>
        /// <returns>记录总数</returns>
        public static int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList,int maxCount)
        {
            return dal.GetPageData(out tb, PageSize, PageIndex, queryCondition, sortExp, fieldList, maxCount);
        }
	}
}