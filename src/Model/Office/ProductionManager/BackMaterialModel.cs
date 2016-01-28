using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class BackMaterialModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _backno;
        private string _subject;
        private int _taskid;
        private string _fromtype;
        private int _takeid;
        private int _processdeptid;
        private string _tasktype;
        private int _manufacturetype;
        private int _saleid;
        private int _saledeptid;
        private decimal _counttotal;
        private int _taker;
        private int _receiver;
        private DateTime _receivedate;
        private string _remark;
        private int _creator;
        private DateTime _createdate;
        private string _billstatus;
        private int _confirmor;
        private DateTime _confirmdate;
        private int _closer;
        private DateTime _closedate;
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
        /// 退料单编号
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
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
        /// 生产任务单ID
        /// </summary>
        public int TaskID
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 源单类型（0无来源，1领料单）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 领料单ID
        /// </summary>
        public int TakeID
        {
            set { _takeid = value; }
            get { return _takeid; }
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
        /// 退料数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 退料人ID
        /// </summary>
        public int Taker
        {
            set { _taker = value; }
            get { return _taker; }
        }
        /// <summary>
        /// 收料人ID
        /// </summary>
        public int Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 收料日期
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
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
        private string _detbackno;
        private string _detsortno;
        private string _detproductid;
        private string _detbackreasonid;
        private string _detstorageid;
        private string _detbackcount;
        private string _detprice;
        private string _dettotalprice;
        private string _detremark;
        private string _detfromtype;
        private string _detfrombillid;
        private string _detfrombillno;
        private string _detfromlineno;
        private string _detUnitID;
        private string _detUsedUnitID;
        private string _detUsedUnitCount;
        private string _detExRate;
        private string _detBatchNo;
        private string _detUsedPrice;

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
        /// 退料单编号
        /// </summary>
        public string DetBackNo
        {
            set { _detbackno = value; }
            get { return _detbackno; }
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
        /// 退料原因ID(对应原因表ID)
        /// </summary>
        public string DetBackReasonID
        {
            set { _detbackreasonid = value; }
            get { return _detbackreasonid; }
        }
        /// <summary>
        /// 退料仓库ID
        /// </summary>
        public string DetStorageID
        {
            set { _detstorageid = value; }
            get { return _detstorageid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string DetBackCount
        {
            set { _detbackcount = value; }
            get { return _detbackcount; }
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
        /// 备注
        /// </summary>
        public string DetRemark
        {
            set { _detremark = value; }
            get { return _detremark; }
        }
        /// <summary>
        /// 源单类型（0无来源，1领料单）
        /// </summary>
        public string DetFromType
        {
            set { _detfromtype = value; }
            get { return _detfromtype; }
        }
        /// <summary>
        /// 源单ID（领料单ID）
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
        /// 基本单位
        /// </summary>
        public string DetUnitID
        {
            set { _detUnitID = value; }
            get { return _detUnitID; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string DetUsedUnitID
        {
            set { _detUsedUnitID = value; }
            get { return _detUsedUnitID; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string DetUsedUnitCount
        {
            set { _detUsedUnitCount = value; }
            get { return _detUsedUnitCount; }
        }
        /// <summary>
        /// 换算率
        /// </summary>
        public string DetExRate
        {
            set { _detExRate = value; }
            get { return _detExRate; }
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string DetBatchNo
        {
            set { _detBatchNo = value; }
            get { return _detBatchNo; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public string DetUsedPrice
        {
            set { _detUsedPrice = value; }
            get { return _detUsedPrice; }
        }

        #endregion Detail Model
    }
}
