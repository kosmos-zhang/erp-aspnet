using System;

namespace XBase.Model.KnowledgeCenter
{

	/// <summary>
	///知识分类表 实体
	/// </summary>
	public class KnowledgeType
	{
		private int _iD;
		/// <summary>
		///ID
		/// </summary>
		public int ID
		{
			get{ return _iD;}
			set{ _iD=value;}
		}

		private string _flag;
		/// <summary>
		///1-文献，2-范本
		/// </summary>
		public string Flag
		{
			get{ return _flag;}
			set{ _flag=value;}
		}

		private string _typeName;
		/// <summary>
		///知识分类名称
		/// </summary>
		public string TypeName
		{
			get{ return _typeName;}
			set{ _typeName=value;}
		}

		private int _supperTypeID;
		/// <summary>
		///对应本表ID
		/// </summary>
		public int SupperTypeID
		{
			get{ return _supperTypeID;}
			set{ _supperTypeID=value;}
		}

        private string _path;
        /// <summary>
        ///分类路径
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }


		private DateTime _modifiedDate;
		/// <summary>
		///修改时间
		/// </summary>
		public DateTime ModifiedDate
		{
			get{ return _modifiedDate;}
			set{ _modifiedDate=value;}
		}

		private string _modifiedUserID;
		/// <summary>
		///修改用户ID
		/// </summary>
		public string ModifiedUserID
		{
			get{ return _modifiedUserID;}
			set{ _modifiedUserID=value;}
		}

	}
}