<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitGuid.aspx.cs" Inherits="Pages_Office_SystemManager_InitGuid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>系统初始化向导</title>
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
                    <img src="../../../images/number/logo_csh.jpg" width="320" height="65" /><strong>
                        <br />
                        <br />
                    </strong><span class="redbold">首次使用系统，请按以下说明与步骤完成系统初始化和准备工作。</span></p>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <strong>一、首先根据贵公司实际使用需求初步规划好本系统的操作用户及权限分配方案，并确定各业务板块的管理人员。规划确定后，用系统超级管理员用户（即贵公司从客服获取的唯一管理员用户）登录本系统，建立各业务板块的管理员用户，并给这些用户分配好权限。</strong><br />
                    <br />
                    <img src="../../../images/number/Num_01.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD),"../HumanManager/EmployeeInfo_Add.aspx?FromPage=7&ModuleID=") %>">
                        建立员工档案</a><br />
                    <br />
                    将需要创建系统操作用户的员工档案建立，可以只填入必须的字段内容，其他非必填字段可留待人力资源板块的负责人员登陆系统后再进一步的完善。
                </p>
                <p>
                    <img src="../../../images/number/Num_02.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_AddRoleInfo),"../Systemmanager/RoleInfo_Edit.aspx?ModuleID=") %>">
                        建立系统操作用户的角色</a>
                    <br />
                    <br />
                    本系统通过角色来分配和管理权限（权限为本系统的各操作菜单或功能按钮等），角色类似于一个岗位或者权限组，先建立角色，并给角色分配好权限，然后将权限赋给系统的操作用户。<br />
                    <br />
                    <img src="../../../images/number/setup_test01.png" /><br />
                    <br />
                    <br />
                    <img src="../../../images/number/Num_03.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_SerchRoleFunction),"../Systemmanager/RoleFunction_Edit.aspx?ModuleID=") %>">
                        给角色分配权限</a>
                    <br />
                    <br />
                    建立好角色以后，还需要给角色分配具体的操作权限。
                    <br />
                    <br />
                    <img src="../../../images/number/Num_04.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_AddUserInfo),"../Systemmanager/UserInfo_Add.aspx?ModuleID=") %>">
                        建立系统操作用户</a>
                    <br />
                    <br />
                    建立系统的操作用户，并指定其关联的员工档案。
                    <br />
                    <br />
                    <img src="../../../images/number/Num_05.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_AddUserRole),"../Systemmanager/UserRole_Add.aspx?ModuleID=") %>">
                        为操作用户关联角色</a>
                    <br />
                    <br />
                    建立好操作用户后，即可给用户关联一个角色，只有关联了角色的操作用户才拥有系统的实际操作权限。
                    <br />
                    <br />
                    <img src="../../../images/number/Num_06.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(2191301,"../Systemmanager/ItemCodingRuleList.aspx?TypeFlag=0&ModuleID=") %>">
                        系统基础设置</a>
                    <br />
                    为了保证贵公司基础数据编码的完整性和一致性，您需要设置各基础数据的编号规则。
                    <br />
                    <br />
                    <strong>二、由负责人力资源管理的用户登录系统，建立组织机构和员工档案<br />
                    </strong><strong></strong>
                    <br />
                    <img src="../../../images/number/Num_01.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_DEPT_EDIT),"../HumanManager/DeptInfo_Info.aspx?ModuleID=") %>">
                        设置公司的组织机构</a><br />
                    <br />
                    对照贵公司的组织机构划分，从上到下逐级添加各个部门的信息。<br />
                    <br />
                    <img src="../../../images/number/Num_02.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%= GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_DEPTQUARTER_EDIT),"../HumanManager/DeptQuarter_Info.aspx?ModuleID=") %>">
                        设置公司的岗位</a><br />
                    <br />
                    根据贵公司各个部门的岗位划分，从上到下逐级添加各个岗位的信息。
                    <br />
                    <br />
                    <img src="../../../images/number/Num_03.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD),"../HumanManager/EmployeeInfo_Add.aspx?FromPage=7&ModuleID=") %>">
                        建立员工档案</a><br />
                    <br />
                    将所有在职员工的档案录入系统，并完善第一部分中所建立的员工档案。<br />
                    详细的人力资源体系构建向导请参阅人力资源管理板块下的向导说明。
                    <br />
                    <br />
                    <strong>三、用管理员用户登录系统，建立各业务模块的操作用户并分配权限</strong><strong></strong><br />
                    操作步骤同第一部分的步骤2至步骤5。
                    <br />
                    <br />
                    <strong>四、各业务板块的管理员用户登录系统，完成各业务模块的初始化工作</strong><strong></strong><br />
                    <br />
                    <img src="../../../images/number/Num_01.jpg" width="26" height="26" align="absmiddle" />
                    各模块的基本设置<br />
                    <br />
                    为了方便单据的快速录入，各模块均提供了基本设置功能，各模块使用之前的准备工作都可在基本设置下完成。<br />
                    需要进行审批的单据，请在基本设置下设置各单据的审批流程。<br />
                    <br />
                    <img src="../../../images/number/Num_02.jpg" width="26" height="26" align="absmiddle" />
                    供应链设置
                    <br />
                    <br />
                    若开通了进销存模块，则必须先进行供应链相关的基础设置，建立物品（商品）档案、仓库档案，请在供应链设置下完成各项设置。
                    <br />
                    <br />
                    若开通了进销存模块（如：采购、销售、库存），则必须先进行供应链相关的基础设置，建立物品（商品）档案、设置业务参数，请在供应链设置以及库存管理下完成各项设置。<br />
                    <span class="Blue">（1） <a href="<%=GetLinks(2081301,"../Systemmanager/CodePublicType.aspx?TypeFlag=5&ModuleID=") %>">
                        基本设置</a></span><br />
                    在供应链设置模块下的基本设置中按以下顺序分别进行设置：<br />
                    分类属性设置 - 物品分类 - 物品特性设置 - 计量单位、计量单位组 - 原因设置 - 费用设置 - 币种类别设置 - 批次规则设置 - 单据扩展属性设置<br />
                    <br />
                    <span class="Blue">（2）<a href="<%=GetLinks(20818,"../Systemmanager/ParameterSetting.aspx?ModuleID=") %>">业务参数设置</a></span><br />
                    根据贵公司的业务情况进行业务规则的设置，业务规则的设置很重要，请仔细设置，有些设置完成后不可再变更。<br />
                    <br />
                    <span class="Blue">（3）<a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_STORAGE_STORAGEINFO),"../StorageManager/StorageInfo.aspx?ModuleID=") %>">建立仓库档案</a></span><br />
                    根据贵公司的仓库分步情况，建立各个仓库的档案。系统要求必须至少建立一个仓库。<br />
                    <br />
                    <span class="Blue">（4）<a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_AddProduct),"../SupplyChain/ProductInfoAdd.aspx?SysModuleID=21915&ModuleID=") %>">建立物品档案</a></span><br />
                    物品（商品）档案是整个进销存系统的最根本数据，可以单个录入，也可以通过模板文件批量导入物品档案到系统中。<br />
                    只有经过确认（审核状态为发布）的物品档案才能被进销存单据调用。<br />
                    <br />
                    <span class="Blue">（5）<a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.Menu_OtherCorpInfo),"../SupplyChain/OtherCorpInfo.aspx?ModuleID=") %>">建立其他往来单位档案</a></span><br />
                    根据贵公司的实际情况，若需要对客户和供应商之外的其他往来单位如：银行、运输商等进行管理，请在往来单位设置下建立往来单位档案。<br />
                    此步骤也可以跳过不进行设置。<br />
                    <br />
                    <span class="Blue">（6）进行库存初始化</span><br />
                    请参见库存管理下的初始化向导。<br />
                    <br />
                    <br />
                    <strong>五、开始正常使用系统<br />
                        <br />
                    </strong>完成以后初始化和准备工作后，即可开始正常使用本系统。
                </p>
            </td>
        </tr>
    </table>
</body>
</html>
