<%@ WebHandler Language="C#" Class="StorageJournal" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StorageJournal : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        HttpRequest request = context.Request;
        string action = context.Request.Params["Action"].Trim().ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!string.IsNullOrEmpty(action))
        {
            if (action == "SumInfo")
            {
                //从请求中获取排序列
                string orderString = request.QueryString["OrderBy"];

                //排序：默认为升序
                string orderBy = "asc";
                //要排序的字段，如果为空，默认为"ID"
                string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";
                //降序时如果设置为降序
                if (orderString.EndsWith("_d"))
                {
                    //排序：降序
                    orderBy = "desc";
                }
                //从请求中获取当前页
                int pageIndex = int.Parse(request.QueryString["PageIndex"]);
                //从请求中获取每页显示记录数
                int pageCount = int.Parse(request.QueryString["PageCount"]);
                //跳过记录数
                int skipRecord = (pageIndex - 1) * pageCount;

                int totalCount = 0;
                string ord = orderByCol + " " + orderBy;

                //获取数据

                //设置查询条件
                string ddlStorage, txtProductNo, ProductID, StartDate, EndDate, EFIndex, EFDesc, ProviderID, BatchNo, SourceType, SourceNo, CreatorID, ckbIsM = "";

                ddlStorage = request.QueryString["ddlStorage"].Trim().ToString();
               // txtProductName = request.QueryString["txtProductName"].Trim().ToString();
                txtProductNo = request.QueryString["txtProductNo"].Trim().ToString();
                ProductID = request.QueryString["ProductID"].Trim().ToString();
                StartDate = request.QueryString["StartDate"].Trim().ToString();
                EndDate = request.QueryString["EndDate"].Trim().ToString();
                EFIndex = request.QueryString["EFIndex"].ToString().Trim();
                EFDesc = request.QueryString["EFDesc"].ToString().Trim();
                ProviderID = request.QueryString["ProviderID"].ToString().Trim();
                BatchNo = request.QueryString["BatchNo"].ToString().Trim();
                SourceType = request.QueryString["SourceType"].ToString().Trim();
                SourceNo = request.QueryString["SourceNo"].ToString().Trim();
                CreatorID = request.QueryString["CreatorID"].ToString().Trim();
                ckbIsM = request.QueryString["ckbIsM"].ToString().Trim();
                UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                DataTable dt = new DataTable();

                string extQueryStr = string.Empty;
                if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                {
                    extQueryStr = "   b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' " + "@b.ExtField"+EFIndex ;
                }
                
                if (string.IsNullOrEmpty(ProviderID))
                {
                    string QueryStr = " and  CompanyCD='" + CompanyCD + "' ";
                    if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
                    {
                        QueryStr += " and StorageID= '";
                        QueryStr += ddlStorage;
                        QueryStr += "' ";
                    }
                    if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
                    {
                        QueryStr += " and ProductID= '";
                        QueryStr += ProductID;
                        QueryStr += "' ";
                    }

                    if (StartDate.Trim().Length > 0)
                    {
                        QueryStr += " and HappenDate >='";
                        QueryStr += StartDate+" 00:00:00";
                        QueryStr += "' ";
                    }

                    if (EndDate.Trim().Length > 0)
                    {
                        QueryStr += " and HappenDate <='";
                        QueryStr += EndDate + " 23:59:59";
                        QueryStr += "' ";
                    }


                    if (BatchNo != "0")
                    {
                        if (BatchNo == "未设置批次")
                        {
                            QueryStr += " and ( BatchNo is null or BatchNo='') ";
                        }
                        else
                        {
                            QueryStr += " and BatchNo='"+BatchNo+"' ";
                        }
                    }


                    if (SourceType.Trim().Length > 0 && SourceType != "0")
                    {
                        QueryStr += " and BillType= '";
                        QueryStr += SourceType;
                        QueryStr += "' ";
                    }


                    if (SourceNo.Trim().Length > 0 && SourceNo != "0")
                    {
                        if (ckbIsM == "0")
                        {
                            QueryStr += " and BillNo= '";
                            QueryStr += SourceNo;
                            QueryStr += "' ";
                        }
                        else
                        {
                            QueryStr += " and BillNo like '%";
                            QueryStr += SourceNo;
                            QueryStr += "%' ";
                        }
                    }


                    if (CreatorID.Trim().Length > 0 && CreatorID != "0")
                    {
                        QueryStr += " and Creator= '";
                        QueryStr += CreatorID;
                        QueryStr += "' ";
                    }
                    
                    

                    //查询数据
                    //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
                    dt = StrongeJournalBus.GetSumStrongJournal(QueryStr, extQueryStr, pageIndex, pageCount, ord, ref totalCount);
                }
                else
                {
                    string QueryStr = " and  a.CompanyCD='" + CompanyCD + "' and c.ProviderID='"+ProviderID+"'  ";
                    if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
                    {
                        QueryStr += " and b.StorageID= '";
                        QueryStr += ddlStorage;
                        QueryStr += "' ";
                    }

                    if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
                    {
                        QueryStr += " and b.ProductID= '";
                        QueryStr += ProductID;
                        QueryStr += "' ";
                    }

                    
                    
                    if (StartDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.EnterDate >='";
                        QueryStr += StartDate + " 00:00:00";
                        QueryStr += "' ";
                    }

                    if (EndDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.EnterDate <='";
                        QueryStr += EndDate + " 23:59:59";
                        QueryStr += "' ";
                    }


                    if (BatchNo != "0")
                    {
                        if (BatchNo == "未设置批次")
                        {
                            QueryStr += " and ( b.BatchNo is null or b.BatchNo='') ";
                        }
                        else
                        {
                            QueryStr += " and b.BatchNo='" + BatchNo + "' ";
                        }
                    }
                    

                    //查询数据
                    //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
                    dt = StrongeJournalBus.GetStrongJournalByPro(QueryStr, extQueryStr, pageIndex, pageCount, ord, ref totalCount);
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();

            }
            else if (action == "DetailInfo")
            {
                //从请求中获取排序列
                string orderString = request.QueryString["OrderBy"];

                //排序：默认为升序
                string orderBy = "asc";
                //要排序的字段，如果为空，默认为"ID"
                string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";
                //降序时如果设置为降序
                if (orderString.EndsWith("_d"))
                {
                    //排序：降序
                    orderBy = "desc";
                }
                //从请求中获取当前页
                int pageIndex = int.Parse(request.QueryString["PageIndex"]);
                //从请求中获取每页显示记录数
                int pageCount = int.Parse(request.QueryString["PageCount"]);
                //跳过记录数
                int skipRecord = (pageIndex - 1) * pageCount;

                int totalCount = 0;
                string ord = orderByCol + " " + orderBy;

                //获取数据

                //设置查询条件
                string ddlStorage, ProductID, StartDate, EndDate, ProviderID, BatchNo = "";

                ddlStorage = request.QueryString["ddlStorage"].Trim().ToString();
                ProductID = request.QueryString["ProductID"].Trim().ToString();
                StartDate = request.QueryString["StartDate"].Trim().ToString();
                EndDate = request.QueryString["EndDate"].Trim().ToString();
                ProviderID = request.QueryString["ProviderID"].Trim().ToString();
                BatchNo = request.QueryString["BatchNo"].ToString().Trim();
                
                DataTable dt = new DataTable();


                if (string.IsNullOrEmpty(ProviderID))
                {
                    string QueryStr = "  a.CompanyCD='" + CompanyCD + "' ";
                    if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
                    {
                        QueryStr += " and a.StorageID= '";
                        QueryStr += ddlStorage;
                        QueryStr += "' ";
                    }

                    if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
                    {
                        QueryStr += " and a.ProductID= '";
                        QueryStr += ProductID;
                        QueryStr += "' ";
                    }

                    if (StartDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.HappenDate >='";
                        QueryStr += StartDate + " 00:00:00";
                        QueryStr += "' ";
                    }

                    if (EndDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.HappenDate <='";
                        QueryStr += EndDate + " 23:59:59";
                        QueryStr += "' ";
                    }


                    if (BatchNo != "0")
                    {
                        if (BatchNo == "未设置批次"||string.IsNullOrEmpty(BatchNo))
                        {
                            QueryStr += " and ( a.BatchNo is null or a.BatchNo='') ";
                        }
                        else
                        {
                            QueryStr += " and a.BatchNo='" + BatchNo + "' ";
                        }
                    }







                    //查询数据
                    //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
                    dt = StrongeJournalBus.GetDetailStrongJournal(QueryStr, pageIndex, pageCount, ord, ref totalCount);
                }
                else
                {
                    string QueryStr = " and  a.CompanyCD='" + CompanyCD + "' and c.ProviderID='"+ProviderID+"' ";
                    if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
                    {
                        QueryStr += " and e.StorageID= '";
                        QueryStr += ddlStorage;
                        QueryStr += "' ";
                    }

                    if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
                    {
                        QueryStr += " and e.ProductID= '";
                        QueryStr += ProductID;
                        QueryStr += "' ";
                    }

                    if (StartDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.EnterDate >='";
                        QueryStr += StartDate + " 00:00:00";
                        QueryStr += "' ";
                    }

                    if (EndDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.EnterDate <='";
                        QueryStr += EndDate + " 23:59:59";
                        QueryStr += "' ";
                    }


                    if (BatchNo != "0")
                    {
                        if (BatchNo == "未设置批次")
                        {
                            QueryStr += " and ( e.BatchNo is null or e.BatchNo='') ";
                        }
                        else
                        {
                            QueryStr += " and e.BatchNo='" + BatchNo + "' ";
                        }
                    }







                    //查询数据
                    //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
                    dt = StrongeJournalBus.GetDetailStrongJournalByPro(QueryStr, pageIndex, pageCount, ord, ref totalCount);
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End(); 
            }
            else if (action == "DetailInfos")
            {
                //从请求中获取排序列
                string orderString = request.QueryString["orderby"];

                //排序：默认为升序
                string orderBy = "asc";
                //要排序的字段，如果为空，默认为"ID"
                string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";
                //降序时如果设置为降序
                if (orderString.EndsWith("_d"))
                {
                    //排序：降序
                    orderBy = "desc";
                }
                //从请求中获取当前页
                int pageIndex = int.Parse(request.QueryString["pageIndex"]);
                //从请求中获取每页显示记录数
                int pageCount = int.Parse(request.QueryString["pageCount"]);
                //跳过记录数
                int skipRecord = (pageIndex - 1) * pageCount;

                int totalCount = 0;
                string ord = orderByCol + " " + orderBy;

                //获取数据

                //设置查询条件
                string ddlStorage, SourceType, SourceNo, CreatorID, ProductID, StartDate, EndDate, BatchNo, EFIndex, EFDesc, Specification, ColorID, Material, Manufacturer, Size, FromAddr, BarCode,ckbIsM = "";

                ddlStorage = request.QueryString["ddlStorage"].Trim().ToString();
                ProductID = request.QueryString["ProductID"].Trim().ToString();
                StartDate = request.QueryString["StartDate"].Trim().ToString();
                EndDate = request.QueryString["EndDate"].Trim().ToString();
                BatchNo = request.QueryString["BatchNo"].ToString().Trim();
                SourceType = request.QueryString["SourceType"].ToString().Trim();
                SourceNo = request.QueryString["SourceNo"].ToString().Trim();
                CreatorID = request.QueryString["CreatorID"].ToString().Trim();


                Specification = request.QueryString["Specification"].ToString().Trim();
                ColorID = request.QueryString["ColorID"].ToString().Trim();
                Material = request.QueryString["Material"].ToString().Trim();
                Manufacturer = request.QueryString["Manufacturer"].ToString().Trim();
                Size = request.QueryString["Size"].ToString().Trim();
                FromAddr = request.QueryString["FromAddr"].ToString().Trim();
                BarCode = request.QueryString["BarCode"].ToString().Trim();

                EFIndex = request.QueryString["EFIndex"].ToString().Trim();
                EFDesc = request.QueryString["EFDesc"].ToString().Trim();

                ckbIsM = request.QueryString["ckbIsM"].ToString().Trim();
                
                DataTable dt = new DataTable();
                    string QueryStr = "  a.CompanyCD='" + CompanyCD + "' ";
                    if (ddlStorage.Trim().Length > 0 && ddlStorage!="0")
                    {
                        QueryStr += " and a.StorageID= '";
                        QueryStr += ddlStorage;
                        QueryStr += "' ";
                    }

                    if (ProductID.Trim().Length > 0 )
                    {
                        QueryStr += " and a.ProductID= '";
                        QueryStr += ProductID;
                        QueryStr += "' ";
                    }

                    if (StartDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.HappenDate >='";
                        QueryStr += StartDate + " 00:00:00";
                        QueryStr += "' ";
                    }

                    if (EndDate.Trim().Length > 0)
                    {
                        QueryStr += " and a.HappenDate <='";
                        QueryStr += EndDate + " 23:59:59";
                        QueryStr += "' ";
                    }


                    if (BatchNo != "0")
                    {
                        if (BatchNo == "未设置批次" || string.IsNullOrEmpty(BatchNo))
                        {
                            QueryStr += " and ( a.BatchNo is null or a.BatchNo='') ";
                        }
                        else
                        {
                            QueryStr += " and a.BatchNo='" + BatchNo + "' ";
                        }
                    }


                    if (SourceType.Trim().Length > 0 && SourceType != "0")
                    {
                        QueryStr += " and a.BillType= '";
                        QueryStr += SourceType;
                        QueryStr += "' ";
                    }


                    if (SourceNo.Trim().Length > 0 && SourceNo != "0")
                    {
                        if (ckbIsM == "0")
                        {
                            QueryStr += " and a.BillNo= '";
                            QueryStr += SourceNo;
                            QueryStr += "' ";
                        }
                        else
                        {
                            QueryStr += " and a.BillNo like '%";
                            QueryStr += SourceNo;
                            QueryStr += "%' ";
                        }
                    }


                    if (CreatorID.Trim().Length > 0 && CreatorID != "0")
                    {
                        
                        QueryStr += " and a.Creator= '";
                        QueryStr += CreatorID;
                        QueryStr += "' ";
                    }


                    if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                    {
                        QueryStr += "  and   b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                    }



                    if (!string.IsNullOrEmpty(Specification))
                    {
                        QueryStr += "  and   b.Specification LIKE '%" + Specification + "%' ";
                    }
                
                    if (!string.IsNullOrEmpty(ColorID)&&ColorID!="0")
                    {
                        QueryStr += " and    b.ColorID= '" + ColorID + "' ";
                    }

                    if (!string.IsNullOrEmpty(Material)&&Material!="0")
                    {
                        QueryStr += "  and   b.Material = '" + Material + "' ";
                    }
                    if (!string.IsNullOrEmpty(Manufacturer))
                    {
                        QueryStr += " and   b.Manufacturer LIKE '%" + Manufacturer + "%' ";
                    }
                    if (!string.IsNullOrEmpty(Size))
                    {
                        QueryStr += " and   b.Size LIKE '%" + Size + "%' ";
                    }
                    if (!string.IsNullOrEmpty(FromAddr))
                    {
                        QueryStr += "  and   b.FromAddr LIKE '%" + FromAddr + "%' ";
                    }
                    if (!string.IsNullOrEmpty(BarCode))
                    {
                        QueryStr += " and   b.BarCode LIKE '%" + BarCode + "%' ";
                    }
                  
                   
                    
                    







                    //查询数据
                    //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
                dt = StrongeJournalBus.GetDetailStrongJournal(QueryStr, pageIndex, pageCount, ord, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (action == "SubDetailInfos")
            {
                //从请求中获取排序列
                string orderString = request.QueryString["orderby"];

                //排序：默认为升序
                string orderBy = "asc";
                //要排序的字段，如果为空，默认为"ID"
                string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";
                //降序时如果设置为降序
                if (orderString.EndsWith("_d"))
                {
                    //排序：降序
                    orderBy = "desc";
                }
                //从请求中获取当前页
                int pageIndex = int.Parse(request.QueryString["pageIndex"]);
                //从请求中获取每页显示记录数
                int pageCount = int.Parse(request.QueryString["pageCount"]);
                //跳过记录数
                int skipRecord = (pageIndex - 1) * pageCount;

                int totalCount = 0;
                string ord = orderByCol + " " + orderBy;

                //获取数据

                //设置查询条件
                string ddlStorage, SourceType, SourceNo, CreatorID, ProductID, StartDate, EndDate, BatchNo, EFIndex, EFDesc, ckbIsM = "";

                ddlStorage = request.QueryString["ddlStorage"].Trim().ToString();
                ProductID = request.QueryString["ProductID"].Trim().ToString();
                StartDate = request.QueryString["StartDate"].Trim().ToString();
                EndDate = request.QueryString["EndDate"].Trim().ToString();
                BatchNo = request.QueryString["BatchNo"].ToString().Trim();
                SourceType = request.QueryString["SourceType"].ToString().Trim();
                SourceNo = request.QueryString["SourceNo"].ToString().Trim();
                CreatorID = request.QueryString["CreatorID"].ToString().Trim();

                EFIndex = request.QueryString["EFIndex"].ToString().Trim();
                EFDesc = request.QueryString["EFDesc"].ToString().Trim();


                ckbIsM = request.QueryString["ckbIsM"].ToString().Trim();

                DataTable dt = new DataTable();
                string QueryStr = "  a.CompanyCD='" + CompanyCD + "' ";
                if (ddlStorage.Trim().Length > 0 && ddlStorage != "0")
                {
                    QueryStr += " and a.DeptID= '";
                    QueryStr += ddlStorage;
                    QueryStr += "' ";
                }

                if (ProductID.Trim().Length > 0)
                {
                    QueryStr += " and a.ProductID= '";
                    QueryStr += ProductID;
                    QueryStr += "' ";
                }

                if (StartDate.Trim().Length > 0)
                {
                    QueryStr += " and a.HappenDate >='";
                    QueryStr += StartDate + " 00:00:00";
                    QueryStr += "' ";
                }

                if (EndDate.Trim().Length > 0)
                {
                    QueryStr += " and a.HappenDate <='";
                    QueryStr += EndDate + " 23:59:59";
                    QueryStr += "' ";
                }


                if (BatchNo != "0")
                {
                    if (BatchNo == "未设置批次" || string.IsNullOrEmpty(BatchNo))
                    {
                        QueryStr += " and ( a.BatchNo is null or a.BatchNo='') ";
                    }
                    else
                    {
                        QueryStr += " and a.BatchNo='" + BatchNo + "' ";
                    }
                }


                if (SourceType.Trim().Length > 0 && SourceType != "0")
                {
                    QueryStr += " and a.BillType= '";
                    QueryStr += SourceType;
                    QueryStr += "' ";
                }


                if (SourceNo.Trim().Length > 0 && SourceNo != "0")
                {
                    if (ckbIsM == "0")
                    {
                        QueryStr += " and a.BillNo= '";
                        QueryStr += SourceNo;
                        QueryStr += "' ";
                    }
                    else
                    {
                        QueryStr += " and a.BillNo like '%";
                        QueryStr += SourceNo;
                        QueryStr += "%' ";
                    }
                }


                if (CreatorID.Trim().Length > 0 && CreatorID != "0")
                {
                    QueryStr += " and a.Creator= '";
                    QueryStr += CreatorID;
                    QueryStr += "' ";
                }


                if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                {
                    QueryStr += "  and  b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                }










                //查询数据
                //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
                dt = StrongeJournalBus.GetSubStrongJournalDetail(QueryStr, pageIndex, pageCount, ord, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (action == "SumJournal")
            {

                //设置查询条件
                string ddlStorage, SourceType, SourceNo, CreatorID, ProductID, StartDate, EndDate, BatchNo, EFIndex, EFDesc, Specification, ColorID, Material, Manufacturer, Size, FromAddr, BarCode, ckbIsM = "";

                ddlStorage = request.QueryString["ddlStorage"].Trim().ToString();
                ProductID = request.QueryString["ProductID"].Trim().ToString();
                StartDate = request.QueryString["StartDate"].Trim().ToString();
                EndDate = request.QueryString["EndDate"].Trim().ToString();
                BatchNo = request.QueryString["BatchNo"].ToString().Trim();
                SourceType = request.QueryString["SourceType"].ToString().Trim();
                SourceNo = request.QueryString["SourceNo"].ToString().Trim();
                CreatorID = request.QueryString["CreatorID"].ToString().Trim();


                Specification = request.QueryString["Specification"].ToString().Trim();
                ColorID = request.QueryString["ColorID"].ToString().Trim();
                Material = request.QueryString["Material"].ToString().Trim();
                Manufacturer = request.QueryString["Manufacturer"].ToString().Trim();
                Size = request.QueryString["Size"].ToString().Trim();
                FromAddr = request.QueryString["FromAddr"].ToString().Trim();
                BarCode = request.QueryString["BarCode"].ToString().Trim();

                EFIndex = request.QueryString["EFIndex"].ToString().Trim();
                EFDesc = request.QueryString["EFDesc"].ToString().Trim();

                ckbIsM = request.QueryString["ckbIsM"].ToString().Trim();

                DataTable dt = new DataTable();
                string QueryStr = "  a.CompanyCD='" + CompanyCD + "' ";
                if (ddlStorage.Trim().Length > 0 && ddlStorage != "0")
                {
                    QueryStr += " and a.StorageID= '";
                    QueryStr += ddlStorage;
                    QueryStr += "' ";
                }

                if (ProductID.Trim().Length > 0)
                {
                    QueryStr += " and a.ProductID= '";
                    QueryStr += ProductID;
                    QueryStr += "' ";
                }

                if (StartDate.Trim().Length > 0)
                {
                    QueryStr += " and a.HappenDate >='";
                    QueryStr += StartDate + " 00:00:00";
                    QueryStr += "' ";
                }

                if (EndDate.Trim().Length > 0)
                {
                    QueryStr += " and a.HappenDate <='";
                    QueryStr += EndDate + " 23:59:59";
                    QueryStr += "' ";
                }


                if (BatchNo != "0")
                {
                    if (BatchNo == "未设置批次" || string.IsNullOrEmpty(BatchNo))
                    {
                        QueryStr += " and ( a.BatchNo is null or a.BatchNo='') ";
                    }
                    else
                    {
                        QueryStr += " and a.BatchNo='" + BatchNo + "' ";
                    }
                }


                if (SourceType.Trim().Length > 0 && SourceType != "0")
                {
                    QueryStr += " and a.BillType= '";
                    QueryStr += SourceType;
                    QueryStr += "' ";
                }


                if (SourceNo.Trim().Length > 0 && SourceNo != "0")
                {
                    if (ckbIsM == "0")
                    {
                        QueryStr += " and a.BillNo= '";
                        QueryStr += SourceNo;
                        QueryStr += "' ";
                    }
                    else
                    {
                        QueryStr += " and a.BillNo like '%";
                        QueryStr += SourceNo;
                        QueryStr += "%' ";
                    }
                }


                if (CreatorID.Trim().Length > 0 && CreatorID != "0")
                {

                    QueryStr += " and a.Creator= '";
                    QueryStr += CreatorID;
                    QueryStr += "' ";
                }


                if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                {
                    QueryStr += "  and   b.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
                }



                if (!string.IsNullOrEmpty(Specification))
                {
                    QueryStr += "  and   b.Specification LIKE '%" + Specification + "%' ";
                }

                if (!string.IsNullOrEmpty(ColorID) && ColorID != "0")
                {
                    QueryStr += " and    b.ColorID= '" + ColorID + "' ";
                }

                if (!string.IsNullOrEmpty(Material) && Material != "0")
                {
                    QueryStr += "  and   b.Material = '" + Material + "' ";
                }
                if (!string.IsNullOrEmpty(Manufacturer))
                {
                    QueryStr += " and   b.Manufacturer LIKE '%" + Manufacturer + "%' ";
                }
                if (!string.IsNullOrEmpty(Size))
                {
                    QueryStr += " and   b.Size LIKE '%" + Size + "%' ";
                }
                if (!string.IsNullOrEmpty(FromAddr))
                {
                    QueryStr += "  and   b.FromAddr LIKE '%" + FromAddr + "%' ";
                }
                if (!string.IsNullOrEmpty(BarCode))
                {
                    QueryStr += " and   b.BarCode LIKE '%" + BarCode + "%' ";
                }


                string returnStr = StrongeJournalBus.GetSumJournal(QueryStr);


                JsonClass jc;
                jc = new JsonClass("", "", 0);

                if (returnStr.Trim().Length > 0)
                {
                    jc = new JsonClass(returnStr, "", 1);
                }
                else
                {
                    jc = new JsonClass("", "", 0);
                }
                context.Response.Write(jc);

            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}