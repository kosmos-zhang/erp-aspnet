using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
   public    class StorageAdjustModel
    {
       public StorageAdjustModel()
       { }
       #region Model
       private int _id;
       private string _companycd;
       private string _adjustno;
       private int _storageid;
       private int _reasontype;
       private int _executor;
       private int _deptid;
       private DateTime _adjustdate;
       private decimal _totalprice;
       private decimal _counttotal;
       private string _summary;
       private string _remark;
       private string _attachment;
       private int _creator;
       private DateTime _createdate;
       private string _billstatus;
       private int _confirmor;
       private DateTime _confirmdate;
       private int _closer;
       private DateTime _closedate;
       private DateTime _modifieddate;
       private string _modifieduserid;
       private string _title;
       private string _batchno;

       /// <summary>
       /// 批次
       /// </summary>
       public string BatchNo
       {
           set { _batchno = value; }
           get { return _batchno; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int ID
       {
           set { _id = value; }
           get { return _id; }
       }
       public string Title
       {
           get { return _title; }
           set { _title = value; }
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
       public int StorageID
       {
           set { _storageid = value; }
           get { return _storageid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int ReasonType
       {
           set { _reasontype = value; }
           get { return _reasontype; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int Executor
       {
           set { _executor = value; }
           get { return _executor; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int DeptID
       {
           set { _deptid = value; }
           get { return _deptid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public DateTime AdjustDate
       {
           set { _adjustdate = value; }
           get { return _adjustdate; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal TotalPrice
       {
           set { _totalprice = value; }
           get { return _totalprice; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal CountTotal
       {
           set { _counttotal = value; }
           get { return _counttotal; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string Summary
       {
           set { _summary = value; }
           get { return _summary; }
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
       public string Attachment
       {
           set { _attachment = value; }
           get { return _attachment; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int Creator
       {
           set { _creator = value; }
           get { return _creator; }
       }
       /// <summary>
       /// 
       /// </summary>
       public DateTime CreateDate
       {
           set { _createdate = value; }
           get { return _createdate; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string BillStatus
       {
           set { _billstatus = value; }
           get { return _billstatus; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int Confirmor
       {
           set { _confirmor = value; }
           get { return _confirmor; }
       }
       /// <summary>
       /// 
       /// </summary>
       public DateTime ConfirmDate
       {
           set { _confirmdate = value; }
           get { return _confirmdate; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int Closer
       {
           set { _closer = value; }
           get { return _closer; }
       }
       /// <summary>
       /// 
       /// </summary>
       public DateTime CloseDate
       {
           set { _closedate = value; }
           get { return _closedate; }
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
