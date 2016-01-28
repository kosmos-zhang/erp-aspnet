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

using XBase.Business.Office.SupplyChain;
using XBase.Common;
using System.IO;
public partial class Pages_Office_SupplyChain_ProductInfoInsertList : BasePage
{
    UserInfoUtil userinfo = new UserInfoUtil();
    public static DataSet ds = new DataSet();
    public string errorstr = string.Empty; //错误串

    protected void Page_Load(object sender, EventArgs e)
    {
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
    }
    protected void initvalidate()
    {
        this.lbl_result.Text = string.Empty; errorstr = string.Empty;
        setup1.Enabled = false; setup2.Enabled = false; setup3.Enabled = false; setup4.Enabled = false; setup5.Enabled = false; setup6.Enabled = false; this.btn_input.Enabled = false;
        tr_result.Visible = false; tab_end.Visible = false; lbl_end.Visible = false;

        if (Session["newfile"] != null)
        {
            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        }
    }

    /// <summary>
    /// 上传excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_excel_Click(object sender, EventArgs e)
    {
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
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
        }
        else
        {
            try
            {
                //获取企业上传文件路径 
                string upfilepath = ProductInfoBus.GetCompanyUpFilePath(userinfo.CompanyCD);//得到格式如:"D:\zhou"
                //获取企业并构造企业上传文件名称
                Session["newfile"] = DateTime.Now.ToString("yyyyMMddhhmmss") + filename.Substring(filename.LastIndexOf("\\") + 1);
                //Session["newfile"] = "product1111.xls";
                upExcelFile.PostedFile.SaveAs(upfilepath + @"\" + Session["newfile"].ToString());
                this.lbl_result.Text = "Excel文件上传成功!";
                this.setup1.Enabled = true;
                //将excel中的数据读取到ds中
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "文件上传成功");
                try
                {
                    ds = ProductInfoBus.ReadEexcel(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        initvalidate();
                        this.lbl_result.Text = "您导入的Excel表没有数据!";
                    }
                }
                catch (Exception ex)
                {
                    //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "数据读取失败");
                    initvalidate();
                    this.lbl_result.Text = "数据读取失败,原因：" + ex.Message.ToString();
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                }
            }
            catch (Exception ex)
            {
                this.setup1.Enabled = false;
                this.lbl_result.Text = "数据读取失败,原因：" + ex.Message;
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
            }
        }

    }

    /// <summary>
    /// excel内部重复验证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup1_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;

        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        errorstr = string.Empty;

        #region/*excel内部重复判断*/
        //1.物品编号重复判断
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                if (ds.Tables[0].Rows[i]["物品编号"].ToString().Trim() == ds.Tables[0].Rows[k]["物品编号"].ToString().Trim())
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的物品编号与第" + (k + 2).ToString() + "行中的物品编号重复,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr = "<strong>重复值校验错误列表(物品编号)</strong><br>";
            errorstr += suberrorstr;
        }
        suberrorstr = string.Empty;

        ////2.物品名称重复判断
        //j = 0;
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
        //    {
        //        if (ds.Tables[0].Rows[i]["物品名称"].ToString().Trim() == ds.Tables[0].Rows[k]["物品名称"].ToString().Trim())
        //        {
        //            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的物品名称与第" + (k + 2).ToString() + "行中的物品名称重复,导入操作失败!<br>";
        //        }
        //    }
        //}
        //if (j > 0)
        //{
        //    total += j;
        //    errorstr += "<strong>重复值校验错误列表(物品名称)</strong><br>";
        //    errorstr += suberrorstr;
        //    suberrorstr = string.Empty;
        //}

        //3、条码重复判断
        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                if (ds.Tables[0].Rows[i]["条码"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[k]["条码"].ToString().Trim().Length > 0)
                {
                    if (ds.Tables[0].Rows[i]["条码"].ToString().Trim() == ds.Tables[0].Rows[k]["条码"].ToString().Trim())
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的条码与第" + (k + 2).ToString() + "行中的条码重复,导入操作失败!<br>";
                    }
                }
            }
        }
        if (j > 0)
        {
            total += j;
            errorstr += "<strong>重复值校验错误列表(条码)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }
        #endregion

        if ((total += j) < 1)
        {
            this.tr_result.Visible = false;
            this.setup1.Enabled = false;
            this.setup2.Enabled = true;
        }
        else
        {
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
    }

    /// <summary>
    /// 空值验证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup2_Click(object sender, EventArgs e)
    {
        //3.物品非空判断
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号

        errorstr = string.Empty;

        string[] code = { "物品编号", "物品名称", "主放仓库", "单位" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length < 1)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的  " + code[k] + "  值不能为空值,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            errorstr = "<strong>空值校验错误列表</strong><br>";
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
        int j = 0;//定义错误列表序号
        string[] code = { "物品编号", "物品名称", "名称简称", "条码", "规格型号", "颜色","产地", "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价" };
        int[] codelen = { 50, 100, 50, 50, 100, 12,100, 12, 12, 12, 12, 12, 12, 12, 12, 12 };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length > codelen[k])
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 数据长度过长,导入操作失败!<br>";
                }
            }
        }
        if (j > 0)
        {
            errorstr = "<strong>数据长度校验错误列表</strong><br>";
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
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        errorstr = string.Empty;
        string[] code = { "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价","标准成本" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                try
                {
                    if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length > 0)
                    {
                        Single num = Single.Parse(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                    }
                }
                catch
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 格式应该为数值,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            errorstr = "<strong>数据格式校验错误列表（数据类型）</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }
        total += j;
        j = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            try
            {
                string maxstr = ds.Tables[0].Rows[i]["最高库存"].ToString().Trim();
                string minstr = ds.Tables[0].Rows[i]["最低库存"].ToString().Trim();
                string safestr = ds.Tables[0].Rows[i]["安全库存"].ToString().Trim();
                decimal maxNum, minNum, safeNum;
                if (maxstr.Length > 0 && minstr.Length > 0)
                {
                    maxNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最高库存"].ToString().Trim());
                    minNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最低库存"].ToString().Trim());
                    if (maxNum <= minNum)
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的最高库存数值低于或者等于最低库存,导入操作失败!<br>";
                    }
                }
                if (maxstr.Length > 0 && minstr.Length > 0 && safestr.Length > 0)
                {
                    maxNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最高库存"].ToString().Trim());
                    minNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最低库存"].ToString().Trim());
                    safeNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["安全库存"].ToString().Trim());
                    if (safeNum < minNum || safeNum > maxNum)
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的安全库存的数据不在最高库存和最低库存之间,导入操作失败!<br>";
                    }
                }
            }
            catch { }
        }

        if (j > 0) //有错
        {
            errorstr += "<strong>数据格式校验错误列表（库存范围）</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
            return;
        }
        if (total > 0)
        {
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            return;
        }

        this.tr_result.Visible = false;
        this.setup4.Enabled = false;
        this.setup5.Enabled = true;
    }

    protected void setup5_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        errorstr = string.Empty;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            bool flag = ProductInfoBus.ChargeProduct("ProdNo", ds.Tables[0].Rows[i]["物品编号"].ToString().Trim(), userinfo.CompanyCD);
            //1、判断物品编号是否存在
            if (flag)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品编号已经存在,导入操作失败!<br>";
            }
        }
        if (j > 0)
        {
            total += j;
            errorstr = "<strong>数据存在校验(物品编号)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        //2、判断物品名称是否存在
        //j = 0;
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    bool flag = ProductInfoBus.ChargeProduct("ProductName", ds.Tables[0].Rows[i]["物品名称"].ToString().Trim(), userinfo.CompanyCD);
        //    if (flag)
        //    {
        //        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品名称已经存在,导入操作失败!<br>";
        //    }
        //}

        //if (j > 0)
        //{
        //    total = +j;
        //    errorstr += "<strong>数据存在校验(物品名称)</strong><br>";
        //    errorstr += suberrorstr;
        //    suberrorstr = string.Empty;
        //}

        if (total > 0)
        {
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
            this.setup5.Enabled = false;
            this.setup6.Enabled = true;
        }
    }

    protected void setup6_Click(object sender, EventArgs e)
    {
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //判断仓库是否存在
            bool flag = ProductInfoBus.ChargeStorage(ds.Tables[0].Rows[i]["主放仓库"].ToString().Trim(), userinfo.CompanyCD);
            if (!flag)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的仓库不存在,导入操作失败!<br>";
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr = "<strong>数据存在校验错误列表(主放仓库)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["单位"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ChargeCodeUnit(ds.Tables[0].Rows[i]["单位"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品单位不存在,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品单位)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["物品分类"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ChargeCodeType(ds.Tables[0].Rows[i]["物品分类"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品分类不存在,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品分类)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;


        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["条码"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ChargeBarCode(ds.Tables[0].Rows[i]["条码"].ToString().Trim(), userinfo.CompanyCD);
                if (flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的条码已经存在,导入操作失败!<br>";
                }
            }
        }



        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品分类)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }



        //验证颜色是否存在
        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["颜色"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ValidateProductColor(ds.Tables[0].Rows[i]["颜色"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的颜色在系统中不存在,导入操作失败!<br>";
                }
            }
        }


        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品颜色)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }



        if ((total += j) > 0)
        {
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
            this.setup5.Enabled = false;
            this.setup6.Enabled = false;
            lbl_end.Visible = true;
            this.btn_input.Enabled = true;
        }
    }

    protected void btn_input_Click(object sender, EventArgs e)
    {
        try
        {
            if (userinfo.EmployeeID.ToString().Length < 1)
            {
                this.lbl_jg.Text = "该用户没有分配为雇员,导入失败!";
            }
            else
            {
                int num = ProductInfoBus.GetExcelToProductInfo(userinfo.CompanyCD, userinfo.EmployeeID.ToString());
                this.lbl_jg.Text = "Excel数据导入成功";
                if (Session["newfile"] != null)
                {
                    ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                }
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "成功导入" + ds.Tables[0].Rows.Count.ToString()+"条数据");
            }
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
