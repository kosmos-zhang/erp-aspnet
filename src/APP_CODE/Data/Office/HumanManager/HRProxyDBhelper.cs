/**********************************************
 * 类作用：   新建人才代理
 * 建立人：   吴志强
 * 建立时间： 2009/03/25
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
    /// 类名：HRProxyDBHelper
    /// 描述：新建人才代理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/25
    /// 最后修改时间：2009/03/25
    /// </summary>
    ///
    public class HRProxyDBHelper
    {

        #region 通过ID查询人才代理信息
        /// <summary>
        /// 查询人才代理信息
        /// </summary>
        /// <param name="proxyID">人才代理ID</param>
        /// <returns></returns>
        public static DataTable GetProxyInfoWithID(string proxyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine("       CompanyCD          ");
            searchSql.AppendLine("       ,ProxyCompanyCD    ");
            searchSql.AppendLine("       ,ProxyCompanyName  ");
            searchSql.AppendLine("       ,Nature            ");
            searchSql.AppendLine("       ,Address           ");
            searchSql.AppendLine("       ,Corporate         ");
            searchSql.AppendLine("       ,Telephone         ");
            searchSql.AppendLine("       ,Fax               ");
            searchSql.AppendLine("       ,Email             ");
            searchSql.AppendLine("       ,Website           ");
            searchSql.AppendLine("       ,Important         ");
            searchSql.AppendLine("       ,Cooperation       ");
            searchSql.AppendLine("       ,Standard          ");
            searchSql.AppendLine("       ,ContactName       ");
            searchSql.AppendLine("       ,ContactTel        ");
            searchSql.AppendLine("       ,ContactMobile     ");
            searchSql.AppendLine("       ,ContactWeb        ");
            searchSql.AppendLine("       ,ContactPosition   ");
            searchSql.AppendLine("       ,ContactCardNo     ");
            searchSql.AppendLine("       ,ContactRemark     ");
            searchSql.AppendLine("       ,Remark            ");
            searchSql.AppendLine("       ,UsedStatus        ");
            searchSql.AppendLine("       ,ModifiedDate      ");
            searchSql.AppendLine("       ,ModifiedUserID    ");
            searchSql.AppendLine(" FROM officedba.HRProxy   ");
            searchSql.AppendLine(" WHERE ID = @ProxyID      ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@ProxyID", proxyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过检索条件查询人才代理信息
        /// <summary>
        /// 查询人才代理信息
        /// </summary>
        /// <param name="proxyID">人才代理ID</param>
        /// <returns></returns>
        public static DataTable SearchProxyInfo(HRProxyModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine("       ID                 ");
            searchSql.AppendLine("       ,isnull(ProxyCompanyCD,'')ProxyCompanyCD ");
            searchSql.AppendLine("       ,isnull(ProxyCompanyName,'')ProxyCompanyName  ");

            searchSql.AppendLine("       ,CASE Important  ");
            searchSql.AppendLine("   WHEN '1' THEN '不重要' ");
            searchSql.AppendLine("     WHEN '2' THEN '普通' ");
            searchSql.AppendLine("   WHEN '3' THEN '重要'   ");
            searchSql.AppendLine("   WHEN '4' THEN '关键'   ");
            searchSql.AppendLine("         ELSE ''          ");
            searchSql.AppendLine("       END AS Important ");

            searchSql.AppendLine("       ,CASE Cooperation  ");
            searchSql.AppendLine(" WHEN '1' THEN '付费服务' ");
            searchSql.AppendLine(" WHEN '2' THEN '一般服务' ");
            searchSql.AppendLine("         ELSE ''          ");
            searchSql.AppendLine("       END AS Cooperation ");

            searchSql.AppendLine(" ,ISNULL(ContactName,'') AS ContactName");
            searchSql.AppendLine(" ,ISNULL(ContactTel,'') AS ContactTel");
            searchSql.AppendLine(" ,ISNULL(ContactMobile,'') AS ContactMobile");
            searchSql.AppendLine(" ,ISNULL(ContactWeb,'') AS ContactWeb");
            searchSql.AppendLine("       ,CASE WHEN         ");
            searchSql.AppendLine("          UsedStatus = '1'");
            searchSql.AppendLine("          THEN '已启用'   ");
            searchSql.AppendLine("          ELSE '未启用'   ");
            searchSql.AppendLine("       END AS UsedStatus  ");
            searchSql.AppendLine(" FROM officedba.HRProxy   ");
            searchSql.AppendLine(" WHERE CompanyCD = @CompanyCD ");
            //searchSql.AppendLine(" AND UsedStatus='1'  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //企业编号
            if (!string.IsNullOrEmpty(model.ProxyCompanyCD))
            {
                searchSql.AppendLine("	AND ProxyCompanyCD LIKE '%' + @ProxyCompanyCD + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProxyCompanyCD", model.ProxyCompanyCD));
            }
            //企业名称
            if (!string.IsNullOrEmpty(model.ProxyCompanyName))
            {
                searchSql.AppendLine("	AND ProxyCompanyName LIKE '%' + @ProxyCompanyName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProxyCompanyName", model.ProxyCompanyName));
            }
            //重要程度
            if (!string.IsNullOrEmpty(model.Important))
            {
                searchSql.AppendLine("	AND Important = @Important ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Important", model.Important));
            }
            //合作关系
            if (!string.IsNullOrEmpty(model.Cooperation))
            {
                searchSql.AppendLine("	AND Cooperation = @Cooperation ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Cooperation", model.Cooperation));
            }
            //启用状态
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                searchSql.AppendLine("	AND UsedStatus = @UsedStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 新建人才代理信息
        /// <summary>
        /// 新建人才代理信息 
        /// </summary>
        /// <param name="model">人才代理信息</param>
        /// <returns></returns>
        public static bool InsertHRProxyInfo(HRProxyModel model)
        {
            #region SQL文拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.HRProxy ");
            insertSql.AppendLine("            (CompanyCD         ");
            insertSql.AppendLine("            ,ProxyCompanyCD  ");
            insertSql.AppendLine("            ,ProxyCompanyName  ");
            insertSql.AppendLine("            ,Nature            ");
            insertSql.AppendLine("            ,Address           ");
            insertSql.AppendLine("            ,Corporate         ");
            insertSql.AppendLine("            ,Telephone         ");
            insertSql.AppendLine("            ,Fax               ");
            insertSql.AppendLine("            ,Email             ");
            insertSql.AppendLine("            ,Website           ");
            insertSql.AppendLine("            ,Important         ");
            insertSql.AppendLine("            ,Cooperation       ");
            insertSql.AppendLine("            ,Standard          ");
            insertSql.AppendLine("            ,ContactName       ");
            insertSql.AppendLine("            ,ContactTel        ");
            insertSql.AppendLine("            ,ContactMobile     ");
            insertSql.AppendLine("            ,ContactWeb        ");
            insertSql.AppendLine("            ,ContactPosition   ");
            insertSql.AppendLine("            ,ContactCardNo     ");
            insertSql.AppendLine("            ,ContactRemark     ");
            insertSql.AppendLine("            ,Remark            ");
            insertSql.AppendLine("            ,UsedStatus        ");
            insertSql.AppendLine("            ,ModifiedDate      ");
            insertSql.AppendLine("            ,ModifiedUserID)   ");
            insertSql.AppendLine("      VALUES                   ");
            insertSql.AppendLine("            (@CompanyCD        ");
            insertSql.AppendLine("            ,@ProxyCompanyCD   ");
            insertSql.AppendLine("            ,@ProxyCompanyName ");
            insertSql.AppendLine("            ,@Nature           ");
            insertSql.AppendLine("            ,@Address          ");
            insertSql.AppendLine("            ,@Corporate        ");
            insertSql.AppendLine("            ,@Telephone        ");
            insertSql.AppendLine("            ,@Fax              ");
            insertSql.AppendLine("            ,@Email            ");
            insertSql.AppendLine("            ,@Website          ");
            insertSql.AppendLine("            ,@Important        ");
            insertSql.AppendLine("            ,@Cooperation      ");
            insertSql.AppendLine("            ,@Standard         ");
            insertSql.AppendLine("            ,@ContactName      ");
            insertSql.AppendLine("            ,@ContactTel       ");
            insertSql.AppendLine("            ,@ContactMobile    ");
            insertSql.AppendLine("            ,@ContactWeb       ");
            insertSql.AppendLine("            ,@ContactPosition  ");
            insertSql.AppendLine("            ,@ContactCardNo    ");
            insertSql.AppendLine("            ,@ContactRemark    ");
            insertSql.AppendLine("            ,@Remark           ");
            insertSql.AppendLine("            ,@UsedStatus       ");
            insertSql.AppendLine("            ,getdate()         ");
            insertSql.AppendLine("            ,@ModifiedUserID)  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstInsert = new ArrayList();
            //添加插入命令
            lstInsert.Add(comm);
            //执行插入并返回插入结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);
        }
        #endregion

        #region 更新人才代理信息
        /// <summary>
        /// 更新人才代理信息
        /// </summary>
        /// <param name="model">人才代理信息</param>
        /// <returns></returns>
        public static bool UpdateHRProxyInfo(HRProxyModel model)
        {
            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.HRProxy                   ");
            updateSql.AppendLine("    SET                                     ");
            updateSql.AppendLine("       ProxyCompanyName = @ProxyCompanyName ");
            updateSql.AppendLine("       ,Nature = @Nature                    ");
            updateSql.AppendLine("       ,Address = @Address                  ");
            updateSql.AppendLine("       ,Corporate = @Corporate              ");
            updateSql.AppendLine("       ,Telephone = @Telephone              ");
            updateSql.AppendLine("       ,Fax = @Fax                          ");
            updateSql.AppendLine("       ,Email = @Email                      ");
            updateSql.AppendLine("       ,Website = @Website                  ");
            updateSql.AppendLine("       ,Important = @Important              ");
            updateSql.AppendLine("       ,Cooperation = @Cooperation          ");
            updateSql.AppendLine("       ,Standard = @Standard                ");
            updateSql.AppendLine("       ,ContactName = @ContactName          ");
            updateSql.AppendLine("       ,ContactTel = @ContactTel            ");
            updateSql.AppendLine("       ,ContactMobile = @ContactMobile      ");
            updateSql.AppendLine("       ,ContactWeb = @ContactWeb            ");
            updateSql.AppendLine("       ,ContactPosition = @ContactPosition  ");
            updateSql.AppendLine("       ,ContactCardNo = @ContactCardNo      ");
            updateSql.AppendLine("       ,ContactRemark = @ContactRemark      ");
            updateSql.AppendLine("       ,Remark = @Remark                    ");
            updateSql.AppendLine("       ,UsedStatus = @UsedStatus            ");
            updateSql.AppendLine("       ,ModifiedDate = getdate()            ");
            updateSql.AppendLine("       ,ModifiedUserID = @ModifiedUserID    ");
            updateSql.AppendLine("  WHERE ProxyCompanyCD = @ProxyCompanyCD    ");
            updateSql.AppendLine("       AND CompanyCD = @CompanyCD           ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstInsert = new ArrayList();
            //添加插入命令
            lstInsert.Add(comm);
            //执行插入并返回插入结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, HRProxyModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProxyCompanyCD", model.ProxyCompanyCD));//代理公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProxyCompanyName", model.ProxyCompanyName));//代理公司名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Nature", model.Nature));//代理公司性质
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Address", model.Address));//地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Corporate", model.Corporate));//企业法人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Telephone", model.Telephone));//电话
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Fax", model.Fax));//传真
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Email", model.Email));//邮箱
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Website", model.Website));//网址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Important", model.Important));//重要程度(1不重要,2普通,3重要,4关键)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Cooperation", model.Cooperation));//合作关系(1 付费服务，2 一般服务)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Standard", model.Standard));//收费标准
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactName", model.ContactName));//联系人姓名
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactTel", model.ContactTel));//联系人固定电话
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactMobile", model.ContactMobile));//联系人移动电话
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactWeb", model.ContactWeb));//联系人网络通讯
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactPosition ", model.ContactPosition));//联系人公司职务
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactCardNo", model.ContactCardNo));//联系人工号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactRemark", model.ContactRemark));//联系人备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//附加信息
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用标识(0 停用，1 启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 删除人才代理信息
        /// <summary>
        /// 删除人才代理信息
        /// </summary>
        /// <param name="proxyNo">人才代理公司编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteProxyInfo(string proxyNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.HRProxy ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ProxyCompanyCD In( " + proxyNo + ")");
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
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion


        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string strSql = "SELECT ID, CompanyCD, ProxyCompanyCD, ProxyCompanyName, Nature, Address, Corporate, Telephone, Fax, Email, Website,                           ";
            strSql += "CASE Important WHEN '1' THEN '不重要' WHEN '2' THEN '普通' WHEN '3' THEN '重要' WHEN '4' THEN '关键' END AS Important,                        ";
            strSql += "CASE Cooperation WHEN '1' THEN '付费服务' WHEN '2' THEN '一般服务' END AS Cooperation, Standard, ContactName, ContactTel, ContactMobile,      ";
            strSql += "ContactWeb, ContactPosition, ContactCardNo, ContactRemark, Remark,                                                                            ";
            strSql += "CASE UsedStatus WHEN '0' THEN '停用' WHEN '1' THEN '启用' END AS UsedStatus, ModifiedDate, ModifiedUserID                                     ";
            strSql += "FROM officedba.HRProxy                                                                                                                        ";
            strSql += "WHERE (CompanyCD = @CompanyCD) AND (ProxyCompanyCD = @ProxyCompanyCD)                                                                                      ";

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ProxyCompanyCD",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

    }
}
