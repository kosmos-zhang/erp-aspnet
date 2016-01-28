using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
   public class CheckNotPassDetailModel
    {
       public CheckNotPassDetailModel()
       { }

       private int _id;
       private string _companycd;
       private byte[] _processno;
       private int _sortno;
       private int _productid;
       private int _unitid;
       private int _reasonid;
       private decimal _notpassnum;
       private int _processway;
       private decimal _rate;
       private string _remark;
       private DateTime _modifieddate;
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
       /// 
       /// </summary>
       public byte[] ProcessNo
       {
           set { _processno = value; }
           get { return _processno; }
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
       public int ReasonID
       {
           set { _reasonid = value; }
           get { return _reasonid; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal NotPassNum
       {
           set { _notpassnum = value; }
           get { return _notpassnum; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int ProcessWay
       {
           set { _processway = value; }
           get { return _processway; }
       }
       /// <summary>
       /// 
       /// </summary>
       public decimal Rate
       {
           set { _rate = value; }
           get { return _rate; }
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
    }
}
