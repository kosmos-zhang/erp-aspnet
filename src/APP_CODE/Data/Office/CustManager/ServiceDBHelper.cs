using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;

namespace XBase.Data.Office.CustManager
{
    public class ServiceDBHelper
    {
        #region 添加客户服务信息的方法
        /// <summary>
        /// 添加客户服务信息的方法
        /// </summary>
        /// <param name="CustServiceM">客户服务信息</param>
        /// <returns>返回操作记录值</returns>
        public static int CustServiceAdd(CustServiceModel CustServiceM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[23];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ", CustServiceM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@ServeNo       ", CustServiceM.ServeNO);
                param[2] = SqlHelper.GetParameter("@CustID        ", CustServiceM.CustID);
                param[3] = SqlHelper.GetParameter("@CustLinkMan   ", CustServiceM.CustLinkMan);
                param[4] = SqlHelper.GetParameter("@CustLinkTel   ", CustServiceM.CustLinkTel);
                param[5] = SqlHelper.GetParameter("@Title         ", CustServiceM.Title);
                param[6] = SqlHelper.GetParameter("@ServeType     ", CustServiceM.ServeType);
                param[7] = SqlHelper.GetParameter("@Fashion       ", CustServiceM.Fashion);
                param[8] = SqlHelper.GetParameter("@State         ", CustServiceM.State);
                param[9] = SqlHelper.GetParameter("@BeginDate     ", CustServiceM.BeginDate);
                param[10] = SqlHelper.GetParameter("@DateUnit      ", CustServiceM.DateUnit);
                param[11] = SqlHelper.GetParameter("@SpendTime     ", CustServiceM.SpendTime);
                param[12] = SqlHelper.GetParameter("@OurLinkMan    ", CustServiceM.OurLinkMan);
                param[13] = SqlHelper.GetParameter("@Executant     ", CustServiceM.Executant);
                param[14] = SqlHelper.GetParameter("@Contents      ", CustServiceM.Contents);
                param[15] = SqlHelper.GetParameter("@Feedback      ", CustServiceM.Feedback);
                param[16] = SqlHelper.GetParameter("@LinkQA        ", CustServiceM.LinkQA);
                param[17] = SqlHelper.GetParameter("@Remark        ", CustServiceM.Remark);
                param[18] = SqlHelper.GetParameter("@ModifiedDate  ", CustServiceM.ModifiedDate);
                param[19] = SqlHelper.GetParameter("@ModifiedUserID", CustServiceM.ModifiedUserID);
                param[20] = SqlHelper.GetParameter("@CanViewUser", CustServiceM.CanViewUser);
                param[21] = SqlHelper.GetParameter("@CanViewUserName", CustServiceM.CanViewUserName);

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[22] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertCustService", comm, param);
                int Serviceid = Convert.ToInt32(comm.Parameters["@id"].Value);

