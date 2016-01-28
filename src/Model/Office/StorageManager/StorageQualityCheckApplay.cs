using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageQualityCheckApplay
    {
        public StorageQualityCheckApplay()
        { }


        #region Model
        private int _id;
        public int ID
        { get { return _id; } set { _id = value; } }
        private string _companyCD;
        public string CompanyCD
        { get { return _companyCD; } set { _companyCD = value; } }
        private string _applyNO;
        public string ApplyNO
        { get { return _applyNO; } set { _applyNO = value; } }
        private string _title;
        public string Title
        { get { return _title; } set { _title = value; } }
        private string _fromType;
        public string FromType
        { get { return _fromType; } set { _fromType = value; } }

        private int _custID;
        public int CustID
        { get { return _custID; } set { _custID = value; } }
        private string _custBigType;
        public string CustBigType
        { get { return _custBigType; } set { _custBigType = value; } }
        private string _custName;
        public string CustName
        { get { return _custName; } set { _custName = value; } }
        private int _principal;
        public int Principal
        { get { return _principal; } set { _principal = value; } }
        private int _deptID;
        public int DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        private string _checkType;
        public string CheckType
        { get { return _checkType; } set { _checkType = value; } }
        private string _checkMode;
        public string CheckMode
        { get { return _checkMode; } set { _checkMode = value; } }
        private string _checker;
        public string Checker
        { get { return _checker; } set { _checker = value; } }
        private string _checkDeptId;
        public string CheckDeptID
        { get { return _checkDeptId; } set { _checkDeptId = value; } }
        private DateTime _checkDate;
        public DateTime CheckDate
        { get { return _checkDate; } set { _checkDate = value; } }
        private string _remark;
        public string Remark 
        {
            get { return _remark;}
            set{_remark=value;}
        }
        private string _attachment;
        public string Attachment
        { get { return _attachment; } set { _attachment = value; } }
        private int _creator;
        public int Creater
        { get { return _creator; } set { _creator = value; } }
        private DateTime _createDate;
        public DateTime CreateDate
        { get { return _createDate; } set { _createDate = value; } }
        private string _billStatus;
        public string BillStatus
        { get { return _billStatus; } set { _billStatus = value; } }
        private int _confirmor;
        public int Confirmor
        { get { return _confirmor; } set { _confirmor = value; } }
        private DateTime _confirmDate;
        public DateTime ConfirmDate
        { get { return _confirmDate; } set { _confirmDate = value; } }
        private int _closer;
        public int Closer
        { get { return _closer; } set { _closer = value; } }
        private DateTime _closeDate;
        public DateTime CloseDate
        { get { return _closeDate; } set { _closeDate = value; } }
        private DateTime _mdodifiedDate;
        public DateTime MdodifiedDate
        { get { return _mdodifiedDate; } set { _mdodifiedDate = value; } }
        private string _modifiedUserID;
        public string ModifiedUserID
        { get { return _modifiedUserID; } set { _modifiedUserID = value; } }
        private decimal _countTotal;
        public decimal CountTotal
        { get { return _countTotal; } set { _countTotal = value; } }
        #endregion

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
