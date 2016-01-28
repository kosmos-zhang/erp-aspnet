using System;
using XBase.Model.Office.SupplyChain;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace XBase.Data.Office.SupplyChain
{
    public class PorductExtListDBHelper
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(TableExtFields tableExtFields, out string strMsg)
        {
            int EFIndex = GetEFIndex(tableExtFields);//索引
            bool isSucc = false;//是否成功
            if (EFIndex != 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.TableExtFields(");
                strSql.Append("TabName,CompanyCD,EFIndex,EFDesc,EFType,EFValueList)");
                strSql.Append(" values (");
                strSql.Append("'officedba.ProductInfo',@CompanyCD,@EFIndex,@EFDesc,@EFType,@EFValueList)");
                tableExtFields.EFIndex = EFIndex;
                SqlParameter[] parameters = {			
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@EFIndex", SqlDbType.Int,4),
					new SqlParameter("@EFDesc", SqlDbType.VarChar,20),
					new SqlParameter("@EFType", SqlDbType.Char,1),
					new SqlParameter("@EFValueList", SqlDbType.VarChar,256)};

                parameters[0].Value = tableExtFields.CompanyCD;
                parameters[1].Value = tableExtFields.EFIndex;
                parameters[2].Value = tableExtFields.EFDesc;
                parameters[3].Value = tableExtFields.EFType;
                parameters[4].Value = tableExtFields.EFValueList;
                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }

                try
                {
                    SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
                    isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    strMsg = "保存失败，请联系系统管理员！";
                    isSucc = false;
                    throw ex;
                }
            }
            else
            {
                strMsg = "已达最大数量，您不能添加新的商品特性！";
            }
            return isSucc;
        }

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <returns></returns>
        private static int GetEFIndex(TableExtFields tableExtFields)
        {
            int EFIndex = 0;//索引
            int iCount = 0;//使用字段合计
            string strSql = string.Empty;
            strSql = "select count(1) as EFIndex from officedba.TableExtFields where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo'";
            SqlParameter[] parameters = { 
                                            new SqlParameter("@CompanyCD",tableExtFields.CompanyCD)
                                        };
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, parameters));
            //总使用扩展字段小于三十，获取索引，等于三十时不可添加新扩展字段
            if (iCount < 30)
            {
                strSql = "select isnull(max(EFIndex),0) as EFIndex from officedba.TableExtFields where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo'";
                SqlParameter[] parameters1 = { 
                                            new SqlParameter("@CompanyCD",tableExtFields.CompanyCD)
                                        };
                EFIndex = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, parameters1));
                //最大索引未到三十就使用最大索引+1作为当前索引
                if (EFIndex < 30)
                {
                    EFIndex += 1;
                }
                //当最大索引为三十时，查找小于三十的未被使用的索引
                if (EFIndex == 30)
                {
                    for (int i = 1; i < 30; i++)
                    {
                        strSql = "select count(1) as EFIndex from officedba.TableExtFields where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo' and EFIndex=@EFIndex";
                        SqlParameter[] paras = { 
                                            new SqlParameter("@CompanyCD",tableExtFields.CompanyCD),
                                            new SqlParameter("@EFIndex",i)
                                        };
                        iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
                        if (iCount == 0)
                        {
                            EFIndex = i;
                            break;
                        }
                    }
                }
            }
            return EFIndex;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable Getlist(string CompanyCD, string EFDesc, string EFType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,EFIndex,EFDesc,EFType,EFValueList ,case EFType when '1' then '编辑框' when '2' then '选择框' end as EFTypeText from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo' ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            if (EFDesc != "")
            {
                sb.Append(" and  EFDesc like @EFDesc ");
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            if (EFType != "")
            {
                sb.Append(" and  EFType = @EFType ");
                arr.Add(new SqlParameter("@EFType", EFType));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(sb.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 初始化商品档案页面获取所有字段
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllList(string CompanyCD)
        {
            // DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,EFIndex,EFDesc,EFType,EFValueList from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo'  ORDER BY EFType, ID asc ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(TableExtFields tableExtFields, out string strMsg)
        {
            bool isSucc = false;//是否成功
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.TableExtFields set ");
            strSql.Append("EFIndex=@EFIndex,");
            strSql.Append("EFDesc=@EFDesc,");
            strSql.Append("EFType=@EFType,");
            strSql.Append("EFValueList=@EFValueList");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                      new SqlParameter("@ID", SqlDbType.Int,4),
                      new SqlParameter("@EFIndex", SqlDbType.Int,4),
                      new SqlParameter("@EFDesc", SqlDbType.VarChar,20),
                      new SqlParameter("@EFType", SqlDbType.Char,1),
                      new SqlParameter("@EFValueList", SqlDbType.VarChar,256)};
            parameters[0].Value = tableExtFields.ID;
            parameters[1].Value = tableExtFields.EFIndex;
            parameters[2].Value = tableExtFields.EFDesc;
            parameters[3].Value = tableExtFields.EFType;
            parameters[4].Value = tableExtFields.EFValueList;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }

            try
            {
                SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
                isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
                strMsg = "保存成功！";
            }
            catch (Exception ex)
            {
                strMsg = "保存失败，请联系系统管理员！";
                isSucc = false;
                throw ex;
            }

            return isSucc;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strCompanyCD"></param>
        /// <param name="IDS">id列表</param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool Delete(string strCompanyCD, string IDS, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除

            string[] orderNoS = null;
            orderNoS = IDS.Split(',');
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < orderNoS.Length; i++)
            {
                if (!isDel(orderNoS[i], strCompanyCD))
                {
                    strFieldText += "商品特性：" + SqlHelper.ExecuteScalar("select EFDesc from officedba.TableExtFields where ID=" + orderNoS[i], null).ToString() + "|";
                    strMsg += "已被使用不允许删除！|";
                    bTemp = true;
                }
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                try
                {
                    SqlHelper.ExecuteScalar("DELETE from officedba.TableExtFields  where id in(" + allOrderNo + ")", null);
                    isSucc = true;

                    strMsg = "删除成功！";
                }
                catch (Exception ex)
                {

                    strMsg = "删除失败，请联系系统管理员！";
                    isSucc = false;
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
            }
            return isSucc;
        }

        /// <summary>
        /// 属性是否可以删除
        /// </summary>
        /// <returns></returns>
        private static bool isDel(string id, string strCompanyCD)
        {
            bool isSuc = false;
            string strSql = string.Empty;
            string strKeyName = string.Empty;//要删除的字段名
            int index = 0;//要删除属性的索引值
            index = Convert.ToInt32(SqlHelper.ExecuteScalar("select isnull(EFIndex,0) as EFIndex from officedba.TableExtFields where ID=" + id, null));
            strKeyName = "ExtField" + index.ToString();
            strSql = "SELECT COUNT(1) FROM officedba.ProductInfo WHERE (" + strKeyName + " IS NOT NULL) AND (CompanyCD = '" + strCompanyCD + "') AND (" + strKeyName + " <> '') ";
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, null)) == 0)
            {
                isSuc = true;
            }
            return isSuc;
        }

        #endregion  成员方法
    }
}
