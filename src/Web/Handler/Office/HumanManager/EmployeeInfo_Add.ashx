<%@ WebHandler Language="C#" Class="EmployeeInfo_Add" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/13
 * 描    述： 新建人员
 * 修改日期： 2009/03/13
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class EmployeeInfo_Add : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    /// <summary>
    /// 处理新建人员的请求
    /// </summary>
    /// <param name="context">请求上下文</param>
    public void ProcessRequest (HttpContext context)
    {
        //定义储存人员信息的Model变量
        EmployeeInfoModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码已经存在时
        if (model != null)
        {
            //执行保存操作
            bool isSucc = EmployeeInfoBus.SaveEmployeeInfo(model);
            //保存成功时
            if (isSucc)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.EmployeeNo, 1);
            }
            //保存未成功时
            else
            {
                jc = new JsonClass(context.Request.Params["EditFlag"], "", 0);
            }
        }
        else
        {
            jc = new JsonClass("", "", 2);
        }
        //输出响应
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    
    /// <summary>
    /// 从请求中获取人员信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private EmployeeInfoModel EditRequstData(HttpRequest request)
    {
        //定义人员信息Model变量
        EmployeeInfoModel emploModel = new EmployeeInfoModel();
        //编辑标识
        emploModel.EditFlag = request.Params["EditFlag"].ToString().Trim().Trim();

        /* 获取人员编号 */
        string employeeNo = request.Params["EmployeeNo"].ToString().Trim().Trim();
        
        //新建时，设置创建者信息
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(emploModel.EditFlag))
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            emploModel.CreateUserID = userInfo.EmployeeID.ToString().Trim().Trim();//创建人
            emploModel.CreateDate = DateTime.Now;//创建时间

            //人员编号为空时，通过编码规则编号获取人员编号
            if (string.IsNullOrEmpty(employeeNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString().Trim().Trim();
                //通过编码规则代码获取人员编码
                employeeNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_EMPLOYYEEINFO
                                , ConstUtil.CODING_RULE_COLUMN_EMPLOYYEENO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_EMPLOYYEEINFO
                                , ConstUtil.CODING_RULE_COLUMN_EMPLOYYEENO, employeeNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }    
        
        //设置人员编号
        emploModel.EmployeeNo = employeeNo;

        string pagePhotoURL = request.Params["PagePhotoURL"].ToString().Trim().Trim();//页面相片路径
        //相片未指定时
        if (!string.IsNullOrEmpty(pagePhotoURL) && pagePhotoURL.IndexOf("Images/Pic/Pic_Nopic.jpg") > -1)
        {
            emploModel.PagePhotoURL = string.Empty;
        }
        else
        {
            emploModel.PagePhotoURL = pagePhotoURL;
        }
        
        emploModel.PhotoURL = request.Params["PhotoURL"].ToString().Trim().Trim();//数据库中保存的相片路径
        
        emploModel.EmployeeName = request.Params["EmployeeName"].ToString().Trim().Trim();//姓名
        emploModel.EmployeeNum = request.Params["EmployeeNum"].ToString().Trim().Trim();//工号
        emploModel.PYShort = request.Params["PYShort"].ToString().Trim().Trim();//拼音缩写
        emploModel.UsedName = request.Params["UsedName"].ToString().Trim().Trim();//曾用名
        emploModel.NameEn = request.Params["NameEn"].ToString().Trim().Trim();//英文名

        emploModel.Flag = request.Params["Flag"].ToString().Trim().Trim();//分类标识
        emploModel.CardID = request.Params["CardID"].ToString().Trim().Trim();//身份证
        emploModel.SafeguardCard = request.Params["SafeguardCard"].ToString().Trim();//社保卡号

        //if ("2".Equals(emploModel.Flag))
        //{
        emploModel.PositionTitle = request.Params["PositionTitle"].ToString().Trim().Trim();//应聘职务
        //}
        //else
        //{
        emploModel.PositionID = request.Params["Position"].ToString().Trim().Trim();//职称
        emploModel.QuarterID = request.Params["Quarter"].ToString().Trim().Trim();//所在岗位
        emploModel.DeptID = request.Params["DeptID"].ToString().Trim().Trim() == "" ? 0 : Convert.ToInt32(request.Params["DeptID"].ToString().Trim().Trim());//所在部门
        emploModel.AdminLevelID = request.Params["AdminLevelID"].ToString().Trim().Trim() == "" ? 0 : Convert.ToInt32(request.Params["AdminLevelID"].ToString().Trim().Trim());
        
        if(request.Params["EnterDate"].ToString().Trim().Trim() != "")
            emploModel.EnterDate = Convert.ToDateTime(request.Params["EnterDate"].ToString().Trim().Trim());
        //}

        emploModel.Sex = request.Params["Sex"].ToString().Trim().Trim();//性别
        emploModel.Birth = request.Params["Birth"].ToString().Trim().Trim();//出身日期
        emploModel.MarriageStatus = request.Params["Marriage"].ToString().Trim().Trim();//婚姻状况
        emploModel.Origin = request.Params["Origin"].ToString().Trim().Trim();//籍贯
        emploModel.Telephone = request.Params["Telephone"].ToString().Trim().Trim();//联系电话
        emploModel.Mobile = request.Params["Mobile"].ToString().Trim().Trim();//手机号码
        emploModel.EMail = request.Params["EMail"].ToString().Trim().Trim();//电子邮件
        emploModel.OtherContact = request.Params["OtherContact"].ToString().Trim().Trim();//其他联系方式
        emploModel.HomeAddress = request.Params["HomeAddress"].ToString().Trim().Trim();//家庭住址
        emploModel.HealthStatus = request.Params["Health"].ToString().Trim().Trim();//健康状况
        emploModel.CultureLevel = request.Params["Culture"].ToString().Trim().Trim();//学历
        emploModel.GraduateSchool = request.Params["School"].ToString().Trim().Trim();//毕业院校
        emploModel.Professional = request.Params["Professional"].ToString().Trim().Trim();//专业

        emploModel.Landscape = request.Params["Landscape"].ToString().Trim().Trim();//政治面貌
        emploModel.Religion = request.Params["Religion"].ToString().Trim().Trim();//宗教信仰
        emploModel.National = request.Params["National"].ToString().Trim().Trim();//民族
        emploModel.Account = request.Params["Account"].ToString().Trim().Trim();//户口
        emploModel.AccountNature = request.Params["AccountNature"].ToString().Trim().Trim();//户口性质
        emploModel.CountryID = request.Params["Country"].ToString().Trim().Trim();//国籍
        emploModel.Height = request.Params["Height"].ToString().Trim().Trim();//身高
        emploModel.Weight = request.Params["Weight"].ToString().Trim().Trim();//体重
        emploModel.Sight = request.Params["Sight"].ToString().Trim().Trim();//视力
        emploModel.Degree = request.Params["Degree"].ToString().Trim().Trim();//最高学位
        emploModel.DocuType = request.Params["DocuType"].ToString().Trim().Trim();//证件类型

        emploModel.Features = request.Params["Features"].ToString().Trim().Trim();//特长
        emploModel.ComputerLevel = request.Params["ComputerLevel"].ToString().Trim().Trim();//计算机水平
        emploModel.WorkTime = request.Params["WorkTime"].ToString().Trim().Trim();//参加工作时间
        emploModel.TotalSeniority = request.Params["TotalSeniority"].ToString().Trim().Trim();//总工龄
        emploModel.ForeignLanguage1 = request.Params["Language1"].ToString().Trim().Trim();//外语语种1
        emploModel.ForeignLanguage2 = request.Params["Language2"].ToString().Trim().Trim();//外语语种2
        emploModel.ForeignLanguage3 = request.Params["Language3"].ToString().Trim().Trim();//外语语种3
        emploModel.ForeignLanguageLevel1 = request.Params["LanguageLevel1"].ToString().Trim().Trim();//外语水平1
        emploModel.ForeignLanguageLevel2 = request.Params["LanguageLevel2"].ToString().Trim().Trim();//外语水平2
        emploModel.ForeignLanguageLevel3 = request.Params["LanguageLevel3"].ToString().Trim().Trim();//外语水平3

        emploModel.Resume = request.Params["ResumeURL"].ToString().Trim().Trim();//原有的简历路径
        emploModel.PageResume = request.Params["PageResumeURL"].ToString().Trim().Trim();//页面简历路径
        emploModel.ProfessionalDes = request.Params["ProfessionalDes"].ToString().Trim().Trim();//专业描述
        
        
        //设置工作履历
        int workCount = int.Parse(request.Params["WorkCount"]);
        //工作履历输入时，编辑工作履历信息
        if (workCount > 0)
        {
            //遍历工作履历
            for (int i = 1; i <= workCount; i++)
            {
                //定义履历Model变量
                EmployeeHistoryModel historyModel = new EmployeeHistoryModel();
                //开始时间
                historyModel.StartDate = request.Params["WorkStart_" + i].ToString().Trim().Trim();
                //结束时间
                historyModel.EndDate = request.Params["WorkEnd_" + i].ToString().Trim().Trim();
                //工作单位
                historyModel.Company = request.Params["WorkCompany_" + i].ToString().Trim().Trim();
                //所在部门
                historyModel.Department = request.Params["WorkDept_" + i].ToString().Trim().Trim();
                //工作内容
                historyModel.WorkContent = request.Params["WorkContent_" + i].ToString().Trim().Trim();
                //离职原因
                historyModel.LeaveReason = request.Params["LeaveReason_" + i].ToString().Trim().Trim();
                //区分 工作
                historyModel.Flag = ConstUtil.HUMAN_HISTORY_WORK;
                //添加到履历列表中
                emploModel.HistoryList.Add(historyModel);
            }
        }
        //设置学历履历
        int studyCount = int.Parse(request.Params["StudyCount"]);
        //学习履历输入时，编辑学习履历信息
        if (studyCount > 0)
        {
            //遍历学习履历
            for (int i = 1; i <= studyCount; i++)
            {
                //定义履历Model变量
                EmployeeHistoryModel historyModel = new EmployeeHistoryModel();
                //开始时间
                historyModel.StartDate = request.Params["StudyStart_" + i].ToString().Trim().Trim();
                //结束时间
                historyModel.EndDate = request.Params["StudyEnd_" + i].ToString().Trim().Trim();
                //学校名称
                historyModel.SchoolName = request.Params["SchoolName_" + i].ToString().Trim().Trim();
                //专业
                historyModel.Professional = request.Params["Professional_" + i].ToString().Trim().Trim();
                //学历
                historyModel.CultureLevel = request.Params["CultureLevel_" + i].ToString().Trim().Trim();
                //区分 学习
                historyModel.Flag = ConstUtil.HUMAN_HISTORY_STUDY;
                //添加到履历列表中
                emploModel.HistoryList.Add(historyModel);
            }
        }
        //设置技能信息
        int skillCount = int.Parse(request.Params["SkillCount"]);
        //技能信息输入时，编辑技能信息
        if (skillCount > 0)
        {
            //遍历技能信息
            for (int i = 1; i <= skillCount; i++)
            {
                //定义技能信息Model变量
                EmployeeSkillModel skillModel = new EmployeeSkillModel();
                //技能名称
                skillModel.SkillName = request.Params["SkillName_" + i].ToString().Trim().Trim();
                //证件名称
                skillModel.CertificateName = request.Params["CertificateName_" + i].ToString().Trim().Trim();
                //证件编号
                skillModel.CertificateNo = request.Params["CertificateNo_" + i].ToString().Trim().Trim();
                //证件等级
                skillModel.CertificateLevel = request.Params["CertificateLevel_" + i].ToString().Trim().Trim();
                //发证单位
                skillModel.IssueCompany = request.Params["IssueCompany_" + i].ToString().Trim();
                //发证时间
                skillModel.IssueDate = request.Params["IssueDate_" + i].ToString().Trim();
                //有效期
                skillModel.Validity = request.Params["Validity_" + i].ToString().Trim();
                
                //添加到技能列表中
                emploModel.SkillList.Add(skillModel);
            }
        }

        return emploModel;
    }

}