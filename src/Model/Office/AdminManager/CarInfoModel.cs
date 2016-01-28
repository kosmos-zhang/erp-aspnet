/**********************************************
 * 类作用：   CarInfo表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/24
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarInfoModel
    /// 描述：CarInfo表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/24
    /// </summary>
   public class CarInfoModel
   {
        #region CarInfo表数据模板
        private int _id;
        private string _companycd;
        private string _carno;
        private string _carname;
        private string _carmark;
        private string _cartype;
        private string _factory;
        private string _displacement;
        private int _seatcount;
        private decimal _carrying;
        private string _fueltype;
        private string _engineno;
        private decimal _buymoney;
        private DateTime _buydate;
        private int _motorman;
        private int _useddept;
        private string _vendorname;
        private string _vendoraddress;
        private string _contact;
        private string _contacttel;
        private string _status;
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
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CarName
        {
            set { _carname = value; }
            get { return _carname; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarMark
        {
            set { _carmark = value; }
            get { return _carmark; }
        }
        /// <summary>
        /// 车辆类别（1小客，2大客，3小货，4大货，5其他）
        /// </summary>
        public string CarType
        {
            set { _cartype = value; }
            get { return _cartype; }
        }
        /// <summary>
        /// 车辆型号
        /// </summary>
        public string Factory
        {
            set { _factory = value; }
            get { return _factory; }
        }
        /// <summary>
        /// 排量（升）
        /// </summary>
        public string Displacement
        {
            set { _displacement = value; }
            get { return _displacement; }
        }
        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount
        {
            set { _seatcount = value; }
            get { return _seatcount; }
        }
        /// <summary>
        /// 载重（吨）
        /// </summary>
        public decimal Carrying
        {
            set { _carrying = value; }
            get { return _carrying; }
        }
        /// <summary>
        /// 燃油类型（1汽油，2柴油，3天然气，4混合）
        /// </summary>
        public string FuelType
        {
            set { _fueltype = value; }
            get { return _fueltype; }
        }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNo
        {
            set { _engineno = value; }
            get { return _engineno; }
        }
        /// <summary>
        /// 购买金额(元)
        /// </summary>
        public decimal BuyMoney
        {
            set { _buymoney = value; }
            get { return _buymoney; }
        }
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime BuyDate
        {
            set { _buydate = value; }
            get { return _buydate; }
        }
        /// <summary>
        /// 驾驶员ID
        /// </summary>
        public int Motorman
        {
            set { _motorman = value; }
            get { return _motorman; }
        }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int UsedDept
        {
            set { _useddept = value; }
            get { return _useddept; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName
        {
            set { _vendorname = value; }
            get { return _vendorname; }
        }
        /// <summary>
        /// 供应商地址
        /// </summary>
        public string VendorAddress
        {
            set { _vendoraddress = value; }
            get { return _vendoraddress; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            set { _contact = value; }
            get { return _contact; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }
        /// <summary>
        /// 车辆状态（1正常，2停用）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 登记日期
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
        /// 最后更新日期
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
