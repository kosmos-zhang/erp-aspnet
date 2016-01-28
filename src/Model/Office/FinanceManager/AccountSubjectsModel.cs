/**********************************************
 * 类作用：   AccountSubjects表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/03/09
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
    public class AccountSubjectsModel
    {
        private string _companycd;
        private int _id;
        private string _subjectscd;
        private string _subjectsname;
        private string _parentid;
        private string _subjectstype;
        private int _type;
        private string _blancedire;
        private DateTime _createdate;
        private decimal _yinitialvalue;
        private decimal _ytotaldebit;
        private int _auciliarycd;
        private decimal _ytotallenders;
        private decimal beginmoney;
        private int _forcurrencyacc;
        private string _usedstatus;
        private string _remark;
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 科目代码
        /// </summary>
        public string SubjectsCD
        {
            set { _subjectscd = value; }
            get { return _subjectscd; }
        }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string SubjectsName
        {
            set { _subjectsname = value; }
            get { return _subjectsname; }
        }
        /// <summary>
        /// 上级科目编码
        /// </summary>
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 科目所属一级科目类型
        /// </summary>
        public string SubjectsType
        {
            set { _subjectstype = value; }
            get { return _subjectstype; }
        }
        /// <summary>
        /// 科目类型
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string BlanceDire
        {
            set { _blancedire = value; }
            get { return _blancedire; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 年初始值
        /// </summary>
        public decimal YInitialValue
        {
            set { _yinitialvalue = value; }
            get { return _yinitialvalue; }
        }
        /// <summary>
        /// 本年累计借方
        /// </summary>
        public decimal YTotalDebit
        {
            set { _ytotaldebit = value; }
            get { return _ytotaldebit; }
        }
        /// <summary>
        /// 辅助核算
        /// </summary>
        public int AuciliaryCD
        {
            set { _auciliarycd = value; }
            get { return _auciliarycd; }
        }
        /// <summary>
        /// 本年累计贷方
        /// </summary>
        public decimal YTotalLenders
        {

            set { _ytotallenders = value; }
            get { return _ytotallenders; }
        }
        /// <summary>
        /// 期初余额
        /// </summary>
        public decimal BeginMoney
        {
            set { beginmoney = value; }
            get { return beginmoney; }
        }
        /// <summary>
        /// 外币核算
        /// </summary>
        public int ForCurrencyAcc
        {
            set { _forcurrencyacc = value; }
            get { return _forcurrencyacc; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
