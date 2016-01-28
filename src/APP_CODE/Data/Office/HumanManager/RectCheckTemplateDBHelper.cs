/**********************************************
 * 类作用：   面试评测模板操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/16
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

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：RectCheckTemplateDBHelper
    /// 描述：面试评测模板操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/16
    /// 最后修改时间：2009/04/16
    /// </summary>
    ///
    public class RectCheckTemplateDBHelper
    {

        #region 通过ID查询面试评测模板信息
        /// <summary>
        /// 查询面试评测模板信息
        /// </summary>
        /// <param name="templateID">面试评测模板ID</param>
        /// <returns></returns>
        public static DataSet GetRectTemplateInfoWithID(string templateID)
        {
            //定义返回的数据变量
            DataSet dsTemplateInfo = new DataSet();

            #region 查询面试评测模板信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                       ");
            searchSql.AppendLine(" 	 CompanyCD                  ");
            searchSql.AppendLine(" 	,TemplateNo                 ");
            searchSql.AppendLine(" 	,Title                      ");
            searchSql.AppendLine(" 	,QuarterID                  ");
            searchSql.AppendLine(" 	,Remark                     ");
            searchSql.AppendLine(" 	,ModifiedDate               ");
            searchSql.AppendLine(" 	,ModifiedUserID             ");
            searchSql.AppendLine(" FROM                         ");
            searchSql.AppendLine(" 	officedba.RectCheckTemplate ");
            searchSql.AppendLine(" WHERE                        ");
            searchSql.AppendLine(" 	ID = @TemplateID            ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //面试评测模板ID
            param[0] = SqlHelper.GetParameter("@TemplateID", templateID);
            //执行查询
            DataTable dtBaseInfo = new DataTable("BaseInfo");
            dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //设置面试评测模板基本信息
            dsTemplateInfo.Tables.Add(dtBaseInfo);
            //面试评测模板信息存在时，查询招聘目标以及信息发布信息
            if (dtBaseInfo.Rows.Count > 0)
            {
                //获取公司代码
                string companyCD = dtBaseInfo.Rows[0]["CompanyCD"].ToString();
                //获取面试评测模板编号
                string templateNo = dtBaseInfo.Rows[0]["TemplateNo"].ToString();
                //设置招聘目标
                dsTemplateInfo.Tables.Add(GetElemInfoWithID(companyCD, templateNo));
            }

            #endregion

            return dsTemplateInfo;
        }

        #region 通过公司代码以及面试评测模板编号招聘目标信息
        /// <summary>
        /// 通过公司代码以及面试评测模板编号获取招聘目标
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="planNo">面试评测模板编号</param>
        /// <returns></returns>
        private static DataTable GetElemInfoWithID(string companyCD, string templateNo)
        {
            //查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                               ");
            searchSql.AppendLine(" 	 A.CheckElemID                      ");
            searchSql.AppendLine(" 	,B.ElemName                         ");
            searchSql.AppendLine(" 	,A.MaxScore                         ");
            searchSql.AppendLine(" 	,A.Rate                             ");
            searchSql.AppendLine(" 	,A.Remark                           ");
            searchSql.AppendLine(" FROM                                 ");
            searchSql.AppendLine(" 	officedba.RectCheckTemplateElem A   ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.RectCheckElem B ");
            searchSql.AppendLine(" 		ON A.CheckElemID = B.ID         ");
            searchSql.AppendLine(" WHERE                                ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD            ");
            searchSql.AppendLine(" 	AND A.TemplateNo = @TemplateNo      ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //面试评测模板ID
            param[1] = SqlHelper.GetParameter("@TemplateNo", templateNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #endregion

        #region 通过检索条件查询面试评测模板信息
        /// <summary>
        /// 查询面试评测模板信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchTemplateInfo(RectCheckTemplateModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                     ");
            searchSql.AppendLine(" 	 A.ID                                     ");
            searchSql.AppendLine(" 	,A.TemplateNo                             ");
            searchSql.AppendLine(" 	,ISNULL(A.Title, '') AS Title             ");
            searchSql.AppendLine(" 	,ISNULL(B.QuarterName, '') AS QuarterName ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate");
            searchSql.AppendLine(" FROM                                       ");
            searchSql.AppendLine(" 	officedba.RectCheckTemplate A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B         ");
            searchSql.AppendLine(" 		ON A.QuarterID = B.ID                 ");
            searchSql.AppendLine(" WHERE                                      ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //模板编号
            if (!string.IsNullOrEmpty(model.TemplateNo))
            {
                searchSql.AppendLine(" AND A.TemplateNo LIKE '%' + @TemplateNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine(" AND A.Title LIKE '%' + @Title + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine(" AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        public static DataTable SearchTemplateCSInfo(RectCheckTemplateModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                     ");
            searchSql.AppendLine(" 	 A.ID                                     ");
            searchSql.AppendLine(" 	,A.TemplateNo                             ");
            searchSql.AppendLine(" 	,ISNULL(A.Title, '') AS Title             ");
            searchSql.AppendLine(" 	,ISNULL(B.QuarterName, '') AS QuarterName ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate");
            searchSql.AppendLine(" FROM                                       ");
            searchSql.AppendLine(" 	officedba.RectCheckTemplate A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B         ");
            searchSql.AppendLine(" 		ON A.QuarterID = B.ID                 ");
            searchSql.AppendLine(" WHERE                                      ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //模板编号
            if (!string.IsNullOrEmpty(model.TemplateNo))
            {
                searchSql.AppendLine(" AND A.TemplateNo LIKE '%' + @TemplateNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine(" AND A.Title LIKE '%' + @Title + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine(" AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount); //执行查询
        }

        #region 添加面试评测模板以及相关信息
        /// <summary>
        /// 添加面试评测模板
        /// </summary>
        /// <param name="model">面试评测模板信息</param>
        /// <returns></returns>
        public static bool InsertRectTemplateInfo(RectCheckTemplateModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO                 ");
            insertSql.AppendLine(" officedba.RectCheckTemplate ");
            insertSql.AppendLine(" 	(CompanyCD                 ");
            insertSql.AppendLine(" 	,TemplateNo                ");
            insertSql.AppendLine(" 	,Title                     ");
            insertSql.AppendLine(" 	,QuarterID                 ");
            insertSql.AppendLine(" 	,Remark                    ");
            insertSql.AppendLine(" 	,UsedStatus                ");
            insertSql.AppendLine(" 	,ModifiedDate              ");
            insertSql.AppendLine(" 	,ModifiedUserID)           ");
            insertSql.AppendLine(" VALUES                      ");
            insertSql.AppendLine(" 	(@CompanyCD                ");
            insertSql.AppendLine(" 	,@TemplateNo               ");
            insertSql.AppendLine(" 	,@Title                    ");
            insertSql.AppendLine(" 	,@QuarterID                ");
            insertSql.AppendLine(" 	,@Remark                   ");
            insertSql.AppendLine(" 	,@UsedStatus               ");
            insertSql.AppendLine(" 	,getdate()                 ");
            insertSql.AppendLine(" 	,@ModifiedUserID)          ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstInsert = new ArrayList();
            //添加基本信息更新命令
            lstInsert.Add(comm);
            //登陆或者更新要素信息
            EditElemInfo(lstInsert, model.ElemList, model.TemplateNo, model.CompanyCD);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);

        }
        #endregion

        #region 更新面试评测模板以及相关信息
        /// <summary>
        /// 更新面试评测模板以及相关信息
        /// </summary>
        /// <param name="model">面试评测模板信息</param>
        /// <returns></returns>
        public static bool UpdateRectTemplateInfo(RectCheckTemplateModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.RectCheckTemplate ");
            updateSql.AppendLine(" SET  Title = @Title                ");
            updateSql.AppendLine(" 	,QuarterID = @QuarterID           ");
            updateSql.AppendLine(" 	,Remark = @Remark                 ");
            updateSql.AppendLine(" 	,UsedStatus = @UsedStatus                 ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND TemplateNo = @TemplateNo      ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstUpdate = new ArrayList();
            //添加基本信息更新命令
            lstUpdate.Add(comm);
            //登陆或者更新要素信息
            EditElemInfo(lstUpdate, model.ElemList, model.TemplateNo, model.CompanyCD);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人员信息</param>
        private static void SetSaveParameter(SqlCommand comm, RectCheckTemplateModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));//评测模板编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));//岗位ID（对应岗位表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 登陆或更新面试评测模板的要素信息
        /// <summary>
        /// 登陆或更新面试评测模板的要素信息
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstElem">要素信息</param>
        /// <param name="planNo">面试评测模板编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        private static void EditElemInfo(ArrayList lstCommand, ArrayList lstElem, string templateNo, string companyCD)
        {

            //全删全插方式插入招聘目标信息

            #region 删除操作
            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectCheckTemplateElem ");
            deleteSql.AppendLine(" WHERE                          ");
            deleteSql.AppendLine("      TemplateNo = @TemplateNo          ");
            deleteSql.AppendLine("  AND CompanyCD = @CompanyCD    ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //员工编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //添加删除命令
            lstCommand.Add(comm);
            #endregion

            #region 插入操作
            //未填写招聘目标时，返回true
            if (lstElem != null || lstElem.Count > 0)
            {
                /* 插入操作 */
                #region 插入SQL文
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine(" INSERT INTO                     ");
                insertSql.AppendLine(" officedba.RectCheckTemplateElem ");
                insertSql.AppendLine(" 	(CompanyCD                     ");
                insertSql.AppendLine(" 	,TemplateNo                    ");
                insertSql.AppendLine(" 	,CheckElemID                   ");
                insertSql.AppendLine(" 	,MaxScore                      ");
                insertSql.AppendLine(" 	,Rate                          ");
                insertSql.AppendLine(" 	,Remark)                       ");
                insertSql.AppendLine(" VALUES                          ");
                insertSql.AppendLine(" 	(@CompanyCD                    ");
                insertSql.AppendLine(" 	,@TemplateNo                   ");
                insertSql.AppendLine(" 	,@CheckElemID                  ");
                insertSql.AppendLine(" 	,@MaxScore                     ");
                insertSql.AppendLine(" 	,@Rate                         ");
                insertSql.AppendLine(" 	,@Remark)                      ");
                #endregion

                //遍历所有的要素信息
                for (int i = 0; i < lstElem.Count; i++)
                {
                    //获取单条目标记录
                    RectCheckTemplateElemModel model = (RectCheckTemplateElemModel)lstElem[i];
                    //定义Command
                    comm = new SqlCommand();
                    //设置执行 Transact-SQL 语句
                    comm.CommandText = insertSql.ToString();

                    #region 设置参数
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//企业代码
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", templateNo));//评测模板编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckElemID", model.CheckElemID));//评测要素ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxScore", model.MaxScore));//满分
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", model.Rate));//权重
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
                    #endregion

                    //添加插入命令
                    lstCommand.Add(comm);

                }
            }
            #endregion

        }
        #endregion

        #region 删除面试评测模板信息
        /// <summary>
        /// 删除面试评测模板信息
        /// </summary>
        /// <param name="templateNo">面试评测模板编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteRectCheckTemplateInfo(string templateNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectCheckTemplate ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TemplateNo In( " + templateNo + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除要素信息
            DeleteElemInfo(lstDelete, companyCD, templateNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 删除要素信息
        /// <summary>
        /// 删除要素信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="templateNo">面试评测模板编号</param>
        /// <returns></returns>
        private static void DeleteElemInfo(ArrayList lstCommand, string companyCD, string templateNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectCheckTemplateElem ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" TemplateNo In( " + templateNo + " ) ");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

    }
}
