/**********************************************
 * 类作用：   CustComplain表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/03
 ***********************************************/

using System;

namespace XBase.Model.Office.CustManager
{
    public class CustComplainModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _custid;
        private int _custlinkman;
        private string _custlinktel;
        private string _complainno;
        private string _title;
        private int _complaintype;
        private int _destclerk;
        private DateTime? _complaindate;
        private string _critical;
        private string _state;
        private string _dateunit;
        private decimal _spendtime;
        private string _contents;
        private int _complainedman;
        private string _disposalprocess;
        private string _feedback;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _canviewuser;
        private string _canviewusername;

        /// <summary>
        /// 可查看该客户档案的人员ID
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该客户档案的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 投诉客户
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public int CustLinkMan
        {
            set { _custlinkman = value; }
            get { return _custlinkman; }
        }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string CustLinkTel
        {
            set { _custlinktel = value; }
            get { return _custlinktel; }
        }
        /// <summary>
        /// 投诉单编号
        /// </summary>
        public string ComplainNo
        {
            set { _complainno = value; }
            get { return _complainno; }
        }
        /// <summary>
        /// 投诉主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 投诉分类
        /// </summary>
        public int ComplainType
        {
            set { _complaintype = value; }
            get { return _complaintype; }
        }
        /// <summary>
        /// 接待人
        /// </summary>
        public int DestClerk
        {
            set { _destclerk = value; }
            get { return _destclerk; }
        }
        /// <summary>
        /// 投诉时间
        /// </summary>
        public DateTime? ComplainDate
        {
            set { _complaindate = value; }
            get { return _complaindate; }
        }
        /// <summary>
        /// 紧急程度(1宽松,2一般,3较急,4紧急,5特急)
        /// </summary>
        public string Critical
        {
            set { _critical = value; }
            get { return _critical; }
        }
        /// <summary>
        /// 状态（1处理中2未处理3已处理）
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 花费时间单位（小时/天/月）
        /// </summary>
        public string DateUnit
        {
            set { _dateunit = value; }
            get { return _dateunit; }
        }
        /// <summary>
        /// 花费时间
        /// </summary>
        public decimal SpendTime
        {
            set { _spendtime = value; }
            get { return _spendtime; }
        }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        public int ComplainedMan
        {
            set { _complainedman = value; }
            get { return _complainedman; }
        }
        /// <summary>
        /// 处理过程
        /// </summary>
        public string DisposalProcess
        {
            set { _disposalprocess = value; }
            get { return _disposalprocess; }
        }
        /// <summary>
        /// 客户反馈
        /// </summary>
        public string Feedback
        {
            set { _feedback = value; }
            get { return _feedback; }
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
