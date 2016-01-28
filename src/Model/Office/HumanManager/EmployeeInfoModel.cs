/**********************************************
 * 类作用：   EmployeeInfo表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/09
 ***********************************************/
using System;
using System.Collections;
using System.Data;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeInfoModel
    /// 描述：EmployeeInfo表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/09
    /// 最后修改时间：2009/03/09
    /// </summary>
    ///
    public class EmployeeInfoModel
    {

        #region Model
        private int _id;
        private string _employeecd;
        private string _employeeno;
        private string _pyshort;
        private string _companycd;
        private string _cardid;
        private string _safeguardcard;
        private string _employeename;
        private string _usedname;
        private string _nameen;
        private string _sex;
        private string _birth;
        private string _account;
        private string _accountnature;
        private string _countryid;
        private string _height;
        private string _weight;
        private string _sight;
        private string _degree;
        private string _positionid;
        private string _docutype;
        private string _createuserid;
        private string _createusername;
        private DateTime _createdate;
        private string _national;
        private string _marriagestatus;
        private string _origin;
        private string _landscape;
        private string _religion;
        private string _telephone;
        private string _mobile;
        private string _email;
        private string _othercontact;
        private string _homeaddress;
        private string _healthstatus;
        private string _culturelevel;
        private string _graduateschool;
        private string _professional;
        private string _features;
        private string _computerlevel;
        private string _foreignlanguage1;
        private string _foreignlanguagelevel1;
        private string _foreignlanguage2;
        private string _foreignlanguagelevel2;
        private string _foreignlanguage3;
        private string _foreignlanguagelevel3;
        private string _worktime;
        private string _totalseniority;
        private string _photourl;
        private string _pagephotourl;
        private string _positiontitle;
        private string _flag;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _quarterid;
        private int _deptid;
        private string _deptname;
        private int _adminLevelid;
        private DateTime? _enterdate;
        private ArrayList _skillList;//技能信息列表
        private ArrayList _historyList;//履历信息列表
        private string _editFlag;//编辑标识
        private string _resume;
        private string _pageresume;
        private DataTable _contractInfo;
        private DataTable _historyInfo;
        private DataTable _skillInfo;
        private string _professionaldes;//专业描述

        /// <summary>
        /// 专业描述
        /// </summary>
        public string ProfessionalDes
        {
            get
            {
                return _professionaldes;
            }
            set
            {
                _professionaldes = value;
            }
        }

        /// <summary>
        /// 履历信息
        /// </summary>
        public DataTable HistoryInfo
        {
            get
            {
                return _historyInfo;
            }
            set
            {
                _historyInfo = value;
            }
        }

        /// <summary>
        /// 技能信息
        /// </summary>
        public DataTable SkillInfo
        {
            get
            {
                return _skillInfo;
            }
            set
            {
                _skillInfo = value;
            }
        }

        /// <summary>
        /// 合同信息
        /// </summary>
        public DataTable ContractInfo
        {
            get
            {
                return _contractInfo;
            }
            set
            {
                _contractInfo = value;
            }
        }
        
        /// <summary>
        /// 简历
        /// </summary>
        public string Resume
        {
            get
            {
                return _resume;
            }
            set
            {
                _resume = value;
            }
        }

        /// <summary>
        /// 简历
        /// </summary>
        public string PageResume
        {
            get
            {
                return _pageresume;
            }
            set
            {
                _pageresume = value;
            }
        }

        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            get
            {
                return _editFlag;
            }
            set
            {
                _editFlag = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        public int AdminLevelID
        {
            set { _adminLevelid = value; }
            get { return _adminLevelid; }
        }
        public DateTime? EnterDate
        {
            set { _enterdate = value; }
            get { return _enterdate; }
        }
        /// <summary>
        /// 技能列表
        /// </summary>
        public ArrayList SkillList
        {
            set 
            { 
                _skillList = value; 
            }
            get
            {
                if (_skillList == null)
                    _skillList = new ArrayList();

                return _skillList; 
            }
        }
        /// <summary>
        /// 履历列表
        /// </summary>
        public ArrayList HistoryList
        {
            set
            { 
                _historyList = value; 
            }
            get
            {
                if (_historyList == null)
                    _historyList = new ArrayList();

                return _historyList; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeNum
        {
            set { _employeecd = value; }
            get { return _employeecd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeNo
        {
            set { _employeeno = value; }
            get { return _employeeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
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
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }
        public string SafeguardCard
        {
            set { _safeguardcard = value; }
            get { return _safeguardcard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsedName
        {
            set { _usedname = value; }
            get { return _usedname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NameEn
        {
            set { _nameen = value; }
            get { return _nameen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Birth
        {
            set { _birth = value; }
            get { return _birth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccountNature
        {
            set { _accountnature = value; }
            get { return _accountnature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountryID
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Height
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sight
        {
            set { _sight = value; }
            get { return _sight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Degree
        {
            set { _degree = value; }
            get { return _degree; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PositionID
        {
            set { _positionid = value; }
            get { return _positionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DocuType
        {
            set { _docutype = value; }
            get { return _docutype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUserName
        {
            set { _createusername = value; }
            get { return _createusername; }
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
        public string National
        {
            set { _national = value; }
            get { return _national; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MarriageStatus
        {
            set { _marriagestatus = value; }
            get { return _marriagestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Origin
        {
            set { _origin = value; }
            get { return _origin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Landscape
        {
            set { _landscape = value; }
            get { return _landscape; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Religion
        {
            set { _religion = value; }
            get { return _religion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EMail
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OtherContact
        {
            set { _othercontact = value; }
            get { return _othercontact; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HomeAddress
        {
            set { _homeaddress = value; }
            get { return _homeaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HealthStatus
        {
            set { _healthstatus = value; }
            get { return _healthstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CultureLevel
        {
            set { _culturelevel = value; }
            get { return _culturelevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraduateSchool
        {
            set { _graduateschool = value; }
            get { return _graduateschool; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Professional
        {
            set { _professional = value; }
            get { return _professional; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Features
        {
            set { _features = value; }
            get { return _features; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ComputerLevel
        {
            set { _computerlevel = value; }
            get { return _computerlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForeignLanguage1
        {
            set { _foreignlanguage1 = value; }
            get { return _foreignlanguage1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForeignLanguageLevel1
        {
            set { _foreignlanguagelevel1 = value; }
            get { return _foreignlanguagelevel1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForeignLanguage2
        {
            set { _foreignlanguage2 = value; }
            get { return _foreignlanguage2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForeignLanguageLevel2
        {
            set { _foreignlanguagelevel2 = value; }
            get { return _foreignlanguagelevel2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForeignLanguage3
        {
            set { _foreignlanguage3 = value; }
            get { return _foreignlanguage3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForeignLanguageLevel3
        {
            set { _foreignlanguagelevel3 = value; }
            get { return _foreignlanguagelevel3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkTime
        {
            set { _worktime = value; }
            get { return _worktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalSeniority
        {
            set { _totalseniority = value; }
            get { return _totalseniority; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoURL
        {
            set { _photourl = value; }
            get { return _photourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PagePhotoURL
        {
            set { _pagephotourl = value; }
            get { return _pagephotourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PositionTitle
        {
            set { _positiontitle = value; }
            get { return _positiontitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
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
        /// <summary>
        /// 
        /// </summary>
        public string QuarterID
        {
            set { _quarterid = value; }
            get { return _quarterid; }
        }
        #endregion Model

    }
}
