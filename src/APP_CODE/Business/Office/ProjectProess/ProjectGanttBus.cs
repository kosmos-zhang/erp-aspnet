using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.Office.ProjectProess;
using System.Data;

namespace XBase.Business.Office.ProjectProess
{
    public class ProjectGanttBus
    {
       
        public static DataSet GetProssList(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectGanttDBHelper.GetProssList(projectID, userinfo);
        }
    }
}
