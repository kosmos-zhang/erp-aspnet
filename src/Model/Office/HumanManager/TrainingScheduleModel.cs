/**********************************************
 * 类作用：   TrainingSchedule表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/02
 ***********************************************/
using System;


namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TrainingScheduleModel
    /// 描述：TrainingSchedule表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///
    public class TrainingScheduleModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _trainingno;
        private string _scheduledate;
        private string _abstract;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
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
        /// 培训编号（对应培训表中的培训编号）
        /// </summary>
        public string TrainingNo
        {
            set { _trainingno = value; }
            get { return _trainingno; }
        }
        /// <summary>
        /// 进度时间
        /// </summary>
        public string ScheduleDate
        {
            set { _scheduledate = value; }
            get { return _scheduledate; }
        }
        /// <summary>
        /// 内容摘要
        /// </summary>
        public string Abstract
        {
            set { _abstract = value; }
            get { return _abstract; }
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
