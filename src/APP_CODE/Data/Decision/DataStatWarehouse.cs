using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XBase.Data.DBHelper;
using System.Text.RegularExpressions;
namespace XBase.Data.Decision
{
    public class DataStatWarehouse
    {
        private XBase.Model.Decision.DataStat dslist=new XBase.Model.Decision.DataStat();
        private IList<XBase.Model.Decision.DataMySubscribe> _list;

        public void DataStatSend(DateTime lastEndDate,string Frequency)
        {
            try
            {
                string strPro = String.Empty;
                string strSqlwhere = String.Empty;
                DataSet ds = null;
                XBase.Data.Decision.DataMySubscribe _dal = new DataMySubscribe();
                _list = _dal.GetDataMySubscribleListbyCond("Frequency='" + Frequency + "'", "ID");
                for (int i = 0; i < _list.Count; i++)
                {

                    if (_list[i].DataVarValue != "" && _list[i].DataVarValue != null)
                        strSqlwhere = _list[i].DataVarValue;

                    if (strSqlwhere != "")
                    {
                        SqlParameter[] prams = {
				            SqlParameterHelper.MakeInParam("@LastEndDate",SqlDbType.DateTime,20,Convert.ToDateTime(lastEndDate))
                        };
                    try{
                            ds= Database.RunSql(strSqlwhere,prams);
                            if (ds!=null)
                            { 
                               
                                if (ds.Tables[0].Rows[0][0] != null)
                                {  
                                    if (ds.Tables[0].Rows[0][0].ToString()!="")
                                    {  
                                        string[] MobileArr = _list[i].MyMobile.Split(',');
                                        string DataName1 = _list[i].DataName;
                                        string SetResult = Regex.Replace(DataName1, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Replace("&nbsp;","");
                                        SetResult+= _list[i].Format.ToString().Replace("[#统计数据#]", ds.Tables[0].Rows[0][0].ToString());
                                        for (int n = 0; n < MobileArr.Length; n++)
                                        {
                                            XBase.Common.SMSender.InternalSend(MobileArr[n], SetResult + "(统计时间：" + lastEndDate+"---"+DateTime.Now.ToString()+")");
                                        }

                                        XBase.Model.Decision.DataMyCollector collmodel = new XBase.Model.Decision.DataMyCollector();
                                        string[] IdArr = _list[i].DataID.Split('|');
                                        collmodel.CompanyCD = _list[i].CompanyCD;
                                        collmodel.KeyWordID = Convert.ToInt32(IdArr[0]);
                                        collmodel.ActionID = Convert.ToInt32(IdArr[1]);
                                        collmodel.Frequency = int.Parse(Frequency);
                                        collmodel.ActionDetailID = Convert.ToInt32(IdArr[2]);
                                        collmodel.Flag = "1";
                                        collmodel.ReportDate = DateTime.Now;
                                        collmodel.ReadStatus = "0";
                                        collmodel.Owner = "";
                                        collmodel.ReportTxt = SetResult;
                                        XBase.Data.Decision.DataMyCollector colldata = new XBase.Data.Decision.DataMyCollector();
                                        colldata.AddDataMyCollector(collmodel);
                                    }
                                  
                                }
                            }
                         }
                         catch
                         {

                         }

                    }
                }
            }
            catch
            {

            }
            
        }

        /// <summary>
        /// 添加统计信息
        /// </summary>
        /// <param name="p"></param>
        /// <param name="dataMySubscribe"></param>
        private bool AddDataStat(XBase.Model.Decision.DataStat entity)
        {
            SqlParameter[] prams = {									
                SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
                SqlParameterHelper.MakeInParam("@DataID",SqlDbType.Int,4,entity.DataID),
                SqlParameterHelper.MakeInParam("@DataName",SqlDbType.VarChar,100,entity.DataName),
                SqlParameterHelper.MakeInParam("@DataVarValue",SqlDbType.VarChar,100,entity.DataVarValue),
                SqlParameterHelper.MakeInParam("@DateNum",SqlDbType.Decimal,9,entity.DataNum),
                SqlParameterHelper.MakeInParam("@StatType",SqlDbType.Char,1,entity.StatType),
                SqlParameterHelper.MakeInParam("@DateDateim",SqlDbType.DateTime,8,DateTime.Now)
                };
            bool ret;
            Database.RunProc("[statdba].[DataStat_Add]", prams,out ret);
            return ret;
        }

    }
}
