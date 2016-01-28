
/**********************************************
 * 类作用：   EquipmentFittings表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/03/06
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
   public class EquipmentFitModel
    {
        #region Model
        private int _id;
        private string _fittingxh;
        private string _fittingno;
        private string _companycd;
        private string _equipmentid;
        private string _fittingname;
        private string _fittingdescription;
        private string _equiname;
        private string _attachment;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 配件行序号
        /// </summary>
        public string FitXH
        {
            set { _fittingxh = value; }
            get { return _fittingxh; }
        }
        /// <summary>
        /// 配件编号
        /// </summary>
        public string FittingNO
        {
            set { _fittingno = value; }
            get { return _fittingno; }
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
        /// 设备号
        /// </summary>
        public string EquipmentID
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        /// <summary>
        /// 配件名称
        /// </summary>
        public string FittingName
        {
            set { _fittingname = value; }
            get { return _fittingname; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string FittingDescription
        {
            set { _fittingdescription = value; }
            get { return _fittingdescription; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquiName
        {
            set { _equiname = value; }
            get { return _equiname; }
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
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
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
