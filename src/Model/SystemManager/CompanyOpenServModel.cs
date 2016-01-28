/**********************************************
 * 类作用：   CompanyOpenServ表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/01/21
 ***********************************************/

using System;

namespace XBase.Model.SystemManager
{
    /// <summary>
    /// 类名：CompanyOpenServModel
    /// 描述：CompanyOpenServ表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/21
    /// 最后修改时间：2009/01/21
    /// </summary>
    ///
    public class CompanyOpenServModel
    {
        //公司代码
        private string _CompanyCD;
        //最大角色数
        private string _MaxRoles;
        //最大用户数
        private string _MaxUers;
        //文件总大小
        private string _MaxDocSize;
        //单个文件最大大小
        private string _SingleDocSize;
        //最大文件个数
        private string _MaxDocNum;
        //开始日期
        private string _OpenDate;
        //结束日期
        private string _CloseDate;
        //最后修改日期
        private DateTime _ModifiedDate;
        //最后修改人
        private string _ModifiedUserID;
        //备注
        private string _remark;

        private string _LogoImg;


        //插入标志
        private bool _isInsert = false;


        /// <summary>
        /// 公司LOGO
        /// </summary>
        public string LogoImg
        {
            get
            {
                return _LogoImg;
            }
            set
            {
                _LogoImg = value;
            }
        } 


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
        /// 最大角色数
        /// </summary>
        public string MaxRoles
        {
            get
            {
                return _MaxRoles;
            }
            set
            {
                _MaxRoles = value;
            }
        }

        /// <summary>
        /// 最大用户数
        /// </summary>
        public string MaxUers
        {
            get
            {
                return _MaxUers;
            }
            set
            {
                _MaxUers = value;
            }
        }

        /// <summary>
        /// 最大文档大小
        /// </summary>
        public string MaxDocSize
        {
            get
            {
                return _MaxDocSize;
            }
            set
            {
                _MaxDocSize = value;
            }
        }

        /// <summary>
        /// 单个文档最大大小
        /// </summary>
        public string SingleDocSize
        {
            get
            {
                return _SingleDocSize;
            }
            set
            {
                _SingleDocSize = value;
            }
        }

        /// <summary>
        /// 文档最大数
        /// </summary>
        public string MaxDocNum
        {
            get
            {
                return _MaxDocNum;
            }
            set
            {
                _MaxDocNum = value;
            }
        }

        /// <summary>
        /// 开通时间
        /// </summary>
        public string OpenDate
        {
            get
            {
                return _OpenDate;
            }
            set
            {
                _OpenDate = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string CloseDate
        {
            get
            {
                return _CloseDate;
            }
            set
            { 
                _CloseDate = value; 
            } 
        }
        
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifiedDate
        {
            get
            { 
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value; 
            } 
        }
        
        /// <summary>
        /// 修改用户ID
        /// </summary>
        public string ModifiedUserID 
        { 
            get
            { 
                return _ModifiedUserID; 
            }
            set
            {
                _ModifiedUserID = value; 
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark 
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value; 
            }
        }

        /// <summary>
        /// 插入标志位
        /// </summary>
        public bool IsInsert 
        {
            get
            {
                return _isInsert;
            }
            set
            {
                _isInsert = value; 
            } 
        }

    }
}
