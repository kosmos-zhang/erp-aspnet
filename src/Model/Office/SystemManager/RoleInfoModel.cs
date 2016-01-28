/**********************************************
 * 类作用：   RoleInfo表数据模板
 * 建立人：   吴成好
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
    public class RoleInfoModel
    {
        //企业代码
        private string _CompanyCD;
        //角色名称
        private string _RoleName;
        //备注
        private string _Remark;
        private string _RoleID;
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return _CompanyCD;
            }
            set
            {
                _CompanyCD = value;
            }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                _RoleName = value;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
           public string RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }
    }
}
