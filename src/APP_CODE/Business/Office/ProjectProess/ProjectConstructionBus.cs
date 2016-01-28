
/**********************************************
 * 类作用   施工摘要表业务处理层
 * 创建人   xz
 * 创建时间 2010-5-19 19:08:25 
 ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Model.Office.ProjectProess;
using XBase.Data.Office.ProjectProess;


namespace XBase.Business.Office.ProjectProess
{
    /// <summary>
    /// 施工摘要表业务类
    /// </summary>
    public class ProjectConstructionBus
    {
        public static DataTable GetProessList(string projectid,string summaryid, UserInfoUtil userinfo)
        {
            return ProjectConstructionDBHelper.GetProessList(projectid,summaryid, userinfo);
        }

        public static int Add(ProjectConstructionDetails model,string projectName)
        {
            return ProjectConstructionDBHelper.Add(model,projectName);
        }

        public static DataTable GetProessList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            return ProjectConstructionDBHelper.GetProessList(pageindex,pagecount,projectid,summaryname,OrderBy,userinfo,ref totalCount);
        }

        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo)
        {
            ProjectConstructionDBHelper.BindProject(ddl, userinfo);
        }

        public static DataTable GetSummaryByID(string id)
        {
            return ProjectConstructionDBHelper.GetSummaryByID(id);
        }

        public static int Update(ProjectConstructionDetails model)
        {
            return ProjectConstructionDBHelper.Update(model);
        }

        public static int Delete(string IDList)
        {
            return ProjectConstructionDBHelper.Delete(IDList);
        }

    }
}



