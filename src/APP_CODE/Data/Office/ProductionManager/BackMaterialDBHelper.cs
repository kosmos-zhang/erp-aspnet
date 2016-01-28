/**********************************************
 * 类作用：   退料数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/05/07
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;

namespace XBase.Data.Office.ProductionManager
{
    public class BackMaterialDBHelper
    {
        #region 领料单插入
        /// <summary>
        /// 领料单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertBackMaterial(BackMaterialModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  退料单添加SQL语句
                StringBuilder sqlBack = new StringBuilder();
                sqlBack.AppendLine("INSERT INTO officedba.BackMaterial      ");
                sqlBack.AppendLine("           (CompanyCD                   ");
                sqlBack.AppendLine("           ,BackNo                      ");
                sqlBack.AppendLine("           ,Subject                     ");
                sqlBack.AppendLine("           ,FromType                    ");
                sqlBack.AppendLine("           ,TakeID                      ");
                sqlBack.AppendLine("           ,ProcessDeptID               ");
                sqlBack.AppendLine("           ,ManufactureType             ");
                sqlBack.AppendLine("           ,SaleID                      ");
                sqlBack.AppendLine("           ,SaleDeptID                  ");
                sqlBack.AppendLine("           ,CountTotal                  ");
                sqlBack.AppendLine("           ,Taker                       ");
                sqlBack.AppendLine("           ,Remark                      ");
                sqlBack.AppendLine("           ,Creator                     ");
                sqlBack.AppendLine("           ,CreateDate                  ");
                sqlBack.AppendLine("           ,BillStatus                  ");
                sqlBack.AppendLine("           ,ModifiedDate                ");
                sqlBack.AppendLine("           ,ModifiedUserID)             ");
                sqlBack.AppendLine("     VALUES                             ");
                sqlBack.AppendLine("           (@CompanyCD                  ");
                sqlBack.AppendLine("           ,@BackNo                     ");
                sqlBack.AppendLine("           ,@Subject                    ");
                sqlBack.AppendLine("           ,@FromType                   ");
                sqlBack.AppendLine("           ,@TakeID                     ");
                sqlBack.AppendLine("           ,@ProcessDeptID              ");
                sqlBack.AppendLine("           ,@ManufactureType            ");
                sqlBack.AppendLine("           ,@SaleID                     ");
                sqlBack.AppendLine("           ,@SaleDeptID                 ");
                sqlBack.AppendLine("           ,@CountTotal                 ");
                sqlBack.AppendLine("           ,@Taker                      ");
                sqlBack.AppendLine("           ,@Remark                     ");
                sqlBack.AppendLine("           ,@Creator                    ");
                sqlBack.AppendLine("           ,@CreateDate                 ");
                sqlBack.AppendLine("           ,@BillStatus                 ");
                sqlBack.AppendLine("           ,getdate()                   ");
                sqlBack.AppendLine("           ,'"+loginUserID+"')          ");
                sqlBack.AppendLine("set @ID=@@IDENTITY                      ");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlBack.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                comm.Parameters.Add(SqlHelper.GetParameter("@TakeID", model.TakeID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProcessDeptID", model.ProcessDeptID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@SaleID", model.SaleID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@SaleDeptID", model.SaleDeptID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@Taker", model.Taker.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

                listADD.Add(comm);
                #endregion

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion

                // 退料单明细添加SQL语句
                SaveDetail(model, listADD, loginUserID);

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
        /// <summary>
        /// 保存明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        private static void SaveDetail(BackMaterialModel model, ArrayList list, string loginUserID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.DetProductID.Length < 1)
            {
                return;
            }
            string[] detSortNo = model.DetSortNo.Split(',');
            string[] detProductID = model.DetProductID.Split(',');
            string[] detStorageID = model.DetStorageID.Split(',');
            string[] detBackCount = model.DetBackCount.Split(',');
            string[] detPrice = model.DetPrice.Split(',');
            string[] detTotalPrice = model.DetTotalPrice.Split(',');
            string[] detRemark = model.DetRemark.Split(',');
            string[] detFromType = model.DetFromType.Split(',');
            string[] detFromBillID = model.DetFromBillID.Split(',');
            string[] detFromBillNo = model.DetFromBillNo.Split(',');
            string[] detUnitID = model.DetUnitID.Split(',');
            string[] detUsedUnitID = model.DetUsedUnitID.Split(',');
            string[] detUsedUnitCount = model.DetUsedUnitCount.Split(',');
            string[] detExRate = model.DetExRate.Split(',');
            string[] detUsedPrice = model.DetUsedPrice.Split(',');
            string[] detBatchNo = model.DetBatchNo.Split(',');

            for (int i = 0; i < detSortNo.Length; i++)
            {
                StringBuilder sqlDet = new StringBuilder();
                StringBuilder sqlValues = new StringBuilder();
                SqlCommand commDet = new SqlCommand();

                sqlDet.Append("INSERT INTO officedba.BackMaterialDetail");
                sqlDet.Append(" (CompanyCD,BackNo,SortNo,ModifiedDate,ModifiedUserID,Remark");
                sqlValues.AppendFormat(" VALUES(@CompanyCD,@BackNo,@SortNo,getdate(),'{0}',@Remark", loginUserID);

                commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDet.Parameters.Add(SqlHelper.GetParameter("@BackNo", model.BackNo));
                commDet.Parameters.Add(SqlHelper.GetParameter("@SortNo", detSortNo[i].ToString()));
                commDet.Parameters.Add(SqlHelper.GetParameter("@Remark", detRemark[i].ToString()));
                if (!string.IsNullOrEmpty(detProductID[i].Trim()))
                {// 产品ID
                    sqlDet.Append(",ProductID");
                    sqlValues.Append(",@ProductID");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ProductID", detProductID[i]));
                }
                if (!string.IsNullOrEmpty(detStorageID[i].Trim()))
                {// 仓库ID
                    sqlDet.Append(",StorageID");
                    sqlValues.Append(",@StorageID");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@StorageID", detStorageID[i]));
                }

                if (!string.IsNullOrEmpty(detPrice[i].Trim()))
                {//单价
                    sqlDet.Append(",Price");
                    sqlValues.Append(",@Price");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@Price", detPrice[i]));
                }
                if (!string.IsNullOrEmpty(detTotalPrice[i].Trim()))
                {//金额
                    sqlDet.Append(",TotalPrice");
                    sqlValues.Append(",@TotalPrice");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", detTotalPrice[i]));
                }
                if (!string.IsNullOrEmpty(detFromType[i].Trim()))
                {// 源单类型
                    sqlDet.Append(",FromType");
                    sqlValues.Append(",@FromType");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@FromType", detFromType[i]));
                }
                if (!string.IsNullOrEmpty(detFromBillID[i].Trim()))
                {// 源单编码
                    sqlDet.Append(",FromBillID");
                    sqlValues.Append(",@FromBillID");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@FromBillID", detFromBillID[i]));
                }
                if (!string.IsNullOrEmpty(detFromBillNo[i].Trim()))
                {// 源单编号
                    sqlDet.Append(",FromBillNo");
                    sqlValues.Append(",@FromBillNo");
                    commDet.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", detFromBillNo[i]));
                }
                if (userInfo.IsMoreUnit)
                {
                    if (!string.IsNullOrEmpty(detUnitID[i].Trim()))
                    {//基本单位
                        sqlDet.Append(",UnitID");
                        sqlValues.Append(",@UnitID");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@UnitID", detUnitID[i]));
                    }
                    if (!string.IsNullOrEmpty(detBackCount[i].Trim()))
                    {//基本数量
                        sqlDet.Append(",BackCount");
                        sqlValues.Append(",@BackCount");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@BackCount", detBackCount[i]));
                    }
                    if (!string.IsNullOrEmpty(detUsedUnitID[i].Trim()))
                    {//单位
                        sqlDet.Append(",UsedUnitID");
                        sqlValues.Append(",@UsedUnitID");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", detUsedUnitID[i]));
                    }
                    if (!string.IsNullOrEmpty(detUsedUnitCount[i].Trim()))
                    {//数量
                        sqlDet.Append(",UsedUnitCount");
                        sqlValues.Append(",@UsedUnitCount");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", detUsedUnitCount[i]));
                    }
                    if (!string.IsNullOrEmpty(detExRate[i].Trim()))
                    {//换算率
                        sqlDet.Append(",ExRate");
                        sqlValues.Append(",@ExRate");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@ExRate", detExRate[i]));
                    }
                    if (!string.IsNullOrEmpty(detUsedPrice[i].Trim()))
                    {//单价
                        sqlDet.Append(",UsedPrice");
                        sqlValues.Append(",@UsedPrice");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", detUsedPrice[i]));
                    }
                    if (!string.IsNullOrEmpty(detBatchNo[i].Trim()))
                    {//批次
                        sqlDet.Append(",BatchNo");
                        sqlValues.Append(",@BatchNo");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@BatchNo", detBatchNo[i]));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(detUnitID[i].Trim()))
                    {//单位
                        sqlDet.Append(",UnitID");
                        sqlValues.Append(",@UnitID");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@UnitID", detUnitID[i]));
                    }
                    if (!string.IsNullOrEmpty(detBackCount[i].Trim()))
                    {//数量
                        sqlDet.Append(",BackCount");
                        sqlValues.Append(",@BackCount");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@BackCount", detBackCount[i]));
                    }
                }
                //if (detUnitID.Length == detProductID.Length && !string.IsNullOrEmpty(detUnitID[i].Split('|')[0].Trim()))
                //{// 单位
                //    sqlDet.Append(",UnitID");
                //    sqlValues.Append(",@UnitID");
                //    commDet.Parameters.Add(SqlHelper.GetParameter("@UnitID", detUnitID[i].Split('|')[0]));
                //}
                //if (detUnitID.Length == detProductID.Length && detUnitID[i].Split('|').Length == 2 && !string.IsNullOrEmpty(detUnitID[i].Split('|')[1].Trim()))
                //{// 单位换算率
                //    sqlDet.Append(",ExRate");
                //    sqlValues.Append(",@ExRate");
                //    commDet.Parameters.Add(SqlHelper.GetParameter("@ExRate", detUnitID[i].Split('|')[1]));
                //}
                //if (detUsedUnitCount.Length == detProductID.Length && !string.IsNullOrEmpty(detUsedUnitCount[i].Trim()))
                //{// 基本数量
                //    sqlDet.Append(",UsedUnitCount");
                //    sqlValues.Append(",@UsedUnitCount");
                //    commDet.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", detUsedUnitCount[i]));
                //}
                sqlDet.Append(")");
                sqlValues.Append(")");
                commDet.CommandText = sqlDet.ToString() + sqlValues.ToString();

                list.Add(commDet);
            }
        }
        #endregion

        #region 退料单详细信息
        /// <summary>
        /// 退料单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBackInfo(BackMaterialModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("Select * From (");
            infoSql.AppendLine("				select	a.CompanyCD,a.ID,a.BackNo,a.Subject,a.FromType,a.TakeID,b.TakeNo,");
            infoSql.AppendLine("                        case when a.FromType=0 then '无来源' when a.FromType=1 then '领料单' end as strFromType,");
            infoSql.AppendLine("						a.ProcessDeptID,j.DeptName as ProcessDeptName,a.ManufactureType,a.SaleID,c.EmployeeName as SaleReal,");
            infoSql.AppendLine("                        case when a.ManufactureType=0 then '普通' when a.ManufactureType=1 then '返修' when a.ManufactureType=2 then '拆件' end as strManufactureType,");
            infoSql.AppendLine("						a.SaleDeptID,d.DeptName,Convert(numeric(22," + userInfo.SelPoint+ "),a.CountTotal) as CountTotal,a.Taker,e.EmployeeName as TakerReal,");
            infoSql.AppendLine("						a.Receiver,f.EmployeeName as ReceiverReal,isnull( CONVERT(CHAR(10), a.ReceiveDate, 23),'') as ReceiveDate,a.Remark,a.Creator,g.EmployeeName as CreatorReal,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,a.BillStatus,a.Confirmor,h.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("                        case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("						a.Closer,i.EmployeeName as CloserReal,isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,");
            infoSql.AppendLine("                        a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID");
            infoSql.AppendLine("				from officedba.BackMaterial a");
            infoSql.AppendLine("				left join officedba.TakeMaterial b on a.TakeID=b.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo c on a.SaleID=c.ID");
            infoSql.AppendLine("				left join officedba.DeptInfo d on a.SaleDeptID=d.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo e on a.Taker=e.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo f on a.Receiver=f.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo g on a.Creator=g.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo h on a.Confirmor=h.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo i on a.Closer=i.ID");
            infoSql.AppendLine("                left join officedba.DeptInfo j on a.ProcessDeptID=j.ID");
            infoSql.AppendLine("			   )as info");
            infoSql.AppendLine("Where ID=@ID");

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

        #region 修改退料单和各明细信息
        /// <summary>
        /// 修改退料单和各明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool UpdateBackMaterialInfo(BackMaterialModel model, Hashtable htExtAttr, string loginUserID)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  退料单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.BackMaterial                   ");
            sqlEdit.AppendLine("   SET CompanyCD = @CompanyCD                    ");
            sqlEdit.AppendLine("      ,Subject = @Subject                        ");
            sqlEdit.AppendLine("      ,FromType = @FromType                      ");
            sqlEdit.AppendLine("      ,TakeID = @TakeID                          ");
            sqlEdit.AppendLine("      ,ProcessDeptID = @ProcessDeptID            ");
            sqlEdit.AppendLine("      ,ManufactureType = @ManufactureType        ");
            sqlEdit.AppendLine("      ,SaleID = @SaleID                          ");
            sqlEdit.AppendLine("      ,SaleDeptID = @SaleDeptID                  ");
            sqlEdit.AppendLine("      ,CountTotal = @CountTotal                  ");
            sqlEdit.AppendLine("      ,Taker = @Taker                            ");
            sqlEdit.AppendLine("      ,Remark = @Remark                          ");
            if (int.Parse(model.BillStatus) == 2)
            {
                sqlEdit.AppendLine("      ,BillStatus = 3                   ");
            }
            sqlEdit.AppendLine("      ,Confirmor=null                       ");
            sqlEdit.AppendLine("      ,ConfirmDate=null                     ");
            sqlEdit.AppendLine("      ,ModifiedDate = getdate()                 ");
            sqlEdit.AppendLine("      ,ModifiedUserID = '" + loginUserID + "'   ");
            sqlEdit.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID          ");



            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            if (model.FromType == "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@TakeID", "0"));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@TakeID", model.TakeID));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@ProcessDeptID", model.ProcessDeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SaleID", model.SaleID));
            comm.Parameters.Add(SqlHelper.GetParameter("@SaleDeptID", model.SaleDeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Taker", model.Taker));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));

            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 退料单明细处理

            #region 删除退料单明细
            StringBuilder sqlProductDel = new StringBuilder();
            sqlProductDel.AppendLine("delete from officedba.BackMaterialDetail ");
            sqlProductDel.AppendLine("where CompanyCD=@CompanyCD");
            sqlProductDel.AppendLine("and BackNo=(");
            sqlProductDel.AppendLine("				select top 1 BackNo from officedba.BackMaterial where CompanyCD=@CompanyCD and ID=@ID");
            sqlProductDel.AppendLine("			    )");

            SqlCommand commProductDel = new SqlCommand();
            commProductDel.CommandText = sqlProductDel.ToString();
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            listADD.Add(commProductDel);
            #endregion

            // 退料单明细添加SQL语句
            SaveDetail(model, listADD, loginUserID);

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 退料单明细详细信息
        /// <summary>
        /// 退料单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBackDetailInfo(BackMaterialModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("				select	a.CompanyCD,a.ID as DetailID,a.BackNo,a.SortNo,a.ProductID,a.FromBillID,a.FromBillNo,b.ProdNo,b.ProductName,a.StorageID,e.StorageName,");
            detSql.AppendLine("                     case when a.FromType=0 then '无来源' when a.FromType=1 then '领料单' end as strFromType,");
            detSql.AppendLine("						b.UnitID,c.CodeName as UnitName,a.FromType,Convert(numeric(14,"+userInfo.SelPoint+"),f.TakeCount) as TakeCount,Convert(numeric(14,"+userInfo.SelPoint+"),isnull(f.BackCount,0)) as TakeBackCount,");
            detSql.AppendLine("						Convert(numeric(14,"+userInfo.SelPoint+"),a.BackCount) as BackCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.Price) as Price,Convert(numeric(12,"+userInfo.SelPoint+"),a.TotalPrice) as TotalPrice,a.Remark,");
            detSql.AppendLine("                     a.UsedUnitID,u.CodeName as UsedUnitName,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedPrice) as UsedPrice,a.BatchNo,b.IsBatchNo");
            detSql.AppendLine("				from officedba.BackMaterialDetail a");
            detSql.AppendLine("				left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("				left join officedba.CodeUnitType c on b.UnitID=c.ID");
            detSql.AppendLine("             left join officedba.CodeUnitType u on a.UsedUnitID=u.ID");
            detSql.AppendLine("				left join officedba.TakeMaterialDetail f on a.FromBillNo=f.TakeNo and a.FromBillID=f.ID");
            detSql.AppendLine("				left join officedba.StorageInfo e on a.StorageID=e.ID");
            detSql.AppendLine("				where a.CompanyCD=@CompanyCD and BackNo=(select top 1 BackNo from officedba.BackMaterial where ID=@ID)");
            detSql.AppendLine(") as info");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 通过检索条件查询领料单信息
        /// <summary>
        /// 通过检索条件查询领料单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable GetBackMaterialListBycondition(BackMaterialModel model, string ReceiveDateStart, string ReceiveDateEnd, int BillTypeFlag, int BillTypeCode, int FlowStatus, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("				select	a.CompanyCD,a.ID,a.BackNo,a.Subject,isnull( CONVERT(CHAR(10), a.ReceiveDate, 23),'') as ReceiveDate,");
            searchSql.AppendLine("						a.FromType,a.TakeID,isnull(b.TakeNo,'')as TakeNo,a.ProcessDeptID,isnull(c.DeptName,'')as ProcessDeptName,");
            searchSql.AppendLine("                      case when a.FromType=0 then '无来源' when a.FromType=1 then '领料单' end as strFromType,");
            searchSql.AppendLine("						a.ManufactureType,Convert(numeric(10,2),a.CountTotal) as CountTotal,a.Taker,");
            searchSql.AppendLine("                      case when a.ManufactureType=0 then '普通' when a.ManufactureType=1 then '返修' when a.ManufactureType=2 then '拆件' end as strManufactureType,");
            searchSql.AppendLine("						isnull(d.EmployeeName,'')as TakerReal,isnull(a.Receiver,'')as Receiver,isnull(e.EmployeeName,'')as ReceiverReal,");
            searchSql.AppendLine("						a.BillStatus,isnull(f.FlowStatus,'0')as FlowStatus,a.ModifiedDate,f.ID as InstanceID,");
            searchSql.AppendLine("                      a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("              case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            searchSql.AppendLine("              case when f.FlowStatus=1 then '待审批' when f.FlowStatus=2 then '审批中' when f.FlowStatus=3 then '审批通过' when f.FlowStatus=4 then '审批不通过' when f.FlowStatus=5 then '撤消审批' end as strFlowStatus ");
            searchSql.AppendLine("				from officedba.BackMaterial a");
            searchSql.AppendLine("				left join officedba.TakeMaterial b on a.TakeID=b.ID");
            searchSql.AppendLine("				left join officedba.DeptInfo c on a.ProcessDeptID=c.ID");
            searchSql.AppendLine("				left join officedba.EmployeeInfo d on a.Taker=d.ID");
            searchSql.AppendLine("				left join officedba.EmployeeInfo e on a.Receiver=e.ID");
            searchSql.AppendLine("				LEFT JOIN officedba.FlowInstance f ON a.ID=f.BillID and f.BillTypeFlag=@BillTypeFlag and f.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine(" and          f.ID=( ");
            searchSql.AppendLine("                      select  max(ID)");
            searchSql.AppendLine("                      from  officedba.FlowInstance H");
            searchSql.AppendLine("                      where   H.CompanyCD = A.CompanyCD");
            searchSql.AppendLine("                      and H.BillID = A.ID");
            searchSql.AppendLine("                      and H.BillTypeFlag =@BillTypeFlag");
            searchSql.AppendLine("                      and H.BillTypeCode =@BillTypeCode)");
            searchSql.AppendLine("			   )as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD");
            if (FlowStatus > 0)
            {
                searchSql.AppendLine("and InstanceID in(select max(ID) from officedba.FlowInstance where BillTypeFlag=@BillTypeFlag  and BillTypeCode=@BillTypeCode  group by BillNo)");
            }


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));

            //单据编号
            if (!string.IsNullOrEmpty(model.BackNo))
            {
                searchSql.AppendLine("and BackNo like @BackNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackNo", "%" + model.BackNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }
            //源单类型
            if (!string.IsNullOrEmpty(model.FromType))
            {
                if (int.Parse(model.FromType) > -1)
                {
                    searchSql.AppendLine(" and FromType=@FromType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", model.FromType));
                }
            }
            //生产任务单
            if (model.TakeID > 0)
            {
                searchSql.AppendLine(" and TakeID=@TakeID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeID", model.TakeID.ToString()));
            }
            //生产部门
            if (model.ProcessDeptID > 0)
            {
                searchSql.AppendLine(" and ProcessDeptID=@ProcessDeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProcessDeptID", model.ProcessDeptID.ToString()));
            }
            //领料人
            if (model.Taker > 0)
            {
                searchSql.AppendLine(" and a.Taker=@Taker ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taker", model.Taker.ToString()));
            }
            //收料人
            if (model.Receiver > 0)
            {
                searchSql.AppendLine(" and Receiver=@Receiver ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Receiver", model.Receiver.ToString()));
            }
            //收料起始日期
            if (!string.IsNullOrEmpty(ReceiveDateStart))
            {
                searchSql.AppendLine(" and ReceiveDate>=@ReceiveDateStart ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReceiveDateStart", ReceiveDateStart));
            }
            //收料截止日期
            if (!string.IsNullOrEmpty(ReceiveDateEnd))
            {
                searchSql.AppendLine(" and ReceiveDate<=@ReceiveDateEnd ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReceiveDateEnd", ReceiveDateEnd));
            }
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                }
            }
            //审批状态
            if (FlowStatus > -1)
            {
                searchSql.AppendLine("and FlowStatus=@FlowStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus.ToString()));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 删除退料单
        /// <summary>
        /// 删除退料单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteBackMaterial(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.BackMaterialDetail where CompanyCD=@CompanyCD and BackNo=(select BackNo from officedba.BackMaterial where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.BackMaterial where CompanyCD=@CompanyCD and ID=@ID");

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

        #region 收料
        /// <summary>
        /// 收料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ReceiveBackMaterial(BackMaterialModel model, out string reason)
        {
            //1.从退料单中取出明细判断源单是领料单还是无源单
            //2.如果是领料单更新领料单中的退料数量,更新库存表，增加库存量
            //3.如果是无源单，更新库存表，增加库存量
            //4.更新退料单中的收料人和收料日期
            ArrayList listADD = new ArrayList();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            int tempTotalSucceed = 0;
            bool tempOperateResult = false;
            string tempReason = "";
            string intBack = "";
            DataTable dbBack = GetBackInfo(model);
            if (dbBack.Rows.Count > 0)
            {
                intBack = dbBack.Rows[0]["ID"].ToString();
                if (!string.IsNullOrEmpty(dbBack.Rows[0]["Receiver"].ToString()))
                {
                    reason = "已经收过料了";
                }
                else
                {
                    #region 更新领料单中的退料数量
                    DataTable dtBackDetail = GetBackDetailInfo(model);
                    if (dtBackDetail.Rows.Count > 0)
                    {
                        #region 明细处理
                        for (int i = 0; i < dtBackDetail.Rows.Count; i++)
                        {
                            int intFromType = int.Parse(dtBackDetail.Rows[i]["FromType"].ToString());       //源单类型
                            int ProductID = int.Parse(dtBackDetail.Rows[i]["ProductID"].ToString());        //产品ID    
                            int StorageID = int.Parse(dtBackDetail.Rows[i]["StorageID"].ToString());        //仓库ID
                            Decimal BackCount = Decimal.Parse(dtBackDetail.Rows[i]["BackCount"].ToString());//退料数量
                            int FromBillID = int.Parse(dtBackDetail.Rows[i]["FromBillID"].ToString());       //源单ID
                            string FromBillNo = dtBackDetail.Rows[i]["FromBillNo"].ToString();               //源单编号
                            string BatchNo = dtBackDetail.Rows[i]["BatchNo"].ToString();                     //批次
                            if (string.IsNullOrEmpty(BatchNo))
                            {
                                BatchNo = "";
                            }
                            string BackNo = dtBackDetail.Rows[i]["BackNo"].ToString();
                            decimal Price = decimal.Parse(dtBackDetail.Rows[i]["Price"].ToString());
                            decimal productCount = GetStorageProductCount(model.CompanyCD, ProductID, StorageID, BatchNo);/*现有存量*/

                            #region 1.是领料单的，更新领料单中退料数量
                            if (intFromType == 1)
                            {

                                int intInStorageProduct = InStorageProductCount(model.CompanyCD, ProductID, StorageID,BatchNo);
                                if (intInStorageProduct > 0)
                                {
                                    //判断当前所选的物品，在选择的对应的仓库和批次中是否存在

                                    StringBuilder sqlUpdate = new StringBuilder();
                                    sqlUpdate.AppendLine("update officedba.TakeMaterialDetail set BackCount=isnull(BackCount,0)+@BackCount where ID=@FromBillID and TakeNo=@FromBillNo and CompanyCD=@CompanyCD");

                                    SqlCommand comUpdate = new SqlCommand();
                                    comUpdate.CommandText = sqlUpdate.ToString();
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo));
                                    listADD.Add(comUpdate);
                                    tempTotalSucceed++;
                                }
                                else
                                {
                                    tempReason = tempReason + "第" + (i + 1) + "行中的物品在当前所选的仓库中不存在<br>";
                                }

                            }
                            else
                            {
                                tempTotalSucceed++;
                            }
                            #endregion

                            #region 2.更新库存
                            StringBuilder sqlSto = new StringBuilder();
                            sqlSto.AppendLine("update officedba.StorageProduct set ProductCount =isnull(ProductCount,0)+@BackCount where CompanyCD=@CompanyCD and StorageID=@StorageID and ProductID=@ProductID and isnull(BatchNo,'')=@BatchNo");

                            SqlCommand comSto = new SqlCommand();
                            comSto.CommandText = sqlSto.ToString();
                            comSto.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comSto.Parameters.Add(SqlHelper.GetParameter("@StorageID", StorageID));
                            comSto.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
                            comSto.Parameters.Add(SqlHelper.GetParameter("@BackCount", BackCount));
                            comSto.Parameters.Add(SqlHelper.GetParameter("@BatchNo", BatchNo));
                            listADD.Add(comSto);
                            #endregion

                            #region 3.写入数据到库存流水账表
                            StorageAccountModel modelSA = new StorageAccountModel();
                            modelSA.CompanyCD = model.CompanyCD;
                            modelSA.BillType = ConstUtil.STORAGEACCOUNT_BILLTYPE_BACK;                      /*单据类型*/
                            modelSA.ProductID = ProductID;                                                  /*物品ID*/
                            modelSA.StorageID = StorageID;                                                  /*仓库ID*/
                            modelSA.BatchNo = BatchNo;                                                      /*批次*/
                            modelSA.BillNo = BackNo;                                                        /*单据编号*/
                            modelSA.HappenCount = BackCount;                                                /*操作数量*/
                            modelSA.Creator = userInfo.EmployeeID;                                          /*操作人*/
                            modelSA.Price = Price;                                                          /*单价*/
                            modelSA.ProductCount = productCount + BackCount;                                /*现有存量 =原有的现有存量 +退料数量*/
                            modelSA.PageUrl = "Pages/Office/ProductionManager/BackMaterial_Add.aspx?ModuleID=" + ConstUtil.MODULE_ID_BACKMATERIAL_EDIT + "&intBackID=" + intBack;/*页面URL*/

                            listADD.Add(StorageAccountDBHelper.InsertStorageAccountBySqlCommand(modelSA));
                            #endregion
                        }
                        #endregion

                        #region 4.更新退料单
                        if (dtBackDetail.Rows.Count > 0 && dtBackDetail.Rows.Count == tempTotalSucceed)
                        {

                            StringBuilder sqlBack = new StringBuilder();
                            sqlBack.AppendLine("update officedba.BackMaterial set Receiver=@Receiver,ReceiveDate=@ReceiveDate where CompanyCD=@CompanyCD and ID=@ID");

                            SqlCommand comBack = new SqlCommand();
                            comBack.CommandText = sqlBack.ToString();
                            comBack.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comBack.Parameters.Add(SqlHelper.GetParameter("@Receiver", model.Receiver));
                            comBack.Parameters.Add(SqlHelper.GetParameter("@ReceiveDate", model.ReceiveDate));
                            comBack.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                            listADD.Add(comBack);

                            if (SqlHelper.ExecuteTransWithArrayList(listADD))
                            {
                                tempOperateResult = true;
                            }

                        }
                        #endregion

                        reason = tempReason;
                    }
                    else
                    {
                        reason = "退料单明细中没有对应的物品需要收料";
                    }
                    #endregion
                }
            }
            else
            {
                reason = "没有找到相关记录";
            }



            return tempOperateResult;
        }
        #endregion

        #region 判断物品在分仓存量表里是否存在
        public static int InStorageProductCount(string CompanyCD, int intProductID, int intStorageID,string BatchNo)
        {
            string sql = "select count(*) from officedba.StorageProduct where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD  and isnull(BatchNo,'')=@BatchNo";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            parms[1] = SqlHelper.GetParameter("@StorageID", intStorageID);
            parms[2] = SqlHelper.GetParameter("@ProductID", intProductID);
            parms[3] = SqlHelper.GetParameter("@BatchNo", BatchNo);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            return Convert.ToInt32(obj);
        }
        #endregion

        #region 现有存量
        public static decimal GetStorageProductCount(string CompanyCD, int intProductID, int intStorageID, string BatchNo)
        {
            string sql = "select Convert(numeric(10,2),isnull(ProductCount,0)) as ProductCount from officedba.StorageProduct where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD and isnull(BatchNo,'')=@BatchNo";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            parms[1] = SqlHelper.GetParameter("@StorageID", intStorageID);
            parms[2] = SqlHelper.GetParameter("@ProductID", intProductID);
            parms[3] = SqlHelper.GetParameter("@BatchNo", BatchNo);
            object obj = SqlHelper.ExecuteScalar(sql, parms);

            if (obj == null)
            {
                return Convert.ToDecimal("0");
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        #endregion

        #region 确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteBackMaterial(BackMaterialModel model, string loginUserID, int OperateType)
        {
            if (OperateType == 1)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.BackMaterial SET");
                sql.AppendLine(" Confirmor         = @Confirmor,");
                sql.AppendLine(" ConfirmDate        = @ConfirmDate,");
                sql.AppendLine(" ModifiedDate   = getdate(),");
                sql.AppendLine(" BillStatus   = 2,");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");


                SqlParameter[] param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@ID", model.ID);
                param[1] = SqlHelper.GetParameter("@Confirmor", model.Confirmor);
                param[2] = SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate);
                param[3] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            else if (OperateType == 2)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.BackMaterial SET");
                sql.AppendLine(" Closer         = @Closer,");
                sql.AppendLine(" CloseDate   = @CloseDate,");
                sql.AppendLine(" BillStatus   = 4,");
                sql.AppendLine(" ModifiedDate   = getdate(),");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");


                SqlParameter[] param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@ID", model.ID);
                param[1] = SqlHelper.GetParameter("@Closer", model.Closer);
                param[2] = SqlHelper.GetParameter("@CloseDate", model.CloseDate);
                param[3] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" update officedba.BackMaterial set Closer=null,CloseDate=null,ModifiedDate=getdate(),BillStatus=2,ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");


                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@ID", model.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool CancelConfirmOperate(BackMaterialModel model, int BillTypeFlag, int BillTypeCode, string loginUserID)
        {
            ArrayList listADD = new ArrayList();

            //#region 传参
            try
            {
                #region 撤消审批流程
                #region 撤消审批处理逻辑描述
                //可参见撤消审批的存储过程[FlowApproval_Update],个别的判断去掉

                //--1.往流程任务历史记录表（officedba.FlowTaskHistory）插1条处理记录，
                //--记录的步骤序号为0（表示返回到流程提交人环节)，审批状态为撤销审批   
                //Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)
                //Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())

                //--2.更新流程任务处理表（officedba.FlowTaskList）中的流程步骤序号为0（表示返回到流程提交人环节）
                //Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID
                //Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID

                //--3更新流程实例表（officedba.FlowInstance）中的流程状态为“撤销审批”
                //Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID 
                //Where CompanyCD=@CompanyCD 
                //and FlowNo=@tempFlowNo 
                //and BillTypeFlag=@BillTypeFlag 
                //and BillTypeCode=@BillTypeCode 
                //and BillID=@BillID
                #endregion


                DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(model.CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
                if (dtFlowInstance.Rows.Count > 0)
                {
                    //提交审批了的单据
                    string FlowInstanceID = dtFlowInstance.Rows[0]["FlowInstanceID"].ToString();
                    string FlowStatus = dtFlowInstance.Rows[0]["FlowStatus"].ToString();
                    string FlowNo = dtFlowInstance.Rows[0]["FlowNo"].ToString();

                    #region 往流程任务历史记录表
                    StringBuilder sqlHis = new StringBuilder();
                    sqlHis.AppendLine("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)");
                    sqlHis.AppendLine("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");


                    SqlCommand commHis = new SqlCommand();
                    commHis.CommandText = sqlHis.ToString();
                    commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                    listADD.Add(commHis);
                    #endregion

                    #region 更新流程任务处理表
                    StringBuilder sqlTask = new StringBuilder();
                    sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                    sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                    SqlCommand commTask = new SqlCommand();
                    commTask.CommandText = sqlTask.ToString();
                    commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                    listADD.Add(commTask);
                    #endregion

                    #region 更新流程实例表
                    StringBuilder sqlIns = new StringBuilder();
                    sqlIns.AppendLine("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
                    sqlIns.AppendLine("Where CompanyCD=@CompanyCD ");
                    sqlIns.AppendLine("and FlowNo=@tempFlowNo ");
                    sqlIns.AppendLine("and BillTypeFlag=@BillTypeFlag ");
                    sqlIns.AppendLine("and BillTypeCode=@BillTypeCode ");
                    sqlIns.AppendLine("and BillID=@BillID");


                    SqlCommand commIns = new SqlCommand();
                    commIns.CommandText = sqlIns.ToString();
                    commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                    listADD.Add(commIns);
                    #endregion

                }
                #endregion

                #region 处理自己的业务逻辑
                StringBuilder sqlTake = new StringBuilder();
                sqlTake.AppendLine(" UPDATE officedba.BackMaterial SET");
                sqlTake.AppendLine(" Confirmor         = null,");
                sqlTake.AppendLine(" ConfirmDate        = null,");
                sqlTake.AppendLine(" ModifiedDate   = getdate(),");
                sqlTake.AppendLine(" BillStatus   = 1,");
                sqlTake.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sqlTake.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");



                SqlCommand commTake = new SqlCommand();
                commTake.CommandText = sqlTake.ToString();
                commTake.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTake.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTake);
                #endregion

                return SqlHelper.ExecuteTransWithArrayList(listADD);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(BackMaterialModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.BackMaterial set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND BackNo = @BackNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@BackNo", model.BackNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}
