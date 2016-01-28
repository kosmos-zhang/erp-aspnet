/***********************************************
 * 类作用：   SubSellOrder  事务层处理         *
 * 建立人：   王超                             *
 * 建立时间： 2009/05/20                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using System.Data.SqlTypes;
using System.Data.Sql;
using XBase.Model.Office.SubStoreManager;
using System.Collections;

namespace XBase.Data.Office.SubStoreManager
{
    public class SubSellOrderDBHelper
    {
        #region 分店销售订单表操作
        #region 新增
        public static SqlCommand InsertSubSellOrder(SubSellOrderModel SubSellOrderM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.SubSellOrder");
            sql.AppendLine("           (CompanyCD             ");
            sql.AppendLine("           ,OrderNo               ");
            sql.AppendLine("           ,Title                 ");
            sql.AppendLine("           ,DeptID                ");
            sql.AppendLine("           ,SendMode              ");
            sql.AppendLine("           ,Seller                ");
            sql.AppendLine("           ,CustName              ");
            sql.AppendLine("           ,CustTel               ");
            sql.AppendLine("           ,CustMobile            ");
            sql.AppendLine("           ,CustAddr              ");
            sql.AppendLine("           ,CurrencyType          ");
            sql.AppendLine("           ,Rate                  ");
            sql.AppendLine("           ,OrderMethod           ");
            sql.AppendLine("           ,TakeType              ");
            sql.AppendLine("           ,PayType               ");
            sql.AppendLine("           ,MoneyType             ");
            sql.AppendLine("           ,OrderDate             ");
            sql.AppendLine("           ,isAddTax              ");
            sql.AppendLine("           ,PlanOutDate           ");
            sql.AppendLine("           ,OutDate               ");
            sql.AppendLine("           ,CarryType             ");
            sql.AppendLine("           ,BusiStatus            ");
            sql.AppendLine("           ,OutDeptID             ");
            sql.AppendLine("           ,OutUserID             ");
            sql.AppendLine("           ,NeedSetup             ");
            sql.AppendLine("           ,PlanSetDate           ");
            sql.AppendLine("           ,SetDate               ");
            sql.AppendLine("           ,SetUserInfo             ");
            sql.AppendLine("           ,TotalPrice            ");
            sql.AppendLine("           ,Tax                   ");
            sql.AppendLine("           ,TotalFee              ");
            sql.AppendLine("           ,Discount              ");
            sql.AppendLine("           ,SaleFeeTotal          ");
            sql.AppendLine("           ,DiscountTotal         ");
            sql.AppendLine("           ,RealTotal             ");
            sql.AppendLine("           ,PayedTotal            ");
            sql.AppendLine("           ,WairPayTotal          ");
            sql.AppendLine("           ,CountTotal            ");
            sql.AppendLine("           ,BillStatus            ");
            sql.AppendLine("           ,Creator               ");
            sql.AppendLine("           ,CreateDate            ");
            sql.AppendLine("           ,Confirmor             ");
            sql.AppendLine("           ,ConfirmDate           ");
            sql.AppendLine("           ,Closer                ");
            sql.AppendLine("           ,CloseDate             ");
            sql.AppendLine("           ,ModifiedDate          ");
            sql.AppendLine("           ,ModifiedUserID        ");
            sql.AppendLine("           ,Attachment            ");
            sql.AppendLine("           ,Remark                ");
            sql.AppendLine("           ,isOpenbill            ");
            sql.AppendLine("           ,SttlUserID            ");
            sql.AppendLine("           ,SttlDate)             ");
            sql.AppendLine("     VALUES(                      ");
            sql.AppendLine("     		@CompanyCD            ");
            sql.AppendLine("           ,@OrderNo              ");
            sql.AppendLine("           ,@Title                ");
            sql.AppendLine("           ,@DeptID               ");
            sql.AppendLine("           ,@SendMode             ");
            sql.AppendLine("           ,@Seller               ");
            sql.AppendLine("           ,@CustName             ");
            sql.AppendLine("           ,@CustTel              ");
            sql.AppendLine("           ,@CustMobile           ");
            sql.AppendLine("           ,@CustAddr             ");
            sql.AppendLine("           ,@CurrencyType         ");
            sql.AppendLine("           ,@Rate                 ");
            sql.AppendLine("           ,@OrderMethod          ");
            sql.AppendLine("           ,@TakeType             ");
            sql.AppendLine("           ,@PayType              ");
            sql.AppendLine("           ,@MoneyType            ");
            sql.AppendLine("           ,@OrderDate            ");
            sql.AppendLine("           ,@isAddTax             ");
            sql.AppendLine("           ,@PlanOutDate          ");
            sql.AppendLine("           ,@OutDate              ");
            sql.AppendLine("           ,@CarryType            ");
            sql.AppendLine("           ,@BusiStatus           ");
            sql.AppendLine("           ,@OutDeptID            ");
            sql.AppendLine("           ,@OutUserID            ");
            sql.AppendLine("           ,@NeedSetup            ");
            sql.AppendLine("           ,@PlanSetDate          ");
            sql.AppendLine("           ,@SetDate              ");
            sql.AppendLine("           ,@SetUserInfo            ");
            sql.AppendLine("           ,@TotalPrice           ");
            sql.AppendLine("           ,@Tax                  ");
            sql.AppendLine("           ,@TotalFee             ");
            sql.AppendLine("           ,@Discount             ");
            sql.AppendLine("           ,@SaleFeeTotal         ");
            sql.AppendLine("           ,@DiscountTotal        ");
            sql.AppendLine("           ,@RealTotal            ");
            sql.AppendLine("           ,@PayedTotal           ");
            sql.AppendLine("           ,@WairPayTotal         ");
            sql.AppendLine("           ,@CountTotal           ");
            sql.AppendLine("           ,@BillStatus           ");
            sql.AppendLine("           ,@Creator              ");
            sql.AppendLine("           ,@CreateDate           ");
            sql.AppendLine("           ,@Confirmor            ");
            sql.AppendLine("           ,@ConfirmDate          ");
            sql.AppendLine("           ,@Closer               ");
            sql.AppendLine("           ,@CloseDate            ");
            sql.AppendLine("           ,@ModifiedDate         ");
            sql.AppendLine("           ,@ModifiedUserID       ");
            sql.AppendLine("           ,@Attachment           ");
            sql.AppendLine("           ,@Remark               ");
            sql.AppendLine("           ,@isOpenbill           ");
            sql.AppendLine("           ,@SttlUserID           ");
            sql.AppendLine("           ,@SttlDate)            ");
            sql.AppendLine("set @IndexID = @@IDENTITY");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", SubSellOrderM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", SubSellOrderM.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", SubSellOrderM.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", SubSellOrderM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendMode", SubSellOrderM.SendMode));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Seller", SubSellOrderM.Seller));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", SubSellOrderM.CustName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", SubSellOrderM.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustMobile", SubSellOrderM.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", SubSellOrderM.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", SubSellOrderM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", SubSellOrderM.Rate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderMethod", SubSellOrderM.OrderMethod));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeType", SubSellOrderM.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayType", SubSellOrderM.PayType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoneyType", SubSellOrderM.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", SubSellOrderM.OrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isAddTax", SubSellOrderM.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanOutDate", SubSellOrderM.PlanOutDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", SubSellOrderM.OutDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CarryType", SubSellOrderM.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", SubSellOrderM.BusiStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDeptID", SubSellOrderM.OutDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutUserID", SubSellOrderM.OutUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NeedSetup", SubSellOrderM.NeedSetup));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanSetDate", SubSellOrderM.PlanSetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetDate", SubSellOrderM.SetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetUserInfo", SubSellOrderM.SetUserInfo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", SubSellOrderM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tax", SubSellOrderM.Tax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", SubSellOrderM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", SubSellOrderM.Discount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleFeeTotal", SubSellOrderM.SaleFeeTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountTotal", SubSellOrderM.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealTotal", SubSellOrderM.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayedTotal", SubSellOrderM.PayedTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WairPayTotal", SubSellOrderM.WairPayTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", SubSellOrderM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", SubSellOrderM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", SubSellOrderM.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", SubSellOrderM.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", SubSellOrderM.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", SubSellOrderM.ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", SubSellOrderM.Closer));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", SubSellOrderM.CloseDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", SubSellOrderM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", SubSellOrderM.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", SubSellOrderM.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", SubSellOrderM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isOpenbill", SubSellOrderM.isOpenbill));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlUserID", SubSellOrderM.SttlUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlDate", SubSellOrderM.SttlDate));

            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }

        /// <summary>
        /// 扩展属性更新命令
        /// </summary>
        /// <param name="model">分店销售订单</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static SqlCommand UpdateExtAttr(SubSellOrderModel model, Hashtable htExtAttr)
        {
            SqlCommand sqlcomm = new SqlCommand();
            if (htExtAttr == null || htExtAttr.Count < 1)
            {// 没有属性需要修改
                return null;
            }

            StringBuilder sb = new StringBuilder(" UPDATE officedba.SubSellOrder SET ");
            foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
            {
                sb.AppendFormat(" {0}=@{0},", de.Key.ToString());
                sqlcomm.Parameters.Add(SqlHelper.GetParameter(String.Format("@{0}", de.Key.ToString()), de.Value));
            }
            string strSql = sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " WHERE CompanyCD = @CompanyCD  AND OrderNo = @OrderNo ";
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.OrderNo));
            sqlcomm.CommandText = strSql;

            return sqlcomm;
        }
        #endregion

        #region 修改
        public static SqlCommand UpdateSubSellOrder(SubSellOrderModel SubSellOrderM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.SubSellOrder     ");
            sql.AppendLine("   SET CompanyCD       =@CompanyCD      ");
            sql.AppendLine("      ,OrderNo         =@OrderNo        ");
            sql.AppendLine("      ,Title           =@Title          ");
            sql.AppendLine("      ,DeptID          =@DeptID         ");
            sql.AppendLine("      ,SendMode        =@SendMode       ");
            sql.AppendLine("      ,Seller          =@Seller         ");
            sql.AppendLine("      ,CustName        =@CustName       ");
            sql.AppendLine("      ,CustTel         =@CustTel        ");
            sql.AppendLine("      ,CustMobile      =@CustMobile     ");
            sql.AppendLine("      ,CustAddr        =@CustAddr       ");
            sql.AppendLine("      ,CurrencyType    =@CurrencyType   ");
            sql.AppendLine("      ,Rate            =@Rate           ");
            sql.AppendLine("      ,OrderMethod     =@OrderMethod    ");
            sql.AppendLine("      ,TakeType        =@TakeType       ");
            sql.AppendLine("      ,PayType         =@PayType        ");
            sql.AppendLine("      ,MoneyType       =@MoneyType      ");
            sql.AppendLine("      ,OrderDate       =@OrderDate      ");
            sql.AppendLine("      ,isAddTax        =@isAddTax       ");
            sql.AppendLine("      ,PlanOutDate     =@PlanOutDate    ");
            sql.AppendLine("      ,OutDate         =@OutDate        ");
            sql.AppendLine("      ,CarryType       =@CarryType      ");
            sql.AppendLine("      ,BusiStatus      =@BusiStatus     ");
            sql.AppendLine("      ,OutDeptID       =@OutDeptID      ");
            sql.AppendLine("      ,OutUserID       =@OutUserID      ");
            sql.AppendLine("      ,NeedSetup       =@NeedSetup      ");
            sql.AppendLine("      ,PlanSetDate     =@PlanSetDate    ");
            sql.AppendLine("      ,SetDate         =@SetDate        ");
            sql.AppendLine("      ,SetUserInfo       =@SetUserInfo      ");
            sql.AppendLine("      ,TotalPrice      =@TotalPrice     ");
            sql.AppendLine("      ,Tax             =@Tax            ");
            sql.AppendLine("      ,TotalFee        =@TotalFee       ");
            sql.AppendLine("      ,Discount        =@Discount       ");
            sql.AppendLine("      ,SaleFeeTotal    =@SaleFeeTotal   ");
            sql.AppendLine("      ,DiscountTotal   =@DiscountTotal  ");
            sql.AppendLine("      ,RealTotal       =@RealTotal      ");
            sql.AppendLine("      ,PayedTotal      =@PayedTotal     ");
            sql.AppendLine("      ,WairPayTotal    =@WairPayTotal   ");
            sql.AppendLine("      ,CountTotal      =@CountTotal     ");
            sql.AppendLine("      ,BillStatus      =@BillStatus     ");
            sql.AppendLine("      ,Creator         =@Creator        ");
            sql.AppendLine("      ,CreateDate      =@CreateDate     ");
            sql.AppendLine("      ,Confirmor       =@Confirmor      ");
            sql.AppendLine("      ,ConfirmDate     =@ConfirmDate    ");
            sql.AppendLine("      ,Closer          =@Closer         ");
            sql.AppendLine("      ,CloseDate       =@CloseDate      ");
            sql.AppendLine("      ,ModifiedDate    =@ModifiedDate   ");
            sql.AppendLine("      ,ModifiedUserID  =@ModifiedUserID ");
            sql.AppendLine("      ,Attachment      =@Attachment     ");
            sql.AppendLine("      ,Remark          =@Remark         ");
            sql.AppendLine("      ,isOpenbill      =@isOpenbill     ");
            sql.AppendLine("      ,SttlUserID      =@SttlUserID     ");
            sql.AppendLine("      ,SttlDate        =@SttlDate       ");
            sql.AppendLine(" WHERE ID=@ID                           ");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", SubSellOrderM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", SubSellOrderM.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", SubSellOrderM.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", SubSellOrderM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendMode", SubSellOrderM.SendMode));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Seller", SubSellOrderM.Seller));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", SubSellOrderM.CustName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", SubSellOrderM.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustMobile", SubSellOrderM.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", SubSellOrderM.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", SubSellOrderM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", SubSellOrderM.Rate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderMethod", SubSellOrderM.OrderMethod));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeType", SubSellOrderM.TakeType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayType", SubSellOrderM.PayType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoneyType", SubSellOrderM.MoneyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", SubSellOrderM.OrderDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isAddTax", SubSellOrderM.isAddTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanOutDate", SubSellOrderM.PlanOutDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", SubSellOrderM.OutDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CarryType", SubSellOrderM.CarryType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", SubSellOrderM.BusiStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDeptID", SubSellOrderM.OutDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutUserID", SubSellOrderM.OutUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NeedSetup", SubSellOrderM.NeedSetup));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanSetDate", SubSellOrderM.PlanSetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetDate", SubSellOrderM.SetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetUserInfo", SubSellOrderM.SetUserInfo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", SubSellOrderM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tax", SubSellOrderM.Tax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalFee", SubSellOrderM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", SubSellOrderM.Discount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleFeeTotal", SubSellOrderM.SaleFeeTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountTotal", SubSellOrderM.DiscountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealTotal", SubSellOrderM.RealTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayedTotal", SubSellOrderM.PayedTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WairPayTotal", SubSellOrderM.WairPayTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", SubSellOrderM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", SubSellOrderM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", SubSellOrderM.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", SubSellOrderM.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", SubSellOrderM.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", SubSellOrderM.ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", SubSellOrderM.Closer));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", SubSellOrderM.CloseDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", SubSellOrderM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", SubSellOrderM.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", SubSellOrderM.Attachment));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", SubSellOrderM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isOpenbill", SubSellOrderM.isOpenbill));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlUserID", SubSellOrderM.SttlUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlDate", SubSellOrderM.SttlDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", SubSellOrderM.ID));
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 删除
        public static SqlCommand DeleteSubSellOrder(string IDs)
        {
            SqlCommand comm = new SqlCommand();

            String sql = "DELETE FROM officedba.SubSellOrder WHERE ID in (" + IDs + ")";

            comm.CommandText = sql;

            return comm;
        }
        #endregion

        #region 确认
        //确认时填写确认人，确认时间，改变单据状态和业务状态
        //参数：单据ID
        public static SqlCommand ConfirmSubSellOrder(string ID)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.SubSellOrder     ");
            sql.AppendLine("   SET Confirmor = @Confirmor     ");
            sql.AppendLine("      ,ConfirmDate = @ConfirmDate ");
            sql.AppendLine("      ,ModifiedUserID=@ModifiedUserID");
            sql.AppendLine("      ,ModifiedDate=getdate()");
            sql.AppendLine("      ,BillStatus = @BillStatus   ");
            sql.AppendLine("      ,BusiStatus = @BusiStatus   ");
            sql.AppendLine(" WHERE ID=@ID                     ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", DateTime.Now.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql.ToString();
            return comm;
        }

        //确认时如果客户不存在于零售客户表中，则插入该条客户记录
        public static SqlCommand InsertCustInfo(SubSellCustInfoModel SubSellCustInfoM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.SubSellCustInfo ");
            sql.AppendLine("           (CompanyCD                 ");
            sql.AppendLine("           ,DeptID                    ");
            sql.AppendLine("           ,CustName                  ");
            sql.AppendLine("           ,CustTel                   ");
            sql.AppendLine("           ,CustMobile                ");
            sql.AppendLine("           ,CustAddr                  ");
            sql.AppendLine("           ,Creator                   ");
            sql.AppendLine("           ,CreateDate)               ");
            sql.AppendLine("     VALUES                           ");
            sql.AppendLine("     	   (@CompanyCD                ");
            sql.AppendLine("           ,@DeptID                   ");
            sql.AppendLine("           ,@CustName                 ");
            sql.AppendLine("           ,@CustTel                  ");
            sql.AppendLine("           ,@CustMobile               ");
            sql.AppendLine("           ,@CustAddr                 ");
            sql.AppendLine("           ,@Creator                  ");
            sql.AppendLine("           ,getdate())              ");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", SubSellCustInfoM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", SubSellCustInfoM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", SubSellCustInfoM.CustName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", SubSellCustInfoM.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustMobile", SubSellCustInfoM.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", SubSellCustInfoM.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", SubSellCustInfoM.Creator));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 判断是否可以取消确认
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool CanConcelConfirm(string ID)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" SELECT BillStatus ");
            sql.AppendLine("       ,BusiStatus ");
            sql.AppendLine(" FROM officedba.SubSellOrder ");
            sql.AppendLine(" WHERE ID=@ID ");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql.ToString();

            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt.Rows[0]["BillStatus"].ToString().Trim() == "2" && dt.Rows[0]["BusiStatus"].ToString().Trim() == "2")
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 执行取消取消确认操作
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SqlCommand ConcelConfirm(string ID)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.SubSellOrder ");
            sql.AppendLine(" SET BusiStatus = @BusiStatus");
            sql.AppendLine("    ,BillStatus = @BillStatus");
            sql.AppendLine("    ,Confirmor  = @Confirmor");
            sql.AppendLine("    ,ConfirmDate= @ConfirmDate");
            sql.AppendLine("    ,ModifiedDate = @ModifiedDate");
            sql.AppendLine("    ,ModifiedUserID = @ModifiedUserID");
            sql.AppendLine(" WHERE ID=@ID  ");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", string.Empty.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", string.Empty.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd")));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 销售订单发货确认
        public static SqlCommand ConfirmOutSubSellOrder(SubSellOrderModel SubSellOrderM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.SubSellOrder     ");
            //发货信息
            sql.AppendLine("   SET PlanOutDate     =@PlanOutDate    ");
            sql.AppendLine("      ,OutDate         =@OutDate        ");
            sql.AppendLine("      ,CarryType       =@CarryType      ");
            sql.AppendLine("      ,BusiStatus      =@BusiStatus     ");
            sql.AppendLine("      ,BillStatus      =@BillStatus     ");
            sql.AppendLine("      ,OutDeptID       =@OutDeptID      ");
            sql.AppendLine("      ,OutUserID       =@OutUserID      ");
            sql.AppendLine("      ,CustAddr        =@CustAddr       ");
            sql.AppendLine("      ,NeedSetup       =@NeedSetup      ");
            sql.AppendLine("      ,PlanSetDate     =@PlanSetDate    ");
            sql.AppendLine("      ,SetDate         =@SetDate        ");
            sql.AppendLine("      ,SetUserInfo       =@SetUserInfo      ");
            sql.AppendLine("      ,SttlUserID      =@SttlUserID     ");
            sql.AppendLine("      ,SttlDate        =@SttlDate       ");
            sql.AppendLine("      ,Closer           =@Closer");
            sql.AppendLine("      ,CloseDate        =@CloseDate");

            //已付余款金额和未付
            sql.AppendLine("      ,PayedTotal      =@PayedTotal     ");
            sql.AppendLine("      ,WairPayTotal    =@WairPayTotal   ");
            sql.AppendLine(" WHERE ID=@ID                           ");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanOutDate", SubSellOrderM.PlanOutDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", SubSellOrderM.OutDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CarryType", SubSellOrderM.CarryType));
            if (Convert.ToDouble(SubSellOrderM.WairPayTotal) > 0.01)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", "3"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", string.Empty));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", string.Empty));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", "4"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "5"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", SubSellOrderM.Creator));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", SubSellOrderM.CreateDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDeptID", SubSellOrderM.OutDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutUserID", SubSellOrderM.OutUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", SubSellOrderM.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NeedSetup", SubSellOrderM.NeedSetup));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanSetDate", SubSellOrderM.PlanSetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetDate", SubSellOrderM.SetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetUserInfo", SubSellOrderM.SetUserInfo));

            if (Convert.ToDouble(SubSellOrderM.PayedTotal) > 0.01)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlUserID", SubSellOrderM.Creator));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlDate", SubSellOrderM.CreateDate));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlUserID", string.Empty));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlDate", string.Empty));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayedTotal", SubSellOrderM.PayedTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WairPayTotal", SubSellOrderM.WairPayTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", SubSellOrderM.ID));
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }


        /// <summary>
        /// 发货模式为分店发货时，判断该物品信息可以确认么
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="ProductID">产品ID</param>
        /// <param name="OutCount">出库数量</param>
        /// <param name="BatchNo">批次</param>
        /// <returns>1 则数量合法，2 则确认后会有负库存，且不允许出现负库存，3 则确认后会有负库存，允许出现负库存</returns>
        public static int CanConfirmOutSub(string DeptID, string ProductID, string OutCount, string BatchNo)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" SELECT A.ProductID,A.DeptID,SUM(A.ProductCount) AS ProductCount,B.MinusIs");
            sql.AppendLine(" FROM officedba.SubStorageProduct AS A");
            sql.AppendLine(" LEFT JOIN officedba.ProductInfo AS B ON A.ProductID=B.ID ");
            sql.AppendLine(" WHERE A.ProductID=@ProductID");
            sql.AppendLine(" AND A.DeptID=@DeptID");
            sql.AppendLine(" AND ISNULL(A.BatchNo,'')=ISNULL(@BatchNo,'')");
            sql.AppendLine(" GROUP  BY A.ProductID,A.DeptID,B.MinusIs");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));

            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            foreach (DataRow dr in dt.Rows)
            {
                //若该物品不允许出现负库存
                if (Convert.ToDouble(dr["ProductCount"]) < Convert.ToDouble(OutCount))
                {
                    if (dr["MinusIs"].ToString() == "0")
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 更新分店存量表 SubStorageProduct
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ProductID"></param>
        /// <param name="OutCount"></param>
        /// <returns></returns>
        public static SqlCommand UpdateSubStorageProduct(string CompanyCD, string DeptID, string ProductID, string OutCount)
        {
            //取相同记录的其中一个ID 唯一
            int myID = 0;
            SqlCommand myComm = new SqlCommand();
            SqlCommand comm = new SqlCommand();
            string mysql = " select ID from officedba.SubStorageProduct as a  where ProductID=" + ProductID + " and CompanyCD='" + CompanyCD + "' and DeptID=" + DeptID + "";
            DataTable mydt = SqlHelper.ExecuteSql(mysql);
            if (mydt.Rows.Count > 0)
            {
                myID = int.Parse(mydt.Rows[0]["ID"].ToString());
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.SubStorageProduct ");
                sql.AppendLine("   SET ProductCount = ProductCount-@OutCount");
                sql.AppendLine("  WHERE ID=@ID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", myID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutCount", OutCount));

                comm.CommandText = sql.ToString();
            }
            return comm;
        }

        /// <summary>
        /// 发货模式为总部发货时，判断该物品信息可以确认么
        /// </summary>
        /// <param name="StorageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="OutCount"></param>
        /// <returns></returns>
        public static int CanConfirmOutHq(string StorageID, string ProductID, string OutCount, string BatchNo)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT A.ProductID,A.StorageID ");
            sql.AppendLine(", isnull(A.ProductCount,0)-isnull(A.OrderCount,0)-isnull(A.OutCount,0)+isnull(A.RoadCount,0)+isnull(A.InCount,0) AS ProductCount ");
            sql.AppendLine(",B.MinusIs FROM officedba.StorageProduct AS A ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.ProductID=B.ID ");
            sql.AppendLine(" WHERE A.ProductID=@ProductID");
            sql.AppendLine(" AND A.StorageID=@StorageID");
            sql.AppendLine(" AND ISNULL(A.BatchNo,'')=ISNULL(@BatchNo,'')");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));

            comm.CommandText = sql.ToString();

            DataTable dt = SqlHelper.ExecuteSearch(comm);
            foreach (DataRow dr in dt.Rows)
            {
                //若该物品不允许出现负库存
                if (Convert.ToDecimal(dr["ProductCount"].ToString()) < Convert.ToDecimal(OutCount))
                {
                    if (dr["MinusIs"].ToString() == "0")
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 更新分仓存量表
        /// </summary>
        /// <param name="StorageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="OutCount"></param>
        /// <returns></returns>
        public static SqlCommand UpdateStorageProduct(string StorageID, string ProductID, string OutCount)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE officedba.StorageProduct ");
            sql.AppendLine(" SET ProductCount= ProductCount-@OutCount");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD ");
            sql.AppendLine(" AND StorageID=@StorageID");
            sql.AppendLine(" AND ProductID=@ProductID");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutCount", OutCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 销售订单结算确认
        //更新结算人，结算时间，安装信息，已付款信息
        public static SqlCommand ConfirmSettSubSellOrder(SubSellOrderModel SubSellOrderM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.SubSellOrder     ");
            sql.AppendLine("   SET isOpenbill      =@isOpenbill     ");
            sql.AppendLine("      ,SttlUserID      =@SttlUserID     ");
            sql.AppendLine("      ,SttlDate        =@SttlDate       ");

            sql.AppendLine("      ,BusiStatus      =@BusiStatus     ");
            sql.AppendLine("      ,BillStatus      =@BillStatus     ");
            sql.AppendLine("      ,Closer          =@Closer         ");
            sql.AppendLine("      ,CloseDate       =@CloseDate      ");


            sql.AppendLine("      ,NeedSetup       =@NeedSetup      ");
            sql.AppendLine("      ,PlanSetDate     =@PlanSetDate    ");
            sql.AppendLine("      ,SetDate         =@SetDate        ");
            sql.AppendLine("      ,SetUserInfo       =@SetUserInfo      ");

            sql.AppendLine("      ,PayedTotal      =@PayedTotal     ");
            sql.AppendLine("      ,WairPayTotal    =@WairPayTotal   ");
            sql.AppendLine("      ,ModifiedDate    =@ModifiedDate  ");
            sql.AppendLine("      ,ModifiedUserID  =@ModifiedUserID  ");
            sql.AppendLine(" WHERE ID=@ID                           ");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@isOpenbill", SubSellOrderM.isOpenbill));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlUserID", SubSellOrderM.SttlUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SttlDate", SubSellOrderM.SttlDate));

            if (System.Math.Abs(Convert.ToDouble(SubSellOrderM.WairPayTotal)) < 0.01)
            {//结算完成
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", "4"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "5"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", SubSellOrderM.Creator));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", SubSellOrderM.CreateDate));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", "3"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", string.Empty));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", string.Empty));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NeedSetup", SubSellOrderM.NeedSetup));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanSetDate", SubSellOrderM.PlanSetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetDate", SubSellOrderM.SetDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetUserInfo", SubSellOrderM.SetUserInfo));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayedTotal", SubSellOrderM.PayedTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WairPayTotal", SubSellOrderM.WairPayTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", SubSellOrderM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", SubSellOrderM.ModifiedUserID));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", SubSellOrderM.ID));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 检索客户信息
        public static SqlCommand GetCustInfo(SubSellCustInfoModel SubSellCustInfoM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ID                          ");
            sql.AppendLine("      ,CompanyCD                   ");
            sql.AppendLine("      ,DeptID                      ");
            sql.AppendLine("      ,isnull(CustName  ,'') AS  CustName                  ");
            sql.AppendLine("      ,isnull(CustTel ,'') AS   CustTel                   ");
            sql.AppendLine("      ,isnull(CustMobile ,'') AS  CustMobile                 ");
            sql.AppendLine("      ,isnull(CustAddr  ,'') AS CustAddr                   ");
            sql.AppendLine("      ,isnull(Creator ,'') AS Creator                     ");
            sql.AppendLine("      ,isnull(CreateDate,'') AS CreateDate                 ");
            sql.AppendLine("  FROM officedba.SubSellCustInfo   ");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD ");
            sql.AppendLine(" AND DeptID=@DeptID ");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", SubSellCustInfoM.DeptID));
            if (SubSellCustInfoM.CustName != "")
            {
                sql.AppendLine(" AND CustName like @CustName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", "%" + SubSellCustInfoM.CustName + "%"));
            }
            if (SubSellCustInfoM.CustTel != "")
            {
                sql.AppendLine(" AND CustTel like @CustTel");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", "%" + SubSellCustInfoM.CustTel + "%"));
            }
            if (SubSellCustInfoM.CustMobile != "")
            {
                sql.AppendLine(" AND CustMobile like @CustMobile");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustMobile", "%" + SubSellCustInfoM.CustMobile + "%"));
            }
            if (SubSellCustInfoM.CustAddr != "")
            {
                sql.AppendLine(" AND CustAddr like @CustAddr");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", "%" + SubSellCustInfoM.CustAddr + "%"));
            }
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 插入客户信息
        public static SqlCommand InsertSubSellCustInfo(SubSellCustInfoModel SubSellCustInfoM)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.SubSellCustInfo ");
            sql.AppendLine("           (CompanyCD                 ");
            sql.AppendLine("           ,DeptID                    ");
            sql.AppendLine("           ,CustName                  ");
            sql.AppendLine("           ,CustTel                   ");
            sql.AppendLine("           ,CustMobile                ");
            sql.AppendLine("           ,CustAddr                  ");
            sql.AppendLine("           ,Creator                   ");
            sql.AppendLine("           ,CreateDate)               ");
            sql.AppendLine("     VALUES                           ");
            sql.AppendLine("     	  (@CompanyCD                 ");
            sql.AppendLine("          ,@DeptID                    ");
            sql.AppendLine("          ,@CustName                  ");
            sql.AppendLine("          ,@CustTel                   ");
            sql.AppendLine("          ,@CustMobile                ");
            sql.AppendLine("          ,@CustAddr                  ");
            sql.AppendLine("          ,@Creator                   ");
            sql.AppendLine("          ,getdate())               ");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", SubSellCustInfoM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", SubSellCustInfoM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", SubSellCustInfoM.CustName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", SubSellCustInfoM.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustMobile", SubSellCustInfoM.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", SubSellCustInfoM.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));

            return comm;
        }
        #endregion

        #region 检索销售订单
        public static SqlCommand SelectSubSellOrder(SubSellOrderModel SubSellOrderM, int pageIndex, int pageCount, string OrderBy, string EFIndex, string EFDesc, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                     ");
            sql.AppendLine("      ,A.CompanyCD              ");
            sql.AppendLine("      ,A.OrderNo                ");
            sql.AppendLine("      ,A.Title                  ");
            sql.AppendLine("      ,A.DeptID                 ");
            sql.AppendLine("      ,isnull(B.DeptName,'') AS DeptName ");
            sql.AppendLine("      ,A.SendMode               ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName            ");
            sql.AppendLine("      ,A.Seller                 ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS SellerName ");
            sql.AppendLine("      ,isnull(A.CustName,'') AS CustName              ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS  CustTel              ");
            sql.AppendLine("      ,isnull(A.CustMobile,'') AS CustMobile            ");
            sql.AppendLine("      ,isnull(A.CustAddr,'') AS CustAddr              ");
            sql.AppendLine("      ,A.BusiStatus             ");
            sql.AppendLine(",case A.BusiStatus when '1' then '下单' when '2' then '出库' when '3' then '结算' when '4' then '完成' end AS BusiStatusName");
            sql.AppendLine("      ,A.BillStatus             ");
            sql.AppendLine(", case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName    ");
            sql.AppendLine("  FROM officedba.SubSellOrder AS A");
            sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS B ON A.DeptID=B.ID  ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller=C.ID");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", SubSellOrderM.CompanyCD));
            if (SubSellOrderM.OrderNo != "")
            {
                sql.AppendLine(" AND A.OrderNo like @OrderNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + SubSellOrderM.OrderNo + "%"));
            }
            if (SubSellOrderM.Title != "")
            {
                sql.AppendLine(" AND A.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + SubSellOrderM.Title + "%"));
            }
            if (SubSellOrderM.SendMode != "0")
            {
                sql.AppendLine(" AND A.SendMode = @SendMode");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendMode", SubSellOrderM.SendMode));
            }
            if (SubSellOrderM.CustName != "")
            {
                sql.AppendLine(" AND A.CustName like @CustName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", "%" + SubSellOrderM.CustName + "%"));
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and A.ExtField" + EFIndex + " LIKE @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            if (SubSellOrderM.CustTel != "")
            {
                sql.AppendLine(" AND A.CustTel like @CustTel");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustTel", "%" + SubSellOrderM.CustTel + "%"));
            }
            if (SubSellOrderM.CustAddr != "")
            {
                sql.AppendLine(" AND A.CustAddr like @CustAddr");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustAddr", "%" + SubSellOrderM.CustAddr + "%"));
            }
            if (SubSellOrderM.DeptID != "")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", SubSellOrderM.DeptID));
            }
            if (SubSellOrderM.Seller != "")
            {
                sql.AppendLine(" AND A.Seller = @Seller");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Seller", SubSellOrderM.Seller));
            }
            if (SubSellOrderM.BusiStatus != "0")
            {
                sql.AppendLine(" AND A.BusiStatus = @BusiStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiStatus", SubSellOrderM.BusiStatus));
            }
            if (SubSellOrderM.BillStatus != "0")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", SubSellOrderM.BillStatus));
            }
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 根据销售订单ID检索主表信息
        public static SqlCommand GetSubSellOrder(string ID)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT     A.ID                                                                              ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.SttlDate,23 ),'') AS SttlDate                                 ");
            sql.AppendLine(", isnull(A.CompanyCD       ,'') AS CompanyCD                                                 ");
            sql.AppendLine(", isnull(A.OrderNo         ,'') AS OrderNo                                                   ");
            sql.AppendLine(", isnull(A.Title           ,'') AS Title                                                     ");
            sql.AppendLine(", isnull(A.DeptID          ,0) AS DeptID                                                     ");
            sql.AppendLine(", isnull(B.DeptName        ,'') AS DeptName                                                  ");
            sql.AppendLine(", isnull(A.SendMode        ,'') AS SendMode                                                  ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName");
            sql.AppendLine(", isnull(A.Seller          ,0) AS Seller                                                     ");
            sql.AppendLine(", isnull(C.EmployeeName    ,'') AS SellerName                                              ");
            sql.AppendLine(", isnull(A.CustName        ,'') AS CustName                                                  ");
            sql.AppendLine(", isnull(A.CustTel         ,'') AS CustTel                                                   ");
            sql.AppendLine(", isnull(A.CustMobile      ,'') AS CustMobile                                                ");
            sql.AppendLine(", isnull(A.CustAddr        ,'') AS CustAddr                                                  ");
            sql.AppendLine(", isnull(A.CurrencyType    ,0) AS CurrencyType                                               ");
            sql.AppendLine(", isnull(A.Rate            ,0) AS Rate                                                       ");
            sql.AppendLine(", isnull(A.OrderMethod     ,0) AS OrderMethod                                                ");
            sql.AppendLine(", isnull(A.TakeType        ,0) AS TakeType                                                   ");
            sql.AppendLine(", isnull(A.PayType         ,0) AS PayType                                                    ");
            sql.AppendLine(", isnull(A.MoneyType       ,0) AS MoneyType                                                  ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.OrderDate,23 ),'') AS OrderDate                               ");
            sql.AppendLine(", isnull(A.isAddTax        ,'') AS isAddTax                                                  ");
            sql.AppendLine(", isnull(A.PlanOutDate     ,'') AS PlanOutDate                                               ");
            sql.AppendLine(", isnull(A.OutDate         ,'') AS OutDate                                                   ");
            sql.AppendLine(", isnull(A.CarryType       ,0) AS CarryType                                                  ");
            sql.AppendLine(", isnull(A.BusiStatus      ,'') AS BusiStatus                                                ");
            sql.AppendLine(",case A.BusiStatus when '1' then '下单' when '2' then '出库' when '3' then '结算' when '4' then '完成' end AS BusiStatusName");
            sql.AppendLine(", isnull(A.OutDeptID       ,0) AS OutDeptID                                                  ");
            sql.AppendLine(", isnull(D.DeptName        ,'') AS OutDeptName                                               ");
            sql.AppendLine(", isnull(A.OutUserID       ,0) AS OutUserID                                                  ");
            sql.AppendLine(", isnull(E.EmployeeName    ,'') AS OutUserName                                               ");
            sql.AppendLine(", isnull(A.NeedSetup       ,'') AS NeedSetup                                                 ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.PlanSetDate,23 ),'') AS PlanSetDate                           ");
            sql.AppendLine(", isnull(A.SetDate         ,'') AS SetDate                                                   ");
            sql.AppendLine(", isnull(A.SetUserInfo       ,'') AS SetUserInfo                                                  ");
            sql.AppendLine(", isnull(A.SetUserInfo    ,'') AS SetUserName                                               ");
            sql.AppendLine(", isnull(A.TotalPrice      ,0) AS TotalPrice                                                 ");
            sql.AppendLine(", isnull(A.Tax             ,0) AS Tax                                                        ");
            sql.AppendLine(", isnull(A.TotalFee        ,0) AS TotalFee                                                   ");
            sql.AppendLine(", isnull(A.Discount        ,0) AS Discount                                                   ");
            sql.AppendLine(", isnull(A.SaleFeeTotal    ,0) AS SaleFeeTotal                                               ");
            sql.AppendLine(", isnull(A.DiscountTotal   ,0) AS DiscountTotal                                              ");
            sql.AppendLine(", isnull(A.RealTotal       ,0) AS RealTotal                                                  ");
            sql.AppendLine(", isnull(A.PayedTotal      ,0) AS PayedTotal                                                 ");
            sql.AppendLine(", isnull(A.WairPayTotal    ,0) AS WairPayTotal                                               ");
            sql.AppendLine(", isnull(A.CountTotal      ,0) AS CountTotal                                                 ");
            sql.AppendLine(", isnull(A.BillStatus      ,'') AS BillStatus                                                ");
            sql.AppendLine(", case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单' ");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName    ");
            sql.AppendLine(", isnull(A.Creator         ,0) AS Creator                                                    ");
            sql.AppendLine(", isnull(G.EmployeeName    ,'') AS CreatorName                                               ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.CreateDate,23 ),'') AS CreateDate                             ");
            sql.AppendLine(", isnull(A.Confirmor       ,0) AS Confirmor                                                  ");
            sql.AppendLine(", isnull(H.EmployeeName    ,'') AS ConfirmorName                                             ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.ConfirmDate,23 ),'') AS ConfirmDate                           ");
            sql.AppendLine(", isnull(A.Closer          ,0) AS Closer                                                     ");
            sql.AppendLine(", isnull(I.EmployeeName    ,'') AS CloserName                                                ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.CloseDate,23 ),'') AS CloseDate                               ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.ModifiedDate,23 ),'') AS ModifiedDate                                              ");
            sql.AppendLine(", isnull(A.ModifiedUserID  ,'') AS ModifiedUserID                                            ");
            sql.AppendLine(", isnull(A.Attachment      ,'') AS Attachment                                                ");
            sql.AppendLine(", isnull(A.Remark          ,'') AS Remark                                                    ");
            sql.AppendLine(", isnull(A.isOpenbill      ,'') AS isOpenbill                                                ");
            sql.AppendLine(", isnull(A.SttlUserID      ,'') AS SttlUserID                                                ");
            sql.AppendLine(", isnull(J.EmployeeName    ,'') AS SttlUserName                                              ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(23),A.SttlDate,23 ),'') AS SttlDate                                 ");
            sql.AppendLine(", A.ExtField1,A.ExtField2,A.ExtField3,A.ExtField4,A.ExtField5,A.ExtField6,A.ExtField7,A.ExtField8,A.ExtField9,A.ExtField10 ");
            sql.AppendLine("FROM officedba.SubSellOrder AS A                                                             ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS B ON A.DeptID = B.ID AND A.CompanyCD = B.CompanyCD           ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller = C.ID AND A.CompanyCD = C.CompanyCD       ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.OutDeptID = D.ID AND A.CompanyCD = D.CompanyCD        ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS E ON A.OutUserID = E.ID AND A.CompanyCD = E.CompanyCD    ");
            //sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.SetUserInfo = F.ID AND A.CompanyCD = F.CompanyCD    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.Creator = G.ID AND A.CompanyCD = G.CompanyCD      ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS H ON A.Confirmor = H.ID AND A.CompanyCD = H.CompanyCD    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS I ON A.Closer = I.ID AND A.CompanyCD = I.CompanyCD       ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS J ON A.SttlUserID = J.ID AND A.CompanyCD = J.CompanyCD   ");
            sql.AppendLine("WHERE A.ID=@ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql.ToString();

            return comm;
        }
        /// <summary>
        ///  单据打印
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SqlCommand GetSubSellOrderPrint(string ID)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT     A.ID                                                                              ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(10),A.SttlDate,23 ),'') AS SttlDate                                 ");
            sql.AppendLine(", isnull(A.CompanyCD       ,'') AS CompanyCD                                                 ");
            sql.AppendLine(", isnull(A.OrderNo         ,'') AS OrderNo                                                   ");
            sql.AppendLine(", isnull(A.Title           ,'') AS Title                                                     ");
            sql.AppendLine(", isnull(A.DeptID          ,0) AS DeptID                                                     ");
            sql.AppendLine(", isnull(B.DeptName        ,'') AS DeptName                                                  ");
            sql.AppendLine(", isnull(A.SendMode        ,'') AS SendMode                                                  ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName");
            sql.AppendLine(", isnull(A.Seller          ,0) AS Seller                                                     ");
            sql.AppendLine(", isnull(C.EmployeeName    ,'') AS SellerName                                              ");
            sql.AppendLine(", isnull(A.CustName        ,'') AS CustName                                                  ");
            sql.AppendLine(", isnull(A.CustTel         ,'') AS CustTel                                                   ");
            sql.AppendLine(", isnull(A.CustMobile      ,'') AS CustMobile                                                ");
            sql.AppendLine(", isnull(A.CustAddr        ,'') AS CustAddr                                                  ");
            sql.AppendLine(", isnull(A.CurrencyType    ,0) AS CurrencyType                                               ");
            sql.AppendLine(", isnull(A.Rate    ,0) AS Rate                                               ");
            sql.AppendLine(", isnull(A.OrderMethod     ,0) AS OrderMethod                                                ");
            sql.AppendLine(", isnull(A.TakeType        ,0) AS TakeType                                                   ");
            sql.AppendLine(", isnull(A.PayType         ,0) AS PayType                                                    ");
            sql.AppendLine(", isnull(A.MoneyType       ,0) AS MoneyType                                                  ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(10),A.OrderDate,23 ),'') AS OrderDate                               ");
            sql.AppendLine(",case isnull(A.isAddTax,0)  when '1' then '是' when '0' then '否'  end as isAddTax");
            sql.AppendLine(", isnull(A.PlanOutDate     ,'') AS PlanOutDate                                               ");
            sql.AppendLine(", isnull(A.OutDate         ,'') AS OutDate                                                   ");
            sql.AppendLine(", isnull(A.CarryType       ,0) AS CarryType                                                  ");
            sql.AppendLine(", isnull(A.BusiStatus      ,'') AS BusiStatus                                                ");
            sql.AppendLine(",case A.BusiStatus when '1' then '下单' when '2' then '出库' when '3' then '结算' when '4' then '完成' end AS BusiStatusName");
            sql.AppendLine(", isnull(A.OutDeptID       ,0) AS OutDeptID                                                  ");
            sql.AppendLine(", isnull(D.DeptName        ,'') AS OutDeptName                                               ");
            sql.AppendLine(", isnull(A.OutUserID       ,0) AS OutUserID                                                  ");
            sql.AppendLine(", isnull(E.EmployeeName    ,'') AS OutUserName                                               ");
            sql.AppendLine(",case isnull(A.NeedSetup,0)  when '1' then '是' when '0' then '否'  end as NeedSetup");
            sql.AppendLine(", isnull(A.PlanSetDate,'') AS PlanSetDate                           ");
            sql.AppendLine(", isnull(A.SetDate         ,'') AS SetDate                                                   ");
            sql.AppendLine(", isnull(A.SetUserInfo,'') AS SetUserInfo                                                  ");
            sql.AppendLine(", isnull(F.EmployeeName    ,'') AS SetUserName                                               ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TotalPrice,0)) TotalPrice                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.Tax,0)) Tax                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TotalFee,0)) TotalFee                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.Discount,0)) Discount                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.SaleFeeTotal,0)) SaleFeeTotal                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.DiscountTotal,0)) DiscountTotal                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.RealTotal,0)) RealTotal                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.PayedTotal,0)) PayedTotal                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.WairPayTotal,0)) WairPayTotal                                                      ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.CountTotal,0)) CountTotal                                                      ");
            sql.AppendLine(", isnull(A.BillStatus      ,'') AS BillStatus                                                ");
            sql.AppendLine(", case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName    ");
            sql.AppendLine(", isnull(A.Creator         ,0) AS Creator                                                    ");
            sql.AppendLine(", isnull(G.EmployeeName    ,'') AS CreatorName                                               ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(10),A.CreateDate,23 ),'') AS CreateDate                             ");
            sql.AppendLine(", isnull(A.Confirmor       ,0) AS Confirmor                                                  ");
            sql.AppendLine(", isnull(H.EmployeeName    ,'') AS ConfirmorName                                             ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(10),A.ConfirmDate,23 ),'') AS ConfirmDate                           ");
            sql.AppendLine(", isnull(A.Closer          ,0) AS Closer                                                     ");
            sql.AppendLine(", isnull(I.EmployeeName    ,'') AS CloserName                                                ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(10),A.CloseDate,23 ),'') AS CloseDate                               ");
            sql.AppendLine(", isnull(CONVERT(VARCHAR(10),A.ModifiedDate,23 ),'') AS ModifiedDate                                              ");
            sql.AppendLine(", isnull(A.ModifiedUserID  ,'') AS ModifiedUserID                                            ");
            sql.AppendLine(", isnull(A.Attachment      ,'') AS Attachment                                                ");
            sql.AppendLine(", isnull(A.Remark          ,'') AS Remark                                                    ");
            sql.AppendLine(",case isnull(A.isOpenbill,0)  when '1' then '是' when '0' then '否'  end as isOpenbill");
            sql.AppendLine(", isnull(A.SttlUserID      ,'') AS SttlUserID                                                ");
            sql.AppendLine(", isnull(J.EmployeeName    ,'') AS SttlUserName                                              ");

            sql.AppendLine(", isnull(K.TypeName    ,'') AS OrderMethodTypeName                                            ");
            sql.AppendLine(", isnull(L.TypeName    ,'') AS TakeTypeTypeName                                            ");
            sql.AppendLine(", isnull(M.TypeName    ,'') AS PayTypeTypeName                                            ");
            sql.AppendLine(", isnull(N.TypeName    ,'') AS MoneyTypeTypeName                                            ");
            sql.AppendLine(", isnull(O.TypeName    ,'') AS CarryTypeTypeName                                            ");
            sql.AppendLine(", isnull(P.CurrencyName    ,'') AS CurrencyName                                            ");
            sql.AppendLine(", A.ExtField1,A.ExtField2,A.ExtField3,A.ExtField4,A.ExtField5,A.ExtField6,A.ExtField7,A.ExtField8,A.ExtField9,A.ExtField10 ");

            sql.AppendLine("FROM officedba.SubSellOrder AS A                                                             ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS B ON A.DeptID = B.ID AND A.CompanyCD = B.CompanyCD           ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller = C.ID AND A.CompanyCD = C.CompanyCD       ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.OutDeptID = D.ID AND A.CompanyCD = D.CompanyCD        ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS E ON A.OutUserID = E.ID AND A.CompanyCD = E.CompanyCD    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.SetUserInfo = F.ID AND A.CompanyCD = F.CompanyCD    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.Creator = G.ID AND A.CompanyCD = G.CompanyCD      ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS H ON A.Confirmor = H.ID AND A.CompanyCD = H.CompanyCD    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS I ON A.Closer = I.ID AND A.CompanyCD = I.CompanyCD       ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS J ON A.SttlUserID = J.ID AND A.CompanyCD = J.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS K ON A.OrderMethod = K.ID AND A.CompanyCD = K.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS L ON A.TakeType = L.ID AND A.CompanyCD = L.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS M ON A.PayType = M.ID AND A.CompanyCD = M.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS N ON A.MoneyType = N.ID AND A.CompanyCD = N.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS O ON A.CarryType = O.ID AND A.CompanyCD = O.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CurrencyTypeSetting AS P ON A.CurrencyType = P.ID AND A.CompanyCD = P.CompanyCD   ");


            sql.AppendLine("WHERE A.ID=@ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion
        #endregion

        #region 分店销售订单明细表操作

        #region 新增
        public static SqlCommand InsertSubSellOrderDetail(SubSellOrderDetailModel SubSellOrderDetailM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.SubSellOrderDetail ");
            sql.AppendLine("           (CompanyCD                    ");
            sql.AppendLine("           ,DeptID                       ");
            sql.AppendLine("           ,OrderNo                      ");
            sql.AppendLine("           ,SortNo                       ");
            sql.AppendLine("           ,ProductID                    ");
            sql.AppendLine("           ,StorageID                    ");
            sql.AppendLine("           ,ProductCount                 ");
            sql.AppendLine("           ,UnitID                       ");
            sql.AppendLine("           ,UnitPrice                    ");
            sql.AppendLine("           ,TaxPrice                     ");
            sql.AppendLine("           ,Discount                     ");
            sql.AppendLine("           ,TaxRate                      ");
            sql.AppendLine("           ,TotalFee                     ");
            sql.AppendLine("           ,TotalPrice                   ");
            sql.AppendLine("           ,TotalTax                     ");
            sql.AppendLine("           ,Remark                       ");
            sql.AppendLine("           ,UsedUnitID                   ");
            sql.AppendLine("           ,UsedUnitCount                ");
            sql.AppendLine("           ,UsedPrice                    ");
            sql.AppendLine("           ,BatchNo                    ");
            sql.AppendLine("           ,ExRate)                      ");
            sql.AppendLine("     VALUES	 (                           ");
            sql.AppendLine("            @CompanyCD                   ");
            sql.AppendLine("           ,@DeptID                      ");
            sql.AppendLine("           ,@OrderNo                     ");
            sql.AppendLine("           ,@SortNo                      ");
            sql.AppendLine("           ,@ProductID                   ");
            sql.AppendLine("           ,@StorageID                   ");
            sql.AppendLine("           ,@ProductCount                ");
            sql.AppendLine("           ,@UnitID                      ");
            sql.AppendLine("           ,@UnitPrice                   ");
            sql.AppendLine("           ,@TaxPrice                    ");
            sql.AppendLine("           ,@Discount                    ");
            sql.AppendLine("           ,@TaxRate                     ");
            sql.AppendLine("           ,@TotalFee                    ");
            sql.AppendLine("           ,@TotalPrice                  ");
            sql.AppendLine("           ,@TotalTax                    ");
            sql.AppendLine("           ,@Remark                     ");
            sql.AppendLine("           ,@UsedUnitID                   ");
            sql.AppendLine("           ,@UsedUnitCount                ");
            sql.AppendLine("           ,@UsedPrice                    ");
            sql.AppendLine("           ,@BatchNo                    ");
            sql.AppendLine("           ,@ExRate)                      ");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", SubSellOrderDetailM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", SubSellOrderDetailM.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderNo", SubSellOrderDetailM.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@SortNo", SubSellOrderDetailM.SortNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", SubSellOrderDetailM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@StorageID", SubSellOrderDetailM.StorageID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", SubSellOrderDetailM.ProductCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@UnitID", SubSellOrderDetailM.UnitID));
            comm.Parameters.Add(SqlHelper.GetParameter("@UnitPrice", SubSellOrderDetailM.UnitPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaxPrice", SubSellOrderDetailM.TaxPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@Discount", SubSellOrderDetailM.Discount));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaxRate", SubSellOrderDetailM.TaxRate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalFee", SubSellOrderDetailM.TotalFee));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", SubSellOrderDetailM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalTax", SubSellOrderDetailM.TotalTax));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", SubSellOrderDetailM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", SubSellOrderDetailM.UsedUnitID));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", SubSellOrderDetailM.UsedUnitCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", SubSellOrderDetailM.UsedPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@BatchNo", SubSellOrderDetailM.BatchNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ExRate", SubSellOrderDetailM.ExRate));
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 删除
        public static SqlCommand DeleteSubSellOrderDetail(string OrderNo)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.SubSellOrderDetail");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND OrderNo in ('" + OrderNo + "')");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 根据销售订单ID，检索相关明细操作
        public static SqlCommand GetSubSellOrderDetail(string ID)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                                              ");
            sql.AppendLine(", A.CompanyCD                                                                            ");
            sql.AppendLine(", A.UsedUnitID                                                                            ");
            sql.AppendLine(", A.UsedUnitCount                                                                            ");
            sql.AppendLine(", A.UsedPrice                                                                            ");
            sql.AppendLine(", A.ExRate                                                                            ");
            sql.AppendLine(", A.DeptID                                                                               ");
            sql.AppendLine(", A.OrderNo                                                                              ");
            sql.AppendLine(", A.SortNo                                                                               ");
            sql.AppendLine(", A.ProductID                                                                            ");
            sql.AppendLine(", B.ProdNo AS ProductNo                                                                  ");
            sql.AppendLine(", B.ProductName                                                                          ");
            sql.AppendLine(", B.Specification                                                                        ");
            sql.AppendLine(", A.StorageID                                                                            ");
            sql.AppendLine(", A.ProductCount                                                                         ");
            sql.AppendLine(", A.UnitID                                                                               ");
            sql.AppendLine(", C.CodeName  AS UnitName                                                                ");
            sql.AppendLine(", A.UnitPrice                                                                            ");
            sql.AppendLine(", A.TaxPrice                                                                             ");
            sql.AppendLine(", A.Discount                                                                             ");
            sql.AppendLine(", A.TaxRate                                                                              ");
            sql.AppendLine(", A.TotalFee                                                                             ");
            sql.AppendLine(", A.TotalPrice                                                                           ");
            sql.AppendLine(", isnull(A.BackCount,0) AS BackCount             ");
            sql.AppendLine(", A.TotalTax                                                                             ");
            sql.AppendLine(", A.Remark,A.BatchNo,B.IsBatchNo                                                                               ");
            sql.AppendLine("FROM officedba.SubSellOrderDetail AS A                                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.ProductID = B.ID AND A.CompanyCD = B.CompanyCD ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.UnitID = C.ID AND A.CompanyCD = C.CompanyCD   ");
            sql.AppendLine("INNER JOIN officedba.SubSellOrder AS D ON A.CompanyCD=D.CompanyCD AND A.OrderNo=D.OrderNo AND A.DeptID=D.DeptID AND D.ID=@ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();

            return comm;
        }
        public static SqlCommand GetSubSellOrderDetailPrint(string ID)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                                              ");
            sql.AppendLine(", A.CompanyCD                                                                            ");
            sql.AppendLine(", A.DeptID                                                                               ");
            sql.AppendLine(", A.OrderNo                                                                              ");
            sql.AppendLine(", A.SortNo                                                                               ");
            sql.AppendLine(", A.ProductID                                                                            ");
            sql.AppendLine(", B.ProdNo AS ProductNo                                                                  ");
            sql.AppendLine(", B.ProductName                                                                          ");
            sql.AppendLine(", B.Specification                                                                        ");
            sql.AppendLine(", E.StorageName AS StorageID                                                                            ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.ProductCount,0)) ProductCount");
            sql.AppendLine(", A.UnitID                                                                               ");
            sql.AppendLine(", C.CodeName  AS UnitName                                                                ");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.UnitPrice,0)) UnitPrice");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TaxPrice,0)) TaxPrice");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.Discount,0)) Discount");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TaxRate,0)) TaxRate");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TotalFee,0)) TotalFee");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TotalPrice,0)) TotalPrice");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.BackCount,0)) BackCount");
            sql.AppendLine(", convert(numeric(12,2),isnull(A.TotalTax,0)) TotalTax");
            sql.AppendLine(", A.Remark,A.BatchNo,B.IsBatchNo,A.UsedUnitID,A.UsedUnitCount,CU.CodeName AS UsedUnitName ");
            sql.AppendLine("FROM officedba.SubSellOrderDetail AS A                                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.ProductID = B.ID AND A.CompanyCD = B.CompanyCD ");
            sql.AppendLine("LEFT JOIN officedba.StorageInfo AS E ON A.StorageID = E.ID AND A.CompanyCD = E.CompanyCD ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C ON A.UnitID = C.ID AND A.CompanyCD = C.CompanyCD   ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS CU ON A.UsedUnitID = CU.ID AND A.CompanyCD = CU.CompanyCD   ");
            sql.AppendLine("INNER JOIN officedba.SubSellOrder AS D ON A.CompanyCD=D.CompanyCD AND A.OrderNo=D.OrderNo AND A.DeptID=D.DeptID AND D.ID=@ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #endregion

        #region 分店物品控件
        /// <summary>
        /// 分店物品控件
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="Rate"></param>
        /// <param name="LastRate"></param>
        /// <param name="totalCount"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="ProductName"></param>
        /// <param name="ProductNo"></param>
        /// <param name="Specification"></param>
        /// <returns></returns>
        public static DataTable GetProdPrice(string CompanyCD, string DeptID, int pageIndex
            , int pageCount, string OrderBy, string Rate, string LastRate, ref int totalCount
            , string EFIndex, string EFDesc, string ProductName, string ProductNo, string Specification)
        {
            try
            {
                DataTable AllDt = new DataTable();
                AllDt.Columns.Add(new DataColumn("ProductID", typeof(Int32)));
                AllDt.Columns.Add(new DataColumn("ProductNo", typeof(string)));
                AllDt.Columns.Add(new DataColumn("ProductName", typeof(string)));
                AllDt.Columns.Add(new DataColumn("Specification", typeof(string)));
                AllDt.Columns.Add(new DataColumn("UnitID", typeof(Int32)));
                AllDt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                AllDt.Columns.Add(new DataColumn("SubPriceTax", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("SubPrice", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("SubTax", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("Discount", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("Flag", typeof(string)));
                AllDt.Columns.Add(new DataColumn("IsBatchNo", typeof(string)));
                AllDt.Columns.Add(new DataColumn("ProductCount", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("BatchNo", typeof(string)));
                SqlCommand mycomm = new SqlCommand();
                string mysql = @" SELECT DISTINCT a.ProductID,a.ProductCount,a.BatchNo 
                            FROM officedba.SubStorageProduct as a 
                            INNER JOIN officedba.ProductInfo as b ON a.ProductID=b.ID AND b.CheckStatus=1
                            WHERE a.CompanyCD=@CompanyCD AND a.DeptID=@DeptID";
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
                if (!String.IsNullOrEmpty(ProductName))
                {
                    mysql += " AND b.ProductName LIKE @ProductName";
                    mycomm.Parameters.Add(SqlHelper.GetParameter("@ProductName", String.Format("%{0}%", ProductName)));
                }
                if (!String.IsNullOrEmpty(ProductNo))
                {
                    mysql += " AND b.ProdNo LIKE @ProductNo";
                    mycomm.Parameters.Add(SqlHelper.GetParameter("@ProductNo", String.Format("%{0}%", ProductNo)));
                }
                if (!String.IsNullOrEmpty(Specification))
                {
                    mysql += " AND b.Specification LIKE @Specification";
                    mycomm.Parameters.Add(SqlHelper.GetParameter("@Specification", String.Format("%{0}%", Specification)));
                }
                mycomm.CommandText = mysql;
                //获取该分店的所有物品
                DataTable myDt = SqlHelper.PagerWithCommand(mycomm, pageIndex, pageCount, "ProductID DESC ", ref totalCount);
                foreach (DataRow drdr in myDt.Rows)
                {
                    StringBuilder mysql1 = new StringBuilder();

                    string BarCodeSql1 = string.Empty;
                    if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                    {
                        BarCodeSql1 = " AND b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                    }

                    mysql1.AppendLine(" select isnull(a.ProductID,0) as ProductID,isnull(b.ProdNo,'') as ProductNo,isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification,isnull(b.UnitID,0) as UnitID,isnull(c.CodeName,'') as UnitName,b.IsBatchNo");
                    mysql1.AppendLine("  ,isnull(a.SubPriceTax,0)/" + Rate + " as SubPriceTax");
                    mysql1.AppendLine("  ,isnull(a.SubPrice,0)/" + Rate + " as SubPrice ");
                    mysql1.AppendLine("       ,isnull(a.SubTax,0) as SubTax,isnull(a.Discount,0) as Discount,'' as Flag ");
                    mysql1.AppendLine("        from officedba.SubProductSellPrice as a left  join officedba.ProductInfo as b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD");
                    mysql1.AppendLine("       left join officedba.CodeUnitType as c on b.UnitID=c.ID and a.CompanyCD=c.CompanyCD ");
                    mysql1.AppendLine("  where a.ProductID=" + drdr["ProductID"] + " and (a.DeptID=0 OR  a.DeptID=" + DeptID + ") and a.CompanyCD='" + CompanyCD + "'" + BarCodeSql1);
                    mysql1.AppendLine(" ORDER BY a.DeptID DESC ");
                    DataTable mydt1 = SqlHelper.ExecuteSql(mysql1.ToString());
                    DataRow mydr1 = AllDt.NewRow();
                    if (mydt1.Rows.Count > 0)
                    {
                        mydr1["ProductID"] = mydt1.Rows[0]["ProductID"];
                        mydr1["ProductNo"] = mydt1.Rows[0]["ProductNo"];
                        mydr1["ProductName"] = mydt1.Rows[0]["ProductName"];
                        mydr1["Specification"] = mydt1.Rows[0]["Specification"];
                        mydr1["UnitID"] = mydt1.Rows[0]["UnitID"];
                        mydr1["UnitName"] = mydt1.Rows[0]["UnitName"];
                        mydr1["SubPriceTax"] = mydt1.Rows[0]["SubPriceTax"];
                        mydr1["SubPrice"] = mydt1.Rows[0]["SubPrice"];
                        mydr1["SubTax"] = mydt1.Rows[0]["SubTax"];
                        mydr1["Discount"] = mydt1.Rows[0]["Discount"];
                        mydr1["Flag"] = mydt1.Rows[0]["Flag"];
                        mydr1["IsBatchNo"] = mydt1.Rows[0]["IsBatchNo"];
                        mydr1["ProductCount"] = drdr["ProductCount"];
                        mydr1["BatchNo"] = drdr["BatchNo"];
                        AllDt.Rows.Add(mydr1);
                        continue;
                    }
                    else
                    {
                        StringBuilder mysql2 = new StringBuilder();

                        string BarCodeSql2 = string.Empty;
                        if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                        {
                            BarCodeSql2 = " AND b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                        }

                        mysql2.AppendLine(" select isnull(a.ProductID,0) as ProductID,isnull(b.ProdNo,'') as ProductNo,isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification,isnull(b.UnitID,0) as UnitID,isnull(c.CodeName,'') as UnitName,b.IsBatchNo");
                        mysql2.AppendLine("  ,isnull(a.SendPriceTax,0)/" + Rate + " as SubPriceTax");
                        mysql2.AppendLine("  ,isnull(a.SendPrice,0)/" + Rate + " as SubPrice ");
                        mysql2.AppendLine("      ,isnull(a.SendTax,0) as SubTax,isnull(a.Discount,0) as Discount,'' as Flag ");
                        mysql2.AppendLine("      from officedba.SubProductSendPrice as a left  join officedba.ProductInfo as b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD");
                        mysql2.AppendLine("      left join officedba.CodeUnitType as c on b.UnitID=c.ID and a.CompanyCD=c.CompanyCD ");
                        mysql2.AppendLine("  where a.ProductID=" + drdr["ProductID"] + " and (a.DeptID=0 OR  a.DeptID=" + DeptID + ") and a.CompanyCD='" + CompanyCD + "'" + BarCodeSql2);
                        mysql2.AppendLine(" ORDER BY a.DeptID DESC ");
                        DataTable mydt2 = SqlHelper.ExecuteSql(mysql2.ToString());
                        if (mydt2.Rows.Count > 0)
                        {
                            DataRow mydr2 = AllDt.NewRow();
                            mydr2["ProductID"] = mydt2.Rows[0]["ProductID"];
                            mydr2["ProductNo"] = mydt2.Rows[0]["ProductNo"];
                            mydr2["ProductName"] = mydt2.Rows[0]["ProductName"];
                            mydr2["Specification"] = mydt2.Rows[0]["Specification"];
                            mydr2["UnitID"] = mydt2.Rows[0]["UnitID"];
                            mydr2["UnitName"] = mydt2.Rows[0]["UnitName"];
                            mydr2["SubPriceTax"] = mydt2.Rows[0]["SubPriceTax"];
                            mydr2["SubPrice"] = mydt2.Rows[0]["SubPrice"];
                            mydr2["SubTax"] = mydt2.Rows[0]["SubTax"];
                            mydr2["Discount"] = mydt2.Rows[0]["Discount"];
                            mydr2["Flag"] = mydt2.Rows[0]["Flag"];
                            mydr2["IsBatchNo"] = mydt2.Rows[0]["IsBatchNo"];
                            mydr2["ProductCount"] = drdr["ProductCount"];
                            mydr2["BatchNo"] = drdr["BatchNo"];
                            AllDt.Rows.Add(mydr2);
                            continue;
                        }
                        else
                        {
                            StringBuilder mysql3 = new StringBuilder();

                            string BarCodeSql3 = string.Empty;
                            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                            {
                                BarCodeSql3 = " AND a.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                            }

                            mysql3.AppendLine(" select isnull(a.ID,0) as ProductID,isnull(a.ProdNo,'') as ProductNo,isnull(a.ProductName,'') as ProductName,a.IsBatchNo,isnull(a.Specification,'') as Specification,isnull(a.UnitID,0) as UnitID,isnull(b.CodeName,'') as UnitName,isnull(a.StandardSell*(1+isnull(a.TaxRate,0)/100) ");
                            mysql3.AppendLine("  /" + Rate + ",0) as SubPriceTax");
                            mysql3.AppendLine("  ,isnull(a.StandardSell,0)/" + Rate + " as SubPrice ");
                            mysql3.AppendLine("         ,isnull(a.TaxRate,0)/100 as SubTax,isnull(a.Discount,0)  as Discount,'' as Flag ");
                            mysql3.AppendLine("        from officedba.ProductInfo as a ");
                            mysql3.AppendLine("        left join officedba.CodeUnitType as b on a.UnitID=b.ID and a.CompanyCD=b.CompanyCD ");
                            mysql3.AppendLine("  where a.ID=" + drdr["ProductID"] + "  and a.CompanyCD='" + CompanyCD + "'" + BarCodeSql3);
                            DataTable mydt3 = SqlHelper.ExecuteSql(mysql3.ToString());
                            if (mydt3.Rows.Count > 0)
                            {
                                DataRow mydr3 = AllDt.NewRow();
                                mydr3["ProductID"] = mydt3.Rows[0]["ProductID"];
                                mydr3["ProductNo"] = mydt3.Rows[0]["ProductNo"];
                                mydr3["ProductName"] = mydt3.Rows[0]["ProductName"];
                                mydr3["Specification"] = mydt3.Rows[0]["Specification"];
                                mydr3["UnitID"] = mydt3.Rows[0]["UnitID"];
                                mydr3["UnitName"] = mydt3.Rows[0]["UnitName"];
                                mydr3["SubPriceTax"] = mydt3.Rows[0]["SubPriceTax"];
                                mydr3["SubPrice"] = mydt3.Rows[0]["SubPrice"];
                                mydr3["SubTax"] = mydt3.Rows[0]["SubTax"];
                                mydr3["Discount"] = mydt3.Rows[0]["Discount"];
                                mydr3["Flag"] = mydt3.Rows[0]["Flag"];
                                mydr3["IsBatchNo"] = mydt3.Rows[0]["IsBatchNo"];
                                mydr3["ProductCount"] = drdr["ProductCount"];
                                mydr3["BatchNo"] = drdr["BatchNo"];
                                AllDt.Rows.Add(mydr3);
                            }
                        }
                    }
                }
                DataView dv = AllDt.DefaultView;
                dv.Sort = OrderBy;
                return dv.ToTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 分店物品---条码扫描---控件
        public static DataTable GetProdPrice(string CompanyCD, string DeptID, string Rate, string LastRate, string EFIndex, string EFDesc, string BarCode)
        {
            try
            {
                DataTable AllDt = new DataTable();
                AllDt.Columns.Add(new DataColumn("ProductID", typeof(Int32)));
                AllDt.Columns.Add(new DataColumn("ProductNo", typeof(string)));
                AllDt.Columns.Add(new DataColumn("ProductName", typeof(string)));
                AllDt.Columns.Add(new DataColumn("Specification", typeof(string)));
                AllDt.Columns.Add(new DataColumn("UnitID", typeof(Int32)));
                AllDt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                AllDt.Columns.Add(new DataColumn("SubPriceTax", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("SubPrice", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("SubTax", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("Discount", typeof(Decimal)));
                AllDt.Columns.Add(new DataColumn("Flag", typeof(string)));
                AllDt.Columns.Add(new DataColumn("IsBatchNo", typeof(string)));

                SqlCommand mycomm = new SqlCommand();
                string mysql = " select distinct ProductID from officedba.SubStorageProduct as a  INNER JOIN officedba.ProductInfo as b on a.ProductID=b.ID AND b.CheckStatus=1  where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID and b.BarCode=@BarCode ";
                mycomm.Parameters.Add(SqlHelper.GetParameter("@BarCode", BarCode));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                mycomm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
                mycomm.CommandText = mysql;
                //获取该分店的所有物品
                DataTable myDt = SqlHelper.ExecuteSearch(mycomm);
                foreach (DataRow drdr in myDt.Rows)
                {
                    StringBuilder mysql1 = new StringBuilder();

                    string BarCodeSql1 = string.Empty;
                    if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                    {
                        BarCodeSql1 = " AND b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                    }

                    mysql1.AppendLine(" select b.IsBatchNo, isnull(a.ProductID,0) as ProductID,isnull(b.ProdNo,'') as ProductNo,isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification,isnull(b.UnitID,0) as UnitID,isnull(c.CodeName,'') as UnitName");
                    mysql1.AppendLine("  ,isnull(a.SubPriceTax,0)/" + Rate + " as SubPriceTax");
                    mysql1.AppendLine("  ,isnull(a.SubPrice,0)/" + Rate + " as SubPrice ");
                    mysql1.AppendLine("       ,isnull(a.SubTax,0) as SubTax,isnull(a.Discount,0) as Discount,'' as Flag ");
                    mysql1.AppendLine("        from officedba.SubProductSellPrice as a left  join officedba.ProductInfo as b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD");
                    mysql1.AppendLine("       left join officedba.CodeUnitType as c on b.UnitID=c.ID and a.CompanyCD=c.CompanyCD ");
                    mysql1.AppendLine("  where a.ProductID=" + drdr["ProductID"] + " and (a.DeptID=0 OR a.DeptID=" + DeptID + ") and a.CompanyCD='" + CompanyCD + "'" + BarCodeSql1);
                    mysql1.AppendLine(" ORDER BY a.DeptID DESC ");
                    DataTable mydt1 = SqlHelper.ExecuteSql(mysql1.ToString());
                    DataRow mydr1 = AllDt.NewRow();
                    if (mydt1.Rows.Count > 0)
                    {
                        mydr1["ProductID"] = mydt1.Rows[0]["ProductID"];
                        mydr1["ProductNo"] = mydt1.Rows[0]["ProductNo"];
                        mydr1["ProductName"] = mydt1.Rows[0]["ProductName"];
                        mydr1["Specification"] = mydt1.Rows[0]["Specification"];
                        mydr1["UnitID"] = mydt1.Rows[0]["UnitID"];
                        mydr1["UnitName"] = mydt1.Rows[0]["UnitName"];
                        mydr1["SubPriceTax"] = mydt1.Rows[0]["SubPriceTax"];
                        mydr1["SubPrice"] = mydt1.Rows[0]["SubPrice"];
                        mydr1["SubTax"] = mydt1.Rows[0]["SubTax"];
                        mydr1["Discount"] = mydt1.Rows[0]["Discount"];
                        mydr1["Flag"] = mydt1.Rows[0]["Flag"];
                        mydr1["IsBatchNo"] = mydt1.Rows[0]["IsBatchNo"];
                        AllDt.Rows.Add(mydr1);
                        continue;
                    }
                    else
                    {
                        StringBuilder mysql2 = new StringBuilder();
                        string BarCodeSql2 = string.Empty;
                        if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                        {
                            BarCodeSql2 = " AND b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                        }
                        mysql2.AppendLine(" select b.IsBatchNo, isnull(a.ProductID,0) as ProductID,isnull(b.ProdNo,'') as ProductNo,isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification,isnull(b.UnitID,0) as UnitID,isnull(c.CodeName,'') as UnitName");
                        mysql2.AppendLine("  ,isnull(a.SendPriceTax,0)/" + Rate + " as SubPriceTax");
                        mysql2.AppendLine("  ,isnull(a.SendPrice,0)/" + Rate + " as SubPrice ");
                        mysql2.AppendLine("      ,isnull(a.SendTax,0) as SubTax,isnull(a.Discount,0) as Discount,'' as Flag ");
                        mysql2.AppendLine("      from officedba.SubProductSendPrice as a left  join officedba.ProductInfo as b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD");
                        mysql2.AppendLine("      left join officedba.CodeUnitType as c on b.UnitID=c.ID and a.CompanyCD=c.CompanyCD ");
                        mysql2.AppendLine("  where a.ProductID=" + drdr["ProductID"] + " and (a.DeptID=0 OR  a.DeptID=" + DeptID + ") and a.CompanyCD='" + CompanyCD + "'" + BarCodeSql2);
                        mysql2.AppendLine(" ORDER BY a.DeptID DESC ");
                        DataTable mydt2 = SqlHelper.ExecuteSql(mysql2.ToString());
                        if (mydt2.Rows.Count > 0)
                        {
                            DataRow mydr2 = AllDt.NewRow();
                            mydr2["ProductID"] = mydt2.Rows[0]["ProductID"];
                            mydr2["ProductNo"] = mydt2.Rows[0]["ProductNo"];
                            mydr2["ProductName"] = mydt2.Rows[0]["ProductName"];
                            mydr2["Specification"] = mydt2.Rows[0]["Specification"];
                            mydr2["UnitID"] = mydt2.Rows[0]["UnitID"];
                            mydr2["UnitName"] = mydt2.Rows[0]["UnitName"];
                            mydr2["SubPriceTax"] = mydt2.Rows[0]["SubPriceTax"];
                            mydr2["SubPrice"] = mydt2.Rows[0]["SubPrice"];
                            mydr2["SubTax"] = mydt2.Rows[0]["SubTax"];
                            mydr2["Discount"] = mydt2.Rows[0]["Discount"];
                            mydr2["Flag"] = mydt2.Rows[0]["Flag"];
                            mydr2["IsBatchNo"] = mydt2.Rows[0]["IsBatchNo"];
                            AllDt.Rows.Add(mydr2);
                            continue;
                        }
                        else
                        {
                            StringBuilder mysql3 = new StringBuilder();
                            string BarCodeSql3 = string.Empty;
                            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                            {
                                BarCodeSql3 = " AND a.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                            }
                            mysql3.AppendLine(" select a.IsBatchNo, isnull(a.ID,0) as ProductID,isnull(a.ProdNo,'') as ProductNo,isnull(a.ProductName,'') as ProductName,isnull(a.Specification,'') as Specification,isnull(a.UnitID,0) as UnitID,isnull(b.CodeName,'') as UnitName,isnull(a.StandardSell*(1+isnull(a.TaxRate,0)/100) ");
                            mysql3.AppendLine("  /" + Rate + ",0) as SubPriceTax");
                            mysql3.AppendLine("  ,isnull(a.StandardSell,0)/" + Rate + " as SubPrice ");
                            mysql3.AppendLine("         ,isnull(a.TaxRate,0)/100 as SubTax,isnull(a.Discount,0)  as Discount,'' as Flag ");
                            mysql3.AppendLine("        from officedba.ProductInfo as a ");
                            mysql3.AppendLine("        left join officedba.CodeUnitType as b on a.UnitID=b.ID and a.CompanyCD=b.CompanyCD ");
                            mysql3.AppendLine("  where a.ID=" + drdr["ProductID"] + "  and a.CompanyCD='" + CompanyCD + "'" + BarCodeSql3);
                            DataTable mydt3 = SqlHelper.ExecuteSql(mysql3.ToString());
                            if (mydt3.Rows.Count > 0)
                            {
                                DataRow mydr3 = AllDt.NewRow();
                                mydr3["ProductID"] = mydt3.Rows[0]["ProductID"];
                                mydr3["ProductNo"] = mydt3.Rows[0]["ProductNo"];
                                mydr3["ProductName"] = mydt3.Rows[0]["ProductName"];
                                mydr3["Specification"] = mydt3.Rows[0]["Specification"];
                                mydr3["UnitID"] = mydt3.Rows[0]["UnitID"];
                                mydr3["UnitName"] = mydt3.Rows[0]["UnitName"];
                                mydr3["SubPriceTax"] = mydt3.Rows[0]["SubPriceTax"];
                                mydr3["SubPrice"] = mydt3.Rows[0]["SubPrice"];
                                mydr3["SubTax"] = mydt3.Rows[0]["SubTax"];
                                mydr3["Discount"] = mydt3.Rows[0]["Discount"];
                                mydr3["Flag"] = mydt3.Rows[0]["Flag"];
                                mydr3["IsBatchNo"] = mydt3.Rows[0]["IsBatchNo"];
                                AllDt.Rows.Add(mydr3);
                            }
                        }
                    }
                }
                return AllDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 分店客户需要
        public static bool SubStorageCustAdd(SubSellCustInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("INSERT INTO [officedba].[SubSellCustInfo]  ");
            sql.AppendLine("           ([CompanyCD]                    ");
            sql.AppendLine("           ,[DeptID]                       ");
            sql.AppendLine("           ,[CustName]                     ");
            sql.AppendLine("           ,[CustTel]                      ");
            sql.AppendLine("           ,[CustMobile]                   ");
            sql.AppendLine("           ,[CustAddr]                     ");
            sql.AppendLine("           ,[Creator]                      ");
            sql.AppendLine("           ,[CreateDate])                  ");
            sql.AppendLine("     VALUES                                ");
            sql.AppendLine("           (@CompanyCD                     ");
            sql.AppendLine("           ,@DeptID                        ");
            sql.AppendLine("           ,@CustName                      ");
            sql.AppendLine("           ,@CustTel                       ");
            sql.AppendLine("           ,@CustMobile                    ");
            sql.AppendLine("           ,@CustAddr                      ");
            sql.AppendLine("           ,@Creator                       ");
            sql.AppendLine("           ,@CreateDate)                   ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustTel", model.CustTel));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustMobile", model.CustMobile));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustAddr", model.CustAddr));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
            comm.CommandText = sql.ToString();
            if (SqlHelper.ExecuteTransWithCommand(comm))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检索客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetAllCust(SubSellCustInfoModel model, string Method, int pageIndex, int pageSize, string OrderBy, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT [ID]                                    ");
            sql.AppendLine("      ,isnull(CustName,'') as CustName         ");
            sql.AppendLine("      ,isnull(CustTel,'') as CustTel           ");
            sql.AppendLine("      ,isnull(CustMobile,'') as CustMobile     ");
            sql.AppendLine("      ,isnull(CustAddr,'') as CustAddr         ");
            sql.AppendLine("  FROM [officedba].[SubSellCustInfo]           ");
            sql.AppendLine("  where CompanyCD=@CompanyCD                   ");
            sql.AppendLine("   and DeptID=@DeptID                           ");
            if (!string.IsNullOrEmpty(model.CustName))
            {
                sql.AppendLine("   and CustName like @CustName             ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustName", "%" + model.CustName + "%"));
            }
            if (!string.IsNullOrEmpty(model.CustTel))
            {
                sql.AppendLine("   and CustTel like @CustTel             ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustTel", "%" + model.CustTel + "%"));
            }
            if (!string.IsNullOrEmpty(model.CustMobile))
            {
                sql.AppendLine("   and CustMobile like @CustMobile             ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustMobile", "%" + model.CustMobile + "%"));
            }
            if (!string.IsNullOrEmpty(model.CustAddr))
            {
                sql.AppendLine("   and CustAddr like @CustAddr             ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustAddr", "%" + model.CustAddr + "%"));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            if (Method == "out")
            {
                sql.AppendLine(" order by " + OrderBy + "");
            }
            comm.CommandText = sql.ToString();
            DataTable dt = new DataTable();
            if (Method == "out")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            return dt;

        }
        /// <summary>
        /// 获取一个
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetOneData(int ID)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT ID                                        ");
            sql.AppendLine("      ,isnull(CustName,'') as CustName         ");
            sql.AppendLine("      ,isnull(CustTel,'') as CustTel           ");
            sql.AppendLine("      ,isnull(CustMobile,'') as CustMobile     ");
            sql.AppendLine("      ,isnull(CustAddr,'') as CustAddr         ");
            sql.AppendLine("  FROM [officedba].[SubSellCustInfo]           ");
            sql.AppendLine("  where ID=@ID                   ");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        public static bool UpdateCust(SubSellCustInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("UPDATE [officedba].[SubSellCustInfo]   ");
            sql.AppendLine("   SET [CustName] = @CustName          ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            if (!string.IsNullOrEmpty(model.CustTel))
            {
                sql.AppendLine("      ,[CustTel] = @CustTel            ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustTel", model.CustTel));

            }
            if (!string.IsNullOrEmpty(model.CustMobile))
            {
                sql.AppendLine("      ,[CustMobile] = @CustMobile      ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustMobile", model.CustMobile));
            }
            if (!string.IsNullOrEmpty(model.CustAddr))
            {
                sql.AppendLine("      ,[CustAddr] = @CustAddr          ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustAddr", model.CustAddr));
            }
            sql.AppendLine(" where ID=@ID                          ");

            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        public static bool DelCust(int ID)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" Delete from [officedba].[SubSellCustInfo] where ID=@ID");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion


    }
}
