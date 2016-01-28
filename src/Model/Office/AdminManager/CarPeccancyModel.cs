/**********************************************
 * 类作用：   CarPeccancy表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/04
 ***********************************************/
using System;


namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarPeccancyModel
    /// 描述：CarPeccancy表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarPeccancyModel
   {
        #region CarPeccancy表数据模板
        private int _id;
        private string _companycd;
        private string _carno;
        private int _employeeid;
        private DateTime _happendate;
        private string _happenplace;
        private string _description;
        private string _party;
        private decimal _fee;
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
        /// 车辆编号（对应车辆信息表编号）
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 驾驶员（对应员工表ID）
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenDate
        {
            set { _happendate = value; }
            get { return _happendate; }
        }
        /// <summary>
        /// 发生地点
        /// </summary>
        public string HappenPlace
        {
            set { _happenplace = value; }
            get { return _happenplace; }
        }
        /// <summary>
        /// 情况说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 同行人员
        /// </summary>
        public string Party
        {
            set { _party = value; }
            get { return _party; }
        }
        /// <summary>
        /// 处罚金额（元）
        /// </summary>
        public decimal Fee
        {
            set { _fee = value; }
            get { return _fee; }
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
