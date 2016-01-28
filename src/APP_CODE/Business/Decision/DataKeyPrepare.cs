using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Decision
{
    public class DataKeyPrepare
    {
        private XBase.Data.Decision.DataKeyPrepare _dao = new XBase.Data.Decision.DataKeyPrepare();

        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetDataKeyPrepareAll()
        {
            return _dao.GetDataKeyPrepareAll();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public IList<XBase.Model.Decision.DataKeyPrepare>  GetDataKeyPrepareListbyCond(string cond, string orderExp)
        {
            return _dao.GetDataKeyPrepareListbyCond(cond, orderExp);
        }

        public XBase.Model.Decision.DataKeyPrepare GetDataKeyPrepareListbyId(string Id)
        {
            IList<XBase.Model.Decision.DataKeyPrepare> _list = GetDataKeyPrepareListbyCond("[DataId]=" + Id, "[DataId]");
            return _list[0];
        }
    }
}
