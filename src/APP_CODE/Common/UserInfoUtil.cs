/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.29
 * 描    述： 登陆用户信息类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Common
{
    /// <summary>
    /// 类名：UserInfoUtil
    /// 描述：提供与用户相关的一些属性
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/29
    /// 最后修改时间：2009/04/14
    /// 修改人：游德春 
    /// 修改内容：增加 Serializable 属性
    /// </summary>
    ///
    [Serializable]
    public class UserInfoUtil
    {
        //用户ID
        private string _UserID = string.Empty;
        //用户名
        private string _UserName = string.Empty;
        //用户公司代码
        private string _CompanyCD = string.Empty;
        //用户公司名称
        private string _CompanyName = string.Empty;
        //用户部门ID
        private int _DepartmentID;
        private string _DepartmentName;
        //角色列表
        private int[] _Role;
        //用户人员编号
        private int _employeeID;
        //用户员工名称
        private string _employeename;
        //用户工号
        private string _employeeNum;
        //用户菜单信息
        private DataTable _MenuInfo = null;
        //用户页面操作权限信息
        private DataTable _AuthorityInfo = null;
        //用户是否超级管理员
        private string  _IsRoot;
        //是否启用条码
        private bool _isbarcode;
        //出入库是否显示价格
        private bool _isdisplayprcie;
        //是否多计量单位
        private bool _ismoreunit;
        //是否超订单发货
        private bool _isoverorder;
        //允许出入库价格为零
        private bool _iszero;
        //是否启用批次规则设置
        private bool _isbatch;
        //是否启用自动生成凭证
        private bool _isvoucher;
        //是否启用自动审核登帐
        private bool _isapply;
        //小数位数
        private string _selpoint;


        /// <summary>
        /// 是否启用批次  true:启用 false:不启用
        /// </summary>
        public bool IsBarCode
        {
            get { return _isbarcode; }
            set { _isbarcode = value; }
        }

        /// <summary>
        /// 出入库是否显示价格与金额   true:显示 false:不显示
        /// </summary>
        public bool IsDisplayPrice
        {
            get { return _isdisplayprcie; }
            set
            {
                _isdisplayprcie = value;
            }
        }
        /// <summary>
        /// 是否启用多计量单位   true:启用 false:不启用
        /// </summary>
        public bool IsMoreUnit
        {
            get 
            { 
                return _ismoreunit; 
            }
            set
            {
                _ismoreunit = value;
            }
        }
        /// <summary>
        /// 是否启用超订单发货   true:启用 false:不启用
        /// </summary>
        public bool IsOverOrder
        {
            get
            {
                return _isoverorder;
            }
            set
            {
                _isoverorder = value;
            }
        }
        /// <summary>
        /// 允许出入库价格为零   true:启用  false:停用
        /// </summary>
        public bool IsZero
        {
            get
            {
                return _iszero;
            }
            set
            {
                _iszero = value;
            }
        }

        /// <summary>
        /// 是否启用批次规则设置   true:启用 false:不启用
        /// </summary>
        public bool IsBatch
        {
            get
            {
                return _isbatch;
            }
            set
            {
                _isbatch = value;
            }
        }
        /// <summary>
        /// 是否启用自动生成凭证   true:启用 false:不启用
        /// </summary>
        public bool IsVoucher
        {
            get
            {
                return _isvoucher;
            }
            set
            {
                _isvoucher = value;
            }
        }
        /// <summary>
        /// 是否启用自动审核登帐   true:启用 false:不启用
        /// </summary>
        public bool IsApply
        {
            get
            {
                return _isapply;
            }
            set
            {
                _isapply = value;
            }
        }
        /// <summary>
        /// 小数位数 默认为2
        /// </summary>
        public string SelPoint
        {
            get
            {
                return _selpoint;
            }
            set
            {
                _selpoint = value;
            }
        }

        public string EmployeeNum
        {
            get
            {
                return _employeeNum;
            }
            set
            {
                _employeeNum = value;
            }
        }

        public string EmployeeName
        {
            get
            {
                return _employeename;
            }
            set
            {
                _employeename = value;
            }
        }

        public int EmployeeID
        {
            get
            {
                return _employeeID;
            }
            set
            {
                _employeeID = value;
            }
        }

        public int[] Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }

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

        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }

        public int DeptID
        {
            get
            {
                return _DepartmentID;
            }
            set
            {
                _DepartmentID = value;
            }
        }

        public string DeptName
        {
            get
            {
                return _DepartmentName;
            }
            set
            {
                _DepartmentName = value;
            }
        }

        public DataTable MenuInfo
        {
            get
            {
                return _MenuInfo;
            }
            set
            {
                _MenuInfo = value;
            }
        }

        public DataTable AuthorityInfo
        {
            get
            {
                return _AuthorityInfo;
            }
            set
            {
                _AuthorityInfo = value;
            }
        }

        public string IsRoot
        {
            get
            {
                return _IsRoot;
            }
            set
            {
                _IsRoot = value;
            }
        }

    }
}
