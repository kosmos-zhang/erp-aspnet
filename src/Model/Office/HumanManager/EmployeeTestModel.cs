/**********************************************
 * 类作用：   EmployeeTest表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
 ***********************************************/
using System;
using System.Collections;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeTestModel
    /// 描述：EmployeeTest表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    public class EmployeeTestModel
    {

        #region Model
        private string _id;
        private string _companycd;
        private string _testno;
        private string _title;
        private string _teacher;
        private string _startdate;
        private string _enddate;
        private string _addr;
        private string _content;
        private string _testresult;
        private string _remark;
        private string _status;
        private string _attachment;
        private string _pageattachment;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        private string _absencecount;
        private string _attachmentname;
        private ArrayList _scorelist;
        /// <summary>
        /// 得分列表
        /// </summary>
        public ArrayList ScoreList
        {
            set { _scorelist = value; }
            get
            {
                if (_scorelist == null) _scorelist = new ArrayList();
                return _scorelist; 
            }
        }
        /// <summary>
        /// 编辑模式
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
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
        /// 考试编号
        /// </summary>
        public string TestNo
        {
            set { _testno = value; }
            get { return _testno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 考试负责人ID(对应员工表ID)
        /// </summary>
        public string Teacher
        {
            set { _teacher = value; }
            get { return _teacher; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 考试地点
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 考试内容摘要
        /// </summary>
        public string TestContent
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 考试结果
        /// </summary>
        public string TestResult
        {
            set { _testresult = value; }
            get { return _testresult; }
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
        /// 考试状态(0 未开始，1 已结束)

        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 附件文件名称
        /// </summary>
        public string AttachmentName
        {
            set { _attachmentname = value; }
            get { return _attachmentname; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string PageAttachment
        {
            set { _pageattachment = value; }
            get { return _pageattachment; }
        }
        /// <summary>
        /// 缺考人数
        /// </summary>
        public string AbsenceCount
        {
            set { _absencecount = value; }
            get { return _absencecount; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}
