/**********************************************
 * 类作用：   工资项设置
 * 建立人：   吴志强
 * 建立时间： 2009/04/24
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
    /// 类名：SalaryItemDBHelper
    /// 描述：工资项设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/04
    /// 最后修改时间：2009/05/04
    /// </summary>
    ///
    public class SalaryItemDBHelper
    {

        public static DataTable SearchEmplInfo(EmployeeSearchModel model)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                           ");
            searchSql.AppendLine(" 	 A.ID                                           ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeNo, '') AS EmployeeNo         ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeNum, '') AS EmployeeNum       ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeName, '') AS EmployeeName     ");
            searchSql.AppendLine(" 	,ISNULL(C.QuarterName, '') AS QuarterName       ");
            searchSql.AppendLine(" 	,ISNULL(A.QuarterID, '') AS QuarterID           ");
            searchSql.AppendLine(" 	,ISNULL(B.DeptName, '') AS DeptName             ");
            searchSql.AppendLine(" 	,ISNULL(A.DeptID, '') AS Dept                   ");
            searchSql.AppendLine(" 	,ISNULL(D.TypeName, '') AS AdminLevelName       ");
            searchSql.AppendLine(" 	,ISNULL(A.AdminLevelID, '') AS AdminLevelID     ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') ");
            searchSql.AppendLine(" 		AS EnterDate                                ");
            searchSql.AppendLine(" FROM                                             ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B                  ");
            searchSql.AppendLine(" 		ON B.companyCD =A.companyCD AND A.DeptID = B.ID                          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C               ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND A.QuarterID = C.ID                       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D            ");
            searchSql.AppendLine(" 		ON D.companyCD=A.companyCD AND A.AdminLevelID = D.ID                    ");
            searchSql.AppendLine(" WHERE                                            ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                        ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag                              ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //非离职
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));

            //编号
            if (!string.IsNullOrEmpty(model.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE  '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", model.EmployeeNo));
            }
            //姓名
            if (!string.IsNullOrEmpty(model.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE  '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", model.EmployeeName));
            }
            //姓名
            if (!string.IsNullOrEmpty(model.Dept))
            {
                searchSql.AppendLine("	AND B.DeptName LIKE  '%' + @DeptID + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.Dept));
            }
            //岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //岗位职等
            if (!string.IsNullOrEmpty(model.AdminLevelID))
            {
                searchSql.AppendLine("	AND A.AdminLevelID = @AdminLevelID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.AdminLevelID));
            }

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetOutEmployeeInfo(EmployeeSearchModel model)
        {

            //int year = DateTime.Now.Year;
            //int month = DateTime.Now.Month;
            //if (month == 1)
            //{
            //    year = year - 1;
            //    month = 12;
            //}
            //else
            //{
            //    month = month - 1;
            //}
            //DateTime nowll=new DateTime (year ,month ,1);

           
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            //searchSql.AppendLine(" SELECT                                ");
            searchSql.AppendLine(" 	select 	 A.ID                      ");           
searchSql.AppendLine(",A.EmployeeNo                         ");
 	searchSql.AppendLine(",A.EmployeeNum                       "); 
 	searchSql.AppendLine(",A.CompanyCD                          ");
 	searchSql.AppendLine(",A.EmployeeName                    ");   
 	searchSql.AppendLine(",A.QuarterID                          ");
 	searchSql.AppendLine(",C.QuarterName                       "); 
 searchSql.AppendLine("	,A.DeptID                             ");
 searchSql.AppendLine("	,B.DeptName                          "); 
 searchSql.AppendLine("	,A.AdminLevelID                      "); 
 searchSql.AppendLine("	,D.TypeName AS AdminLevelName   ");
    //searchSql.AppendLine("  ,e.OutDate  ");
 searchSql.AppendLine("from officedba.EmployeeInfo A   ");
searchSql.AppendLine("left outer join officedba.MoveNotify e ");
searchSql.AppendLine("on E.CompanyCD=A.CompanyCD  and A.id=e.EmployeeID and e.BillStatus='2'    ");
searchSql.AppendLine("LEFT JOIN officedba.DeptInfo B        ");
searchSql.AppendLine("	ON B.companyCD=A.companyCD AND B.ID = A.DeptID                ");
 	searchSql.AppendLine("LEFT JOIN officedba.DeptQuarter C     ");
    searchSql.AppendLine("	ON C.companyCD=A.companyCD AND C.ID = A.QuarterID             ");
 	searchSql.AppendLine("LEFT JOIN officedba.CodePublicType D  ");
    searchSql.AppendLine("	ON D.companyCD=A.companyCD AND D.ID = A.AdminLevelID    ");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD             ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag    and  e.OutDate > @OutStartDate and e.OutDate < @OutEndDate               ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", nowll.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutStartDate", model .StartDate ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutEndDate", model .EndDate ));
            //在职标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "3"));

            #region 页面查询条件
            //编号
            if (!string.IsNullOrEmpty(model.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", model.EmployeeNo));
            }
            //工号
            if (!string.IsNullOrEmpty(model.EmployeeNum))
            {
                searchSql.AppendLine("	AND A.EmployeeNum = @EmployeeNum ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNum", model.EmployeeNum));
            }
            //姓名
            if (!string.IsNullOrEmpty(model.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", model.EmployeeName));
            }
            //所在岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //岗位职等
            if (!string.IsNullOrEmpty(model.AdminLevelID))
            {
                searchSql.AppendLine("	AND A.AdminLevelID = @AdminLevelID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.AdminLevelID));
            }
            #endregion

            //设置排序
            searchSql.AppendLine(" ORDER BY A.DeptID, A.EnterDate ");
            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }


        #region 查询工资项信息
        /// <summary>
        /// 查询工资项信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="isUsed">是否启用</param>
        /// <returns></returns>
        public static DataTable SearchSalaryItemInfo(string companyCD, bool isUsed)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 ItemNo                ");
            searchSql.AppendLine(" 	,CompanyCD             ");
            searchSql.AppendLine(" 	,ItemName              ");
            searchSql.AppendLine(" 	,ItemOrder             ");
            searchSql.AppendLine(" 	,Calculate             ");
            searchSql.AppendLine(" 	,PayFlag               ");
            searchSql.AppendLine(" 	,ChangeFlag            ");
            searchSql.AppendLine(" 	,UsedStatus            ");
            searchSql.AppendLine(" 	,Remark  ,ParamsList              ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.SalaryItem   ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //只查询启用中的数据
            if (isUsed)
            {
                //添加启用参数
                searchSql.AppendLine(" AND UsedStatus = @UsedStatus ");
                //启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", ConstUtil.USED_STATUS_ON));
            }
            //指定排序
            searchSql.AppendLine(" ORDER BY ItemOrder ");

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 保存工资项信息
        /// <summary>
        /// 保存工资项信息
        /// </summary>
        /// <param name="lstEdit">工资项信息</param>
        /// <returns></returns>
        public static bool SaveSalaryItemInfo(ArrayList lstEdit, string companyCD)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //保存库列表
                ArrayList lstSave = new ArrayList();
                //遍历所有工资项，进行增删改操作
                for (int i = 0; i < lstEdit.Count; i++)
                {
                    //获取值
                    SalaryItemModel model = (SalaryItemModel)lstEdit[i];
                    //设置公司代码
                    model.CompanyCD = companyCD;
                    //更新
                    if ("1".Equals(model.EditFlag))
                    {
                        //执行更新操作
                        lstSave.Add(UpdateSalaryItemInfo(model,companyCD ));
                    }
                    //插入
                    else if ("0".Equals(model.EditFlag))
                    {
                        //执行插入操作
                        lstSave.Add(InsertSalaryItemInfo(model));
                    }
                    //删除
                    else
                    {
                        //执行删除操作
                        lstSave.Add(DeleteSalaryItemInfo(model.ItemNo,companyCD ));
                        lstSave.Add(DeleteSalaryEmployeeInfo(model.ItemNo,companyCD ));
                    }
                }
                //执行保存操作
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstSave);
                //获取插入数据的ID
                for (int j = 0; j < lstEdit.Count; j++)
                {
                    //获取值
                    SalaryItemModel model = (SalaryItemModel)lstEdit[j];
                    //插入时
                    if ("0".Equals(model.EditFlag))
                    {
                        //获取插入的命令
                        SqlCommand comm = (SqlCommand)lstSave[j];
                        //设置工资项编号
                        model.ItemNo = comm.Parameters["@SalaryItemNo"].Value.ToString();
                    }
                }
            }

            return isSucc;
        }
        #endregion
       
        #region 新建工资项信息
        /// <summary>
        /// 新建工资项申请信息 
        /// </summary>
        /// <param name="model">工资项申请信息</param>
        /// <returns></returns>
        private   static SqlCommand InsertSalaryItemInfo(SalaryItemModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO          ");
            insertSql.AppendLine(" officedba.SalaryItem ");
            insertSql.AppendLine(" 	(CompanyCD          ");
            insertSql.AppendLine(" 	,ItemName           ");
            insertSql.AppendLine(" 	,ItemOrder          ");
            insertSql.AppendLine(" 	,Calculate          ");
            insertSql.AppendLine(" 	,ParamsList          ");
            insertSql.AppendLine(" 	,PayFlag            ");
            insertSql.AppendLine(" 	,ChangeFlag         ");
            insertSql.AppendLine(" 	,UsedStatus         ");
            insertSql.AppendLine(" 	,Remark)            ");
            insertSql.AppendLine(" VALUES               ");
            insertSql.AppendLine(" 	(@CompanyCD         ");
            insertSql.AppendLine(" 	,@ItemName          ");
            insertSql.AppendLine(" 	,@ItemOrder         ");
            insertSql.AppendLine(" 	,@Calculate         ");
            insertSql.AppendLine(" 	,@ParamsList          ");
            insertSql.AppendLine(" 	,@PayFlag           ");
            insertSql.AppendLine(" 	,@ChangeFlag        ");
            insertSql.AppendLine(" 	,@UsedStatus        ");
            insertSql.AppendLine(" 	,@Remark)           ");
            insertSql.AppendLine("   SET @SalaryItemNo= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@SalaryItemNo", SqlDbType.Int));

            //执行插入并返回插入结果
            return comm;
        }
        #endregion
       
        public static bool InsertSalaryItem(SalaryItemModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO          ");
            insertSql.AppendLine(" officedba.SalaryItem ");
            insertSql.AppendLine(" 	(CompanyCD          ");
            insertSql.AppendLine(" 	,ItemName           ");
            insertSql.AppendLine(" 	,ItemOrder          ");
            insertSql.AppendLine(" 	,Calculate          ");
            insertSql.AppendLine(" 	,PayFlag            ");
            insertSql.AppendLine(" 	,ChangeFlag         ");
            insertSql.AppendLine(" 	,UsedStatus         ");
            insertSql.AppendLine(" 	,Remark)            ");
            insertSql.AppendLine(" VALUES               ");
            insertSql.AppendLine(" 	(@CompanyCD         ");
            insertSql.AppendLine(" 	,@ItemName          ");
            insertSql.AppendLine(" 	,@ItemOrder         ");
            insertSql.AppendLine(" 	,@Calculate         ");
            insertSql.AppendLine(" 	,@PayFlag           ");
            insertSql.AppendLine(" 	,@ChangeFlag        ");
            insertSql.AppendLine(" 	,@UsedStatus        ");
            insertSql.AppendLine(" 	,@Remark)           ");
            //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            #endregion
            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemName", model.ItemName));//工资项名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemOrder", model.ItemOrder));//排列先后顺序
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Calculate", model.Calculate));//计算公式
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayFlag", model.PayFlag));//是否扣款(0否，1是)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeFlag", model.ChangeFlag));//是否为固定项（0否，1是）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注

            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

            return isSucc;


        }
        #region 更新工资项申请信息
        /// <summary>
        /// 更新工资项申请信息
        /// </summary>
        /// <param name="model">工资项申请信息</param>
        /// <returns></returns>
        private static SqlCommand UpdateSalaryItemInfo(SalaryItemModel model,string  CompanyCD)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.SalaryItem ");
            updateSql.AppendLine(" SET                         ");
            updateSql.AppendLine(" 	 ItemName = @ItemName      ");
            updateSql.AppendLine(" 	,ItemOrder = @ItemOrder    ");
            updateSql.AppendLine(" 	,Calculate = @Calculate    ");
            updateSql.AppendLine(" 	,ParamsList = @ParamsList    ");
            updateSql.AppendLine(" 	,PayFlag = @PayFlag        ");
            updateSql.AppendLine(" 	,ChangeFlag = @ChangeFlag  ");
            updateSql.AppendLine(" 	,UsedStatus = @UsedStatus  ");
            updateSql.AppendLine(" 	,Remark = @Remark          ");
            updateSql.AppendLine(" WHERE                       ");   
            updateSql.AppendLine("CompanyCD = @CompanyCD  and  	          ");
            updateSql.AppendLine(" 	ItemNo = @ItemNo           ");
        
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //工资项编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", model.ItemNo));
       
            //其他参数
            SetSaveParameter(comm, model);
            //执行更新
            return comm;
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, SalaryItemModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemName", model.ItemName));//工资项名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemOrder", model.ItemOrder));//排列先后顺序
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Calculate", model.Calculate));//计算公式
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayFlag", model.PayFlag));//是否扣款(0否，1是)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeFlag", model.ChangeFlag));//是否为固定项（0否，1是）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ParamsList", model.CalculateParam ));//备注
        }
        #endregion


        //public static bool DeleteInfo(string ItemNo,string companyCD)
        //{

        //    #region 插入SQL拼写
        //    StringBuilder deleteSql = new StringBuilder();
        //    deleteSql.AppendLine(" DELETE FROM officedba.SalaryItem ");
        //    deleteSql.AppendLine(" WHERE ");
        //    deleteSql.AppendLine(" ItemNo = @ItemNo");
        //    //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
        //    #endregion
        //    //定义插入基本信息的命令
        //    SqlCommand comm = new SqlCommand();
        //    comm.CommandText = insertSql.ToString();
        //    //设置保存的参数
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", ItemNo ));

        //    //添加返回参数
        //    //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

        //    //执行插入操作
        //    bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

        //    return isSucc;


        //}
        private static SqlCommand DeleteSalaryEmployeeInfo(string no, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryEmployee ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD=@CompanyCD and ItemNo = @ItemNo  ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", no));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return comm;
        }
        #region 删除工资项信息
        /// <summary>
        /// 删除工资项信息
        /// </summary>
        /// <param name="no">工资项编号</param>
        /// <returns></returns>
        private static SqlCommand DeleteSalaryItemInfo(string no, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryItem ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD=@CompanyCD  and  ItemNo = @ItemNo  ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", no));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD ));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return comm;
        }
        #endregion

        #region 校验提成工资项是否被使用
        /// <summary>
        /// 校验提成工资项是否被使用
        /// </summary>
        /// <param name="itemNo">提成工资项编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool IsUsedItem(string itemNo, string companyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                      ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount      ");
            searchSql.AppendLine(" FROM                        ");
            searchSql.AppendLine(" 	officedba.SalaryStandard   ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine(" CompanyCD=@CompanyCD and    ItemNo = @ItemNo         ");

            SqlCommand cmd = new SqlCommand();
            //设置SQL语句
            cmd.CommandText = searchSql.ToString();
            //工资项编号
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", itemNo));
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD ));
            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSearch(cmd);
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        #endregion

    }
}
