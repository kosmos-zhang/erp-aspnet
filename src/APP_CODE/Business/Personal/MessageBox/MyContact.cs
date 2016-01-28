using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Personal.MessageBox
{
	/// <summary>
	/// 业务逻辑类MyContact 的摘要说明。
	/// </summary>
	public class MyContact
	{
		private readonly XBase.Data.Personal.MessageBox.MyContact dal=new XBase.Data.Personal.MessageBox.MyContact();
		public MyContact()
		{}
		#region  成员方法

        public DataTable GetListEx(string companyCD,int creator)
        {
            return dal.GetListEx(companyCD,creator);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(XBase.Model.Personal.MessageBox.MyContact model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(XBase.Model.Personal.MessageBox.MyContact model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Personal.MessageBox.MyContact GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataTable GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}


         /// <summary>
        /// 获得行数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

		#endregion  成员方法
	}
}

