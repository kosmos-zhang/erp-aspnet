using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
  public   class DayEndModelDetail
    {
        #region Model
        private String  _id;
        private String _companycd;
        private String  _dailydate;
        private String  _productid;
        private String  _storageid;
        private String _batchno;
        private String  _initincount;
        private String  _initbatchcount;
        private String  _phurincount;
        private String  _makeincount;
        private String  _salebackincount;
        private String  _subsalebackincount;
        private String  _redincount;
        private String  _otherincount;
        private String  _backincount;
        private String  _takeincount;
        private String  _dispincount;
        private String  _sendincount;
        private String  _saleoutcount;
        private String  _subsaleoutcount;
        private String  _phurbackoutcount;
        private String  _redoutcount;
        private String  _otheroutcount;
        private String  _dispoutcount;
        private String  _lendcount;
        private String  _adjustcount;
        private String  _badcount;
        private String  _checkcount;
        private String  _takeoutcount;
        private String  _sendoutcount;
        private String  _InTotal;
        private String  _outtotal;
        private String  _todaycount;
        private String  _salefee;
        private String  _salebackfee;
        private String  _phurfee;
        private String  _phurbackfee;
        private String _remark;
        private String  _createdate;
        private String  _creator;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public String  ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 日结日期
        /// </summary>
        public String  DailyDate
        {
            set { _dailydate = value; }
            get { return _dailydate; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public String  ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public String  StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 期初库存录入数量
        /// </summary>
        public String  InitInCount
        {
            set { _initincount = value; }
            get { return _initincount; }
        }
        /// <summary>
        /// 期初库存导入数量
        /// </summary>
        public String  InitBatchCount
        {
            set { _initbatchcount = value; }
            get { return _initbatchcount; }
        }
        /// <summary>
        /// 采购入库数量
        /// </summary>
        public String  PhurInCount
        {
            set { _phurincount = value; }
            get { return _phurincount; }
        }
        /// <summary>
        /// 生产完工入库数量
        /// </summary>
        public String  MakeInCount
        {
            set { _makeincount = value; }
            get { return _makeincount; }
        }
        /// <summary>
        /// 销售退货入库数量
        /// </summary>
        public String  SaleBackInCount
        {
            set { _salebackincount = value; }
            get { return _salebackincount; }
        }
        /// <summary>
        /// 门店销售退货入库数量（针对总店发货模式）
        /// </summary>
        public String  SubSaleBackInCount
        {
            set { _subsalebackincount = value; }
            get { return _subsalebackincount; }
        }
        /// <summary>
        /// 红冲入库数量
        /// </summary>
        public String  RedInCount
        {
            set { _redincount = value; }
            get { return _redincount; }
        }
        /// <summary>
        /// 其他入库数量
        /// </summary>
        public String  OtherInCount
        {
            set { _otherincount = value; }
            get { return _otherincount; }
        }
        /// <summary>
        /// 借货返还数量
        /// </summary>
        public String  BackInCount
        {
            set { _backincount = value; }
            get { return _backincount; }
        }
        /// <summary>
        /// 生产退料数量
        /// </summary>
        public String  TakeInCount
        {
            set { _takeincount = value; }
            get { return _takeincount; }
        }
        /// <summary>
        /// 库存调拨入库数量
        /// </summary>
        public String  DispInCount
        {
            set { _dispincount = value; }
            get { return _dispincount; }
        }
        /// <summary>
        /// 配送退货入库数量
        /// </summary>
        public String  SendInCount
        {
            set { _sendincount = value; }
            get { return _sendincount; }
        }
        /// <summary>
        /// 销售出库数量
        /// </summary>
        public String  SaleOutCount
        {
            set { _saleoutcount = value; }
            get { return _saleoutcount; }
        }
        /// <summary>
        /// 门店销售数量（针对总店发货模式）
        /// </summary>
        public String  SubSaleOutCount
        {
            set { _subsaleoutcount = value; }
            get { return _subsaleoutcount; }
        }
        /// <summary>
        /// 采购退货出库数量
        /// </summary>
        public String  PhurBackOutCount
        {
            set { _phurbackoutcount = value; }
            get { return _phurbackoutcount; }
        }
        /// <summary>
        /// 红冲出库数量
        /// </summary>
        public String  RedOutCount
        {
            set { _redoutcount = value; }
            get { return _redoutcount; }
        }
        /// <summary>
        /// 其他出库数量
        /// </summary>
        public String  OtherOutCount
        {
            set { _otheroutcount = value; }
            get { return _otheroutcount; }
        }
        /// <summary>
        /// 库存调拨出库数量
        /// </summary>
        public String  DispOutCount
        {
            set { _dispoutcount = value; }
            get { return _dispoutcount; }
        }
        /// <summary>
        /// 借出数量
        /// </summary>
        public String  LendCount
        {
            set { _lendcount = value; }
            get { return _lendcount; }
        }
        /// <summary>
        /// 库存调整数量（调减记为负数，调增记为正数。正数当入库，负数当出库）
        /// </summary>
        public String  AdjustCount
        {
            set { _adjustcount = value; }
            get { return _adjustcount; }
        }
        /// <summary>
        /// 库存报损数量
        /// </summary>
        public String  BadCount
        {
            set { _badcount = value; }
            get { return _badcount; }
        }
        /// <summary>
        /// 盘点调整数量（盘亏记负，盘盈记正。正数当入库，负数当出库）
        /// </summary>
        public String  CheckCount
        {
            set { _checkcount = value; }
            get { return _checkcount; }
        }
        /// <summary>
        /// 领料出库数量
        /// </summary>
        public String  TakeOutCount
        {
            set { _takeoutcount = value; }
            get { return _takeoutcount; }
        }
        /// <summary>
        /// 配送出库数量
        /// </summary>
        public String  SendOutCount
        {
            set { _sendoutcount = value; }
            get { return _sendoutcount; }
        }
        /// <summary>
        /// 入库总数量（等于前面所有入库数量字段的累加减去红冲入库数量）
        /// </summary>
        public String  InTotal
        {
            set { _InTotal = value; }
            get { return _InTotal; }
        }
        /// <summary>
        /// 出库总数量（等于前面所有出库数量字段的累加减去红冲出库数量）
        /// </summary>
        public String  OutTotal
        {
            set { _outtotal = value; }
            get { return _outtotal; }
        }
        /// <summary>
        /// 当日结存量 
        /// </summary>
        public String  TodayCount
        {
            set { _todaycount = value; }
            get { return _todaycount; }
        }
        /// <summary>
        /// 当日销售金额
        /// </summary>
        public String  SaleFee
        {
            set { _salefee = value; }
            get { return _salefee; }
        }
        /// <summary>
        /// 当日销售退货金额
        /// </summary>
        public String  SaleBackFee
        {
            set { _salebackfee = value; }
            get { return _salebackfee; }
        }
        /// <summary>
        /// 当日采购金额
        /// </summary>
        public String  PhurFee
        {
            set { _phurfee = value; }
            get { return _phurfee; }
        }
        /// <summary>
        /// 当日采购退货金额
        /// </summary>
        public String  PhurBackFee
        {
            set { _phurbackfee = value; }
            get { return _phurbackfee; }
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
        /// 操作日期
        /// </summary>
        public String  CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 操作人(手工日结的取当前登录人的ID,自动日结的默认为0)
        /// </summary>
        public String  Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        #endregion Model
    }
}
