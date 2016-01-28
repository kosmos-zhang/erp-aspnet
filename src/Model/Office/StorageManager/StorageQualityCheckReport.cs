using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageQualityCheckReportModel
    {
        public StorageQualityCheckReportModel()
        { }
        private int _id;
        private string _companycd;
        private string _reportno;
        private string _title;
        private string _fromtype;
        private int _reportid;
        private int _fromlineno;
        private string _checktype;
        private string _checkmode;
        private int _productid;
        private int _applyuserid;
        private int _applydeptid;
        private int _checker;
        private int _checdeptid;
        private DateTime _checkdate;
        private string _checkcontent;
        private string _checkstandard;
        private decimal _checknum;
        private decimal _samplenum;
        //private decimal _samplebadnum;
        //private decimal _samplepassnum;
        private decimal _passnum;
        private decimal _passpercent;
        private decimal _nopass;
        private string _checkresult;
        private string _ispass;
        private string _isrecheck;
        private string _remark;
        private string _attachment;
        private int _creator;
        private DateTime _createdate;
        private string _billstatus;
        private int _confirmor;
        private DateTime _confirmdate;
        private int _closer;
        private DateTime _closedate;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _FromReportNo;
        private string _ReportName;
        private int _OtherCorpID;
        private string _OtherCorpNo;
        private string _OtherCorpName;
        private string _CorpBigType;
        private int _FromDetailID;
        private int _DeptID;
        private int _Principal;
        /// <summary>
        /// 
        /// </summary>
        /// 
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int Principal
        {
            get { return _Principal; }
            set { _Principal = value; }
        }
        public int Dept
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }
        public int FromDetailID
        { get { return _FromDetailID; } set { _FromDetailID = value; } }
        public string OtherCorpName
        { get { return _OtherCorpName; } set { _OtherCorpName = value; } }
        public string CorpBigType
        { get { return _CorpBigType; } set { _CorpBigType = value; } }
        public string OtherCorpNo
        {
            get { return _OtherCorpNo; }
            set { _OtherCorpNo = value; }
        }
        public int OtherCorpID
        {
            get { return _OtherCorpID; }
            set { _OtherCorpID = value; }
        }
        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
    }

        public string FromReportNo
        {
            get { return _FromReportNo; }
            set { _FromReportNo = value; }
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
        public string ReportNo
        {
            set { _reportno = value; }
            get { return _reportno; }
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
        public string FromType
        {
            set { _fromtype = value; }
            get { return _fromtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReportID
        {
            set { _reportid = value; }
            get { return _reportid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FromLineNo
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckType
        {
            set { _checktype = value; }
            get { return _checktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckMode
        {
            set { _checkmode = value; }
            get { return _checkmode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ApplyUserID
        {
            set { _applyuserid = value; }
            get { return _applyuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ApplyDeptID
        {
            set { _applydeptid = value; }
            get { return _applydeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckDeptId
        {
            set { _checdeptid = value; }
            get { return _checdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckContent
        {
            set { _checkcontent = value; }
            get { return _checkcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckStandard
        {
            set { _checkstandard = value; }
            get { return _checkstandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CheckNum
        {
            set { _checknum = value; }
            get { return _checknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SampleNum
        {
            set { _samplenum = value; }
            get { return _samplenum; }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public decimal SampleBadNum
        //{
        //    set { _samplebadnum = value; }
        //    get { return _samplebadnum; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public decimal SamplePassNum
        //{
        //    set { _samplepassnum = value; }
        //    get { return _samplepassnum; }
        //}
        /// <summary>
        /// 
        /// </summary>
        public decimal PassNum
        {
            set { _passnum = value; }
            get { return _passnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PassPercent
        {
            set { _passpercent = value; }
            get { return _passpercent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal NoPass
        {
            set { _nopass = value; }
            get { return _nopass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isRecheck
        {
            set { _isrecheck = value; }
            get { return _isrecheck; }
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
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
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
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate
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

        #region 扩展属性

        private string extField1 = String.Empty; //扩展属性1
        private string extField2 = String.Empty; //扩展属性2
        private string extField3 = String.Empty; //扩展属性3
        private string extField4 = String.Empty; //扩展属性4
        private string extField5 = String.Empty; //扩展属性5
        private string extField6 = String.Empty; //扩展属性6
        private string extField7 = String.Empty; //扩展属性7
        private string extField8 = String.Empty; //扩展属性8
        private string extField9 = String.Empty; //扩展属性9
        private string extField10 = String.Empty; //扩展属性10

        /// <summary>
        /// 扩展属性1
        /// </summary>
        public string ExtField1
        {
            get
            {
                return extField1;
            }
            set
            {
                extField1 = value;
            }
        }

        /// <summary>
        /// 扩展属性2
        /// </summary>
        public string ExtField2
        {
            get
            {
                return extField2;
            }
            set
            {
                extField2 = value;
            }
        }

        /// <summary>
        /// 扩展属性3
        /// </summary>
        public string ExtField3
        {
            get
            {
                return extField3;
            }
            set
            {
                extField3 = value;
            }
        }

        /// <summary>
        /// 扩展属性4
        /// </summary>
        public string ExtField4
        {
            get
            {
                return extField4;
            }
            set
            {
                extField4 = value;
            }
        }

        /// <summary>
        /// 扩展属性5
        /// </summary>
        public string ExtField5
        {
            get
            {
                return extField5;
            }
            set
            {
                extField5 = value;
            }
        }

        /// <summary>
        /// 扩展属性6
        /// </summary>
        public string ExtField6
        {
            get
            {
                return extField6;
            }
            set
            {
                extField6 = value;
            }
        }

        /// <summary>
        /// 扩展属性7
        /// </summary>
        public string ExtField7
        {
            get
            {
                return extField7;
            }
            set
            {
                extField7 = value;
            }
        }

        /// <summary>
        /// 扩展属性8
        /// </summary>
        public string ExtField8
        {
            get
            {
                return extField8;
            }
            set
            {
                extField8 = value;
            }
        }

        /// <summary>
        /// 扩展属性9
        /// </summary>
        public string ExtField9
        {
            get
            {
                return extField9;
            }
            set
            {
                extField9 = value;
            }
        }

        /// <summary>
        /// 扩展属性10
        /// </summary>
        public string ExtField10
        {
            get
            {
                return extField10;
            }
            set
            {
                extField10 = value;
            }
        }
        #endregion


    }
}
