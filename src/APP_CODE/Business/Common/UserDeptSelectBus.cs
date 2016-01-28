/**********************************************
 * 作    者： 江贻明
 * 创建日期： 2009.06.17
 * 描    述： 员工以及部门选择页面，查询部门
 *            以及员工信息
 * 版    本： 0.1.0
 ***********************************************/
using System.Data;
using System;
using XBase.Data.Common;
using XBase.Common;
namespace XBase.Business.Common
{
    public class UserDeptSelectBus
    {
        #region 常量定义
        const string SHOWTYEP_CODE_SELECT_DEPT = "1";//部门
        const string SHOWTYPE_CODE_USERS= "2";//人员

        const string OPRT_CODE_SELECT = "1";//单选
        const string OPRT_CODE_SELECTS = "2";//多选
        #endregion

        #region 获取分公司信息
        public static DataTable GetSubCompanyinfo(string ShowType)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                DataTable dt= UserDeptSelectDBHelper.GetSubCompanyinfo(companyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (ShowType== OPRT_CODE_SELECT)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            rows["DeptName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["DeptName"].ToString() + "'   >" + rows["DeptName"].ToString();
                        }
                    }
                    else if (ShowType== OPRT_CODE_SELECTS)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            rows["DeptName"] = "<input type='checkbox' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["DeptName"].ToString() + "'   >" + rows["DeptName"].ToString();
                        }
                    }
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取人员信息
        public static DataTable GetUserInfo(string ShowType, string OprtType)  
        {
            try
            {
                  string  companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                  DataTable dt = UserDeptSelectDBHelper.GetUserInfo(companyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //单选人员
                    if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECT)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            if (rows["Flag"].ToString().Trim() == "3")
                            {
                                rows["EmployeesName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["EmployeesName"].ToString() + "'   >" + rows["EmployeesName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color=red>已离职</font>";
                            }
                            else
                            {
                                rows["EmployeesName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["EmployeesName"].ToString() + "'   >" + rows["EmployeesName"].ToString();
                            }
                        }
                    }
                    else if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECTS)
                    {//多选人员
                        foreach (DataRow rows in dt.Rows)
                        {
                            if (rows["Flag"].ToString().Trim() == "3")
                            {
                                rows["EmployeesName"] =  rows["EmployeesName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color=red>已离职</font>";
                            }
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
        #endregion


        #region 获取人员信息--add by Moshenlin 2010-06-07
        public static DataTable GetUserInfo(string ShowType, string OprtType,string IsShow)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                DataTable dt = new DataTable();
                if (IsShow == "1")
                {
                    dt = UserDeptSelectDBHelper.GetUserInfo(companyCD);
                }
                else
                {
                    dt = UserDeptSelectDBHelper.GetUserInfo(companyCD,"");
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    //单选人员
                    if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECT)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            if (rows["Flag"].ToString().Trim() == "3")
                            {
                                rows["EmployeesName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["EmployeesName"].ToString() + "'   >" + rows["EmployeesName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color=red>已离职</font>";
                            }
                            else
                            {
                                rows["EmployeesName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["EmployeesName"].ToString() + "'   >" + rows["EmployeesName"].ToString();
                            }
                        }
                    }
                    else if (ShowType == SHOWTYPE_CODE_USERS && OprtType == OPRT_CODE_SELECTS)
                    {//多选人员
                        foreach (DataRow rows in dt.Rows)
                        {
                            if (rows["Flag"].ToString().Trim() == "3")
                            {
                                rows["EmployeesName"] = rows["EmployeesName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color=red>已离职</font>";
                            }
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
        #endregion

        #region 获取部门信息
        public static DataTable GetDeptInfoByCompanyCD(string ShowType,string OprtType)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               // string CompanyCD = "1001";
                DataTable dt = UserDeptSelectDBHelper.GetDeptInfo(companyCD);
                if (Convert.ToInt32(ShowType) > 1)
                {
                    ShowType = string.Empty;
                    OprtType = string.Empty;
                }
                if (!string.IsNullOrEmpty(ShowType) &&
                    !string.IsNullOrEmpty(OprtType))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //单选部门 
                        if (ShowType == SHOWTYEP_CODE_SELECT_DEPT && OprtType == OPRT_CODE_SELECT)
                        {
                            foreach (DataRow rows in dt.Rows)
                            {
                                rows["DeptName"] = "<input type='radio' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["DeptName"].ToString() + "'   >" + rows["DeptName"].ToString();
                            }

                        }//多选部门
                        //else if (ShowType == SHOWTYEP_CODE_SELECT_DEPT && OprtType == OPRT_CODE_SELECTS)
                        //{
                        //    foreach (DataRow rows in dt.Rows)
                        //    {
                        //        rows["DeptName"] = "<input type='checkbox' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["DeptName"].ToString() + "'   >" + rows["DeptName"].ToString();
                        //    }
                        //}
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDeptInfo(string TypeID)
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                companyCD = "AAAAAA";
            }
            //查询部门信息
            DataTable dtDept = UserDeptSelectDBHelper.GetDeptInfo(companyCD);
            //部门信息不存在时，返回
            if (dtDept == null || dtDept.Rows.Count < 1) return dtDept;

            //定义返回的部门信息变量
            DataTable dtReturn = new DataTable();
            //复制部门信息表结构
            dtReturn = dtDept.Clone();
            #region 部门信息排序处理
            //获取第一级部门信息
            DataRow[] drSuperDept = dtDept.Select("SuperDeptID IS NULL");
            //遍历第一级部门
            for (int i = 0; i < drSuperDept.Length; i++)
            {
                DataRow drFirstDept = (DataRow)drSuperDept[i];
                //获取部门ID
                int deptID = (int)drFirstDept["ID"];
                //替换部门名称内容
                if (TypeID == ConstUtil.TYPE_DANX_CODE)
                {
                    drFirstDept["DeptName"] = "<input type='radio' name='select'  id='chk_" + deptID.ToString() + "' value='" + ConstUtil.DEPT_EMPLOY_SELECT_DEPT
                  + deptID.ToString() + "|" + drFirstDept["DeptName"].ToString() + "'>" + drFirstDept["DeptName"].ToString();
                }
                else if (TypeID == ConstUtil.TYPE_DUOX_CODE)
                {
                    drFirstDept["DeptName"] = "<input type='checkbox' name='select'  id='chk_" + deptID.ToString() + "' value='" + ConstUtil.DEPT_EMPLOY_SELECT_DEPT
                                  + deptID.ToString() + "|" + drFirstDept["DeptName"].ToString() + "'>" + drFirstDept["DeptName"].ToString();
                }
                //导入第一级部门
                dtReturn.ImportRow(drFirstDept);
                //设定子部门
                dtReturn = ReorderDeptRow(dtReturn, deptID, dtDept, 1, TypeID);
            }
            #endregion
            return dtReturn;
        }
        #endregion

        #region 部门信息排序处理
        /// <summary>
        /// 部门信息排序处理
        /// 获取部门的子部门信息，支持无限级的子部门
        /// </summary>
        /// <param name="dtReturn">返回的数据集</param>
        /// <param name="deptID">部门ID</param>
        /// <param name="dtDept">部门信息</param>
        /// <param name="align">对齐位置</param>
        /// <returns></returns>
        private static DataTable ReorderDeptRow(DataTable dtReturn, int deptID, 
            DataTable dtDept, int align,string TypeID)
        {
            //获取部门的子部门
            DataRow[] drSubDept = dtDept.Select("SuperDeptID = " + deptID);
            //遍历所有子部门
            for (int i = 0; i < drSubDept.Length; i++)
            {
                //通过对齐位置，来控制该部门前空格数
                string alignPosition = string.Empty;
                for (int j = 0; j < align; j++)
                {
                    alignPosition += "&nbsp;&nbsp;";
                }
                //获取子部门数据
                DataRow drSubDeptTemp = (DataRow)drSubDept[i];
                //获取子部门ID
                int subDeptID = (int)drSubDeptTemp["ID"];
                if (TypeID == ConstUtil.TYPE_DANX_CODE)
                {
                    drSubDeptTemp["DeptName"] = alignPosition + "<input type='radio' name='select'  id='chk_" + subDeptID.ToString() + "' value='" + ConstUtil.DEPT_EMPLOY_SELECT_DEPT
                                   + subDeptID.ToString() + "|" + drSubDeptTemp["DeptName"].ToString() + "'>" + drSubDeptTemp["DeptName"].ToString();
                }
                else if (TypeID == ConstUtil.TYPE_DUOX_CODE)
                {
                    drSubDeptTemp["DeptName"] = alignPosition + "<input type='checkbox'   id='chk_" + subDeptID.ToString() + "' value='" + ConstUtil.DEPT_EMPLOY_SELECT_DEPT
                                                    + subDeptID.ToString() + "|" + drSubDeptTemp["DeptName"].ToString() + "'>" + drSubDeptTemp["DeptName"].ToString();
                }
            
                //导入子部门
                dtReturn.ImportRow(drSubDeptTemp);
                //生成子部门的子部门信息
                dtReturn = ReorderDeptRow(dtReturn, subDeptID, dtDept, align + 1,TypeID);
            }
            return dtReturn;
        }
        #endregion

        #region 获取员工信息
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfo()
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                //companyCD = "AAAAAA";
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                companyCD = "AAAAAA";
            }
            //查询员工信息
            return UserDeptSelectDBHelper.GetUserInfo(companyCD);
        }


        #endregion

        #region 获取部门信息(单选)
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDepartmentInfo()
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                companyCD = "AAAAAA";
            }
            //string companyCD = "AAAAAA";
            //查询部门信息
            DataTable dtDept = UserDeptSelectDBHelper.GetDeptInfo(companyCD);
            //部门信息不存在时，返回
            if (dtDept == null || dtDept.Rows.Count < 1) return dtDept;

            //定义返回的部门信息变量
            DataTable dtReturn = new DataTable();
            //复制部门信息表结构
            dtReturn = dtDept.Clone();

            #region 部门信息排序处理

            //获取第一级部门信息
            DataRow[] drSuperDept = dtDept.Select("SuperDeptID IS NULL");
            //遍历第一级部门
            for (int i = 0; i < drSuperDept.Length; i++)
            {
                DataRow drFirstDept = (DataRow)drSuperDept[i];
                //获取部门ID
                int deptID = (int)drFirstDept["ID"];
                //替换部门名称内容
                drFirstDept["DeptName"] = "<input type='radio' name=\"radipDept\"  id='chk_" + deptID.ToString() + "' value='" + drFirstDept["ID"].ToString() + "' onclick=\"popDeptObj.FillDeptValue(this,'" + drFirstDept["DeptName"].ToString() + "');\">" + drFirstDept["DeptName"].ToString();
                //导入第一级部门
                dtReturn.ImportRow(drFirstDept);
                //设定子部门
                dtReturn = ReorderDepartmentRow(dtReturn, deptID, dtDept, 1);
            }

            #endregion

            return dtReturn;
        }
        #endregion

        #region 部门信息排序处理
        /// <summary>
        /// 部门信息排序处理
        /// 获取部门的子部门信息，支持无限级的子部门
        /// </summary>
        /// <param name="dtReturn">返回的数据集</param>
        /// <param name="deptID">部门ID</param>
        /// <param name="dtDept">部门信息</param>
        /// <param name="align">对齐位置</param>
        /// <returns></returns>
        private static DataTable ReorderDepartmentRow(DataTable dtReturn, int deptID, DataTable dtDept, int align)
        {
            //获取部门的子部门
            DataRow[] drSubDept = dtDept.Select("SuperDeptID = " + deptID);

            //遍历所有子部门
            for (int i = 0; i < drSubDept.Length; i++)
            {
                //通过对齐位置，来控制该部门前空格数
                string alignPosition = string.Empty;
                for (int j = 0; j < align; j++)
                {
                    alignPosition += "&nbsp;&nbsp;";
                }
                //获取子部门数据
                DataRow drSubDeptTemp = (DataRow)drSubDept[i];
                //获取子部门ID
                int subDeptID = (int)drSubDeptTemp["ID"];
                drSubDeptTemp["DeptName"] = alignPosition + "<input type='radio' name=\"radipDept\" id='chk_" + subDeptID.ToString() + "' value='" + drSubDeptTemp["ID"].ToString() + "' onclick=\"popDeptObj.FillDeptValue(this,'" + drSubDeptTemp["DeptName"].ToString() + "');\">" + drSubDeptTemp["DeptName"].ToString();
                //导入子部门
                dtReturn.ImportRow(drSubDeptTemp);
                //生成子部门的子部门信息
                dtReturn = ReorderDepartmentRow(dtReturn, subDeptID, dtDept, align + 1);
            }
            return dtReturn;
        }
        #endregion
    }
}
