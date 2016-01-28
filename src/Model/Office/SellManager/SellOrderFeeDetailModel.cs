using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public   class SellOrderFeeDetailModel
    {
        public SellOrderFeeDetailModel()
		{}
        #region Model
        private int _id;
        private string _orderno;
        private int? _sortno;
        private int? _feeid;
        private decimal? _feetotal;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _companycd;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int? SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 费用类别ID（对应费用代码表ID）
        /// </summary>
        public int? FeeID
        {
            set { _feeid = value; }
            get { return _feeid; }
        }
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal? FeeTotal
        {
            set { _feetotal = value; }
            get { return _feetotal; }
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
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        #endregion Model

    }
}
