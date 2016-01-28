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
using XBase.Business.Office.HumanManager;
public partial class Pages_Office_HumanManager_HProfileFromExcel : System.Web.UI.Page
{
    UserInfoUtil userinfo = new UserInfoUtil();
    public static DataSet ds = new DataSet();
    public string errorstr = string.Empty; //错误串
    private static int ruleCode; //企业员工编号规则码
    protected void Page_Load(object sender, EventArgs e)
    {
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        if (!Page.IsPostBack)
        {
            initState();
        }
    }

    protected void initState()
    {
        lbl_upresult.Text = string.Empty; errorstr = string.Empty;
        setup1.Enabled = false; setup2.Enabled = false; setup3.Enabled = false; btn_input.Enabled = false; setup4.Enabled = false;
        lbl_validateend.Visible = false; tab_end.Visible = false; tr_result.Visible = false; tab_end.Visible = false;

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
                //Session["newfile"] = "HR_ZHOU.xls";
                upExcelFile.PostedFile.SaveAs(upfilepath + @"\" + Session["newfile"].ToString());
                this.lbl_upresult.Text = "Excel文件上传成功!";
                this.setup1.Enabled = true;
                //将excel中的数据读取到ds中
                try
                {
                    ds = EmployeeInfoBus.ReadEexcel(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        initState();
                        this.lbl_upresult.Text = "您导入的Excel表没有数据!";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    initState();
                    this.lbl_upresult.Text = "数据读取失败,原因：" + ex.Message;
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
        //非空判断
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        string[] code = { "姓名", "性别" };
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
            errorstr = "<strong>Excel空值校验错误列表</strong><br>";
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
            tr_repeat.Visible = false;
            this.setup1.Enabled = false;
            this.setup2.Enabled = true;
        }
    }

    protected void setup2_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        string[] code = { "生日", "入职时间", "参加工作时间" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                try
                {
                    if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length > 0)
                    {
                        string timedata = ds.Tables[0].Rows[i][code[k]].ToString().Trim();
                        int index = -1;
                        char tagstr='A';
                        char[] tag = {'-','/','.' };
                        int ii=0;
                        do
                        {
                            index = timedata.IndexOf(tag[ii]);
                            if (index > -1)
                            {
                                tagstr = tag[ii];
                                break;
                            }
                            ii++;
                        } while (ii < 3);
                        if (tagstr=='A')
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 日期格式错误,正确的格式如：2009-08-13，2009/08/13或者2009.08.13,导入操作失败!<br>";
                        }
                        else
                        {
                            timedata = timedata.Replace(tagstr,'-');
                            try
                            {
                                DateTime mydate = Convert.ToDateTime(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                            }
                            catch
                            {
                                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 日期格式错误,正确的格式如：2009-08-13，2009/08/13或者2009.08.13,导入操作失败!<br>";
                            }
                            //string[] alldata = timedata.Split(tagstr);
                            //if (alldata[0].Length == 4 && (alldata[1].Length == 1 || alldata[1].Length == 2) && (alldata[2].Length == 1 || alldata[2].TrimEnd(' ').Length == 2))
                            //{
                            //    DateTime mydate = Convert.ToDateTime(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                            //}
                            //else
                            //{
                            //    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 日期格式错误,正确的格式如：2009-08-13，2009/08/13或者2009.08.13,导入操作失败!<br>";
                            //}
                        }
                    }  
                }
                catch
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 日期格式错误,正确的格式如：2009-08-13，2009/08/13或者2009.08.13,导入操作失败!<br>";
                }
            }
        }
        total += j;
        if (j > 0)
        {
            errorstr = "<strong>格式校验错误列表(时间格式)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["性别"].ToString().Trim() != "男" && ds.Tables[0].Rows[i]["性别"].ToString().Trim() != "女")
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的性别格式错误，应该填写为(男/女),导入操作失败!<br>";
            }
        }
        total += j;
        if (j > 0)
        {
            errorstr += "<strong>格式校验错误列表(性别格式)</strong><br>";
            errorstr += suberrorstr;
        }
        j = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["人员分类"].ToString().Trim() != "在职人员" && ds.Tables[0].Rows[i]["人员分类"].ToString().Trim() != "人才储备" && ds.Tables[0].Rows[i]["人员分类"].ToString().Trim() != "离职人员")
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的人员分类错误，应该填写为(在职人员/人才储备/离职人员),导入操作失败!<br>";
            }
        }
        total += j;
        if (j > 0)
        {
            errorstr += "<strong>格式校验错误列表(人才类型格式)</strong><br>";
            errorstr += suberrorstr;
        }

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
            tr_repeat.Visible = false;
            this.setup2.Enabled = false;
            this.setup3.Enabled = true;
        }
    }

    protected void setup3_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim().Length > 0)
            {
                bool flag = EmployeeInfoBus.ChargeDeptInfo(ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim(), ds.Tables[0].Rows[i]["部门编号"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    if (ds.Tables[0].Rows[i]["部门编号"].ToString().Trim().Length > 0)
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() + "</span>的 部门 不存在,导入操作失败!<br>";
                    }
                    else
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 目前所在部门 不存在,导入操作失败!<br>";
                    }
                    tr_repeat.Visible = false;
                }

                //部门多于一个选择
                int num = EmployeeInfoBus.ChargeDeptInfoNum(ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim(), ds.Tables[0].Rows[i]["部门编号"].ToString().Trim(), userinfo.CompanyCD);
                if (num > 1)
                {
                    DataTable DeptInfoDt = EmployeeInfoBus.GetDeptInfo(ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim(), userinfo.CompanyCD);
                    Repeat_data.DataSource = DeptInfoDt;
                    Repeat_data.DataBind();
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 目前所在部门 重复,导入操作失败!<br><br><span style='color:red'>解决办法</span>:<span style='color:blue'>在数据重复列表中找到正确的部门编号,填写到Excel文件中,然后重新导入</span>";
                    this.tr_result.Visible = true;
                    tr_repeat.Visible = true;
                    tr_quqrter.Visible = false;
                    tr_dept.Visible = true;
                    errorstr = suberrorstr;
                    return;
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>是否存在校验错误列表(部门)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
            this.tr_result.Visible = true;
            tr_quqrter.Visible = false;
            tr_dept.Visible = false;
            return;
        }

        j = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //判断岗位是否存在
            if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0)
            {
                bool flag = EmployeeInfoBus.ChargeQuarterInfo(ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim(), ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim(), ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim(), ds.Tables[0].Rows[i]["部门编号"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag) //岗位不存在
                {
                    if (ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[i]["部门编号"].ToString().Trim().Length > 0)
                    {
                        if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + 
                           "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() + 
                           "</span>,部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                           "</span>,岗位名称为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在岗位"].ToString() +
                           "</span>,岗位编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["岗位编号"].ToString() +
                           "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + 
                              "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                              "</span>,部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                              "</span>,岗位名称为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在岗位"].ToString() + 
                              "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else if (ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                               "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                               "</span>,部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                               "</span>,岗位编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["岗位编号"].ToString() +
                               "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                              "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                              "</span>,部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                              "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                    }
                    else if (ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim().Length > 0)
                    {
                        if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                           "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                           "</span>,岗位名称为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在岗位"].ToString() +
                           "</span>,岗位编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["岗位编号"].ToString() +
                           "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                              "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                              "</span>,岗位名称为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在岗位"].ToString() +
                              "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else if (ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                               "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                               "</span>,岗位编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["岗位编号"].ToString() +
                               "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                              "行中部门为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在部门"].ToString() +
                              "</span>,部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                              "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                    }
                    else if (ds.Tables[0].Rows[i]["部门编号"].ToString().Trim().Length > 0)
                    {
                        if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                           "行中的部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                           "</span>,岗位名称为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在岗位"].ToString() +
                           "</span>,岗位编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["岗位编号"].ToString() +
                           "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else if (ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                              "行中的部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                              "</span>,岗位名称为<span style='color:blue'>" + ds.Tables[0].Rows[i]["目前所在岗位"].ToString() +
                              "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else if (ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim().Length > 0)
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                               "行中的部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                               "</span>,岗位编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["岗位编号"].ToString() +
                               "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                        else
                        {
                            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() +
                              "行中的部门编号为<span style='color:blue'>" + ds.Tables[0].Rows[i]["部门编号"].ToString() +
                              "</span>的 岗位 不存在,导入操作失败!<br>";
                        }
                    }
                    tr_repeat.Visible = false;
                }

                //岗位多于一个选择
                DataTable QuarterDT = EmployeeInfoBus.ChargeQuarterInfoNum(ds.Tables[0].Rows[i]["目前所在岗位"].ToString().Trim(), ds.Tables[0].Rows[i]["岗位编号"].ToString().Trim(), ds.Tables[0].Rows[i]["目前所在部门"].ToString().Trim(), ds.Tables[0].Rows[i]["部门编号"].ToString().Trim(), userinfo.CompanyCD);
                if (QuarterDT.Rows.Count > 1)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 目前所在岗位 重复,导入操作失败!<br><br><span style='color:red'>解决办法</span>:<span style='color:blue'>在数据重复列表中找到正确的岗位编号,填写到Excel文件中,然后重新导入</span>";
                    this.tr_result.Visible = true;
                    tr_quqrter.Visible = true;
                    tr_repeat.Visible = true;
                    tr_dept.Visible = false;
                    rpt_quarter.DataSource = QuarterDT;
                    rpt_quarter.DataBind();
                    errorstr = suberrorstr;
                    return;
                }
            }

        }

        if (j > 0)
        {
            total += j;
            errorstr = "<strong>是否存在校验错误列表(岗位)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["婚姻状况"].ToString().Trim().Length > 0)
            {
                bool flag = EmployeeInfoBus.ChargeHyInfo(ds.Tables[0].Rows[i]["婚姻状况"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 婚姻状况 不存在,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            total += j;
            errorstr += "<strong>是否存在校验错误列表(婚姻状况)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        if (total > 0)
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
            this.setup3.Enabled = false;
            this.setup4.Enabled = true;
        }
    }

    protected void setup4_Click(object sender, EventArgs e)
    {
        errorstr = string.Empty;
        ruleCode = EmployeeInfoBus.GetCodeRuleID(userinfo.CompanyCD);

        if (ruleCode < 1)
        {
            errorstr = "<strong>员工编号编码规则没有设置,设置步骤如下</strong><br><br>办公模式->系统管理->公用设置->基础数据编号设置，点击[新建]按钮，选择“基础数据名称”为“人员”";
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
            tr_repeat.Visible = false;
            this.setup4.Enabled = false;
            this.btn_input.Enabled = true;
            lbl_validateend.Visible = true;
        }
    }

    protected void btn_input_Click(object sender, EventArgs e)
    {
        try
        {
            userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            EmployeeInfoBus.GetExcelToEmployeeInfo(userinfo.CompanyCD);
            this.lbl_jg.Text = "Excel数据导入成功";
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "成功导入" + ds.Tables[0].Rows.Count.ToString() + "条数据");
            /*
             * 更新有特殊符号的员工编号"##@@$$@@##"
             */
            DataSet nullds = EmployeeInfoBus.GetNullEmployeeList(userinfo.CompanyCD);
            for (int i = 0; i < nullds.Tables[0].Rows.Count; i++)
            {
                //获取规则码
                string employeeNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(ruleCode.ToString(), "EmployeeInfo", "EmployeeNo");
                EmployeeInfoBus.UpdateEmployeeInfo(userinfo.CompanyCD, employeeNo, nullds.Tables[0].Rows[i]["ID"].ToString());
            }
            /*
             * 更新结束
             */

            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
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
