/**********************************************
 * 类作用：   目标检查信息事务层处理
 * 建立人：   王乾睿
 * 建立时间： 2009.04.20
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

using XBase.Common;
using XBase.Model.Personal.AimManager;
using XBase.Data.Personal.AimManager;

namespace XBase.Business.Personal.AimManager
{
    /// <summary>
    /// 类名：AimInfoBus
    /// 描述：目标检查信息事务层处理
    /// 作者：王乾睿
    /// 创建时间：2009.4.20
    /// 最后修改时间：2009.4.20
    /// </summary>
    ///
    public class AimCheckInfoBus
    {
        #region  查询目标检查
        public static DataTable GetAimCheckList(int pageindex, int pagesize, Hashtable parm, out int RecordCount)
        {

            DataTable aimlisttable = new DataTable();
            int rec = 0;
            try
            {
                //执行更新操作
                aimlisttable = AimCheckInfoDBHelper.SelectAimCheckList(pageindex, pagesize, parm,out rec);
            }
            catch
            {
                //输出日志

            }
            RecordCount = rec;
            return aimlisttable;
        }

        #endregion

        #region 编辑目标检查
        public static int SaveAimCheckInfo(AimCheckInfoModel model)
        {
            //定义返回变量
            int isSucc = 0;

            //ID存在时，更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    //执行更新操作
                    isSucc = AimCheckInfoDBHelper.UpdateAimCheckInfo(model);
                }
                catch
                {
                    //输出日志

                }
            }
            //插入
            else
            {
                try
                {
                    //执行插入操作
                    isSucc = AimCheckInfoDBHelper.InsertAimCheckInfo(model);
                }
                catch
                {
                    //输出日志

                }
            }
            return isSucc;
        }
        #endregion
        #region 删除目标检查
        public static int DelAimCheckInfo(string[] IdStr)
        {
            int delCount = IdStr.Length;
            try
            {
                //执行更新操作
                if (AimCheckInfoDBHelper.DelAimInfoByIdArray(IdStr) != true)
                    return 0;
            }
            catch
            {
                return 0;
                //输出日志
            }
            return delCount;
        }
        #endregion

    }
}