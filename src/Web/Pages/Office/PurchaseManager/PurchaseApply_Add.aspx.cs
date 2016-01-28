using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Text;
using XBase.Business.Office.SupplyChain;
using System.IO;

public partial class Pages_Office_PurchaseManager_PurchaseApply_Add : BasePage 
{
    #region Apply ID
    public int intApplyID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion

    #region ModuleID
    public int ModuleID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ModuleID"], out tempID);
            return tempID;
        }
    }
    #endregion


    #region From Type
    public int intFromType
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intFromType"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";


        if (!IsPostBack)
        {
            BindApplyReason();
            FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
            FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_APPLY;
            #region 采购类别
            ddlTypeID.TypeCode = "5";
            ddlTypeID.TypeFlag = "7";
            ddlTypeID.IsInsertSelect = true;
            #endregion
            txtApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            codruleApply.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            codruleApply.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_APPLY;
            //设置主生产计划单据编号不可见
            divApplyNo.Attributes.Add("style", "display:none;");
            //自动生成编号的控件设置为可见
            divCodeRule.Attributes.Add("style", "display:block;");

            InitPageStatus();
        }
    }
    private void BindApplyReason()
    {
        DataTable dt = PurchaseApplyDBHelper.GetApplyReason();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlApplyReason.DataSource = dt;
            ddlApplyReason.DataTextField = "CodeName";
            ddlApplyReason.DataValueField = "ID";
            ddlApplyReason.DataBind();

        }
        ddlApplyReason.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    #region  返回相关：初始化页面
    private void InitPageStatus()
    {

        #region 新建、修改共通处理
        //业务单列表模块ID
        //hidListModuleID.Value = this.ListModuleID.ToString();
        //获取请求参数
        string requestParam = Request.QueryString.ToString();
        //通过参数个数来判断是否从菜单过来
        int firstIndex = requestParam.IndexOf("&");
        //从列表过来时
        if (firstIndex > 0)
        {
            if (this.intFromType > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
            }
            else
            {
                btnBack.Visible = true;
            }
            //获取列表的查询条件
            string searchCondition = requestParam.Substring(firstIndex);
            //设置检索条件
            hidSearchCondition.Value = searchCondition;
        }
        //设置隐藏域条码的值
        //if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        //{
        //    hidCodeBar.Value = "1";
        //}
        //else
        //{
        //    hidCodeBar.Value = "0";
        //}
        #endregion
    }
    #endregion

    /*记录异常信息*/
    string ErrorMsg = string.Empty;
    protected void imgExportExcelUC_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

        /*文件名*/
        string FileName = string.Empty;
        /*上传后完整的文件路径包含文件名*/
        string FileNewUrl = string.Empty;

        /*验证登陆*/
        if (UserInfo == null)
            ErrorMsg += "登录已超时，请重新登陆\\n";

        #region 上传验证
        /*获取公司的上传路径*/
        string FileUrl = ProductInfoBus.GetCompanyUpFilePath(UserInfo.CompanyCD);

        /*验证该公司路径是否存在 不存在则创建*/
        DirectoryInfo dir = new DirectoryInfo(FileUrl);
        if (!dir.Exists)
        {
            try
            {
                dir.Create();
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString() + "\\n";
            }
        }
        /*验证是否选择了文件*/
        if (string.IsNullOrEmpty(fileExportExcel.PostedFile.FileName))
        {
            MessageBox("请选择需要导入的Excel文件", "msg1");
            return;
        }



        /*验证文件类型*/
        string FileExtension = fileExportExcel.PostedFile.FileName.Split('.')[1].ToUpper();
        if (FileExtension != "XLS" && FileExtension != "XLSX")
            ErrorMsg += "文件错误，请上传正确的Excel文件\\n";

        /*判断是否存在异常*/
        if (!string.IsNullOrEmpty(ErrorMsg))
        {
            MessageBox(ErrorMsg, "msg2");
            return;
        }

        /*上传文件*/
        FileName = Guid.NewGuid().ToString() + "." + FileExtension.ToLower();
        FileNewUrl = FileUrl + "\\" + FileName;
        try
        {
            fileExportExcel.PostedFile.SaveAs(FileNewUrl);
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.ToString() + "\\n";
        }
        #endregion

        #region Excel转换成datatable
        /*验证是否包含异常信息*/
        if (!string.IsNullOrEmpty(ErrorMsg))
        {
            MessageBox(ErrorMsg, "msg3");
            return;
        }


        /*将Excel转换成DataTable*/
        System.Data.DataTable dt = ExcelToDataTable(FileNewUrl);

        /*验证Excel是否为空*/
        if (dt == null || dt.Rows.Count <= 0)
        {
            MessageBox("Excel文件为空，请重新上传Excel文件", "msg6");
            return;
        }

        /*将Excel转换成DataTable后，抛弃上传的Excel文件*/
        DelExcel(FileNewUrl);

        /*重新设置列名*/
        dt.Columns[0].ColumnName = "ProductName";
        dt.Columns[1].ColumnName = "ApplyCount";
        dt.Columns[2].ColumnName = "ApplyDate";

        /*重复商品验证*/
        Hashtable htKeys = new Hashtable();
        Hashtable htHas = new Hashtable();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow fRow = dt.Rows[i];
            for (int j = i + 1; j < dt.Rows.Count; j++)
            {
                DataRow sRow = dt.Rows[j];
                if (fRow["ProductName"].ToString() == sRow["ProductName"].ToString() && !htHas.ContainsKey(j))
                {
                    if (htKeys.ContainsKey(fRow["ProductName"].ToString()))
                    {
                        htKeys[fRow["ProductName"].ToString()] += (j + 2).ToString() + "，";
                        htHas.Add(j, j);
                    }
                    else
                    {
                        htKeys.Add(fRow["ProductName"].ToString(), (i + 2).ToString() + "，" + (j + 2).ToString() + "，");
                        htHas.Add(i, i);
                        htHas.Add(j, j);
                    }
                }
            }
        }

        /*提示错误信息*/
        if (htKeys.Count > 0)
        {
            foreach (string key in htKeys.Keys)
            {
                ErrorMsg += "商品 " + key + " 同时出现在第 " + htKeys[key].ToString().Substring(0, htKeys[key].ToString().Length - 1) + " 行\\n";
            }
            ErrorMsg += "可直接按 Ctrl+C 复制该对话框";
            MessageBox(ErrorMsg, "msg7");
            return;
        }



        /*验证数值*/
        Regex IsNumeric = new Regex(@"^(-?\d+)(\.\d+)?$");
        /*验证日期*/
        Regex IsDate = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$");

        /*验证需求数量与需求日期 格式*/
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            /*验证是否有空值存在*/
            if (row["ProductName"] == null || row["ProductName"].ToString() == string.Empty ||
               row["ApplyCount"] == null || row["ApplyCount"].ToString() == string.Empty ||
               row["ApplyDate"] == null || row["ApplyDate"].ToString() == string.Empty)
            {
                ErrorMsg += "第" + (i + 2).ToString() + "行数据存在空值\\n";
                continue;
            }
            /*验证需求数量格式*/
            if (!IsNumeric.IsMatch(row["ApplyCount"].ToString()))
            {
                ErrorMsg += "第" + (i + 2).ToString() + "行需求数量格式错误，不是数值类型\\n";
            }
            /*验证需求日期格式*/
            if (!IsDate.IsMatch(row["ApplyDate"].ToString()))
            {
                ErrorMsg += "第" + (i + 2).ToString() + "行需求日期格式错误，不是有效的日期类型。例：" + DateTime.Now.ToString("yyyy-MM-dd") + "\\n";
            }
        }


        if (!string.IsNullOrEmpty(ErrorMsg))
        {
            ErrorMsg += "可直接按 Ctrl+C 复制该对话框";
            MessageBox(ErrorMsg, "msg5");
            return;
        }


        /*构造商品名称字符，用于数据库检索*/
        string ProductNameKeys = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == dt.Rows.Count - 1)
            {
                ProductNameKeys += "'" + dt.Rows[i]["ProductName"].ToString() + "'";
            }
            else
            {
                ProductNameKeys += "'" + dt.Rows[i]["ProductName"].ToString() + "',";
            }

        }

        /*使用商品名称查询数据库*/
        DataTable dtQuery = XBase.Business.Office.PurchaseManager.PurchaseApplyBus.GetGoodsByProductName(UserInfo.CompanyCD, ProductNameKeys);

        /*验证Excel中的商品是否存在*/
        if (dtQuery.Rows.Count != dt.Rows.Count)
        {
            //存在未找到的商品 遍历比较
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool Flag = false;
                foreach (DataRow row in dtQuery.Rows)
                {
                    if (row["ProductName"].ToString() == dt.Rows[i]["ProductName"].ToString())
                    {
                        Flag = true;
                    }
                }

                if (!Flag)
                {
                    ErrorMsg += "第" + (i + 2).ToString() + "行未找到商品名称为 " + dt.Rows[i]["ProductName"].ToString() + " 的商品，请检查产品名称是否正确\\n";
                }
            }

        }

        if (!string.IsNullOrEmpty(ErrorMsg))
        {
            MessageBox(ErrorMsg, "msgr");
            return;
        }

        /*重新构造DataTable*/
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add(new DataColumn("ID", typeof(string)));
        dtNew.Columns.Add(new DataColumn("ProdNo", typeof(string)));
        dtNew.Columns.Add(new DataColumn("ProductName", typeof(string)));
        dtNew.Columns.Add(new DataColumn("Specification", typeof(string)));
        dtNew.Columns.Add(new DataColumn("ColorName", typeof(string)));
        
        dtNew.Columns.Add(new DataColumn("UnitID", typeof(string)));
        dtNew.Columns.Add(new DataColumn("CodeName", typeof(string)));
        dtNew.Columns.Add(new DataColumn("ApplyCount", typeof(string)));
        dtNew.Columns.Add(new DataColumn("ApplyDate", typeof(string)));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow drow = dt.Rows[i];
            foreach (DataRow row in dtQuery.Rows)
            {
                if (row["ProductName"].ToString() == drow["ProductName"].ToString())
                {
                    DataRow newRow = dtNew.NewRow();
                    newRow["ID"] = row["ID"].ToString();
                    newRow["ProdNo"] = row["ProdNo"].ToString();
                    newRow["ProductName"] = row["ProductName"].ToString();
                    newRow["Specification"] = row["Specification"].ToString();
                    newRow["ColorName"] = row["ColorName"].ToString();
                    newRow["CodeName"] = row["CodeName"].ToString();
                    string rr=drow["ApplyCount"].ToString();
                    newRow["ApplyCount"] = drow["ApplyCount"].ToString();


                    newRow["ApplyDate"] = Convert.ToDateTime(drow["ApplyDate"].ToString()).ToString("yyyy-MM-dd");
                    newRow["UnitID"] = row["UnitID"].ToString();
                    dtNew.Rows.Add(newRow);
                }
            }
        }

        /*将数据添加到页面*/
        decimal   TotalCount = 0; 
        divDetailTbl.InnerHtml = CreateHtml(dtNew, ref TotalCount);
        txtCountTotal.Value = TotalCount.ToString("0.00");
        divDetailTb2.InnerHtml = CreateHtmlForTwo(dtNew);


        #endregion
    }


    /*将Excel转换成datatable */
    protected System.Data.DataTable ExcelToDataTable(string FileUrl)
    {
        /*转换成DataTable*/
        string SheetName = "Sheet1";//默认为sheet1
        string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FileUrl + ";Extended Properties='Excel 8.0;IMEX=1'";
        string strExcel = string.Format("select * from [{0}$]", SheetName);
        DataSet ds = new DataSet();
        using (OleDbConnection conn = new OleDbConnection(strConn))
        {
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
            adapter.Fill(ds, SheetName);
            conn.Close();
        }
        System.Data.DataTable dt = ds.Tables[SheetName];
        return dt;
    }


    protected new UserInfoUtil UserInfo
    {
        get
        {
            if (SessionUtil.Session["UserInfo"] != null)
                return (UserInfoUtil)SessionUtil.Session["UserInfo"];
            else
                return null;
        }
    }

    /*提示对话框*/
    protected void MessageBox(string msg, string key)
    {
        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), key, "<script type=\"text/javascript\">alert(\"" + msg + "\");</script>");
    }

    /*删除上传的Excel*/
    protected void DelExcel(string FileUrl)
    {
        FileInfo fileinfo = new FileInfo(FileUrl);
        if (fileinfo.Exists)
        {
            try
            {
                fileinfo.Delete();
            }
            catch
            { }
        }
    }

    /*构造HTML*/
    protected string CreateHtml(DataTable dt, ref decimal  TotalCount)
    {
        /*读取原因*/
        DataTable dtReason = PurchaseApplyDBHelper.GetApplyReason();

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.AppendLine("  <table width=\"99%\" border=\"0\" id=\"dg_Log\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\"    bgcolor=\"#999999\">");
 
       sbHtml.AppendLine("                 <tr>");
       sbHtml.AppendLine("                    <td bgcolor=\"#E6E6E6\" class=\"Blue\" width=\"50\" align=\"center\">    <input type=\"checkbox\" name=\"checkall\" id=\"checkall\" onclick=\"SelectAll()\" title=\"全选\"  style=\"cursor: hand\" /> </td>");

       sbHtml.AppendLine("                     <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">  序号</td>");
 
 
        sbHtml.AppendLine("                  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\"> 物品编号<span class=\"redbold\">*</span> </td>");

        sbHtml.AppendLine("                  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">物品名称<span class=\"redbold\">*</span> </td>");

        sbHtml.AppendLine("                    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">规格  </td>");

        sbHtml.AppendLine("                    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">颜色  </td>");

        sbHtml.AppendLine("                       <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">   <span id=\"spUnitID\">单位</span>  <span class=\"redbold\">*</span>  </td>");
        sbHtml.AppendLine("                <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\" style=\"width:3%; display:none \" id=\"spUsedUnitID\">单位 </td>");

        sbHtml.AppendLine("                       <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" style=\"width:3%\"> <span id=\"SpProductCount\">需求数量</span><span class=\"redbold\" id=\"spCount\">*</span></td>");

        sbHtml.AppendLine("                  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"  valign=\"middle\" style=\"width:7%; display:none \" id=\"spUsedUnitCount\">  需求数量 <span class=\"redbold\">*</span> </td>");

        sbHtml.AppendLine("                   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">需求日期<span class=\"redbold\">*</span></td>");

        sbHtml.AppendLine("                    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">  申请原因 </td>");

        sbHtml.AppendLine("                    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\">   源单编号 </td>");

        sbHtml.AppendLine("                      <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" >    源单序号</td>"); 
        sbHtml.AppendLine("                </tr>");
        txtTRLastIndex.Value = (Convert.ToInt32(txtTRLastIndex.Value) + dt.Rows.Count).ToString();
        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            decimal ApplyCount1 = row["ApplyCount"] == null ? 0 : Convert.ToDecimal(row["ApplyCount"].ToString ());
            TotalCount += ApplyCount1;



            sbHtml.AppendLine("<tr id=\"Item_Row_" + i.ToString() + "\">");

            sbHtml.AppendLine("<td class=\"cell\"><input name='chk' id='chk_Option_" +  i.ToString()  + "' value=\"0\" type='checkbox' onclick=\"ClearAllOption();\" /></td> ");

            sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id=\"TD_Text_SortNo_" + i.ToString() + "\" value=\"" + i.ToString() + " \"  class=\"tdinput\" size=\"5\" disabled=\"disabled\" /></td>");

            sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id=\"TD_Text_ProdNo_" + i.ToString() + "\" value=\"" + row["ProdNo"].ToString() + "\" class=\"tdinput\" size=\"8\" onclick=\"popTechObj.ShowList('" + i.ToString() + "');\" readonly=\"readonly\" /></td>");

            sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"hidden\" id=\"Hidden_TD_Text_ProductID_" + i.ToString() + "\" value=\"" + row["ID"].ToString() + "\"  /><input type=\"text\" id=\"TD_Text_ProductID_" + i.ToString() + "\" value=\"" + row["ProductName"].ToString() + "\" class=\"tdinput\" size=\"10\" onclick=\"popTechObj.ShowList('" + i.ToString() + "');\" readonly=\"readonly\" /></td>");

            sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id=\"TD_Text_Specification_" + i.ToString() + "\" value=\"" + row["Specification"].ToString() + "\" class=\"tdinput\" size=\"8\" readonly=\"readonly\" /></td>");
            sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id=\"DtlSColor" + i.ToString() + "\" value=\"" + row["ColorName"].ToString() + "\" class=\"tdinput\" size=\"8\" readonly=\"readonly\" /></td>");
            if (!((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + i.ToString() + "' value=\"" + row["UnitID"].ToString() + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + i.ToString() + "' value=\"" + row["CodeName"].ToString() + "\" class=\"tdinput\"  size=\"6\" readonly=\"readonly\"/></td>");

                decimal ApplyCount = decimal.Parse(row["ApplyCount"] == null ? "0" : row["ApplyCount"].ToString());

                sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id='TD_Text_ProductCount_" + i.ToString() + "'   class=\"tdinput\" size=\"10\"  value=\"" + ApplyCount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint) + "\" onblur=\"fnCalculateTotal();\" /></td>");
            }
            else
            {
                sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"hidden\"  id='Hidden_TD_Text_UnitID_" + i.ToString() + "' value=\"" + row["UnitID"].ToString() + "\" /><input type=\"text\"  id='TD_Text_UnitID_" + i.ToString() + "' value=\"" + row["CodeName"].ToString() + "\" class=\"tdinput\"  size=\"6\" readonly=\"readonly\"/></td>");

                string unitHtml = "";
                decimal tmpExRate = 1;/*换算率*/

                /*计量单位HTML*/
                unitHtml = XBase.Business.Common.UnitGroup.GetUnitGroupByProductId(row["ID"].ToString(), "InUnit", "SignItem_TD_UnitID_Select" +  i.ToString() , "ChangeUnit(this," +  i.ToString()  + ")", "", ref tmpExRate);
                decimal ApplyCount = decimal.Parse(row["ApplyCount"] == null ? "0" : row["ApplyCount"].ToString());

                decimal usedCount = 0;
                if (tmpExRate != 0)
                {
                usedCount=    ApplyCount * tmpExRate;
                }
                    

                string us = usedCount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

                sbHtml.AppendLine("<td  class=\"cell\"><div id=\"unitdiv" +  i.ToString()  + "\">" + unitHtml + "</div></td>");

                sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id='TD_Text_ProductCount_" + i.ToString() + "'   class=\"tdinput\" size=\"10\" value=\"" + us+ "\"   onblur=\"fnCalculateTotal();\" /></td>");

                sbHtml.AppendLine("<td class=\"cell\"  ><input id='UsedUnitCount" + i.ToString() + "' type='text' class=\"tdinput\"  value=\"" + ApplyCount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint) + "\"  style='width:90%;' onblur=\"Number_round(this," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "); ChangeUnit(this.id," + i.ToString() + ")\"  /></td>");







            } 

     sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id='TD_Text_StartDate_" +  i.ToString()  + "' value=\""+row["ApplyDate"].ToString()+"\"  class=\"tdinput\"  onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('TD_Text_StartDate_" + i.ToString()  + "')})\" size=\"10\"    onpropertychange =\"fnCalculateTotal();\"/></td>");

                 sbHtml.AppendLine("<td class=\"cell\"  >"+CreateSelectForReason(dtReason, i.ToString())+" </td>");

                 sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id='TD_Text_FromBillNo_" + i.ToString() + "' value=\"\" class=\"tdinput\" size=\"10\" readonly=\"readonly\" disabled=\"disabled\" /><input type=\"hidden\" id='TD_Text_FromBillID_" + i.ToString() + "' value=\"0\" class=\"tdinput\" size=\"10\" /></td>");

                 sbHtml.AppendLine("<td class=\"cell\"  ><input type=\"text\" id='TD_Text_FromLineNo_" + i.ToString() + "' value=\"\" class=\"tdinput\"  size=\"5\" readonly=\"readonly\" disabled=\"disabled\" /></td>");

      sbHtml.AppendLine("</tr>");
            i++;
        }
        sbHtml.AppendLine("      </table> ");
        return sbHtml.ToString();
    }

    /*构造第二个表格*/
    protected string CreateHtmlForTwo(DataTable dt)
    {
        StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine("  ");
              sbHtml.AppendLine("    <table width=\"99%\" border=\"0\" id=\"dg_LogSecond\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\"");
              sbHtml.AppendLine("           bgcolor=\"#999999\">");
               sbHtml.AppendLine("          <tr>");
                  sbHtml.AppendLine("           <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" align=\"center\">");
              sbHtml.AppendLine("                   序号");
                sbHtml.AppendLine("             </td>");
                  sbHtml.AppendLine("           <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
                  sbHtml.AppendLine("               物品编号");
                 sbHtml.AppendLine("            </td>");
                  sbHtml.AppendLine("           <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
                  sbHtml.AppendLine("               物品名称");
                   sbHtml.AppendLine("          </td>");
                   sbHtml.AppendLine("          <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
                   sbHtml.AppendLine("              规格");
                  sbHtml.AppendLine("           </td>");

                  sbHtml.AppendLine("          <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
                  sbHtml.AppendLine("              颜色");
                  sbHtml.AppendLine("           </td>");


                  sbHtml.AppendLine("             <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
                 sbHtml.AppendLine("               <span id=\"spUnitID2\">单位</span> ");
                sbHtml.AppendLine("         </td>");
               sbHtml.AppendLine("          <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\" style=\"width:3%; display:none \" id=\"spUsedUnitID2\">");
                  sbHtml.AppendLine("          单位 ");
                 sbHtml.AppendLine("        </td> ");
               sbHtml.AppendLine("             <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" style=\"width:3%\">");
                  sbHtml.AppendLine("           <span id=\"SpProductCount2\">申请数量</span> ");
                 sbHtml.AppendLine("        </td>");
                   sbHtml.AppendLine("        <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"  valign=\"middle\" style=\"width:7%; display:none \" id=\"spUsedUnitCount2\">");
                  sbHtml.AppendLine("           申请数量 <span class=\"redbold\">*</span>");
               sbHtml.AppendLine("          </td>");
                        
                            
                   sbHtml.AppendLine("          <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
                 sbHtml.AppendLine("                需求日期");
               sbHtml.AppendLine("              </td>");
               sbHtml.AppendLine("              <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
              sbHtml.AppendLine("                   已计划数量");
                sbHtml.AppendLine("             </td>");
                sbHtml.AppendLine("         </tr>");
               sbHtml.AppendLine("     </table>");




 
     
     

        return sbHtml.ToString();
    }

    /*构造原因下拉控件*/
    protected string CreateSelectForReason(DataTable dt, string RowID)
    {
       
        StringBuilder sbSelect = new StringBuilder();
        sbSelect.AppendLine("<select class='tdinput' id=\"TD_Text_Reason_" + RowID + "\">");
        sbSelect.AppendLine("<option value=\"\">--请选择--</option>");
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                sbSelect.AppendLine("<option value=\"" + row["ID"].ToString() + "\">" + row["CodeName"].ToString() + "</option>");
            }

        }

        sbSelect.AppendLine("</select>");

        return sbSelect.ToString();
    }
}





