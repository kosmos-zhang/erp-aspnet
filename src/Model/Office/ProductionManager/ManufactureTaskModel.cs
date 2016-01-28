using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureTaskModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _taskno;
        private string _fromtype;
        private string _subject;
        private int _principal;
        private int _deptid;
        private string _tasktype;
        private int _manufacturetype;
        private decimal _counttotal;
        private string _billstatus;
        private string _documenturl;
        private int _projectid;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime _confirmdate;
        private int _closer;
        private DateTime _closedate;
        private string _remark;
        private DateTime _modifieddate;
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
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 任务单编号
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 源单类型（0无来源，1主生产计划）
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
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
        /// 任务类型(0:普通1：返修)
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
        /// 安排生产数量总计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
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
        /// 附件
        /// </summary>
        public string DocumentURL
        {
            set { _documenturl = value; }
            get { return _documenturl; }
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
        private string _dettaskno;
        private string _detsortno;
        private string _detproductid;
        private string _detproductcount;
        private string _detusedunitid;
        private string _detusedunitcount;
        private string _detexrate;
        private string _detbomid;
        private string _detrouteid;
        private string _detstartdate;
        private string _detenddate;
        private string _detfromtype;
        private string _detfrombillid;
        private string _detfrombillno;
        private string _detfromlineno;
        private string _detproductedcount;
        private string _detincount;
        private string _detapplycheckcount;
        private string _detcheckedcount;
        private string _detpasscount;
        private string _detnotpasscount;
        private string _detremark;
        private string _detmodifieddate;
        private string _detmodifieduserid;
        /// <summary>
        /// 任务单编号
        /// </summary>
        public string DetTaskNo
        {
            set { _dettaskno = value; }
            get { return _dettaskno; }
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
        /// 数量
        /// </summary>
        public string DetProductCount
        {
            set { _detproductcount = value; }
            get { return _detproductcount; }
        }
        /// <summary>
        /// 计量单位ID 
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
        /// 换算率
        /// </summary>
        public string DetExRate
        {
            set { _detexrate = value; }
            get { return _detexrate; }
        }
        /// <summary>
        /// 物料清单ID
        /// </summary>
        public string DetBomID
        {
            set { _detbomid = value; }
            get { return _detbomid; }
        }
        /// <summary>
        /// 工艺路线ID
        /// </summary>
        public string DetRouteID
        {
            set { _detrouteid = value; }
            get { return _detrouteid; }
        }
        /// <summary>
        /// 计划开工时间
        /// </summary>
        public string DetStartDate
        {
            set { _detstartdate = value; }
            get { return _detstartdate; }
        }
        /// <summary>
        /// 计划完工时间
        /// </summary>
        public string DetEndDate
        {
            set { _detenddate = value; }
            get { return _detenddate; }
        }
        /// <summary>
        /// 源单类型（0无来源，1主生产计划）
        /// </summary>
        public string DetFromType
        {
            set { _detfromtype = value; }
            get { return _detfromtype; }
        }
        /// <summary>
        /// 源单ID（对应主生产计划ID）
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
        /// 已生产数量（由生产任务汇报模块更新）
        /// </summary>
        public string DetProductedCount
        {
            set { _detproductedcount = value; }
            get { return _detproductedcount; }
        }
        /// <summary>
        /// 已入库数量（由生产完工入库模块更新）
        /// </summary>
        public string DetInCount
        {
            set { _detincount = value; }
            get { return _detincount; }
        }
        /// <summary>
        /// 已报质检数量(由质检模块更新)
        /// </summary>
        public string DetApplyCheckCount
        {
            set { _detapplycheckcount = value; }
            get { return _detapplycheckcount; }
        }
        /// <summary>
        /// 实检数量(由质检模块更新)
        /// </summary>
        public string DetCheckedCount
        {
            set { _detcheckedcount = value; }
            get { return _detcheckedcount; }
        }
        /// <summary>
        /// 合格数量(由质检模块更新)
        /// </summary>
        public string DetPassCount
        {
            set { _detpasscount = value; }
            get { return _detpasscount; }
        }
        /// <summary>
        /// 不合格数量(由质检模块更新)
        /// </summary>
        public string DetNotPassCount
        {
            set { _detnotpasscount = value; }
            get { return _detnotpasscount; }
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
