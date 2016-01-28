<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitGuid.aspx.cs" Inherits="Pages_Office_HumanManager_InitGuid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>人力资源体系建立向导</title>
   <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        <!
        -- body
        {
            margin-left: 30px;
            margin-top: 30px;
            margin-right: 30px;
            margin-bottom: 30px;
        }
        -- ></style>
</head>
<body>
    <table width="100%" border="0" cellpadding="10" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="40">
                <p align="center" class="Title">
                    <img src="../../../images/number/logo_hr.jpg" width="420" height="65" /><strong>
                        <br />
                        <br />
                    </strong><span class="redbold">初次使用人力资源管理系统，请参照以下步骤构建贵公司的人力资源管理体系。</span></p>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <img src="../../../images/number/Num_01.jpg" width="26" height="26" align="absmiddle" />
                     <a href="<%= GetLinks(2011901,"../Systemmanager/CodePublicType.aspx?TypeFlag=2&ModuleID=") %>">基本设置</a>
                    <br />
                    <br />
                    为了方便单据的快速录入，人力资源管理系统提供了基本设置功能，人力资源管理系统所涉及到的公共分类属性（下拉列表选项）以及单据的编号规则统一在这里设置。<br />
                    <br />
                    <img src="../../../images/number/Num_02.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_DEPT_EDIT),"../HumanManager/DeptInfo_Info.aspx?ModuleID=") %>">设置公司的组织机构</a>
                    <br />
                    <br />
                    对照贵公司的组织机构划分，从上到下逐级添加各个部门的信息。<br />
                    <br />
                    <img src="../../../images/number/Num_03.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_DEPTQUARTER_EDIT),"../HumanManager/DeptQuarter_Info.aspx?ModuleID=") %>">设置公司的岗位</a>
                    <br />
                    <br />
                    根据贵公司各个部门的岗位划分，从上到下逐级添加各个岗位的信息，明确各个岗位的职责和任职资格要求，定义岗位说明书。
                    <br />
                    <br />
                    <img src="../../../images/number/Num_04.jpg" width="26" height="26" align="absmiddle" />
                   <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD),"../HumanManager/EmployeeInfo_Add.aspx?FromPage=6&ModuleID=") %>">建立在职员工档案</a>
                    <br />
                    <br />
                    将所有在职员工的档案录入系统。<br />
                    <br />
                    <img src="../../../images/number/Num_05.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_RECTCHECKELEM_EDIT),"../HumanManager/RectCheckElem_Info.aspx?ModuleID=") %>">建立招聘面试评测模板</a>
                    <br />
                    <br />
                    根据岗位说明书的定义的各岗位任职资格，确定各岗位相应的面试评测模板。
                    <br />
                    先设置面试评测模板的要素，然后建立各岗位的面试模板。
                    <br />
                    <br />
                    <img src="../../../images/number/Num_06.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_SALARY_SET),"../HumanManager/SalarySet_Edit.aspx?ModuleID=") %>">设置薪酬体系结构</a>
                    <br />
                    <br />
                    根据公司的薪酬体系结构，设置人员薪酬结构，包括：固定工资项设置、浮动工资项设置、社会保险设置、个人所得税设置、岗位工资设置、员工薪资结构设置。<br />
                    <br />
                    <img src="../../../images/number/Num_07.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_PERFORMANCECHECK),"../HumanManager/PerformanceType.aspx?ModuleID=") %>">建立绩效考核模板</a>
                    <br />
                    <br />
                    根据贵公司的考核要求，先建立绩效考核的类型（如：月考核、季度考核、年终考核）。
                    <br />
                    然后设置考核指标和各岗位对应的绩效考核模板。
                    <br />
                    最后设置好各个员工的考核流程（日常进行考核时系统会自动根据考核流程进行考评）。<br />
                    <br />
                    <img src="../../../images/number/Num_08.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_Flow),"../Systemmanager/ApprovalFlowSet.aspx?TypeFlag=2&ModuleID=") %>">设置审批流程</a>
                </p>
                设置好日常人力资源管理中需要进行审批的各类单据对应的审批流程，确定各流程步骤的审批人。（实际业务单据提交审批时会根据设置好的流程进行流转审批）
            </td>
        </tr>
    </table>
</body>
</html>
