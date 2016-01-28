/**********************************************
 * 类作用：   退料数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/30
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
    public class TakeMaterialDBHelper
    {
        #region 领料单插入
        /// <summary>
        /// 领料单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertTakeMaterial(TakeMaterialModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  领料单添加SQL语句
                StringBuilder sqlTake = new StringBuilder();
                sqlTake.AppendLine("INSERT INTO officedba.TakeMaterial			");
                sqlTake.AppendLine("           (CompanyCD						");
                sqlTake.AppendLine("           ,TakeNo							");
                sqlTake.AppendLine("           ,Subject							");
                sqlTake.AppendLine("           ,FromType						");
                sqlTake.AppendLine("           ,TaskID							");
                sqlTake.AppendLine("           ,ProcessDeptID					");
                sqlTake.AppendLine("           ,ManufactureType					");
                sqlTake.AppendLine("           ,SaleID							");
                sqlTake.AppendLine("           ,SaleDeptID						");
                sqlTake.AppendLine("           ,CountTotal						");
                sqlTake.AppendLine("           ,Creator							");
                sqlTake.AppendLine("           ,CreateDate						");
                sqlTake.AppendLine("           ,Taker                           ");
                sqlTake.AppendLine("           ,BillStatus						");
                sqlTake.AppendLine("           ,Remark							");
                sqlTake.AppendLine("           ,ProjectID						");
                sqlTake.AppendLine("           ,ModifiedDate					");
                sqlTake.AppendLine("           ,ModifiedUserID)					");
                sqlTake.AppendLine("     VALUES									");
                sqlTake.AppendLine("           (@CompanyCD						");
                sqlTake.AppendLine("           ,@TakeNo							");
                sqlTake.AppendLine("           ,@Subject						");
                sqlTake.AppendLine("           ,@FromType						");
                sqlTake.AppendLine("           ,@TaskID							");
                sqlTake.AppendLine("           ,@ProcessDeptID					");
                sqlTake.AppendLine("           ,@ManufactureType				");
                sqlTake.AppendLine("           ,@SaleID							");
                sqlTake.AppendLine("           ,@SaleDeptID						");
                sqlTake.AppendLine("           ,@CountTotal						");
                sqlTake.AppendLine("           ,@Creator						");
                sqlTake.AppendLine("           ,@CreateDate						");
                sqlTake.AppendLine("           ,@Taker							");
                sqlTake.AppendLine("           ,@BillStatus						");
                sqlTake.AppendLine("           ,@Remark							");
                sqlTake.AppendLine("           ,@ProjectID						");
                sqlTake.AppendLine("           ,getdate()						");
                sqlTake.AppendLine("           ,'" + loginUserID + "')        ");
                sqlTake.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlTake.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@TakeNo", model.TakeNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                comm.Parameters.Add(SqlHelper.GetParameter("@TaskID", model.TaskID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProcessDeptID", model.ProcessDeptID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
                comm.Parameters.Add(SqlHelper.GetParameter("@SaleID", model.SaleID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@SaleDeptID", model.SaleDeptID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@Taker", model.Taker.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

                listADD.Add(comm);
                #endregion

                // 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);

                //领料单明细添加SQL语句
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
        private static void SaveDetail(TakeMaterialModel model, ArrayList list, string loginUserID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.DetProductID.Length < 1)
            {
                return;
            }
            string[] detSortNo = model.DetSortNo.Split(',');
            string[] detProductID = model.DetProductID.Split(',');
            string[] detStorageID = model.DetStorageID.Split(',');
            string[] detTakeCount = model.DetTakeCount.Split(',');
            string[] detPrice = model.DetPrice.Split(',');
            string[] detTotalPrice = model.DetTotalPrice.Split(',');
            string[] detRemark = model.DetRemark.Split(',');
            string[] detFromType = model.DetFromType.Split(',');
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

                sqlDet.Append("INSERT INTO officedba.TakeMaterialDetail");
                sqlDet.Append(" (CompanyCD,TakeNo,SortNo,ModifiedDate,ModifiedUserID,Remark");
                sqlValues.AppendFormat(" VALUES(@CompanyCD,@TakeNo,@SortNo,getdate(),'{0}',@Remark", loginUserID);

                commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDet.Parameters.Add(SqlHelper.GetParameter("@TakeNo", model.TakeNo));
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
                if (userInfo.IsMoreUnit)
                {
                    if (!string.IsNullOrEmpty(detUnitID[i].Trim()))
                    {//基本单位
                        sqlDet.Append(",UnitID");
                        sqlValues.Append(",@UnitID");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@UnitID", detUnitID[i]));
                    }
                    if (!string.IsNullOrEmpty(detTakeCount[i].Trim()))
                    {//基本数量
                        sqlDet.Append(",TakeCount");
                        sqlValues.Append(",@TakeCount");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@TakeCount", detTakeCount[i]));
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
                    if (!string.IsNullOrEmpty(detTakeCount[i].Trim()))
                    {//数量
                        sqlDet.Append(",TakeCount");
                        sqlValues.Append(",@TakeCount");
                        commDet.Parameters.Add(SqlHelper.GetParameter("@TakeCount", detTakeCount[i]));
                    }
                }

                sqlDet.Append(")");
                sqlValues.Append(")");
                commDet.CommandText = sqlDet.ToString() + sqlValues.ToString();

                list.Add(commDet);
            }
        }
        #endregion

        #region 修改领料单和各明细信息
        /// <summary>
        /// 修改领料单和各明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelProduct"></param>
        /// <param name="modelStaff"></param>
        /// <param name="modelMachine"></param>
        /// <param name="modelMeterial"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateTakeMaterialInfo(TakeMaterialModel model, Hashtable htExtAttr, string loginUserID)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  领料单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.TakeMaterial               ");
            sqlEdit.AppendLine("   SET CompanyCD = @CompanyCD               ");
            sqlEdit.AppendLine("      ,Subject = @Subject                   ");
            sqlEdit.AppendLine("      ,FromType = @FromType                 ");
            sqlEdit.AppendLine("      ,TaskID = @TaskID                     ");
            sqlEdit.AppendLine("      ,ProcessDeptID = @ProcessDeptID       ");
            sqlEdit.AppendLine("      ,ManufactureType = @ManufactureType   ");
            sqlEdit.AppendLine("      ,SaleID = @SaleID                     ");
            sqlEdit.AppendLine("      ,SaleDeptID = @SaleDeptID             ");
            sqlEdit.AppendLine("      ,CountTotal = @CountTotal             ");
            sqlEdit.AppendLine("      ,Taker = @Taker                       ");
            sqlEdit.AppendLine("      ,Remark = @Remark                     ");
            sqlEdit.AppendLine("      ,ProjectID = @ProjectID               ");
            if (int.Parse(model.BillStatus) == 2)
            {
                sqlEdit.AppendLine("      ,BillStatus = 3                   ");
            }
            sqlEdit.AppendLine("      ,Confirmor=null                       ");
            sqlEdit.AppendLine("      ,ConfirmDate=null                     ");
            sqlEdit.AppendLine("      ,ModifiedDate = getdate()             ");
            sqlEdit.AppendLine("      ,ModifiedUserID = '" + loginUserID + "'   ");
            sqlEdit.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID      ");



            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaskID", model.TaskID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProcessDeptID", model.ProcessDeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
            comm.Parameters.Add(SqlHelper.GetParameter("@SaleID", model.SaleID));
            comm.Parameters.Add(SqlHelper.GetParameter("@SaleDeptID", model.SaleDeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Taker", model.Taker));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID));

            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 领料单明细处理

            // 删除领料单明细
            StringBuilder sqlProductDel = new StringBuilder();
            sqlProductDel.AppendLine("delete from officedba.TakeMaterialDetail ");
            sqlProductDel.AppendLine("where CompanyCD=@CompanyCD");
            sqlProductDel.AppendLine("and TakeNo=(");
            sqlProductDel.AppendLine("				select top 1 TakeNo from officedba.TakeMaterial where CompanyCD=@CompanyCD and ID=@ID");
            sqlProductDel.AppendLine("			    )");

            SqlCommand commProductDel = new SqlCommand();
            commProductDel.CommandText = sqlProductDel.ToString();
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            listADD.Add(commProductDel);


            //领料单明细添加SQL语句
            SaveDetail(model, listADD, loginUserID);


            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 保存时判断明细里的物品在分仓存量表里是否存在
        /// <summary>
        /// 保存时判断明细里的物品在分仓存量表里是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool isCanTakeMaterial(TakeMaterialModel model, out string reason)
        {
            string tempReason = "";
            bool isCan = true;

            #region 领料单明细QL语句
            if (model.DetProductID.Length > 0)
            {

                string[] detProductID = model.DetProductID.Split(',');
                string[] detStorageID = model.DetStorageID.Split(',');
                string[] detTakeCount = model.DetTakeCount.Split(',');
                string[] detBatchNo = model.DetBatchNo.Split(',');

                for (int i = 0; i < detProductID.Length; i++)
                {
                    decimal TakeCount = decimal.Parse(detTakeCount[i].ToString());
                    int ProductID = int.Parse(detProductID[i].ToString());
                    int StorageID = int.Parse(detStorageID[i].ToString());
                    string BatchNo = detBatchNo[i].ToString();
                    decimal productCount = InStorageProductCount(model.CompanyCD, ProductID, StorageID, BatchNo);

                    if (TakeCount > productCount)
                    {
                        isCan = false;
                        if (productCount == decimal.Parse("-1"))
                        {
                            tempReason = tempReason + "第" + (i + 1) + "行中的物品在当前所选的仓库中不存在<br>";
                        }
                        else
                        {
                            tempReason = tempReason + "第" + (i + 1) + "行中的领料数量大于该物品的库存量" + Decimal.Round(productCount, 2).ToString() + "<br>";
                        }
                    }
                }

            }
            #endregion

            reason = tempReason;
            return isCan;
        }
        #endregion

        #region 领料单详细信息
        /// <summary>
        /// 领料单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTakeInfo(TakeMaterialModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("				select	a.ID,a.CompanyCD,a.TakeNo,a.Subject,a.FromType,a.TaskID,b.TaskNo,");
            infoSql.AppendLine("                        case when a.FromType=0 then '无来源' when a.FromType=1 then '生产任务单' end as strFromType,");
            infoSql.AppendLine("						a.ProcessDeptID,j.DeptName as ProcessDeptName,a.ManufactureType,a.SaleID,c.EmployeeName as SaleReal,");
            infoSql.AppendLine("                        case when a.ManufactureType=0 then '普通' when a.ManufactureType=1 then '返修' when a.ManufactureType=2 then '拆件' end as strManufactureType,");
            infoSql.AppendLine("                        case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("						a.SaleDeptID,d.DeptName,Convert(numeric(22,"+userInfo.SelPoint+"),a.CountTotal) as CountTotal,a.Creator,e.EmployeeName as CreatorReal,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,a.Taker,f.EmployeeName as TakerReal,");
            infoSql.AppendLine("						a.Handout,g.EmployeeName as HandoutReal,isnull( CONVERT(CHAR(10), a.TakeDate, 23),'') as TakeDate,");
            infoSql.AppendLine("						a.BillStatus,a.Confirmor,h.EmployeeName as ConfirmorReal,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.Confirmdate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("						a.Closer,i.EmployeeName as CloserReal,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,a.Remark,a.ProjectID,p.ProjectName,");
            infoSql.AppendLine("                        a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID");
            infoSql.AppendLine("				from officedba.TakeMaterial a");
            infoSql.AppendLine("				left join officedba.ManufactureTask b on a.TaskID=b.ID ");
            infoSql.AppendLine("				left join officedba.EmployeeInfo c on a.SaleID=c.ID");
            infoSql.AppendLine("				left join officedba.DeptInfo d on a.SaleDeptID=d.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo e on a.Creator=e.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo f on a.Taker =f.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo g on a.Handout=g.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo h on a.Confirmor=h.ID");
            infoSql.AppendLine("				left join officedba.EmployeeInfo i on a.Closer=i.ID");
            infoSql.AppendLine("				left join officedba.DeptInfo j on a.ProcessDeptID=j.ID");
            infoSql.AppendLine("                left join officedba.ProjectInfo p on a.ProjectID=p.ID");
            infoSql.AppendLine(") as info");
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

        #region 领料单明细详细信息
        /// <summary>
        /// 领料单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTakeDetailInfo(TakeMaterialModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("				select	a.CompanyCD,a.ID as DetailID,a.TakeNo,a.SortNo,a.ProductID,b.ProductName,b.ProdNo,a.BatchNo,isnull(b.IsBatchNo,0) as IsBatchNo,");
            detSql.AppendLine("						b.UnitID,c.CodeName as UnitName,a.StorageID,d.StorageName,d.StorageNo,Convert(numeric(14,"+userInfo.SelPoint+"),a.TakeCount) as TakeCount,");
            detSql.AppendLine("						Convert(numeric(14," + userInfo.SelPoint + "),a.Price) as Price,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedPrice) as UsedPrice,Convert(numeric(22," + userInfo.SelPoint + "),a.TotalPrice) as TotalPrice,");
            detSql.AppendLine("						isnull(Convert(numeric(14,"+userInfo.SelPoint+"),BackCount),0) as BackCount,a.Remark,a.FromType,");
            detSql.AppendLine("                     case when a.FromType=0 then '无来源' when a.FromType=1 then '生产任务单' end as strFromType,a.UsedUnitID,e.CodeName as UsedUnitName,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount  ");
            detSql.AppendLine("				from officedba.TakeMaterialDetail a");
            detSql.AppendLine("				left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("				left join officedba.CodeUnitType c on b.UnitID=c.Id");
            detSql.AppendLine("             left join officedba.CodeUnitType e on a.UsedUnitID=e.ID");
            detSql.AppendLine("				left join officedba.StorageInfo d on a.StorageID=d.ID");
            detSql.AppendLine("				where TakeNo=(select top 1 TakeNo from officedba.TakeMaterial where ID=@ID)");
            detSql.AppendLine(") as info");
            detSql.AppendLine("where CompanyCD=@CompanyCD ");
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
        public static DataTable GetTakeMaterialListBycondition(TakeMaterialModel model, string TakeDateStart, string TakeDateEnd, int BillTypeFlag, int BillTypeCode, int FlowStatus, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.CompanyCD,a.ID,a.TakeNo,a.Subject,a.FromType,a.TaskID,isnull( f.TaskNo,'')as TaskNo,a.ModifiedDate,");
            searchSql.AppendLine("		    a.ProcessDeptID,isnull(b.DeptName,'')as ProcessDeptName,a.ManufactureType,");
            searchSql.AppendLine("          a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("          case when a.FromType=0 then '无来源' when a.FromType=1 then '生产任务单' end as strFromType,");
            searchSql.AppendLine("          case when a.ManufactureType=0 then '普通' when a.ManufactureType=1 then '返修' when a.ManufactureType=2 then '拆件' end as strManufactureType,");
            searchSql.AppendLine("		    isnull(a.Taker,'')as Taker,isnull(c.EmployeeName,'')as TakerReal,");
            searchSql.AppendLine("		    isnull(a.Handout,'')as Handout,isnull(d.EmployeeName,'')as HandoutReal,");
            searchSql.AppendLine("		    isnull( CONVERT(CHAR(10), a.TakeDate, 23),'') as TakeDate,a.BillStatus,p.ProjectName,a.ProjectID,");
            searchSql.AppendLine("          case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            searchSql.AppendLine("           case when e.FlowStatus=1 then '待审批' when e.FlowStatus=2 then '审批中' when e.FlowStatus=3 then '审批通过' when e.FlowStatus=4 then '审批不通过' when e.FlowStatus=5 then '撤消审批' end as strFlowStatus,");
            searchSql.AppendLine("		    isnull(e.FlowStatus,'0')as FlowStatus");
            searchSql.AppendLine("from officedba.TakeMaterial a");
            searchSql.AppendLine("left join officedba.Deptinfo b on a.ProcessDeptID=b.ID");
            searchSql.AppendLine("left join officedba.EmployeeInfo c on a.Taker=c.ID");
            searchSql.AppendLine("left join officedba.EmployeeInfo d on a.Handout=d.ID");
            searchSql.AppendLine("LEFT JOIN officedba.ProjectInfo p on a.ProjectID=p.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.FlowInstance e ON a.ID=e.BillID and e.BillTypeFlag=@BillTypeFlag and e.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine(" and e.ID=( ");
            searchSql.AppendLine("                      select  max(ID)");
            searchSql.AppendLine("                      from  officedba.FlowInstance H");
            searchSql.AppendLine("                      where   H.CompanyCD = A.CompanyCD");
            searchSql.AppendLine("                      and H.BillID = A.ID");
            searchSql.AppendLine("                      and H.BillTypeFlag =@BillTypeFlag");
            searchSql.AppendLine("                      and H.BillTypeCode =@BillTypeCode)");
            searchSql.AppendLine("LEFT JOIN officedba.ManufactureTask f on a.TaskID=f.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

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
            if (!string.IsNullOrEmpty(model.TakeNo))
            {
                searchSql.AppendLine("and a.TakeNo like @TakeNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeNo", "%" + model.TakeNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and a.Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }
            //源单类型
            if (!string.IsNullOrEmpty(model.FromType))
            {
                if (int.Parse(model.FromType) > -1)
                {
                    searchSql.AppendLine(" and a.FromType=@FromType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", model.FromType));
                }
            }
            //生产任务单
            if (model.TaskID > 0)
            {
                searchSql.AppendLine(" and a.TaskID=@TaskID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskID", model.TaskID.ToString()));
            }
            //生产部门
            if (model.ProcessDeptID > 0)
            {
                searchSql.AppendLine(" and a.ProcessDeptID=@ProcessDeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProcessDeptID", model.ProcessDeptID.ToString()));
            }
            //加工类型
            if (model.ManufactureType > 0)
            {
                searchSql.AppendLine(" and a.ManufactureType=@ManufactureType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ManufactureType", model.ManufactureType.ToString()));
            }
            //领料人
            if (model.Taker > 0)
            {
                searchSql.AppendLine(" and a.Taker=@Taker ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taker", model.Taker.ToString()));
            }
            //领料单控件（取出发过料的所有领料单）
            if (model.Handout == -1)
            {
                searchSql.AppendLine(" and a.Handout is not null ");
            }
            //发料人
            if (model.Handout > 0)
            {
                searchSql.AppendLine(" and a.Handout=@Handout ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Handout", model.Handout.ToString()));
            }

            //发料起始日期
            if (!string.IsNullOrEmpty(TakeDateStart))
            {
                searchSql.AppendLine(" and a.TakeDate>=@TakeDateStart ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeDateStart", TakeDateStart));
            }
            //发料截止日期
            if (!string.IsNullOrEmpty(TakeDateEnd))
            {
                searchSql.AppendLine(" and a.TakeDate<=@TakeDateEnd ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeDateEnd", TakeDateEnd));
            }
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and a.BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                }
            }
            if (model.ProjectID > 0)
            {
                searchSql.AppendLine(" and a.ProjectID=@ProjectID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID.ToString()));
            }
            //审批状态
            if (FlowStatus > -1)
            {
                searchSql.AppendLine("and e.FlowStatus=@FlowStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus.ToString()));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 领料单MRP运算：查询任务单明细中的设置了BOM，需要领料的
        /// <summary>
        /// 查询任务单明细中的
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTaskDetailBom_ByTaskID(int intTaskID, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select CompanyCD,ProductID,Convert(numeric(10,2),ProductCount) as ProductCount from officedba.ManufactureTaskDetail ");
            infoSql.AppendLine("where TaskNo=(");
            infoSql.AppendLine("				select TaskNo from officedba.ManufactureTask where ID=@ID");
            infoSql.AppendLine("		     )");
            infoSql.AppendLine("and BomID is not null");
            infoSql.AppendLine("and CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", intTaskID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查询BOM子件
        /// <summary>
        /// 查询BOM子件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetSubBomList_ByBomID(int intBomID, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select ProductID,Convert(numeric(10,2),Quota) as Quota,Convert(numeric(10,2),RateLoss) as RateLoss from officedba.BomDetail ");
            infoSql.AppendLine("where BomNo=(");
            infoSql.AppendLine("				select top 1 BomNo from officedba.Bom where ID=@ID");
            infoSql.AppendLine("			)");
            infoSql.AppendLine("and Quota is not null");
            infoSql.AppendLine("and RateLoss is not null");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", intBomID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查询BOM
        /// <summary>
        /// 查询BOM
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetBomList_ByParentNo(int intParentNo, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select ID from officedba.Bom where CompanyCD=@CompanyCD and parentNo=@ParentNo");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ParentNo", intParentNo.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据物品ID查询物品详细信息
        /// <summary>
        /// 根据物品ID查询物品详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetProductList_ByPorductID(string productID, string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select a.ID,a.ProdNo,a.ProductName,TakeCount='0.00',a.UnitID,b.CodeName as UnitName,a.StorageID,c.StorageName,Convert(numeric(10,2),isnull(a.StandardCost,0)) as StandardCost from officedba.productInfo a left join officedba.CodeUnitType b on a.UnitID=b.ID left join officedba.StorageInfo c on a.StorageID=c.ID where a.CompanyCD=@CompanyCD and a.ID in(" + productID + ")");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 删除领料单
        /// <summary>
        /// 删除领料单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteTakematerial(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.TakeMaterialDetail where CompanyCD=@CompanyCD and TakeNo=(select TakeNo from officedba.TakeMaterial where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.TakeMaterial where CompanyCD=@CompanyCD and ID=@ID");

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

        #region 发料
        /// <summary>
        /// 发料(此处可能需要判断当前物品A所在的仓库A1在分仓存量表里是否存在，如果存在，再判断分仓存量表里的库存是否大于当前该物品的领料统计，如果小于返回提示)
        /// </summary>
        /// <param name="model"></param>
        public static bool SendTakeMaterial(TakeMaterialModel model,out string reason)
        {
            //判断当前物品在分仓存量表里是否存在,如果存在，再判断分仓存量表里的库存是否大于当前该物品的领料数量，如果大于则更新分仓存量表，如果小于则返回提示
            //更新分仓库存量表
            //更新领料表
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                ArrayList listADD = new ArrayList();

                int tempTotalSucceed = 0;
                bool tempOperateResult = false;
                string tempReason = "";
                string intTake = "";
                DataTable dbTake = GetTakeInfo(model);
                if (dbTake.Rows.Count > 0)
                {
                    intTake = dbTake.Rows[0]["ID"].ToString();
                    if (!string.IsNullOrEmpty(dbTake.Rows[0]["Handout"].ToString()))
                    {
                        reason = "已经发过料了";
                    }
                    else
                    {
                        #region 业务处理
                        DataTable dtDetail = GetTakeDetailInfo(model);
                        if (dtDetail.Rows.Count > 0)
                        {
                            #region 明细处理
                            for (int i = 0; i < dtDetail.Rows.Count; i++)
                            {
                                decimal TakeCount = decimal.Parse(dtDetail.Rows[i]["TakeCount"].ToString());
                                int ProductID = int.Parse(dtDetail.Rows[i]["ProductID"].ToString());
                                int StorageID = int.Parse(dtDetail.Rows[i]["StorageID"].ToString());
                                string BatchNo = dtDetail.Rows[i]["BatchNo"].ToString();
                                if (string.IsNullOrEmpty(BatchNo))
                                {
                                    BatchNo = "";
                                }
                                string TakeNo =dtDetail.Rows[i]["TakeNo"].ToString();
                                decimal Price = decimal.Parse(dtDetail.Rows[i]["Price"].ToString());
                                decimal productCount = InStorageProductCount(model.CompanyCD, ProductID, StorageID,BatchNo);/*现有存量*/

                                #region 1.判断当前物品的领料数量是否大于分仓存量表中的库存量
                                if (TakeCount <= productCount)
                                {
                                    #region 更新分仓存量表
                                    StringBuilder sqlUpdate = new StringBuilder();
                                    SqlCommand comUpdate = new SqlCommand();
                                    sqlUpdate.AppendLine("update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)-@TakeCount where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD and isnull(BatchNo,'')=@BatchNo");

                                    comUpdate.CommandText = sqlUpdate.ToString();
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@BatchNo", BatchNo));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@TakeCount", TakeCount));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@StorageID", StorageID));
                                    comUpdate.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
                                    listADD.Add(comUpdate);
                                    #endregion

                                    tempTotalSucceed++;
                                }
                                else
                                {
                                    if (productCount == decimal.Parse("-1"))
                                    {
                                        tempReason = tempReason + "第" + (i + 1) + "行中的物品在当前所选的仓库中不存在<br>";
                                    }
                                    else
                                    {
                                        tempReason = tempReason + "第" + (i + 1) + "行中的领料数量大于该物品的库存量" + Decimal.Round(productCount, 2).ToString() + "<br>";
                                    }
                                }
                                #endregion

                                #region 2.写入数据到库存流水账表
                                StorageAccountModel modelSA = new StorageAccountModel();
                                modelSA.CompanyCD = model.CompanyCD;
                                modelSA.BillType = ConstUtil.STORAGEACCOUNT_BILLTYPE_TAKE;                      /*单据类型*/
                                modelSA.ProductID = ProductID;                                                  /*物品ID*/
                                modelSA.StorageID = StorageID;                                                  /*仓库ID*/
                                modelSA.BatchNo = BatchNo;                                                      /*批次*/
                                modelSA.BillNo = TakeNo;                                                        /*单据编号*/
                                modelSA.HappenCount = TakeCount;                                                /*操作数量*/
                                modelSA.Creator = userInfo.EmployeeID;                                          /*操作人*/
                                modelSA.Price = Price;                                                          /*单价*/
                                modelSA.ProductCount = productCount-TakeCount;                                  /*现有存量*/
                                modelSA.PageUrl = "Pages/Office/ProductionManager/TakeMaterial_Add.aspx?ModuleID=" + ConstUtil.MODULE_ID_TAKEMATERIAL_EDIT + "&intTakeID=" + intTake;/*页面URL*/

                                listADD.Add(StorageAccountDBHelper.InsertStorageAccountBySqlCommand(modelSA));
                                #endregion
                            }
                            #endregion

                            if (dtDetail.Rows.Count > 0 && dtDetail.Rows.Count == tempTotalSucceed)
                            {
                                #region 更新领料表中的发料人和发料时间
                                if (!string.IsNullOrEmpty(model.TakeDate.ToShortDateString()) && !string.IsNullOrEmpty(model.Handout.ToString()))
                                {
                                    //例：Update officedba.TakeMaterial set Handout=@Handout,TakeDate=@TakeDate where CompanyCD=@CompanyCD and ID=@ID
                                    StringBuilder sqlTake = new StringBuilder();
                                    sqlTake.AppendLine("Update officedba.TakeMaterial set Handout=@Handout,TakeDate=@TakeDate where CompanyCD=@CompanyCD and ID=@ID");

                                    SqlCommand comTake = new SqlCommand();
                                    comTake.CommandText = sqlTake.ToString();
                                    comTake.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                    comTake.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                                    comTake.Parameters.Add(SqlHelper.GetParameter("@Handout", model.Handout));
                                    comTake.Parameters.Add(SqlHelper.GetParameter("@TakeDate", model.TakeDate));
                                    listADD.Add(comTake);

                                    if (SqlHelper.ExecuteTransWithArrayList(listADD))
                                    {
                                        tempOperateResult = true;
                                    }
                                }
                                #endregion
                            }
                            reason = tempReason;
                        }
                        else
                        {
                            reason = "领料单明细中没有对应的物品需要发料";
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
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 判断物品在分仓存量表里是否存在
        public static decimal InStorageProductCount(string CompanyCD, int intProductID, int intStorageID,string BatchNo)
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
        public static bool ConfirmOrCompleteTakeMaterial(TakeMaterialModel model, string loginUserID, int OperateType)
        {
            ArrayList listADD = new ArrayList();
            if (OperateType == 1)
            {
                StringBuilder sqlTake = new StringBuilder();
                sqlTake.AppendLine(" UPDATE officedba.TakeMaterial SET");
                sqlTake.AppendLine(" Confirmor         = @Confirmor,");
                sqlTake.AppendLine(" ConfirmDate        = @ConfirmDate,");
                sqlTake.AppendLine(" ModifiedDate   = getdate(),");
                sqlTake.AppendLine(" BillStatus   = 2,");
                sqlTake.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sqlTake.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                SqlCommand commTake = new SqlCommand();
                commTake.CommandText = sqlTake.ToString();
                commTake.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTake.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                commTake.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));
                commTake.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                listADD.Add(commTake);

            }
            else if (OperateType == 2)
            {
                StringBuilder sqlTake = new StringBuilder();
                sqlTake.AppendLine(" UPDATE officedba.TakeMaterial SET");
                sqlTake.AppendLine(" Closer         = @Closer,");
                sqlTake.AppendLine(" CloseDate   = @CloseDate,");
                sqlTake.AppendLine(" BillStatus   = 4,");
                sqlTake.AppendLine(" ModifiedDate   = getdate(),");
                sqlTake.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sqlTake.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                SqlCommand commTake = new SqlCommand();
                commTake.CommandText = sqlTake.ToString();
                commTake.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTake.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                commTake.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate));
                commTake.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTake);
            }
            else
            {
                StringBuilder sqlTake = new StringBuilder();
                sqlTake.AppendLine(" update officedba.TakeMaterial set Closer=null,CloseDate=null,ModifiedDate=getdate(),BillStatus=2,ModifiedUserID = '" + loginUserID + "'");
                sqlTake.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");


                SqlCommand commTake = new SqlCommand();
                commTake.CommandText = sqlTake.ToString();
                commTake.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTake.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTake);
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
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
        public static bool CancelConfirmOperate(TakeMaterialModel model, int BillTypeFlag, int BillTypeCode, string loginUserID)
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
                sqlTake.AppendLine(" UPDATE officedba.TakeMaterial SET");
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
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in(" + ID + ")";
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

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(TakeMaterialModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.TakeMaterial set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND TakeNo = @TakeNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@TakeNo", model.TakeNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 运营模式：(生产领料单明细)
        /// <summary>
        /// 通过检索条件查询领料单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable GetTakeMaterialListBycondition_Operating(string CompanyCD, int ProcessDeptID, string TaskNo, string ProdNo, string ProductName, string ConfirmDateStart, string ConfirmDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("	select  b.ID,a.CompanyCD,a.ProcessDeptID,c.DeptName,a.TaskID,d.TaskNo,a.TakeNo,");
            searchSql.AppendLine("			a.Subject,b.ProductID,e.ProductName,e.ProdNo,e.Specification,e.UnitID,");
            searchSql.AppendLine("			f.CodeName as UnitName,Convert(numeric(10," + point + "),b.TakeCount) as TakeCount,Convert(char(20),Convert(numeric(10," + point + "),b.TakeCount))+'&nbsp;' as TakeCount1,");
            searchSql.AppendLine("			a.Taker,g.EmployeeName as TakerReal,a.BillStatus,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,isnull( CONVERT(CHAR(10), a.TakeDate, 23),'') as TakeDate,");
            searchSql.AppendLine("			Convert(numeric(10," + point + "),b.BackCount) as BackCount,Convert(char(20),Convert(numeric(10," + point + "),b.BackCount))+'&nbsp;' as BackCount1");
            searchSql.AppendLine("	from officedba.TakeMaterial a");
            searchSql.AppendLine("	left outer join officedba.TakeMaterialDetail b on a.TakeNo=b.TakeNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on a.ProcessDeptID=c.ID");
            searchSql.AppendLine("	left join officedba.ManufactureTask d on a.TaskID=d.ID");
            searchSql.AppendLine("	left join officedba.ProductInfo e on b.ProductID=e.ID");
            searchSql.AppendLine("	left join officedba.CodeUnitType f on e.UnitID=f.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo g on a.Taker=g.ID");
            searchSql.AppendLine(") as tmpdt");
            searchSql.AppendLine("where CompanyCD=@CompanyCD and BillStatus=2");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--生产部门
            if (ProcessDeptID > 0)
            {
                searchSql.AppendLine(" and ProcessDeptID=@ProcessDeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProcessDeptID", ProcessDeptID.ToString()));
            }
            //--生产任务单编号
            if (!string.IsNullOrEmpty(TaskNo))
            {
                searchSql.AppendLine(" and TaskNo like @TaskNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + TaskNo + "%"));
            }
            //--物料编号
            if (!string.IsNullOrEmpty(ProdNo))
            {
                searchSql.AppendLine(" and ProdNo like @ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + ProdNo + "%"));
            }
            //--物料名称
            if (!string.IsNullOrEmpty(ProductName))
            {
                searchSql.AppendLine(" and ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateStart", ConfirmDateStart));
            }
            //发料截止日期
            if (!string.IsNullOrEmpty(ConfirmDateEnd))
            {
                searchSql.AppendLine(" and ConfirmDate<=@ConfirmDateEnd ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateEnd", ConfirmDateEnd));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产领料单明细)
        /// <summary>
        /// 通过检索条件查询领料单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable GetTakeMaterialListBycondition_Operating_Print(string CompanyCD, int ProcessDeptID, string TaskNo, string ProdNo, string ProductName, string ConfirmDateStart, string ConfirmDateEnd, string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("	select  b.ID,a.CompanyCD,a.ProcessDeptID,c.DeptName,a.TaskID,d.TaskNo,a.TakeNo,");
            searchSql.AppendLine("			a.Subject,b.ProductID,e.ProductName,e.ProdNo,e.Specification,e.UnitID,");
            searchSql.AppendLine("			f.CodeName as UnitName,Convert(numeric(10,2),b.TakeCount) as TakeCount,");
            searchSql.AppendLine("			a.Taker,g.EmployeeName as TakerReal,a.BillStatus,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,isnull( CONVERT(CHAR(10), a.TakeDate, 23),'') as TakeDate,");
            searchSql.AppendLine("			Convert(numeric(10,2),b.BackCount) as BackCount");
            searchSql.AppendLine("	from officedba.TakeMaterial a");
            searchSql.AppendLine("	left outer join officedba.TakeMaterialDetail b on a.TakeNo=b.TakeNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on a.ProcessDeptID=c.ID");
            searchSql.AppendLine("	left join officedba.ManufactureTask d on a.TaskID=d.ID");
            searchSql.AppendLine("	left join officedba.ProductInfo e on b.ProductID=e.ID");
            searchSql.AppendLine("	left join officedba.CodeUnitType f on e.UnitID=f.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo g on a.Taker=g.ID");
            searchSql.AppendLine(") as tmpdt");
            searchSql.AppendLine("where CompanyCD=@CompanyCD and BillStatus=2");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--生产部门
            if (ProcessDeptID > 0)
            {
                searchSql.AppendLine(" and ProcessDeptID=@ProcessDeptID");
                arr.Add(new SqlParameter("@ProcessDeptID", ProcessDeptID.ToString()));

            }
            //--生产任务单编号
            if (!string.IsNullOrEmpty(TaskNo))
            {
                searchSql.AppendLine(" and TaskNo like @TaskNo");
                arr.Add(new SqlParameter("@TaskNo", "%" + TaskNo + "%"));
            }
            //--物料编号
            if (!string.IsNullOrEmpty(ProdNo))
            {
                searchSql.AppendLine(" and ProdNo like @ProdNo");
                arr.Add(new SqlParameter("@ProdNo", "%" + ProdNo + "%"));
            }
            //--物料名称
            if (!string.IsNullOrEmpty(ProductName))
            {
                searchSql.AppendLine(" and ProductName like @ProductName");
                arr.Add(new SqlParameter("@ProductName", "%" + ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                arr.Add(new SqlParameter("@ConfirmDateStart", ConfirmDateStart));
            }
            //发料截止日期
            if (!string.IsNullOrEmpty(ConfirmDateEnd))
            {
                searchSql.AppendLine(" and ConfirmDate<=@ConfirmDateEnd ");
                arr.Add(new SqlParameter("@ConfirmDateEnd", ConfirmDateEnd));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine(" order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

    }
}
