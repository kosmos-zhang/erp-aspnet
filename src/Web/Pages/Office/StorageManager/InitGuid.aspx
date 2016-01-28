<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitGuid.aspx.cs" Inherits="Pages_Office_StorageManager_InitGuid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>库存初始化</title>
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
                    <img src="../../../images/number/logo_kc.jpg" width="380" height="65" /><strong>
                        <br />
                        <br />
                    </strong><span class="redbold">初次使用库存管理系统，请参照以下步骤进行库存初始化。</span></p>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <img src="../../../images/number/Num_01.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(2052101,"../SystemManager/CodePublicType.aspx?TypeFlag=9&ModuleID=") %>">基本设置</a><br />
                    为了方便单据的快速录入，库存管理系统提供了基本设置功能，库存管理系统所涉及到的公共分类属性（下拉列表选项）以及单据的编号规则统一在这里设置。<br />
                    <br />
                    <img src="../../../images/number/Num_02.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_STORAGE_STORAGEINFO),"../StorageManager/StorageInfo.aspx?ModuleID=") %>">建立仓库档案</a><br />
                    在库存档案下进行设置。<br />
                    仓库是库存管理的基本数据对象，所有的库存管理都是围绕物品（商品）以及仓库进行的。<br />
                    请根据贵公司的仓库情况建立库存档案，系统要求必须至少建立一个仓库。<br />
                    <br />
                    <img src="../../../images/number/Num_03.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(int.Parse(XBase.Common.ConstUtil.MODULE_ID_STORAGE_STORAGEINITAIL_ADD),"../StorageManager/StorageInitailAdd.aspx?ModuleID=") %>">期初库存录入</a><br />
                    首次使用系统时，应盘点核对清楚所有需要进行管理的物品（商品）的库存存量，并录入系统，作为系统的期初库存。<br />
                    期初库存可以逐条物品（商品）录入，也可以批量导入。<br />
                    请在库存档案下完成期初库存录入。<br />
                    <br />
                    <img src="../../../images/number/Num_04.jpg" width="26" height="26" align="absmiddle" />
                    <a href="<%=GetLinks(2052103,"../SystemManager/ApprovalFlowSet.aspx?TypeFlag=8&ModuleID=") %>">设置审批流程</a><br />
                    库存管理中有些单据若需要进行审批管理，则需要设置好相应的审批流程。<br />
                </p>
            </td>
        </tr>
    </table>
</body>
</html>
