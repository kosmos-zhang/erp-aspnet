/**********************************************
 * 类作用：   生产任务单数据层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/23
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.ProductionManager
{
    public class ManufactureTaskDBHelper
    {
        #region 生产任务单插入
        /// <summary>
        /// 生产任务单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureTask(ManufactureTaskModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  生产任务单添加SQL语句
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine("INSERT INTO officedba.ManufactureTask   ");
                sqlTask.AppendLine("           (CompanyCD                   ");
                sqlTask.AppendLine("           ,TaskNo                      ");
                sqlTask.AppendLine("           ,Subject                     ");
                sqlTask.AppendLine("           ,Principal                   ");
                sqlTask.AppendLine("           ,DeptID                      ");
                sqlTask.AppendLine("           ,FromType                    ");
                sqlTask.AppendLine("           ,ManufactureType             ");
                sqlTask.AppendLine("           ,CountTotal                  ");
                sqlTask.AppendLine("           ,BillStatus                  ");
                sqlTask.AppendLine("           ,Creator                     ");
                sqlTask.AppendLine("           ,CreateDate                  ");
                sqlTask.AppendLine("           ,Remark                      ");
                sqlTask.AppendLine("           ,DocumentURL                 ");
                sqlTask.AppendLine("           ,ProjectID                   ");
                sqlTask.AppendLine("           ,ModifiedDate                ");
                sqlTask.AppendLine("          ,ModifiedUserID)              ");
                sqlTask.AppendLine("     VALUES                             ");
                sqlTask.AppendLine("          (@CompanyCD                   ");
                sqlTask.AppendLine("           ,@TaskNo                     ");
                sqlTask.AppendLine("           ,@Subject                    ");
                sqlTask.AppendLine("           ,@Principal                  ");
                sqlTask.AppendLine("           ,@DeptID                     ");
                sqlTask.AppendLine("           ,@FromType                   ");
                sqlTask.AppendLine("           ,@ManufactureType            ");
                sqlTask.AppendLine("           ,@CountTotal                 ");
                sqlTask.AppendLine("           ,@BillStatus                 ");
                sqlTask.AppendLine("           ,@Creator                    ");
                sqlTask.AppendLine("           ,@CreateDate                 ");
                sqlTask.AppendLine("           ,@Remark                     ");
                sqlTask.AppendLine("           ,@DocumentURL                ");
                sqlTask.AppendLine("           ,@ProjectID                  ");
                sqlTask.AppendLine("           ,getdate()                   ");
                sqlTask.AppendLine("           ,'"+loginUserID+"')			");
                sqlTask.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlTask.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
                comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@DocumentURL", model.DocumentURL));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                listADD.Add(comm);
                #endregion

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion

                #region 生产任务单明细信息添加SQL语句
                if (!String.IsNullOrEmpty(model.DetSortNo) && !String.IsNullOrEmpty(model.DetProductID) && !String.IsNullOrEmpty(model.DetProductCount) && !String.IsNullOrEmpty(model.DetStartDate) && !String.IsNullOrEmpty(model.DetEndDate))
                {
                    //SortNo,ProductID,ProductCount,BomID,RouteID,StartDate,EndDate,FromType,FromBillID,FrombIllNo,FromLineNo
                    string[] dtSortNo = model.DetSortNo.Split(',');
                    string[] dtProductID = model.DetProductID.Split(',');
                    string[] dtProductCount = model.DetProductCount.Split(',');
                    string[] dtBomID = model.DetBomID.Split(',');
                    string[] dtRouteID = model.DetRouteID.Split(',');
                    string[] dtStartDate = model.DetStartDate.Split(',');
                    string[] dtEndDate = model.DetEndDate.Split(',');
                    string[] dtFromType = model.DetFromType.Split(',');
                    string[] dtFromBillID = model.DetFromBillID.Split(',');
                    string[] dtFromBillNo = model.DetFromBillNo.Split(',');
                    string[] dtFromLineNo = model.DetFromLineNo.Split(',');
                    string[] dtUsedUnitID = model.DetUsedUnitID.Split(',');
                    string[] dtUsedUnitCount = model.DetUsedUnitCount.Split(',');
                    string[] dtExRate = model.DetExRate.Split(',');


                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (dtProductID.Length >= 1)
                    {
                        for (int i = 0; i < dtProductID.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("INSERT INTO officedba.ManufactureTaskDetail");  
                            cmdsql.AppendLine("           (CompanyCD");                  
                            cmdsql.AppendLine("           ,TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,SortNo");                
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,ProductID");    
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,ProductCount");    
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("           ,UsedUnitID");
                                cmdsql.AppendLine("           ,UsedUnitCount");
                                cmdsql.AppendLine("           ,ExRate");   
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,BomID");       
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,RouteID");     
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,StartDate");   
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,EndDate");  
                                }
                            }

                            cmdsql.AppendLine("           ,FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FromBillID");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,FromLineNo");
                                }
                            }                   
                            cmdsql.AppendLine("           ,ModifiedDate");               
                            cmdsql.AppendLine("           ,ModifiedUserID)");            
                            cmdsql.AppendLine("     VALUES");                            
                            cmdsql.AppendLine("           (@CompanyCD");                 
                            cmdsql.AppendLine("           ,@TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@SortNo");      
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@ProductID");    
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@ProductCount");      
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("           ,@UsedUnitID");
                                cmdsql.AppendLine("           ,@UsedUnitCount");
                                cmdsql.AppendLine("           ,@ExRate");   
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@BomID"); 
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@RouteID"); 
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@StartDate");    
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@EndDate"); 
                                }
                            }

                            cmdsql.AppendLine("           ,@FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FromBillID ");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("           ,@FromLineNo");
                                }
                            } 
                                                      
                            cmdsql.AppendLine("           ,getdate()  ");            
                            cmdsql.AppendLine("           ,'"+loginUserID+"')");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", dtProductCount[i].ToString()));
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@BomID", dtBomID[i].ToString()));
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@RouteID", dtRouteID[i].ToString()));
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@StartDate", dtStartDate[i].ToString()));
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@EndDate", dtEndDate[i].ToString()));
                                }
                            }
                            comms.Parameters.Add(SqlHelper.GetParameter("@FromType", dtFromType[i].ToString()));
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                                }
                            }

                            listADD.Add(comms);
                        }
                    }
                }
                #endregion

                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                else
                {
                    ID = "0";
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改生产任务单和生产任务单明细信息
        /// <summary>
        /// 修改生产任务单和生产任务单明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateManufactureTaskInfo(ManufactureTaskModel model, Hashtable htExtAttr, string loginUserID, string UpdateID)
        {
            //获取登陆用户ID
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  生产任务单修改SQL语句
            StringBuilder sqlDet = new StringBuilder();
            sqlDet.AppendLine("UPDATE officedba.ManufactureTask");      
            sqlDet.AppendLine("   SET CompanyCD = @CompanyCD");          
            sqlDet.AppendLine("      ,TaskNo = @TaskNo");                
            sqlDet.AppendLine("      ,FromType = @FromType");            
            sqlDet.AppendLine("      ,Subject = @Subject");              
            sqlDet.AppendLine("      ,Principal = @Principal");          
            sqlDet.AppendLine("      ,DeptID = @DeptID");                
            sqlDet.AppendLine("      ,ManufactureType = @ManufactureType");
            sqlDet.AppendLine("      ,CountTotal = @CountTotal");               
            sqlDet.AppendLine("      ,Remark = @Remark");
            sqlDet.AppendLine("      ,DocumentURL = @DocumentURL");
            sqlDet.AppendLine("      ,ProjectID = @ProjectID");
            sqlDet.AppendLine("      ,ModifiedDate = getdate()");    
            sqlDet.AppendLine("      ,ModifiedUserID = '"+loginUserID+"'");
            sqlDet.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlDet.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ManufactureType", model.ManufactureType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@DocumentURL", model.DocumentURL));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProjectID", model.ProjectID));
            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 生产任务单元明细信息更新语句
            //1.先删除不在生产任务单明细中的
            //2.更新明细中的ID
            //3.添加其它明细

            #region 先删除不在生产任务单明细中的
            if (!string.IsNullOrEmpty(UpdateID))
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.ManufactureTaskDetail where CompanyCD=@CompanyCD and TaskNo=@TaskNo and  ID not in(" + UpdateID + ")");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));

                listADD.Add(commDel);
            }
            else
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.ManufactureTaskDetail where CompanyCD=@CompanyCD and TaskNo=@TaskNo");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));

                listADD.Add(commDel);
            }
            #endregion


            #region 添加或更新操作
            string[] updateID = UpdateID.Split(',');
            if (!string.IsNullOrEmpty(UpdateID) && updateID.Length > 0)
            {
                if (!String.IsNullOrEmpty(model.DetSortNo) && !String.IsNullOrEmpty(model.DetProductID) && !String.IsNullOrEmpty(model.DetProductCount) && !String.IsNullOrEmpty(model.DetStartDate) && !String.IsNullOrEmpty(model.DetEndDate))
                {
                    //SortNo,ProductID,ProductCount,BomID,RouteID,StartDate,EndDate,FromType,FromBillID,FrombIllNo,FromLineNo
                    string[] dtSortNo = model.DetSortNo.Split(',');
                    string[] dtProductID = model.DetProductID.Split(',');
                    string[] dtProductCount = model.DetProductCount.Split(',');
                    string[] dtBomID = model.DetBomID.Split(',');
                    string[] dtRouteID = model.DetRouteID.Split(',');
                    string[] dtStartDate = model.DetStartDate.Split(',');
                    string[] dtEndDate = model.DetEndDate.Split(',');
                    string[] dtFromType = model.DetFromType.Split(',');
                    string[] dtFromBillID = model.DetFromBillID.Split(',');
                    string[] dtFromBillNo = model.DetFromBillNo.Split(',');
                    string[] dtFromLineNo = model.DetFromLineNo.Split(',');
                    string[] dtUsedUnitID = model.DetUsedUnitID.Split(',');
                    string[] dtUsedUnitCount = model.DetUsedUnitCount.Split(',');
                    string[] dtExRate = model.DetExRate.Split(',');

                    for (int i = 0; i < updateID.Length; i++)
                    {
                        int intUpdateID = int.Parse(updateID[i].ToString());
                        if (intUpdateID > 0)
                        {

                            #region 更新MRP明细中的ID
                            StringBuilder sqlEdit = new StringBuilder();
                            sqlEdit.AppendLine("UPDATE officedba.ManufactureTaskDetail		");
                            sqlEdit.AppendLine("SET                                         ");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("       SortNo = @SortNo						");
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,ProductID = @ProductID				");
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,ProductCount = @ProductCount			");
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                sqlEdit.AppendLine("      ,UsedUnitID = @UsedUnitID			");
                                sqlEdit.AppendLine("      ,UsedUnitCount = @UsedUnitCount			");
                                sqlEdit.AppendLine("      ,ExRate = @ExRate			");
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,BomID = @BomID						");
                                }
                            }
                            else
                            {
                                sqlEdit.AppendLine("      ,BomID = null						");
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,RouteID = @RouteID					");
                                }
                            }
                            else
                            {
                                sqlEdit.AppendLine("      ,RouteID = null						");
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,StartDate = @StartDate				");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,EndDate = @EndDate					");
                                }
                            }

                            sqlEdit.AppendLine("      ,FromType = @FromType					");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,FromBillID = @FromBillID				");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,FromBillNo = @FromBillNo				");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    sqlEdit.AppendLine("      ,FromLineNo = @FromLineNo				");
                                }
                            }

                            sqlEdit.AppendLine("      ,ModifiedDate = getdate()			    ");
                            sqlEdit.AppendLine("      ,ModifiedUserID = '"+loginUserID+"'	");
                            sqlEdit.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID		");														

                            SqlCommand commEdit = new SqlCommand();
                            commEdit.CommandText = sqlEdit.ToString();
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductCount", dtProductCount[i].ToString()));
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@BomID", dtBomID[i].ToString()));
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@RouteID", dtRouteID[i].ToString()));
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@StartDate", dtStartDate[i].ToString()));
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@EndDate", dtEndDate[i].ToString()));
                                }
                            }
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@FromType", dtFromType[i].ToString()));
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    commEdit.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                                }
                            }
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@ID", intUpdateID));

                            listADD.Add(commEdit);
                            #endregion
                        }
                        else
                        {
                            #region 添加MRP明细中的ID
                            //页面上这些字段都是必填，数组的长度必须是相同的
                            System.Text.StringBuilder sqlIn = new System.Text.StringBuilder();
                            sqlIn.AppendLine("INSERT INTO officedba.ManufactureTaskDetail");
                            sqlIn.AppendLine("           (CompanyCD");
                            sqlIn.AppendLine("           ,TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,SortNo");
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,ProductID");
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,ProductCount");
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                sqlIn.AppendLine("           ,UsedUnitID");
                                sqlIn.AppendLine("           ,UsedUnitCount");
                                sqlIn.AppendLine("           ,ExRate");
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,BomID");
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,RouteID");
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,StartDate");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,EndDate");
                                }
                            }

                            sqlIn.AppendLine("           ,FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FromBillID");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,FromLineNo");
                                }
                            }
                            sqlIn.AppendLine("           ,ModifiedDate");
                            sqlIn.AppendLine("           ,ModifiedUserID)");
                            sqlIn.AppendLine("     VALUES");
                            sqlIn.AppendLine("           (@CompanyCD");
                            sqlIn.AppendLine("           ,@TaskNo");
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@SortNo");
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@ProductID");
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@ProductCount");
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                sqlIn.AppendLine("           ,@UsedUnitID");
                                sqlIn.AppendLine("           ,@UsedUnitCount");
                                sqlIn.AppendLine("           ,@ExRate");
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@BomID");
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@RouteID");
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@StartDate");
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@EndDate");
                                }
                            }

                            sqlIn.AppendLine("           ,@FromType");
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FromBillID ");
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FromBillNo");
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    sqlIn.AppendLine("           ,@FromLineNo");
                                }
                            }

                            sqlIn.AppendLine("           ,getdate()  ");
                            sqlIn.AppendLine("           ,'" + loginUserID + "')");

                            SqlCommand commIn = new SqlCommand();
                            commIn.CommandText = sqlIn.ToString();
                            commIn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                            if (dtSortNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtSortNo[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                                }
                            }
                            if (dtProductID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                                }
                            }
                            if (dtProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtProductCount[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@ProductCount", dtProductCount[i].ToString()));
                                }
                            }
                            if (userInfo.IsMoreUnit)
                            {
                                commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                commIn.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            if (dtBomID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtBomID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@BomID", dtBomID[i].ToString()));
                                }
                            }
                            if (dtRouteID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtRouteID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@RouteID", dtRouteID[i].ToString()));
                                }
                            }
                            if (dtStartDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtStartDate[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@StartDate", dtStartDate[i].ToString()));
                                }
                            }
                            if (dtEndDate[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtEndDate[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@EndDate", dtEndDate[i].ToString()));
                                }
                            }

                            commIn.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                            if (dtFromBillID[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillID[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));
                                }
                            }
                            if (dtFromBillNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromBillNo[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", dtFromBillNo[i].ToString()));
                                }
                            }
                            if (dtFromLineNo[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(dtFromLineNo[i].ToString().Trim()))
                                {
                                    commIn.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", dtFromLineNo[i].ToString()));
                                }
                            }

                            listADD.Add(commIn);
                            #endregion
                        }
                    }
                }
            }
            #endregion


            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 生产任务单详细信息
        /// <summary>
        /// 生产任务单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTaskInfo(ManufactureTaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("				select	a.CompanyCD,a.ID,a.TaskNo,a.FromType,a.Subject,a.Principal,g.EmployeeName as PricipalReal,a.DocumentURL,");
            infoSql.AppendLine("						a.DeptID,f.DeptName,a.TaskType,a.ManufactureType,Convert(numeric(16,"+userInfo.SelPoint+"),a.CountTotal) as CountTotal ,");
            infoSql.AppendLine("                        case when a.ManufactureType=0 then '普通'  when a.ManufactureType=1 then '返修' when a.ManufactureType=2 then '拆件' end as strManufactureType,");
            infoSql.AppendLine("                        case when a.FromType=0 then '无来源' when a.FromType=1 then '主生产计划' end as strFromType,");
            infoSql.AppendLine("                        case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("						a.BillStatus,a.Creator,b.EmployeeName as CreatorReal,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("						a.Confirmor,c.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("						a.Closer,d.EmployeeName as CloserReal,isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,a.Remark,");
            infoSql.AppendLine("                        a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("						isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID,h.EmployeeName as ModifiedUserReal,a.ProjectID,p.ProjectName ");
            infoSql.AppendLine("				from officedba.ManufactureTask a");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as b on a.Creator=b.ID");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as c on a.Confirmor=c.ID");
            infoSql.AppendLine("            					left join officedba.EmployeeInfo as d on a.Closer=d.ID");
            infoSql.AppendLine("								left join officedba.EmployeeInfo as g on a.Principal=g.ID");
            infoSql.AppendLine("            					left join officedba.DeptInfo as f on a.DeptID=f.ID");
            infoSql.AppendLine("								left join officedba.UserInfo as i on a.ModifiedUserID=i.UserID");
            infoSql.AppendLine("								left join officedba.EmployeeInfo h on i.EmployeeID=h.ID");
            infoSql.AppendLine("                                left join officedba.ProjectInfo p on a.ProjectID=p.ID");
            infoSql.AppendLine(") as info");
            infoSql.AppendLine("Where ID=@ID");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 生产任务单明细详细信息
        /// <summary>
        /// 生产任务单明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTaskDetailInfo(ManufactureTaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("				select	a.CompanyCD,a.ID as DetailID,a.TaskNo,a.SortNo,a.ProductID,b.ProdNo,b.ProductName,");
            detSql.AppendLine("						b.Specification,b.UnitID,c.CodeName as UnitName,a.UsedUnitID,f.CodeName as UsedUnitName,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount,");
            detSql.AppendLine("						Convert(numeric(14," + userInfo.SelPoint+ "),a.ProductCount) as ProductCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.ProductedCount) as ProductedCount,");
            detSql.AppendLine("						a.BomID,d.BomNo,a.RouteID,e.RouteNo,");
            detSql.AppendLine("						isnull( CONVERT(CHAR(10), a.StartDate, 23),'') as StartDate,");
            detSql.AppendLine("						isnull( CONVERT(CHAR(10), a.EndDate, 23),'') as EndDate,");
            detSql.AppendLine("						a.FromType,a.FromBillID,a.FromBillNo,a.FromLineNo,Convert(numeric(14," + userInfo.SelPoint+ "),a.InCount) as InCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.ApplyCheckCount) as ApplyCheckCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.CheckedCount) as CheckedCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.PassCount) as PassCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.NotPassCount) as NotPassCount");
            detSql.AppendLine("				from officedba.ManufactureTaskDetail a");
            detSql.AppendLine("				left join officedba.ProductInfo b on b.ID=a.ProductID");
            detSql.AppendLine("				left join officedba.CodeUnitType c on b.UnitID=c.ID");
            detSql.AppendLine("				left join officedba.CodeUnitType f on a.UsedUnitID=f.ID");
            detSql.AppendLine("				left outer join officedba.Bom d on a.BomID=d.ID");
            detSql.AppendLine("				left outer join officedba.TechnicsRouting e on a.RouteID=e.ID");
            detSql.AppendLine("				where a.CompanyCD=@CompanyCD and TaskNo = (select top 1 TaskNo from officedba.ManufactureTask where ID=@ID)");
            detSql.AppendLine(") as info");
            detSql.AppendLine("where CompanyCD=@CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 源单总览：主生产计划明细
        /// <summary>
        /// 主生产计划明细
        /// </summary>
        /// <param name="strMasterID"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleFromPlan(string CompanyCD,string strPlanID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("	select  a.CompanyCD,a.ProductID,b.ProductName,b.ProdNo,b.UnitID,c.CodeName as UnitName,b.Specification,a.UsedUnitID,Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedUnitCount,0)) as UsedUnitCount,d.ID as BomID,d.BomNo,d.[Type] as BomType,");
            detSql.AppendLine("			Convert(numeric(14," + userInfo.SelPoint+ "),isnull(a.ProduceCount,0)) as ProduceCount,a.PlanNo,a.ID as DetailID,a.SortNo from officedba.MasterProductScheduleDetail a");
            detSql.AppendLine("	left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("	left join officedba.CodeUnitType c on b.UnitID=c.ID");
            detSql.AppendLine("	left join officedba.Bom d on a.ProductID=d.ProductID");
            detSql.AppendLine("	where a.CompanyCD=@CompanyCD and PlanNo in(select PlanNo from officedba.MasterProductSchedule where ID in(" + strPlanID + "))");
            detSql.AppendLine(")as info");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 通过检索条件查询生产任务单信息
        /// <summary>
        /// 通过检索条件查询生产任务单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition(ManufactureTaskModel model, int FlowStatus, int BillTypeFlag, int BillTypeCode, string CreateDate, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select  a.CompanyCD,");
            searchSql.AppendLine("		a.ID,");
            searchSql.AppendLine("		a.TaskNo,");
            searchSql.AppendLine("		a.Subject,");
            searchSql.AppendLine("		a.Principal,isnull(b.EmployeeName,'') as PrincipalReal,");
            searchSql.AppendLine("		a.DeptID,d.DeptName,");
            searchSql.AppendLine("		a.ManufactureType,p.ProjectName,isnull(a.ProjectID,0) as ProjectID,");
            searchSql.AppendLine("      case when a.ManufactureType=0 then '普通'  when a.ManufactureType=1 then '返修'  when a.ManufactureType=2 then '拆件' end as strManufactureType,");
            searchSql.AppendLine("      case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=3 then '变更' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatus,");
            searchSql.AppendLine("      case when e.FlowStatus=1 then '待审批' when e.FlowStatus=2 then '审批中' when e.FlowStatus=3 then '审批通过' when e.FlowStatus=4 then '审批不通过' when e.FlowStatus=5 then '撤消审批' end as strFlowStatus,");
            searchSql.AppendLine("	    isnull(e.FlowStatus,'0')as FlowStatus,");
            searchSql.AppendLine("      a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("		a.Creator,isnull(c.EmployeeName,'') as CreatorReal,");
            searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            searchSql.AppendLine("		a.BillStatus,a.ModifiedDate ");
            searchSql.AppendLine("from officedba.ManufactureTask a");
            searchSql.AppendLine("LEFT JOIN officedba.EmployeeInfo b on a.Principal=b.ID "); 
            searchSql.AppendLine("LEFT JOIN officedba.EmployeeInfo c on a.Creator=c.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.DeptInfo d on a.DeptID=d.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.ProjectInfo p on a.ProjectID=p.ID ");
            searchSql.AppendLine("LEFT JOIN officedba.FlowInstance e ON a.ID=e.BillID ");
            searchSql.AppendLine(" and e.BillTypeFlag=@BillTypeFlag");
            searchSql.AppendLine(" and e.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine(" and e.ID=( ");
            searchSql.AppendLine("                      select  max(ID)");
            searchSql.AppendLine("                      from  officedba.FlowInstance H");
            searchSql.AppendLine("                      where   H.CompanyCD = A.CompanyCD");
            searchSql.AppendLine("                      and H.BillID = A.ID");
            searchSql.AppendLine("                      and H.BillTypeFlag =@BillTypeFlag");
            searchSql.AppendLine("                      and H.BillTypeCode =@BillTypeCode)");

            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));

            //单据编号
            if (!string.IsNullOrEmpty(model.TaskNo))
            {
                searchSql.AppendLine("and a.TaskNo like @TaskNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + model.TaskNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and a.Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }
            //加工类型
            if (model.ManufactureType > -1)
            {
                searchSql.AppendLine(" and a.ManufactureType=@ManufactureType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ManufactureType", model.ManufactureType.ToString()));
            }
            //负责人
            if (model.Principal > 0)
            {
                searchSql.AppendLine(" and a.Principal=@Principal ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Principal", model.Principal.ToString()));
            }
            //部门
            if (model.DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID.ToString()));
            }
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and a.BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                }
            }
            //审批状态
            if (FlowStatus > -1)
            {
                searchSql.AppendLine(" and FlowStatus=@FlowStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus.ToString()));
            }

            //创建日期
            if (!string.IsNullOrEmpty(CreateDate))
            {
                searchSql.AppendLine(" and CreateDate=@CreateDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", CreateDate));
            }
            if (model.ProjectID > 0)
            {
                searchSql.AppendLine(" and a.ProjectID=@ProjectID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID.ToString()));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 删除主生产任务
        /// <summary>
        /// 删除主生产任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteManufactureTask(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.ManufactureTaskDetail where CompanyCD=@CompanyCD and TaskNo=(select top 1 TaskNo from officedba.ManufactureTask where CompanyCD=@CompanYCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.ManufactureTask where CompanyCD=@CompanyCD and ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlBom.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 修改：确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteManufactureTask(ManufactureTaskModel model, string loginUserID, int OperateType)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            //1：确认  2:结单  3:取消结单
            if (OperateType == 1)
            {
                DataTable dbTask = GetTaskInfo(model);
                if (dbTask.Rows.Count > 0)
                {
                    int BillStatus = int.Parse(dbTask.Rows[0]["BillStatus"].ToString());
                    if (BillStatus == 1)
                    {
                        #region 更新相关地方的数据
                        //注：单据确认后自动更新对应主生产计划明细中的已下达数量
                        //    更新对应的物品在分仓存量表（分仓存量表officedba.StorageProduct）中在途量（增加），
                        //    更新存量时，先根据物品从物品档案表中获取该物品对应的主放仓库ID，再更新分仓存量表中对应的物品+仓库的在途量

                        DataTable dtTaskDetail = new DataTable();
                        dtTaskDetail = GetTaskDetailInfo(model);
                        if (dtTaskDetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTaskDetail.Rows.Count; i++)
                            {
                                Decimal ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["ProductCount"].ToString()));
                                if (userInfo.IsMoreUnit)
                                {
                                    ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["UsedUnitCount"].ToString()));
                                }
                                int ProductID = int.Parse(dtTaskDetail.Rows[i]["ProductID"].ToString());

                                if (!string.IsNullOrEmpty(dtTaskDetail.Rows[i]["FromBillID"].ToString()))
                                {
                                    int FromBillID = int.Parse(dtTaskDetail.Rows[i]["FromBillID"].ToString());
                                    string FromBillNo = dtTaskDetail.Rows[i]["FromBillNo"].ToString();


                                    if (FromBillID > 0)
                                    {
                                        #region  更新主生产计划中已下达数量语句
                                        //update officedba.MasterProductSchedule set PlanCount=PlanCount+@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID
                                        StringBuilder sqlPlan = new StringBuilder();
                                        sqlPlan.AppendLine("Update officedba.MasterProductScheduleDetail set PlanCount=isnull(PlanCount,0)+@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID");

                                        SqlCommand commPlan = new SqlCommand();
                                        commPlan.CommandText = sqlPlan.ToString();
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));

                                        listADD.Add(commPlan);
                                        #endregion


                                    }
                                }
                                #region 更新分仓存量表里的在途量
                                //Update officedba.StorageProduct Set RoadCount=RoadCount+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)
                                StringBuilder sqlStoPro = new StringBuilder();
                                sqlStoPro.AppendLine("Update officedba.StorageProduct Set RoadCount=isnull(RoadCount,0)+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)");

                                SqlCommand commStoPro = new SqlCommand();
                                commStoPro.CommandText = sqlStoPro.ToString();
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));

                                listADD.Add(commStoPro);
                                #endregion
                            }
                        }
                        #endregion

                        #region 更新主表
                        StringBuilder sqlTask = new StringBuilder();
                        sqlTask.AppendLine(" UPDATE officedba.ManufactureTask SET");
                        sqlTask.AppendLine(" Confirmor         = @Confirmor,");
                        sqlTask.AppendLine(" ConfirmDate        = @ConfirmDate,");
                        sqlTask.AppendLine(" ModifiedDate   = getdate(),");
                        sqlTask.AppendLine(" BillStatus   = 2,");
                        sqlTask.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                        sqlTask.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                        SqlCommand commTask = new SqlCommand();
                        commTask.CommandText = sqlTask.ToString();
                        commTask.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                        commTask.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                        commTask.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));
                        commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                        listADD.Add(commTask);
                        #endregion
                    }
                }

            }
            else if (OperateType == 2)
            {
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine(" UPDATE officedba.ManufactureTask SET");
                sqlTask.AppendLine(" Closer         = @Closer,");
                sqlTask.AppendLine(" CloseDate   = @CloseDate,");
                sqlTask.AppendLine(" BillStatus   = 4,");
                sqlTask.AppendLine(" ModifiedDate   = getdate(),");
                sqlTask.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sqlTask.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                SqlCommand commTask = new SqlCommand();
                commTask.CommandText = sqlTask.ToString();
                commTask.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTask.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                commTask.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate));
                commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTask);
            }
            else
            {
                StringBuilder sqlTask = new StringBuilder();
                sqlTask.AppendLine(" update officedba.ManufactureTask set Closer=null,CloseDate=null,ModifiedDate=getdate(),BillStatus=2,ModifiedUserID = '" + loginUserID + "'");
                sqlTask.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");

                SqlCommand commTask = new SqlCommand();
                commTask.CommandText = sqlTask.ToString();
                commTask.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                listADD.Add(commTask);
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 单据是否被引用
        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrence(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in(select top 1 TaskNo from officedba.ManufactureTask where ID in("+ID+"))";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region 单据是否被领料单引用
        /// <summary>
        /// 单据是否被领料单引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrenceTakeMaterial(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in("+ID+")";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region 根据MRP编号和明细ID查找明细的源单编号和源单ID
        /// <summary>
        /// 根据MRP编号和明细ID查找明细的源单编号和源单ID 
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="MRPNo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetMRPDetail_ByMRPNoID(string CompanyCD, string MRPNo,int ID)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select Top 1 FromBillNo,FromBillID from officedba.MRPDetail where CompanyCD=@CompanyCD and MRPNo=@MRPNo and ID=@ID");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MRPNo", MRPNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));
            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 取消确认
        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool CancelConfirmOperate(ManufactureTaskModel model, int BillTypeFlag, int BillTypeCode, string loginUserID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();

            //#region 传参
            try
            {
                DataTable dbTask = GetTaskInfo(model);
                if (dbTask.Rows.Count > 0)
                {
                    int BillStatus = int.Parse(dbTask.Rows[0]["BillStatus"].ToString());
                    if (BillStatus == 2)
                    {
                        #region 撤消审批流程
                        #region 撤消审批处理逻辑描述
                        //可参见撤消审批的存储过程[FlowApproval_Update],个别的判断去掉

                        //--1.往流程任务历史记录表（officedba.FlowTaskHistory）插1条处理记录，
                        //--记录的步骤序号为0（表示返回到流程提交人环节)，审批状态为撤销审批   
                        //Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)
                        //Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())

                        //--2.更新流程任务处理表（officedba.FlowTaskList）中的流程步骤序号为0（表示返回到流程提交人环节）
                        //Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID
                        //Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID

                        //--3更新流程实例表（officedba.FlowInstance）中的流程状态为“撤销审批”
                        //Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID 
                        //Where CompanyCD=@CompanyCD 
                        //and FlowNo=@tempFlowNo 
                        //and BillTypeFlag=@BillTypeFlag 
                        //and BillTypeCode=@BillTypeCode 
                        //and BillID=@BillID
                        #endregion


                        DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(model.CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
                        if (dtFlowInstance.Rows.Count > 0)
                        {
                            //提交审批了的单据
                            string FlowInstanceID = dtFlowInstance.Rows[0]["FlowInstanceID"].ToString();
                            string FlowStatus = dtFlowInstance.Rows[0]["FlowStatus"].ToString();
                            string FlowNo = dtFlowInstance.Rows[0]["FlowNo"].ToString();

                            #region 往流程任务历史记录表
                            StringBuilder sqlHis = new StringBuilder();
                            sqlHis.AppendLine("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)");
                            sqlHis.AppendLine("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");


                            SqlCommand commHis = new SqlCommand();
                            commHis.CommandText = sqlHis.ToString();
                            commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                            commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(commHis);
                            #endregion

                            #region 更新流程任务处理表
                            StringBuilder sqlTask = new StringBuilder();
                            sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                            sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                            SqlCommand commTask = new SqlCommand();
                            commTask.CommandText = sqlTask.ToString();
                            commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(commTask);
                            #endregion

                            #region 更新流程实例表
                            StringBuilder sqlIns = new StringBuilder();
                            sqlIns.AppendLine("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
                            sqlIns.AppendLine("Where CompanyCD=@CompanyCD ");
                            sqlIns.AppendLine("and FlowNo=@tempFlowNo ");
                            sqlIns.AppendLine("and BillTypeFlag=@BillTypeFlag ");
                            sqlIns.AppendLine("and BillTypeCode=@BillTypeCode ");
                            sqlIns.AppendLine("and BillID=@BillID");


                            SqlCommand commIns = new SqlCommand();
                            commIns.CommandText = sqlIns.ToString();
                            commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                            commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(commIns);
                            #endregion

                        }
                        #endregion

                        #region  处理自己的业务逻辑
                        #region 更新相关地方的数据
                        //注：单据确认后自动更新对应主生产计划明细中的已下达数量
                        //    更新对应的物品在分仓存量表（分仓存量表officedba.StorageProduct）中在途量（增加），
                        //    更新存量时，先根据物品从物品档案表中获取该物品对应的主放仓库ID，再更新分仓存量表中对应的物品+仓库的在途量


                        DataTable dtTaskDetail = new DataTable();
                        dtTaskDetail = GetTaskDetailInfo(model);
                        if (dtTaskDetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTaskDetail.Rows.Count; i++)
                            {
                                Decimal ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["ProductCount"].ToString()));
                                if (userInfo.IsMoreUnit)
                                {
                                    ProductCount = Decimal.Round(Decimal.Parse(dtTaskDetail.Rows[i]["UsedUnitCount"].ToString()));
                                }
                                int ProductID = int.Parse(dtTaskDetail.Rows[i]["ProductID"].ToString());

                                if (!string.IsNullOrEmpty(dtTaskDetail.Rows[i]["FromBillID"].ToString()))
                                {
                                    int FromBillID = int.Parse(dtTaskDetail.Rows[i]["FromBillID"].ToString());
                                    string FromBillNo = dtTaskDetail.Rows[i]["FromBillNo"].ToString();


                                    if (FromBillID > 0)
                                    {
                                        #region  更新主生产计划中已下达数量语句
                                        //update officedba.MasterProductSchedule set PlanCount=PlanCount+@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID
                                        StringBuilder sqlPlan = new StringBuilder();
                                        sqlPlan.AppendLine("Update officedba.MasterProductScheduleDetail set PlanCount=isnull(PlanCount,0)-@ProductCount where CompanyCD=@CompanyCD and PlanNo=@FromBillNo and ID=@FromBillID");

                                        SqlCommand commPlan = new SqlCommand();
                                        commPlan.CommandText = sqlPlan.ToString();
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", FromBillNo));
                                        commPlan.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));

                                        listADD.Add(commPlan);
                                        #endregion
                                    }
                                }
                                #region 更新分仓存量表里的在途量
                                //Update officedba.StorageProduct Set RoadCount=RoadCount+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)
                                StringBuilder sqlStoPro = new StringBuilder();
                                sqlStoPro.AppendLine("Update officedba.StorageProduct Set RoadCount=isnull(RoadCount,0)-@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=(select StorageID from officedba.ProductInfo where CompanyCD=@CompanyCD and ID=@ProductID)");

                                SqlCommand commStoPro = new SqlCommand();
                                commStoPro.CommandText = sqlStoPro.ToString();
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductCount", ProductCount));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commStoPro.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));

                                listADD.Add(commStoPro);
                                #endregion
                            }
                        }
                        #endregion

                        StringBuilder sqlConfirm = new StringBuilder();
                        sqlConfirm.AppendLine(" UPDATE officedba.ManufactureTask SET");
                        sqlConfirm.AppendLine(" Confirmor         = null,");
                        sqlConfirm.AppendLine(" ConfirmDate        = null,");
                        sqlConfirm.AppendLine(" ModifiedDate   = getdate(),");
                        sqlConfirm.AppendLine(" BillStatus   = 1,");
                        sqlConfirm.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                        sqlConfirm.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

                        SqlCommand commConfirm = new SqlCommand();
                        commConfirm.CommandText = sqlConfirm.ToString();
                        commConfirm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                        commConfirm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

                        listADD.Add(commConfirm);
                        #endregion
                    }
                }
                return SqlHelper.ExecuteTransWithArrayList(listADD);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(ManufactureTaskModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.ManufactureTask set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND TaskNo = @TaskNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@TaskNo", model.TaskNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 运营模式：(生产任务单执行汇总表)
        /// <summary>
        /// 通过检索条件查询生产任务单执行汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition_Operating(string CompanyCD, int DeptID, string ConfirmDateStart, string ConfirmDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select");
            searchSql.AppendLine("		a.ID,a.CompanyCD,b.DeptID,c.DeptName,");
            searchSql.AppendLine("		b.Principal,d.EmployeeName,b.TaskNo,");
            searchSql.AppendLine("		b.ConfirmDate,Convert(numeric(16," + point + "),a.ProductCount) as ProductCount,Convert(char(20),Convert(numeric(16," + point + "),a.ProductCount))+'&nbsp;' as ProductCount1,Convert(numeric(10," + point + "),a.ProductedCount) as ProductedCount,Convert(char(20),Convert(numeric(16," + point + "),a.ProductedCount))+'&nbsp;' as ProductedCount1,");
            searchSql.AppendLine("		Convert(numeric(16," + point + "),a.InCount) as InCount,Convert(char(20),Convert(numeric(16," + point + "),a.InCount))+'&nbsp;' as InCount1,Convert(numeric(16," + point + "),a.NotPassCount) as NotPassCount,Convert(char(20),Convert(numeric(16," + point + "),a.NotPassCount))+'&nbsp;' as NotPassCount1,Convert(numeric(14," + point + "),TaskInfo.WorkTimeTotals)WorkTimeTotals,Convert(char(20),Convert(numeric(14," + point + "),TaskInfo.WorkTimeTotals))+'&nbsp;' as WorkTimeTotals1,b.BillStatus");
            searchSql.AppendLine("	from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("	left join officedba.ManufactureTask b on a.TaskNo=b.TaskNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on b.DeptID=c.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo d on b.Principal=d.ID");
            searchSql.AppendLine("	left join (");
            searchSql.AppendLine("				select sum(WorkTimeTotal) as WorkTimeTotals,g.id from");
            searchSql.AppendLine("				officedba.ManufactureReport  f,officedba.ManufactureTaskDetail g");
            searchSql.AppendLine("				where g.TaskNo=f.TaskNo group by g.id");
            searchSql.AppendLine("	) as TaskInfo on a.ID=TaskInfo.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info where CompanyCD=@CompanyCD and BillStatus=2 ");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--生产部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID.ToString()));
            }
            //--确认起始日期
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateStart", ConfirmDateStart));
            }
            //--确认截止日期
            if (!string.IsNullOrEmpty(ConfirmDateEnd))
            {
                searchSql.AppendLine(" and ConfirmDate<=@ConfirmDateEnd ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateEnd", ConfirmDateEnd));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产任务单执行汇总表)
        /// <summary>
        /// 通过检索条件查询打印生产任务单执行汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskListBycondition_Operating_Print(string CompanyCD, int DeptID, string ConfirmDateStart, string ConfirmDateEnd, string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select");
            searchSql.AppendLine("		a.ID,a.CompanyCD,b.DeptID,c.DeptName,");
            searchSql.AppendLine("		b.Principal,d.EmployeeName,b.TaskNo,");
            searchSql.AppendLine("		b.ConfirmDate,a.ProductCount,a.ProductedCount,");
            searchSql.AppendLine("		a.InCount,a.NotPassCount,TaskInfo.WorkTimeTotals,b.BillStatus");
            searchSql.AppendLine("	from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("	left join officedba.ManufactureTask b on a.TaskNo=b.TaskNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on b.DeptID=c.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo d on b.Principal=d.ID");
            searchSql.AppendLine("	left join (");
            searchSql.AppendLine("				select sum(WorkTimeTotal) as WorkTimeTotals,g.id from");
            searchSql.AppendLine("				officedba.ManufactureReport  f,officedba.ManufactureTaskDetail g");
            searchSql.AppendLine("				where g.TaskNo=f.TaskNo group by g.id");
            searchSql.AppendLine("	) as TaskInfo on a.ID=TaskInfo.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info where CompanyCD=@CompanyCD and BillStatus=2 ");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--生产部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and DeptID=@DeptID");
                arr.Add(new SqlParameter("@DeptID", DeptID.ToString()));

            }
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                arr.Add(new SqlParameter("@ConfirmDateStart", ConfirmDateStart));
            }
            //发料截止日期
            if (!string.IsNullOrEmpty(ConfirmDateEnd))
            {
                searchSql.AppendLine(" and ConfirmDate<=@ConfirmDateEnd ");
                arr.Add(new SqlParameter("@ConfirmDateEnd", ConfirmDateEnd));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(在制品存量统计表)
        /// <summary>
        /// 在制品存量统计表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductBycondition_Operating(string CompanyCD, int ProductID,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct a.CompanyCD,stoInfo.* from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,");
            searchSql.AppendLine("					info.ProductCounts,info.CanUseCounts,info.OrderCounts,info.RoadCounts,info.OutCounts,info.ProductCounts1,info.CanUseCounts1,info.OrderCounts1,info.RoadCounts1,info.OutCounts1 from (");
            searchSql.AppendLine("							select	ProductID,");
            searchSql.AppendLine("									Convert(numeric(12,"+point+"),isnull(sum(ProductCount),0)) as ProductCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),(Convert(numeric(12," + point + "),isnull(sum(ProductCount),0)))+");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OutCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)))) as CanUseCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)) as OrderCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)) as RoadCounts,");
            searchSql.AppendLine("									Convert(numeric(12," + point + "),isnull(sum(OutCount),0)) as OutCounts,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(ProductCount),0)))+'&nbsp;' as ProductCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),(Convert(numeric(12," + point + "),isnull(sum(ProductCount),0)))+");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OutCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)))))+'&nbsp;' as CanUseCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(OrderCount),0)))+'&nbsp;' as OrderCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))+'&nbsp;' as RoadCounts1,");
            searchSql.AppendLine("									Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(OutCount),0)))+'&nbsp;' as OutCounts1");
            searchSql.AppendLine("							from officedba.StorageProduct");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");





            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--物品
            if (ProductID > 0)
            {
                searchSql.AppendLine(" and stoInfo.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印在制品存量统计表)
        /// <summary>
        /// 打印在制品存量统计表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductBycondition_Operating_Print(string CompanyCD, int ProductID,string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct a.CompanyCD,stoInfo.* from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,");
            searchSql.AppendLine("					info.ProductCounts,info.CanUseCounts,info.OrderCounts,info.RoadCounts,info.OutCounts from (");
            searchSql.AppendLine("							select	ProductID,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(ProductCount),0)) as ProductCounts,");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(ProductCount),0)))+");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(RoadCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(OutCount),0)))-");
            searchSql.AppendLine("									(Convert(numeric(12,2),isnull(sum(OrderCount),0))) as CanUseCounts,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(OrderCount),0)) as OrderCounts,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(RoadCount),0)) as RoadCounts,");
            searchSql.AppendLine("									Convert(numeric(12,2),isnull(sum(OutCount),0)) as OutCounts");
            searchSql.AppendLine("							from officedba.StorageProduct");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--物品
            if (ProductID > 0)
            {
                searchSql.AppendLine(" and stoInfo.ProductID=@ProductID ");
                arr.Add(new SqlParameter("@ProductID", ProductID));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ProductID ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(在制品价值汇总表)
        /// <summary>
        /// 在制品价值汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductValueBycondition_Operating(string CompanyCD, int ProductID,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select distinct a.CompanyCD,stoInfo.*");
            searchSql.AppendLine(" from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,info.RoadCounts,info.RoadCounts1,");
            searchSql.AppendLine("				    Convert(numeric(12," + point + "),isnull(a.StandardSell,0)) as StandardSell,Convert(char(20),Convert(numeric(12," + point + "),isnull(a.StandardSell,0)))+'&nbsp;' as StandardSell1,");
            searchSql.AppendLine("					Convert(numeric(12," + point + "),Convert(numeric(12," + point + "),isnull(a.StandardSell,0))*info.RoadCounts) as SellPrince,Convert(char(20),Convert(numeric(12," + point + "),Convert(numeric(12," + point + "),isnull(a.StandardSell,0))*info.RoadCounts))+'&nbsp;' as SellPrince1");
            searchSql.AppendLine("			 from (");
            searchSql.AppendLine("							select	ProductID,Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)) as RoadCounts,Convert(char(20),Convert(numeric(12," + point + "),isnull(sum(RoadCount),0)))+'&nbsp;' as RoadCounts1");
            searchSql.AppendLine("							from officedba.StorageProduct where CompanyCD=@CompanyCD");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo ");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");
            searchSql.AppendLine("and ConfirmDate is not null");






            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            if (ProductID > 0)
            {
                searchSql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印在制品价值汇总表)
        /// <summary>
        /// 在制品价值汇总表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CheckDateStart"></param>
        /// <param name="CheckDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetRoadStorageProductValueBycondition_Operating_Print(string CompanyCD, int ProductID,string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select distinct a.CompanyCD,stoInfo.*");
            searchSql.AppendLine(" from officedba.ManufactureTaskDetail a");
            searchSql.AppendLine("left join (");
            searchSql.AppendLine("			select	info.ProductID,a.ProdNo,a.ProductName,a.Specification,b.CodeName as UnitName,info.RoadCounts,");
            searchSql.AppendLine("				    Convert(numeric(12,2),isnull(a.StandardSell,0)) as StandardSell,");
            searchSql.AppendLine("					Convert(numeric(12,2),Convert(numeric(12,2),isnull(a.StandardSell,0))*info.RoadCounts) as SellPrince");
            searchSql.AppendLine("			 from (");
            searchSql.AppendLine("							select	ProductID,Convert(numeric(12,2),isnull(sum(RoadCount),0)) as RoadCounts");
            searchSql.AppendLine("							from officedba.StorageProduct where CompanyCD=@CompanyCD");
            searchSql.AppendLine("							group by ProductID");
            searchSql.AppendLine("			) as info");
            searchSql.AppendLine("			left join officedba.ProductInfo a on info.ProductID=a.ID");
            searchSql.AppendLine("			left join officedba.CodeUnitType b on a.UnitID=b.ID");
            searchSql.AppendLine(") as stoInfo ");
            searchSql.AppendLine("on a.ProductID=stoInfo.ProductID");
            searchSql.AppendLine("left join officedba.ManufactureTask c on a.TaskNo=c.TaskNo");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("and c.BillStatus=2");
            //searchSql.AppendLine("and (a.InCount is null or a.InCount <ProductCount)");
            searchSql.AppendLine("and ConfirmDate is not null");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            if (ProductID > 0)
            {
                searchSql.AppendLine(" and a.ProductID=@ProductID ");
                arr.Add(new SqlParameter("@ProductID", ProductID.ToString()));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ProductID ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(生产日报表)
        /// <summary>
        /// 生产日报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskDateBycondition_Operating(string CompanyCD, int DeptID,string theDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.CompanyCD,e.DeptName,a.DeptID,c.ProdNo,c.ProductName,d.CodeName as UnitName,c.Specification,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),isnull(b.FinishNum,0)) as FinishNum,Convert(char(20),Convert(numeric(12," + point + "),isnull(b.FinishNum,0)))+'&nbsp;' FinishNum1,Convert(numeric(20," + point + "),b.WorkTime)WorkTime,Convert(char(50),Convert(numeric(20," + point + "),b.WorkTime))+'&nbsp;' WorkTime1,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),b.FinishNum/b.WorkTime) as ProductionTotal,Convert(char(20),Convert(numeric(12," + point + "),b.FinishNum/b.WorkTime))+'&nbsp;' ProductionTotal1,");
            searchSql.AppendLine("		CONVERT(CHAR(10), a.ConfirmDate, 23)as ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReport a ");
            searchSql.AppendLine("left join officedba. ManufactureReportProduct b on a.ReportNo=b.ReportNo");
            searchSql.AppendLine("left join officedba.ProductInfo c on b.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID");
            searchSql.AppendLine("left join officedba.DeptInfo e on a.DeptID=e.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID.ToString()));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(theDate))
            {

                searchSql.AppendLine("and CONVERT(CHAR(10), a.ConfirmDate, 23)=@TheDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TheDate", theDate));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产日报表)
        /// <summary>
        /// 打印生产日报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theDate"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskDateBycondition_Operating_Print(string CompanyCD, int DeptID,string theDate, string orderColumn, string orderType)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.CompanyCD,e.DeptName,a.DeptID,c.ProdNo,c.ProductName,c.Specification,d.CodeName as UnitName,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),isnull(b.FinishNum,0)) as FinishNum,b.WorkTime,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),b.FinishNum/b.WorkTime) as ProductionTotal,");
            searchSql.AppendLine("		CONVERT(CHAR(10), a.ConfirmDate, 23)as ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReport a ");
            searchSql.AppendLine("left join officedba. ManufactureReportProduct b on a.ReportNo=b.ReportNo");
            searchSql.AppendLine("left join officedba.ProductInfo c on b.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID");
            searchSql.AppendLine("left join officedba.DeptInfo e on a.DeptID=e.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("and ConfirmDate is not null");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                arr.Add(new SqlParameter("@DeptID", DeptID));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(theDate))
            {
                searchSql.AppendLine(" and CONVERT(CHAR(10), a.ConfirmDate, 23)=@TheDate");
                arr.Add(new SqlParameter("@TheDate", theDate));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine(" order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 运营模式：(生产月报表)
        /// <summary>
        /// 生产月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="theMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskMonthBycondition_Operating(string CompanyCD, int DeptID, string QueryDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.ID,g.DeptID,h.DeptName,a.ProductID,c.ProdNo,c.ProductName,");
            searchSql.AppendLine("		c.Specification,d.CodeName as UnitName,Convert(numeric(12," + point + "),f.ProductCount)  as OrderCount,Convert(char(20),Convert(numeric(14," + point + "),f.ProductCount))+'&nbsp;'  as OrderCount1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),b.ProductCount)  as ProductCount,Convert(char(20),Convert(numeric(12," + point + "),b.ProductCount))+'&nbsp;'  as ProductCount1,Convert(numeric(14," + point + "),b.ProductedCount)  as ProductedCount,Convert(char(20),Convert(numeric(14," + point + "),b.ProductedCount))+'&nbsp;'  as ProductedCount1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),(b.ProductCount-isnull(b.ProductedCount,0))) as UnFinishcount,Convert(char(20),Convert(numeric(14," + point + "),(b.ProductCount-isnull(b.ProductedCount,0))))+'&nbsp;' as UnFinishcount1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),((b.ProductedCount/b.ProductCount)*100)) as FinishPercent,Convert(char(20),Convert(numeric(14," + point + "),((b.ProductedCount/b.ProductCount)*100)))+'&nbsp;' as FinishPercent1,");
            searchSql.AppendLine("		Convert(numeric(20," + point + "),a.WorkTime)WorkTime,Convert(char(50),Convert(numeric(20," + point + "),a.WorkTime))+'&nbsp;' WorkTime1,");
            searchSql.AppendLine("		Convert(numeric(14," + point + "),(a.FinishNum/a.WorkTime )) as ChanLiang,Convert(char(20),Convert(numeric(14," + point + "),(a.FinishNum/a.WorkTime )))+'&nbsp;' as ChanLiang1,");
            searchSql.AppendLine("		e.FromBillNo,e.FromBillId,");
            searchSql.AppendLine("		g.ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReportProduct a");
            searchSql.AppendLine("left join officedba.ManufactureTaskDetail b on a.FromBillNo=b.TaskNo and a.FromBillID=b.ID");
            searchSql.AppendLine("left join officedba.MasterProductScheduleDetail  e on b.FromBillNo=e.PlanNo and b.FromBillID=e.ID");
            searchSql.AppendLine("left join officedba.SellOrderDetail f on e.FromBillNo=f.OrderNo and e.FromBillID=f.ID");
            searchSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID ");
            searchSql.AppendLine("left join officedba.ManufactureReport g on a.ReportNo=g.ReportNo");
            searchSql.AppendLine("left join officedba.DeptInfo h on g.DeptID=h.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and g.ConfirmDate is not null");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //--公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and g.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID.ToString()));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(QueryDate))
            {

                searchSql.AppendLine("and ConfirmDate>=@QueryDate and ConfirmDate <dateadd(month,1,@QueryDate)");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QueryDate", QueryDate));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 运营模式：(打印生产月报表)
        /// <summary>
        /// 打印生产月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="QueryDate"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureTaskMonthBycondition_Operating_Print(string CompanyCD, int DeptID, string QueryDate, string orderColumn, string orderType)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select	a.ID,g.DeptID,h.DeptName,a.ProductID,c.ProdNo,c.ProductName,");
            searchSql.AppendLine("		c.Specification,d.CodeName as UnitName,f.ProductCount  as OrderCount,");
            searchSql.AppendLine("		b.ProductCount,b.ProductedCount,");
            searchSql.AppendLine("		(b.ProductCount-Convert(numeric(12," + point + "),isnull(b.ProductedCount,0))) as UnFinishcount,");
            searchSql.AppendLine("		Convert(numeric(12," + point + "),(Convert(numeric(12," + point + "),b.ProductedCount)/b.ProductCount)*100) as FinishPercent,");
            searchSql.AppendLine("		a.WorkTime,");
            searchSql.AppendLine("		(a.FinishNum/a.WorkTime ) as ChanLiang,");
            searchSql.AppendLine("		e.FromBillNo,e.FromBillId,");
            searchSql.AppendLine("		g.ConfirmDate");
            searchSql.AppendLine("from officedba.ManufactureReportProduct a");
            searchSql.AppendLine("left join officedba.ManufactureTaskDetail b on a.FromBillNo=b.TaskNo and a.FromBillID=b.ID");
            searchSql.AppendLine("left join officedba.MasterProductScheduleDetail  e on b.FromBillNo=e.PlanNo and b.FromBillID=e.ID");
            searchSql.AppendLine("left join officedba.SellOrderDetail f on e.FromBillNo=f.OrderNo and e.FromBillID=f.ID");
            searchSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
            searchSql.AppendLine("left join officedba.CodeUnitType d on c.UnitID=d.ID ");
            searchSql.AppendLine("left join officedba.ManufactureReport g on a.ReportNo=g.ReportNo");
            searchSql.AppendLine("left join officedba.DeptInfo h on g.DeptID=h.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and g.ConfirmDate is not null");


            #endregion

            ArrayList arr = new ArrayList();
            //--公司编码
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            //--部门
            if (DeptID > 0)
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                arr.Add(new SqlParameter("@DeptID", DeptID));
            }
            //--查询日期
            if (!string.IsNullOrEmpty(QueryDate))
            {

                searchSql.AppendLine("and ConfirmDate>=@QueryDate and ConfirmDate <dateadd(month,1,@QueryDate)");
                arr.Add(new SqlParameter("@QueryDate", QueryDate));
            }

            if (!string.IsNullOrEmpty(orderColumn))
            {
                searchSql.AppendLine("order by " + orderColumn);
            }
            else
            {
                searchSql.AppendLine(" order by ConfirmDate ");
            }
            if (!string.IsNullOrEmpty(orderType))
            {
                searchSql.AppendLine("  " + orderType);
            }

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion


    }
}
