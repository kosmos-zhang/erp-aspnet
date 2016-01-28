using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class MRPModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _mrpno;
        private string _subject;
        private int _planid;
        private string _remark;
        private int _principal;
        private int _deptid;
        private decimal _counttotal;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime _confirmdate;
        private int _closer;
        private DateTime _closedate;
        private string _billstatus;
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
        /// 主题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 主生产计划ID（对应主生产计划表ID）
        /// </summary>
        public int PlanID
        {
            set { _planid = value; }
            get { return _planid; }
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
        /// 计划数量合计
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
        /// 单据状态
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
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
        private string _detmrpno;
        private string _detsortno;
        private string _detproductid;
        private string _detunitid;
        private string _detgrosscount;
        private string _detplancount;
        private string _detusedunitid;
        private string _detusedunitcount;
        private string _detexrate;
        private string _detplandate;
        private string _detmaterialsource;
        private string _detfrombillid;
        private string _detfrombillno;
        private string _detfromlineno;
        private string _detremark;
        private string _detprocessedcount;
        private string _detmodifieddate;
        private string _detmodifieduserid;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string detID
        {
            set { _detid = value; }
            get { return _detid; }
        }
        /// <summary>
        /// MRP编号
        /// </summary>
        public string detMRPNo
        {
            set { _detmrpno = value; }
            get { return _detmrpno; }
        }
        /// <summary>
        /// 序号（行号）
        /// </summary>
        public string detSortNo
        {
            set { _detsortno = value; }
            get { return _detsortno; }
        }
        /// <summary>
        /// 物品ID
        /// </summary>
        public string detProductID
        {
            set { _detproductid = value; }
            get { return _detproductid; }
        }
        /// <summary>
        /// 基本单位
        /// </summary>
        public string detUnitID
        {
            set { _detunitid = value; }
            get { return _detunitid; }
        }
        /// <summary>
        /// 毛需求量
        /// </summary>
        public string detGrossCount
        {
            set { _detgrosscount = value; }
            get { return _detgrosscount; }
        }
        /// <summary>
        /// 计划数量
        /// </summary>
        public string detPlanCount
        {
            set { _detplancount = value; }
            get { return _detplancount; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string detUsedUnitID
        {
            set { _detusedunitid = value; }
            get { return _detusedunitid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string detUsedUnitCount
        {
            set { _detusedunitcount = value; }
            get { return _detusedunitcount; }
        }
        /// <summary>
        /// 换算率
        /// </summary>
        public string detExRate
        {
            set { _detexrate = value; }
            get { return _detexrate; }
        }
        /// <summary>
        /// 计划供料日期
        /// </summary>
        public string detPlanDate
        {
            set { _detplandate = value; }
            get { return _detplandate; }
        }
        /// <summary>
        /// 物料来源0:采购 1：生产2：库存
        /// </summary>
        public string detMaterialSource
        {
            set { _detmaterialsource = value; }
            get { return _detmaterialsource; }
        }
        /// <summary>
        /// 源单ID（主生产计划明细ID)
        /// </summary>
        public string detFromBillID
        {
            set { _detfrombillid = value; }
            get { return _detfrombillid; }
        }
        /// <summary>
        /// 源单编号(主生产计划编号)
        /// </summary>
        public string detFromBillNo
        {
            set { _detfrombillno = value; }
            get { return _detfrombillno; }
        }
        /// <summary>
        /// 源单行号
        /// </summary>
        public string detFromLineNo
        {
            set { _detfromlineno = value; }
            get { return _detfromlineno; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string detRemark
        {
            set { _detremark = value; }
            get { return _detremark; }
        }
        /// <summary>
        /// 已处理数量（由生产任务单更新）
        /// </summary>
        public string detProcessedCount
        {
            set { _detprocessedcount = value; }
            get { return _detprocessedcount; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string detModifiedDate
        {
            set { _detmodifieddate = value; }
            get { return _detmodifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string detModifiedUserID
        {
            set { _detmodifieduserid = value; }
            get { return _detmodifieduserid; }
        }
        #endregion Detail Model
    }
}
