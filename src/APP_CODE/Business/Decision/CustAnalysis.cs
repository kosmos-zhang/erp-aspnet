using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Decision
{
	/// <summary>
	/// 业务逻辑类CustAnalysis 的摘要说明。
	/// </summary>
	public class CustAnalysis
	{
        private readonly XBase.Data.Decision.CustAnalysis dal = new XBase.Data.Decision.CustAnalysis();
		public CustAnalysis()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(XBase.Model.Decision.CustAnalysis model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(XBase.Model.Decision.CustAnalysis model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
	    //public void Delete()
		//{
			//该表无主键信息，请自定义主键/条件字段
			//dal.Delete();
		//}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public XBase.Model.Decision.CustAnalysis GetModel(int ID)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(ID);
		}


        public DataTable GetModel(string CustNO, string CompanyCD, string ModelType, string Year, string Month) 
        {
            try
            {
                DataTable dt = dal.GetModel(CustNO, CompanyCD, ModelType, Year, Month).Tables[0];
                DataTable dt1=DataModelExpIt.ExpDataTable(dt, CompanyCD, ModelType,5);
                return dt1;
            }
            catch 
            {
                return null;
            }
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

          /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListDistributing(string strWhere)
        {
            return dal.GetListDistributing(strWhere); 
        }
         /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int topRowsCount, string orderby, string strWhere)
        {
            return dal.GetList(topRowsCount, orderby, strWhere);
        }

         /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListSum(int topRowsCount, string orderby, string strWhere)
        {
            return dal.GetListSum(topRowsCount, orderby, strWhere);
        }
		
        /// <summary>
        /// 客户等级分布_图表
        /// </summary>
        public DataSet GetDistributing(string CompanyCD,string Year,string Month)
        {
            return dal.GetDistributing(CompanyCD,Year,Month);
        }

         public DataTable GetPageData(int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList,ref int TotalCount)
         {
            return dal.GetPageData(PageSize,PageIndex,queryCondition,sortExp,fieldList,ref TotalCount);
         }
        /// <summary>
        /// 添加历史记录
        /// </summary>
         public void AddAnalysisHistory(string CustNO, string CustName, string CustGrade, string GradeDate)
         {
             dal.AddAnalysisHistory(CustNO, CustName, CustGrade, GradeDate);
         }

         /// <summary>
        /// 获取历史记录
        /// </summary>
         public DataSet GetAnalysisHistoryByCustNO(string CustNO)
         {
             return dal.GetAnalysisHistoryByCustNO(CustNO);
         }

         /// <summary>
         /// 获取历史记录
         /// </summary>
         public DataSet GetAnalysisHistoryByCustNO(string CustNO, string GradeDate)
         {
             return dal.GetAnalysisHistoryByCustNO(CustNO,GradeDate);
         }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

	
		#endregion  成员方法
	}
}

