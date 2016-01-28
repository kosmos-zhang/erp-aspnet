using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace XBase.Business.Decision
{
    public class DataStat
    {
        private static readonly XBase.Data.Decision.DataStat _dal = new XBase.Data.Decision.DataStat();

        public IList<XBase.Model.Decision.DataStat> GetDataStatListbyCond(string cond, string orderExp)
        {
            return _dal.GetDataStatListbyCond(cond, orderExp);
        }

        public static int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            return _dal.GetPageData(out tb, PageSize, PageIndex, queryCondition, sortExp, fieldList);
        }
    }
}
