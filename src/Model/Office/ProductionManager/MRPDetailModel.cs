using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class MRPDetailModel
    {
        public MRPDetailModel()
		{}
        #region Model
        private string _companycd;
        private int _id;
        private string _mrpno;
        private int _sortno;
        private int _productid;
        private int _unitid;
        private decimal _plancount;
        private DateTime _plandate;
        private string _materialsource;
        private int _frombillid;
        private int _fromlineno;
        private string _remark;
        private decimal _processedcount;
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
        /// MRP编号
        /// </summary>
        public string MRPNo
        {
            set { _mrpno = value; }
            get { return _mrpno; }
        }
        /// <summary>
        /// 序号（行号）
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 计量单位ID
        /// </summary>
        public int UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal PlanCount
        {
            set { _plancount = value; }
            get { return _plancount; }
        }
        /// <summary>
        /// 计划供料日期
        /// </summary>
        public DateTime PlanDate
        {
            set { _plandate = value; }
            get { return _plandate; }
        }
        /// <summary>
        /// 物料来源0:采购 1：生产2：库存
        /// </summary>
        public string MaterialSource
        {
            set { _materialsource = value; }
            get { return _materialsource; }
        }
        /// <summary>
        /// 来源销售订单ID（对应销售订单表ID）
        /// </summary>
        public int FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 来源销售订单行号
        /// </summary>
        public int FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
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
        /// 已处理数量（由生产任务单更新）
        /// </summary>
        public decimal ProcessedCount
        {
            set { _processedcount = value; }
            get { return _processedcount; }
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

    }
}
