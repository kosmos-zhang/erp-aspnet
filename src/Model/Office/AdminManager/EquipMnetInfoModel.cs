
/**********************************************
 * 类作用：   EquipmentInfo表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/02/26
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：EquipMnetInfoModel
    /// 描述：EquipmentInfo表数据模板
    /// 
    /// 作者：lysong
    /// 创建时间：2009/02/26
    /// 最后修改时间：2009/02/26
    /// </summary>
    ///
    public class EquipMnetInfoModel
    {
        #region 字段
        /// <summary>
        /// id
        /// </summary>
        private System.Int32 _id;
        /// <summary>
        /// 设备编号
        /// </summary>
        private System.String _equipmentNo;
        /// <summary>
        /// 设备序列号
        /// </summary>
        private System.String _equipmentIndex;
        /// <summary>
        /// 公司代码
        /// </summary>
        private System.String _companyCD;
        /// <summary>
        /// 设备名称
        /// </summary>
        private System.String _equipmentName;
        /// <summary>
        /// 规格类别
        /// </summary>
        private System.String _norm;
        /// <summary>
        /// 精度
        /// </summary>
        private System.String _precision;
        /// <summary>
        /// 购入日期
        /// </summary>
        private DateTime _buyDate;
        /// <summary>
        /// 供应商
        /// </summary>
        private System.String _provider;
        /// <summary>
        /// 类别
        /// </summary>
        private int _type;
        /// <summary>
        /// 保修期
        /// </summary>
        private System.String _warranty;
        /// <summary>
        /// 检定期限
        /// </summary>
        private string _examinePeriod;
        /// <summary>
        /// 当前使用人
        /// </summary>
        private int _currentUser;
        /// <summary>
        /// 当前部门
        /// </summary>
        private int _currentDepartment;
        /// <summary>
        /// 有无配件
        /// </summary>
        private System.String _fittingFlag;
      
        /// <summary>
        /// 单位
        /// </summary>
        private int _unit;
        private decimal _money;
        private string _equifrom;
        private string _attachment;
        private string _pyshort;
        private int _creator;
        private DateTime _createdate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private int _equipmentcount;
        /// <summary>
        /// 
        /// </summary>
        private System.String _status;
        /// <summary>
        /// 设备明细
        /// </summary>
        private System.String _equipmentDetail;
       
       
        #endregion

        public EquipMnetInfoModel()
        {
        }

        #region
        /// <summary>
        /// id
        /// </summary>
        public System.Int32 ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int EquipmentCount
        {
            get
            {
                return this._equipmentcount;
            }
            set
            {
                this._equipmentcount = value;
            }
        }
        
        /// <summary>
        /// _pyshort
        /// </summary>
        public string PYShort
        {
            get
            {
                return this._pyshort;
            }
            set
            {
                this._pyshort = value;
            }
        }
        /// <summary>
        /// _creator
        /// </summary>
        public System.Int32 Creator
        {
            get
            {
                return this._creator;
            }
            set
            {
                this._creator = value;
            }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return this._createdate;
            }
            set
            {
                this._createdate = value;
            }
        }
        /// <summary>
        /// _modifieddate
        /// </summary>
        public DateTime ModifiedDate
        {
            get
            {
                return this._modifieddate;
            }
            set
            {
                this._modifieddate = value;
            }
        }
        /// <summary>
        /// ModifiedUserid
        /// </summary>
        public string ModifiedUserid
        {
            get
            {
                return this._modifieduserid;
            }
            set
            {
                this._modifieduserid = value;
            }
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public System.String EquipmentNo
        {
            get
            {
                return this._equipmentNo;
            }
            set
            {
                this._equipmentNo = value;
            }
        }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public System.String EquipmentIndex
        {
            get
            {
                return this._equipmentIndex;
            }
            set
            {
                this._equipmentIndex = value;
            }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public System.String CompanyCD
        {
            get
            {
                return this._companyCD;
            }
            set
            {
                this._companyCD = value;
            }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public System.String EquipmentName
        {
            get
            {
                return this._equipmentName;
            }
            set
            {
                this._equipmentName = value;
            }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public System.String Norm
        {
            get
            {
                return this._norm;
            }
            set
            {
                this._norm = value;
            }
        }
        /// <summary>
        /// 精度
        /// </summary>
        public System.String Precision
        {
            get
            {
                return this._precision;
            }
            set
            {
                this._precision = value;
            }
        }
        /// <summary>
        /// 购入日期
        /// </summary>
        public DateTime BuyDate
        {
            get
            {
                return this._buyDate;
            }
            set
            {
                this._buyDate = value;
            }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public System.String Provider
        {
            get
            {
                return this._provider;
            }
            set
            {
                this._provider = value;
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        /// <summary>
        /// 保修期
        /// </summary>
        public System.String Warranty
        {
            get
            {
                return this._warranty;
            }
            set
            {
                this._warranty = value;
            }
        }
        /// <summary>
        /// 检定周期
        /// </summary>
        public string ExaminePeriod
        {
            get
            {
                return this._examinePeriod;
            }
            set
            {
                this._examinePeriod = value;
            }
        }
        /// <summary>
        /// 当前使用人
        /// </summary>
        public int CurrentUser
        {
            get
            {
                return this._currentUser;
            }
            set
            {
                this._currentUser = value;
            }
        }
        /// <summary>
        /// 当前部门
        /// </summary>
        public int CurrentDepartment
        {
            get
            {
                return this._currentDepartment;
            }
            set
            {
                this._currentDepartment = value;
            }
        }
        /// <summary>
        /// 有无配件
        /// </summary>
        public System.String FittingFlag
        {
            get
            {
                return this._fittingFlag;
            }
            set
            {
                this._fittingFlag = value;
            }
        }
        
        /// <summary>
        /// 单位
        /// </summary>
        public int Unit
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }
       
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 设备来源
        /// </summary>
        public string EquiFrom
        {
            set { _equifrom = value; }
            get { return _equifrom; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        
        /// <summary>
        /// 设备状态
        /// </summary>
        public System.String Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        /// <summary>
        /// 设备明细
        /// </summary>
        public System.String EquipmentDetail
        {
            get
            {
                return this._equipmentDetail;
            }
            set
            {
                this._equipmentDetail = value;
            }
        }
      
        #endregion
    }
}
