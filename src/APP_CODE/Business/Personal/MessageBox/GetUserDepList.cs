using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Personal.MessageBox
{
    public class GetUserDepList
    {
        private readonly XBase.Data.Personal.MessageBox.GetUserDepList dal = new XBase.Data.Personal.MessageBox.GetUserDepList();

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public DataTable GetUserInfo(string companyCD)
        {
            return dal.GetUserInfo(companyCD);
        }


        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门信息</returns>
        public DataTable GetDeptInfo(string companyCD)
        {
            return dal.GetDeptInfo(companyCD);
        }

    }
}
