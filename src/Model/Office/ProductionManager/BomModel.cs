using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class BomModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _bomno;
        private string _subject;
        private int _parentno;
        private string _type;
        private string _verson;
        private int _routeid;
        private int _creator;
        private DateTime _createdate;
        private string _remark;
        private string _productid;
        private int _unitid;
        private int _usedunitid;
        private string _exrate;
        private string _usedstatus;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 
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
        /// 物料清单编号
        /// </summary>
        public string BomNo
        {
            set { _bomno = value; }
            get { return _bomno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 上级BOM的ID(对应本表的ID)
        /// </summary>
        public int ParentNo
        {
            set { _parentno = value; }
            get { return _parentno; }
        }
        /// <summary>
        /// Bom类型 0：工程Bom 1： 生产Bom 2：销售Bom 3：成本Bom
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Verson
        {
            set { _verson = value; }
            get { return _verson; }
        }
        /// <summary>
        /// 工艺路线ID（对应工艺路线表ID）
        /// </summary>
        public int RouteID
        {
            set { _routeid = value; }
            get { return _routeid; }
        }
        /// <summary>
        /// 建档人
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
        /// 父件（物品ID，对应物品表ID）
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 物料计量单位（对应物品计量单位表ID）
        /// </summary>
        public int UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public int UsedUnitID
        {
            set { _usedunitid = value; }
            get { return _usedunitid; }
        }
        /// <summary>
        /// 换算率
        /// </summary>
        public string ExRate
        {
            set { _exrate = value; }
            get { return _exrate; }
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
        private string _detbomno;
        private string _detproductid;
        private string _detproducttype;
        private string _detunitid;
        private string _detquota;
        private string _detrateloss;
        private string _detismain;
        private string _detusedstatus;
        private string _detsourcetype;
        private string _detremark;
        /// <summary>
        /// 物料清单编号（对应物料清单表BOM编号）
        /// </summary>
        public string DetBomNo
        {
            set { _detbomno = value; }
            get { return _detbomno; }
        }
        /// <summary>
        /// 子件ID（对应物品表ID）
        /// </summary>
        public string DetProductID
        {
            set { _detproductid = value; }
            get { return _detproductid; }
        }
        /// <summary>
        /// 子件类型ID（对应物品分类代码ID）
        /// </summary>
        public string DetProductType
        {
            set { _detproducttype = value; }
            get { return _detproducttype; }
        }
        /// <summary>
        /// 子件单位ID（对应物品计量单位表ID）
        /// </summary>
        public string DetUnitID
        {
            set { _detunitid = value; }
            get { return _detunitid; }
        }
        /// <summary>
        /// 定额
        /// </summary>
        public string DetQuota
        {
            set { _detquota = value; }
            get { return _detquota; }
        }
        /// <summary>
        /// 损耗率
        /// </summary>
        public string DetRateLoss
        {
            set { _detrateloss = value; }
            get { return _detrateloss; }
        }
        /// <summary>
        /// 是否关键件
        /// </summary>
        public string DetIsMain
        {
            set { _detismain = value; }
            get { return _detismain; }
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
        /// 来源类型 0：自制
        /// </summary>
        public string DetSourceType
        {
            set { _detsourcetype = value; }
            get { return _detsourcetype; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string DetRemark
        {
            set { _detremark = value; }
            get { return _detremark; }
        }
        #endregion Detail Model
    }
}
