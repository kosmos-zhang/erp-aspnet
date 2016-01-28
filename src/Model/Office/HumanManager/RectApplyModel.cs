/**********************************************
 * 类作用：   RectApply表数据模板
 * 建立人：   王保军
 * 建立时间： 2009/06/22
 ***********************************************/
using System;
using System.Collections;
namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectApplyModel
    /// 描述：RectApply表数据模板
    /// 
    /// 作者：王保军
    /// 创建时间： 2009/06/22
    /// 最后修改时间： 2009/06/22
    /// </summary>
    ///
    public class RectApplyModel
    {

        #region Model
        private string _id;
        private string _rectapplyno;
        private string _companycd;
        private string _deptid;
        private string _maxnum;
        private string _nownum;
        private string _jobname;
        private string _rectcount;
        private string _workplace;
        private string _worknature;
        private string _useddate;
        private string _principal;
        private string _requstreason;
        private string _maxage;
        private string _minage;
        private string _sex;
        private string _workage;
        private string _culturelevel;
        private string _professional;
        private string _workneeds;
        private string _otherability;
        private string _remark;
        private string _billstatus;
        private string _creator;
        private string _createdate;
        private string _confirmor;
        private string _confirmdate;
        private string _closer;
        private string _closedate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _flowStatusName;
        private string _flowStatusID;
        private string _jobDescripe;

        private string _editFlag;


        private string _principalName;
      
        private string _jobID;
        private ArrayList _goalList;
        private string _requireNum;
        /// <summary>
        ///  需求人数
        /// </summary>  
        public string RequireNum
        {
            set { _requireNum = value; }
            get { return _requireNum; }
        }
        /// <summary>
        /// 人员需求
        /// </summary>
        public ArrayList GoalList
        {
            set { _goalList = value; }
            get
            {
                if (_goalList == null) _goalList = new ArrayList();
                return _goalList;
            }
        }
        /// <summary>
        ///  岗位ID
        /// </summary>  
        public string JobID
        {
            set { _jobID = value; }
            get { return _jobID; }
        }
    

        public string PrincipalName
        {
            set { _principalName = value; }
            get { return _principalName; }
        }
              public string EditFlag
        {
            set { _editFlag = value; }
            get { return _editFlag; }
        }


        private bool _isSuccess;
        public bool  IsSuccess
        {
            set { _isSuccess = value; }
            get { return _isSuccess; }
        }
        /// <summary>
        /// 职务说明
        /// </summary>
        public string JobDescripe
        {
            set { _jobDescripe = value; }
            get { return _jobDescripe; }
        }
        public string  FlowStatusID
        {
            set { _flowStatusID = value; }
            get { return _flowStatusID; }
        }
        public string FlowStatusName
        {
            set { _flowStatusName = value; }
            get { return _flowStatusName; }
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
        /// 申请编号
        /// </summary>
        public string RectApplyNo
        {
            set { _rectapplyno = value; }
            get { return _rectapplyno; }
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
        /// 申请部门
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 部门编制（人）
        /// </summary>
        public string MaxNum
        {
            set { _maxnum = value; }
            get { return _maxnum; }
        }
        /// <summary>
        /// 现有人数（人）
        /// </summary>
        public string NowNum
        {
            set { _nownum = value; }
            get { return _nownum; }
        }
        /// <summary>
        /// 招聘岗位（多个逗号分隔
        /// </summary>
        public string JobName
        {
            set { _jobname = value; }
            get { return _jobname; }
        }
        /// <summary>
        /// 招聘人数
        /// </summary>
        public string RectCount
        {
            set { _rectcount = value; }
            get { return _rectcount; }
        }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string WorkPlace
        {
            set { _workplace = value; }
            get { return _workplace; }
        }
        /// <summary>
        /// 工作性质
        /// </summary>
        public string WorkNature
        {
            set { _worknature = value; }
            get { return _worknature; }
        }
        /// <summary>
        /// 报到日期
        /// </summary>
        public string UsedDate
        {
            set { _useddate = value; }
            get { return _useddate; }
        }
        /// <summary>
        /// 经办人(对应员工表ID)
        /// </summary>
        public string Principal
        {
            set { _principal = value; }
            get { return _principal; }
        }
        /// <summary>
        /// 经办人(对应员工表ID)
        /// </summary>
        public string RequstReason
        {
            set { _requstreason = value; }
            get { return _requstreason; }
        }
        /// <summary>
        /// 截止年龄（岁）
        /// </summary>
        public string MaxAge
        {
            set { _maxage = value; }
            get { return _maxage; }
        }
        /// <summary>
        /// 截止年龄（岁）
        /// </summary>
        public string MinAge
        {
            set { _minage = value; }
            get { return _minage; }
        }
        /// <summary>
        /// 性别(1 男，2 女)
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 工作年限（1在读学生,2应届毕业生,3 一年以内,4一年以上,5三年以上,6五年以上,7十年以上,8二十年以上,9退休人员）
        /// </summary>
        public string WorkAge
        {
            set { _workage = value; }
            get { return _workage; }
        }
        /// <summary>
        /// 学历ID(对应分类代码表ID)
        /// </summary>
        public string CultureLevel
        {
            set { _culturelevel = value; }
            get { return _culturelevel; }
        }
        /// <summary>
        /// 专业ID(对应分类代码表ID)
        /// </summary>
        public string Professional
        {
            set { _professional = value; }
            get { return _professional; }
        }
        /// <summary>
        /// 工作要求
        /// </summary>
        public string WorkNeeds
        {
            set { _workneeds = value; }
            get { return _workneeds; }
        }
        /// <summary>
        /// 其他能力
        /// </summary>
        public string OtherAbility
        {
            set { _otherability = value; }
            get { return _otherability; }
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
        /// 单据状态（1制单，2执行，4手工结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人ID
        /// </summary>
        public string Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public string CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
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