                return Serviceid;

            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 修改客户服务信息的方法
        /// <summary>
        /// 修改客户服务信息的方法
        /// </summary>
        /// <param name="CustServiceM">服务信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateService(CustServiceModel CustServiceM)
        {
            try
            {
                #region 拼写修改联系人信息SQL语句
                StringBuilder sqlcontact = new StringBuilder();
                sqlcontact.AppendLine("UPDATE officedba.CustService set ");
                sqlcontact.AppendLine("CompanyCD = @CompanyCD, ");
                //sqlcontact.AppendLine("ServeNo = @ServeNo, ");
                sqlcontact.AppendLine("CustID = @CustID, ");
                sqlcontact.AppendLine("CustLinkMan = @CustLinkMan, ");
                sqlcontact.AppendLine("CustLinkTel = @CustLinkTel, ");
                sqlcontact.AppendLine("Title = @Title, ");
                sqlcontact.AppendLine("ServeType = @ServeType, ");
                sqlcontact.AppendLine("Fashion = @Fashion, ");
                sqlcontact.AppendLine("State = @State, ");
                sqlcontact.AppendLine("BeginDate = @BeginDate, ");
                sqlcontact.AppendLine("DateUnit = @DateUnit, ");
                sqlcontact.AppendLine("SpendTime = @SpendTime, ");
                sqlcontact.AppendLine("OurLinkMan = @OurLinkMan, ");
                sqlcontact.AppendLine("Executant = @Executant, ");
                sqlcontact.AppendLine("Contents = @Contents, ");
                sqlcontact.AppendLine("Feedback = @Feedback, ");
                sqlcontact.AppendLine("LinkQA = @LinkQA, ");
                sqlcontact.AppendLine("Remark = @Remark, ");
                sqlcontact.AppendLine("CanViewUser = @CanViewUser,    ");
                sqlcontact.AppendLine("CanViewUserName = @CanViewUserName, ");
                sqlcontact.AppendLine("ModifiedDate = @ModifiedDate, ");
                sqlcontact.AppendLine("ModifiedUserID = @ModifiedUserID ");
                sqlcontact.AppendLine(" WHERE ");
                sqlcontact.AppendLine("ID = @ID ");
                #endregion

                #region 设置修改联系人信息参数
                SqlParameter[] param = new SqlParameter[22];
                param[0] = SqlHelper.GetParameter("@ID", CustServiceM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD", CustServiceM.CompanyCD);
                //param[2] = SqlHelper.GetParameter("@ServeNo", CustServiceM.ServeNO);
                param[2] = SqlHelper.GetParameter("@CustID", CustServiceM.CustID);
                param[3] = SqlHelper.GetParameter("@CustLinkMan", CustServiceM.CustLinkMan);
                param[4] = SqlHelper.GetParameter("@CustLinkTel", CustServiceM.CustLinkTel);
                param[5] = SqlHelper.GetParameter("@Title", CustServiceM.Title);
                param[6] = SqlHelper.GetParameter("@ServeType", CustServiceM.ServeType);
                param[7] = SqlHelper.GetParameter("@Fashion", CustServiceM.Fashion);
                param[8] = SqlHelper.GetParameter("@State", CustServiceM.State);
                param[9] = SqlHelper.GetParameter("@BeginDate", CustServiceM.BeginDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustServiceM.BeginDate.ToString()));
                param[10] = SqlHelper.GetParameter("@DateUnit", CustServiceM.DateUnit);
                param[11] = SqlHelper.GetParameter("@SpendTime", CustServiceM.SpendTime);
                param[12] = SqlHelper.GetParameter("@OurLinkMan", CustServiceM.OurLinkMan);
                param[13] = SqlHelper.GetParameter("@Executant", CustServiceM.Executant);
                param[14] = SqlHelper.GetParameter("@Contents", CustServiceM.Contents);
                param[15] = SqlHelper.GetParameter("@Feedback", CustServiceM.Feedback);
                param[16] = SqlHelper.GetParameter("@LinkQA", CustServiceM.LinkQA);
                param[17] = SqlHelper.GetParameter("@Remark", CustServiceM.Remark);
                param[18] = SqlHelper.GetParameter("@ModifiedDate", CustServiceM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustServiceM.ModifiedDate.ToString()));
                param[19] = SqlHelper.GetParameter("@ModifiedUserID", CustServiceM.ModifiedUserID);
                param[20] = SqlHelper.GetParameter("@CanViewUser", CustServiceM.CanViewUser);
                param[21] = SqlHelper.GetParameter("@CanViewUserName", CustServiceM.CanViewUserName);
                #endregion

                SqlHelper.ExecuteTransSql(sqlcontact.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据查询条件获取客户服务信息
        /// <summary>
        /// 根据查询条件获取客户服务信息
        /// </summary>
        /// <param name="CustName">客户名</param>
        /// <param name="CustServiceM">服务信息</param>
        /// <param name="ServiceDateBegin">开始时间</param>
        /// <param name="ServiceDateEnd">结束时间</param>
        /// <param name="CustLinkMan">客户联系人</param>
        /// <param name="Executant">执行人</param>
        /// <returns>查询结果</returns>
        public static DataTable GetServiceInfoServerType(string ServerType, string CustName, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " s.id,s.ServeNo,s.title,CONVERT(varchar(100), s.BeginDate, 20) BeginDate,s.custid,c.custname custnam," +
                               " p.TypeName ServeType," +
                               " (case s.Fashion when '1' then '远程支持' when '2' then '现场服务' when '3' then '综合服务' end) Fashion," +
                               " e.EmployeeName,l.LinkManName  " +
                               " from " +
                                   " officedba.CustService s" +
                               " left join officedba.custinfo c on  c.id = s.custid " +
                               " left join  officedba.CodePublicType p on p.id = s.ServeType" +
                               " left join officedba.EmployeeInfo e on e.id = s.Executant" +
                               " left join officedba.CustLinkMan l on l.id = s.CustLinkMan" +
                               " where " +
                                   " s.CompanyCD = '" + CustServiceM.CompanyCD + "'";
                
                if (ServerType != "" && ServerType!="0")
                    sql += " and s.ServeType = " + ServerType + " ";            
                if (CustName != "")
                    sql += " and c.id = '" + CustName + "'";
                if (CustServiceM.ServeType != 0)
                    sql += " and p.id = " + CustServiceM.ServeType + "";
                if (CustServiceM.Fashion != 0)
                    sql += " and s.Fashion = " + CustServiceM.Fashion + "";
                if (ServiceDateBegin != "")
                    sql += " and s.BeginDate >= '" + ServiceDateBegin + "'";
                if (ServiceDateEnd != "")
                    sql += " and s.BeginDate <= '" + ServiceDateEnd + "'";
                if (CustServiceM.Title != "")
                    sql += " and s.title like '%" + CustServiceM.Title + "%'";
                if (Executant != "")
                    sql += " and e.EmployeeName like '%" + Executant + "%'";
                if (CustLinkMan != "")
                    sql += " and l.LinkManName like '%" + CustLinkMan + "%'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }


        public static DataTable GetServiceInfoByServerPerson(string ServerPerson, string CustName, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " s.id,s.ServeNo,s.title,CONVERT(varchar(100), s.BeginDate, 20) BeginDate,s.custid,c.custname custnam," +
                               " p.TypeName ServeType," +
                               " (case s.Fashion when '1' then '远程支持' when '2' then '现场服务' when '3' then '综合服务' end) Fashion," +
                               " e.EmployeeName,l.LinkManName  " +
                               " from " +
                                   " officedba.CustService s" +
                               " left join officedba.custinfo c on  c.id = s.custid " +
                               " left join  officedba.CodePublicType p on p.id = s.ServeType" +
                               " left join officedba.EmployeeInfo e on e.id = s.Executant" +
                               " left join officedba.CustLinkMan l on l.id = s.CustLinkMan" +
                               " where " +
                                   " s.CompanyCD = '" + CustServiceM.CompanyCD + "'";

                if (ServerPerson != "" && ServerPerson != "0")
                    sql += " and s.Executant = '" + ServerPerson + "'";
                if (CustName != "")
                    sql += " and c.id = '" + CustName + "'";
                if (CustServiceM.ServeType != 0)
                    sql += " and p.id = " + CustServiceM.ServeType + "";
                if (CustServiceM.Fashion != 0)
                    sql += " and s.Fashion = " + CustServiceM.Fashion + "";
                if (ServiceDateBegin != "")
                    sql += " and s.BeginDate >= '" + ServiceDateBegin + "'";
                if (ServiceDateEnd != "")
                    sql += " and s.BeginDate <= '" + ServiceDateEnd + "'";
                if (CustServiceM.Title != "")
                    sql += " and s.title like '%" + CustServiceM.Title + "%'";
                if (Executant != "")
                    sql += " and e.EmployeeName like '%" + Executant + "%'";
                if (CustLinkMan != "")
                    sql += " and l.LinkManName like '%" + CustLinkMan + "%'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }


        public static DataTable GetServiceInfoBycondition(string CanUserID, string CustName, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " s.id,s.ServeNo,s.title,CONVERT(varchar(100), s.BeginDate, 20) BeginDate,s.custid,c.custname custnam," +
                               " p.TypeName ServeType,c.CustNo,c.CustBig, c.CanViewUser,c.Manager,c.Creator," +
                               " (case s.Fashion when '1' then '远程支持' when '2' then '现场服务' when '3' then '综合服务' end) Fashion," +
                               " e.EmployeeName,l.LinkManName  " +
                               " from " +
                                   " officedba.CustService s" +
                               " left join officedba.custinfo c on  c.id = s.custid " +
                               " left join  officedba.CodePublicType p on p.id = s.ServeType" +
                               " left join officedba.EmployeeInfo e on e.id = s.Executant" +
                               " left join officedba.CustLinkMan l on l.id = s.CustLinkMan" +
                               " where " +
                                   " s.CompanyCD = '" + CustServiceM.CompanyCD + "'" +
                                   " and (s.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = s.Executant or '" + CanUserID + "' = s.OurLinkMan or s.CanViewUser = ',,' or s.CanViewUser is null )";
                if (CustName != "")
                    sql += " and c.id = '" + CustName + "'";
                if (CustServiceM.ServeType != 0)
                    sql += " and p.id = " + CustServiceM.ServeType + "";
                if (CustServiceM.Fashion != 0)
                    sql += " and s.Fashion = " + CustServiceM.Fashion + "";
                if (ServiceDateBegin != "")
                    sql += " and s.BeginDate >= '" + ServiceDateBegin + "'";
                if (ServiceDateEnd != "")
                    sql += " and s.BeginDate <= '" + ServiceDateEnd + "'";
                if (CustServiceM.Title != "")
                    sql += " and s.title like '%" + CustServiceM.Title + "%'";
                if (Executant != "")
                    sql += " and e.EmployeeName like '%" + Executant + "%'";
                if (CustLinkMan != "")
                    sql += " and l.LinkManName like '%" + CustLinkMan + "%'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
     
        #endregion

        #region 获取产品销售记录的方法
        /// <summary>
        /// 获取产品销售记录的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>查询结果</returns>
        public static DataTable GetSellAnnal(string CompanyCD, string CustID, string ProductID, string DateBegin, string DateEnd)
        {
            bool isMoreUnit=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit;
            string selPointLen = ((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).SelPoint;//小数精度
            try
            {
                #region sql语句 
                string sql = "select isnull(so.OrderNo,'') OrderNo,isnull(so.title,'') title, ";
                sql += " isnull(c.MaxCreditDate,'') MaxCreditDate,isnull(datediff(day, so.orderDate,getdate())-c.MaxCreditDate,'')days, ";
                sql += " p.ProductName,isnull(c.id,0) custid,isnull(c.custname,'') custname,so.id,";
                sql += " isnull(convert(varchar(100),convert(decimal(22," + selPointLen + "),sd.ProductCount)),'') as ProductCount,";
                sql += " isnull(convert(varchar(100),convert(decimal(22," + selPointLen + "),sd.UnitPrice)),'') UnitPrice,";//单价
                sql += " isnull(convert(varchar(100),convert(decimal(22," + selPointLen + "),sd.totalPrice)),'') TotalPrice,";//金额
                sql += " isnull(c.CustNo,'') CustNo,isnull(c.CustBig,'1') CustBig,isnull(c.CanViewUser,'')CanViewUser,isnull(c.Manager,'')Manager,isnull(c.Creator,'')Creator,";
                sql += " convert(decimal(22," + selPointLen + "),isnull(ssd.OutCount,'0')) OutCount,";
                sql += " convert(decimal(22," + selPointLen + "),isnull(ssd.BackCount,'0')) BackCount,";
                sql += " convert(decimal(22," + selPointLen + "),isnull(sd.SendCount,'0')) SendCount,CONVERT(varchar(100),so.orderDate, 23) orderDate,";
                sql += " isnull(e.EmployeeName,'') EmployeeName";
                if (isMoreUnit)
                {
                    sql += ",isnull(cu1.CodeName,'') as UnitName,isnull(cu2.CodeName,'') as UsedUnitName ";
                }
                else
                {
                    sql += ",'' as UnitName,isnull(cu1.CodeName,'') as UsedUnitName ";//基本单位，单位。为了方便handler层的处理故用这种方式
                }
                sql += " from";
                sql += " (select OrderNo,ProductID,CompanyCD,sum(SendCount) as SendCount,sum(TotalFee*Discount/100) as totalPrice";
                //多计量单位
                if (isMoreUnit)
                {
                    sql += ",UnitID,UsedUnitID";
                    sql += " ,sum(UsedUnitCount) as ProductCount ";//数量
                }
                else
                {
                    sql += ",UnitID";
                    sql += " ,sum(ProductCount) as ProductCount ";//数量
                }
                sql += " ,TaxPrice as  UnitPrice ";//含税单价
                sql += " from officedba.SellOrderDetail where CompanyCD='" + CompanyCD + "'";
                sql += " group by OrderNo,ProductID,CompanyCD,TaxPrice";
                if (isMoreUnit)
                {
                    sql += ",UnitID,UsedUnitID";
                }
                else
                { 
                    sql += ",UnitID";
                }
                sql += ") as sd ";
                sql += " left join officedba.sellorder as so on so.OrderNo=sd.OrderNo and so.CompanyCD=sd.CompanyCD ";
                sql += " left join officedba.CustInfo as c on  c.ID=so.CustID  ";
                sql += " left join officedba.EmployeeInfo e on e.id = so.Seller";
                sql += " left join officedba.productinfo as  p on  p.id=sd.productId ";
                sql += " left join (select FromBillID,productid,sum(OutCount) OutCount,sum(BackCount) BackCount ";
                sql += " from officedba.SellSendDetail where FromType='1' and CompanyCD='" + CompanyCD + "'";
                sql += " group by FromBillID,productid,CompanyCD) as ssd on ssd.FromBillID=so.id and ssd.productid=sd.productid";
                if (isMoreUnit)
                {
                    sql += " left join officedba.CodeUnitType cu1 on cu1.ID=sd.UnitID ";
                    sql += " left join officedba.CodeUnitType cu2 on cu2.ID=sd.UsedUnitID ";
                }
                else
                { 
                    sql += " left join officedba.CodeUnitType cu1 on cu1.ID=sd.UnitID ";
                }
                sql +=  " where so.orderno is not null";

                if (CustID != "")
                    sql += " and c.id  = " + CustID + "";
                if (ProductID != "")
                    sql += " and p.id = " + ProductID + "";
                if (DateBegin != "")
                    sql += " and orderDate >= '" + DateBegin.ToString() + "'";
                if (DateEnd != "")
                    sql += " and orderDate <= '" + DateEnd.ToString() + "'";              

                #endregion

                DataTable dt = SqlHelper.ExecuteSql(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取产品销售记录列表（分页）
        /// <summary>
        /// 获取产品销售记录列表（分页）
        /// </summary>
        /// <param name="userinfo">用户session信息</param>
        /// <param name="CustID">检索条件：客户</param>
        /// <param name="ProductID">检索条件：物品</param>
        /// <param name="DateBegin">检索条件：开始时间</param>
        /// <param name="DateEnd">检索条件：结束时间</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellAnnalList(XBase.Common.UserInfoUtil userinfo, string CustID, string ProductID, string DateBegin, string DateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            bool isMoreUnit = userinfo.IsMoreUnit;//是否启用多计量单位
            string selPointLen = userinfo.SelPoint;//小数位数
            string CompanyCD = userinfo.CompanyCD;//公司编码

            #region sql语句
            string sql = "select isnull(so.OrderNo,'') OrderNo,isnull(so.title,'') title, ";
            sql += " isnull(c.MaxCreditDate,'') MaxCreditDate,isnull(datediff(day, so.orderDate,getdate())-c.MaxCreditDate,'')days, ";
            sql += " p.ProductName,isnull(c.id,0) custid,isnull(c.custname,'') custname,so.id,";
            sql += " convert(decimal(22," + selPointLen + "),sd.ProductCount) as ProductCount,";
            sql += " convert(decimal(22," + selPointLen + "),sd.UnitPrice) as UnitPrice,";//单价
            sql += " convert(decimal(22," + selPointLen + "),sd.totalPrice) as TotalPrice,";//金额
            sql += " isnull(c.CustNo,'') CustNo,isnull(c.CustBig,'1') CustBig,isnull(c.CanViewUser,'')CanViewUser,isnull(c.Manager,'')Manager,isnull(c.Creator,'')Creator,";
            sql += " convert(decimal(22," + selPointLen + "),isnull(ssd.OutCount,0)) OutCount,";
            sql += " convert(decimal(22," + selPointLen + "),isnull(ssd.BackCount,0)) BackCount,";
            sql += " convert(decimal(22," + selPointLen + "),isnull(sd.SendCount,0)) SendCount,CONVERT(varchar(100),so.orderDate, 23) orderDate,";
            sql += " isnull(e.EmployeeName,'') EmployeeName";
            if (isMoreUnit)
            {
                sql += ",isnull(cu1.CodeName,'') as UnitName,isnull(cu2.CodeName,'') as UsedUnitName ";
            }
            else
            {
                sql += ",'' as UnitName,isnull(cu1.CodeName,'') as UsedUnitName ";//基本单位，单位。为了方便handler层的处理故用这种方式
            }
            sql += " from";
            sql += " (select OrderNo,ProductID,CompanyCD,sum(SendCount) as SendCount,sum(TotalFee*Discount/100) as totalPrice";
            //多计量单位
            if (isMoreUnit)
            {
                sql += ",UnitID,UsedUnitID";
                sql += " ,sum(UsedUnitCount) as ProductCount ";//数量
            }
            else
            {
                sql += ",UnitID";
                sql += " ,sum(ProductCount) as ProductCount ";//数量
            }
            sql += " ,TaxPrice as  UnitPrice ";//含税单价
            sql += " from officedba.SellOrderDetail where CompanyCD=@CompanyCD ";
            sql += " group by OrderNo,ProductID,CompanyCD,TaxPrice";
            if (isMoreUnit)
            {
                sql += ",UnitID,UsedUnitID";
            }
            else
            {
                sql += ",UnitID";
            }
            sql += ") as sd ";
            sql += " left join officedba.sellorder as so on so.OrderNo=sd.OrderNo and so.CompanyCD=sd.CompanyCD ";
            sql += " left join officedba.CustInfo as c on  c.ID=so.CustID  ";
            sql += " left join officedba.EmployeeInfo e on e.id = so.Seller";
            sql += " left join officedba.productinfo as  p on  p.id=sd.productId ";
            sql += " left join (select FromBillID,productid,sum(OutCount) OutCount,sum(BackCount) BackCount ";
            sql += " from officedba.SellSendDetail where FromType='1' and CompanyCD=@CompanyCD ";
            sql += " group by FromBillID,productid,CompanyCD) as ssd on ssd.FromBillID=so.id and ssd.productid=sd.productid";
            if (isMoreUnit)
            {
                sql += " left join officedba.CodeUnitType cu1 on cu1.ID=sd.UnitID ";
                sql += " left join officedba.CodeUnitType cu2 on cu2.ID=sd.UsedUnitID ";
            }
            else
            {
                sql += " left join officedba.CodeUnitType cu1 on cu1.ID=sd.UnitID ";
            }
            sql += " where so.orderno is not null";
            SqlCommand comm = new SqlCommand();

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            if (CustID != "")
            { 
                sql += " and c.id  = @CustID ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID.Trim()));
            }
            if (ProductID != "")
            {
                sql += " and p.id = @ProductID ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.Trim()));
            }
            if (DateBegin != "")
            {
                sql += " and orderDate >= @DateBegin ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateBegin", DateBegin.Trim()));
            }

            if (DateEnd != "")
            {
                sql += " and orderDate < dateadd(day,1,@DateEnd) ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DateEnd", DateEnd.Trim()));
            }  
            #endregion

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 根据服务ID获取此条客户服务信息
        /// <summary>
        /// 根据服务ID获取此条客户服务信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="serviceid">服务信息ID</param>
        /// <returns>返回一条结果记录</returns>
        public static DataTable GetServiceInfoByID(string CompanyCD, int serviceid)
        {
            try
            {
                string sql = "select " +
                                   " s.ID,s.ServeNo,s.custid,c.custno,c.custname,s.CustLinkMan,l.LinkManName,s.CustLinkTel,s.Title,s.ServeType," +
                                   " s.Fashion,s.State,CONVERT(varchar(100), s.BeginDate, 20) BeginDate," +
                                   "s.DateUnit,s.SpendTime," +
                                   " s.OurLinkMan,e1.EmployeeName OurLinkManName,s.Executant,e2.EmployeeName ExecutantName,s.Contents,s.Feedback,s.LinkQA,s.Remark, " +
                                   " CONVERT(varchar(100), s.ModifiedDate, 23) ModifiedDate,s.ModifiedUserID " +
                                   " ,s.CanViewUser,s.CanViewUserName " +
                               " from " +
                                   " officedba.CustService s " +
                                   " left join officedba.custinfo c on c.id = s.custid " +
                                   " left join officedba.custlinkman l on l.id = s.CustLinkMan " +
                                   " left join officedba.EmployeeInfo e1 on e1.id = s.OurLinkMan " +
                                   " left join officedba.EmployeeInfo e2 on e2.id = s.Executant " +
                               " where " +
                                   " s.id= @id " +
                               " and s.CompanyCD = @CompanyCD ";


                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", serviceid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 服务一览表_报表
        /// <summary>
        /// 服务一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceList(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select b.CustNO,b.CustName,c.EmployeeName,a.ServeNO,a.Title,substring(a.Contents,0,10) Contents , ");
                sql.Append("  d.TypeName,a.BeginDate ServeDate,  ");
                sql.Append(" (case State when '1' then '执行中' when '2' then '已完成' else '' end) State ");
                sql.Append("  from officedba. CustService as a inner join ");
                sql.Append(" officedba.CustInfo as b on a.CustId=b.Id left join ");
                sql.Append(" officedba.EmployeeInfo as c on b.Manager=c.Id left join ");
                sql.Append(" officedba.CodePublicType as d on a.ServeType=d.Id");
                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (ServeType != "")
                {
                    sql.Append(" and a.ServeType=");
                    sql.Append(ServeType);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceListPrint(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select b.CustNO,b.CustName,c.EmployeeName,a.ServeNO,a.Title,substring(a.Contents,0,10) Contents , ");
                sql.Append("  d.TypeName,a.BeginDate ServeDate,  ");
                sql.Append(" (case State when '1' then '执行中' when '2' then '已完成' else '' end) State ");
                sql.Append("  from officedba. CustService as a inner join ");
                sql.Append(" officedba.CustInfo as b on a.CustId=b.Id left join ");
                sql.Append(" officedba.EmployeeInfo as c on b.Manager=c.Id left join ");
                sql.Append(" officedba.CodePublicType as d on a.ServeType=d.Id ");
                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (ServeType != "")
                {
                    sql.Append(" and a.ServeType=");
                    sql.Append(ServeType);
                }
                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 服务次数统计_报表
        /// <summary>
        /// 服务次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustServiceCount(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName, isnull(c.TypeName,'') TypeName,isnull(b.ServeCount,'0') ServeCount  ");
                sql.Append(" from officedba.CustInfo a inner join  ");
                sql.Append(" (select count(1) ServeCount,CustId,ServeType from officedba.CustService where 1=1 ");

                if (ServeType != "")
                {
                    sql.Append(" and ServeType=");
                    sql.Append(ServeType);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by CustId,ServeType) b on a.Id=b.CustId left join ");
                sql.Append(" officedba.CodePublicType c on b.ServeType=c.Id ");

                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and a.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
               

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetCustServiceCountPrint(string CustName, string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName, isnull(c.TypeName,'') TypeName,isnull(b.ServeCount,'0') ServeCount  ");
                sql.Append(" from officedba.CustInfo a inner join  ");
                sql.Append(" (select count(1) ServeCount,CustId,ServeType from officedba.CustService where 1=1 ");

                if (ServeType != "")
                {
                    sql.Append(" and ServeType=");
                    sql.Append(ServeType);
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by CustId,ServeType) b on a.Id=b.CustId left join ");
                sql.Append(" officedba.CodePublicType c on b.ServeType=c.Id ");

                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and a.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
               
                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按服务分类统计_报表
        /// <summary>
        /// 按服务分类统计_报表
        /// </summary>
        /// <param name="TypeId">服务分类</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustServiceByType(string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  b.ServeType, a.TypeName,isnull(b.ServeCount,'0') ServeCount from ");
                sql.Append(" (select ID,TypeName from officedba.codepublictype where  TypeFlag=4 and TypeCode=7  and CompanyCD='" + CompanyCD + "') a ");
                sql.Append(" left join (select count(1) ServeCount,ServeType from officedba.CustService where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }



                sql.Append(" group by ServeType ) b ");
                sql.Append(" on a.Id=b.ServeType where 1=1 ");

                if (ServeType != "")
                {
                    sql.Append(" and a.Id=");
                    sql.Append(ServeType);
                }

                sql.Append(" and ServeCount <>0 ");


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetCustServiceByTypePrint(string ServeType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " s.id,s.ServeNo,s.ServeType,s.title,CONVERT(varchar(100), s.BeginDate, 20) BeginDate,s.custid,c.custname custnam," +
                               " p.TypeName ServeTypeName," +
                               " (case s.Fashion when '1' then '远程支持' when '2' then '现场服务' when '3' then '综合服务' end) Fashion," +
                               " e.EmployeeName,l.LinkManName  " +
                               " from " +
                                   " officedba.CustService s" +
                               " left join officedba.custinfo c on  c.id = s.custid " +
                               " left join  officedba.CodePublicType p on p.id = s.ServeType" +
                               " left join officedba.EmployeeInfo e on e.id = s.Executant" +
                               " left join officedba.CustLinkMan l on l.id = s.CustLinkMan" +
                               " where " +
                                   " s.CompanyCD = '" + CompanyCD + "'";
                if (ServeType != "" && ServeType != "0")
                    sql += " and s.ServeType = " + ServeType + " ";
                if (LinkDateBegin != "")
                    sql += " and s.BeginDate >= '" + LinkDateBegin + "'";
                if (LinkDateEnd != "")
                    sql += " and s.BeginDate <= '" + LinkDateEnd + "'";

                #endregion
                int TotalCount = 0;
                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), 1, 99999, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 按服务执行人统计_报表
        /// <summary>
        /// 按服务执行人统计_报表
        /// </summary>
        /// <param name="TypeId">执行人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceByMan(string Executant, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.EmployeeName,a.ServeCount from ");
                sql.Append(" (select count(*) ServeCount,Executant,CustId from officedba.CustService where 1=1 ");

                if (CompanyCD != "") 
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (Executant != "")
                {
                    sql.Append(" and Executant=");
                    sql.Append(Executant.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by Executant,CustId) a Inner join");
                sql.Append(" officedba.EmployeeInfo b on a.Executant=b.Id Inner join  ");
                sql.Append(" officedba.CustInfo c on a.CustId=c.Id ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceByManPrint(string Executant, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.EmployeeName,a.ServeCount from ");
                sql.Append(" (select count(*) ServeCount,Executant,CustId from officedba.CustService where 1=1 ");

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (Executant != "")
                {
                    sql.Append(" and Executant=");
                    sql.Append(Executant.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and BeginDate >= '");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("' ");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and BeginDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("') ");
                }

                sql.Append(" group by Executant,CustId) a Inner join");
                sql.Append(" officedba.EmployeeInfo b on a.Executant=b.Id Inner join  ");
                sql.Append(" officedba.CustInfo c on a.CustId=c.Id ");

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 零服务客户统计_报表
        /// <summary>
        /// 零服务客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetServiceByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustService where 1=1 ");

                if (Days != "")
                {
                    sql.Append(" and BeginDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");


                sql.Append(" and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetServiceByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustService where 1=1 ");

                if (Days != "")
                {
                    sql.Append(" and BeginDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");


                sql.Append(" and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 客户服务信息打印
        /// <summary>
        /// 客户服务信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="serviceid"></param>
        /// <returns></returns>
        public static DataTable PrintService(string CompanyCD, string serviceid)
        {
            try
            {
                string sql = "select " +
                                   " s.ID,s.ServeNo,s.custid,c.custno,c.custname,s.CustLinkMan,l.LinkManName,s.CustLinkTel,s.Title,s.ServeType,cp.TypeName ServeTypaNm, " +
                                   " (case s.Fashion when '1' then '远程支持' when '2' then '现场服务' when '3' then '综合服务' end)Fashion," +
                                   " (case s.State when '0' then '执行中' when '1' then '已完成' end)State,CONVERT(varchar(100), s.BeginDate, 20) BeginDate," +
                                   "(case s.DateUnit when '1' then '小时' when '2' then '天' when '3' then '月' end)DateUnit,s.SpendTime," +
                                   " s.OurLinkMan,e1.EmployeeName OurLinkManName,s.Executant,e2.EmployeeName ExecutantName,s.Contents,s.Feedback,s.LinkQA,s.Remark, " +
                                   " CONVERT(varchar(100), s.ModifiedDate, 23) ModifiedDate,s.ModifiedUserID,s.CanViewUserName " +
                               " from " +
                                   " officedba.CustService s " +
                                   " left join officedba.custinfo c on c.id = s.custid " +
                                   " left join officedba.codepublictype cp on cp.id = s.ServeType" +
                                   " left join officedba.custlinkman l on l.id = s.CustLinkMan " +
                                   " left join officedba.EmployeeInfo e1 on e1.id = s.OurLinkMan " +
                                   " left join officedba.EmployeeInfo e2 on e2.id = s.Executant " +
                               " where " +
                                   " s.id= @id " +
                               " and s.CompanyCD = @CompanyCD ";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", serviceid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion 

        #region 导出客户服务信息
        /// <summary>
        /// 导出客户服务信息
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="CustServiceM"></param>
        /// <param name="ServiceDateBegin"></param>
        /// <param name="ServiceDateEnd"></param>
        /// <param name="Executant"></param>
        /// <param name="CustLinkMan"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportServiceInfo(string CanUserID,string CustID, CustServiceModel CustServiceM, string ServiceDateBegin, string ServiceDateEnd, string Executant, string CustLinkMan, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " s.id,s.ServeNo,s.title,CONVERT(varchar(100), s.BeginDate, 20) BeginDate,s.custid,c.custname custnam," +
                               " p.TypeName ServeType," +
                               " (case s.Fashion when '1' then '远程支持' when '2' then '现场服务' when '3' then '综合服务' end) Fashion," +
                               " e.EmployeeName,l.LinkManName  " +
                               " from " +
                                   " officedba.CustService s" +
                               " left join officedba.custinfo c on  c.id = s.custid " +
                               " left join  officedba.CodePublicType p on p.id = s.ServeType" +
                               " left join officedba.EmployeeInfo e on e.id = s.Executant" +
                               " left join officedba.CustLinkMan l on l.id = s.CustLinkMan" +
                               " where " +
                                   " s.CompanyCD = '" + CustServiceM.CompanyCD + "'" +
                                   " and (s.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = s.Executant or '" + CanUserID + "' = s.OurLinkMan or s.CanViewUser = ',,' or s.CanViewUser is null )";
                if (CustID != "")
                    sql += " and c.id = '" + CustID + "'";
                if (CustServiceM.ServeType != 0)
                    sql += " and p.id = " + CustServiceM.ServeType + "";
                if (CustServiceM.Fashion != 0)
                    sql += " and s.Fashion = " + CustServiceM.Fashion + "";
                if (ServiceDateBegin != "")
                    sql += " and s.BeginDate >= '" + ServiceDateBegin + "'";
                if (ServiceDateEnd != "")
                    sql += " and s.BeginDate <= '" + ServiceDateEnd + "'";
                if (CustServiceM.Title != "")
                    sql += " and s.title like '%" + CustServiceM.Title + "%'";
                if (Executant != "")
                    sql += " and e.EmployeeName like '%" + Executant + "%'";
                if (CustLinkMan != "")
                    sql += " and l.LinkManName like '%" + CustLinkMan + "%'";
                #endregion

                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
