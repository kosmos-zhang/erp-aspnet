using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;


using XBase.Data.Office.DefManager;
namespace XBase.Business.DefManager
{
    public class DefineBus
    {
        public static string GetDefineTableByCode(string tableid)
        {
            return DefineDBHelper.GetDefineTableByCode(tableid);
        }

        public static DataSet GetTableStruct(string tableid)
        {
            return DefineDBHelper.GetTableStruct(tableid);
        }

        //public static DataSet GetTableStruct(string tableid, bool isSearch)
        //{

        //    return DefineDBHelper.GetTableStruct(tableid, isSearch);
        //}

        public static DataSet GetTableStruct(string tableid, bool isSearch, bool isShow)
        {

            return DefineDBHelper.GetTableStruct(tableid, isSearch, isShow);
        }

        public static bool GetModuleByTableID(string tableid)
        {
            return DefineDBHelper.GetModuleByTableID(tableid);
        }

        public static DataTable GetDefTableList(Hashtable hs, ref string tablename,ref int totalcount)
        {
            return DefineDBHelper.GetDefTableList(hs, ref tablename,ref totalcount);
        }

        public static DataSet GetModuleValueByTableID(string tableid)
        {
            return DefineDBHelper.GetModuleValueByTableID(tableid);
        }


        public static int AddDefTable(Hashtable hs)
        {
            return DefineDBHelper.AddDefTable(hs);
        }

        public static int UpdateDefTable(Hashtable hs)
        {
            return DefineDBHelper.UpdateDefTable(hs);
        }

        public static DataTable GetDetailsTableStruct(string tableid, ref string info)
        {
            return DefineDBHelper.GetDetailsTableStruct(tableid, ref info);
        }

        public static DataSet FillPageData(string tableid, string id, ref string info)
        {
            return DefineDBHelper.FillPageData(tableid, id, ref info);
        }

        public static string GetRelationByTableID(string tableid)
        {
            return DefineDBHelper.GetRelationByTableID(tableid);
        }

        public static string GetRelationTable(string tableid)
        {
            return DefineDBHelper.GetRelationTable(tableid);
        }

        public static string GetTableRows(string tableid)
        {
            return DefineDBHelper.GetTableRows(tableid);
        }

        public static string GetRelationByParentTableID(string tableid, string flag)
        {
            return DefineDBHelper.GetRelationByParentTableID(tableid,flag);
        }

        public static DataSet GetTableHead(string tableid)
        {
            return DefineDBHelper.GetTableHead(tableid);
        }

        public static DataSet GetTableSearch(string tableid)
        {
            return DefineDBHelper.GetTableSearch(tableid);
        }

        public static string GetSubTableByParentID(string tableid)
        {
            return DefineDBHelper.GetSubTableByParentID(tableid);
        }

        public static int DelTableList(string tableid, string Itemlist)
        {
            return DefineDBHelper.DelTableList(tableid, Itemlist);
        }

        public static DataTable GetDataTableList(string tableid)
        {
            return DefineDBHelper.GetDataTableList(tableid);
        }
    }
}
