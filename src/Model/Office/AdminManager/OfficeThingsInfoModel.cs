/**********************************************
 * 类作用：   OfficeThingsInfo表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/07
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsInfoModel
    /// 描述：OfficeThingsInfo表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/07
    /// </summary>
   public class OfficeThingsInfoModel
   {
        #region OfficeThingsInfo表数据模板
        private int _id;
        private string _companycd;
        private string _thingno;
        private string _thingname;
        private int _typeid;
        private string _thingtype;
        private int _unitid;
        private int _mincount;
        private int _creator;
        private DateTime _createdate;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
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
        /// 用品编号
        /// </summary>
        public string ThingNo
        {
            set { _thingno = value; }
            get { return _thingno; }
        }
        /// <summary>
        /// 用品名称
        /// </summary>
        public string ThingName
        {
            set { _thingname = value; }
            get { return _thingname; }
        }
        /// <summary>
        /// 用品分类(对应设备用品分类表ID)
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string ThingType
        {
            set { _thingtype = value; }
            get { return _thingtype; }
        }
        /// <summary>
        /// 单位(对应分类代码表，设备用品计量单位)
        /// </summary>
        public int UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 报警下限
        /// </summary>
        public int MinCount
        {
            set { _mincount = value; }
            get { return _mincount; }
        }
        /// <summary>
        /// 建档人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
