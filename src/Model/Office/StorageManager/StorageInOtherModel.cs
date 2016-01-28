using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageInOtherModel
    {
        public StorageInOtherModel()
        { }
        #region Model
        private string _id;
        private string _companycd;
        private string _inno;
        private string _title;
        private string _reasontype;
        private string _deptid;
        private string _othercorpid;
        private string _othercorpname;
        private string _corpbigtype;
        private string _purchaser;
        private string _paytype;
        private string _sendaddress;
        private string _receiveoveraddress;
        private string _currencytype;
        private string _rate;
        private string _taker;
        private string _checker;
        private string _executor;
        private string _enterdate;
        private string _totalprice;
        private string _counttotal;
        private string _summary;
        private string _remark;
        private string _creator;
        private string _createdate;
        private string _billstatus;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _fromtype;
        private string _frombillid;
        //拓展属性
        private string _efindex;
        private string _efdesc;

        private string _canviewuser;
        private string _canviewusername;
        private string _projectid;

        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }

        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EFIndex
        {
            set { _efindex = value; }
            get { return _efindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EFDesc
        {
            set { _efdesc = value; }
            get { return _efdesc; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InNo
        {
            set { _inno = value; }
            get { return _inno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReasonType
        {
            set { _reasontype = value; }
            get { return _reasontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OtherCorpID
        {
            set { _othercorpid = value; }
            get { return _othercorpid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OtherCorpName
        {
            set { _othercorpname = value; }
            get { return _othercorpname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CorpBigType
        {
            set { _corpbigtype = value; }
            get { return _corpbigtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Purchaser
        {
            set { _purchaser = value; }
            get { return _purchaser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendAddress
        {
            set { _sendaddress = value; }
            get { return _sendaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveOverAddress
        {
            set { _receiveoveraddress = value; }
            get { return _receiveoveraddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Taker
        {
            set { _taker = value; }
            get { return _taker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EnterDate
        {
            set { _enterdate = value; }
            get { return _enterdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
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
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
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
        public string Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        #endregion Model

    }
}

