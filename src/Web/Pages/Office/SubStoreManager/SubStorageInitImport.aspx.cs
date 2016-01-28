using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using XBase.Business.Office.SubStoreManager;
using XBase.Common;
using XBase.Business.Office.SupplyChain;
using XBase.Business.Office.LogisticsDistributionManager;

public partial class Pages_Office_SubStoreManager_SubStorageInitImport : BasePage
{
    public static DataTable dt = new DataTable();
    public string errorstr = string.Empty; //错误串

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化验证数据
    /// </summary>
    protected void initvalidate()
    {
        this.lbl_result.Text = string.Empty;
        errorstr = string.Empty;
        setup2.Enabled = false;
        setup3.Enabled = false;
        setup4.Enabled = false;
        setup6.Enabled = false;
        btn_input.Enabled = false;
        tr_result.Visible = false;
        tab_end.Visible = false;
        lbl_end.Visible = false;

        if (Session["newfile"] != null)
        {
        }
    }

    /// <summary>
    /// 上传excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        string upfilepath = "";
        if (upExcelFile.PostedFile.FileName.Length < 1)
        {
            this.lbl_result.Text = "请选择要上传的Excel文件!";
            return;
        }
        else
        {
            this.initvalidate();
        }
        string filename = upExcelFile.PostedFile.FileName;
        string subfile = filename.Substring(filename.LastIndexOf(".") + 1);
        if (subfile.ToUpper() != "XLS" && subfile.ToUpper() != "XLSX")
        {
            this.lbl_result.Text = "导入文件格式错误,必须为XLS,XLSX格式!";
        }
        else
        {
            try
            {
                //获取企业上传文件路径 
                upfilepath = ProductInfoBus.GetCompanyUpFilePath(UserInfo.CompanyCD);//得到格式如:"D:\zhou"
                //获取企业并构造企业上传文件名称
                Session["newfile"] = DateTime.Now.ToString("yyyyMMddhhmmss") + filename.Substring(filename.LastIndexOf("\\") + 1);
                upfilepath += @"\" + Session["newfile"].ToString();
                upExcelFile.PostedFile.SaveAs(upfilepath);
                this.lbl_result.Text = "Excel文件上传成功!";
                this.setup2.Enabled = true;
                //将excel中的数据读取到ds中
                try
                {
                    string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + upfilepath;
                    string sql = "SELECT distinct * FROM [Sheet1$] WHERE 流水号 IS NOT NULL";
                    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, connStr);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count < 1)
                    {
                        initvalidate();
                        this.lbl_result.Text = "您导入的Excel表没有数据!";
                    }
                }
                catch (Exception ex)
                {
                    initvalidate();
                    this.lbl_result.Text = "数据读取失败,原因：" + ex.Message.ToString();
                    ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                }
            }
            catch (Exception ex)
            {
                this.setup2.Enabled = false;
                this.lbl_result.Text = "数据读取失败,原因：" + ex.Message;
                ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
            }
            finally
            {

                if (File.Exists(upfilepath))
                {
                    File.Delete(upfilepath);
                }
            }
        }

    }


    /// <summary>
    /// 空值验证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup2_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (String.IsNullOrEmpty(dt.Rows[i]["分店名称"].ToString()))
            {
                suberrorstr += string.Format("第{0}行分店名称不能为空,导入操作失败!<br>", i + 2);
            }
            if (String.IsNullOrEmpty(dt.Rows[i]["物品编号"].ToString()))
            {
                suberrorstr += string.Format("第{0}行物品编号不能为空,导入操作失败!<br>", i + 2);
            }
        }
        if (suberrorstr.Length > 0)
        {
            errorstr = "<strong>空值校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup2.Enabled = false;
            this.setup3.Enabled = true;
        }
    }

    /// <summary>
    /// 长度判断
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup3_Click(object sender, EventArgs e)
    {
        //3.长度判断
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        string[] code = { "分店名称", "物品编号", "现有存量", "批次" };
        int[] codelen = { 50, 50, 9, 50 };
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                if (dt.Rows[i][code[k]].ToString().Trim().Length > codelen[k])
                {
                    suberrorstr += string.Format("第{0}行中的 {1} 数据长度过长,导入操作失败!<br>", i + 2, code[k]);
                }
            }
        }
        if (suberrorstr.Length > 0)
        {
            errorstr = "<strong>数据长度校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup3.Enabled = false;
            this.setup4.Enabled = true;
        }
    }

    /// <summary>
    /// 判断是否为实数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup4_Click(object sender, EventArgs e)
    {
        //4.判断是否为实数
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        decimal dec = 0m;
        string[] code = { "现有存量" };
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!decimal.TryParse(dt.Rows[i][code[0]].ToString(), out dec))
            {
                suberrorstr += string.Format("第{0}行中的 {1} 格式应该为数值,导入操作失败!<br>", i + 2, code[0]);
            }
        }
        if (suberrorstr.Length > 0)
        {
            errorstr = "<strong>数据格式校验错误列表（数据类型和范围）</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup4.Enabled = false;
            this.setup6.Enabled = true;
        }
    }

    protected void setup6_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        var temp = from d in dt.AsEnumerable()
                   group d by d.Field<object>("分店名称") into g
                   select g;

        foreach (var item in temp)
        {
            if (!SubProductSendPriceBus.ExisitDeptName(UserInfo.CompanyCD, item.Key.ToString().Trim()))
            {
                suberrorstr += string.Format(" {0} 分店不存在,导入操作失败!<br>", item.Key);
            }
        }
        temp = from d in dt.AsEnumerable()
               group d by d.Field<object>("物品编号") into g
               select g;

        foreach (var item in temp)
        {
            if (!SubProductSendPriceBus.ExisitProdNo(UserInfo.CompanyCD, item.Key.ToString().Trim()))
            {
                suberrorstr += string.Format(" {0} 物品编号不存在,导入操作失败!<br>", item.Key);
            }
        }
        if (suberrorstr.Length > 0)
        {
            errorstr = "<strong>数据存在校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup6.Enabled = false;
            lbl_end.Visible = true;
            this.btn_input.Enabled = true;
        }
    }

    protected void btn_input_Click(object sender, EventArgs e)
    {
        try
        {
            if (UserInfo.EmployeeID.ToString().Length < 1)
            {
                this.lbl_jg.Text = "该用户没有分配为雇员,导入失败!";
            }
            else
            {

                SubStorageBus.ImportData(dt, UserInfo);
                this.lbl_jg.Text = "Excel数据导入成功";
            }
            this.tab_end.Visible = true;
            btn_input.Enabled = false;
            ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), dt.Rows.Count, 1, "成功导入" + dt.Rows.Count.ToString() + "条数据");
        }
        catch (Exception ex)
        {
            btn_input.Enabled = true;
            this.tab_end.Visible = true;
            this.lbl_jg.Text = ex.Message;
            ProductInfoBus.LogInsert(UserInfo.CompanyCD, UserInfo.DeptID, UserInfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_jg.Text);
        }
    }
}
