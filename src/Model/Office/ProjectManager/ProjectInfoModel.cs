using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProjectManager
{
   public class ProjectInfoModel
    {
        #region 项目信息实体
        private int _id;
        private string _companycd;
        private string _projectno;
        private string _projectname;
        private int _custid;
        private string _custlinkman;
        private string _linktel;
        private int _linkman;
        private string _tel;
        private string _address;
        private DateTime _startdate;
        private DateTime _enddate;
        private string _overview;
        private string _remark;
        private int _creator;
        private DateTime _createdate;
        private string _canviewuser;
        private string _canviewusername;
        private decimal _investment; 
        private string _extfield1;
        private string _extfield2;
        private string _extfield3;
        private string _extfield4;
        private string _extfield5;
        private string _extfield6;
        private string _extfield7;
        private string _extfield8;
        private string _extfield9;
        private string _extfield10;
        private string _extfield11;
        private string _extfield12;
        private string _extfield13;
        private string _extfield14;
        private string _extfield15;
        private string _extfield16;
        private string _extfield17;
        private string _extfield18;
        private string _extfield19;
        private string _extfield20;
        /// <summary>
        /// 主键
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
        /// 项目编号
        /// </summary>
        public string ProjectNo
        {
            set { _projectno = value; }
            get { return _projectno; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 客户
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户方负责人
        /// </summary>
        public string CustLinkMan
        {
            set { _custlinkman = value; }
            get { return _custlinkman; }
        }
        /// <summary>
        /// 客户方负责人电话
        /// </summary>
        public string LinkTel
        {
            set { _linktel = value; }
            get { return _linktel; }
        }
        /// <summary>
        /// 我方负责人
        /// </summary>
        public int LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 我方负责人电话
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 项目所在地
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 项目开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 项目结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 项目概述
        /// </summary>
        public string OverView
        {
            set { _overview = value; }
            get { return _overview; }
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
        /// 制表人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制表日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 可查看人员列表（ID，多个）
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }
        /// <summary>
        /// 投资额
        /// </summary>
        public Decimal Investment
        {
            set { _investment = value; }
            get { return _investment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField1
        {
            set { _extfield1 = value; }
            get { return _extfield1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField2
        {
            set { _extfield2 = value; }
            get { return _extfield2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField3
        {
            set { _extfield3 = value; }
            get { return _extfield3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField4
        {
            set { _extfield4 = value; }
            get { return _extfield4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField5
        {
            set { _extfield5 = value; }
            get { return _extfield5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField6
        {
            set { _extfield6 = value; }
            get { return _extfield6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField7
        {
            set { _extfield7 = value; }
            get { return _extfield7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField8
        {
            set { _extfield8 = value; }
            get { return _extfield8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField9
        {
            set { _extfield9 = value; }
            get { return _extfield9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField10
        {
            set { _extfield10 = value; }
            get { return _extfield10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField11
        {
            set { _extfield11 = value; }
            get { return _extfield11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField12
        {
            set { _extfield12 = value; }
            get { return _extfield12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField13
        {
            set { _extfield13 = value; }
            get { return _extfield13; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField14
        {
            set { _extfield14 = value; }
            get { return _extfield14; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField15
        {
            set { _extfield15 = value; }
            get { return _extfield15; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField16
        {
            set { _extfield16 = value; }
            get { return _extfield16; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField17
        {
            set { _extfield17 = value; }
            get { return _extfield17; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField18
        {
            set { _extfield18 = value; }
            get { return _extfield18; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField19
        {
            set { _extfield19 = value; }
            get { return _extfield19; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField20
        {
            set { _extfield20 = value; }
            get { return _extfield20; }
        }
        #endregion Model
    }
}
