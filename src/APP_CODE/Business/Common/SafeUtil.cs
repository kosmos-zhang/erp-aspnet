/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.30
 * 描    述： 系统安全权限类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using XBase.Data;
using System.Security.Cryptography;
using XBase.Common;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：SafeUtil
    /// 描述：提供与系统访问权限相关的一些获取菜单和可允许的业务操作的方法
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class SafeUtil
    {
        /// <summary>
        /// 获得用户可操作菜单数据
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>返回用户可操作的菜单集。</returns>
        public static DataTable InitMenuData(string userID, string companyCD)
        {
            //获取系统默认设置的菜单项
            DataTable dtMenu = CommonUtilDBHelper.GetMenuData(userID, companyCD);
            //添加企业文化菜单
            
           // InitCompCulture(dtMenu, companyCD);

            return dtMenu;
        }

        public static DataTable InitMenuData(string userID, string companyCD, string filterID)
        {
            //获取系统默认设置的菜单项
            DataTable dtMenu = CommonUtilDBHelper.GetMenuData(userID, companyCD, filterID);
            //添加企业文化菜单
            
           // InitCompCulture(dtMenu, companyCD);

            return dtMenu;
        }

        public static ArrayList InitMenuDataSimple(string userID, string companyCD)
        {
            return CommonUtilDBHelper.InitMenuDataSimple(userID, companyCD);
        }

        #region 企业文化菜单设置

        /// <summary>
        /// 企业文化菜单设置
        /// </summary>
        /// <param name="dtMenu">菜单信息</param>
        /// <param name="companyCD">公司代码</param>
        private static void InitCompCulture(DataTable dtMenu, string companyCD)
        {
            //获取客户自己设置的企业文化菜单项 
            DataTable dtCulture = CommonUtilDBHelper.GetCultureInfo(companyCD);
            //添加一列用于判断是否包含子分类
            dtCulture.Columns.Add(new DataColumn("HasSubMenu"));
            DataTable dtOrderCulture = new DataTable();
            dtOrderCulture = dtCulture.Clone();
            //客户自定义企业文化菜单存在的时候
            if (dtCulture != null && dtCulture.Rows.Count > 0)
            {
                //获取第一级分类信息
                DataRow[] drSuperType = dtCulture.Select("SupperTypeID =0");
                //遍历第一级部门
                for (int i = 0; i < drSuperType.Length; i++)
                {
                    DataRow drFirstType = (DataRow)drSuperType[i];
                    //获取部门ID
                    string typeID = drFirstType["ID"].ToString().Trim();
                    //重新设置子分类ID，以便实现和菜单生成的接口一致
                    drFirstType["ID"] = ConstUtil.MENU_PERSONAL_COMPANY_CULTURE + typeID;
                    //重新设置父分类ID，以便实现和菜单生成的接口一致
                    drFirstType["SupperTypeID"] = ConstUtil.MENU_PERSONAL_COMPANY_CULTURE;
                    //设定子分类
                    bool hasSubMenu = ReorderRow(dtOrderCulture, typeID, dtCulture, drFirstType["ID"].ToString().Trim());
                    //有子分类时
                    if (hasSubMenu)
                    {
                        drFirstType["HasSubMenu"] = "1";
                    }
                    else
                    {
                        drFirstType["HasSubMenu"] = "0";
                    }
                    //导入第一级分类
                    dtOrderCulture.ImportRow(drFirstType);
                }
            }
            else
            {
                return;
            }

            #region 将企业文化分类插入菜单项
            
            //客户自定义企业文化菜单存在的时候
            if (dtOrderCulture != null && dtOrderCulture.Rows.Count > 0)
            {
                //
                dtMenu.DefaultView.Sort = "ModuleID";
                //
                int compCulture = dtMenu.DefaultView.Find(ConstUtil.MENU_PERSONAL_COMPANY_CULTURE);

                //遍历所有的自定义企业文化
                for (int i = 0; i < dtOrderCulture.Rows.Count; i++)
                {
                    //新建一个菜单项
                    DataRow drCulture = dtMenu.NewRow();
                    //模块ID
                    drCulture["ModuleID"] = dtCulture.Rows[i]["ID"].ToString();
                    //分类名称设置为菜单名
                    drCulture["ModuleName"] = dtCulture.Rows[i]["TypeName"].ToString();
                    //模块类型 A-表示应用系统 S-表示子系统（六大模块） M-表示菜单 P-页面
                    //如果还有子分类时，则不表示菜单
                    if ("1".Equals(dtCulture.Rows[i]["HasSubMenu"].ToString()))
                    {
                        drCulture["ModuleType"] = "S";
                    }
                    else
                    {
                        drCulture["ModuleType"] = "M";
                    }
                    //上级模块ID
                    drCulture["ParentID"] = dtCulture.Rows[i]["SupperTypeID"].ToString();
                    //属性类型，主要用于对象更灵活的控制，目前值有：link和空值
                    drCulture["PropertyType"] = "link";
                    //属性值，当属性类型为“link”时，则属性值表示link 内容
                    drCulture["PropertyValue"] = "Pages/Office/AdminManager/Equipment_Add.aspx";
                    //添加企业文化到菜单数据集中
                    dtMenu.Rows.InsertAt(drCulture, compCulture + i + 1);
                }
            }

            #endregion

        }

        #endregion

        #region 数据重新排序处理

        /// <summary>
        /// 数据重新排序处理
        /// 
        /// </summary>
        /// <param name="dtReturn">返回的数据集</param>
        /// <param name="typeID">分类ID</param>
        /// <param name="dtCulture">分类信息</param>
        /// <param name="superMenu">父级菜单</param>
        /// <returns></returns>
        private static bool ReorderRow(DataTable dtReturn, string typeID, DataTable dtCulture, string superMenu)
        {
            //获取分类的子分类
            DataRow[] drSubType = dtCulture.Select("SupperTypeID = '" + typeID + "'");

            if (drSubType.Length < 1){
                return false;
            }
            //遍历所有子分类
            for (int i = 0; i < drSubType.Length; i++)
            {
                //获取子分类数据
                DataRow drSubTypeTemp = (DataRow)drSubType[i];
                //获取子分类ID
                string subTypeID = drSubTypeTemp["ID"].ToString().Trim();
                //重新设置子分类ID，以便实现和菜单生成的接口一致
                drSubTypeTemp["ID"] = superMenu + subTypeID;
                //重新设置父分类ID，以便实现和菜单生成的接口一致
                drSubTypeTemp["SupperTypeID"] = superMenu;
                //生成子分类的子分类信息
                bool hasSubMenu = ReorderRow(dtReturn, subTypeID, dtCulture, drSubTypeTemp["ID"].ToString());
                //有子分类时
                if (hasSubMenu)
                {
                    drSubTypeTemp["HasSubMenu"] = "1";
                }
                else
                {
                    drSubTypeTemp["HasSubMenu"] = "0";
                }
                //导入子分类
                dtReturn.ImportRow(drSubTypeTemp);
            }
            return true;
        }

        #endregion

        #region 获得页面上的操作按钮

        /// <summary>
        /// 获得页面上的操作按钮
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回用户可操作的页面业务集。</returns>
        public static DataTable InitPageAuthority(string UserID, string CompanyCD)
        {
            return CommonUtilDBHelper.GetPageAuthority(UserID, CompanyCD);
        }

        #endregion

        #region 用户页面上可操作的业务

        /// <summary>
        /// 获得用户页面上可操作的业务
        /// </summary>
        /// <param name="ModuleID">页面ID</param>
        /// <param name="UserInfo">用户信息</param>
        /// <returns>返回一个用户可操作的业务 Hashtable 集。</returns>
        public static string[] GetPageAuthority(string ModuleID, UserInfoUtil UserInfo)
        {
          //  return GetPageAuthorityFromDB(ModuleID, UserInfo);

            //获得用户权限
            DataTable data = UserInfo.AuthorityInfo;

            if (data!=null &&  data.Rows.Count > 0)
            {
                //获得该页面的可操作业务
                DataRow[] rows = data.Select("ModuleID = '" + ModuleID + "'");
                int iCount = rows.Length;
                if (iCount > 0)
                {
                    //实例化

                    string[] authority = new String[iCount];
                    //将权限添加到返回集中
                    for (int i = 0; i < rows.Length; i++)
                    {
                        string FunctionCD = (string)rows[i]["FunctionCD"];
                        authority[i] = FunctionCD;
                    }
                    return authority;
                }
            }
            //返回可操作的业务集
            return null;

        }

        public static string[] GetPageAuthorityFromDB(string ModuleID, UserInfoUtil UserInfo)
        {
            //获得用户权限
            DataTable data = CommonUtilDBHelper.GetPageAuthority(UserInfo.UserID, UserInfo.CompanyCD, ModuleID);

            if (data.Rows.Count > 0)
            {
                //获得该页面的可操作业务
                DataRow[] rows = data.Select("ModuleID = '" + ModuleID + "'");
                int iCount = rows.Length;
                if (iCount > 0)
                {
                    //实例化

                    string[] authority = new String[iCount];
                    //将权限添加到返回集中
                    for (int i = 0; i < rows.Length; i++)
                    {
                        string FunctionCD = (string)rows[i]["FunctionCD"];
                        authority[i] = FunctionCD;
                    }
                    return authority;
                }
            }
            //返回可操作的业务集
            return null;
        }

        #endregion

        #region 企业分配模块
        public static DataTable GetCompanyModule(string CompanyCD)
        {
            return CommonUtilDBHelper.GetCompanyModule(CompanyCD);
        }
        #endregion

    }
}
