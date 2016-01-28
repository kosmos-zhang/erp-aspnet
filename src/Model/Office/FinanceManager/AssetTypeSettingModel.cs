/**********************************************
 * 类作用：   固定资产类别表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
  public  class AssetTypeSettingModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _typename;
        private string _assettype;
        private string _countmethod;
        private int _estimateuseyear;
        private int  _estiresivalue;
        private string _subjectscd;
        private string _usedstatus;
        private string _remark;
        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 资产类型
        /// </summary>
        public string AssetType
        {
            set { _assettype = value; }
            get { return _assettype; }
        }
        /// <summary>
        /// 计算方法
        /// </summary>
        public string CountMethod
        {
            set { _countmethod = value; }
            get { return _countmethod; }
        }
        /// <summary>
        /// 预计使用年限
        /// </summary>
        public int EstimateUseYear
        {
            set { _estimateuseyear = value; }
            get { return _estimateuseyear; }
        }
        /// <summary>
        /// 预计净残值率
        /// </summary>
        public int  EstiResiValue
        {
            set { _estiresivalue = value; }
            get { return _estiresivalue; }
        }
        /// <summary>
        /// 累计折旧科目
        /// </summary>
        public string SubjectsCD
        {
            set { _subjectscd = value; }
            get { return _subjectscd; }
        }
        /// <summary>
        /// 使用状态
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
        #endregion Model
    }
}
