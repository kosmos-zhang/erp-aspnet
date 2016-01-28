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
using XBase.Business.Office.CustManager;
using System.Text.RegularExpressions;

public partial class Pages_Office_CustManager_CustInfo_Export : BasePage 
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
        this.lbl_validateend.Visible = false;
        lbl_jg.Text = string.Empty;

        this.lbl_result.Text = string.Empty;
        errorstr = string.Empty;
       
        if (Session["newfile"] != null)
        {
            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        }
    }    

    #region 把excel数据读入dataset返回(服务端读取)
    /// <summary>
    /// 把excel数据读入dataset返回(服务端读取)
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private DataSet ConvertXlsToDataSet(string FilePath)
    {
        //string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;IMEX=1;HDR=YES'";
        try
        {
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + FilePath;
            System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strCon);
            string strCom = "SELECT * FROM [Sheet1$]";
            Conn.Open();
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "[Sheet1$]");
            Conn.Close();
            return ds;
        }
        catch
        {
            return null;
        }
    }
    #endregion
       
   

    #region 批量导入
    protected void btn_input_Click(object sender, EventArgs e)
    {
        try
        {
            if (userinfo.EmployeeID.ToString().Length < 1 || userinfo.UserID.Length < 1)
            {
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                this.lbl_jg.Text = "该用户没有分配为雇员,导入失败!";               
            }
            else
            {                
                DataTable dt = ds.Tables[0];
                if (CustInfoBus.InsertCustInfoRecord(dt, userinfo.CompanyCD,userinfo.EmployeeID.ToString(),userinfo.UserID))
                {
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "客户档案导入成功！");
                    this.lbl_jg.Text = "Excel数据导入成功";
                }
                if (Session["newfile"] != null)
                {
                    ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                }
            }
            this.tab_end.Visible = true;
            btn_input.Enabled = false;
        }
        catch (Exception ex)
        {
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
            btn_input.Enabled = true;
            this.tab_end.Visible = true;
            this.lbl_jg.Text = ex.Message;
        }
    }
    #endregion

    #region 点击上传按钮
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        #region 上传文件
        //上传文件
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
            //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "导入文件格式错误");
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
                upExcelFile.PostedFile.SaveAs(upfilepath + @"\" + Session["newfile"].ToString());

                #region
                try
                {
                    //ds = ProductInfoBus.ReadEexcel(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);//导入临时表
                    ds = ConvertXlsToDataSet(upfilepath + @"\" + Session["newfile"].ToString());
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                string RetVal = ds.Tables[0].Rows[0]["序号"].ToString() + ds.Tables[0].Rows[0]["客户编号"].ToString() +
                                                ds.Tables[0].Rows[0]["客户名称"].ToString() + ds.Tables[0].Rows[0]["客户简介"].ToString() +
                                                ds.Tables[0].Rows[0]["人员编号"].ToString() + ds.Tables[0].Rows[0]["分管业务员"].ToString() +
                                                ds.Tables[0].Rows[0]["区域"].ToString() +
                                                ds.Tables[0].Rows[0]["省"].ToString() + ds.Tables[0].Rows[0]["市"].ToString() +
                                                ds.Tables[0].Rows[0]["联系人"].ToString() + ds.Tables[0].Rows[0]["电话"].ToString() +
                                                ds.Tables[0].Rows[0]["手机"].ToString() + ds.Tables[0].Rows[0]["传真"].ToString() +
                                                ds.Tables[0].Rows[0]["在线咨询"].ToString() + ds.Tables[0].Rows[0]["公司网址"].ToString() +
                                                ds.Tables[0].Rows[0]["邮编"].ToString() + ds.Tables[0].Rows[0]["电子邮件"].ToString() +
                                                ds.Tables[0].Rows[0]["收货地址"].ToString() + ds.Tables[0].Rows[0]["经营范围"].ToString() +
                                                ds.Tables[0].Rows[0]["单位性质"].ToString() + ds.Tables[0].Rows[0]["成立时间"].ToString() +
                                                ds.Tables[0].Rows[0]["注册资本(万元)"].ToString() + ds.Tables[0].Rows[0]["资产规模(万元)"].ToString() +
                                                ds.Tables[0].Rows[0]["年销售额(万元)"].ToString() + ds.Tables[0].Rows[0]["员工总数(个)"].ToString() +
                                                ds.Tables[0].Rows[0]["法人代表"].ToString() + ds.Tables[0].Rows[0]["行业"].ToString() +
                                                ds.Tables[0].Rows[0]["注册地址"].ToString() + ds.Tables[0].Rows[0]["关系描述"].ToString() +
                                                ds.Tables[0].Rows[0]["发展计划"].ToString() + ds.Tables[0].Rows[0]["合作方法"].ToString();
                               
                            }
                            catch
                            {
                                this.lbl_result.Text = "数据读取失败,可能原因：Excel模板格式不正确";
                                //initvalidate();
                                this.tr_result.Visible = false;
                                this.lbl_validateend.Visible = false;
                                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                                return;
                            }
                            
                            this.lbl_result.Text = "Excel文件上传成功!";
                            this.setup1.Enabled = true;
                            this.setup2.Enabled = false;
                            this.setup3.Enabled = false;
                            this.setup4.Enabled = false;
                            this.setup5.Enabled = false;
                            this.setup6.Enabled = false;
                            this.setup7.Enabled = false;
                            this.setup8.Enabled = false;
                            this.tr_result.Visible = false;
                           
                            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());                           
                        }
                        else
                        {
                            initvalidate();
                            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                            this.lbl_result.Text = "您导入的Excel表没有数据!";
                        }
                    }
                    else
                    {
                        initvalidate();
                        ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                        this.lbl_result.Text = "您导入的Excel表没有数据!";
                    }
                }
                catch (Exception ex)
                {
                    //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "数据读取失败");
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                    initvalidate();
                    this.lbl_result.Text = "数据读取失败,原因：" + ex.Message.ToString();
                    //this.lbl_result.Text = "数据读取失败!";
                }
                #endregion
            }
            catch (Exception ex)
            {
                //this.setup1.Enabled = false;
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                this.lbl_result.Text = "数据读取失败,原因：" + ex.Message;
            }
        }
        #endregion
    }
    #endregion

    #region Excel空值校验
    protected void setup1_Click(object sender, EventArgs e)
    {
        //排除DELETE删除的空行
        DataTable dt = ds.Tables[0];
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {           
            if (dt.Rows[i].RowState != DataRowState.Deleted)
            {
                for (int f = 0; f > 30; f++ )
                {
                    if (dt.Rows[i][f].ToString().Trim() != "")
                    {
                        return;
                    }
                    else
                    {
                        dt.Rows[i].Delete();
                    }
                }
            }
        }
        dt.AcceptChanges();

        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        errorstr = string.Empty;

        string[] code = { "序号", "客户编号", "客户名称" };
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
            this.setup1.Enabled = false;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup1.Enabled = false;
            this.setup2.Enabled = true;
        }
    }
    #endregion

    #region Excel内部重复数据校验
    protected void setup2_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        #region excel内部重复判断 序号
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                if (ds.Tables[0].Rows[i]["序号"].ToString().Trim() == ds.Tables[0].Rows[k]["序号"].ToString().Trim())
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的序号与第" + (k + 2).ToString() + "行中的序号重复,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>重复值校验错误列表(序号重复)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }
        #endregion

        #region/*excel内部重复判断客户编号、客户名称2个字段值同时相同*/
        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                if (ds.Tables[0].Rows[i]["客户编号"].ToString().Trim() == ds.Tables[0].Rows[k]["客户编号"].ToString().Trim() ||
                    ds.Tables[0].Rows[i]["客户名称"].ToString().Trim() == ds.Tables[0].Rows[k]["客户名称"].ToString().Trim())
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的客户编号或客户名称与第" + (k + 2).ToString() + "行中的客户编号或客户名称重复,导入操作失败!<br>";
                }
            }
        }
        if (j > 0)
        {
            total += j;
            errorstr += "<strong>重复值校验错误列表(客户编号或客户名称有重复值)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }
        #endregion

        if (total > 0)
        {
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
            this.tr_result.Visible = true;
            this.setup2.Enabled = false;
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup2.Enabled = false;
            this.setup6.Enabled = true;
        }
    }
    #endregion

    #region 判断客户编号、名称是否存在
    protected void setup3_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int k = 0;
        
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["客户编号"].ToString().Length > 0)
            {
                string[] arr = new string[] { "CustNo", "CompanyCD" };
                string[] CustNos = new string[] { ds.Tables[0].Rows[i]["客户编号"].ToString().Trim(), userinfo.CompanyCD };

                bool NoHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.CustInfo", arr, CustNos);
                if (NoHas)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的客户编号与数据库中记录重复,导入操作失败!<br>";
                }
            }
            if (ds.Tables[0].Rows[i]["客户名称"].ToString().Length > 0)
            {
                string[] arr = new string[] { "CustName", "CompanyCD" };
                string[] CustNames = new string[] { ds.Tables[0].Rows[i]["客户名称"].ToString().Trim(), userinfo.CompanyCD };

                bool NameHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.CustInfo", arr, CustNames);
                if (NameHas)
                {
                    suberrorstr += (++k).ToString() + "： " + "第" + (i + 2).ToString() + "行中的客户名称与数据库中记录重复,导入操作失败!<br>";
                }
            }
        }
        if (j > 0 || k > 0)
        {
            errorstr += "<strong>是否存在校验错误列表(客户编号,名称)</strong><br>";
            errorstr += suberrorstr;

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
            this.tr_result.Visible = true;
            this.setup3.Enabled = false;
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup3.Enabled = false;
            this.setup4.Enabled = true;
        }
    }
    #endregion

    #region 日期时间格式校验
    protected void setup4_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        string[] code = { "成立时间" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                try
                {
                    if (ds.Tables[0].Rows[i][code[k]].ToString().Length > 0)
                    {
                        DateTime.Parse(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                    }
                }
                catch
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 格式错误,导入操作失败!<br>";
                }
            }
        }
        if (j > 0)
        {
            errorstr = "<strong>日期时间格式校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            this.setup4.Enabled = false;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);

            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup4.Enabled = false;
            this.setup5.Enabled = true;
        }
    }
    #endregion

    #region 人员与人员编号匹配校验
    protected void setup5_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号

        DataColumn Manager = new DataColumn();
        ds.Tables[0].Columns.Add("Manager");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["人员编号"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[i]["分管业务员"].ToString().Trim().Length > 0)
            {
                DataTable dt = EmployeeInfoDBHelper.IsHaveEmployeesByEmployeeNOAndName(ds.Tables[0].Rows[i]["分管业务员"].ToString().Trim(), ds.Tables[0].Rows[i]["人员编号"].ToString().Trim(), userinfo.CompanyCD);
                if (dt.Rows.Count < 1)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的分管业务员与对应的人员编号不匹配,导入操作失败!<br>";
                }
                else
                {
                    ds.Tables[0].Rows[i]["Manager"] = dt.Rows[0]["ID"].ToString();
                }
            }
        }
        if (j > 0)
        {
            errorstr = "<strong>人员与人员编号匹配式校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            this.setup5.Enabled = false;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);

            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup5.Enabled = false;
            this.setup7.Enabled = true;
        }
    }
    #endregion

    #region 特殊字符校验
    protected void setup6_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int k = 0;
        errorstr = string.Empty;
       
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (checkString(ds.Tables[0].Rows[i]["客户名称"].ToString().Trim()))
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的客户名称含有特殊字符,导入操作失败!<br>";
            }
            if (chenkHave(ds.Tables[0].Rows[i]["客户编号"].ToString().Trim()))
            {
                suberrorstr += (++k).ToString() + "： " + "第" + (i + 2).ToString() + "行中的客户编号含有特殊字符,导入操作失败!<br>";
            }
        }

        if (j > 0 || k > 0)
        {
            errorstr = "<strong>含有特殊字符校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            this.setup6.Enabled = false;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);

            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup6.Enabled = false;
            this.setup3.Enabled = true;
        }
    }
    #endregion

    #region 验证特殊字符
    public static bool checkString(string source)
    {
        //Regex regExp = new Regex("[/^[a-zA-Z0-9_-()[]{}.]+$/g]");
        Regex regExp = new Regex(@"^[^'\\<>%?/]*$");
        return !regExp.IsMatch(source);
    }

    public static bool chenkHave(string source)
    {
        Regex regExp = new Regex(@"^[a-zA-Z0-9-()_\[\]{}\.\\]+$");
        return !regExp.IsMatch(source);
    }
    #endregion

    #region 数字格式校验
    protected void setup7_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        string[] code = { "注册资本(万元)", "资产规模(万元)", "年销售额(万元)", "员工总数(个)" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                try
                {
                    if (ds.Tables[0].Rows[i][code[k]].ToString().Length > 0)
                    {
                        if (k == 3)
                        {
                            Convert.ToInt32(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                        }
                        else
                        {
                            Convert.ToDecimal(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                            //decimal.Round(decimal.Parse(ds.Tables[0].Rows[i][code[k]].ToString().Trim()), 2);
                        }
                    }
                }
                catch
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 格式错误,导入操作失败!<br>";
                }
            }
        }
        if (j > 0 )
        {
            errorstr = "<strong>数字格式校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            this.setup7.Enabled = false;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);

            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup7.Enabled = false;
            this.setup8.Enabled = true;
        }
    }
    #endregion

    #region 单位性质校验（1事业，2企业，3社团，4自然人，5其他）
    protected void setup8_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号

        DataColumn EmployeeID = new DataColumn();
        ds.Tables[0].Columns.Add("CompanyType");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["单位性质"].ToString().Trim().Length > 0)
            {
                //单位性质（1事业，2企业，3社团，4自然人，5其他）
                switch (ds.Tables[0].Rows[i]["单位性质"].ToString())
                {
                    case "事业":
                        ds.Tables[0].Rows[i]["CompanyType"] = "1";
                        break;
                    case "企业":
                        ds.Tables[0].Rows[i]["CompanyType"] = "2";
                        break;
                    case "社团":
                        ds.Tables[0].Rows[i]["CompanyType"] = "3";
                        break;
                    case "自然人":
                        ds.Tables[0].Rows[i]["CompanyType"] = "4";
                        break;
                    case "其他":
                        ds.Tables[0].Rows[i]["CompanyType"] = "5";
                        break;
                    default:
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 单位性质不存在,导入操作失败!<br>";
                        break;
                }
            }
        }
        if (j > 0)
        {
            errorstr = "<strong>单位性质校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            this.setup8.Enabled = false;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);

            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup8.Enabled = false;
            this.setup9.Enabled = true;
            //this.btn_input.Enabled = true;
            //lbl_validateend.Visible = true;
        }
    }
    #endregion

    #region 区域校验
    protected void setup9_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号

        DataColumn AreaID = new DataColumn();
        ds.Tables[0].Columns.Add("AreaID");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["区域"].ToString().Trim().Length > 0)
            {
                DataTable dt = CustInfoBus.CheckArea(ds.Tables[0].Rows[i]["区域"].ToString().Trim(), userinfo.CompanyCD);

                if (dt.Rows.Count < 1)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的区域与系统的区域不匹配,导入操作失败!<br>";
                }
                else
                {
                    ds.Tables[0].Rows[i]["AreaID"] = dt.Rows[0]["ID"].ToString();
                }                
            }
        }
        if (j > 0)
        {
            errorstr = "<strong>与系统的区域匹配式校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            this.setup5.Enabled = false;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);

            }
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup9.Enabled = false;           
            this.btn_input.Enabled = true;
            lbl_validateend.Visible = true;
        }
    }
    #endregion
}
