using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
   public  class CheckNotPassModel
    {
       public CheckNotPassModel()
       { }

       private int _id;
       private string _companycd;
       private string _processno;
       private string _fromtype;
       private int _reportid;
       private int _executor;
       private DateTime _processdate;
       private string _remark;
       private DateTime _createdate;
       private string _bullstatus;
       private int _confirmor;
       private DateTime _confirmordate;
       private int _closer;
       private DateTime _closerdate;
       private DateTime _modifieddate;
       private string _modifieduserid;
       private int _creator;
       private string _Attachment;
       private string _title;
       /// <summary>
       /// 
       /// </summary>
       public int ID
       {
           set { _id = value; }
           get { return _id; }
       }
       public string Attachment
       { get { return _Attachment; } set { _Attachment = value; } }
       /// <summary>
       /// 
       /// </summary>
       public string CompanyCD
       {
           set { _companycd = value; }
           get { return _companycd; }
       }
       public string Title
       {
           get { return _title; }
           set { _title = value; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string ProcessNo
       {
           set { _processno = value; }
           get { return _processno; }
       }
       /// <summary>
       /// 
       /// </summary>
       public string FromType
       {
           set { _fromtype = value; }
           get { return _fromtype; }
       }
       /// <summary>
       /// 
       /// </summary>
       public int ReportID
       {
           set { _reportid = value; }
           get { return _reportid; }
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
       public DateTime ProcessDate
       {
           set { _processdate = value; }
           get { return _processdate; }
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
           set { _bullstatus = value; }
           get { return _bullstatus; }
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
       public DateTime ConfirmorDate
       {
           set { _confirmordate = value; }
           get { return _confirmordate; }
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
       public DateTime CloserDate
       {
           set { _closerdate = value; }
           get { return _closerdate; }
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
       /// <summary>
       /// 
       /// </summary>
       public int Creator
       {
           set { _creator = value; }
           get { return _creator; }
       }

       #region 扩展属性

       private string extField1 = String.Empty; //扩展属性1
       private string extField2 = String.Empty; //扩展属性2
       private string extField3 = String.Empty; //扩展属性3
       private string extField4 = String.Empty; //扩展属性4
       private string extField5 = String.Empty; //扩展属性5
       private string extField6 = String.Empty; //扩展属性6
       private string extField7 = String.Empty; //扩展属性7
       private string extField8 = String.Empty; //扩展属性8
       private string extField9 = String.Empty; //扩展属性9
       private string extField10 = String.Empty; //扩展属性10

       /// <summary>
       /// 扩展属性1
       /// </summary>
       public string ExtField1
       {
           get
           {
               return extField1;
           }
           set
           {
               extField1 = value;
           }
       }

       /// <summary>
       /// 扩展属性2
       /// </summary>
       public string ExtField2
       {
           get
           {
               return extField2;
           }
           set
           {
               extField2 = value;
           }
       }

       /// <summary>
       /// 扩展属性3
       /// </summary>
       public string ExtField3
       {
           get
           {
               return extField3;
           }
           set
           {
               extField3 = value;
           }
       }

       /// <summary>
       /// 扩展属性4
       /// </summary>
       public string ExtField4
       {
           get
           {
               return extField4;
           }
           set
           {
               extField4 = value;
           }
       }

       /// <summary>
       /// 扩展属性5
       /// </summary>
       public string ExtField5
       {
           get
           {
               return extField5;
           }
           set
           {
               extField5 = value;
           }
       }

       /// <summary>
       /// 扩展属性6
       /// </summary>
       public string ExtField6
       {
           get
           {
               return extField6;
           }
           set
           {
               extField6 = value;
           }
       }

       /// <summary>
       /// 扩展属性7
       /// </summary>
       public string ExtField7
       {
           get
           {
               return extField7;
           }
           set
           {
               extField7 = value;
           }
       }

       /// <summary>
       /// 扩展属性8
       /// </summary>
       public string ExtField8
       {
           get
           {
               return extField8;
           }
           set
           {
               extField8 = value;
           }
       }

       /// <summary>
       /// 扩展属性9
       /// </summary>
       public string ExtField9
       {
           get
           {
               return extField9;
           }
           set
           {
               extField9 = value;
           }
       }

       /// <summary>
       /// 扩展属性10
       /// </summary>
       public string ExtField10
       {
           get
           {
               return extField10;
           }
           set
           {
               extField10 = value;
           }
       }
       #endregion
    }
}
