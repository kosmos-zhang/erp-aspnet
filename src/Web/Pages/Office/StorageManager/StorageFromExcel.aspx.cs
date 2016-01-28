using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using XBase.Business.Office.StorageManager;
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using System.IO;
public partial class Pages_Office_StorageManager_StorageFromExcel : BasePage
{
    UserInfoUtil userinfo = new UserInfoUtil();
    public static DataSet ds = new DataSet();
    public string errorstr = string.Empty; //错误串
    protected void Page_Load(object sender, EventArgs e)
    {
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
    }

    /// <summary>
    /// 状态初始化
    /// </summary>
    protected void initState()
    {
        lbl_upresult.Text = string.Empty; errorstr = string.Empty;
        setup1.Enabled = false; setup2.Enabled = false; setup3.Enabled = false; btn_input.Enabled = false; setup4.Enabled = false;
        lbl_validateend.Visible = false; tab_end.Visible = false; tr_result.Visible = false;

        if (Session["newfile"] != null)
        {
            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        }
    }

    protected void btn_excel_Click(object sender, EventArgs e)
    {
        if (this.upExcelFile.PostedFile.FileName.Length > 0)
        {
            this.initState();
        }
        else
        {
            this.lbl_upresult.Text = "请选择要上传的Excel文件!";
            return;
        }
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        ////如果该公司或分店已经存在月结数据，不能再使用导入功能
        //if (!StorageInitailBus.ISADD(userinfo.CompanyCD))
        //{
        //    lbl_upresult.Text = "存在月结数据,不允许导入,操作失败";
        //    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_upresult.Text);
        //    return;
        //}

        string filename = upExcelFile.PostedFile.FileName;
        string subfile = filename.Substring(filename.LastIndexOf(".") + 1);
        if (subfile.ToUpper() != "XLS" && subfile.ToUpper() != "XLSX")
        {
            this.lbl_upresult.Text = "导入文件格式错误,必须为XLS,XLSX格式!";
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_upresult.Text);
        }
        else
        {
            try
            {
                //获取企业上传文件路径 
                string upfilepath = ProductInfoBus.GetCompanyUpFilePath(userinfo.CompanyCD);//得到格式如:"D:\zhou"
                //获取企业并构造企业上传文件名称
                Session["newfile"] = DateTime.Now.ToString("yyyyMMddhhmmss") + filename.Substring(filename.LastIndexOf("\\") + 1);
                //Session["newfile"] = "Book1.xlsx";
                upExcelFile.PostedFile.SaveAs(upfilepath + @"\" + Session["newfile"].ToString());
                this.lbl_upresult.Text = "Excel文件上传成功!";
                this.setup1.Enabled = true;
                //将excel中的数据读取到ds中

                try
                {
                    ds = StorageInfoBus.ReadEexcel(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        initState();
                        this.lbl_upresult.Text = "您导入的Excel表没有数据!";
                    }
                }
                catch (Exception ex)
                {
                    initState();
                    this.lbl_upresult.Text = "数据读取失败!错误原因:" + ex.Message;
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_upresult.Text);
                }
            }
            catch (Exception ex)
            {
                this.setup1.Enabled = false;
                this.lbl_upresult.Text = "数据读取失败,原因：" + ex.Message;
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_upresult.Text);
            }
        }

    }

    protected void setup1_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        //总店情况
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //判断仓库是否存在
            bool flag = ProductInfoBus.ChargeStorage(ds.Tables[0].Rows[i]["仓库"].ToString().Trim(), userinfo.CompanyCD);
            if (!flag)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的仓库不存在,导入操作失败!<br>";
            }
        }

        total += j;
        if (j > 0)
        {
            errorstr = "<strong>是否存在校验错误列表(仓库)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            bool flag = StorageInfoBus.ChargeProductInfo(ds.Tables[0].Rows[i]["物品"].ToString().Trim(), userinfo.CompanyCD, ds.Tables[0].Rows[i]["物品编号"].ToString());
            if (!flag)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品不存在,导入操作失败!<br>";
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>是否存在校验错误列表(物品)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        if ((total += j) > 0)
        {
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
            this.tr_result.Visible = true;
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup1.Enabled = false;
            this.setup2.Enabled = true;
        }
    }

    protected void setup2_Click(object sender, EventArgs e)
    {
        //非空判断
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号

        int k = 0; //定义批次错误序列号
        string batchNoError = string.Empty;

        int m = 0;//定义单价错误序列号
        string priceError = string.Empty;//定义单价错误记录
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["现有存量"].ToString().Trim().Length < 1)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的现有存量不能为空值,导入操作失败!<br>";
            }

            //判断是是否启用批次
            if (XBase.Business.Office.StorageManager.StorageBus.IsBatchByProductNameAndNo(ds.Tables[0].Rows[i]["物品编号"].ToString(), ds.Tables[0].Rows[i]["物品"].ToString(), UserInfo.CompanyCD))
            {
                if (ds.Tables[0].Rows[i]["批次"].ToString().Trim().Length < 1)
                {
                    batchNoError += (++k).ToString() + "： " + " 物品（" + ds.Tables[0].Rows[i]["物品"].ToString() + "）开启批次，第" + (i + 2).ToString() + "行中的批次不能为空值,导入操作失败!<br>";
                }
            }

            if (ds.Tables[0].Rows[i]["单价"].ToString().Trim().Length < 1)
            {
                priceError += (++m).ToString() + "： " + "第" + (i + 2).ToString() + "行中的单价不能为空值,导入操作失败!<br>";
            }

        }

        if (j > 0 || k > 0 || m > 0)
        {
            errorstr = "<strong>Excel空值校验错误列表</strong><br>";
            if (j > 0)
                errorstr += suberrorstr;
            if (k > 0)
                errorstr += batchNoError;
            if (m > 0)
                errorstr += priceError;

            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup2.Enabled = false;
            this.setup3.Enabled = true;
        }
    }

    protected void setup3_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int m = 0;//定义单价格式错误序号
        string priceError = string.Empty;//定义单价格式错记录
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            try
            {
                Single.Parse(ds.Tables[0].Rows[i]["现有存量"].ToString().Trim());
            }
            catch
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 现有存量 格式应该为整数,导入操作失败!<br>";
            }

            try
            {
                decimal tmpPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["单价"].ToString().Trim());
            }
            catch
            {
                priceError += (++m).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 单价 格式错误,导入操作失败!<br>";
            }
        }
        if (j > 0 || m > 0)
        {
            errorstr = "<strong>数据格式校验错误列表</strong><br>";
            if (j > 0)
                errorstr += suberrorstr;
            if (m > 0)
                errorstr += priceError;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup3.Enabled = false;
            this.setup4.Enabled = true;
            //lbl_validateend.Visible = true;
            //this.btn_input.Enabled = true;
        }
    }

    protected void setup4_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        //判断记录是否重复

        string firststr = string.Empty;
        string nextstr = string.Empty;
        #region//总店（判断：批次+仓库+商品+单价）
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            firststr = ds.Tables[0].Rows[i]["批次"].ToString().Trim() + ds.Tables[0].Rows[i]["仓库"].ToString().Trim() + ds.Tables[0].Rows[i]["物品编号"].ToString().Trim() + ds.Tables[0].Rows[i]["单价"].ToString().Trim();
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                nextstr = ds.Tables[0].Rows[k]["批次"].ToString().Trim() + ds.Tables[0].Rows[k]["仓库"].ToString().Trim() + ds.Tables[0].Rows[k]["物品编号"].ToString().Trim() + ds.Tables[0].Rows[i]["单价"].ToString().Trim();
                if (firststr == nextstr)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行与" + "第" + (k + 2).ToString() + "行重复,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            errorstr = "<strong>记录重复校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup4.Enabled = false;
            lbl_validateend.Visible = true;
            this.btn_input.Enabled = true;
        }
        #endregion

    }

    protected void btn_input_Click(object sender, EventArgs e)
    {
        try
        {
            userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StorageInfoBus.GetExcelToStorageInfo(userinfo.CompanyCD, userinfo.UserID, userinfo.IsBatch ? "1" : "0", userinfo.EmployeeID);
            this.lbl_jg.Text = "Excel数据导入成功";
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "成功导入" + ds.Tables[0].Rows.Count.ToString() + "条数据");
            this.tab_end.Visible = true;
            btn_input.Enabled = false;
        }
        catch (Exception ex)
        {
            btn_input.Enabled = true;
            this.tab_end.Visible = true;
            this.lbl_jg.Text = ex.Message;
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_jg.Text);
        }
    }

}
