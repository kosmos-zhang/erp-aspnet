/**********************************************
 * 类作用：   RectCheckTemplate表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/15
 ***********************************************/
using System;
using System.Collections;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectCheckTemplateModel
    /// 描述：RectCheckTemplate表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class RectCheckTemplateModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _templateno;
        private string _title;
        private string _quarterid;
        private string _quartername;
        private string _usedstatus;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        private ArrayList _elemlist;
        /// <summary>
        /// 编辑标识
        /// </summary>
        public ArrayList ElemList
        {
            get
            {
                if (_elemlist == null) _elemlist = new ArrayList();
                return _elemlist;
            }
            set
            {
                _elemlist = value;
            }
        }
        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            get
            {
                return _editflag;
            }
            set
            {
                _editflag = value;
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
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 评测模板编号
        /// </summary>
        public string TemplateNo
        {
            set { _templateno = value; }
            get { return _templateno; }
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
        /// 岗位ID（对应岗位表ID） 
        /// </summary>
        public string QuarterID
        {
            set { _quarterid = value; }
            get { return _quarterid; }
        }
        /// <summary>
        /// 岗位ID（对应岗位表ID） 
        /// </summary>
        public string QuarterName
        {
            set { _quartername = value; }
            get { return _quartername; }
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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
        /// 最后更新时间
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
