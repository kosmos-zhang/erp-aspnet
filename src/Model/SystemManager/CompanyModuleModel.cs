/**********************************************
 * 类作用：   CompanyModule表数据模板
 * 建立人：   吴成好
 * 建立时间： 2009/01/22
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.SystemManager
{
    public class CompanyModuleModel
    {
        //企业代码
        private string _CompanyCD;
        //模块ID
        private string _ModuleID;

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
        /// 模块ID
        /// </summary>
        public string ModuleID
        {
            get
            {
                return _ModuleID;
            }
            set
            {
                _ModuleID = value;
            }
        }

    }

}
