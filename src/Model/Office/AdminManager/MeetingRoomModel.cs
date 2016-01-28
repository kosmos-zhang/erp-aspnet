/**********************************************
 * 类作用：   officedba.MeetingRoom表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/04/27
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    public class MeetingRoomModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _roomname;
        private string _place;
        private int _personcount;
        private string _multimediaflag;
        private string _remark;
        private string _usedstatus;
        private DateTime? _modifieddate;
        private string _modifieduserid;
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
        /// 会议室名称
        /// </summary>
        public string RoomName
        {
            set { _roomname = value; }
            get { return _roomname; }
        }
        /// <summary>
        /// 会议室位置
        /// </summary>
        public string Place
        {
            set { _place = value; }
            get { return _place; }
        }
        /// <summary>
        /// 可容纳人数
        /// </summary>
        public int PersonCount
        {
            set { _personcount = value; }
            get { return _personcount; }
        }
        /// <summary>
        /// 是否多功能(0 否，1 是)
        /// </summary>
        public string MultimediaFlag
        {
            set { _multimediaflag = value; }
            get { return _multimediaflag; }
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
        /// 启用状态(0 停用，1 启用)
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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
