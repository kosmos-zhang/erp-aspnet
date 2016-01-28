/**********************************************
 * 类作用：   TrainingAsse表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/05
 ***********************************************/
using System;
using System.Collections;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TrainingAsseModel
    /// 描述：TrainingAsse表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/05
    /// 最后修改时间：2009/04/05
    /// </summary>
    ///
    public class TrainingAsseModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _trainingno;
        private string _asseno;
        private string _checkperson;
        private string _trainingplan;
        private string _filluser;
        private string _leadviews;
        private string _description;
        private string _checkway;
        private string _checkdate;
        private string _generalcomment;
        private string _checkremark;
        private string _modifieddate;
        private string _modifieduserid;
        private ArrayList _resultlist;
        private string _editflag;
        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 考评结果
        /// </summary>
        public ArrayList ResultList
        {
            set { _resultlist = value; }
            get 
            {
                if (_resultlist == null) _resultlist = new ArrayList();
                return _resultlist; 
            }
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
        /// 考核编号
        /// </summary>
        public string AsseNo
        {
            set { _asseno = value; }
            get { return _asseno; }
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
        /// 培训编号（对应培训表中的培训编号）
        /// </summary>
        public string TrainingNo
        {
            set { _trainingno = value; }
            get { return _trainingno; }
        }
        /// <summary>
        /// 考核人
        /// </summary>
        public string CheckPerson
        {
            set { _checkperson = value; }
            get { return _checkperson; }
        }
        /// <summary>
        /// 培训规划
        /// </summary>
        public string TrainingPlan
        {
            set { _trainingplan = value; }
            get { return _trainingplan; }
        }
        /// <summary>
        /// 填写人
        /// </summary>
        public string FillUser
        {
            set { _filluser = value; }
            get { return _filluser; }
        }
        /// <summary>
        /// 领导意见
        /// </summary>
        public string LeadViews
        {
            set { _leadviews = value; }
            get { return _leadviews; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 考核方式
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 考核时间
        /// </summary>
        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 考核总评
        /// </summary>
        public string GeneralComment
        {
            set { _generalcomment = value; }
            get { return _generalcomment; }
        }
        /// <summary>
        /// 考核备注
        /// </summary>
        public string CheckRemark
        {
            set { _checkremark = value; }
            get { return _checkremark; }
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
