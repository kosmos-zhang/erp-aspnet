/***************************************
 * 
 * 修改记录：
 *      添加CompanyCD字段varchar(50)
 *      2010-4-6 add by hexw
 * 
 * ***********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.DefManager
{
    public class ModuleTableModel
    {
        private int _id;//流水号
        private string _moduleContent;//模板内容
        private string _tableID;//表ID
        private string _moduleType;//模板类型：0添加模板
        private string _useStatus;//启用状态
        private string _companyCD;//公司编码
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            get { return _companyCD; }
            set { _companyCD = value; }
        }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string ModuleContent
        {
            get { return _moduleContent; }
            set { _moduleContent = value; }
        }
        /// <summary>
        /// 表ID
        /// </summary>
        public string TableID
        {
            get { return _tableID; }
            set { _tableID = value; }
        }
        /// <summary>
        /// 模板类型：0添加模板
        /// </summary>
        public string ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string UseStatus
        {
            get { return _useStatus; }
            set { _useStatus = value; }
        }
    }
}
