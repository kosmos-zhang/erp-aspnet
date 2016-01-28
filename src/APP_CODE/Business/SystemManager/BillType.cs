using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace XBase.Business.SystemManager
{
    public class BillType
    {
        private static readonly XBase.Data.SystemManager.BillType dal = new XBase.Data.SystemManager.BillType();


        public static DataTable GetBillTypeList()
        {            
            return dal.GetBillTypeList();
        }
    }
}
