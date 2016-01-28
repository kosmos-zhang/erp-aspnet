using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using XBase.Common;
namespace XBase.Data.Decision
{
	/// <summary>
	/// 数据访问类DataCustAnalysis。
	/// </summary>
	public class CustAnalysis
	{
        public CustAnalysis()
		{}
		#region  成员方法



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(XBase.Model.Decision.CustAnalysis model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into statdba.DataCustAnalysis(");
			strSql.Append("CustNo,CustName,CompanyCD,CreateDate,CompanyType,StaffCount,SetupDate,CapitalScale,SaleroomY,SetupMoney,ArrearageCount,ArrearagePrice,BuyCount,BuyPrice,RefundmentCount,RefundmentPrice,ComplainCount,CustGrade,CustProint)");
			strSql.Append(" values (");
			strSql.Append("@CustNo,@CustName,@CompanyCD,@CreateDate,@CompanyType,@StaffCount,@SetupDate,@CapitalScale,@SaleroomY,@SetupMoney,@ArrearageCount,@ArrearagePrice,@BuyCount,@BuyPrice,@RefundmentCount,@RefundmentPrice,@ComplainCount,@CustGrade,@CustProint)");
			SqlParameter[] parameters = {				
					new SqlParameter("@CustNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,100),
					new SqlParameter("@CompanyCD", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CompanyType", SqlDbType.Int,4),
					new SqlParameter("@StaffCount", SqlDbType.Int,4),
					new SqlParameter("@SetupDate", SqlDbType.Int,4),
					new SqlParameter("@CapitalScale", SqlDbType.Int,4),
					new SqlParameter("@SaleroomY", SqlDbType.Int,4),
					new SqlParameter("@SetupMoney", SqlDbType.Int,4),
					new SqlParameter("@ArrearageCount", SqlDbType.Int,4),
					new SqlParameter("@ArrearagePrice", SqlDbType.Decimal,13),
					new SqlParameter("@BuyCount", SqlDbType.Int,4),
					new SqlParameter("@BuyPrice", SqlDbType.Decimal,13),
                    new SqlParameter("@RefundmentCount", SqlDbType.Int,4),
					new SqlParameter("@RefundmentPrice", SqlDbType.Decimal,13),
					new SqlParameter("@ComplainCount", SqlDbType.Int,4),
					new SqlParameter("@CustGrade", SqlDbType.NVarChar,100),
					new SqlParameter("@CustProint", SqlDbType.Int,4)};
			
			parameters[0].Value = model.CustNo;
			parameters[1].Value = model.CustName;
			parameters[2].Value = model.CompanyCD;
			parameters[3].Value = model.CreateDate;
			parameters[4].Value = model.CompanyType;
			parameters[5].Value = model.StaffCount;
			parameters[6].Value = model.SetupDate;
			parameters[7].Value = model.CapitalScale;
			parameters[8].Value = model.SaleroomY;
			parameters[9].Value = model.SetupMoney;
			parameters[10].Value = model.ArrearageCount;
			parameters[11].Value = model.ArrearagePrice;
			parameters[12].Value = model.BuyCount;
			parameters[13].Value = model.BuyPrice;
			parameters[14].Value = model.RefundmentCount;
			parameters[15].Value = model.RefundmentPrice;
			parameters[16].Value = model.ComplainCount;
			parameters[17].Value = model.CustGrade;
			parameters[18].Value = model.CustProint;

			Database.RunSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(XBase.Model.Decision.CustAnalysis model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update statdba.DataCustAnalysis set ");			
			strSql.Append("CustNo=@CustNo,");
			strSql.Append("CustName=@CustName,");
			strSql.Append("CompanyCD=@CompanyCD,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("CompanyType=@CompanyType,");
			strSql.Append("StaffCount=@StaffCount,");
			strSql.Append("SetupDate=@SetupDate,");
			strSql.Append("CapitalScale=@CapitalScale,");
			strSql.Append("SaleroomY=@SaleroomY,");
			strSql.Append("SetupMoney=@SetupMoney,");
			strSql.Append("ArrearageCount=@ArrearageCount,");
			strSql.Append("ArrearagePrice=@ArrearagePrice,");
			strSql.Append("BuyCount=@BuyCount,");
			strSql.Append("BuyPrice=@BuyPrice,");
			strSql.Append("RefundmentCount=@RefundmentCount,");
			strSql.Append("RefundmentPrice=@RefundmentPrice,");
			strSql.Append("ComplainCount=@ComplainCount,");
			strSql.Append("CustGrade=@CustGrade,");
			strSql.Append("CustProint=@CustProint");
            strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CustNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,100),
					new SqlParameter("@CompanyCD", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CompanyType", SqlDbType.Int,4),
					new SqlParameter("@StaffCount", SqlDbType.Int,4),
					new SqlParameter("@SetupDate", SqlDbType.Int,4),
					new SqlParameter("@CapitalScale", SqlDbType.Int,4),
					new SqlParameter("@SaleroomY", SqlDbType.Int,4),
					new SqlParameter("@SetupMoney", SqlDbType.Int,4),
					new SqlParameter("@ArrearageCount", SqlDbType.Int,4),
					new SqlParameter("@ArrearagePrice", SqlDbType.Decimal,13),
					new SqlParameter("@BuyCount", SqlDbType.Int,4),
					new SqlParameter("@BuyPrice", SqlDbType.Decimal,13),
					new SqlParameter("@RefundmentCount", SqlDbType.Int,4),
					new SqlParameter("@RefundmentPrice", SqlDbType.Decimal,13),
					new SqlParameter("@ComplainCount", SqlDbType.Int,4),
					new SqlParameter("@CustGrade", SqlDbType.NVarChar,100),
					new SqlParameter("@CustProint", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CustNo;
			parameters[2].Value = model.CustName;
			parameters[3].Value = model.CompanyCD;
			parameters[4].Value = model.CreateDate;
			parameters[5].Value = model.CompanyType;
			parameters[6].Value = model.StaffCount;
			parameters[7].Value = model.SetupDate;
			parameters[8].Value = model.CapitalScale;
			parameters[9].Value = model.SaleroomY;
			parameters[10].Value = model.SetupMoney;
			parameters[11].Value = model.ArrearageCount;
			parameters[12].Value = model.ArrearagePrice;
			parameters[13].Value = model.BuyCount;
			parameters[14].Value = model.BuyPrice;
			parameters[15].Value = model.RefundmentCount;
			parameters[16].Value = model.RefundmentPrice;
			parameters[17].Value = model.ComplainCount;
			parameters[18].Value = model.CustGrade;
			parameters[19].Value = model.CustProint;

			Database.RunSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Decision.CustAnalysis GetModel(int ID)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,CustNo,CustName,CompanyCD,CreateDate,CompanyType,StaffCount,SetupDate,CapitalScale,SaleroomY,SetupMoney,ArrearageCount,ArrearagePrice,BuyCount,BuyPrice,RefundmentCount,RefundmentPrice,ComplainCount,CustGrade,CustProint from statdba.DataCustAnalysis ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.Decision.CustAnalysis model = new XBase.Model.Decision.CustAnalysis();
            DataSet ds = Database.RunSql(strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.CustNo=ds.Tables[0].Rows[0]["CustNo"].ToString();
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
				model.CompanyCD=ds.Tables[0].Rows[0]["CompanyCD"].ToString();
				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CompanyType"].ToString()!="")
				{
					model.CompanyType=int.Parse(ds.Tables[0].Rows[0]["CompanyType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StaffCount"].ToString()!="")
				{
					model.StaffCount=int.Parse(ds.Tables[0].Rows[0]["StaffCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SetupDate"].ToString()!="")
				{
					model.SetupDate=int.Parse(ds.Tables[0].Rows[0]["SetupDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CapitalScale"].ToString()!="")
				{
					model.CapitalScale=int.Parse(ds.Tables[0].Rows[0]["CapitalScale"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SaleroomY"].ToString()!="")
				{
					model.SaleroomY=int.Parse(ds.Tables[0].Rows[0]["SaleroomY"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SetupMoney"].ToString()!="")
				{
					model.SetupMoney=int.Parse(ds.Tables[0].Rows[0]["SetupMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ArrearageCount"].ToString()!="")
				{
					model.ArrearageCount=int.Parse(ds.Tables[0].Rows[0]["ArrearageCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ArrearagePrice"].ToString()!="")
				{
					model.ArrearagePrice=decimal.Parse(ds.Tables[0].Rows[0]["ArrearagePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BuyCount"].ToString()!="")
				{
					model.BuyCount=int.Parse(ds.Tables[0].Rows[0]["BuyCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BuyPrice"].ToString()!="")
				{
					model.BuyPrice=decimal.Parse(ds.Tables[0].Rows[0]["BuyPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RefundmentCount"].ToString()!="")
				{
					model.RefundmentCount=int.Parse(ds.Tables[0].Rows[0]["RefundmentCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RefundmentPrice"].ToString()!="")
				{
					model.RefundmentPrice=decimal.Parse(ds.Tables[0].Rows[0]["RefundmentPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ComplainCount"].ToString()!="")
				{
					model.ComplainCount=int.Parse(ds.Tables[0].Rows[0]["ComplainCount"].ToString());
				}
				model.CustGrade=ds.Tables[0].Rows[0]["CustGrade"].ToString();
				if(ds.Tables[0].Rows[0]["CustProint"].ToString()!="")
				{
					model.CustProint=int.Parse(ds.Tables[0].Rows[0]["CustProint"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        public DataSet GetModel(string CustNO, string CompanyCD, string ModelType,string Year,string Month) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append(" min(ID) ID, ");
            strSql.Append(" min(CustNO) CustNO, ");
            strSql.Append(" min(CustName) CustName, ");
            strSql.Append(" min(CompanyCD) CompanyCD, ");
            strSql.Append(" min(CreateDate) CreateDate, ");
            strSql.Append(" min(CompanyType) CompanyType, ");
            strSql.Append(" min(staffCount) staffCount, ");
            strSql.Append(" min(SetupDate) SetupDate, ");
            strSql.Append(" min(CapitalScale) CapitalScale, ");
            strSql.Append(" min(SaleroomY) SaleroomY, ");
            strSql.Append(" min(SetupMoney) SetupMoney, ");
            strSql.Append(" sum(ArrearageCount) ArrearageCount, ");
            strSql.Append(" sum(ArrearagePrice) ArrearagePrice, ");
            strSql.Append(" sum(BuyCount) BuyCount, ");
            strSql.Append(" sum(BuyPrice) BuyPrice, ");
            strSql.Append(" sum(RefundmentCount) RefundmentCount, ");
            strSql.Append(" sum(RefundmentPrice) RefundmentPrice, ");
            strSql.Append(" sum(ComplainCount) ComplainCount, ");
            strSql.Append(" min(CustGrade) CustGrade ");
            strSql.Append(" FROM statdba.DataCustAnalysis ");
            strSql.Append("where 1=1");
            if (CompanyCD != "")
            {
                strSql.Append("and CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("'");
            }

            if (CustNO != "") 
            {
                strSql.Append("and CustNO='");
                strSql.Append(CustNO);
                strSql.Append("'");
            }

            if (Year != "") 
            {
                strSql.Append(" and datepart(yyyy,CreateDate)='");
                strSql.Append(Year);
                strSql.Append("'");
            }

            if (ModelType == "0") 
            {
                strSql.Append(" AND datepart(MM,CreateDate)='");
                strSql.Append(Month);
                strSql.Append("'");
            }

            if (ModelType == "1")
            {
                strSql.Append(" AND datepart(MM,CreateDate)>=");
                strSql.Append(Convert.ToString(Convert.ToInt32(Month)*3-2));
                strSql.Append(" AND datepart(MM,CreateDate)<=");
                strSql.Append(Convert.ToString(Convert.ToInt32(Month) * 3));
            }
            strSql.Append(" group by CustNO ");
            return Database.RunSql(strSql.ToString(),null);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet  GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CustNo,CustName,CompanyCD,CreateDate,CompanyType,StaffCount,SetupDate,CapitalScale,SaleroomY,SetupMoney,ArrearageCount,ArrearagePrice,BuyCount,BuyPrice,RefundmentCount,RefundmentPrice,ComplainCount,CustGrade,CustProint ");
            strSql.Append(" FROM statdba.DataCustAnalysis ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return Database.RunSql(strSql.ToString());
		}

        /// <summary>
        /// 分页处理
        /// </summary>
        public DataSet GetListDistributing(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append(" min(ID) ID, ");
            strSql.Append(" min(CustNO) CustNO, ");
            strSql.Append(" min(CustName) CustName, ");
            strSql.Append(" min(CompanyCD) CompanyCD, ");
            strSql.Append(" min(CreateDate) CreateDate, ");
            strSql.Append(" min(CompanyType) CompanyType, ");
            strSql.Append(" min(staffCount) staffCount, ");
            strSql.Append(" min(SetupDate) SetupDate, ");
            strSql.Append(" min(CapitalScale) CapitalScale, ");
            strSql.Append(" min(SaleroomY) SaleroomY, ");
            strSql.Append(" min(SetupMoney) SetupMoney, ");
            strSql.Append(" sum(ArrearageCount) ArrearageCount, ");
            strSql.Append(" sum(ArrearagePrice) ArrearagePrice, ");
            strSql.Append(" sum(BuyCount) BuyCount, ");
            strSql.Append(" sum(BuyPrice) BuyPrice, ");
            strSql.Append(" sum(RefundmentCount) RefundmentCount, ");
            strSql.Append(" sum(RefundmentPrice) RefundmentPrice, ");
            strSql.Append(" sum(ComplainCount) ComplainCount, ");
            strSql.Append(" min(CustGrade) CustGrade ");
            strSql.Append(" FROM statdba.DataCustAnalysis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" group by CustNO ");
            return Database.RunSql(strSql.ToString());
        }



        /// <summary>
        /// 客户等级分布_图表
        /// </summary>
        public DataSet GetDistributing(string CompanyCD,string Year,string Month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select GName,isnull(GradeCount,0) GradeCount from statdba.DataCustLevel a left join ");
            strSql.Append(" (select count(1) GradeCount,CustGrade from statdba.DataCustAnalysis ");
            strSql.Append("  where CompanyCD='");
            strSql.Append(CompanyCD);
            strSql.Append("' ");
            if (Year != "") 
            {
                strSql.Append(" and datepart(yyyy,CreateDate)='");
                strSql.Append(Year);
                strSql.Append("'");
            }

            if (Month != "")
            {
                strSql.Append(" and datepart(dd,CreateDate)='");
                strSql.Append(Month);
                strSql.Append("'");
            }
            strSql.Append(" group by CustGrade) b  ");
            strSql.Append("  on  a.GName=b.CustGrade  ");
            strSql.Append("  where CompanyCD='");
            strSql.Append(CompanyCD);
            strSql.Append("'");
            return Database.RunSql(strSql.ToString());
        }


        /// <summary>
        /// 分页处理
        /// </summary>
        public DataTable GetPageData(int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList,ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append(" min(ID) ID, ");
            strSql.Append(" min(CustNO) CustNO, ");
            strSql.Append(" min(CustName) CustName, ");
            strSql.Append(" min(CompanyCD) CompanyCD, ");
            strSql.Append(" min(CreateDate) CreateDate, ");
            strSql.Append(" min(CompanyType) CompanyType, ");
            strSql.Append(" min(staffCount) staffCount, ");
            strSql.Append(" min(SetupDate) SetupDate, ");
            strSql.Append(" min(CapitalScale) CapitalScale, ");
            strSql.Append(" min(SaleroomY) SaleroomY, ");
            strSql.Append(" min(SetupMoney) SetupMoney, ");
            strSql.Append(" sum(ArrearageCount) ArrearageCount, ");
            strSql.Append(" sum(ArrearagePrice) ArrearagePrice, ");
            strSql.Append(" sum(BuyCount) BuyCount, ");
            strSql.Append(" sum(BuyPrice) BuyPrice, ");
            strSql.Append(" sum(RefundmentCount) RefundmentCount, ");
            strSql.Append(" sum(RefundmentPrice) RefundmentPrice, ");
            strSql.Append(" sum(ComplainCount) ComplainCount, ");
            strSql.Append(" min(CustGrade) CustGrade ");
            strSql.Append(" FROM statdba.DataCustAnalysis ");
            strSql.Append( queryCondition );
            strSql.Append(" group by CustNO ");
            return XBase.Data.DBHelper.SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), PageIndex, PageSize, sortExp, null, ref TotalCount);
        }

        /// <summary>
        /// 添加历史记录
        /// </summary>
        public void AddAnalysisHistory(string CustNO, string CustName, string CustGrade, string GradeDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into statdba.DataCustAnalysisHistory(");
            strSql.Append("CustNo,CustName,CustGrade,GradeDate)");
            strSql.Append(" values (");
            strSql.Append("@CustNo,@CustName,@CustGrade,@GradeDate)");
            SqlParameter[] parameters = {				
					new SqlParameter("@CustNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,100),
					new SqlParameter("@CustGrade", SqlDbType.NVarChar,100),
					new SqlParameter("@GradeDate", SqlDbType.NVarChar,100)};

            parameters[0].Value = CustNO;
            parameters[1].Value = CustName;
            parameters[2].Value = CustGrade;
            parameters[3].Value = GradeDate;

            Database.RunSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取历史记录
        /// </summary>
        public DataSet GetAnalysisHistoryByCustNO(string CustNO) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from statdba.DataCustAnalysisHistory ");
            strSql.Append("where CustNO='");
            strSql.Append(CustNO);
            strSql.Append("'");
            return Database.RunSql(strSql.ToString(),null);
        }

        /// <summary>
        /// 获取历史记录
        /// </summary>
        public DataSet GetAnalysisHistoryByCustNO(string CustNO,string GradeDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from statdba.DataCustAnalysisHistory ");
            strSql.Append("where CustNO='");
            strSql.Append(CustNO);
            strSql.Append("'");
            strSql.Append("and GradeDate='");
            strSql.Append(GradeDate);
            strSql.Append("'");
            return Database.RunSql(strSql.ToString(), null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int topRowsCount,string orderby,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + topRowsCount.ToString() + " ID,CustNo,CustName,CompanyCD,CreateDate,CompanyType,StaffCount,SetupDate,CapitalScale,SaleroomY,SetupMoney,ArrearageCount,ArrearagePrice,BuyCount,BuyPrice,RefundmentCount,RefundmentPrice,ComplainCount,CustGrade,CustProint ");
            strSql.Append(" FROM statdba.DataCustAnalysis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (orderby.Trim() != "")
            {
                strSql.Append(" Order by " + orderby);
            }

            return Database.RunSql(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListSum(int topRowsCount, string orderby, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + topRowsCount.ToString());
            strSql.Append(" MIN(ID) AS ID,MIN(CustNo) AS CustNo,MIN(CustName) AS CustName,MIN(CompanyCD) AS CompanyCD,MIN(CreateDate) AS CreateDate,MIN(CompanyType) AS CompanyType,MIN(StaffCount) AS StaffCount");
            strSql.Append(",MIN(SetupDate) AS SendDate,MIN(CapitalScale) as CapitalScale,MIN(SaleroomY) AS SaleroomY,MIN(SetupMoney) AS SetupMoney,SUM(ArrearageCount) AS ArrearageCount,SUM(ArrearagePrice) AS ArrearagePrice");
            strSql.Append(",SUM(BuyCount) AS BuyCount,SUM(BuyPrice) AS BuyPrice,SUM(RefundmentCount) AS RefundmentCount,SUM(RefundmentPrice) AS RefundmentPrice,SUM(ComplainCount) AS ComplainCount,MIN(CustGrade) AS CustGrade,MIN(CustProint)  AS CustProint");

            strSql.Append(" FROM statdba.DataCustAnalysis ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" group by CustNo");


            //if (orderby.Trim() != "")
            //{
            //    strSql.Append(" Order by " + orderby);
            //}

            return Database.RunSql(strSql.ToString());
        }

        	
		#endregion  成员方法
	}
}

