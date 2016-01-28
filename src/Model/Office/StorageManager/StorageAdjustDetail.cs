using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
   public  class StorageAdjustDetail
    {
       public StorageAdjustDetail()
       { }
       #region Model
       private int _id;
       private string _companycd;
       private string _adjustno;
       private int _sortno;
       private int _productid;
       private int _unitid;
       private string _adjusttype;
       private decimal _adjustcount;
       private decimal _costprice;
       private decimal _costpricetotal;
       private string _remark;
       private DateTime _modifieddate;
       private string _modifieduserid;

       private string _batchno;
       private string _usedunitid;
       private string _usedunitcount;
       private string _usedprice;
       private string _exrate;
       /// <summary>
       /// 批次
       /// </summary>
       public string BatchNo
       {
           set { _batchno = value; }
           get { return _batchno; }
       }
       /// <summary>
       /// 单位(实际使用的单位)
       /// </summary>
       public string UsedUnitID
       {
           set { _usedunitid = value; }
           get { return _usedunitid; }
       }
       /// <summary>
       /// 数量(实际使用单位的数量)
       /// </summary>
       public string UsedUnitCount
       {
           set { _usedunitcount = value; }
           get { return _usedunitcount; }
       }
       /// <summary>
       /// 单价（实际单价）
       /// </summary>
       public string UsedPrice
       {
           set { _usedprice = value; }
           get { return _usedprice; }
       }
       /// <summary>
       /// 单位换算率
       /// </summary>
       public string ExRate
       {
           set { _exrate = value; }
           get { return _exrate; }
       }
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
       /// 
       /// </summary>
       public string AdjustNo
       {
           set { _adjustno = value; }
           get { return _adjustno; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int SortNo
       {
           set { _sortno = value; }
           get { return _sortno; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int ProductID
       {
           set { _productid = value; }
           get { return _productid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int UnitID
       {
           set { _unitid = value; }
           get { return _unitid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string AdjustType
       {
           set { _adjusttype = value; }
           get { return _adjusttype; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal AdjustCount
       {
           set { _adjustcount = value; }
           get { return _adjustcount; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal CostPrice
       {
           set { _costprice = value; }
           get { return _costprice; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal CostPriceTotal
       {
           set { _costpricetotal = value; }
           get { return _costpricetotal; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string Remark
       {
           set { _remark = value; }
           get { return _remark; }
       }
       /// <summary>
       /// 
       /// </summary>
       public DateTime ModifiedDate
       {
           set { _modifieddate = value; }
           get { return _modifieddate; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ModifiedUserID
       {
           set { _modifieduserid = value; }
           get { return _modifieduserid; }
       }
       #endregion Model
    }
}
