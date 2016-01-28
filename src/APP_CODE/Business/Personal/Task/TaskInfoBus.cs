using System;
using System.Collections.Generic;
using System.Text;

using XBase.Data.DBHelper;
using XBase.Model.Personal.Task;
using XBase.Data.Personal.Task;
namespace XBase.Business.Personal.Task
{
    public class TaskInfoBus
    {
        public static int InsertTaskInfo(TaskModel model)
        {
            int isSucc = 0;
            try
            {
                //执行更新操作
                isSucc = TaskInfoDBHelper.AddTaskInfo(model);
            }
            catch 
            {
                //输出日志
            }
            return isSucc;
        }

        public static bool UpdateTaskInfo(TaskModel model)
        {
            bool isSucc = false;
            try
            {
                //执行更新操作
                isSucc = TaskInfoDBHelper.UpdateTaskInfo(model);
            }
            catch 
            {
                //输出日志
            }
            return isSucc;
        }

        public static bool ChangeStatus(TaskModel model)
        {
            bool isSucc = false;
            try
            {
                //执行更新操作
                isSucc = TaskInfoDBHelper.ChangeStatus(model);
            }
            catch 
            {
                //输出日志
            }
            return isSucc;
        }

        public static bool DeleteTask(string[] id)
        {
            bool isSucc = false;
            try
            {
                //执行更新操作
                isSucc = TaskInfoDBHelper.DeleteTaskById(id);
            }
            catch 
            {
                //输出日志
            }
            return isSucc;
        }

        public static bool ReportTask(TaskModel model)
        {
            bool isSucc = false;
            try
            {
                //执行更新操作
                isSucc = TaskInfoDBHelper.ReportTask(model);
            }
            catch 
            {
                //输出日志
            }
            return isSucc;
        }
        public static bool CheckTask(TaskModel model)
        {
            bool isSucc = false;
            try
            {
                //执行更新操作
                isSucc = TaskInfoDBHelper.CheckTask(model);
            }
            catch 
            {
                //输出日志
            }
            return isSucc;
        }
    }
}
