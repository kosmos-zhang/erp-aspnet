/***********************************************
 * 类作用：   SubStorageManager表数据模板      *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/18                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubDeliveryBackModel
    /// 描述：SubDeliveryBack表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/05/18
    /// </summary>
    ///
    public class SubDeliveryBackModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _deptid;
        private string _backno;
        private int _sendid;
        private string _title;
        private int _applyuserid;
        private int _outdeptid;
        private decimal _totalprice;
        private decimal _counttotal;
        private decimal _backfeesum;
        private string _backreason;
        private string _busistatus;
        private int _outuserid;
        private DateTime ? _outdate;
        private int _inuserid;
        private DateTime ? _indate;
        private string _remark;
        private int _creator;
        private DateTime ? _createdate;
        private string _billstatus;
        private int _confirmor;
        private DateTime ? _confirmdate;
        private int _closer;
        private DateTime ? _closedate;
        private DateTime ? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 配送退货单号
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
        }
        /// <summary>
        /// 对应配送单ID
        /// </summary>
        public int SendID
        {
            set { _sendid = value; }
            get { return _sendid; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 退货申请人
        /// </summary>
        public int ApplyUserID
        {
            set { _applyuserid = value; }
            get { return _applyuserid; }
        }
        /// <summary>
        /// 配送部门
        /// </summary>
        public int OutDeptID
        {
            set { _outdeptid = value; }
            get { return _outdeptid; }
        }
        /// <summary>
        /// 退货金额合计
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 退货数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 退货费用合计
        /// </summary>
        public decimal BackFeeSum
        {
            set { _backfeesum = value; }
            get { return _backfeesum; }
        }
        /// <summary>
        /// 退货理由描述
        /// </summary>
        public string BackReason
        {
            set { _backreason = value; }
            get { return _backreason; }
        }
        /// <summary>
        /// 业务状态（1退货申请，2退货出库，3验收入库，4退货完成）
        /// </summary>
        public string BusiStatus
        {
            set { _busistatus = value; }
            get { return _busistatus; }
        }
        /// <summary>
        /// 分店出库人
        /// </summary>
        public int OutUserID
        {
            set { _outuserid = value; }
            get { return _outuserid; }
        }
        /// <summary>
        /// 分店出库时间
        /// </summary>
        public DateTime ? OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
        }
        /// <summary>
        /// 验货入库人
        /// </summary>
        public int InUserID
        {
            set { _inuserid = value; }
            get { return _inuserid; }
        }
        /// <summary>
        /// 验货入库时间
        /// </summary>
        public DateTime ? InDate
        {
            set { _indate = value; }
            get { return _indate; }
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
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime ? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人
        /// </summary>
        public int Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public DateTime ? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
