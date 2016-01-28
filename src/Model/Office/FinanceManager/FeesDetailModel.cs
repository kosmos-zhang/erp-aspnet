using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.FinanceManager
{
    public class FeesDetailModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _feesno;
        private string _sortno;
        private string _feeid;
        private decimal _feetotal;
        private string _uses;
        private DateTime? _occurtime;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _subjectsno;
        private string _subjectsname;

        /// <summary>
        /// 费用科目编码
        /// </summary>
        public string SubjectsNo
        {
            set { _subjectsno = value; }
            get { return _subjectsno; }
        }
        /// <summary>
        /// 费用科目名称
        /// </summary>
        public string SubjectsName
        {
            set { _subjectsname = value; }
            get { return _subjectsname; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 费用主表单据编号
        /// </summary>
        public string FeesNo
        {
            set { _feesno = value; }
            get { return _feesno; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 费用类别ID（对应费用代码表ID）
        /// </summary>
        public string FeeID
        {
            set { _feeid = value; }
            get { return _feeid; }
        }
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal FeeTotal
        {
            set { _feetotal = value; }
            get { return _feetotal; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string Uses
        {
            set { _uses = value; }
            get { return _uses; }
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime? OccurTime
        {
            set { _occurtime = value; }
            get { return _occurtime; }
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
        public DateTime? ModifiedDate
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
