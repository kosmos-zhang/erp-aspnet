using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageLossModel
    {
        public StorageLossModel()
		{}
		#region Model
		private string _id;
		private string _companycd;
		private string _lossno;
		private string _title;
		private string _executor;
		private string _deptid;
		private string _storageid;
		private string _reasontype;
		private string _lossdate;
		private string _totalprice;
		private string _counttotal;
		private string _summary;
		private string _remark;
		private string _attachment;
		private string _creator;
		private string _createdate;
		private string _billstatus;
		private string _confirmor;
		private string _confirmdate;
		private string _closer;
		private string _closedate;
		private string _modifieddate;
		private string _modifieduserid;
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
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LossNo
		{
			set{ _lossno=value;}
			get{return _lossno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Executor
		{
			set{ _executor=value;}
			get{return _executor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeptID
		{
			set{ _deptid=value;}
			get{return _deptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StorageID
		{
			set{ _storageid=value;}
			get{return _storageid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReasonType
		{
			set{ _reasontype=value;}
			get{return _reasontype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LossDate
		{
			set{ _lossdate=value;}
			get{return _lossdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TotalPrice
		{
			set{ _totalprice=value;}
			get{return _totalprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CountTotal
		{
			set{ _counttotal=value;}
			get{return _counttotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Summary
		{
			set{ _summary=value;}
			get{return _summary;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Attachment
		{
			set{ _attachment=value;}
			get{return _attachment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillStatus
		{
			set{ _billstatus=value;}
			get{return _billstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Confirmor
		{
			set{ _confirmor=value;}
			get{return _confirmor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConfirmDate
		{
			set{ _confirmdate=value;}
			get{return _confirmdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Closer
		{
			set{ _closer=value;}
			get{return _closer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CloseDate
		{
			set{ _closedate=value;}
			get{return _closedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModifiedDate
		{
			set{ _modifieddate=value;}
			get{return _modifieddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModifiedUserID
		{
			set{ _modifieduserid=value;}
			get{return _modifieduserid;}
		}
		#endregion Model

	}
}

