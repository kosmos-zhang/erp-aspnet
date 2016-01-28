/**********************************************
 * 类作用：   组织机构图
 * 建立人：   吴志强
 * 建立时间： 2009/04/15
 ***********************************************/
using System;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using System.Data;
using System.Text;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：DeptQuarterEmployeeBus
    /// 描述：组织机构图
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class DeptQuarterEmployeeBus
    {  
        #region 常量定义
        const string SHOWTYEP_CODE_SELECT_DEPT = "1";//部门
        const string SHOWTYPE_CODE_USERS= "2";//人员

        const string OPRT_CODE_SELECT = "1";//单选
        const string OPRT_CODE_SELECTS = "2";//多选
        #endregion
      /// <summary>
        ///   获取机构信息
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
        public static DataTable GetDeptInfoWithCompanyCD( )
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
           string  companyCD = userInfo.CompanyCD;
            //执行查询

           return DeptQuarterEmployeeDBHelper.GetDeptInfoWithCompanyCD(companyCD);

        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        //public static DataTable GetDeptQuarterEmployeeInfo()
        //{
        //    //获取登陆用户信息
        //    UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //    //获取公司代码
        //   string  companyCD = userInfo.CompanyCD;
        //    //执行查询

        //   return DeptQuarterEmployeeDBHelper.GetEmployeeInfoWithCompanyCD(companyCD);

        //}



        public static string GetDeptQuarterEmployeeInfo()
        {
            //定义变量
            StringBuilder sbReturnInfo = new StringBuilder("<span style=\"font-size:15px; height:auto \">"+((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName +" 组织机构图：</span>  <br />   <br />");
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //获取机构信息
            DataTable dtDeptInfo = DeptQuarterEmployeeDBHelper.GetDeptInfoWithCompanyCD(companyCD);
            //获取人员信息
            DataTable dtEmployeeInfo = DeptQuarterEmployeeDBHelper.GetEmployeeInfoWithCompanyCD(companyCD);

            //信息存在
            if (dtDeptInfo != null && dtDeptInfo.Rows.Count > 0)
            {

                //获取第一级信息
                DataRow[] drSuper = dtDeptInfo.Select("SuperID IS NULL");
                //遍历第一级
                for (int i = 0; i < drSuper.Length; i++)
                {
                    DataRow drFirst = (DataRow)drSuper[i];
                    //获取机构ID
                    int id = GetSafeData.ValidateDataRow_Int(drFirst, "ID");
                    //获取机构名
                    string deptName = GetSafeData.ValidateDataRow_String(drFirst, "DisplayName");

                    //设置样式表单
                    string style = "T";
                    //生成子树代码
                    //sbReturnInfo.Append("<table border='0' cellpadding='0' cellspacing='0'><tr><td>");
                    //获取机构的人员
                    DataRow[] drDeptEmpl = dtEmployeeInfo.Select("DeptID = " + id);
                    if (drDeptEmpl != null && drDeptEmpl.Length > 0)
                    {
                        //sbReturnInfo.Append("<div class=\"I\">");
                        for (int j = 0; j < drDeptEmpl.Length; j++)
                        {
                            //获取人员岗位
                            string quarterName = GetSafeData.ValidateDataRow_String(drDeptEmpl[j], "QuarterName");
                            //获取人员姓名
                            string emplName = GetSafeData.ValidateDataRow_String(drDeptEmpl[j], "EmployeeName");
                            //判断是否是最后一个
                            if (i == drSuper.Length - 1 && j == drDeptEmpl.Length - 1)
                            {
                                style = "L";
                            }
                            sbReturnInfo.Append("<div class=\"" + style + "\">");
                            sbReturnInfo.Append(deptName + "：" + quarterName + "&nbsp;" + emplName);
                            sbReturnInfo.Append("</div>");
                        }
                        //sbReturnInfo.Append("</div>");
                    }
                    else
                    {
                        //判断是否是最后一个
                        if (i == drSuper.Length - 1)
                        {
                            style = "L";
                        }
                        sbReturnInfo.Append("<div class=\"" + style + "\">");
                        sbReturnInfo.Append(deptName);
                        sbReturnInfo.Append("</div>");
                    }

                    //设置子信息
                    sbReturnInfo.Append(InitDeptSubInfo(dtDeptInfo, id, dtEmployeeInfo));

                    //sbReturnInfo.Append("</td></tr></table>");
                }
            }
            //返回
            return sbReturnInfo.ToString();
        }

        #region 设置子机构信息
        /// <summary>
        /// 设置子机构信息
        /// </summary>
        /// <param name="dtDeptInfo">机构信息</param>
        /// <param name="id">机构ID</param>
        /// <param name="dtEmployeeInfo">人员信息</param>
        /// <returns></returns>
        private static string InitDeptSubInfo(DataTable dtDeptInfo, int id, DataTable dtEmployeeInfo)
        {
            StringBuilder sbSubInfo = new StringBuilder();
            //获取子信息
            DataRow[] drSub = dtDeptInfo.Select("SuperID = " + id);
            //
            if (drSub != null && drSub.Length > 0)
            {
                //sbSubInfo.Append("<div class=\"I\">");

                //生成子树代码
                //sbSubInfo.Append("<table border='0' cellpadding='0' cellspacing='0'><tr><td>");

                //遍历所有子信息
                for (int i = 0; i < drSub.Length; i++)
                {
                    //获取机构ID
                    int subId = GetSafeData.ValidateDataRow_Int(drSub[i], "ID");
                    //获取机构名
                    string subName = GetSafeData.ValidateDataRow_String(drSub[i], "DisplayName");

                    //设置样式表单
                    string style = "T";
                    //获取机构的人员
                    DataRow[] drDeptEmpl = dtEmployeeInfo.Select("DeptID = " + subId);
                    if (drDeptEmpl != null && drDeptEmpl.Length > 0)
                    {
                        sbSubInfo.Append("<div class=\"I\">");
                        for (int j = 0; j < drDeptEmpl.Length; j++)
                        {
                            //获取人员岗位
                            string quarterName = GetSafeData.ValidateDataRow_String(drDeptEmpl[j], "QuarterName");
                            //获取人员姓名
                            string emplName = GetSafeData.ValidateDataRow_String(drDeptEmpl[j], "EmployeeName");
                            //判断是否是最后一个
                            if (i == drSub.Length - 1 && j == drDeptEmpl.Length - 1)
                            {
                                style = "L";
                            }
                            sbSubInfo.Append("<div class=\"" + style + "\">");
                            sbSubInfo.Append(subName + "：" + quarterName + "&nbsp;" + emplName);
                            sbSubInfo.Append("</div>");
                        }
                        sbSubInfo.Append("</div>");
                    }
                    else
                    {
                        //判断是否是最后一个
                        if (i == drSub.Length - 1)
                        {
                            style = "L";
                        }
                        sbSubInfo.Append("<div class=\"" + style + "\">");
                        sbSubInfo.Append(subName);
                        sbSubInfo.Append("</div>");
                    }

                    //设置子信息
                    sbSubInfo.Append(InitDeptSubInfo(dtDeptInfo, subId, dtEmployeeInfo));
                }
                //sbSubInfo.Append("</td></tr></table>");
                //sbSubInfo.Append("</div>");
            }
            return sbSubInfo.ToString();
        }
        #endregion
        public static DataTable GetUserInfo(string ShowType, string OprtType)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //  string CompanyCD = "1001";
                DataTable dt = DeptQuarterEmployeeDBHelper.GetUserInfo(companyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //单选人员
                    if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECT)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            rows["EmployeesName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["EmployeesName"].ToString() + "'   >" + rows["EmployeesName"].ToString();
                        }
                    }//多选人员
                    else if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECTS)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            rows["EmployeesName"] = "<input type='checkbox' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["EmployeesName"].ToString() + "'   >" + rows["EmployeesName"].ToString();
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetUserQuterInfo(string ShowType, string OprtType)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //  string CompanyCD = "1001";
                DataTable dt = DeptQuarterEmployeeDBHelper.GetUserQuterInfo(companyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //单选人员
                    if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECT)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            rows["QuarterName"] = "<input type='radio' name='select'  id='chk_" + rows["QuarterID"] + "' value='" + rows["QuarterID"] + "|" + rows["QuarterName"].ToString() + "'   >" + rows["EmployeesName"].ToString();
                        }
                    }//多选人员
                    else if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECTS)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            rows["QuarterName"] = "<input type='checkbox' name='select'  id='chk_" + rows["QuarterID"] + "' value='" + rows["QuarterID"] + "|" + rows["QuarterName"].ToString() + "'   >" + rows["QuarterName"].ToString();
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
