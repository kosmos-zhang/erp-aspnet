using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using XBase.Common;
using XBase.Model.Personal.Agenda;
using XBase.Data.Personal.Agenda;

namespace XBase.Business.Personal.Agenda
{
  public   class AgendaListInfoBus
    {
         public static  DataTable GetAgendaCountByDate( DateTime start ,DateTime end,int uid ,bool iscurrentuser){
            
             return AgendaListInfoDBHelper.SelectAgendaCount(start, end, uid, iscurrentuser);
         }

         public static DataTable GetAgendaCountByDay(DateTime sdate, int uid, bool iscurrentuser) {
             return AgendaListInfoDBHelper.SelectAgendaCountByDay(sdate, uid, iscurrentuser);
         }

    }
}
