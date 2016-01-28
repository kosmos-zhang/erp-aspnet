/**********************************************
 * 类作用：   辅助数据项设置数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/06/01
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using System.Collections;

namespace XBase.Data.Office.FinanceManager
{
   public class AdditionalItemDBHelper
    {
       /// <summary>
       /// 默认构造函数
       /// </summary>
       public AdditionalItemDBHelper()
       {

       }
       /// <summary>
       /// 添加辅助数据项
       /// </summary>
        /// <param name="Model">辅助数据项实体</param>
       /// <param name="IntID">抛出生成记录的主键ID</param>
       /// <returns></returns>
       public bool InsertAdditionalItem(ArrayList List)
       {
           int nev = 0;
           for (int i = 0; i < List.Count; i++)
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("insert into officedba.AdditionalItem(");
               sql.AppendLine("DuringDate,ItemName,Line,CurrentAmount,PreAmount,CompanyCD)");
               sql.AppendLine(" values (");
               sql.AppendLine("@DuringDate" + i + ",@ItemName" + i + ",@Line" + i + ",@CurrentAmount" + i + ",@PreAmount" + i + ",@CompanyCD" + i + ")");

               SqlParameter[] parms = new SqlParameter[6];
               parms[0] = SqlHelper.GetParameter("@DuringDate" + i + "", (List[i] as AdditionalItemModel).DuringDate);
               parms[1] = SqlHelper.GetParameter("@ItemName" + i + "", (List[i] as AdditionalItemModel).ItemName);
               parms[2] = SqlHelper.GetParameter("@Line" + i + "", (List[i] as AdditionalItemModel).Line);
               parms[3] = SqlHelper.GetParameter("@CurrentAmount" + i + "", (List[i] as AdditionalItemModel).CurrentAmount);
               parms[4] = SqlHelper.GetParameter("@PreAmount" + i + "", (List[i] as AdditionalItemModel).PreAmount);
               parms[5] = SqlHelper.GetParameter("@CompanyCD" + i + "", (List[i] as AdditionalItemModel).CompanyCD);
               SqlHelper.ExecuteTransSql(sql.ToString(), parms);
               nev += SqlHelper.Result.OprateCount;
           }

          
           return nev > 0 ? true : false;
       }

       /// <summary>
       /// 更新辅助数据项信息
       /// </summary>
       /// <param name="Model">辅助数据项实体</param>
       /// <returns></returns>
       public bool UpdateAdditionalItem(AdditionalItemModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("update officedba.AdditionalItem set ");
           sql.AppendLine(" DuringDate=@DuringDate,ItemName=@ItemName,Line=@Line,CurrentAmount=@CurrentAmount,");
           sql.AppendLine(" PreAmount=@PreAmount,CompanyCD=@CompanyCD  ");
           sql.AppendLine(" where ID=@ID");
          
           SqlParameter[] parms = new SqlParameter[7];
           parms[0] = SqlHelper.GetParameter("@DuringDate", Model.DuringDate);
           parms[1] = SqlHelper.GetParameter("@ItemName", Model.ItemName);
           parms[2] = SqlHelper.GetParameter("@Line", Model.Line);
           parms[3] = SqlHelper.GetParameter("@CurrentAmount", Model.CurrentAmount);
           parms[4] = SqlHelper.GetParameter("@PreAmount", Model.PreAmount);
           parms[5] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
           parms[6] = SqlHelper.GetParameter("@ID", Model.ID);

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;

       }
       /// <summary>
       /// 删除辅助数据项信息
       /// </summary>
       /// <param name="DuringDate">会计期间</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool DeleteAdditionalItem(string DuringDate, string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("delete from officedba.AdditionalItem ");
           sql.AppendLine(" where CompanyCD=@CompanyCD and  DuringDate=@DuringDate ");
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@DuringDate", DuringDate);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;

       }
       /// <summary>
       /// 判断辅助数据项是否设置
       /// </summary>
       /// <param name="DuringDate">会计期间</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool IsExist(string DuringDate, string CompanyCD)
       {
           int nev = 0;
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select count(ID) from officedba.AdditionalItem ");
           sql.AppendLine(" where CompanyCD=@CompanyCD and  DuringDate=@DuringDate ");
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@DuringDate", DuringDate);

           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           if (obj != null)
           {
               nev = Convert.ToInt32(obj);
           }
           return nev > 0 ? true : false;
       }

       /// <summary>
       /// 获取辅助数据项信息
       /// </summary>
       /// <param name="DuringDate">会计期间</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public DataTable GetAdditionalItemInfo(string DuringDate, string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,DuringDate,ItemName,Line,CurrentAmount,PreAmount,CompanyCD from officedba.AdditionalItem ");
           sql.AppendLine(" where CompanyCD=@CompanyCD and  DuringDate=@DuringDate  order by ID asc");
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@DuringDate", DuringDate);

           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
    }
}
