using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class TakeMaterialModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _takeno;
        private string _subject;
        private string _fromtype;
        private int _taskid;
        private int _processdeptid;
        private string _tasktype;
        private int _manufacturetype;
        private int _saleid;
        private int _saledeptid;
        private decimal _counttotal;
        private int _creator;
        private DateTime _createdate;
        private int _taker;
        private int _handout;
        private DateTime _takedate;
        private string _billstatus;
        private int _confirmor;
        private int _projectid;
        private DateTime _confirmdate;
        private int _closer;
        private DateTime _closedate;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 领料单编号
        /// </summary>
        public string TakeNo
        {
            set { _takeno = value; }
            get { return _takeno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 源单类型（0无来源，1生产任务单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 生产任务单ID
        /// </summary>
        public int TaskID
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 加工部门ID
        /// </summary>
        public int ProcessDeptID
        {
            set { _processdeptid = value; }
            get { return _processdeptid; }
        }
        /// <summary>
        /// 任务类型（0:普通1：返修
        /// </summary>
        public string TaskType
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }
        /// <summary>
        /// 加工类型ID（系统分类代码表设置）
        /// </summary>
        public int ManufactureType
        {
            set { _manufacturetype = value; }
            get { return _manufacturetype; }
        }
        /// <summary>
        /// 业务员ID
        /// </summary>
        public int SaleID
        {
            set { _saleid = value; }
            get { return _saleid; }
        }
        /// <summary>
        /// 业务员部门ID
        /// </summary>
        public int SaleDeptID
        {
            set { _saledeptid = value; }
            get { return _saledeptid; }
        }
        /// <summary>
        /// 领料数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 领料人ID
        /// </summary>
        public int Taker
        {
            set { _taker = value; }
            get { return _taker; }
        }
        /// <summary>
        /// 发料人ID
        /// </summary>
        public int Handout
        {
            set { _handout = value; }
            get { return _handout; }
        }
        /// <summary>
        /// 发料日期
        /// </summary>
        public DateTime TakeDate
        {
            set { _takedate = value; }
            get { return _takedate; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 所属项目
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人ID
        /// </summary>
        public int Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public DateTime CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model


        #region Detail Model
        private string _detcompanycd;
        private string _detid;
        private string _dettakeno;
        private string _detsortno;
        private string _detproductid;
        private string _detstorageid;
        private string _dettakecount;
        private string _detprice;
        private string _dettotalprice;
        private string _detbackcount;
        private string _detremark;
        private string _detfromtype;
        private string _detfrombillid;
        private string _detfrombillno;
        private string _detfromlineno;
        /// <summary>
        /// 公司编码
        /// </summary>
        public string DetCompanyCD
        {
            set { _detcompanycd = value; }
            get { return _detcompanycd; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public string DetID
        {
            set { _detid = value; }
            get { return _detid; }
        }
        /// <summary>
        /// 领料单编号
        /// </summary>
        public string DetTakeNo
        {
            set { _dettakeno = value; }
            get { return _dettakeno; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string DetSortNo
        {
            set { _detsortno = value; }
            get { return _detsortno; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public string DetProductID
        {
            set { _detproductid = value; }
            get { return _detproductid; }
        }
        /// <summary>
        /// 仓库ID（对应仓库表ID）
        /// </summary>
        public string DetStorageID
        {
            set { _detstorageid = value; }
            get { return _detstorageid; }
        }
        /// <summary>
        /// 领料数量
        /// </summary>
        public string DetTakeCount
        {
            set { _dettakecount = value; }
            get { return _dettakecount; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public string DetPrice
        {
            set { _detprice = value; }
            get { return _detprice; }
        }
        /// <summary>
        /// 总金额
        /// </summary>
        public string DetTotalPrice
        {
            set { _dettotalprice = value; }
            get { return _dettotalprice; }
        }
        /// <summary>
        /// 已退料数量（由退料单更新）
        /// </summary>
        public string DetBackCount
        {
            set { _detbackcount = value; }
            get { return _detbackcount; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string DetRemark
        {
            set { _detremark = value; }
            get { return _detremark; }
        }
        /// <summary>
        /// 源单类型（0无来源，1生产任务单）
        /// </summary>
        public string DetFromType
        {
            set { _detfromtype = value; }
            get { return _detfromtype; }
        }
        /// <summary>
        /// 源单ID
        /// </summary>
        public string DetFromBillID
        {
            set { _detfrombillid = value; }
            get { return _detfrombillid; }
        }
        /// <summary>
        /// 源单编号
        /// </summary>
        public string DetFromBillNo
        {
            set { _detfrombillno = value; }
            get { return _detfrombillno; }
        }
        /// <summary>
        /// 源单行号
        /// </summary>
        public string DetFromLineNo
        {
            set { _detfromlineno = value; }
            get { return _detfromlineno; }
        }

        /// <summary>
        /// 基本计量数量
        /// </summary>
        private string _usedUnitCount = String.Empty;

        /// <summary>
        /// 基本计量数量
        /// </summary>
        public string DetUsedUnitCount
        {
            get
            {
                return _usedUnitCount;
            }
            set
            {
                _usedUnitCount = value;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        private string _usedUnitID = String.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string DetUsedUnitID
        {
            get
            {
                return _usedUnitID;
            }
            set
            {
                _usedUnitID = value;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        private string _unitID = String.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string DetUnitID
        {
            get
            {
                return _unitID;
            }
            set
            {
                _unitID = value;
            }
        }


        /// <summary>
        /// 单位换算率
        /// </summary>
        private string _exRate = String.Empty;

        /// <summary>
        /// 单位换算率
        /// </summary>
        public string DetExRate
        {
            get
            {
                return _exRate;
            }
            set
            {
                _exRate = value;
            }
        }
        /// <summary>
        /// 批次
        /// </summary>
        private string _batchNo = String.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string DetBatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                _batchNo = value;
            }
        }

        /// <summary>
        /// 单价
        /// </summary>
        private string _usedPrice = String.Empty;

        /// <summary>
        /// 单价
        /// </summary>
        public string DetUsedPrice
        {
            get
            {
                return _usedPrice;
            }
            set
            {
                _usedPrice = value;
            }
        }
        #endregion Model
    }
}
