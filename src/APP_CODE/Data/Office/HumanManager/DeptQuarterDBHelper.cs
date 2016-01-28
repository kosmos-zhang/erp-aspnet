/**********************************************
 * 类作用：   机构岗位
 * 建立人：   吴志强
 * 建立时间： 2009/04/13
   * 修改人：   王保军
 * 建立时间： 2009/08/27
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
    /// <summary>
    /// 类名：DeptQuarterDBHelper
    /// 描述：机构岗位
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/13
    /// 最后修改时间：2009/04/13
    /// </summary>
    ///
    public class DeptQuarterDBHelper
    {






        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="modeList"></param>
        /// <returns></returns>
        public static bool DeleteByQuarterSet(IList<QuterModuleSetModel>  modeList)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("delete from officedba.QuterModuleSet where Sign='2' and  CompanyCD=@CompanyCD and DeptID=@DeptID  and QuarterNo=@QuarterNo ");
            //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            #endregion
            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", modeList[0].CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", modeList[0].DeptID));	//类型名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterNo", modeList[0].QuarterNo));	//类型名称
            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

            return isSucc;



        }


        public static bool SaveQuarterSet(IList<QuterModuleSetModel> modeList)
        {
            DeleteByQuarterSet(modeList);
            bool isSucc;
            foreach (QuterModuleSetModel model in modeList)
            {


                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("INSERT INTO officedba.QuterModuleSet ");
                insertSql.AppendLine("           (CompanyCD             ");
                insertSql.AppendLine("           ,DeptID                ");
                insertSql.AppendLine("           ,QuarterNo              ");
                insertSql.AppendLine("           ,ModuleID                 ");
                insertSql.AppendLine("           ,TypeID           "); 
                insertSql.AppendLine("           ,Sign)                 ");

                insertSql.AppendLine("     VALUES                        ");
                insertSql.AppendLine("           (@CompanyCD            ");
                insertSql.AppendLine("           ,@DeptID               ");
                insertSql.AppendLine("           ,@QuarterNo             ");
                insertSql.AppendLine("           ,@ModuleID               ");
                insertSql.AppendLine("           ,@TypeID          "); 
                insertSql.AppendLine("           ,@Sign)                ");
                //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                SetSaveParameter(comm, model);

                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                if (!isSucc)
                {
                    isSucc = false;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return true;

        }


        private static void SetSaveParameter(SqlCommand comm, QuterModuleSetModel model)
        {
            //设置参数

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));	//类型名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterNo", model.QuarterNo));	//创建人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModuleID", model.ModuleID));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sign", model.Sign));	//更新用户ID  

        }

        #region 添加机构岗位信息
        /// <summary>
        /// 添加机构岗位信息
        /// </summary>
        /// <param name="model">机构岗位信息</param>
        /// <returns></returns>
        public static bool InsertDeptQuarterInfo(DeptQuarterModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.DeptQuarter ");
            insertSql.AppendLine("            (CompanyCD             ");
            insertSql.AppendLine("            ,DeptID                ");
            insertSql.AppendLine("            ,QuarterNo             ");
            insertSql.AppendLine("            ,SuperQuarterID        ");
            insertSql.AppendLine("            ,PYShort               ");
            insertSql.AppendLine("            ,QuarterName           ");
            insertSql.AppendLine("            ,TypeID                ");
            insertSql.AppendLine("            ,LevelID               ");
            insertSql.AppendLine("            ,KeyFlag               ");
            insertSql.AppendLine("            ,Duty                  ");
            insertSql.AppendLine("            ,DutyRequire           ");
            insertSql.AppendLine("            ,Attachment            ");
            insertSql.AppendLine("            ,UsedStatus            ");
            insertSql.AppendLine("            ,Description           ");
            insertSql.AppendLine("            ,ModifiedDate          ");
            insertSql.AppendLine("            ,QuterContent          ");
            
            insertSql.AppendLine("            ,ModifiedUserID)       ");
            insertSql.AppendLine("      VALUES                       ");
            insertSql.AppendLine("            (@CompanyCD            ");
            insertSql.AppendLine("            ,@DeptID               ");
            insertSql.AppendLine("            ,@QuarterNo            ");
            insertSql.AppendLine("            ,@SuperQuarterID       ");
            insertSql.AppendLine("            ,@PYShort              ");
            insertSql.AppendLine("            ,@QuarterName          ");
            insertSql.AppendLine("            ,@TypeID               ");
            insertSql.AppendLine("            ,@LevelID              ");
            insertSql.AppendLine("            ,@KeyFlag              ");
            insertSql.AppendLine("            ,@Duty                 ");
            insertSql.AppendLine("            ,@DutyRequire          ");
            insertSql.AppendLine("            ,@Attachment           ");
            insertSql.AppendLine("            ,@UsedStatus           ");
            insertSql.AppendLine("            ,@Description          ");
            insertSql.AppendLine("            ,getdate()             ");
            insertSql.AppendLine("            ,@QuterContent          ");
            insertSql.AppendLine("            ,@ModifiedUserID)      ");
            #endregion

            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        #endregion

        #region 更新组织机构信息
        /// <summary>
        /// 更新组织机构信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool UpdateDeptQuarterInfo(DeptQuarterModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.DeptQuarter            ");
            updateSql.AppendLine("    SET                                  ");
            updateSql.AppendLine("        PYShort = @PYShort               ");
            updateSql.AppendLine("       ,QuarterName = @QuarterName       ");
            updateSql.AppendLine("       ,TypeID = @TypeID                 ");
            updateSql.AppendLine("       ,LevelID = @LevelID               ");
            updateSql.AppendLine("       ,KeyFlag = @KeyFlag               ");
            updateSql.AppendLine("       ,Duty = @Duty                     ");
            updateSql.AppendLine("       ,DutyRequire = @DutyRequire       ");
            updateSql.AppendLine("       ,Attachment = @Attachment         ");
            updateSql.AppendLine("       ,UsedStatus = @UsedStatus         ");
            updateSql.AppendLine("       ,Description = @Description       ");

            updateSql.AppendLine("       ,QuterContent = @QuterContent       ");
            
            updateSql.AppendLine("       ,ModifiedDate = getdate()         ");
            updateSql.AppendLine("       ,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine("  WHERE                                  ");
            updateSql.AppendLine(" 	  CompanyCD = @CompanyCD               ");
            updateSql.AppendLine(" 	  AND QuarterNo = @QuarterNo          ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">保存信息</param>
        private static void SetSaveParameter(SqlCommand comm, DeptQuarterModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterNo", model.QuarterNo));//岗位编号
            if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));//部门ID（对应部门表ID）
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SuperQuarterID", model.SuperQuarterID));//上级岗位ID
            }


            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuterContent", model.QuterContent));//岗位拼音代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));//岗位拼音代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterName", model.QuarterName));//岗位名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));//岗位类型ID（对应分类代码表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelID", model.LevelID));//岗位级别ID（对应分类代码表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@KeyFlag", model.KeyFlag));//是否关键岗位(0否，1是)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Duty", model.Duty));//岗位职责
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DutyRequire", model.DutyRequire));//任职资格
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.PageAttachment));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Description", model.Description));//描述
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后更新用户ID
        }
        #endregion

        #region 通过检索条件查询组织机构信息
        /// <summary>
        /// 查询组织机构信息
        /// </summary>
        /// <param name="quarterID">机构岗位ID</param>
        /// <returns></returns>
        public static DataTable SearchQuarterInfo(string quarterID, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                          ");
            searchSql.AppendLine(" 	 A.ID                          ");
            searchSql.AppendLine(" 	,A.DeptID                      ");
            searchSql.AppendLine(" 	,A.QuarterNo                   ");
            searchSql.AppendLine(" 	,A.SuperQuarterID              ");
            searchSql.AppendLine(" 	,A.QuarterName                 ");
            searchSql.AppendLine(" 	,( SELECT                      ");
            searchSql.AppendLine(" 		COUNT(ID)                  ");
            searchSql.AppendLine(" 	   FROM                        ");
            searchSql.AppendLine(" 		officedba.DeptQuarter B    ");
            searchSql.AppendLine(" 	   WHERE                       ");
            searchSql.AppendLine(" 		  B.CompanyCD=A.CompanyCD  and  B.SuperQuarterID = A.ID ");
            searchSql.AppendLine(" 		) AS SubCount              ");
            searchSql.AppendLine(" FROM officedba.DeptQuarter A    ");
            searchSql.AppendLine(" WHERE                           ");
            searchSql.AppendLine(" 	  A.CompanyCD=@CompanyCD and A.SuperQuarterID = @QuarterID  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //上级岗位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", quarterID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD ));        
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion




        public static DataTable GetQuarterModelSet(string DeptID, string QuarterNo, string CompanyCD)
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.ModuleID                              ");
            searchSql.AppendLine(" 	,A.TypeID                       "); 

            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.QuterModuleSet A            "); 
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine("   A.CompanyCD=@CompanyCD  and  	A.QuarterNo = @QuarterNo and A.DeptID=@DeptID   and A.Sign='2'           ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            //组织机构ID
            param[0] = SqlHelper.GetParameter("@DeptID", DeptID);
            param[1] = SqlHelper.GetParameter("@QuarterNo", QuarterNo);
            param[2] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        public static DataTable GetQuarterDescrible( )
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT    ID,                          ");
            searchSql.AppendLine(" 	isnull(QuterName,'') as QuterName                             ");

            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba. QuterDescribInfo            ");
            #endregion

          
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString());
        }

        public static DataTable selectQuarterDescrible(string DescibleID)
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                            ");
            searchSql.AppendLine(" 	isnull(QuterContent,'') as QuterContent                             ");
            searchSql.AppendLine(" 	,isnull(QuterName,'') as QuterName                             ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba. QuterDescribInfo  where ID=@ID             ");
            #endregion
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //组织机构ID
            param[0] = SqlHelper.GetParameter("@ID", DescibleID);

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        public static DataTable selectQuarterSet(string DescibleID)
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                            ");
            searchSql.AppendLine("  TypeID,ModuleID                           ");

            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba. QuterModuleSet  where QuterDescribID=@QuterDescribID  and sign='1'            ");
            #endregion
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //组织机构ID
            param[0] = SqlHelper.GetParameter("@QuterDescribID", DescibleID);

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }


        #region 通过岗位ID查询岗位信息
        /// <summary>
        /// 查询岗位信息
        /// </summary>
        /// <param name="quarterID">岗位ID</param>
        /// <returns></returns>
        public static DataTable GetDeptQuarterInfoWithID(string quarterID, string CompanyCD)
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.ID                              ");
            searchSql.AppendLine(" 	,A.CompanyCD                       ");
            searchSql.AppendLine(" 	,A.DeptID                          ");
            searchSql.AppendLine(" 	,isnull(C.DeptName,'')  AS DeptName            ");
            searchSql.AppendLine(" 	,isnull(A.QuarterNo,'') as  QuarterNo                      ");
            searchSql.AppendLine(" 	,A.SuperQuarterID                  ");
            searchSql.AppendLine(" 	,isnull(B.QuarterName,'') AS SuperQuarterName ");
            searchSql.AppendLine(" 	,isnull(A.PYShort,'') as PYShort                         ");
            searchSql.AppendLine(" 	,isnull(A.QuarterName,'') as     QuarterName                 ");
            searchSql.AppendLine(" 	,A.TypeID                          ");
            searchSql.AppendLine(" 	,A.LevelID                         ");
            searchSql.AppendLine(" 	,A.KeyFlag                         ");
            searchSql.AppendLine(" 	,isnull(A.Duty,'') as     Duty                         ");
            searchSql.AppendLine(" 	,isnull(A.DutyRequire,'') as  DutyRequire                    ");
            searchSql.AppendLine(" 	,isnull(A.Attachment,'') as Attachment                      ");
            searchSql.AppendLine(" 	,A.UsedStatus                      ");
            searchSql.AppendLine(" 	,isnull(A.Description,'') as  Description                    ");
            searchSql.AppendLine(" 	,isnull(A.QuterContent,'') as QuterContent                     ");

            
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.DeptQuarter A            ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B  ");
            searchSql.AppendLine(" 		ON  A.CompanyCD=B.CompanyCD and  A.SuperQuarterID = B.ID    ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C     ");
            searchSql.AppendLine(" 		ON  C.CompanyCD=A.CompanyCD   and  A.DeptID = C.ID         ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine("   A.CompanyCD=@CompanyCD  and  	A.ID = @QuarterID              ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //组织机构ID
            param[0] = SqlHelper.GetParameter("@QuarterID", quarterID);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD );
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        #endregion

        #region 删除机构岗位信息
        /// <summary>
        /// 删除机构岗位信息
        /// </summary>
        /// <param name="quarterID">组织机构ID</param>
        /// <returns></returns>
        public static bool DeleteQuarterInfo(string quarterID, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.DeptQuarter ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD=@CompanyCD and ID = @QuarterID ");

            //定义命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@QuarterID", quarterID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD ));
            ArrayList lstDelete = new ArrayList();
            lstDelete.Add(comm);

            //删除SQL拼写
            StringBuilder deleteSubSql = new StringBuilder();
            deleteSubSql.AppendLine(" DELETE FROM officedba.DeptQuarter ");
            deleteSubSql.AppendLine(" WHERE ");
            deleteSubSql.AppendLine("  CompanyCD=@CompanyCD and SuperQuarterID = @QuarterID ");

            //定义命令
            comm = new SqlCommand();
            comm.CommandText = deleteSubSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@QuarterID", quarterID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            lstDelete.Add(comm);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 通过机构ID获取岗位信息
        /// <summary>
        /// 通过机构ID获取岗位信息
        /// </summary>
        /// <param name="deptID">机构ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetQuarterInfoWithDeptID(string deptID,string quarterID,string companyCD)
        {

           

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                          ");
            searchSql.AppendLine(" 	 A.ID                          ");
            searchSql.AppendLine(" 	,A.DeptID                      ");
            searchSql.AppendLine(" 	,A.QuarterNo                   ");
            searchSql.AppendLine(" 	,A.SuperQuarterID              ");
            searchSql.AppendLine(" 	,A.QuarterName                 ");
            searchSql.AppendLine(" 	,( SELECT                      ");
            searchSql.AppendLine(" 		COUNT(ID)                  ");
            searchSql.AppendLine(" 	   FROM                        ");
            searchSql.AppendLine(" 		officedba.DeptQuarter B    ");
            searchSql.AppendLine(" 	   WHERE                       ");
            searchSql.AppendLine(" 		B.SuperQuarterID = A.ID    ");
            searchSql.AppendLine(" 		) AS SubCount              ");
            searchSql.AppendLine(" FROM officedba.DeptQuarter A    ");
            searchSql.AppendLine(" WHERE                           ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD       ");
            //searchSql.AppendLine(" 	AND A.SuperQuarterID IS not  NULL   ");
            if (string.IsNullOrEmpty(quarterID))
            {
                searchSql.AppendLine(" 	AND A.SuperQuarterID IS NULL   ");
            }
            #endregion
         

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //组织机构ID输入时
            if (!string.IsNullOrEmpty(deptID))
            {
                searchSql.AppendLine(" AND A.DeptID = @DeptID  ");
                //机构ID参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", deptID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 获取公司岗位信息
        /// <summary>
        /// 获取公司岗位信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetQuarterInfoWithCompanyCD(string companyCD)
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 dp.ID                    ");
            searchSql.AppendLine(" 	,dp.DeptID                ");
            searchSql.AppendLine(" 	,dp.QuarterNo             ");
            searchSql.AppendLine(" 	,d.deptname+'_'+dp.QuarterName QuarterName ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.DeptQuarter dp ");
            searchSql.AppendLine(" left join officedba.DeptInfo d on d.id = dp.DeptID ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	dp.CompanyCD = @CompanyCD ");
            searchSql.AppendLine(" 	AND dp.UsedStatus = @UsedStatus and d.deptname IS NOT null   ");
            searchSql.AppendLine("  order by dp.deptid ");

            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //公司代码
            param[1] = SqlHelper.GetParameter("@UsedStatus", ConstUtil.USED_STATUS_ON);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 获取机构岗位分类的方法
        /// <summary>
        /// 获取机构岗位分类的方法
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetQuarterType(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                    "id,QuarterName,isnull(SuperQuarterID,0) SuperQuarterID" +
                              " from " +
                                    "officedba.DeptQuarter" +
                              " where " +
                                    "CompanyCD=@CompanyCD and UsedStatus=1";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
    }
}
