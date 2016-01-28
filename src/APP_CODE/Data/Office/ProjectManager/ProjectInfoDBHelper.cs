using System;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.ProjectManager;

namespace XBase.Data.Office.ProjectManager
{
    /// <summary>
    /// 类名：ProjectInfoDBHelper
    /// 描述：项目管理
    /// 作者：lysong
    /// 创建时间：2010/03/25
    /// </summary>
   public class ProjectInfoDBHelper
   {
       #region 获取项目信息列表
        /// <summary>
        /// 获取项目信息列表
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetProjectInfoList(string ProjectName, string ProjectNo, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "SELECT A.ID,A.ProjectNo,A.ProjectName,"
                          + "Convert(varchar(10),A.StartDate,120)StartDate,"
                          +"Convert(varchar(10),A.EndDate,120)EndDate "
                          +",A.CustID,isnull(C.CustName,'') as CustName "
                          +"FROM "
                          +"officedba.ProjectInfo A "
                          +" left join officedba.CustInfo C on C.ID=A.CustID "
                          + " WHERE A.CompanyCD=@CompanyCD ";
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(ProjectName))
            {
                sql += " and A.ProjectName like '%'+ @ProjectName +'%' ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectName", ProjectName));
            }
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                sql += " and A.ProjectNo like '%'+ @ProjectNo +'%' ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectNo", ProjectNo));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

       #region 获取项目信息
       /// <summary>
       /// 获取项目信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static DataTable GetProjectInfo(ProjectInfoModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder infoSql = new StringBuilder();
           infoSql.AppendLine("select	a.ProjectNo,a.ID,a.ProjectName,a.CustID,a.CustLinkMan,a.LinkTel,");
           infoSql.AppendLine("		CASE a.StartDate WHEN NULL THEN '' ELSE CONVERT(CHAR(10), a.StartDate,23) END as StartDate,a.LinkMan,a.Tel,");
           infoSql.AppendLine("     CASE a.EndDate WHEN NULL THEN '' ELSE CONVERT(CHAR(10), a.EndDate,23) END as EndDate,a.Address,a.OverView,a.Remark,");
           infoSql.AppendLine("		c.EmployeeName as LinkManName,Convert(numeric(14," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),a.Investment) as Investment,");
           infoSql.AppendLine("		d.CustName,a.CanViewUser,[dbo].[getEmployeeNameString](a.CanViewUser) as CanViewUserName,");
           infoSql.AppendLine("     a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
           infoSql.AppendLine("     a.ExtField11,a.ExtField12,a.ExtField13,a.ExtField14,a.ExtField15,a.ExtField16,a.ExtField17,a.ExtField18,a.ExtField19,a.ExtField20 ");
           infoSql.AppendLine("from officedba.ProjectInfo a");
           infoSql.AppendLine("left join officedba.EmployeeInfo c on a.LinkMan=c.ID");
           infoSql.AppendLine("left join officedba.CustInfo d on a.CustID=d.ID");
           infoSql.AppendLine("Where a.ID=@ID");
           //过滤单据：显示当前用户拥有权限查看的单据
           int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
           infoSql.AppendLine(" and ( charindex('," + empid + ",' , ','+a.CanViewUser+',')>0 or a.Creator=" + empid + " OR a.CanViewUser='' OR a.CanViewUser is null) ");

           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


           //指定命令的SQL文
           comm.CommandText = infoSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       #endregion

       #region 通过检索条件查询项目档案列表
       /// <summary>
       /// 通过检索条件查询项目档案列表
       /// </summary>
       /// <param name="model"></param>
       /// <param name="startDate"></param>
       /// <param name="endDate"></param>
       /// <param name="EFIndex"></param>
       /// <param name="EFDesc"></param>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="OrderBy"></param>
       /// <param name="totalCount"></param>
       /// <returns></returns>
       public static DataTable GetProjectInfoListBycondition(ProjectInfoModel model,string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select	a.ID,a.ProjectNo,a.ProjectName,a.CustID,b.CustName,");
           searchSql.AppendLine("		a.LinkMan,c.EmployeeName as LinkManName,a.ModifiedDate,");
           searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.StartDate, 23),'') as StartDate,");
           searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.EndDate, 23),'') as EndDate,Convert(numeric(14," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),a.Investment) as Investment ");
           searchSql.AppendLine("from officedba.ProjectInfo a");
           searchSql.AppendLine("left join officedba.CustInfo b on a.CustID=b.ID");
           searchSql.AppendLine("left join officedba.EmployeeInfo c on a.LinkMan=c.ID");
           searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
           int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
           searchSql.AppendLine(" and ( charindex('," + empid + ",' , ','+a.CanViewUser+',')>0 or a.Creator=" + empid + " OR a.CanViewUser='' OR a.CanViewUser is null)  ");


           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

           //单据编号
           if (!string.IsNullOrEmpty(model.ProjectNo))
           {
               searchSql.AppendLine("and a.ProjectNo like @ProjectNo");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectNo", "%" + model.ProjectNo + "%"));
           }
           //项目名称
           if (!string.IsNullOrEmpty(model.ProjectName))
           {
               searchSql.AppendLine(" and a.ProjectName like @ProjectName");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectName", "%" + model.ProjectName + "%"));
           }
           //客户ID
           if (model.CustID > 0)
           {
               searchSql.AppendLine(" and a.CustID=@CustID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
           }
           //我方负责人
           if (model.LinkMan > 0)
           {
               searchSql.AppendLine(" and a.LinkMan=@LinkMan ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@LinkMan", model.LinkMan.ToString()));
           }
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
       }
       #endregion

       #region 项目档案插入
       /// <summary>
       /// 项目档案插入
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool InsertProjectInfo(ProjectInfoModel model, Hashtable htExtAttr, out string ID, string StartDate, string EndDate)
       {

           ArrayList listADD = new ArrayList();
           bool result = false;


           #region  项目档案添加SQL语句
           StringBuilder sqlPro = new StringBuilder();
           sqlPro.AppendLine("INSERT INTO officedba.ProjectInfo");
           sqlPro.AppendLine("           (CompanyCD");
           sqlPro.AppendLine("           ,ProjectNo");
           sqlPro.AppendLine("           ,ProjectName");
           sqlPro.AppendLine("           ,CustID");
           sqlPro.AppendLine("           ,CustLinkMan");
           sqlPro.AppendLine("           ,LinkTel");
           sqlPro.AppendLine("           ,LinkMan");
           sqlPro.AppendLine("           ,Tel");
           sqlPro.AppendLine("           ,Address");
           if (!string.IsNullOrEmpty(StartDate))
           {
               sqlPro.AppendLine("           ,StartDate");
           }
           if (!string.IsNullOrEmpty(EndDate))
           {
               sqlPro.AppendLine("           ,EndDate");
           }
           sqlPro.AppendLine("           ,OverView");
           sqlPro.AppendLine("           ,Remark");
           sqlPro.AppendLine("           ,Creator");
           sqlPro.AppendLine("           ,CreateDate");
           sqlPro.AppendLine("           ,Investment");
           sqlPro.AppendLine("           ,ModifiedDate");
           sqlPro.AppendLine("           ,CanViewUser)");
           sqlPro.AppendLine("     VALUES");
           sqlPro.AppendLine("           (@CompanyCD");
           sqlPro.AppendLine("           ,@ProjectNo");
           sqlPro.AppendLine("           ,@ProjectName");
           sqlPro.AppendLine("           ,@CustID");
           sqlPro.AppendLine("           ,@CustLinkMan");
           sqlPro.AppendLine("           ,@LinkTel");
           sqlPro.AppendLine("           ,@LinkMan");
           sqlPro.AppendLine("           ,@Tel");
           sqlPro.AppendLine("           ,@Address");
           if (!string.IsNullOrEmpty(StartDate))
           {
               sqlPro.AppendLine("           ,@StartDate");
           }
           if (!string.IsNullOrEmpty(EndDate))
           {
               sqlPro.AppendLine("           ,@EndDate");
           }
           sqlPro.AppendLine("           ,@OverView");
           sqlPro.AppendLine("           ,@Remark");
           sqlPro.AppendLine("           ,@Creator");
           sqlPro.AppendLine("           ,@CreateDate");
           sqlPro.AppendLine("           ,@Investment");
           sqlPro.AppendLine("           ,getdate()");
           sqlPro.AppendLine("           ,@CanViewUser)");
           sqlPro.AppendLine("set @ID=@@IDENTITY");

           SqlCommand comm = new SqlCommand();
           comm.CommandText = sqlPro.ToString();
           comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameter("@ProjectNo", model.ProjectNo));
           comm.Parameters.Add(SqlHelper.GetParameter("@ProjectName", model.ProjectName));
           comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
           comm.Parameters.Add(SqlHelper.GetParameter("@CustLinkMan", model.CustLinkMan));
           comm.Parameters.Add(SqlHelper.GetParameter("@LinkTel", model.LinkTel));
           comm.Parameters.Add(SqlHelper.GetParameter("@LinkMan", model.LinkMan));
           comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
           comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
           if (!string.IsNullOrEmpty(StartDate))
           {
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
           }
           if (!string.IsNullOrEmpty(EndDate))
           {
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           comm.Parameters.Add(SqlHelper.GetParameter("@OverView", model.OverView));
           comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
           comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
           comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
           comm.Parameters.Add(SqlHelper.GetParameter("@Investment", model.Investment));
           comm.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", model.CanViewUser));
           comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));



           listADD.Add(comm);
           #endregion
           try
           {
               #region 扩展属性
               SqlCommand cmd = new SqlCommand();
               GetExtAttrCmd(model, htExtAttr, cmd);
               if (htExtAttr.Count > 0)
                   listADD.Add(cmd);
               #endregion

               if (SqlHelper.ExecuteTransWithArrayList(listADD))
               {
                   ID = comm.Parameters["@ID"].Value.ToString();
                   result = true;
               }
               else
               {
                   ID = "0";
               }
               return result;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 修改项目档案
       /// <summary>
       /// 修改项目档案
       /// </summary>
       /// <param name="model"></param>
       /// <param name="loginUserID"></param>
       /// <returns></returns>
       public static bool UpdateProjectInfo(ProjectInfoModel model, Hashtable htExtAttr,string StartDate,string EndDate)
       {
           //获取登陆用户ID
           ArrayList listADD = new ArrayList();

           if (model.ID <= 0)
           {
               return false;
           }

           #region  主生产计划单修改SQL语句
           StringBuilder sqlEdit = new StringBuilder();
           sqlEdit.AppendLine("UPDATE officedba.ProjectInfo");
           sqlEdit.AppendLine("   SET ProjectName = @ProjectName");
           sqlEdit.AppendLine("      ,CustID = @CustID");
           sqlEdit.AppendLine("      ,CustLinkMan = @CustLinkMan");
           sqlEdit.AppendLine("      ,LinkTel = @LinkTel");
           sqlEdit.AppendLine("      ,LinkMan = @LinkMan");
           sqlEdit.AppendLine("      ,Tel = @Tel");
           sqlEdit.AppendLine("      ,Address = @Address");
           if (!string.IsNullOrEmpty(StartDate))
           {
               sqlEdit.AppendLine("      ,StartDate = @StartDate");
           }
           if (!string.IsNullOrEmpty(EndDate))
           {
               sqlEdit.AppendLine("      ,EndDate = @EndDate");
           }
           sqlEdit.AppendLine("      ,OverView = @OverView");
           sqlEdit.AppendLine("      ,Remark = @Remark");
           sqlEdit.AppendLine("      ,CanViewUser = @CanViewUser");
           sqlEdit.AppendLine("      ,Investment = @Investment");
           sqlEdit.AppendLine("      ,ModifiedDate = getdate()");
           sqlEdit.AppendLine(" WHERE ID=@ID");



           SqlCommand comm = new SqlCommand();
           comm.CommandText = sqlEdit.ToString();
           comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
           comm.Parameters.Add(SqlHelper.GetParameter("@ProjectName", model.ProjectName));
           comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
           comm.Parameters.Add(SqlHelper.GetParameter("@CustLinkMan", model.CustLinkMan));
           comm.Parameters.Add(SqlHelper.GetParameter("@LinkTel", model.LinkTel));
           comm.Parameters.Add(SqlHelper.GetParameter("@LinkMan", model.LinkMan));
           comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
           comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
           if (!string.IsNullOrEmpty(StartDate))
           {
               comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
           }
           if (!string.IsNullOrEmpty(EndDate))
           {
               comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
           }
           comm.Parameters.Add(SqlHelper.GetParameter("@OverView", model.OverView));
           comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
           comm.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", model.CanViewUser));
           comm.Parameters.Add(SqlHelper.GetParameter("@Investment", model.Investment));


           listADD.Add(comm);
           #endregion

           #region 拓展属性
           SqlCommand cmd = new SqlCommand();
           GetExtAttrCmd(model, htExtAttr, cmd);
           if (htExtAttr.Count > 0)
               listADD.Add(cmd);
           #endregion

           return SqlHelper.ExecuteTransWithArrayList(listADD);
       }
       #endregion

       #region 扩展属性保存操作
       /// <summary>
       /// 扩展属性保存操作
       /// </summary>
       /// <returns></returns>
       private static void GetExtAttrCmd(ProjectInfoModel model, Hashtable htExtAttr, SqlCommand cmd)
       {
           try
           {
               string strSql = string.Empty;

               strSql = "UPDATE officedba.ProjectInfo set ";
               foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
               {
                   strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                   cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
               }
               int iLength = strSql.Length - 1;
               strSql = strSql.Substring(0, iLength);
               strSql += " where CompanyCD = @CompanyCD  AND ProjectNo = @ProjectNo";
               cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
               cmd.Parameters.AddWithValue("@ProjectNo", model.ProjectNo);
               cmd.CommandText = strSql;
           }
           catch (Exception)
           { }


       }
       #endregion

       #region 删除项目档案
       /// <summary>
       /// 删除项目档案
       /// </summary>
       /// <param name="ID"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static bool DeleteProjectInfo(string ID, string CompanyCD)
       {
           ArrayList listADD = new ArrayList();
           string[] arrID = ID.Split(',');
           if (arrID.Length > 0)
           {
               for (int i = 0; i < arrID.Length; i++)
               {
                   StringBuilder sqlDet = new StringBuilder();
                   sqlDet.AppendLine("delete from officedba.ProjectInfo where ID=@ID");
          

                   SqlCommand commDet = new SqlCommand();
                   commDet.CommandText = sqlDet.ToString();
                   commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                   listADD.Add(commDet);
               }
           }
           return SqlHelper.ExecuteTransWithArrayList(listADD);
       }
       #endregion

       #region 单据是否被引用
       /// <summary>
       /// 单据是否被引用
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="ID"></param>
       /// <param name="TableName"></param>
       /// <param name="ColumnName"></param>
       /// <returns></returns>
       public static int CountRefrence(string CompanyCD,string ProjectID)
       {
           StringBuilder sbSql = new StringBuilder();
           sbSql.AppendLine("select sum(CountProject) as CountProject from (");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.FeeApply where CompanyCD=@CompanyCD and ProjectID in("+ProjectID+")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.FeeReturn where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.PurchaseOrder where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.PurchaseArrive where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.PurchaseReject where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.SellOrder where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.SellSend where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.SellBack where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.StorageInOther where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.StorageOutOther where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.ManufactureTask where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.TakeMaterial where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.Fees where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.IncomeBill where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.PayBill where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.ProjectBudget where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.ProjectBaseNum where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.SubBudget where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.ProjectBudgetDetails where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.budgetSummary where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine("	union");
           sbSql.AppendLine("	select count(ProjectID) as CountProject from officedba.budgetPrice where CompanyCD=@CompanyCD and ProjectID in(" + ProjectID + ")");
           sbSql.AppendLine(") as info");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           object obj = SqlHelper.ExecuteScalar(sbSql.ToString(), parms);
           if (obj == null)
           {
               return 0;
           }
           else
           {
               return Convert.ToInt32(obj);
           }
       }
       #endregion
   }
}
