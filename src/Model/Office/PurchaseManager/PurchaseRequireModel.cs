using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    public class PurchaseRequireModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _mrpcd;
        private string _prodid;
        private string _prodno;
        private string _prodname;
        private string _prodtypeid;
        private string _prodtypename;
        private string _standards;
        private string _unitid;
        private string _needcount;
        private string _hasnum;
        private string _wantingnum;
        private string _waitingdays;
        private string _requiredate;
        private string _endrequiredate;
        private string _purpose;
        private string _remark;
        private string _billstatus;
        private string _ordercount;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;
        private string _createcondition;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 资源需求计划ID（对应资源需求计划表ID）
        /// </summary>
        public string MRPCD
        {
            set { _mrpcd = value; }
            get { return _mrpcd; }
        }
        /// <summary>
        /// 物料编码（对应物品表ID）
        /// </summary>
        public string ProdID
        {
            set { _prodid = value; }
            get { return _prodid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProdNo
        {
            set { _prodno = value; }
            get { return _prodno; }
        }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string ProdName
        {
            set { _prodname = value; }
            get { return _prodname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProdTypeID
        {
            set { _prodtypeid = value; }
            get { return _prodtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProdTypeName
        {
            set { _prodtypename = value; }
            get { return _prodtypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Standards
        {
            set { _standards = value; }
            get { return _standards; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NeedCount
        {
            set { _needcount = value; }
            get { return _needcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HasNum
        {
            set { _hasnum = value; }
            get { return _hasnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WantingNum
        {
            set { _wantingnum = value; }
            get { return _wantingnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WaitingDays
        {
            set { _waitingdays = value; }
            get { return _waitingdays; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndRequireDate
        {
            set { _endrequiredate = value; }
            get { return _endrequiredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Purpose
        {
            set { _purpose = value; }
            get { return _purpose; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderCount
        {
            set { _ordercount = value; }
            get { return _ordercount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateCondition
        {
            set { _createcondition = value; }
            get { return _createcondition; }
        }
        #endregion Model
    }
}
