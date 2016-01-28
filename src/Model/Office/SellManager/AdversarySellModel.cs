using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public  class AdversarySellModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _custno;
        private int? _chanceid;
        private int? _custid;
        private string _project;
        private decimal? _price;
        private string _power;
        private string _advantage;
        private string _disadvantage;
        private string _policy;
        private string _remark;
        private int? _creator;
        private DateTime? _creatdate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// ID，自动生成
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
        /// 竞争对手编号
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>
        /// 对应销售机会
        /// </summary>
        public int? ChanceID
        {
            set { _chanceid = value; }
            get { return _chanceid; }
        }
        /// <summary>
        /// 竞争客户
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 竞争产品/方案
        /// </summary>
        public string Project
        {
            set { _project = value; }
            get { return _project; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 竞争能力
        /// </summary>
        public string Power
        {
            set { _power = value; }
            get { return _power; }
        }
        /// <summary>
        /// 优势
        /// </summary>
        public string Advantage
        {
            set { _advantage = value; }
            get { return _advantage; }
        }
        /// <summary>
        /// 劣势
        /// </summary>
        public string disadvantage
        {
            set { _disadvantage = value; }
            get { return _disadvantage; }
        }
        /// <summary>
        /// 应对策略
        /// </summary>
        public string Policy
        {
            set { _policy = value; }
            get { return _policy; }
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
        /// 创建人ID（对应员工表ID）
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatDate
        {
            set { _creatdate = value; }
            get { return _creatdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
