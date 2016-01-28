/**********************************************
 * 类作用：  数据订阅
 * 建立人：   朱贤兆
 * 建立时间： 2009/06/05
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Decision
{
    public class DataKeyWord
    {
        private static readonly XBase.Data.Decision.DataKeyWord dal = new XBase.Data.Decision.DataKeyWord();

        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetAll()
        {
            return dal.GetAll();
        }

        public static DataSet GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

    }
}
