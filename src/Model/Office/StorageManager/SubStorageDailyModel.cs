using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
 public    class SubStorageDailyModel
    {
        #region Model
        private String   _id;
        private String  _companycd;
        private String    _deptid;
        private String    _dailydate;
        private String    _productid;
        private String    _storageid;
        private String  _batchno;
        private String    _initincount;
        private String    _sendincount;
        private String    _subsalebackincount;
        private String    _salebackincount;
        private String    _dispincont;
        private String    _subsaleoutcount;
        private String    _saleoutcount;
        private String    _dispoutcount;
        private String    _sendoutcount;
        private String    _intotal;
        private String    _outtotal;
        private String    _todaycount;
        private String    _salefee;
        private String    _salebackfee;
        private String  _remark;
        private String    _createdate;
        private String    _creator;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public String   ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public String  CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public String    DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 日结日期
        /// </summary>
        public String    DailyDate
        {
            set { _dailydate = value; }
            get { return _dailydate; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public String    ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public String    StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 批次
        /// </summary>
        public String  BatchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 期初库存录入数量
        /// </summary>
        public String    InitInCount
        {
            set { _initincount = value; }
            get { return _initincount; }
        }
        /// <summary>
        /// 配送入库数量
        /// </summary>
        public String    SendInCount
        {
            set { _sendincount = value; }
            get { return _sendincount; }
        }
        /// <summary>
        /// 销售退货入库数量1（分店发货模式）
        /// </summary>
        public String    SubSaleBackInCount
        {
            set { _subsalebackincount = value; }
            get { return _subsalebackincount; }
        }
        /// <summary>
        /// 销售退货入库数量2（总店发货模式）
        /// </summary>
        public String    SaleBackInCount
        {
            set { _salebackincount = value; }
            get { return _salebackincount; }
        }
        /// <summary>
        /// 门店调拨入库数量
        /// </summary>
        public String    DispInCont
        {
            set { _dispincont = value; }
            get { return _dispincont; }
        }
        /// <summary>
        /// 销售出库数量（分店发货模式）
        /// </summary>
        public String    SubSaleOutCount
        {
            set { _subsaleoutcount = value; }
            get { return _subsaleoutcount; }
        }
        /// <summary>
        /// 销售出库数量（总店发货模式）
        /// </summary>
        public String    SaleOutCount
        {
            set { _saleoutcount = value; }
            get { return _saleoutcount; }
        }
        /// <summary>
        /// 门店调拨出库数量
        /// </summary>
        public String    DispOutCount
        {
            set { _dispoutcount = value; }
            get { return _dispoutcount; }
        }

        private string _InitBatchCount;
          /// <summary>
        /// 期初库存导入
        /// </summary>
        public String InitBatchCount
        {
            set { _InitBatchCount = value; }
            get { return _InitBatchCount; }
        }
     
        /// <summary>
        /// 配送退货出库数量
        /// </summary>
        public String    SendOutCount
        {
            set { _sendoutcount = value; }
            get { return _sendoutcount; }
        }
        /// <summary>
        /// 入库总数量（等于前面所有入库数量字段的累加）
        /// </summary>
        public String    InTotal
        {
            set { _intotal = value; }
            get { return _intotal; }
        }
        /// <summary>
        /// 出库总数量（等于前面所有出库数量字段的累加）
        /// </summary>
        public String    OutTotal
        {
            set { _outtotal = value; }
            get { return _outtotal; }
        }
        /// <summary>
        /// 当日结存量(现有存量，从门店分仓存量表获取)
        /// </summary>
        public String    TodayCount
        {
            set { _todaycount = value; }
            get { return _todaycount; }
        }
        /// <summary>
        /// 当日销售金额
        /// </summary>
        public String    SaleFee
        {
            set { _salefee = value; }
            get { return _salefee; }
        }
        /// <summary>
        /// 当日销售退货金额
        /// </summary>
        public String    SaleBackFee
        {
            set { _salebackfee = value; }
            get { return _salebackfee; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public String  Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 操作日期
        /// </summary>
        public String    CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 操作人(手工日结的取当前登录人的ID,自动日结的默认为0)
        /// </summary>
        public String    Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        #endregion Model

    }
}
