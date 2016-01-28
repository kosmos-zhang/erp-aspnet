using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class TechnicsRoutingModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _routeno;
        private string _routename;
        private string _pyshort;
        private string _verson;
        private string _ismaintech;
        private int _bomid;
        private int _productid;
        private string _usedstatus;
        private int _creator;
        private DateTime _createdate;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
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
        /// 工艺路线编号
        /// </summary>
        public string RouteNo
        {
            set { _routeno = value; }
            get { return _routeno; }
        }
        /// <summary>
        /// 工艺路线名称
        /// </summary>
        public string RouteName
        {
            set { _routename = value; }
            get { return _routename; }
        }
        /// <summary>
        /// 拼音缩写
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 版本
        /// </summary>
        public string Verson
        {
            set { _verson = value; }
            get { return _verson; }
        }
        /// <summary>
        /// 是否主工艺0：否1：是
        /// </summary>
        public string IsMainTech
        {
            set { _ismaintech = value; }
            get { return _ismaintech; }
        }
        /// <summary>
        /// 物料清单编码
        /// </summary>
        public int BomID
        {
            set { _bomid = value; }
            get { return _bomid; }
        }
        /// <summary>
        /// 物品ID（对应物品表ID）
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
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
        /// 建档人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

        #region Detail Model
        private string _detprocsequnumber;
        private string _detsequid;
        private string _detwcid;
        private string _dettimeunit;
        private string _detisoutsource;
        private string _detcheckway;
        private string _dettimewage;
        private string _detpiecewage;
        private string _detremark;
        private string _detusedstatus;
        private string _detreadytime;
        private string _detruntime;
        private string _detischarge;
        /// <summary>
        /// 顺序号
        /// </summary>
        public string DetProcSequNumber
        {
            set { _detprocsequnumber = value; }
            get { return _detprocsequnumber; }
        }
        /// <summary>
        /// 工序ID
        /// </summary>
        public string DetSequID
        {
            set { _detsequid = value; }
            get { return _detsequid; }
        }
        /// <summary>
        /// 工作中心ID
        /// </summary>
        public string DetWCID
        {
            set { _detwcid = value; }
            get { return _detwcid; }
        }
        /// <summary>
        /// 时间单位
        /// </summary>
        public string DetTimeUnit
        {
            set { _dettimeunit = value; }
            get { return _dettimeunit; }
        }
        /// <summary>
        /// 是否外协0：否1：是
        /// </summary>
        public string DetIsoutsource
        {
            set { _detisoutsource = value; }
            get { return _detisoutsource; }
        }
        /// <summary>
        /// 检验方式：0:免检1:全检2:抽检 3：不定期检验
        /// </summary>
        public string DetCheckWay
        {
            set { _detcheckway = value; }
            get { return _detcheckway; }
        }
        /// <summary>
        /// 单位计时工资
        /// </summary>
        public string DetTimeWage
        {
            set { _dettimewage = value; }
            get { return _dettimewage; }
        }
        /// <summary>
        /// 单位计件工资
        /// </summary>
        public string DetPieceWage
        {
            set { _detpiecewage = value; }
            get { return _detpiecewage; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string DetRemark
        {
            set { _detremark = value; }
            get { return _detremark; }
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string DetUsedStatus
        {
            set { _detusedstatus = value; }
            get { return _detusedstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetReadyTime
        {
            set { _detreadytime = value; }
            get { return _detreadytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetRunTime
        {
            set { _detruntime = value; }
            get { return _detruntime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetIsCharge
        {
            set { _detischarge = value; }
            get { return _detischarge; }
        }
        #endregion Detail Model
    }
}
