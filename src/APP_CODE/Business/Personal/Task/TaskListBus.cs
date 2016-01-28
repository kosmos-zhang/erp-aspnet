using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using XBase.Model.Personal.Task;
using XBase.Data.Personal.Task;

namespace XBase.Business.Personal.Task
{
    public class TaskListBus
    {
        public static DataTable GetTaskList(TaskModel model, string orderby)
        {
            DataTable dtTaskList = new DataTable();
            try
            {
                //执行更新操作
                dtTaskList = TaskListDBHelper.SelectTaskList(model,orderby);
            }
            catch 
            {
                //输出日志
            }
            return dtTaskList;
        }

        public static DataTable GetTaskListReportDept(TaskModel model)
        {
            DataTable dtTaskList = new DataTable();
            try
            {
                //执行更新操作
                dtTaskList = TaskListDBHelper.GetTaskListReportDept(model);
            }
            catch 
            {
                //输出日志
            }
            return dtTaskList;
        }

        public static DataTable GetTaskListReportPrincipal(TaskModel model)
        {
            DataTable dtTaskList = new DataTable();
            try
            {
                //执行更新操作
                dtTaskList = TaskListDBHelper.GetTaskListReportPrincipal(model);
            }
            catch 
            {
                //输出日志
            }
            return dtTaskList;
        }

        public static DataTable GetTaskListReportStatus(TaskModel model)
        {
            DataTable dtTaskList = new DataTable();
            try
            {
                //执行更新操作
                dtTaskList = TaskListDBHelper.GetTaskListReportStatus(model);
            }
            catch 
            {
                //输出日志
            }
            return dtTaskList;
        }
    }
}
