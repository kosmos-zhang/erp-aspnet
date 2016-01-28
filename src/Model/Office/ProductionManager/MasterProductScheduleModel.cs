using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class MasterProductScheduleModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _planno;
        private string _subject;
        private int _principal;
        private int _deptid;
        private decimal _counttotal; 
        private int _creator;
        private DateTime _createdate;
        private string _billstatus;
        private int _confirmor;
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
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
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
        /// 负责人ID
        /// </summary>
        public int Principal
        {
            set { _principal = value; }
            get { return _principal; }
        }
        /// <summary>
        /// 生产部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 生产数量合计
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
        /// 单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
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
        private string _detid;
        private string _detcompanycd;
        private string _detplanno;
        private string _detsortno;
        private string _detproductid;
        private string _detusedunitid;
        private string _detusedunitcount;
        private string _detexrate;
        private string _detunitid;
        private string _detspecification;
        private string _detproductcount;
        private string _detproducecount;
        private string _detstartdate;
        private string _detenddate;
        private string _detfrombillid;
        private string _detfrombillno;
        private string _detfromlineno;
        private string _detplancount;
        private string _detprocessedcount;
        private string _detremark;
        private string _detmodifieddate;
        private string _detmodifieduserid;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string DetID
        {
            set { _detid = value; }
            get { return _detid; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string DetCompanyCD
        {
            set { _detcompanycd = value; }
            get { return _detcompanycd; }
        }
        /// <summary>
        /// 主生产计划编号（对应主生产计划表）
        /// </summary>
        public string DetPlanNo
        {
            set { _detplanno = value; }
            get { return _detplanno; }
        }
        /// <summary>
        /// 序号（行号）
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
        /// 基本单位
        /// </summary>
        public string DetUnitID
        {
            set { _detunitid = value; }
            get { return _detunitid; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string DetUsedUnitID
        {
            set { _detusedunitid = value; }
            get { return _detusedunitid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string DetUsedUnitCount
        {
            set { _detusedunitcount = value; }
            get { return _detusedunitcount; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string DetExRate
        {
            set { _detexrate = value; }
            get { return _detexrate; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string DetSpecification
        {
            set { _detspecification = value; }
            get { return _detspecification; }
        }
        /// <summary>
        /// 计划需求数量
        /// </summary>
        public string DetProductCount
        {
            set { _detproductcount = value; }
            get { return _detproductcount; }
        }
        /// <summary>
        /// 计划需求数量
        /// </summary>
        public string DetProduceCount
        {
            set { _detproducecount = value; }
            get { return _detproducecount; }
        }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public string DetStartDate
        {
            set { _detstartdate = value; }
            get { return _detstartdate; }
        }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public string DetEndDate
        {
            set { _detenddate = value; }
            get { return _detenddate; }
        }
        /// <summary>
        /// 来源销售订单明细表ID（对应销售订单明细表ID）
        /// </summary>
        public string DetFromBillID
        {
            set { _detfrombillid = value; }
            get { return _detfrombillid; }
        }
        /// <summary>
        /// 来源销售订单编号（对应销售订单编号）
        /// </summary>
        public string DetFromBillNo
        {
            set { _detfrombillno = value; }
            get { return _detfrombillno; }
        }        /// <summary>
        /// 来源销售订单行号
        /// </summary>
        public string DetFromLineNo
        {
            set { _detfromlineno = value; }
            get { return _detfromlineno; }
        }
        /// <summary>
        /// 已下达数量（由生产任务单更新）
        /// </summary>
        public string DetPlanCount
        {
            set { _detplancount = value; }
            get { return _detplancount; }
        }
        /// <summary>
        /// 已生产数量（由生产任务单更新）
        /// </summary>
        public string DetProcessedCount
        {
            set { _detprocessedcount = value; }
            get { return _detprocessedcount; }
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
        /// 最后更新日期
        /// </summary>
        public string DetModifiedDate
        {
            set { _detmodifieddate = value; }
            get { return _detmodifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string DetModifiedUserID
        {
            set { _detmodifieduserid = value; }
            get { return _detmodifieduserid; }
        }
        #endregion Detail Model
    }
}
