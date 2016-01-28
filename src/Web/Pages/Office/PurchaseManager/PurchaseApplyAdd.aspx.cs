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




public partial class Pages_Office_PurchaseManager_Purchase_Add : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        {
            btnGetGoods.Visible = true;
        }
        else
        {
            btnGetGoods.Visible = false;
        }
        if (!Page.IsPostBack)
        {   // 绑定原因
            BindApplyReason();
            // 绑定审批流程
            BindFlowApply();

            #region 采购类别
            ddlTypeID.TypeCode = "5";
            ddlTypeID.TypeFlag = "7";
            ddlTypeID.IsInsertSelect = true;
            #endregion
            if (Request.QueryString["ID"] == null)
            {
          
                UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                txtCreatorID.Value = userInfo.EmployeeID.ToString();
                txtCreatorName.Value = userInfo.EmployeeName;
                txtModifiedUserID.Value = userInfo.UserID;
                txtModifiedUserName.Value = userInfo.UserID;
                txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }
            PurApplyNo.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            PurApplyNo.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_APPLY;


       
            
         
       
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
    private void BindFlowApply()
    {
        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_APPLY;
    }



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
                    newRow["CodeName"] = row["CodeName"].ToString();
                    newRow["ApplyCount"] = drow["ApplyCount"].ToString();
                    newRow["ApplyDate"] = Convert.ToDateTime(drow["ApplyDate"].ToString()).ToString("yyyy-MM-dd");
                    newRow["UnitID"] = row["UnitID"].ToString();
                    dtNew.Rows.Add(newRow);
                }
            }
        }

        /*将数据添加到页面*/
        int TotalCount = 0;
        divDetailTbl1.InnerHtml = CreateHtml(dtNew, ref TotalCount);
        txtCountTotal.Value = TotalCount.ToString();
        divDetailTbl2.InnerHtml = CreateHtmlForTwo(dtNew);


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
    protected string CreateHtml(DataTable dt, ref int TotalCount)
    {
        /*读取原因*/
        DataTable dtReason = PurchaseApplyDBHelper.GetApplyReason();

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.AppendLine("<table width=\"99%\" border=\"0\" id=\"DetailSTable\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" ");
        sbHtml.AppendLine("bgcolor=\"#999999\"> ");
        sbHtml.AppendLine("<tr> ");
        sbHtml.AppendLine("   <td bgcolor=\"#E6E6E6\" width=\"50\" align=\"center\"> ");
        sbHtml.AppendLine("  全选<input type=\"checkbox\" id=\"checkall\" onclick=\"fnSelectAll();\" title=\"全选\" /> ");
        sbHtml.AppendLine("  </td> ");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" align=\"center\"> ");
        sbHtml.AppendLine("       序号 ");
        sbHtml.AppendLine("   </td> ");
        sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\"> ");
        sbHtml.AppendLine("      商品编号<span class=\"redbold\">*</span> ");
        sbHtml.AppendLine("      <uc6:MaterialChoose ID=\"MaterialChoose1\" runat=\"server\" /> ");
        sbHtml.AppendLine("  </td> ");
        sbHtml.AppendLine("  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\"> ");
        sbHtml.AppendLine("       商品名称<span class=\"redbold\">*</span> ");
        sbHtml.AppendLine("   </td> ");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"> ");
        sbHtml.AppendLine("        规格<span class=\"redbold\">*</span> ");
        sbHtml.AppendLine("    </td> ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\"> ");
        sbHtml.AppendLine("        单位<span class=\"redbold\">*</span> ");
        sbHtml.AppendLine("    </td> ");
        sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"> ");
        sbHtml.AppendLine("      需求数量<span class=\"redbold\">*</span> ");
        sbHtml.AppendLine("    </td> ");
        sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"> ");
        sbHtml.AppendLine("        需求日期<span class=\"redbold\">*</span> ");
        sbHtml.AppendLine("    </td> ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" style=\"width: 7%\"> ");
        sbHtml.AppendLine("         申请原因 ");
        sbHtml.AppendLine("     </td> ");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"> ");
        sbHtml.AppendLine("          源单编号 ");
        sbHtml.AppendLine("     </td> ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" > ");
        sbHtml.AppendLine("       源单序号 ");
        sbHtml.AppendLine("     </td> ");
        sbHtml.AppendLine("      </tr> ");
        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            TotalCount += Convert.ToInt32(row["ApplyCount"].ToString());
            sbHtml.AppendLine("<tr id=\"SignItem" + i.ToString() + "\">");
            sbHtml.AppendLine("<td style=\"display:none;\"><input  id='DtlSFromBillID" + i.ToString() + "' type='text' class=\"tdinput\"  style='width:90%;' /><input  id='DtlSFromLineNo" + i.ToString() + "' type='text' class=\"tdinput\"  style='width:90%;'  disabled/><input id='DtlSRemark" + i.ToString() + "' SpecialWorkCheck='明细来源备注' type='text' class=\"tdinput\" style='width:90%;'   /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input name='chk' id='chk" + i.ToString() + "' onclick=\"IfSelectAll('chk','checkall')\"  value=" + i.ToString() + " type='checkbox' size='20'  /></td><input  id='DtlSProdID" + i.ToString() + "'  type='hidden' class=\"tdinput\" style='width:90%;'  value=\"" + row["ID"].ToString() + "\"/>");
            sbHtml.AppendLine("<td class=\"cell\"  id=\"DtlSSortNo" + i.ToString() + "\">" + i.ToString() + "</td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  id='DtlSProdNo" + i.ToString() + "'readonly  onclick=\"fnGetProduct(" + i.ToString() + ");\" type='text' class=\"tdinput\" style='width:90%;'  value=\"" + row["ProdNo"].ToString() + "\"/></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlSProdName" + i.ToString() + "'onclick=\"fnGetProduct(" + i.ToString() + ");\" value=\"" + row["ProductName"].ToString() + "\"  type='text' class=\"tdinput\"  style='width:90%;' disabled /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlSSpecification" + i.ToString() + "' type='text' class=\"tdinput\"  value=\"" + row["Specification"].ToString() + "\" style='width:90%;' disabled /><input   id='DtlSUnitID" + i.ToString() + "' type='hidden' style='width:90%;' value=\"" + row["UnitID"].ToString() + "\" /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlSUnitName" + i.ToString() + "' type='text' class=\"tdinput\" style='width:90%;' disabled  value=\"" + row["CodeName"].ToString() + "\" /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  id='DtlSPlanCount" + i.ToString() + "'onblur=\"Number_round(this,2);fnCalculateTotal();\"  type='text' class=\"tdinput\" style='width:90%;'  value=\"" + row["ApplyCount"].ToString() + "\" /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlSPlanTakeDate" + i.ToString() + "' onclick=\"WdatePicker()\" onchange=\"fnCalculateTotal();\"  type='text' class=\"tdinput\"  style='width:90%;' value=\"" + row["ApplyDate"].ToString() + "\" /><input  id='DtlSFromBillID" + i.ToString() + "' type='hidden' class=\"tdinput\"  style='width:90%;' /></td>");
            sbHtml.AppendLine("<td class=\"cell\">" + CreateSelectForReason(dtReason, i.ToString()) + "</td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  id='DtlSFromBillNO" + i.ToString() + "' type='text' class=\"tdinput\" style='width:90%;' disabled /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  id='DtlSFromLineNo" + i.ToString() + "' type='text' class=\"tdinput\"  style='width:90%;'  disabled/></td>");
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
        sbHtml.AppendLine("  <table width=\"99%\" border=\"0\" id=\"DetailTable\" style=\"behavior: url(../../../css/draggrid.htc)\"");
        sbHtml.AppendLine("      align=\"center\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#999999\">");
        sbHtml.AppendLine("      <tr>");
        sbHtml.AppendLine("   <td bgcolor=\"#E6E6E6\" class=\"Blue\" width=\"50\" align=\"center\">");
        sbHtml.AppendLine("      <input type=\"checkbox\" id=\"Checkbox1\" onclick=\"SelectAll()\" title=\"全选\" style=\"cursor: hand\" />");
        sbHtml.AppendLine("  </td>");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" align=\"center\">");
        sbHtml.AppendLine("    序号");
        sbHtml.AppendLine("  </td>");
        sbHtml.AppendLine("  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
        sbHtml.AppendLine("     商品编号");
        sbHtml.AppendLine("    </td>");
        sbHtml.AppendLine("  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
        sbHtml.AppendLine("      商品名称");
        sbHtml.AppendLine("  </td>");
        sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\">");
        sbHtml.AppendLine("       规格");
        sbHtml.AppendLine("    </td>");
        sbHtml.AppendLine("  <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\">");
        sbHtml.AppendLine("      单位");
        sbHtml.AppendLine("   </td>");
        sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\">");
        sbHtml.AppendLine("       申请数量");
        sbHtml.AppendLine("   </td>");
        sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\">");
        sbHtml.AppendLine("     需求日期");
        sbHtml.AppendLine("  </td>");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" width=\"6%\" id=\"PlanedCount\"");
        sbHtml.AppendLine("        style=\"display: none\">");
        sbHtml.AppendLine("        已计划数量");
        sbHtml.AppendLine("      </td>");
        sbHtml.AppendLine("   </tr>");

        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            string rowID = i.ToString();
            sbHtml.AppendLine("<tr>");
            sbHtml.AppendLine("<td class=\"tdColInputCenter\"><input name='Dtlchk' id='Dtlchk" + rowID + "'  onclick=\"IfSelectAll('Dtlchk','checkall')\"  value=" + rowID + " type='checkbox' size='20'  /></td>");
            sbHtml.AppendLine("<td class=\"cell\" id=\"DtlSortNo" + rowID + "\">" + rowID + "</td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  id='DtlProdNo" + rowID + "'readonly  type='text' class=\"tdinput\" style='width:90%;' disabled value=\"" + row["ProdNo"].ToString() + "\"/><input  id='DtlProdID" + rowID + "' type='hidden' class=\"tdinput\" style='width:90%;'disabled   value=\"" + row["ID"].ToString() + "\" /></td>");

            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlProdName" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;'  disabled value=\"" + row["ProductName"].ToString() + "\"/></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlSpecification" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' disabled  value=\"" + row["Specification"].ToString() + "\"/><input   id='DtlUnitID" + rowID + "' type='hidden' style='width:90%;'disabled value=\"" + row["UnitID"].ToString() + "\"/></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlUnitName" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'   disabled value=\"" + row["CodeName"].ToString() + "\"/></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  id='DtlPlanCount" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'   SpecialWorkCheck='申请数量' disabled  value=\"" + row["ApplyCount"].ToString() + "\"  /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input id='DtlRequireDate" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' readonly disabled value=\"" + row["ApplyDate"].ToString() + "\"/><input id='DtlPlanedCount" + rowID + "' SpecialWorkCheck='明细备注' type='hidden' class=\"tdinput\" style='width:90%;' disabled=\"disabled\"   /><input id='DtlRemark" + rowID + "' type='hidden' class=\"tdinput\" style='width:90%;'   /></td>");

            sbHtml.AppendLine("</tr>");
            i++;
        }
        sbHtml.AppendLine("    </table>");

        return sbHtml.ToString();
    }

    /*构造原因下拉控件*/
    protected string CreateSelectForReason(DataTable dt, string RowID)
    {
        StringBuilder sbSelect = new StringBuilder();
        sbSelect.AppendLine("<select ID=\"DtlSApplyReason" + RowID + "\">");
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





