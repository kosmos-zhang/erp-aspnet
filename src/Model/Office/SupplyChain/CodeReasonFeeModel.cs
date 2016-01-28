using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
   public class CodeReasonFeeModel
    {
       public CodeReasonFeeModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _codename;
		private int _flag;
		private string _description;
		private string _usedstatus;
		private DateTime _modifieddate;
		private string _modifieduserid;
        private string _CodeSymbol;
        private string _feesubjectsNo;
        
		/// <summary>
		/// 
		/// </summary>
		public int ID
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
        public string FeeSubjectsNo
		{
            set { _feesubjectsNo = value; }
            get { return _feesubjectsNo; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string CodeName
		{
		set{ _codename=value;}
		get{return _codename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Flag
		{
		set{ _flag=value;}
		get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
		set{ _description=value;}
		get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UsedStatus
		{
		set{ _usedstatus=value;}
		get{return _usedstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ModifiedDate
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
        public string CodeSymbol
        {
            set { _CodeSymbol = value; }
            get { return _CodeSymbol; }
        }
		#endregion Model
    }
}
