/**********************************************
 * 类作用：   FixAssetInfo表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/04/03
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
   public class FixAssetInfoModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _fixno;
        private string _fixname;
        private string _fixtype;
        private string _fixspec;
        private string _fixmodel;
        private int _fixnumber;
        private string _unit;
        private decimal _originalvalue;
        private decimal _revaprice;
        private string _usedept;
        private string _storeplace;
        private string _respperson;
        private DateTime _registerdate;
        private decimal _begidiscount;
        private decimal _reduvaluere;
        private decimal _netvalue;
        private string _usedstatus;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _remark;
        /// <summary>
        /// 企业编码
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
        /// 资产编号
        /// </summary>
        public string FixNo
        {
            set { _fixno = value; }
            get { return _fixno; }
        }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string FixName
        {
            set { _fixname = value; }
            get { return _fixname; }
        }
        /// <summary>
        /// 资产类型
        /// </summary>
        public string FixType
        {
            set { _fixtype = value; }
            get { return _fixtype; }
        }
        /// <summary>
        /// 资产规格
        /// </summary>
        public string FixSpec
        {
            set { _fixspec = value; }
            get { return _fixspec; }
        }
        /// <summary>
        /// 资产型号
        /// </summary>
        public string FixModel
        {
            set { _fixmodel = value; }
            get { return _fixmodel; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int FixNumber
        {
            set { _fixnumber = value; }
            get { return _fixnumber; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 原值
        /// </summary>
        public decimal OriginalValue
        {
            set { _originalvalue = value; }
            get { return _originalvalue; }
        }
        /// <summary>
        /// 重估价
        /// </summary>
        public decimal RevaPrice
        {
            set { _revaprice = value; }
            get { return _revaprice; }
        }
        /// <summary>
        /// 使用部门
        /// </summary>
        public string UseDept
        {
            set { _usedept = value; }
            get { return _usedept; }
        }
        /// <summary>
        /// 存放地点
        /// </summary>
        public string StorePlace
        {
            set { _storeplace = value; }
            get { return _storeplace; }
        }
        /// <summary>
        /// 责任人
        /// </summary>
        public string RespPerson
        {
            set { _respperson = value; }
            get { return _respperson; }
        }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime RegisterDate
        {
            set { _registerdate = value; }
            get { return _registerdate; }
        }
        /// <summary>
        /// 期初累计折扣
        /// </summary>
        public decimal BegiDisCount
        {
            set { _begidiscount = value; }
            get { return _begidiscount; }
        }
        /// <summary>
        /// 期初减值准备
        /// </summary>
        public decimal ReduValueRe
        {
            set { _reduvaluere = value; }
            get { return _reduvaluere; }
        }
        /// <summary>
        /// 净值
        /// </summary>
        public decimal NetValue
        {
            set { _netvalue = value; }
            get { return _netvalue; }
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
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
