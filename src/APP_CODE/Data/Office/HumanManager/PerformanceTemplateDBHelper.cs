/**********************************************
 * 类作用：   考核模板数据操作
 * 建立人：   王保军
 * 建立时间： 2009/05/07
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;

namespace XBase.Data.Office.HumanManager
{
    public class PerformanceTemplateDBHelper
    {
        /// <summary>
        /// 获得考核指标信息
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetPerformanceElemList(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" select * from officedba.PerformanceElem where CompanyCD=@CompanyCD and UsedStatus=@UsedStatus");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];

            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");
            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        /// <summary>
        /// 获取考核类型列表
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetPerformanceType(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" select * from officedba.PerformanceType  where CompanyCD=@CompanyCD and UsedStatus=@UsedStatus ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        /// <summary>
        ///批插入模板指标
        /// </summary>
        /// <param name="modeList"></param>
        /// <returns></returns>
        public static bool InsertPerformenceTempElm(IList<PerformanceTemplateElemModel> modeList)
        {
            foreach (PerformanceTemplateElemModel model in modeList)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("INSERT INTO officedba. PerformanceTemplateElem ");
                insertSql.AppendLine("           (CompanyCD             ");
                insertSql.AppendLine("           ,TemplateNo                ");
                insertSql.AppendLine("           ,ElemID              ");
                insertSql.AppendLine("           ,ElemOrder                 ");
                insertSql.AppendLine("           ,Rate           ");
                insertSql.AppendLine("           ,ModifiedDate               ");
                insertSql.AppendLine("           ,ModifiedUserID)                 ");

                insertSql.AppendLine("     VALUES                        ");
                insertSql.AppendLine("           (@CompanyCD            ");
                insertSql.AppendLine("           ,@TemplateNo               ");
                insertSql.AppendLine("           ,@ElemID             ");
                insertSql.AppendLine("           ,@ElemOrder               ");
                insertSql.AppendLine("           ,@Rate          ");
                insertSql.AppendLine("           ,getdate()              ");
                insertSql.AppendLine("           ,@ModifiedUserID)                ");
                //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//类型名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemID", model.ElemID));	//创建人
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemOrder", model.ElemOrder));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", model.Rate.ToString()));	//更新用户ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID

                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                if (!isSucc)
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;



        }
        /// <summary>
        /// 更新指标库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdatePerformenceTemplate(PerformanceTemplateModel model)
        {
            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("update officedba.PerformanceTemplate set Title=@Title,TypeID=@TypeID,Description=@Description,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID,UsedStatus=@UsedStatus where  CompanyCD=@CompanyCD and  TemplateNo=@TemplateNo");

            //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            #endregion
            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//类型名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));	//创建人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Description", model.Description));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID

            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            if (isSucc)
            {
                return true;
            }
            else
            {
                return false;
            }





        }
        /// <summary>
        /// 删除指标信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeletePerformenceTemElem(PerformanceTemplateModel model)
        {
            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("delete from officedba.PerformanceTemplateElem where CompanyCD=@CompanyCD and TemplateNo=@TemplateNo ");

            //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            #endregion
            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//类型名称

            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            if (isSucc)
            {
                return true;
            }
            else
            {
                return false;
            }





        }

        /// <summary>
        /// 插入一条模板信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertPerformenceTemplate(PerformanceTemplateModel model)
        {
            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("INSERT INTO officedba. PerformanceTemplate ");
            insertSql.AppendLine("           (CompanyCD             ");
            insertSql.AppendLine("           ,TemplateNo                ");
            insertSql.AppendLine("           ,Title              ");
            insertSql.AppendLine("           ,TypeID                 ");
            insertSql.AppendLine("           ,Description           ");
            insertSql.AppendLine("           ,UsedStatus                 ");
            insertSql.AppendLine("           ,Creator           ");
            insertSql.AppendLine("           ,CreateDate)           ");
            insertSql.AppendLine("     VALUES                        ");
            insertSql.AppendLine("           (@CompanyCD            ");
            insertSql.AppendLine("           ,@TemplateNo               ");
            insertSql.AppendLine("           ,@Title             ");
            insertSql.AppendLine("           ,@TypeID               ");
            insertSql.AppendLine("           ,@Description          ");
            insertSql.AppendLine("           ,@UsedStatus               ");
            insertSql.AppendLine("           ,@Creator          ");
            insertSql.AppendLine("           ,getdate()  )         ");
            //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            #endregion
            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));	//类型名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));	//创建人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Description", model.Description));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));	//更新用户ID

            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            if (isSucc)
            {
                return true;
            }
            else
            {
                return false;
            }





        }
        /// <summary>
        /// 检索（根据模板的名称和模板ID为条件）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable SearchTemplateInfo(PerformanceTemplateModel model)
        {

            //ISNULL(TypeName, '') AS TypeName
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();

            searchSql.AppendLine("select a.ID,ISNULL(a.TemplateNo,'') AS TemplateNo,ISNULL(a.Title,'') AS Title,ISNULL(c.TypeName,'') AS TypeName,ISNULL(a.Description,'') as Description,ISNULL(a.UsedStatus,'') as UsedStatus,ISNULL(b.EmployeeName,'') as CreaterName,ISNULL( CONVERT(VARCHAR(10), a.CreateDate,21),'') as CreateDate, isnull( Convert(varchar(100),a.ModifiedDate,23),'') AS ModifiedDate   from officedba.PerformanceTemplate a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.Creator=b.ID left outer join officedba.PerformanceType c on c.CompanyCD=a.CompanyCD and  a.TypeID=c.ID    where a.CompanyCD = @CompanyCD  ");
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                searchSql.AppendLine("and a.TypeID=@TypeID");
            }
            if (!string.IsNullOrEmpty(model.Title))
            {

                searchSql.AppendLine("and a.title like @title");
            }



            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@title", "%" + model.Title + "%"));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //l
            //启用状态


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static bool IsTemplateUsed(string TemplateNo, string CompanyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                       ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
            searchSql.AppendLine(" FROM                         ");
            searchSql.AppendLine(" 	officedba.PerformanceTemplateEmp");
            searchSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "' ");
            searchSql.AppendLine("and TemplateNo='"+TemplateNo+"'");

            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        /// <summary>
        /// 根据模板编号获取模板信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetPerformanceElemInfo(PerformanceTemplateModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.ID,a.TemplateNo as TemplateNo,ISNULL(a.Title,'') as Title,ISNULL(c.TypeName,'') as TypeName,ISNULL(a.Description,'') as Description,a.UsedStatus,b.ElemID,b.ElemOrder,b.Rate,ISNULL(d.ElemName,'') as ElemName,a.typeID,isnull(e.ElemName,'') as ParentName,e.ID  as ParentID,CONVERT(VARCHAR(10),a.CreateDate,21) AS CreateDate  from officedba.PerformanceTemplate a right outer join officedba.PerformanceTemplateElem b on  b.CompanyCD=a.CompanyCD  and a.TemplateNo=b.TemplateNo   left outer join officedba.PerformanceType c on   c.CompanyCD=a.CompanyCD and  a.TypeID=c.ID  left outer join officedba.PerformanceElem  d on   d.CompanyCD=a.CompanyCD  and b.ElemID=d.ID  left outer join officedba.PerformanceElem  e on   e.CompanyCD=a.CompanyCD and d.ParentElemNo=e.ID   where a.CompanyCD = @CompanyCD and a.TemplateNo=@TemplateNo order by e.ID");
            #endregion



            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));
            //l
            //启用状态


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static bool IsExist(string TemplateNo, string CompanyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                       ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
            searchSql.AppendLine(" FROM                         ");
            searchSql.AppendLine(" 	officedba.PerformanceTemplate ");
            searchSql.AppendLine(" WHERE  CompanyCD='"+CompanyCD+"' ");
            searchSql.AppendLine("and TemplateNo='"+TemplateNo+"'");

            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        public static bool DeletePerTemplateInfo(string TemplateNo, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.PerformanceTemplate ");
            deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
            deleteSql.AppendLine("and  TemplateNo='"+TemplateNo+"'");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //执行插入操作并返回更新结果
           bool result= SqlHelper.ExecuteTransWithCommand(comm);
           if (result)
           {
               StringBuilder deleteSq = new StringBuilder();
               deleteSq.AppendLine(" DELETE FROM officedba.PerformanceTemplateElem ");
               deleteSq.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
               deleteSq.AppendLine("and  TemplateNo='"+TemplateNo+"'");

               //定义更新基本信息的命令
               SqlCommand com = new SqlCommand();
               com.CommandText = deleteSq.ToString();

               //执行插入操作并返回更新结果
                 bool sign=  SqlHelper.ExecuteTransWithCommand(com);
                 if (sign)
                 {
                     StringBuilder delete = new StringBuilder();
                     delete.AppendLine(" DELETE FROM officedba.PerformanceTemplateEmp ");
                     delete.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
                     delete.AppendLine("and  TemplateNo='" + TemplateNo + "'");

                     //定义更新基本信息的命令
                     SqlCommand com1 = new SqlCommand();
                     com1.CommandText = delete.ToString();

                     //执行插入操作并返回更新结果
                     return  SqlHelper.ExecuteTransWithCommand(com1);
                 }
                 else
                 {
                     return false;
                 }



           }
           else
           {
               return false;

           }
        }




    }
}
