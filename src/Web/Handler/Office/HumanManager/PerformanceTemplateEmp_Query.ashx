<%@ WebHandler Language="C#" Class="PerformanceTemplateEmp_Query" %>
/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/04/21
 * 描    述： 考核类型设置
 * 修改日期： 2009/04/23
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class PerformanceTemplateEmp_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
         string action = context.Request.Params["Action"];
        //保存评分项目信息  
        string ID = context.Request.Params["DeptID"];
        //初期化评分项目树时
        if ("InitTree".Equals(action))
        {
            //定义变量
            StringBuilder sbDeptTree = new StringBuilder();
            //获取评分项目数据
            DataTable dtDeptInfo = PerformanceElemBus.SearchDeptInfo(ID);
            //评分项目数据存在时
            if (dtDeptInfo != null && dtDeptInfo.Rows.Count > 0)
            {
                //获取记录数
                int deptCount = dtDeptInfo.Rows.Count;
                //遍历所有组织机构，以显示数据
                for (int i = 0; i < dtDeptInfo.Rows.Count; i++)
                {
                    //获取评分项目ID
                    string deptID = GetSafeData.GetStringFromInt(dtDeptInfo.Rows[i], "ElemNo");
                    //或评分项目名称
                    string deptName = GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[i], "ElemName");
                    //获取父评分项目
                    string superDeptID = GetSafeData.GetStringFromInt(dtDeptInfo.Rows[i], "ParentElemNo");
                    //获取是否有子评分项目 
                    int subCount = GetSafeData.ValidateDataRow_Int(dtDeptInfo.Rows[i], "SubCount");

                    //样式名称
                    string className = "file";
                    //Javascript事件名
                    string jsAction = "";
                    //样式表名称
                    string showClass = "list";

                    //有子结点时
                    if (subCount > 0)
                    {
                        //最后一个结点
                        if (i == deptCount - 1)
                        {
                            className = "folder_close_end";
                            showClass = "list_last";
                        }
                        else
                        {
                            className = "folder_close";
                        }
                        jsAction = "onclick=\"ShowDeptTree('" + deptID + "');\"";
                    }
                    else if (i == deptCount - 1)
                    {
                        className = "file_end";
                    }
                    //生成子树代码
                    sbDeptTree.Append("<table border='0' cellpadding='0' cellspacing='0'>");
                    sbDeptTree.Append("<tr><td><div id='divSuper_" + deptID + "' class='" + className + "' " + jsAction
                                    + " alt='" + deptName + "'><a  href='#' onclick=\"SetSelectValue('"
                                    + deptID + "','" + deptName + "','" + superDeptID + "');\"><div id='divDeptName_" + deptID
                                    + "'>" + " " + deptName + "</div></a></div>");
                    sbDeptTree.Append("<div id='divSub_" + deptID + "' style='display:none;' class='" + showClass + "'></div></td></tr>");
                    sbDeptTree.Append("</table>");
                }
            }
            //返回生成的评分项目树 
            context.Response.Write(sbDeptTree.ToString());
        }
        else if ("EditInfo".Equals(action))
        {
            PerformanceElemModel model = new PerformanceElemModel();
            //编辑模式
            model.EditFlag = context.Request.QueryString["EditFlag"];
            //类型ID 
            model.ID = context.Request.QueryString["ElemID"];
            //类型名称
            model.ElemName = context.Request.QueryString["ElemName"];
            //启用状态
            model.UsedStatus = context.Request.QueryString["UsedStatus"];
            //指标标准分
            model.StandardScore = context.Request.QueryString["StandardScore"];
            //指标最小分
            model.MinScore = context.Request.QueryString["MinScore"];
            //指标最大分
            model.MaxScore = context.Request.QueryString["MaxScore"];
            //评分标准
            model.AsseStandard = context.Request.QueryString["AsseStandard"];
            //评分来源
            model.AsseFrom = context.Request.QueryString["AsseFrom"];
            //备注
            model.Remark = context.Request.QueryString["Remark"];
            //评分细则
            model.ScoreRules = context.Request.QueryString["ScoreRules"];
            // 指标编号
            model.ElemNo = context.Request.QueryString["ElemNo"];
            //父指标编号

            string parentElemNo = context.Request.QueryString["ParentElemNo"];
            //判断节点是否为新建顶级节点，如果是，将ParentElemNo置空
            if (parentElemNo == "undefined")
            {
                model.ParentElemNo = "";
            }
            else
            {
                model.ParentElemNo = parentElemNo;
            }
            //执行保存 

            bool isSucc = PerformanceElemBus.SaveRectCheckElemInfo(model);
            //成功
            if (isSucc)
            {

                context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.ID + "|" + model.ParentElemNo + "|" + model.ElemName, 1));
            }
            //失败
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }

        //修改时获取数据操作 
        else if ("GetInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["ElemID"];
            //获取考核类型信息

            DataTable dtDeptInfo = PerformanceElemBus.GetRectCheckElemInfoWithID(elemID);

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
            }
            else
            {
                sbReturn.Append("[]");
            }
            sbReturn.Append("}");
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            context.Response.End();
        }
        //查询数据操作 
        else if ("SearchInfo".Equals(action))
        {
            //执行查询
            SearchData(context);
        }
        else if ("SearchFlowInfo".Equals(action))
        {
            //执行查询
            SearchFlowData(context);
        }
        //删除操作
        else if ("DeleteInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["DeleteNO"];
            //替换引号
            elemID = elemID.Replace("'", "");
            //判断要素是否被使用
           // bool isUsed = PerformanceTemplateBus.IsTemplateUsed(elemID);
            //已经被使用
            //if (isUsed)
            //{
            //    //输出响应 返回不执行删除
            //    context.Response.Write(new JsonClass("", "", 2));
            //}
            //else
            //{
                //删除要素
            
         
                bool isSucc = PerformanceTemplateEmpBus.DeletePerTemplateEmpInfo(elemID); 
                //删除成功
                if (isSucc)
                {
                    //输出响应
                    context.Response.Write(new JsonClass("", "", 1));
                }
                //删除失败
                else
                {
                    //输出响应
                    context.Response.Write(new JsonClass("", "", 0));
                }
            //}
        }
        else if ("InsertInfo".Equals(action))
        {

           InsertData(context);
        
        }
        else if ("GetEmployeeInfo".Equals(action))
        {


            //获取要素ID
            string employeeID = context.Request.QueryString["EmployeeID"];
            string templateNo = context.Request.QueryString["templateNo"];
            //获取考核类型信息

            PerformanceTemplateEmpModel model=new PerformanceTemplateEmpModel ();
            model.EmployeeID =employeeID;
            model.TemplateNo = templateNo;
            DataTable dtDeptInfo = PerformanceTemplateEmpBus.GetEmployeeInfo(model);

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
            sbReturn.Append("}");
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            context.Response.End();
        }
        else if ("InsertUpdateInfo".Equals(action))
        {

            InsertUpdateMessage(context);
        
        
        }
    }
    //"&EmployeeID="+EmployeeID +"&ScoreEmployee="+ScoreEmployee+"&TemplateNo="+TemplateNo;
    private void InsertData(HttpContext context)
    {


        string employeeID = context.Request.QueryString["EmployeeID"];
        string scoreEmployee = context.Request.QueryString["ScoreEmployee"];
        string templateNo = context.Request.QueryString["TemplateNo"];

        string[] employeeIDList = employeeID.Split(',');
        string[] templateNoList = templateNo.Split(',');
        string [] scoreEmployeeList = scoreEmployee.Split(',');
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
       
       IList <PerformanceTemplateEmpModel > modelList=new List <PerformanceTemplateEmpModel>();
        foreach (string id in employeeIDList)
        {

            foreach (string no in templateNoList)
            {

                int length = scoreEmployeeList.Length;

                for (int m = 0; m < length-2;m= m + 5)
                {
                    
                    
                    PerformanceTemplateEmpModel  model = new PerformanceTemplateEmpModel();

                    model.EmployeeID = id;
                    model.TemplateNo = no;
                    model.Rate = scoreEmployeeList[m + 2];
                    model.StepName = scoreEmployeeList[m + 1];
                    model.StepNo = scoreEmployeeList[m + 3];
                    model.ScoreEmployee = scoreEmployeeList[m];
                    model.CompanyCD = userInfo.CompanyCD;
                    model.ModifiedUserID = userInfo.EmployeeID.ToString ();
                    model.EditFlag = "INSERT";
                    model.remark = scoreEmployeeList[m+4];
                    modelList.Add(model);
                    
                    
                    
                     
                }
                
                
                
                
                
                 
            }
             
        }

        bool isSucc =PerformanceTemplateEmpBus .InsertPerformenceTempElm (modelList,null  );
        //成功
        if (isSucc)
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        //失败
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
        
        
        
        
         
    }
    private void SearchFlowData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        string ScoreEmployeeID = context.Request.QueryString["ScoreEmployee"];
        string EmployeeID = context.Request.QueryString["EmployeeID"];
            
        string tempLateName=context.Request.QueryString["TemplateName"];
        
        
        //获取数据
        PerformanceTemplateEmpModel searchModel = new PerformanceTemplateEmpModel();
        searchModel.EmployeeID = EmployeeID;
        searchModel.ScoreEmployee = ScoreEmployeeID;
        //设置查询条件
        //要素名称
       /// searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
       /// searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];
 
        //查询数据
        DataTable dtData = PerformanceTemplateEmpBus .SearchFlowInfo (searchModel);

        if (!string.IsNullOrEmpty(tempLateName))
        {

            dtData = GetNewDataTable(dtData, "TemplateName like '%"+tempLateName +"%'");
        
        }
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTemplateEmpModel()
             {
                 EmployeeName = x.Element("EmployeeName")==null ?"":x.Element("EmployeeName").Value,//ID
                 TemplateName = x.Element("TemplateName") == null ? "" : x.Element("TemplateName").Value,//类型名称
                 StepName = x.Element("StepName") == null ? "" : x.Element("StepName").Value,//类型名称 
                 ScoreEmployeeName = x.Element("ScoreEmployee") == null ? "" : x.Element("ScoreEmployee").Value,//类型名称
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//类型名称
                 EmployeeID = x.Element("EmployeeID") == null ? "" : x.Element("EmployeeID").Value,//类型名称
                 TemplateNo = EmployeeID = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value


             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTemplateEmpModel()
             {
                 EmployeeName = x.Element("EmployeeName") == null ? "" : x.Element("EmployeeName").Value,//ID
                 TemplateName = x.Element("TemplateName") == null ? "" : x.Element("TemplateName").Value,//类型名称
                 StepName = x.Element("StepName") == null ? "" : x.Element("StepName").Value,//类型名称 
                 ScoreEmployeeName = x.Element("ScoreEmployee") == null ? "" : x.Element("ScoreEmployee").Value,//类型名称
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//类型名称
                 EmployeeID = x.Element("EmployeeID") == null ? "" : x.Element("EmployeeID").Value,//类型名称
                 TemplateNo = EmployeeID = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value

             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    
    private void SearchData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TemplateNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据
        PerformanceTemplateModel searchModel = new PerformanceTemplateModel();
        //设置查询条件
        //要素名称
       /// searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
       /// searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];
 
        //查询数据
        DataTable dtData = PerformanceTemplateEmpBus.SearchRectCheckElemInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("TemplateNo").Value,//ID
                 Title = x.Element("Title").Value//类型名称



             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("TemplateNo").Value,//ID
                 Title = x.Element("Title").Value//类型名称
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private DataTable GetNewDataTable(DataTable dt, string condition)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }


    private void InsertUpdateMessage(HttpContext context)
    {
        string messageList = context.Request.QueryString["messageList"];
        string TemplateNo = context.Request.QueryString["TemplateNo"];
        string[] oldTemplateNo = TemplateNo.Split(',');
        string[] newTemplate = GetString(oldTemplateNo );
        string[] messageBlock = messageList.Split(',');
        DataTable temp = new DataTable();
        DataColumn newDC;
        newDC = new DataColumn("EmployeeID", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);
        newDC = new DataColumn("TemplateNo", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);
        newDC = new DataColumn("ScoreEmployee", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);
        newDC = new DataColumn("StepNo", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);
        newDC = new DataColumn("StepName", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);

        newDC = new DataColumn("Rate", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);
        newDC = new DataColumn("Remark", System.Type.GetType("System.String"));
        temp.Columns.Add(newDC);
        bool signRate = false;
        bool signStepName = false;
        for (int i = 0; i < messageBlock.Length-6; i = i + 7)
        {
            DataRow newRow = temp.NewRow();
            newRow[0] = messageBlock[i].ToString ().Trim ();
            newRow[1] = messageBlock[i + 1].ToString().Trim();

            newRow[2] = messageBlock[i + 2].ToString().Trim();
            newRow[3] = messageBlock[i + 3].ToString().Trim();

            newRow[4] = messageBlock[i + 4].ToString().Trim();
            newRow[5] = messageBlock[i + 5].ToString().Trim();
            newRow[6] = messageBlock[i + 6].ToString().Trim();
            temp.Rows.Add(newRow );
        }
        
             ArrayList  rateList=new ArrayList ();
            ArrayList stepNoList=new ArrayList ();
        for (int a=0;a<newTemplate.Length ;a++)
        {
       
            DataTable dt = GetNewDataTable(temp, "TemplateNo = '" + newTemplate [a] + "'");
            int rowsCount=dt.Rows.Count ;
            for (int i = 0; i < rowsCount; i++)
            {
                rateList.Add (dt.Rows [i][5]==null ?"0":dt.Rows [i][5].ToString ());
                stepNoList.Add(dt.Rows[i][3] == null ? "10000" : dt.Rows[i][3].ToString());
                 
            }
         
              
            
         }
        if (CheckRate(rateList))
        {
            signRate = true;
      
        }
        if (CheckStepNo(stepNoList, temp .Rows.Count))
        {
            signStepName = true;

         
        }
        
        if (signRate)
        {
            context.Response.Write(new JsonClass("", "员工的一个模板必须权重信息相加为100", 0));
        }
        else if (signStepName)
        {
            context.Response.Write(new JsonClass("", "考核步骤没有按顺序排列", 2));
             
        }
        //失败
        else
        {
            IList<PerformanceTemplateEmpModel> modelList = new List<PerformanceTemplateEmpModel>();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            for (int i = 0; i < messageBlock.Length - 6; i = i + 7)
            {
                PerformanceTemplateEmpModel model=new PerformanceTemplateEmpModel ();
                DataRow newRow = temp.NewRow();
                model .EmployeeID  = messageBlock[i];
                model .TemplateNo  = messageBlock[i + 1];

                model.ScoreEmployee  = messageBlock[i + 2];
                model.StepNo  = messageBlock[i + 3];

                model.StepName = messageBlock[i + 4];
                model.Rate  = messageBlock[i + 5];
                model.remark  = messageBlock[i + 6];
                model.EditFlag = "UPDATE";
                model.ModifiedUserID = userInfo.EmployeeID.ToString();
                model.CompanyCD = userInfo.CompanyCD;
                modelList.Add(model );
            }
            bool signEdit;
            signEdit = PerformanceTemplateEmpBus.InsertPerformenceTempElm(modelList,newTemplate );
            if (signEdit)
            {

                context.Response.Write(new JsonClass("", "", 1));
            }
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }
       
    
    
    
    
    }
    private bool CheckRate(ArrayList rateList)
    {
        double   sum = 0.00;
        foreach (string i in rateList)
        {
            sum = sum + Convert.ToDouble (i);
        
        }
        if ((sum < 100.00) || (sum > 100.00))
        {
         
             return true;
        }
        else
        {
             return false;
        }
    
    
    }
    private bool CheckStepNo(ArrayList stepNoList, int rowsCount)
    {
        bool sign = false;
        for (int c = 1; c <= rowsCount; c++)
        {
            if (stepNoList.Contains(c.ToString ()))
            {
                continue;
            }
            else
            {
                sign = true; 
            }
             
        }
        return sign;

    }
    public static string[] GetString(string[] myData)
    {
        if (myData.Length > 0)
        {
            Array.Sort(myData); //先对数组进行排序
            int size = 1;  //定义删除重复项后的数组长度 
            for (int i = 1; i < myData.Length; i++)
                if (myData[i] != myData[i - 1])
                    size++;
            String[] myTempData = new String[size];
            int j = 0;

            myTempData[j++] = myData[0];

            for (int i = 1; i < myData.Length; i++) //遍历数组成员 
                if (myData[i] != myData[i - 1])  //如果相邻的两个不相等则放入新数组
                    myTempData[j++] = myData[i];

            return myTempData;
        }
        return myData;


    }
    
    
    
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}