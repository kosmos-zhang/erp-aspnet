/**********************************************
 * 类作用：   UserInfo表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/01/10
 ***********************************************/

using System;

namespace XBase.Model.Office.SystemManager
{
    /// <summary>
    /// 类名：UserInfoModel
    /// 描述：UserInfo表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///
    public class UserInfoModel
    {
        //公司代码
        private string _CompanyCD;
        //用户ID
	    private string _UserID;
        //用户名
	    private string _UserName;
        //密码
	    private string _password;
        //锁定标志
	    private string _LockFlag;
        //开通日期
        private DateTime? _OpenDate=null;
        //结束日期
        private DateTime? _CloseDate=null;
        //记录修改日期
	    private DateTime _ModifiedDate;
        //记录修改人
	    private string _ModifiedUserID;
        //备注
        private string _Remark;
        //更新插入标注
        private bool _IsInsert = false;
        //员工编号
        private int? _EmployeeID;
        //启用状态
        private string _UsedStatus;
        //是否启用USBKEY
        private string _IsHardValidate;
        //是否启用USBKEY
        public string IsHardValidate
        {
            get { return _IsHardValidate; }
            set { _IsHardValidate = value; }
        }
       //启用状态
        public string UsedStatus
        {
            get
            {
                return _UsedStatus;
            }
            set
            {
                _UsedStatus = value;
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
        /// 用户ID
        /// </summary>
        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// 锁定标志
        /// </summary>
        public string LockFlag
        {
            get
            {
                return _LockFlag;
            }
            set
            {
                _LockFlag = value;
            }
        }

        /// <summary>
        /// 开通日期
        /// </summary>
        public DateTime? OpenDate
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
        /// 结束日期
        /// </summary>
        public DateTime? CloseDate
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
        /// 公司编码
        /// </summary>
        public int? EmployeeID
        {
            set { _EmployeeID = value; }
            get { return _EmployeeID; }
        }
        /// <summary>
        /// 记录修改日期
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
        /// 记录修改人
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
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }

        /// <summary>
        /// 更新插入标注
        /// </summary>
        public bool IsInsert
        {
            get
            {
                return _IsInsert;
            }
            set
            {
                _IsInsert = value;
            }
        }
    }
}
