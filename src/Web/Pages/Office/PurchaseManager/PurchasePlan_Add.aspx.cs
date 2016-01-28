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

public partial class Pages_Office_PurchaseManager_PurchasePlan_Add : BasePage
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
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";

        if (!IsPostBack)
        {
            if (HiddenAction.Value== "Add")
            {
                #region 通过session赋值
                UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
              
                txtCreatorName.Value = userInfo.EmployeeName;
                txtCreatorID.Value = userInfo.EmployeeID.ToString();
                txtBillStatusName.Value = "制单";
                txtBillStatusID.Value = "1";
                txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifiedUserID.Value = userInfo.UserID;
                txtModifiedUserName.Value = userInfo.UserID;
                #endregion
            }
            else if (HiddenAction.Value == "Update")
            {

            }

            #region 单据规则
            PurPlanNo.CodingType = ConstUtil.CODING_RULE_PURCHASE;
            PurPlanNo.ItemTypeID = ConstUtil.CODING_RULE_PURCHASE_PLAN;
            //PurPlanNo.TableName = "PurchasePlan";
            //PurPlanNo.ColumnName = "PlanNo";
            #endregion

            #region 采购类别
            PurchaseType.TypeCode = "5";
            PurchaseType.TypeFlag = "7";
            PurchaseType.IsInsertSelect = true;
            #endregion
            BindApplyReason();
            BindFlowApply();
        }
    }
    private void BindApplyReason()
    {
        DataTable dt = PurchaseApplyDBHelper.GetApplyReason();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlApplyReasonHidden.DataSource = dt;
            ddlApplyReasonHidden.DataTextField = "CodeName";
            ddlApplyReasonHidden.DataValueField = "ID";
            ddlApplyReasonHidden.DataBind();
        }
        ddlApplyReasonHidden.Items.Insert(0, new ListItem("--请选择--",""));
    }
    #region 绑定审批流程
    void BindFlowApply()
    {
        FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_PURCHASE;
        FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_PURCHASE_PLAN;
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
                MessageBox(ex.ToString(), "msg9");
                return;
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
            MessageBox(ex.ToString(), "msg10");
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
                    //ErrorMsg += "第" + (i + 2).ToString() + "行与第" + (j + 2).ToString() + "行商品名称相同：" + fRow["ProductName"].ToString()+"\\n";

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
                ErrorMsg += "第" + (i + 2).ToString() + "行计划数量格式错误，不是数值类型\\n";
            }
            /*验证需求日期格式*/
            if (!IsDate.IsMatch(row["ApplyDate"].ToString()))
            {
                ErrorMsg += "第" + (i + 2).ToString() + "行计划交货日期格式错误，不是有效的日期类型。例：" + DateTime.Now.ToString("yyyy-MM-dd") + "\\n";
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
        dtNew.Columns.Add(new DataColumn("ApplyPrice", typeof(string)));
        dtNew.Columns.Add(new DataColumn("TotalPrice", typeof(string)));

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
                    newRow["ApplyCount"] = drow["ApplyCount"].ToString();
                    newRow["ApplyDate"] = Convert.ToDateTime(drow["ApplyDate"].ToString()).ToString("yyyy-MM-dd");
                    newRow["UnitID"] = row["UnitID"].ToString();
                    newRow["ApplyPrice"] = row["StandardBuy"].ToString();
                    decimal  StandardBuy = row["StandardBuy"] == null ? 0 :Convert .ToDecimal ( row["StandardBuy"].ToString());

                    decimal ApplyCount = drow["ApplyCount"] == null ? 0 : Convert.ToDecimal(drow["ApplyCount"].ToString());
                    newRow["TotalPrice"] = (StandardBuy * ApplyCount).ToString("0.00");

                    dtNew.Rows.Add(newRow);
                }
            }
        }

        /*将数据添加到页面*/
        decimal TotalCount = decimal.Zero;
        decimal TotalPrice = decimal.Zero;
        divDetailTbl1.InnerHtml = CreateHtml(dtNew, ref TotalCount, ref TotalPrice);
        txtPlanCnt.Value = TotalCount.ToString("0.00");
        txtPlanMoney.Value = TotalPrice.ToString("0.00");
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
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
                adapter.Fill(ds, SheetName);
                conn.Close();
            }
        }
        catch
        {
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
            catch (Exception ex)
            {
                MessageBox(ex.ToString(), ",msg12");
                return;
            }
        }
    }

    /*构造HTML*/
    protected string CreateHtml(DataTable dt, ref decimal TotalCount, ref decimal TotalPrice)
    {

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.AppendLine("   <table width=\"99%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#999999\"  ");
        sbHtml.AppendLine("  id=\"Tb_DtlS\">  ");
        sbHtml.AppendLine("   <tr>  ");
        sbHtml.AppendLine("      <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("         全选<input type=\"checkbox\" visible=\"false\" name=\"CheckAllDtlS\" id=\"CheckAllDtlS\" onclick=\"SelectAllDtlS()\"  ");
        sbHtml.AppendLine("              value=\"checkbox\" />  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\" align=\"center\">  ");
        sbHtml.AppendLine("          序号  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("      <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("           商品编号<span class=\"redbold\">*</span>  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("      <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("         商品名称  ");
        sbHtml.AppendLine("     </td>  ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("         规格  ");
        sbHtml.AppendLine("      </td>  ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("         颜色  ");
        sbHtml.AppendLine("      </td>  ");
        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\" style=\"width:3%;\" >  ");
        sbHtml.AppendLine("       <span id=\"spUnitID\">单位</span>    ");
        sbHtml.AppendLine("     </td>  "); 
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\" style=\"width:3%; display:none \" id=\"spUsedUnitID\">单位   </td> ");
     


        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\" id=\"spUnitPrice\" >  ");
        sbHtml.AppendLine("        含税价<span class=\"redbold\">*</span>  ");
        sbHtml.AppendLine("    </td>  ");

   
            sbHtml.AppendLine("   <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\" style=\"width:7%; display:none \" id=\"spUsedPrice\" >    含税价<span class=\"redbold\">*</span>   </td> ");
    

        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" style=\"width:7%\">  ");
        sbHtml.AppendLine("     <span id=\"SpProductCount\">计划数量</span><span class=\"redbold\" id=\"spCount\">*</span>  ");
        sbHtml.AppendLine("      </td>  ");

        sbHtml.AppendLine(" <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"  valign=\"middle\" style=\"width:7%; display:none \" id=\"spUsedUnitCount\">  计划数量 <span class=\"redbold\">*</span></td> ");


        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("        计划金额  ");
        sbHtml.AppendLine("    </td>  ");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("        计划交货日期<span class=\"redbold\">*</span>  ");
        sbHtml.AppendLine("    </td>  ");
        sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("        源单编号  ");
        sbHtml.AppendLine("     </td>  ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\" style =\"width:4%\">  ");
        sbHtml.AppendLine("         源单序号  ");
        sbHtml.AppendLine("        </td>  ");
        sbHtml.AppendLine("        <td align=\"center\" style=\"width:12%\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("          供应商<span class=\"redbold\">*</span>  ");
        sbHtml.AppendLine("      </td>  ");
        sbHtml.AppendLine("   </tr>  ");

        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            TotalCount += Convert.ToDecimal(row["ApplyCount"].ToString());
            TotalPrice += Convert.ToDecimal(row["TotalPrice"].ToString());
            string rowID = i.ToString();
            sbHtml.AppendLine("<tr id=\"DtlSSignItem" + rowID + "\">");
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSchk' id='DtlSchk" + rowID + "' value=" + rowID + " onclick=\"IfSelectAll('DtlSchk','CheckAllDtlS')\"  type='checkbox' size='20'  /><input name='DtlSProductID" + rowID + "'  id='DtlSProductID" + rowID + "'  type='hidden' class=\"tdinput\" style='width:90%'  value=\"" + row["ID"].ToString() + "\"/></td>");
            sbHtml.AppendLine("<td class=\"cell\" id=\"DtlSSortNo" + rowID + "\">" + rowID + "</td>");
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSProductNo" + rowID + "'  id='DtlSProductNo" + rowID + "' readonly onclick=\"popTechObj.ShowList(" + rowID + ");\" type='text' class=\"tdinput\" style='width:90%'  value=\"" + row["ProdNo"].ToString() + "\" /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSProductName" + rowID + "' id='DtlSProductName" + rowID + "' type='text' class=\"tdinput\"  style='width:90%'  disabled value=\"" + row["ProductName"].ToString() + "\" /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  name='DtlSSpecification'" + rowID + " id='DtlSSpecification" + rowID + "' type='text' class=\"tdinput\"  style='width:80%;' disabled   value=\"" + row["Specification"].ToString() + "\"/></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input  name='DtlSColor'" + rowID + " id='DtlSColor" + rowID + "' type='text' class=\"tdinput\"  style='width:80%;' disabled   value=\"" + row["ColorName"].ToString() + "\"/></td>");

            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                string unitHtml = "";
                decimal tmpExRate = 1;/*换算率*/

                /*计量单位HTML*/
                unitHtml = XBase.Business.Common.UnitGroup.GetUnitGroupByProductId(row["ID"].ToString(), "InUnit", "SignItem_TD_UnitID_Select" + rowID, "ChangeUnit(this," + rowID + "," + row["ApplyPrice"].ToString() + ")", "", ref tmpExRate);

                sbHtml.AppendLine("<td class=\"cell\"><input  name='DtlSUnitID'" + rowID + " id='DtlSUnitID" + rowID + "' type='hidden' style='width:80%;' value=\"" + row["UnitID"].ToString() + "\"/><input name='DtlSUnitName'" + rowID + " id='DtlSUnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:80%'  disabled  value=\"" + row["CodeName"].ToString() + "\"/></td>");
                sbHtml.AppendLine("<td  class=\"cell\"><div id=\"unitdiv" + rowID + "\">" + unitHtml + "</div></td>");

                sbHtml.AppendLine("<td class=\"cell\" style='display:none'><input name='DtlSUnitPrice'" + rowID + " id='DtlSUnitPrice" + rowID + "'type='text' class=\"tdinput\"  onblur=\"Number_round(this," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + ");fnMergeDetail();\" style='width:90%'   value=\"" + row["ApplyPrice"].ToString() + "\" /> </td>");



                decimal UsedPrice = (decimal.Parse(row["ApplyPrice"] == null ? "0" : row["ApplyPrice"].ToString()) * tmpExRate);
                string used = UsedPrice.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                decimal ApplyCount = decimal.Parse(row["ApplyCount"] == null ? "0" : row["ApplyCount"].ToString());

                decimal app = ApplyCount * decimal.Parse(row["ApplyPrice"] == null ? "0" : row["ApplyPrice"].ToString()) / tmpExRate;

                string TotalPriceS = app.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

                decimal usedCount = 0;
                if (tmpExRate != 0)
                {
                    usedCount = ApplyCount * tmpExRate;
                }




                string us = usedCount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

                sbHtml.AppendLine("<td class=\"cell\"><input id='UsedPrice" + rowID + "'type='text' class=\"tdinput\" onblur=\"fnMergeDetail();\"  value=" + used + "  style='width:80%;'   /></td>");
                sbHtml.AppendLine("<td class=\"cell\" style=\"display:none\"><input id='UsedPricHid" + rowID + "'type='text' class=\"tdinput\"   value=" + used + "  style='width:80%;'   /></td>");
                sbHtml.AppendLine("<td class=\"cell\" style=\"display:none\"> <input name='DtlSRequireCount" + rowID + "' id='DtlSRequireCount" + rowID + "'  type='hidden' class=\"tdinput\" style='width:90%' disabled  /></td>");


                sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSPlanCount" + rowID + "' id='DtlSPlanCount" + rowID + "'   onblur=\"Number_round(this," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + ");fnMergeDetail();\" type='text' class=\"tdinput\" style='width:90%'  value=\"" +  us+ "\"/></td>");

                sbHtml.AppendLine("<td class=\"cell\"><input id='UsedUnitCount" + rowID + "' type='text' class=\"tdinput\"  value='" + ApplyCount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint) + "'  style='width:90%;' onblur=\" Number_round(this," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "); fnMergeDetail();\"  /></td>");



            

              

          
                sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSTotalPrice" + rowID + "' id='DtlSTotalPrice" + rowID + "'  type='text' class=\"tdinput\"  style='width:90%' disabled value=\"" + TotalPriceS + "\" /><input name='DtlSRequireDate" + rowID + "' id='DtlSRequireDate" + rowID + "' type='hidden' class=\"tdinput\" style='width:90%' disabled  /></td>");
            }
            else
            {
                sbHtml.AppendLine("<td class=\"cell\"><input  name='DtlSUnitID'" + rowID + " id='DtlSUnitID" + rowID + "' type='hidden' style='width:80%;' value=\"" + row["UnitID"].ToString() + "\"/><input name='DtlSUnitName'" + rowID + " id='DtlSUnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:80%'  disabled  value=\"" + row["CodeName"].ToString() + "\"/></td>");

                decimal s = Convert.ToDecimal(row["ApplyPrice"]==null ?"0": row["ApplyPrice"].ToString());
                string sd = s.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

                sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSUnitPrice'" + rowID + " id='DtlSUnitPrice" + rowID + "'type='text' class=\"tdinput\"  onblur=\"Number_round(this," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + ");fnMergeDetail();\" style='width:90%'   value=\"" + sd+ "\" /><input name='DtlSRequireCount" + rowID + "' id='DtlSRequireCount" + rowID + "'  type='hidden' class=\"tdinput\" style='width:90%' disabled  /></td>");

                decimal sdsd = Convert.ToDecimal(row["ApplyCount"] == null ? "0" : row["ApplyCount"].ToString());
                string sdsdsd = sdsd.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

                sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSPlanCount" + rowID + "' id='DtlSPlanCount" + rowID + "'   onblur=\"Number_round(this," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + ");fnMergeDetail();\" type='text' class=\"tdinput\" style='width:90%'  value=\"" + sdsdsd + "\"/></td>");

                decimal TotalPrice1 = Convert.ToDecimal(row["TotalPrice"] == null ? "0" : row["TotalPrice"].ToString());
                string TotalPrice2 = TotalPrice1.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

                sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSTotalPrice" + rowID + "' id='DtlSTotalPrice" + rowID + "'  type='text' class=\"tdinput\"  style='width:90%' disabled value=\"" + TotalPrice2 + "\" /><input name='DtlSRequireDate" + rowID + "' id='DtlSRequireDate" + rowID + "' type='hidden' class=\"tdinput\" style='width:90%' disabled  /></td>");
              
            }


          

      
          
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSPlanTakeDate" + rowID + "' id='DtlSPlanTakeDate" + rowID + "'   readonly=\"readonly\" onclick=\"WdatePicker();\"  onPropertyChange=\"fnMergeDetail();\" type='text' class=\"tdinput\"  style='width:90%'  value=\"" + row["ApplyDate"].ToString() + "\" /><span style=\"display:none\"><select class='tdinput' id='DtlSApplyReason" + rowID + "' disabled ></select><input name='DtlSFromBillID" + rowID + "' id='DtlSFromBillID" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled /><input name='DtlSProviderID" + rowID + "' id='DtlSProviderID" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' /><input name='DtlSRemark" + rowID + "' id='DtlSRemark" + rowID + "' type='text' class=\"tdinput\" style='width:90%'   /><input name='RqrID" + rowID + "' id='RqrID" + rowID + "' type='text'/></span></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSFromBillNo" + rowID + "' id='DtlSFromBillNo" + rowID + "' type='text' class=\"tdinput\" style='width:90%' disabled /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSFromLineNo" + rowID + "' id='DtlSFromLineNo" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled /></td>");
            sbHtml.AppendLine("<td class=\"cell\"><input name='DtlSProviderName" + rowID + "' id='DtlSProviderName" + rowID + "' onclick=\"popProviderObj.ShowProviderList('DtlSProviderID" + rowID + "','DtlSProviderName" + rowID + "','','Plan');\"   type='text' class=\"tdinput\"  style='width:90%' readonly /></td>");
            sbHtml.AppendLine("</tr>");
            i++;
        }

        sbHtml.AppendLine("  </table>  ");

        return sbHtml.ToString();

    }

    /*构造第二个表格*/
    protected string CreateHtmlForTwo(DataTable dt)
    {
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.AppendLine("    ");
        sbHtml.AppendLine("  <table width=\"99%\" border=\"0\" id=\"Tb_Dtl\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\"  ");
        sbHtml.AppendLine("      bgcolor=\"#999999\">  ");
        sbHtml.AppendLine("      <tr>  ");
        sbHtml.AppendLine("          <td height=\"20\" align=\"center\" bgcolor=\"#E6E6E6\" style=\"width: 30px\">  ");
        sbHtml.AppendLine("              序号  ");
        sbHtml.AppendLine("         </td>  ");
        sbHtml.AppendLine("         <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("             商品编号  ");
        sbHtml.AppendLine("         </td>  ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("          商品名称  ");
        sbHtml.AppendLine("      </td>  ");
        sbHtml.AppendLine("      <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("           规格  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("      <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("           颜色  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\" style=\"width:7%;\"  >  ");
        sbHtml.AppendLine("          <span id=\"spUnitID2\">单位</span>   ");
        sbHtml.AppendLine("      </td>  ");

           sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\" style=\"width:3%; display:none \" id=\"spUsedUnitID2\">   单位      </td> ");

        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("           供应商  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\" style=\"width:7%\">  ");
        sbHtml.AppendLine("             <span id=\"SpProductCount2\">计划数量</span>   ");
        sbHtml.AppendLine("        </td>  ");

             sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\"  valign=\"middle\" style=\"width:7%; display:none \" id=\"spUsedUnitCount2\">      计划数量 <span class=\"redbold\">*</span>  </td> ");


        sbHtml.AppendLine("        <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("          计划交货日期  ");
        sbHtml.AppendLine("      </td>  ");
        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\" id=\"spUnitPrice2\" >  ");
        sbHtml.AppendLine("           计划采购单价  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("     <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("          计划采购金额  ");
        sbHtml.AppendLine("       </td>  ");

             sbHtml.AppendLine("    <td align=\"center\" bgcolor=\"#E6E6E6\" class=\"ListTitle\" valign=\"middle\" style=\"width:7%; display:none \" id=\"spUsedPrice2\" >       计划采购单价  </td>  ");

        sbHtml.AppendLine("       <td align=\"center\" bgcolor=\"#E6E6E6\">  ");
        sbHtml.AppendLine("           已订购数量  ");
        sbHtml.AppendLine("       </td>  ");
        sbHtml.AppendLine("     </tr>  ");
        //int i = 1;
        //foreach (DataRow row in dt.Rows)
        //{
        //    string rowID = i.ToString();
        //    sbHtml.AppendLine("<tr id=\"DtlSignItem" + i.ToString() + "\">");
        //    sbHtml.AppendLine("<td class=\"cell\">" + rowID + "</td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlProductID" + rowID + "'  value=\"" + row["ID"].ToString() + "\"  id='DtlProductID" + rowID + "'  type='hidden' class=\"tdinput\" style='width:90%' disabled /> <input name='DtlProductNo" + rowID + "'  value=\"" + row["ProdNo"].ToString() + "\" id='DtlProductNo" + rowID + "' disabled  type='text' class=\"tdinput\" style='width:90%' /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlProductName" + rowID + "'  value=\"" + row["ProductName"].ToString() + "\" id='DtlProductName" + rowID + "'disabled type='text' class=\"tdinput\"  style='width:90%'  /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input  name='DtlSpecification'" + rowID + " value=\"" + row["Specification"].ToString() + "\" id='DtlSpecification" + rowID + "'disabled type='text' class=\"tdinput\"  style='width:90%;'/><input  name='DtlUnitID'" + rowID + " id='DtlUnitID" + rowID + "' value =\"" + row["UnitID"].ToString() + "\" type='hidden' style='width:90%;' disabled/></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlUnitName'" + rowID + " value=\"" + row["CodeName"].ToString() + "\"  id='DtlUnitName" + rowID + "'type='text' class=\"tdinput\" disabled style='width:90%'   /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlProviderName" + rowID + "' id='DtlProviderName" + rowID + "'type='text' class=\"tdinput\" disabled style='width:90%'   /><input name='DtlProviderID" + rowID + "' id='DtlProviderID" + rowID + "'type='hidden' class=\"tdinput\" disabled style='width:90%'   /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlProductCount" + rowID + "'  value=\"" + row["ApplyCount"].ToString() + "\" id='DtlProductCount" + rowID + "' disabled type='text' class=\"tdinput\" style='width:90%' /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlRequireDate" + rowID + "' value=\"" + row["ApplyDate"].ToString() + "\" id='DtlRequireDate" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlUnitPrice" + rowID + "'  value=\"" + row["ApplyPrice"].ToString() + "\" id='DtlUnitPrice" + rowID + "' type='text' class=\"tdinput\"  style='width:90%' disabled /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlTotalPrice" + rowID + "' value=\"" + row["TotalPrice"].ToString() + "\" id='DtlTotalPrice" + rowID + "' type='text' class=\"tdinput\"  style='width:90%'  disabled/><input name='DtlRemark" + rowID + "' id='DtlRemark" + rowID + "' type='hidden' class=\"tdinput\" style='width:90%'   /></td>");
        //    sbHtml.AppendLine("<td class=\"cell\"><input name='DtlOrderCount" + rowID + "' id='DtlOrderCount" + rowID + "'  type='text' class=\"tdinput\" style='width:90%' disabled  /></td>");
        //    sbHtml.AppendLine("</tr>");
        //    i++;
        //}

        sbHtml.AppendLine("    </table>  ");
        return sbHtml.ToString();
    }

    /*构造原因下拉控件*/
    protected string CreateSelectForReason(DataTable dt, string RowID)
    {
        StringBuilder sbSelect = new StringBuilder();
        sbSelect.AppendLine("<select ID=\"DtlSApplyReason" + RowID + "\">");
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                sbSelect.AppendLine("<option value=\"" + row["ID"].ToString() + "\">" + row["CodeName"].ToString() + "</option>");
            }

        }
        sbSelect.AppendLine("<option value=\"\">--请选择--</option>");

        return sbSelect.ToString();
    }





}
