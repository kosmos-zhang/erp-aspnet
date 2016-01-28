/***********************************************************************
 * 
 * Module Name:Business.PageBus
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2008-12-29 00:00:00
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using XBase.Data;
using System.Data;

namespace XBase.Business
{
   public class PageBus
    {
        #region 数据操作

        #region 属性字段
        private static string tablename;
        public static string TableName
        {
            get { return tablename; }
            set { tablename = value; }
        }

        private static string getfields = "*";

        public string GetFields
        {
            get { return getfields; }
            set { getfields = value; }
        }

        private static string orderfieldname = string.Empty;

        public static string MOrderFieldName
        {
            get { return orderfieldname; }
            set { orderfieldname = value; }
        }

        private static int pagesize = 10;

        public static int PageSize
        {
            get { return pagesize; }
            set { pagesize = value; }
        }

        private static int pageindex = 1;

        public static int PageIndex
        {
            get { return pageindex; }
            set { pageindex = value; }
        }


        private static bool docount = false;

        public static bool DoCount
        {
            get { return docount; }
            set { docount = value; }
        }

        private static bool ordertype = false;

        public bool OrderType
        {
            get { return ordertype; }
            set { ordertype = value; }
        }

        private static string strwhere = string.Empty;

        public string StrWhere
        {
            get { return strwhere; }
            set { strwhere = value; }
        }
        #endregion
        #endregion

        #region 构造函数
        public PageBus(string TableName, string StrWhere)
        {
            tablename = TableName;
            strwhere = StrWhere;
            docount = true;
        }

        public PageBus(int PageIndex, int PageSize)
        {
            pageindex = PageIndex;
            pagesize = PageSize;
        }
        public PageBus(string TableName, bool DoCount,
                          string StrGetFields, string Orderfieldname, int PageSize,
                          int PageIndex, bool OrderType, string StrWhere)
        {
            tablename = TableName;
            docount = DoCount;
            getfields = StrGetFields;
            orderfieldname = Orderfieldname;
            pagesize = PageSize;
            pageindex = PageIndex;
            ordertype = OrderType;
            strwhere = StrWhere;
        }
        #endregion

        /// <summary>
        /// 对DataTable数据集进行分页
        /// </summary>
        /// <param name="dtSource">源数据集</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="PageSize">页显示行数</param>
        /// <returns>NewTable</returns>
        public static DataTable GetPageTable(DataTable dtSource, int PageIndex, int PageSize)
        {
            //获取页数未设置时，返回原来数据集
            if (PageIndex == 0 && dtSource == null && pagesize == 0 ) return null;
            try
            {
                //定义返回的数据集
                DataTable dtNew = dtSource.Copy();
                //清空原有的数据
                dtNew.Clear();
                //获取记录的开始行数
                int RowBegin = (PageIndex - 1) * PageSize;
                //获取记录的结束行数
                int RowEnd = PageIndex * PageSize;
                //开始记录数大于总数据数时，返回空数据集
                if (RowBegin >= dtSource.Rows.Count)
                    return dtNew;
                //结束记录数大于总数据数，将结束记录数设置为总数据数
                if (RowEnd > dtSource.Rows.Count)
                    RowEnd = dtSource.Rows.Count;
                //查询需要返回的数据集
                for (int i = RowBegin; i <= RowEnd - 1; i++)
                {
                    //从数据源复制数据到返回集
                    DataRow data = dtSource.Rows[i];
                    dtNew.ImportRow(data);
                }
                return dtNew;
            }
            catch(System.Exception ex)
            {
                throw ex;
            }
        }
       /// <summary>
       /// 对ArrayList数组进行分页
       /// </summary>
       /// <param name="List">数组名称</param>
       /// <param name="PageSize">页显示条数</param>
       /// <param name="PageIndex">页索引</param>
       /// <returns>ArrayList</returns>
        public static ArrayList PageList(ArrayList List, int PageSize, int PageIndex)
        {
           if(List==null) return null;
           ArrayList NewList = new ArrayList();
           try
           {
               if (List.Count > 0)
               {
                   //获取记录的开始行数
                   int RowBegin = (PageIndex - 1) * PageSize;
                   //获取记录的结束行数
                   int RowEnd = PageIndex * PageSize;
                   //开始记录数大于总数据数时，返回空数据集
                   if (RowBegin > List.Count)
                   {
                       return null;
                   }
                   //结束记录数大于总数据数，将结束记录数设置为总数据数
                   if (RowEnd > List.Count)
                       RowEnd = List.Count;
                   //查询需要返回的数据集
                   for (int i = RowBegin; i <= RowEnd - 1; i++)
                   {
                       //从数据源复制数据到返回集
                       NewList.Add(List[i]);
                   }
               }
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
           return NewList;
        }  
        /// <summary>
        /// 根据sql返回结果集
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable PageExecSql(string sql, int PageSize,int PageIndex, ref int PageCount)
        {
            if (string.IsNullOrEmpty(sql)) return null;
            DataTable dt = null;
            DataTable dtNew = null;
            try
            {
                dt= PageHelper.PageExecSql(sql);
                if (dt.Rows.Count > 0)
                {
                    dtNew = dt.Copy();
                    //清空原有的数据
                    dtNew.Clear();
                    //总记录数
                    PageCount = dt.Rows.Count;
                    //获取记录的开始行数
                    int RowBegin = (PageIndex - 1) * PageSize;
                    //获取记录的结束行数
                    int RowEnd = PageIndex * PageSize;
                    //开始记录数大于总数据数时，返回空数据集
                    if (RowBegin > dt.Rows.Count)
                    {
                        return null;
                    }
                    //结束记录数大于总数据数，将结束记录数设置为总数据数
                    if (RowEnd > dt.Rows.Count)
                        RowEnd = dt.Rows.Count;
                    //查询需要返回的数据集
                    for (int i = RowBegin; i <= RowEnd - 1; i++)
                    {
                        //从数据源复制数据到返回集
                        DataRow data = dt.Rows[i];
                        dtNew.ImportRow(data);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dtNew;
        }
       /// <summary>
       /// 获取指定表记录数
       /// </summary>
       /// <returns>int</returns>
       public static int GetCount()
       {
           if (string.IsNullOrEmpty(tablename)) return 0;
           int count = 0;
           try
           {
               count= PageHelper.GetCount(tablename,strwhere,docount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
           return count;
       }
       /// <summary>
       /// 读取指定表分页记录
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetDataReader()
       {
           if (string.IsNullOrEmpty(tablename) && string.IsNullOrEmpty(orderfieldname)) return null;
           DataTable dt = null;
           try
           {
               dt= PageHelper.GetDataReader(docount,tablename,getfields,ordertype,orderfieldname,pageindex,pagesize,strwhere);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
           return dt;
       }
    }
}
