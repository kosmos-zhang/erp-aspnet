using System;
using System.Data;
using System.Text;

namespace XBase.Business.KnowledgeCenter
{
	/// <summary>
	/// 业务
	/// </summary>
	public class MyCollector
	{
		private static readonly XBase.Data.KnowledgeCenter.MyCollector  dal = new XBase.Data.KnowledgeCenter.MyCollector();
		public bool Create(XBase.Model.KnowledgeCenter.MyCollector entity)
		{
			return dal.Create(entity);
		}


		public int UpdateByID(XBase.Model.KnowledgeCenter.MyCollector entity)
		{
			return dal.UpdateByID(entity);
		}


		public int DeleteByID(int iD)
		{
			return dal.DeleteByID(iD);
		}


		public XBase.Model.KnowledgeCenter.MyCollector GetEntityByID(int iD)
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

        public static int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            return dal.GetPageData(out tb, PageSize, PageIndex, queryCondition, sortExp, fieldList);
        }

	}
}