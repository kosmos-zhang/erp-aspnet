/**********************************************
 * 类作用：  生产任务汇报单数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/27
 * 修改日期：2009/05/04
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
    public class ManufactureReportDBHelper
    {
        #region 生产任务汇报单插入
        /// <summary>
        /// 生产任务汇报单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureReport(ManufactureReportModel model, Hashtable htExtAttr, ManufactureReportProductModel modelProduct, ManufactureReportStaffModel modelStaff, ManufactureReportMachineModel modelMachine, ManufactureReportMeterialModel modelMeterial, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  生产任务汇报单添加SQL语句
                StringBuilder sqlReport = new StringBuilder();
                sqlReport.AppendLine("INSERT INTO officedba.ManufactureReport");
                sqlReport.AppendLine("           (CompanyCD");
                sqlReport.AppendLine("           ,ReportNo");
                sqlReport.AppendLine("           ,Subject");
                sqlReport.AppendLine("           ,TaskNo");
                sqlReport.AppendLine("           ,DeptID");
                sqlReport.AppendLine("           ,DailyDate");
                if (model.PlanHRs>0)
                {
                    sqlReport.AppendLine("           ,PlanHRs");
                }
                if (model.RealHRs>0)
                {
                    sqlReport.AppendLine("           ,RealHRs");
                }
                if (model.PlanWorkTime>0)
                {
                    sqlReport.AppendLine("           ,PlanWorkTime");
                }
                if (model.AddWorkTime>0)
                {
                    sqlReport.AppendLine("           ,AddWorkTime");
                }
                if (model.StopWorkTime>0)
                {
                    sqlReport.AppendLine("           ,StopWorkTime");
                }
                if (model.RealWorktime>0)
                {
                    sqlReport.AppendLine("           ,RealWorktime");
                }
                if (model.MachineCount>0)
                {
                    sqlReport.AppendLine("           ,MachineCount");
                }
                if (model.OpenCount>0)
                {
                    sqlReport.AppendLine("           ,OpenCount ");
                }
                if (model.OpenTime>0)
                {
                    sqlReport.AppendLine("           ,OpenTime");
                }
                if (model.OpenPercent>0)
                {
                    sqlReport.AppendLine("           ,OpenPercent");
                }
                if (model.LoadPercent>0)
                {
                    sqlReport.AppendLine("           ,LoadPercent");
                }
                if (model.UsePercent>0)
                {
                    sqlReport.AppendLine("           ,UsePercent");
                }
                if (model.StopCount>0)
                {
                    sqlReport.AppendLine("           ,StopCount");
                }
                if (model.StopTime>0)
                {
                    sqlReport.AppendLine("           ,StopTime");
                }
                sqlReport.AppendLine("           ,StopReason");
                if (model.ProductionTotal>0)
                {
                    sqlReport.AppendLine("           ,ProductionTotal");
                }
                if (model.WorkTimeTotal>0)
                {
                    sqlReport.AppendLine("           ,WorkTimeTotal");
                }
                sqlReport.AppendLine("           ,Reporter");
                sqlReport.AppendLine("           ,ReportDate");
                if (model.TakeNum>0)
                {
                    sqlReport.AppendLine("           ,TakeNum");
                }
                if (model.UsedNum>0)
                {
                    sqlReport.AppendLine("           ,UsedNum ");
                }
                if (model.NowNum>0)
                {
                    sqlReport.AppendLine("           ,NowNum");
                }
                sqlReport.AppendLine("           ,Remark");
                sqlReport.AppendLine("           ,BillStatus");
                sqlReport.AppendLine("           ,Creator");
                sqlReport.AppendLine("           ,CreateDate");
                sqlReport.AppendLine("           ,ModifiedDate");
                sqlReport.AppendLine("           ,ModifiedUserID)");
                sqlReport.AppendLine("     VALUES  ");
                sqlReport.AppendLine("           (@CompanyCD ");
                sqlReport.AppendLine("           ,@ReportNo");
                sqlReport.AppendLine("           ,@Subject");
                sqlReport.AppendLine("           ,@TaskNo");
                sqlReport.AppendLine("           ,@DeptID ");
                sqlReport.AppendLine("           ,@DailyDate  ");
                if (model.PlanHRs>0)
                {
                    sqlReport.AppendLine("           ,@PlanHRs");
                }
                if (model.RealHRs>0)
                {
                    sqlReport.AppendLine("           ,@RealHRs");
                }
                if (model.PlanWorkTime>0)
                {
                    sqlReport.AppendLine("           ,@PlanWorkTime");
                }
                if (model.AddWorkTime>0)
                {
                    sqlReport.AppendLine("           ,@AddWorkTime");
                }
                if (model.StopWorkTime>0)
                {
                    sqlReport.AppendLine("           ,@StopWorkTime");
                }
                if (model.RealWorktime>0)
                {
                    sqlReport.AppendLine("           ,@RealWorktime");
                }
                if (model.MachineCount>0)
                {
                    sqlReport.AppendLine("           ,@MachineCount");
                }
                if (model.OpenCount>0)
                {
                    sqlReport.AppendLine("           ,@OpenCount");
                }
                if (model.OpenTime>0)
                {
                    sqlReport.AppendLine("           ,@OpenTime");
                }
                if (model.OpenPercent>0)
                {
                    sqlReport.AppendLine("           ,@OpenPercent");
                }
                if (model.LoadPercent>0)
                {
                    sqlReport.AppendLine("           ,@LoadPercent");
                }
                if (model.UsePercent>0)
                {
                    sqlReport.AppendLine("           ,@UsePercent");
                }
                if (model.StopCount>0)
                {
                    sqlReport.AppendLine("           ,@StopCount");
                }
                if (model.StopTime>0)
                {
                    sqlReport.AppendLine("           ,@StopTime");
                }
                sqlReport.AppendLine("           ,@StopReason");
                if (model.ProductionTotal>0)
                {
                    sqlReport.AppendLine("           ,@ProductionTotal");
                }
                if (model.WorkTimeTotal>0)
                {
                    sqlReport.AppendLine("           ,@WorkTimeTotal");
                }
                sqlReport.AppendLine("           ,@Reporter");
                sqlReport.AppendLine("           ,@ReportDate");
                if (model.TakeNum>0)
                {
                    sqlReport.AppendLine("           ,@TakeNum");
                }
                if (model.UsedNum>0)
                {
                    sqlReport.AppendLine("           ,@UsedNum");
                }
                if (model.NowNum>0)
                {
                    sqlReport.AppendLine("           ,@NowNum");
                }
                sqlReport.AppendLine("           ,@Remark ");
                sqlReport.AppendLine("           ,1");
                sqlReport.AppendLine("           ,@Creator");
                sqlReport.AppendLine("           ,@CreateDate ");
                sqlReport.AppendLine("           ,getdate()");
                sqlReport.AppendLine("           ,'"+loginUserID+"')");

                sqlReport.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlReport.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                comm.Parameters.Add(SqlHelper.GetParameter("@DailyDate", model.DailyDate));
                if (model.PlanHRs>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@PlanHRs", model.PlanHRs));
                }
                if (model.RealHRs>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@RealHRs", model.RealHRs));
                }
                if (model.PlanWorkTime>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@PlanWorkTime", model.PlanWorkTime));
                }
                if (model.AddWorkTime>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@AddWorkTime", model.AddWorkTime));
                }
                if (model.StopWorkTime>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@StopWorkTime", model.StopWorkTime));
                }
                if (model.RealWorktime>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@RealWorkTime", model.RealWorktime));
                }
                if (model.MachineCount>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@MachineCount", model.MachineCount));
                }
                if (model.OpenCount>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@OpenCount", model.OpenCount));
                }
                if (model.OpenTime>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@OpenTime", model.OpenTime));
                }
                if (model.OpenPercent>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@OpenPercent", model.OpenPercent));
                }
                if (model.LoadPercent>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@LoadPercent", model.LoadPercent));
                }
                if (model.UsePercent>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@UsePercent", model.UsePercent));
                }
                if (model.StopCount>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@StopCount", model.StopCount));
                }
                if (model.StopTime>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@StopTime", model.StopTime));
                }
                comm.Parameters.Add(SqlHelper.GetParameter("@StopReason", model.StopReason));
                if (model.ProductionTotal>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@ProductionTotal", model.ProductionTotal));
                }
                if (model.WorkTimeTotal>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@WorkTimeTotal", model.WorkTimeTotal));
                }
                comm.Parameters.Add(SqlHelper.GetParameter("@Reporter", model.Reporter));
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportDate", model.ReportDate));
                if (model.TakeNum>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@TakeNum", model.TakeNum));
                }
                if (model.UsedNum>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@UsedNum", model.UsedNum));
                }
                if (model.NowNum>0)
                {
                    comm.Parameters.Add(SqlHelper.GetParameter("@NowNum", model.NowNum));
                }
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

                listADD.Add(comm);
                #endregion

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion

                #region 生产明细添加SQL语句
                if (modelProduct.ProductID.Length > 0)
                {
                    string[] detProductID = modelProduct.ProductID.Split(',');
                    string[] detWorkTime = modelProduct.WorkTime.Split(',');
                    string[] detFinishNum = modelProduct.FinishNum.Split(',');
                    string[] detPassNum = modelProduct.PassNum.Split(',');
                    string[] detPassPercent = modelProduct.PassPercent.Split(',');
                    string[] detFromBillNo = modelProduct.FromBillNo.Split(',');
                    string[] detFromBillID = modelProduct.FromBillID.Split(',');
                    string[] detFromLineNo = modelProduct.FromLineNo.Split(',');

                    for (int i = 0; i < detProductID.Length; i++)
                    {
                        StringBuilder sqlProduct = new StringBuilder();
                        sqlProduct.AppendLine("INSERT INTO officedba.ManufactureReportProduct");
                        sqlProduct.AppendLine("           (ReportNo ");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,ProductID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FinishNum");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,PassPercent");
                            }
                        }
                        if (detFromBillNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FromBillNo");
                            }
                        }
                        if (detFromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FromBillID");
                            }
                        }
                        if (detFromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromLineNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FromLineNo");
                            }
                        }
                        sqlProduct.AppendLine("           ,CompanyCD)");
                        sqlProduct.AppendLine("     VALUES");
                        sqlProduct.AppendLine("           (@ReportNo");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@ProductID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FinishNum");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@PassPercent");
                            }
                        }
                        if (detFromBillNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FromBillNo");
                            }
                        }
                        if (detFromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FromBillID");
                            }
                        }
                        if (detFromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromLineNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FromLineNo");
                            }
                        }
                        
                        sqlProduct.AppendLine("           ,@CompanyCD)");

                        SqlCommand commProduct = new SqlCommand();
                        commProduct.CommandText = sqlProduct.ToString();
                        commProduct.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commProduct.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@ProductID", detProductID[i].ToString()));
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@WorkTime", detWorkTime[i].ToString()));
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FinishNum", detFinishNum[i].ToString()));
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@PassNum", detPassNum[i].ToString()));
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@PassPercent", detPassPercent[i].ToString()));
                            }
                        }
                        if (detFromBillNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillNo[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", detFromBillNo[i].ToString()));
                            }
                        }
                        if (detFromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillID[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FromBillID", detFromBillID[i].ToString()));
                            }
                        }
                        if (detFromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromLineNo[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", detFromLineNo[i].ToString()));
                            }
                        }
                        listADD.Add(commProduct);
                    }


                }
                #endregion

                #region 人员明细添加SQL语句
                if (modelStaff.StaffID.Length > 0)
                {
                    string[] detStaffID = modelStaff.StaffID.Split(',');
                    string[] detWorkTime = modelStaff.WorkTime.Split(',');
                    string[] detFinishNum = modelStaff.FinishNum.Split(',');
                    string[] detPassNum = modelStaff.PassNum.Split(',');
                    string[] detPassPercent = modelStaff.PassPercent.Split(',');

                    for (int i = 0; i < detStaffID.Length; i++)
                    {
                        StringBuilder sqlStaff = new StringBuilder();
                        sqlStaff.AppendLine("INSERT INTO officedba.ManufactureReportStaff ");
                        sqlStaff.AppendLine("           (ReportNo");
                        if (detStaffID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detStaffID[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,StaffID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,FinishNum");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,PassPercent");
                            }
                        }
                        sqlStaff.AppendLine("           ,CompanyCD)");
                        sqlStaff.AppendLine("     VALUES");
                        sqlStaff.AppendLine("           (@ReportNo");
                        if (detStaffID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detStaffID[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@StaffID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@FinishNum ");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@PassPercent");
                            }
                        }
                        sqlStaff.AppendLine("           ,@CompanyCD)");

                        SqlCommand commStaff = new SqlCommand();
                        commStaff.CommandText = sqlStaff.ToString();
                        commStaff.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commStaff.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detStaffID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detStaffID[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@StaffID", detStaffID[i].ToString()));
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@WorkTime", detWorkTime[i].ToString()));
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@FinishNum", detFinishNum[i].ToString()));
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@PassNum", detPassNum[i].ToString()));
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@PassPercent", detPassPercent[i].ToString()));
                            }
                        }
                        listADD.Add(commStaff);

                    }
                }
                #endregion

                #region 设备明细添加SQL语句
                if (modelMachine.MachineID.Length > 0 && modelMachine.MachineNo.Length>0 && modelMachine.MachineName.Length>0)
                {
                    string[] detMachineID = modelMachine.MachineID.Split(',');
                    string[] detMachineNo = modelMachine.MachineNo.Split(',');
                    string[] detMachineName = modelMachine.MachineName.Split(',');
                    string[] detUseHour = modelMachine.UseHour.Split(',');
                    string[] detFinishNum = modelMachine.FinishNum.Split(',');
                    string[] detPassNum = modelMachine.PassNum.Split(',');
                    string[] detPassPercent = modelMachine.PassPercent.Split(',');

                    for (int i = 0; i < detMachineID.Length; i++)
                    {
                        StringBuilder sqlMachine = new StringBuilder();
                        sqlMachine.AppendLine("INSERT INTO officedba.ManufactureReportMachine");
                        sqlMachine.AppendLine("           (ReportNo");
                        if (detMachineID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineID[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,MachineID ");
                            }
                        }
                        if (detMachineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineNo[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,MachineNo ");
                            }
                        }
                        if (detMachineName[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineName[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,MachineName ");
                            }
                        }
                        if (detUseHour[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUseHour[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,UseHour ");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,FinishNum ");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,PassNum ");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,PassPercent");
                            }
                        }
                        sqlMachine.AppendLine("           ,CompanyCD) ");
                        sqlMachine.AppendLine("     VALUES ");
                        sqlMachine.AppendLine("           (@ReportNo ");
                        if (detMachineID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineID[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@MachineID  ");
                            }
                        }
                        if (detMachineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineNo[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@MachineNo  ");
                            }
                        }
                        if (detMachineName[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineName[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@MachineName  ");
                            }
                        }
                        if (detUseHour[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUseHour[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@UseHour  ");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@FinishNum ");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@PassNum ");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@PassPercent");
                            }
                        }
                        sqlMachine.AppendLine("           ,@CompanyCD)");

                        SqlCommand commMachine = new SqlCommand();
                        commMachine.CommandText = sqlMachine.ToString();
                        commMachine.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commMachine.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detMachineID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineID[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@MachineID", detMachineID[i].ToString()));
                            }
                        }
                        if (detMachineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineNo[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@MachineNo", detMachineNo[i].ToString()));
                            }
                        }
                        if (detMachineName[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineName[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@MachineName", detMachineName[i].ToString()));
                            }
                        }
                        if (detUseHour[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUseHour[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@UseHour", detUseHour[i].ToString()));
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@FinishNum", detFinishNum[i].ToString()));
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@PassNum", detPassNum[i].ToString()));
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@PassPercent", detPassPercent[i].ToString()));
                            }
                        }
                        listADD.Add(commMachine);

                    }
                }
                #endregion

                #region 物料明细添加SQL语句
                if (modelMeterial.ProductID.Length > 0)
                {
                    string[] detProductID = modelMeterial.ProductID.Split(',');
                    string[] detTakeNum =modelMeterial.TakeNum.Split(',');
                    string[] detBeforeNum =modelMeterial.BeforeNum.Split(',');
                    string[] detUsedNum =modelMeterial.UsedNum.Split(',');
                    string[] detBadNum = modelMeterial.BadNum.Split(',');
                    string[] detNowNum = modelMeterial.NowNum.Split(',');

                    for (int i = 0; i < detProductID.Length; i++)
                    {
                        StringBuilder sqlMeterial = new StringBuilder();
                        sqlMeterial.AppendLine("INSERT INTO officedba.ManufactureReportMeterial");
                        sqlMeterial.AppendLine("           (ReportNo");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,ProductID");
                            }
                        }
                        if (detTakeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detTakeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,TakeNum");
                            }
                        }
                        if (detBeforeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBeforeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,BeforeNum");
                            }
                        }
                        if (detUsedNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUsedNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,UsedNum");
                            }
                        }
                        if (detBadNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBadNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,BadNum");
                            }
                        }
                        if (detNowNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detNowNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,NowNum");
                            }
                        }
                        sqlMeterial.AppendLine("           ,CompanyCD)");
                        sqlMeterial.AppendLine("     VALUES ");
                        sqlMeterial.AppendLine("           (@ReportNo");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@ProductID");
                            }
                        }
                        if (detTakeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detTakeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@TakeNum ");
                            }
                        }
                        if (detBeforeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBeforeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@BeforeNum ");
                            }
                        }
                        if (detUsedNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUsedNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@UsedNum ");
                            }
                        }
                        if (detBadNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBadNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@BadNum");
                            }
                        }
                        if (detNowNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detNowNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@NowNum");
                            }
                        }
                        
                        sqlMeterial.AppendLine("           ,@CompanyCD)");

                        SqlCommand commMeterial = new SqlCommand();
                        commMeterial.CommandText = sqlMeterial.ToString();
                        commMeterial.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commMeterial.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@ProductID", detProductID[i].ToString()));
                            }
                        }
                        if (detTakeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detTakeNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@TakeNum", detTakeNum[i].ToString()));
                            }
                        }
                        if (detBeforeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBeforeNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@BeforeNum", detBeforeNum[i].ToString()));
                            }
                        }
                        if (detUsedNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUsedNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@UsedNum", detUsedNum[i].ToString()));
                            }
                        }
                        if (detBadNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBadNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@BadNum", detBadNum[i].ToString()));
                            }
                        }
                        if (detNowNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detNowNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@NowNum", detNowNum[i].ToString()));
                            }
                        }
                        listADD.Add(commMeterial);
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

        #region 修改主生产任务汇报单和各明细信息
        /// <summary>
        /// 修改主生产任务汇报单和各明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelProduct"></param>
        /// <param name="modelStaff"></param>
        /// <param name="modelMachine"></param>
        /// <param name="modelMeterial"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateManufactureReportInfo(ManufactureReportModel model, Hashtable htExtAttr, ManufactureReportProductModel modelProduct, ManufactureReportStaffModel modelStaff, ManufactureReportMachineModel modelMachine, ManufactureReportMeterialModel modelMeterial, string loginUserID)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  生产任务单修改SQL语句
            StringBuilder sqlReport = new StringBuilder();
            sqlReport.AppendLine("UPDATE officedba.ManufactureReport		");
            sqlReport.AppendLine("   SET Subject = @Subject					");
            sqlReport.AppendLine("      ,TaskNo = @TaskNo					");
            sqlReport.AppendLine("      ,DeptID = @DeptID					");
            sqlReport.AppendLine("      ,DailyDate = @DailyDate				");
            sqlReport.AppendLine("      ,PlanHRs = @PlanHRs					");
            sqlReport.AppendLine("      ,RealHRs = @RealHRs					");
            sqlReport.AppendLine("      ,PlanWorkTime = @PlanWorkTime		");
            sqlReport.AppendLine("      ,AddWorkTime = @AddWorkTime			");
            sqlReport.AppendLine("      ,StopWorkTime = @StopWorkTime		");
            sqlReport.AppendLine("      ,RealWorktime = @RealWorktime		");
            sqlReport.AppendLine("      ,MachineCount = @MachineCount		");
            sqlReport.AppendLine("      ,OpenCount = @OpenCount				");
            sqlReport.AppendLine("      ,OpenTime = @OpenTime				");
            sqlReport.AppendLine("      ,OpenPercent = @OpenPercent			");
            sqlReport.AppendLine("      ,LoadPercent = @LoadPercent			");
            sqlReport.AppendLine("      ,UsePercent = @UsePercent			");
            sqlReport.AppendLine("      ,StopCount = @StopCount				");
            sqlReport.AppendLine("      ,StopTime = @StopTime				");
            sqlReport.AppendLine("      ,StopReason = @StopReason			");
            sqlReport.AppendLine("      ,ProductionTotal = @ProductionTotal	");
            sqlReport.AppendLine("      ,WorkTimeTotal = @WorkTimeTotal  	");
            sqlReport.AppendLine("      ,Reporter = @Reporter				");
            sqlReport.AppendLine("      ,ReportDate = @ReportDate			");
            sqlReport.AppendLine("      ,TakeNum = @TakeNum		            ");
            sqlReport.AppendLine("      ,UsedNum = @UsedNum                 ");
            sqlReport.AppendLine("      ,NowNum = @NowNum					");
            sqlReport.AppendLine("      ,Remark = @Remark                   ");
            sqlReport.AppendLine("      ,ModifiedDate = getdate()           ");
            sqlReport.AppendLine("      ,ModifiedUserID ='"+loginUserID+"'  ");
            sqlReport.AppendLine(" WHERE CompanyCD=@CompanyCD and ID=@ID	");



            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlReport.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", model.TaskNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@DailyDate", model.DailyDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@PlanHRs", model.PlanHRs));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealHRs", model.RealHRs));
            comm.Parameters.Add(SqlHelper.GetParameter("@PlanWorkTime", model.PlanWorkTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@AddWorkTime", model.AddWorkTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@StopWorkTime", model.StopWorkTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@RealWorktime", model.RealWorktime));
            comm.Parameters.Add(SqlHelper.GetParameter("@MachineCount", model.MachineCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@OpenCount", model.OpenCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@OpenTime", model.OpenTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@OpenPercent", model.OpenPercent));
            comm.Parameters.Add(SqlHelper.GetParameter("@LoadPercent", model.LoadPercent));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsePercent", model.UsePercent));
            comm.Parameters.Add(SqlHelper.GetParameter("@StopCount", model.StopCount));
            comm.Parameters.Add(SqlHelper.GetParameter("@StopTime", model.StopTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@StopReason", model.StopReason));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductionTotal", model.ProductionTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@WorkTimeTotal", model.WorkTimeTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Reporter", model.Reporter));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportDate", model.ReportDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TakeNum", model.TakeNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedNum", model.UsedNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@NowNum", model.NowNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 生产明细处理

                #region 删除生产明细
                StringBuilder sqlProductDel = new StringBuilder();
                sqlProductDel.AppendLine("delete from officedba.ManufactureReportProduct ");
                sqlProductDel.AppendLine("where CompanyCD=@CompanyCD");
                sqlProductDel.AppendLine("and ReportNo=(");
                sqlProductDel.AppendLine("				select top 1 ReportNo from officedba.ManufactureReport where CompanyCD=@CompanyCD and ID=@ID");
                sqlProductDel.AppendLine("			    )");

                SqlCommand commProductDel = new SqlCommand();
                commProductDel.CommandText = sqlProductDel.ToString();
                commProductDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commProductDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                listADD.Add(commProductDel);
                #endregion 
                

                #region 生产明细添加SQL语句
                if (modelProduct.ProductID.Length > 0)
                {
                    string[] detProductID = modelProduct.ProductID.Split(',');
                    string[] detWorkTime = modelProduct.WorkTime.Split(',');
                    string[] detFinishNum = modelProduct.FinishNum.Split(',');
                    string[] detPassNum = modelProduct.PassNum.Split(',');
                    string[] detPassPercent = modelProduct.PassPercent.Split(',');
                    string[] detFromBillNo = modelProduct.FromBillNo.Split(',');
                    string[] detFromBillID = modelProduct.FromBillID.Split(',');
                    string[] detFromLineNo = modelProduct.FromLineNo.Split(',');

                    for (int i = 0; i < detProductID.Length; i++)
                    {
                        StringBuilder sqlProduct = new StringBuilder();
                        sqlProduct.AppendLine("INSERT INTO officedba.ManufactureReportProduct");
                        sqlProduct.AppendLine("           (ReportNo ");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,ProductID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FinishNum");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,PassPercent");
                            }
                        }
                        if (detFromBillNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FromBillNo");
                            }
                        }
                        if (detFromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FromBillID");
                            }
                        }
                        if (detFromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromLineNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,FromLineNo");
                            }
                        }
                        sqlProduct.AppendLine("           ,CompanyCD)");
                        sqlProduct.AppendLine("     VALUES");
                        sqlProduct.AppendLine("           (@ReportNo");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@ProductID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FinishNum");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@PassPercent");
                            }
                        }
                        if (detFromBillNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FromBillNo");
                            }
                        }
                        if (detFromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillID[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FromBillID");
                            }
                        }
                        if (detFromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromLineNo[i].ToString().Trim()))
                            {
                                sqlProduct.AppendLine("           ,@FromLineNo");
                            }
                        }
                        sqlProduct.AppendLine("           ,@CompanyCD)");

                        SqlCommand commProduct = new SqlCommand();
                        commProduct.CommandText = sqlProduct.ToString();
                        commProduct.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commProduct.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@ProductID", detProductID[i].ToString()));
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@WorkTime", detWorkTime[i].ToString()));
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FinishNum", detFinishNum[i].ToString()));
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@PassNum", detPassNum[i].ToString()));
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@PassPercent", detPassPercent[i].ToString()));
                            }
                        }
                        if (detFromBillNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillNo[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", detFromBillNo[i].ToString()));
                            }
                        }
                        if (detFromBillID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromBillID[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FromBillID", detFromBillID[i].ToString()));
                            }
                        }
                        if (detFromLineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFromLineNo[i].ToString().Trim()))
                            {
                                commProduct.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", detFromLineNo[i].ToString()));
                            }
                        }
                        listADD.Add(commProduct);
                    }


                }
                #endregion

            #endregion

            #region 人员明细处理

                #region 删除人员明细
                StringBuilder sqlStaffDel = new StringBuilder();
                sqlStaffDel.AppendLine("delete from officedba.ManufactureReportStaff ");
                sqlStaffDel.AppendLine("where CompanyCD=@CompanyCD");
                sqlStaffDel.AppendLine("and ReportNo=(");
                sqlStaffDel.AppendLine("				select top 1 ReportNo from officedba.ManufactureReport where CompanyCD=@CompanyCD and ID=@ID");
                sqlStaffDel.AppendLine("			    )");

                SqlCommand commStaffDel = new SqlCommand();
                commStaffDel.CommandText = sqlStaffDel.ToString();
                commStaffDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commStaffDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                listADD.Add(commStaffDel);
                #endregion

                #region 人员明细添加SQL语句
                if (modelStaff.StaffID.Length > 0)
                {
                    string[] detStaffID = modelStaff.StaffID.Split(',');
                    string[] detWorkTime = modelStaff.WorkTime.Split(',');
                    string[] detFinishNum = modelStaff.FinishNum.Split(',');
                    string[] detPassNum = modelStaff.PassNum.Split(',');
                    string[] detPassPercent = modelStaff.PassPercent.Split(',');

                    for (int i = 0; i < detStaffID.Length; i++)
                    {
                        StringBuilder sqlStaff = new StringBuilder();
                        sqlStaff.AppendLine("INSERT INTO officedba.ManufactureReportStaff ");
                        sqlStaff.AppendLine("           (ReportNo");
                        if (detStaffID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detStaffID[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,StaffID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,FinishNum");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,PassPercent");
                            }
                        }
                        sqlStaff.AppendLine("           ,CompanyCD)");
                        sqlStaff.AppendLine("     VALUES");
                        sqlStaff.AppendLine("           (@ReportNo");
                        if (detStaffID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detStaffID[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@StaffID");
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@WorkTime");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@FinishNum ");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@PassNum");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlStaff.AppendLine("           ,@PassPercent");
                            }
                        }
                        sqlStaff.AppendLine("           ,@CompanyCD)");

                        SqlCommand commStaff = new SqlCommand();
                        commStaff.CommandText = sqlStaff.ToString();
                        commStaff.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commStaff.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detStaffID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detStaffID[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@StaffID", detStaffID[i].ToString()));
                            }
                        }
                        if (detWorkTime[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detWorkTime[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@WorkTime", detWorkTime[i].ToString()));
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@FinishNum", detFinishNum[i].ToString()));
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@PassNum", detPassNum[i].ToString()));
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                commStaff.Parameters.Add(SqlHelper.GetParameter("@PassPercent", detPassPercent[i].ToString()));
                            }
                        }
                        listADD.Add(commStaff);

                    }
                }
                #endregion

            #endregion

            #region 设备明细处理

                #region 删除设备明细
                StringBuilder sqlMachineDel = new StringBuilder();
                sqlMachineDel.AppendLine("delete from officedba.ManufactureReportMachine ");
                sqlMachineDel.AppendLine("where CompanyCD=@CompanyCD");
                sqlMachineDel.AppendLine("and ReportNo=(");
                sqlMachineDel.AppendLine("				select top 1 ReportNo from officedba.ManufactureReport where CompanyCD=@CompanyCD and ID=@ID");
                sqlMachineDel.AppendLine("			    )");

                SqlCommand commMachineDel = new SqlCommand();
                commMachineDel.CommandText = sqlMachineDel.ToString();
                commMachineDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commMachineDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                listADD.Add(commMachineDel);
                #endregion

                #region 设备明细添加SQL语句
                if (modelMachine.MachineID.Length > 0 && modelMachine.MachineNo.Length > 0 && modelMachine.MachineName.Length > 0)
                {
                    string[] detMachineID = modelMachine.MachineID.Split(',');
                    string[] detMachineNo = modelMachine.MachineNo.Split(',');
                    string[] detMachineName = modelMachine.MachineName.Split(',');
                    string[] detUseHour = modelMachine.UseHour.Split(',');
                    string[] detFinishNum = modelMachine.FinishNum.Split(',');
                    string[] detPassNum = modelMachine.PassNum.Split(',');
                    string[] detPassPercent = modelMachine.PassPercent.Split(',');

                    for (int i = 0; i < detMachineID.Length; i++)
                    {
                        StringBuilder sqlMachine = new StringBuilder();
                        sqlMachine.AppendLine("INSERT INTO officedba.ManufactureReportMachine");
                        sqlMachine.AppendLine("           (ReportNo");
                        if (detMachineID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineID[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,MachineID ");
                            }
                        }
                        if (detMachineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineNo[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,MachineNo ");
                            }
                        }
                        if (detMachineName[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineName[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,MachineName ");
                            }
                        }
                        if (detUseHour[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUseHour[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,UseHour ");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,FinishNum ");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,PassNum ");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,PassPercent");
                            }
                        }
                        sqlMachine.AppendLine("           ,CompanyCD) ");
                        sqlMachine.AppendLine("     VALUES ");
                        sqlMachine.AppendLine("           (@ReportNo ");
                        if (detMachineID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineID[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@MachineID  ");
                            }
                        }
                        if (detMachineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineNo[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@MachineNo  ");
                            }
                        }
                        if (detMachineName[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineName[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@MachineName  ");
                            }
                        }
                        if (detUseHour[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUseHour[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@UseHour  ");
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@FinishNum ");
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@PassNum ");
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                sqlMachine.AppendLine("           ,@PassPercent");
                            }
                        }
                        sqlMachine.AppendLine("           ,@CompanyCD)");

                        SqlCommand commMachine = new SqlCommand();
                        commMachine.CommandText = sqlMachine.ToString();
                        commMachine.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commMachine.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detMachineID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineID[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@MachineID", detMachineID[i].ToString()));
                            }
                        }
                        if (detMachineNo[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineNo[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@MachineNo", detMachineNo[i].ToString()));
                            }
                        }
                        if (detMachineName[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detMachineName[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@MachineName", detMachineName[i].ToString()));
                            }
                        }
                        if (detUseHour[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUseHour[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@UseHour", detUseHour[i].ToString()));
                            }
                        }
                        if (detFinishNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detFinishNum[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@FinishNum", detFinishNum[i].ToString()));
                            }
                        }
                        if (detPassNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassNum[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@PassNum", detPassNum[i].ToString()));
                            }
                        }
                        if (detPassPercent[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detPassPercent[i].ToString().Trim()))
                            {
                                commMachine.Parameters.Add(SqlHelper.GetParameter("@PassPercent", detPassPercent[i].ToString()));
                            }
                        }
                        listADD.Add(commMachine);

                    }
                }
                #endregion

            #endregion

            #region 物料明细处理

                #region 删除物料明细
                StringBuilder sqlMeterialDel = new StringBuilder();
                sqlMeterialDel.AppendLine("delete from officedba.ManufactureReportMeterial ");
                sqlMeterialDel.AppendLine("where CompanyCD=@CompanyCD");
                sqlMeterialDel.AppendLine("and ReportNo=(");
                sqlMeterialDel.AppendLine("				select top 1 ReportNo from officedba.ManufactureReport where CompanyCD=@CompanyCD and ID=@ID");
                sqlMeterialDel.AppendLine("			    )");

                SqlCommand commMeterialDel = new SqlCommand();
                commMeterialDel.CommandText = sqlMeterialDel.ToString();
                commMeterialDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commMeterialDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                listADD.Add(commMeterialDel);
                #endregion

                #region 物料明细添加SQL语句
                if (modelMeterial.ProductID.Length > 0)
                {
                    string[] detProductID = modelMeterial.ProductID.Split(',');
                    string[] detTakeNum = modelMeterial.TakeNum.Split(',');
                    string[] detBeforeNum = modelMeterial.BeforeNum.Split(',');
                    string[] detUsedNum = modelMeterial.UsedNum.Split(',');
                    string[] detBadNum = modelMeterial.BadNum.Split(',');
                    string[] detNowNum = modelMeterial.NowNum.Split(',');

                    for (int i = 0; i < detProductID.Length; i++)
                    {
                        StringBuilder sqlMeterial = new StringBuilder();
                        sqlMeterial.AppendLine("INSERT INTO officedba.ManufactureReportMeterial");
                        sqlMeterial.AppendLine("           (ReportNo");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,ProductID");
                            }
                        }
                        if (detTakeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detTakeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,TakeNum");
                            }
                        }
                        if (detBeforeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBeforeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,BeforeNum");
                            }
                        }
                        if (detUsedNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUsedNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,UsedNum");
                            }
                        }
                        if (detBadNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBadNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,BadNum");
                            }
                        }
                        if (detNowNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detNowNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,NowNum");
                            }
                        }
                        sqlMeterial.AppendLine("           ,CompanyCD)");
                        sqlMeterial.AppendLine("     VALUES ");
                        sqlMeterial.AppendLine("           (@ReportNo");
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@ProductID");
                            }
                        }
                        if (detTakeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detTakeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@TakeNum ");
                            }
                        }
                        if (detBeforeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBeforeNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@BeforeNum ");
                            }
                        }
                        if (detUsedNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUsedNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@UsedNum ");
                            }
                        }
                        if (detBadNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBadNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@BadNum");
                            }
                        }
                        if (detNowNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detNowNum[i].ToString().Trim()))
                            {
                                sqlMeterial.AppendLine("           ,@NowNum");
                            }
                        }

                        sqlMeterial.AppendLine("           ,@CompanyCD)");

                        SqlCommand commMeterial = new SqlCommand();
                        commMeterial.CommandText = sqlMeterial.ToString();
                        commMeterial.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commMeterial.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        if (detProductID[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detProductID[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@ProductID", detProductID[i].ToString()));
                            }
                        }
                        if (detTakeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detTakeNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@TakeNum", detTakeNum[i].ToString()));
                            }
                        }
                        if (detBeforeNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBeforeNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@BeforeNum", detBeforeNum[i].ToString()));
                            }
                        }
                        if (detUsedNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detUsedNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@UsedNum", detUsedNum[i].ToString()));
                            }
                        }
                        if (detBadNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detBadNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@BadNum", detBadNum[i].ToString()));
                            }
                        }
                        if (detNowNum[i].ToString().Length > 0)
                        {
                            if (!string.IsNullOrEmpty(detNowNum[i].ToString().Trim()))
                            {
                                commMeterial.Parameters.Add(SqlHelper.GetParameter("@NowNum", detNowNum[i].ToString()));
                            }
                        }
                        listADD.Add(commMeterial);
                    }

                }

                #endregion

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 生产任务汇报单详细信息
        /// <summary>
        /// 生产任务汇报单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReport(ManufactureReportModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select a.CompanyCD,a.ID,a.ReportNo,a.Subject,a.TaskNo,a.Confirmor,e.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("	   a.DeptID,d.DeptName,isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,a.PlanHRs,a.RealHRs,");
            infoSql.AppendLine("	   Convert(numeric(12," + userInfo.SelPoint + "),a.PlanWorkTime) as PlanWorkTime,Convert(numeric(12," + userInfo.SelPoint + "),a.AddWorkTime) as AddWorkTime,Convert(numeric(12," + userInfo.SelPoint + "),a.StopWorkTime) as StopWorkTime,");
            infoSql.AppendLine("	   Convert(numeric(12," + userInfo.SelPoint + "),a.RealWorkTime) as RealWorkTime,a.MachineCount,a.OpenCount,");
            infoSql.AppendLine("       a.OpenTime,Convert(numeric(12,"+userInfo.SelPoint+"),a.OpenPercent) as OpenPercent,Convert(numeric(12,"+userInfo.SelPoint+"),a.LoadPercent) as LoadPercent,Convert(numeric(12,"+userInfo.SelPoint+"),a.UsePercent) as UsePercent,");
            infoSql.AppendLine("       a.StopCount,a.StopTime,a.StopReason,Convert(numeric(12," + userInfo.SelPoint + "),a.ProductionTotal) as ProductionTotal,Convert(numeric(12," + userInfo.SelPoint + "),a.WorkTimeTotal) as WorkTimeTotal,");
            infoSql.AppendLine("       a.Reporter,c.EmployeeName as ReporterReal,");
            infoSql.AppendLine("	   isnull( CONVERT(CHAR(10), a.ReportDate, 23),'') as ReportDate,Convert(numeric(12,"+userInfo.SelPoint+"),a.TakeNum) as TakeNum,Convert(numeric(12,"+userInfo.SelPoint+"),a.UsedNum) as UsedNum,Convert(numeric(12,"+userInfo.SelPoint+"),a.NowNum) as NowNum,a.Remark,a.BillStatus,");
            infoSql.AppendLine("          case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '结单'  end as strBillStatusText,");
            infoSql.AppendLine("       a.Creator,b.EmployeeName as CreatorReal,");
            infoSql.AppendLine("       a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("	   isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID");
            infoSql.AppendLine("from officedba.ManufactureReport a ");
            infoSql.AppendLine("left join officedba.EmployeeInfo b on a.Creator=b.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo c on a.Reporter=c.ID");
            infoSql.AppendLine("left join officedba.DeptInfo d on a.DeptID=d.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo e on a.Confirmor=e.ID");
            infoSql.AppendLine("where a.ID=@ID");
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

        #region 生产明细
        /// <summary>
        /// 生产明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportProduct(ManufactureReportModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select a.ReportNo,a.ProductID,Convert(numeric(14," + userInfo.SelPoint + "),a.WorkTime) as WorkTime,Convert(numeric(14," + userInfo.SelPoint + "),a.FinishNum) as FinishNum,a.FromBillNo,a.FromBillID,a.FromLineNo,");
            infoSql.AppendLine("	   Convert(numeric(14,"+userInfo.SelPoint+"),a.PassNum) as PassNum,Convert(numeric(14,"+userInfo.SelPoint+"),a.PassPercent) as PassPercent,");
            infoSql.AppendLine("	   b.ProdNo,b.ProductName,");
            if (userInfo.IsMoreUnit)
            {
                infoSql.AppendLine("       (Convert(numeric(14," + userInfo.SelPoint + "),isnull(c.UsedUnitCount,0)) - Convert(numeric(14," + userInfo.SelPoint + "),isnull(c.ProductedCount,0))) as UnFinishNum");
            }
            else
            {
                infoSql.AppendLine("       (Convert(numeric(14," + userInfo.SelPoint + "),isnull(c.ProductCount,0)) - Convert(numeric(14," + userInfo.SelPoint + "),isnull(c.ProductedCount,0))) as UnFinishNum");
            }
            infoSql.AppendLine("from officedba. ManufactureReportProduct a");
            infoSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            infoSql.AppendLine("left join officedba.ManufactureTaskDetail c on a.FromBillNo=c.TaskNo and a.FromBillID=c.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");
            infoSql.AppendLine("and a.CompanyCD=@CompanyCD");



            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 人员明细
        /// <summary>
        /// 人员明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportStaff(ManufactureReportModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select a.StaffID,b.EmployeeName as StaffReal,");
            infoSql.AppendLine("	   Convert(numeric(14," + userInfo.SelPoint + "),a.WorkTime) as WorkTime,Convert(numeric(14," + userInfo.SelPoint + "),a.FinishNum) as FinishNum,");
            infoSql.AppendLine("       Convert(numeric(14,"+userInfo.SelPoint+"),a.PassNum) as PassNum,");
            infoSql.AppendLine("       Convert(numeric(14,"+userInfo.SelPoint+"),a.PassPercent) as PassPercent");
            infoSql.AppendLine("from officedba. ManufactureReportStaff a");
            infoSql.AppendLine("left join officedba.EmployeeInfo b");
            infoSql.AppendLine("on a.StaffID=b.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");
            infoSql.AppendLine("and a.CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 设备明细
        /// <summary>
        /// 设备明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportMachine(ManufactureReportModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select  MachineID,b.EquipmentNo as MachineNo,b.EquipmentName as MachineName,UseHour,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),UsePercent) as UsePercent,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),FinishNum) as FinishNum,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),PassNum) as PassNum,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),PassPercent) as PassPercent");
            infoSql.AppendLine("from officedba.ManufactureReportMachine a ");
            infoSql.AppendLine("left join officedba.EquipmentInfo b on a.MachineID=b.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");
            infoSql.AppendLine("and a.CompanyCD=@CompanyCD");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 物料使用明细
        /// <summary>
        /// 物料使用明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportMeterial(ManufactureReportModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select  a.ProductID,b.ProdNo,b.ProductName,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),a.TakeNum) as TakeNum,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),a.BeforeNum) as BeforeNum,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),a.UsedNum) as UsedNum,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),a.BadNum) as BadNum,");
            infoSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),a.NowNum) as NowNum");
            infoSql.AppendLine("from officedba. ManufactureReportMeterial a");
            infoSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");
            infoSql.AppendLine("and a.CompanyCD=@CompanyCD");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 设备列表
        /// <summary>
        /// 设备列表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetEquipmentList(string CompanyCD)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select ID,EquipmentNo,EquipmentName,isnull(Norm,'')as Norm from officedba.EquipmentInfo where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 通过检索条件查询生产任务汇报单信息
        /// <summary>
        /// 通过检索条件查询生产任务汇报单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportListBycondition(ManufactureReportModel model, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("	select	a.ID,a.CompanyCD,a.ReportNo,a.Subject,a.TaskNo,a.BillStatus,");
            searchSql.AppendLine("          case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            searchSql.AppendLine("			a.DeptID,b.DeptName,isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,");
            searchSql.AppendLine("			isnull(Convert(varchar,Convert(numeric(22,"+userInfo.SelPoint+"),a.ProductionTotal) ),'') as ProductionTotal,isnull(Convert(varchar,Convert(numeric(22,"+userInfo.SelPoint+"),a.WorkTimeTotal) ),'') as WorkTimeTotal,");
            searchSql.AppendLine("          a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("			a.Reporter,c.EmployeeName as ReporterReal,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ReportDate, 23),'') as ReportDate,a.ModifiedDate ");
            searchSql.AppendLine("	from officedba.ManufactureReport a");
            searchSql.AppendLine("	left join officedba.DeptInfo b on a.DeptID=b.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo c on c.ID=a.Reporter");
            searchSql.AppendLine(") as info ");
            searchSql.AppendLine("where CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //单据编号
            if (!string.IsNullOrEmpty(model.ReportNo))
            {
                searchSql.AppendLine("and ReportNo like @ReportNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportNo", "%" + model.ReportNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }

            //负责人
            if (model.Reporter > 0)
            {
                searchSql.AppendLine(" and Reporter=@Reporter ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reporter", model.Reporter.ToString()));
            }
            //单据状态
            if (model.BillStatus > 0)
            {
                searchSql.AppendLine(" and BillStatus=@BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus.ToString()));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 删除生产任务汇报单
        /// <summary>
        /// 删除生产任务汇报单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteManufactureReport(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlStaff = new StringBuilder();
                    StringBuilder sqlMachine = new StringBuilder();
                    StringBuilder sqlMeterial = new StringBuilder();
                    StringBuilder sqlProduct = new StringBuilder();
                    StringBuilder sqlReport = new StringBuilder();

                    sqlStaff.AppendLine("delete from officedba.ManufactureReportStaff where CompanyCD=@CompanyCD and ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");

                    sqlMachine.AppendLine("delete from officedba.ManufactureReportMachine where CompanyCD=@CompanyCD and ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");

                    sqlMeterial.AppendLine("delete from officedba.ManufactureReportMeterial where CompanyCD=@CompanyCD and ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");

                    sqlProduct.AppendLine("delete from officedba.ManufactureReportProduct where CompanyCD=@CompanyCD and ReportNo=(select top 1 ReportNo from officedba.ManufactureReport where ID=@ID)");

                    sqlReport.AppendLine("delete from officedba.ManufactureReport where ID=@ID");

                    SqlCommand commStaff = new SqlCommand();
                    commStaff.CommandText = sqlStaff.ToString();
                    commStaff.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commStaff.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commStaff);

                    SqlCommand commMachine = new SqlCommand();
                    commMachine.CommandText = sqlMachine.ToString();
                    commMachine.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commMachine.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commMachine);

                    SqlCommand commMeterial = new SqlCommand();
                    commMeterial.CommandText = sqlMeterial.ToString();
                    commMeterial.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commMeterial.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commMeterial);

                    SqlCommand commProduct = new SqlCommand();
                    commProduct.CommandText = sqlProduct.ToString();
                    commProduct.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commProduct.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commProduct);

                    SqlCommand commReport = new SqlCommand();
                    commReport.CommandText = sqlReport.ToString();
                    commReport.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commReport);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 确认生产任务汇报单
        /// <summary>
        /// 确认生产任务汇报单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool ConfirmMenufactureReport(ManufactureReportModel model, string loginUserID,int EditType)
        {
            //确认时更新对应生产任务单明细中的已生产数量
            ArrayList listADD = new ArrayList();



            if (EditType > 0)
            {
                #region 更新单据状态
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.ManufactureReport SET");
                sql.AppendLine(" BillStatus    = 2,");
                sql.AppendLine(" Confirmor     = @Confirmor,");
                sql.AppendLine(" ConfirmDate   = getdate(),");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  ID=@ID");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                listADD.Add(comm);
                #endregion

                #region 加写生产任务单明细里的已生产数量
                DataTable dbDet = GetManufactureReportProduct(model);
                if (dbDet.Rows.Count > 0)
                {
                    for (int i = 0; i < dbDet.Rows.Count; i++)
                    {
                        //FinishNum FromBillNo,FromBillID
                        if (!string.IsNullOrEmpty(dbDet.Rows[i]["FinishNum"].ToString()) && !string.IsNullOrEmpty(dbDet.Rows[i]["FromBillNo"].ToString()) && !string.IsNullOrEmpty(dbDet.Rows[i]["FromBillID"].ToString()))
                        {
                            //update officedba.ManufactureTaskDetail set ProductedCount= isnull(ProductedCount,0)+ @ProductedCount where CompanyCD=@CompanyCD and TaskNo=@FromBillNo and ID=@FromBillID

                            Decimal finishNum = Decimal.Parse(dbDet.Rows[i]["FinishNum"].ToString());
                            string fromBillNo = dbDet.Rows[i]["FromBillNo"].ToString();
                            int fromBillID = int.Parse(dbDet.Rows[i]["FromBillID"].ToString());

                            #region 更新已生产数量
                            StringBuilder sqlTask = new StringBuilder();
                            sqlTask.AppendLine("Update officedba.ManufactureTaskDetail Set ProductedCount= isnull(ProductedCount,0)+ @ProductedCount where CompanyCD=@CompanyCD and TaskNo=@FromBillNo and ID=@FromBillID");
                            SqlCommand commTask = new SqlCommand();
                            commTask.CommandText = sqlTask.ToString();
                            commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", fromBillNo));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@FromBillID", fromBillID));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@ProductedCount", finishNum));
                            listADD.Add(commTask);
                            #endregion

                        }

                    }
                }

                #endregion
            }
            else
            {
                #region 更新单据状态
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.ManufactureReport SET");
                sql.AppendLine(" BillStatus    = 1,");
                sql.AppendLine(" Confirmor     = null,");
                sql.AppendLine(" ConfirmDate   = null,");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  ID=@ID");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                listADD.Add(comm);
                #endregion

                #region 减写生产任务单明细里的已生产数量
                DataTable dbDet = GetManufactureReportProduct(model);
                if (dbDet.Rows.Count > 0)
                {
                    for (int i = 0; i < dbDet.Rows.Count; i++)
                    {
                        //FinishNum FromBillNo,FromBillID
                        if (!string.IsNullOrEmpty(dbDet.Rows[i]["FinishNum"].ToString()) && !string.IsNullOrEmpty(dbDet.Rows[i]["FromBillNo"].ToString()) && !string.IsNullOrEmpty(dbDet.Rows[i]["FromBillID"].ToString()))
                        {
                            //update officedba.ManufactureTaskDetail set ProductedCount= isnull(ProductedCount,0)+ @ProductedCount where CompanyCD=@CompanyCD and TaskNo=@FromBillNo and ID=@FromBillID

                            Decimal finishNum = Decimal.Parse(dbDet.Rows[i]["FinishNum"].ToString());
                            string fromBillNo = dbDet.Rows[i]["FromBillNo"].ToString();
                            int fromBillID = int.Parse(dbDet.Rows[i]["FromBillID"].ToString());

                            #region 更新已生产数量
                            StringBuilder sqlTask = new StringBuilder();
                            sqlTask.AppendLine("Update officedba.ManufactureTaskDetail Set ProductedCount= isnull(ProductedCount,0)-@ProductedCount where CompanyCD=@CompanyCD and TaskNo=@FromBillNo and ID=@FromBillID");
                            SqlCommand commTask = new SqlCommand();
                            commTask.CommandText = sqlTask.ToString();
                            commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", fromBillNo));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@FromBillID", fromBillID));
                            commTask.Parameters.Add(SqlHelper.GetParameter("@ProductedCount", finishNum));
                            listADD.Add(commTask);
                            #endregion

                        }

                    }
                }

                #endregion
            }

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(ManufactureReportModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.ManufactureReport set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ReportNo = @ReportNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@ReportNo", model.ReportNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 运营模式：(生产任务单汇报明细表)
        /// <summary>
        /// 生产任务单汇报明细表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="TaskNo"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportListBycondition_Operating(string CompanyCD, int DeptID, string TaskNo, string ConfirmDateStart, string ConfirmDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from(");
            searchSql.AppendLine("	select	a.ID,a.DeptID,c.DeptName,a.TaskNo,b.CountTotal,isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,");
            searchSql.AppendLine("			Convert(numeric(10," + point + "),a.ProductionTotal) as ProductionTotal,Convert(char(20),Convert(numeric(10," + point + "),a.ProductionTotal))+'&nbsp;' as ProductionTotal1,a.CompanyCD,");
            searchSql.AppendLine("			Convert(numeric(10," + point + "),a.TakeNum) as TakeNum,Convert(char(20),Convert(numeric(10," + point + "),a.TakeNum))+'&nbsp;' as TakeNum1,Convert(numeric(10," + point + "),a.UsedNum) as UsedNum,Convert(char(20),Convert(numeric(10," + point + "),a.UsedNum))+'&nbsp;' as UsedNum1,");
            searchSql.AppendLine("			a.PlanHRs,a.RealHRs,a.PlanWorkTime,a.AddWorkTime,a.StopWorkTime,a.RealWorkTime,");
            searchSql.AppendLine("			a.OpenCount,a.OpenTime,a.UsePercent,a.Reporter,d.EmployeeName as ReporterReal,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ReportDate, 23),'') as ReportDate,a.BillStatus,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate");
            searchSql.AppendLine("	from officedba.ManufactureReport a");
            searchSql.AppendLine("	left join officedba.ManufactureTask b on a.TaskNo=b.TaskNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on a.DeptID=c.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo d on a.Reporter=d.ID");
            searchSql.AppendLine(") as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD and BillStatus=2");


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
            //--生产任务单编号
            if (!string.IsNullOrEmpty(TaskNo))
            {
                searchSql.AppendLine(" and TaskNo like @TaskNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + TaskNo + "%"));
            }
            if (!string.IsNullOrEmpty(ConfirmDateStart))
            {
                searchSql.AppendLine(" and ConfirmDate>=@ConfirmDateStart ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDateStart", ConfirmDateStart));
            }
            //发料截止日期
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

        #region 运营模式：(打印生产任务单汇报明细表)
        /// <summary>
        /// 生产任务单汇报明细表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="TaskNo"></param>
        /// <param name="ConfirmDateStart"></param>
        /// <param name="ConfirmDateEnd"></param>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable GetManufactureReportListBycondition_Operating_Print(string CompanyCD, int DeptID, string TaskNo, string ConfirmDateStart, string ConfirmDateEnd, string orderColumn, string orderType)
        {



            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from(");
            searchSql.AppendLine("	select	a.ID,a.DeptID,c.DeptName,a.TaskNo,b.CountTotal,isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,");
            searchSql.AppendLine("			Convert(numeric(10,2),a.ProductionTotal) as ProductionTotal,a.CompanyCD,");
            searchSql.AppendLine("			Convert(numeric(10,2),a.TakeNum) as TakeNum,Convert(numeric(10,2),a.UsedNum) as UsedNum,");
            searchSql.AppendLine("			a.PlanHRs,a.RealHRs,a.PlanWorkTime,a.AddWorkTime,a.StopWorkTime,a.RealWorkTime,");
            searchSql.AppendLine("			a.OpenCount,a.OpenTime,a.UsePercent,a.Reporter,d.EmployeeName as ReporterReal,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ReportDate, 23),'') as ReportDate,a.BillStatus,");
            searchSql.AppendLine("			isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate");
            searchSql.AppendLine("	from officedba.ManufactureReport a");
            searchSql.AppendLine("	left join officedba.ManufactureTask b on a.TaskNo=b.TaskNo");
            searchSql.AppendLine("	left join officedba.DeptInfo c on a.DeptID=c.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo d on a.Reporter=d.ID");
            searchSql.AppendLine(") as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD and BillStatus=2");


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
            //--生产任务单编号
            if (!string.IsNullOrEmpty(TaskNo))
            {
                searchSql.AppendLine(" and TaskNo like @TaskNo");
                arr.Add(new SqlParameter("@TaskNo", "%" + TaskNo + "%"));
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

    }
}
