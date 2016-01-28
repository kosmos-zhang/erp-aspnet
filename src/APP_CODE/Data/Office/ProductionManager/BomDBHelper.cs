/**********************************************
 * 类作用：   物料清单数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/03
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.ProductionManager
{
    public class BomDBHelper
    {

        #region 物料清单插入
        /// <summary>
        /// 物料清单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertBom(BomModel model, string loginUserID, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  BOM添加SQL语句
                StringBuilder sqlBom = new StringBuilder();
                sqlBom.AppendLine("INSERT INTO officedba. Bom");
                sqlBom.AppendLine("(CompanyCD,BomNo,Subject,ParentNo,[Type],Verson,RouteID,Creator,CreateDate,Remark,ProductID,UnitID,");
                if (userInfo.IsMoreUnit)
                {
                    sqlBom.AppendLine("UsedUnitID,ExRate,");
                }
                sqlBom.AppendLine("UsedStatus,ModifiedDate,ModifiedUserID)");
                sqlBom.AppendLine("VALUES                  ");
                sqlBom.AppendLine("		(@CompanyCD");
                sqlBom.AppendLine("		,@BomNo");
                sqlBom.AppendLine("		,@Subject");
                sqlBom.AppendLine("		,@ParentNo");
                sqlBom.AppendLine("		,@Type");
                sqlBom.AppendLine("		,@Verson");
                sqlBom.AppendLine("		,@RouteID");
                sqlBom.AppendLine("		,@Creator");
                sqlBom.AppendLine("		,@CreateDate");
                sqlBom.AppendLine("		,@Remark");
                sqlBom.AppendLine("		,@ProductID");
                sqlBom.AppendLine("		,@UnitID");
                if (userInfo.IsMoreUnit)
                {
                    sqlBom.AppendLine("		,@UsedUnitID");
                    sqlBom.AppendLine("		,@ExRate");
                }
                sqlBom.AppendLine("		,@UsedStatus");
                sqlBom.AppendLine("		,getdate()");
                sqlBom.AppendLine("		,'" + loginUserID + "')       ");
                sqlBom.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlBom.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@BomNo", model.BomNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@ParentNo", model.ParentNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Type", model.Type));
                comm.Parameters.Add(SqlHelper.GetParameter("@Verson", model.Verson));
                comm.Parameters.Add(SqlHelper.GetParameter("@RouteID", model.RouteID));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
                comm.Parameters.Add(SqlHelper.GetParameter("@UnitID", model.UnitID));
                if (userInfo.IsMoreUnit)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", model.UsedUnitID));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ExRate", model.ExRate));
                }
                comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

                listADD.Add(comm);
                #endregion

                #region 子件信息添加SQL语句
                if (!String.IsNullOrEmpty(model.DetProductID) && !String.IsNullOrEmpty(model.DetProductType) && !String.IsNullOrEmpty(model.DetUnitID))
                {
                    string[] dtProductID = model.DetProductID.Split(',');
                    string[] dtProductType = model.DetProductType.Split(',');
                    string[] dtUnitID = model.DetUnitID.Split(',');
                    string[] dtQuota = model.DetQuota.Split(',');
                    string[] dtRateLoss = model.DetRateLoss.Split(',');
                    string[] dtIsMain = model.DetIsMain.Split(',');
                    string[] dtUsedStatus = model.DetUsedStatus.Split(',');
                    string[] dtSourceType = model.DetSourceType.Split(',');
                    string[] dtRemark = model.DetRemark.Split(',');

                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (dtProductID.Length >= 1)
                    {
                        for (int i = 0; i < dtProductID.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("insert into officedba.BomDetail");
                            cmdsql.AppendLine("(CompanyCD,");
                            cmdsql.AppendLine("BomNo,");
                            cmdsql.AppendLine("ProductID,");
                            cmdsql.AppendLine("ProductType,");
                            cmdsql.AppendLine("UnitID,");
                            if (dtQuota[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtQuota[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("Quota,");
                                }
                            }
                            if (dtRateLoss[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRateLoss[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("RateLoss,");
                                }
                            }

                            cmdsql.AppendLine("IsMain,");
                            cmdsql.AppendLine("UsedStatus,");
                            if (dtRemark[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRemark[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("Remark,");
                                }
                            }
                            cmdsql.AppendLine("SourceType)");
                            cmdsql.AppendLine(" Values(@CompanyCD");
                            cmdsql.AppendLine("            ,@BomNo");
                            cmdsql.AppendLine("            ,@ProductID");
                            cmdsql.AppendLine("            ,@ProductType");
                            cmdsql.AppendLine("            ,@UnitID");
                            if (dtQuota[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtQuota[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("            ,@Quota");
                                }
                            }
                            if (dtRateLoss[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRateLoss[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("            ,@RateLoss");
                                }
                            }
                            cmdsql.AppendLine("            ,@IsMain");
                            cmdsql.AppendLine("            ,@UsedStatus");
                            if (dtRemark[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRemark[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("            ,@Remark");
                                }
                            }
                            cmdsql.AppendLine("            ,@SourceType)");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@BomNo", model.BomNo));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductType", dtProductType[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", dtUnitID[i].ToString()));
                            if (dtQuota[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtQuota[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@Quota", dtQuota[i].ToString()));
                                }
                            }
                            if (dtRateLoss[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRateLoss[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@RateLoss", dtRateLoss[i].ToString()));
                                }
                            }

                            comms.Parameters.Add(SqlHelper.GetParameter("@IsMain", dtIsMain[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", dtUsedStatus[i].ToString()));
                            if (dtRemark[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRemark[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@Remark", dtRemark[i].ToString()));
                                }
                            }

                            comms.Parameters.Add(SqlHelper.GetParameter("@SourceType", dtSourceType[i].ToString()));
                            listADD.Add(comms);
                        }
                    }
                }
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

        #region BOM树
        /// <summary>
        /// BOM树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomTree(BomModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.BomNo,a.ID ,b.ProductName from officedba.Bom a left join officedba.ProductInfo b on a.ProductID=b.ID where a.CompanyCD=@CompanyCD and a.ParentNo=@ParentNo ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ParentNo", model.ParentNo.ToString()));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 子节点数量
        /// <summary>
        /// 判断该结点下，是否还有子结点
        /// </summary>
        /// <param name="ParentCode">上级编码</param>
        /// <returns>大于0还有子节点，否则无子节点</returns>
        public static int ChildrenCount(BomModel model)
        {
            string sql = "select count(ID) from officedba.Bom  where CompanyCD=@CompanyCD and ParentNo=@ParentNo";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            parms[1] = SqlHelper.GetParameter("@ParentNo", model.ParentNo);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            return Convert.ToInt32(obj);
        }
        #endregion

        #region 父件唯一性验证
        /// <summary>
        /// 父件唯一性验证
        /// </summary>
        /// <param name="ParentCode">上级编码</param>
        /// <returns>大于0：已经有父件引用该物品了，否则无父件引用该物品</returns>
        public static int ProductCount(BomModel model)
        {
            string sql = string.Empty;
            if (model.ID > 0)
            {
                sql = "select Count(ID) from officedba.Bom where CompanyCD=@CompanyCD and ProductID=@ProductID and ID<>@ID";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parms[1] = SqlHelper.GetParameter("@ProductID", model.ProductID);
                parms[2] = SqlHelper.GetParameter("@ID", model.ID);
                object obj = SqlHelper.ExecuteScalar(sql, parms);
                return Convert.ToInt32(obj);
            }
            else
            {
                sql = "select Count(ID) from officedba.Bom where CompanyCD=@CompanyCD and ProductID=@ProductID";
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parms[1] = SqlHelper.GetParameter("@ProductID", model.ProductID);
                object obj = SqlHelper.ExecuteScalar(sql, parms);
                return Convert.ToInt32(obj);
            }


        }
        #endregion

        #region BOM详细信息
        /// <summary>
        /// BOM详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomInfo(BomModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.CompanyCD,a.ID,a.BomNo,a.Subject,a.ParentNo,a.[Type],");
            searchSql.AppendLine("	   a.Verson,a.Creator,a.RouteID,t.RouteName,e.EmployeeName,isnull( CONVERT(CHAR(10),  a.CreateDate, 23),'') as CreateDate,");
            searchSql.AppendLine("	   a.Remark,p.ProductName,a.ProductID,a.UnitID,a.UsedUnitID,a.UsedStatus,");
            searchSql.AppendLine("	   c.CodeName as UnitName,d.CodeName as UsedUnitName,a.ExRate,b.BomNo as ParentBom");
            searchSql.AppendLine("from officedba.Bom a");
            searchSql.AppendLine("left outer join officedba.TechnicsRouting t on t.ID=a.RouteID");
            searchSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Creator");
            searchSql.AppendLine("left join officedba.ProductInfo p on p.ID=a.ProductID");
            searchSql.AppendLine("left join officedba.CodeUnitType c on c.ID=a.UnitID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            searchSql.AppendLine("left outer join officedba.Bom b on b.ID=a.ParentNo");
            searchSql.AppendLine("where a.ID=@ID");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region BOM子件详细信息
        /// <summary>
        /// BOM子件详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomSubInfo(BomModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select b.ID as DetailID,b.BomNo,b.ProductID,b.Remark,");
            searchSql.AppendLine("	   b.ProductType,c.CodeName as TypeName,b.UnitID,");
            searchSql.AppendLine("	   t.CodeName as UnitName,Convert(numeric(14," + userInfo.SelPoint + "),b.Quota) as Quota,Convert(numeric(14," + userInfo.SelPoint + "),b.RateLoss) as RateLoss,b.IsMain,");
            searchSql.AppendLine("       b.UsedStatus,b.SourceType,p.ProductName,isnull(p.Specification,'') as Specification,isnull(cbt.TypeName,'') as ColorName,isnull(cbm.TypeName,'') as MaterialName ");
            searchSql.AppendLine("from officedba.BomDetail b");
            searchSql.AppendLine("left join officedba.ProductInfo p on p.ID=b.ProductID");
            searchSql.AppendLine("left join officedba.CodeProductType c on c.ID=b.ProductType");
            searchSql.AppendLine("left join officedba.CodeUnitType t on t.ID=b.UnitID");
            searchSql.AppendLine("left join officedba.CodePublicType cbt on p.ColorID=cbt.ID");
            searchSql.AppendLine("left join officedba.CodePublicType cbm on p.Material=cbm.ID");
            searchSql.AppendLine("where b.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("	  and BomNo=(select top 1 BomNo from officedba.Bom where CompanyCD=@CompanyCD and ID=@ID)");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 修改BOM和子件信息
        /// <summary>
        /// 修改BOM和子件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateBomInfo(BomModel model, string loginUserID, string UpdateID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  BOM修改SQL语句
            StringBuilder sqlBom = new StringBuilder();
            sqlBom.AppendLine("update officedba.Bom set BomNo=@BomNo");
            sqlBom.AppendLine("                         ,Subject=@Subject");
            sqlBom.AppendLine("                         ,ParentNo=@ParentNo");
            sqlBom.AppendLine("                         ,Type=@Type");
            sqlBom.AppendLine("                         ,Verson=@Verson");
            sqlBom.AppendLine("                         ,RouteID=@RouteID");
            sqlBom.AppendLine("                         ,Remark=@Remark");
            sqlBom.AppendLine("                         ,ProductID=@ProductID");
            sqlBom.AppendLine("                         ,UnitID=@UnitID");
            if (userInfo.IsMoreUnit)
            {
                sqlBom.AppendLine("                         ,UsedUnitID=@UsedUnitID");
                sqlBom.AppendLine("                         ,ExRate=@ExRate");
            }
            sqlBom.AppendLine("                         ,UsedStatus=@UsedStatus");
            sqlBom.AppendLine("                         ,ModifiedDate=getdate()");
            sqlBom.AppendLine("                         ,ModifiedUserID='" + loginUserID + "'");
            sqlBom.AppendLine("where CompanyCD=@CompanyCD");
            sqlBom.AppendLine("and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlBom.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BomNo", model.BomNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@ParentNo", model.ParentNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Type", model.Type));
            comm.Parameters.Add(SqlHelper.GetParameter("@Verson", model.Verson));
            comm.Parameters.Add(SqlHelper.GetParameter("@RouteID", model.RouteID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@UnitID", model.UnitID));
            if (userInfo.IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", model.UsedUnitID));
                comm.Parameters.Add(SqlHelper.GetParameter("@ExRate", model.ExRate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));

            listADD.Add(comm);
            #endregion

            #region Bom子件信息更新语句
            //先删除不在BOM子件中的
            //更新子件中的ID
            //添加其它子件



            #region 先删除不在BOM子件中的
            if (!string.IsNullOrEmpty(UpdateID))
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.BomDetail where CompanyCD=@CompanyCD and BomNo=@BomNo and  ID not in(" + UpdateID + ")");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@BomNo", model.BomNo));

                listADD.Add(commDel);
            }
            #endregion

            #region 添加或更新操作
            string[] updateID = UpdateID.Split(',');
            if (!string.IsNullOrEmpty(UpdateID) && updateID.Length > 0)
            {
                string[] subProductID = model.DetProductID.Split(',');
                string[] subProductType = model.DetProductType.Split(',');
                string[] subUnitID = model.DetUnitID.Split(',');
                string[] subQuota = model.DetQuota.Split(',');
                string[] subRateLoss = model.DetRateLoss.Split(',');
                string[] subIsMain = model.DetIsMain.Split(',');
                string[] subUsedStatus = model.DetUsedStatus.Split(',');
                string[] subSourceType = model.DetSourceType.Split(',');
                string[] subRemark = model.DetRemark.Split(',');


                for (int i = 0; i < updateID.Length; i++)
                {
                    int intUpdateID = int.Parse(updateID[i].ToString());
                    if (intUpdateID > 0)
                    {
                        #region 更新子件中的ID
                        StringBuilder sqlEdit = new StringBuilder();
                        sqlEdit.AppendLine("Update officedba.BomDetail");
                        sqlEdit.AppendLine("Set ProductID=@ProductID,");
                        sqlEdit.AppendLine("	ProductType=@ProductType,");
                        sqlEdit.AppendLine("	UnitID=@UnitID,");
                        if (subQuota[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subQuota[i].ToString().Trim()))
                            {
                                sqlEdit.AppendLine("	Quota=@Quota,");
                            }
                            else
                            {
                                sqlEdit.AppendLine("	Quota=null,");
                            }
                        }
                        else
                        {
                            sqlEdit.AppendLine("	Quota=null,");
                        }
                        if (subRateLoss[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRateLoss[i].ToString().Trim()))
                            {
                                sqlEdit.AppendLine("	RateLoss=@RateLoss,");
                            }
                            else
                            {
                                sqlEdit.AppendLine("	RateLoss=null,");
                            }
                        }
                        else
                        {
                            sqlEdit.AppendLine("	RateLoss=null,");
                        }

                        sqlEdit.AppendLine("	IsMain=@IsMain,");
                        sqlEdit.AppendLine("	UsedStatus=@UsedStatus,");
                        if (subRemark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRemark[i].ToString().Trim()))
                            {
                                sqlEdit.AppendLine("	Remark=@Remark,");
                            }
                            else
                            {
                                sqlEdit.AppendLine("	Remark=null,");
                            }
                        }
                        else
                        {
                            sqlEdit.AppendLine("	Remark=null,");
                        }

                        sqlEdit.AppendLine("	SourceType=@SourceType");
                        sqlEdit.AppendLine("where CompanyCD=@CompanyCD");
                        sqlEdit.AppendLine("and ID=@ID");

                        SqlCommand commEdit = new SqlCommand();
                        commEdit.CommandText = sqlEdit.ToString();
                        commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductID", subProductID[i].ToString()));
                        commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductType", subProductType[i].ToString()));
                        commEdit.Parameters.Add(SqlHelper.GetParameter("@UnitID", subUnitID[i].ToString()));
                        if (subQuota[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subQuota[i].ToString().Trim()))
                            {
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@Quota", subQuota[i].ToString()));
                            }
                        }
                        if (subRateLoss[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRateLoss[i].ToString().Trim()))
                            {
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@RateLoss", subRateLoss[i].ToString()));
                            }
                        }


                        commEdit.Parameters.Add(SqlHelper.GetParameter("@IsMain", subIsMain[i].ToString()));
                        commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", subUsedStatus[i].ToString()));
                        commEdit.Parameters.Add(SqlHelper.GetParameter("@SourceType", subSourceType[i].ToString()));
                        if (subRemark[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRemark[i].ToString().Trim()))
                            {
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@Remark", subRemark[i].ToString()));
                            }
                        }

                        commEdit.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commEdit.Parameters.Add(SqlHelper.GetParameter("@ID", updateID[i].ToString()));
                        listADD.Add(commEdit);
                        #endregion
                    }
                    else
                    {
                        #region 添加子件中的ID
                        StringBuilder sqlIn = new StringBuilder();
                        sqlIn.AppendLine("insert into officedba.BomDetail(");
                        sqlIn.AppendLine("								CompanyCD,");
                        sqlIn.AppendLine("								BomNo,");
                        sqlIn.AppendLine("								ProductID,");
                        sqlIn.AppendLine("								ProductType,");
                        sqlIn.AppendLine("								UnitID,");
                        if (subQuota[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subQuota[i].ToString().Trim()))
                            {
                                sqlIn.AppendLine("								Quota,");
                            }
                        }
                        if (subRateLoss[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRateLoss[i].ToString().Trim()))
                            {
                                sqlIn.AppendLine("								RateLoss,");
                            }
                        }

                        sqlIn.AppendLine("								IsMain,");
                        sqlIn.AppendLine("								UsedStatus,");
                        sqlIn.AppendLine("								SourceType,");
                        sqlIn.AppendLine("								Remark)");
                        sqlIn.AppendLine("values(   @CompanyCD,");
                        sqlIn.AppendLine("		    @BomNo,");
                        sqlIn.AppendLine("		    @ProductID,");
                        sqlIn.AppendLine("		    @ProductType,");
                        sqlIn.AppendLine("		    @UnitID,");
                        if (subQuota[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subQuota[i].ToString().Trim()))
                            {
                                sqlIn.AppendLine("		    @Quota,");
                            }
                        }
                        if (subRateLoss[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRateLoss[i].ToString().Trim()))
                            {
                                sqlIn.AppendLine("		    @RateLoss,");
                            }
                        }


                        sqlIn.AppendLine("		    @IsMain,");
                        sqlIn.AppendLine("		    @UsedStatus,");
                        sqlIn.AppendLine("		    @SourceType,");
                        sqlIn.AppendLine("		    @Remark)");

                        SqlCommand commIn = new SqlCommand();
                        commIn.CommandText = sqlIn.ToString();
                        commIn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@BomNo", model.BomNo));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@ProductID", subProductID[i].ToString()));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@ProductType", subProductType[i].ToString()));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@UnitID", subUnitID[i].ToString()));
                        if (subQuota[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subQuota[i].ToString().Trim()))
                            {
                                commIn.Parameters.Add(SqlHelper.GetParameter("@Quota", subQuota[i].ToString()));
                            }
                        }
                        if (subRateLoss[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(subRateLoss[i].ToString().Trim()))
                            {
                                commIn.Parameters.Add(SqlHelper.GetParameter("@RateLoss", subRateLoss[i].ToString()));
                            }
                        }


                        commIn.Parameters.Add(SqlHelper.GetParameter("@IsMain", subIsMain[i].ToString()));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", subUsedStatus[i].ToString()));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@SourceType", subSourceType[i].ToString()));
                        commIn.Parameters.Add(SqlHelper.GetParameter("@Remark", subRemark[i].ToString()));
                        listADD.Add(commIn);
                        #endregion
                    }
                }
            }
            #endregion

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 通过检索条件查询Bom信息
        /// <summary>
        /// 通过检索条件查询Bom信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomListBycondition(BomModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("	select a.CompanyCD,a.ID,a.BomNo,e.ProductName,a.parentNo,isnull(c.BomNo,'') as ParentBom,a.Type,a.RouteID,a.UsedStatus,isnull(b.RouteName,'') as RouteName,");
            searchSql.AppendLine("  case when a.Type=0 then '工程Bom' when a.Type=1 then '生产Bom' when a.Type=2 then '销售Bom' when a.Type=3 then '成本Bom' end as strType,");
            searchSql.AppendLine("  case when a.UsedStatus=0 then '停用' when a.UsedStatus=1 then '启用' end as strUsedStatus,a.ModifiedDate ");
            searchSql.AppendLine("	from officedba.Bom a");
            searchSql.AppendLine("	left join officedba.TechnicsRouting b on b.ID=a.RouteID");
            searchSql.AppendLine("	left join officedba.Bom c on c.ID=a.ParentNo");
            searchSql.AppendLine("  left join officedba.ProductInfo e on a.ProductID=e.ID");
            searchSql.AppendLine(")as info where CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //Bom编码
            if (!string.IsNullOrEmpty(model.BomNo))
            {
                searchSql.AppendLine(" and BomNo like @BomNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BomNo", "%" + model.BomNo + "%"));
            }
            //Bom类型
            if (!string.IsNullOrEmpty(model.Type))
            {
                if (int.Parse(model.Type) > -1)
                {
                    searchSql.AppendLine(" and Type=@Type ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Type", model.Type));
                }
            }
            //工艺路线
            if (model.RouteID > 0)
            {
                searchSql.AppendLine(" and RouteID=@RouteID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RouteID", model.RouteID.ToString()));
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                if (int.Parse(model.UsedStatus) > -1)
                {
                    //启用状态
                    searchSql.AppendLine(" and UsedStatus=@UsedStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
                }
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region BOM控件查询Bom信息
        /// <summary>
        ///  BOM控件查询Bom信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomControlList(BomModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("	select a.CompanyCD,a.ID,a.BomNo,a.parentNo,isnull(c.BomNo,'') as ParentBom,a.Type,a.RouteID,a.UsedStatus,isnull(b.RouteName,'') as RouteName,d.ProductName,a.ModifiedDate ");
            searchSql.AppendLine("	from officedba.Bom a");
            searchSql.AppendLine("	left outer join officedba.TechnicsRouting b on b.ID=a.RouteID");
            searchSql.AppendLine("	left outer join officedba.Bom c on c.ID=a.ParentNo");
            searchSql.AppendLine("  left join officedba.ProductInfo d on a.ProductID=d.ID");
            if (model.ID > 0)
            {
                searchSql.AppendLine("	where a.ID not in (select ID from officedba.Bom where CompanyCD=@CompanyCD and  ParentNo=@ID) and a.ID<>@ID");
            }
            if (!string.IsNullOrEmpty(model.ProductID))
            {
                if (int.Parse(model.ProductID) > 0)
                {
                    searchSql.AppendLine("	where a.ProductID= @ProductID");
                }
            }
            searchSql.AppendLine(")as info where CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (model.ID > 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));
            }
            if (!string.IsNullOrEmpty(model.ProductID))
            {
                if (int.Parse(model.ProductID) > 0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", model.ProductID));
                }
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                if (int.Parse(model.UsedStatus) > -1)
                {
                    //启用状态
                    searchSql.AppendLine(" and UsedStatus=@UsedStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
                }
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
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
        public static int CountRefrence(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select Count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in(" + ID + ")";

            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
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

        #region 删除物料清单
        /// <summary>
        /// 删除物料清单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteBom(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.BomDetail where CompanyCD=@CompanyCD and BomNo=(select BomNo from officedba.Bom where CompanyCD=@CompanyCD and ID=@ID and ID not in(select ParentNo from officedba.Bom where CompanyCD=@CompanyCD and ParentNo=@ID))");
                    sqlBom.AppendLine("delete from officedba.Bom where CompanyCD=@CompanyCD and ID=@ID and ID not in(select ParentNo from officedba.Bom where CompanyCD=@CompanyCD and ParentNo=@ID)");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlBom.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 验证设置是否合法
        /*
        001 父件A(438), 子件a1(439),a2(440)
        002 父件a1(439),子件A(438),a2(440)
        建第二个BOM保存时需要做验证，避免物料计算时死循环.
        select count(*)as invalids from officedba.Bom a left join officedba.BomDetail b on a.BomNo=b.BomNo where a.CompanyCD='T0004' and b.ProductID=439 and a.ProductID in(438,440)
        */
        public static int CountInvalid(BomModel model)
        {
            string sql = "select count(*)as invalids from officedba.Bom a left join officedba.BomDetail b on a.BomNo=b.BomNo where a.CompanyCD=@CompanyCD and b.ProductID=@ProductID and a.ProductID in("+model.DetProductID+")";

            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@ProductID", model.ProductID);
            parms[1] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
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
