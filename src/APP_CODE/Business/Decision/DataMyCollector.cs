using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Decision
{
    public class DataMyCollector
    {
        private static readonly XBase.Data.Decision.DataMyCollector _dal = new XBase.Data.Decision.DataMyCollector();

        public static int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            return _dal.GetPageData(out tb, PageSize, PageIndex, queryCondition, sortExp, fieldList);
        }

        public bool AddDataMyCollector(XBase.Model.Decision.DataMyCollector entity)
        {
            return _dal.AddDataMyCollector(entity);
        }

        public bool DelDataMyCollector(int id)
        {
            return _dal.DelDataMyCollector(id);
        }

        public bool ModDataMyCollector(XBase.Model.Decision.DataMyCollector entity)
        {
            return _dal.ModDataMyCollector(entity);
        }

        public void ModRead(int Id) 
        {
            _dal.ModRead(Id);
        }

        public IList<XBase.Model.Decision.DataMyCollector> GetDataMyCollectorListbyCond(string strWhere, string strOrderby)
        {
            return _dal.GetDataMyCollectorListbyCond(strWhere, strOrderby);
        }

        public DataTable GetDataMyCollectorById(string Id)
        {
            DataSet ds=_dal.GetDataMyCollectorByWhere("a.[Id]="+Id,"Id");
            DataTable dt = null;
            if (ds!=null)
            {
                if (ds.Tables.Count>0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
    }
}
