<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseApplyEdit.aspx.cs" Inherits="Pages_Office_PurchaseManager_PurchaseApplyEdit" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/BillChoose.ascx" TagName="BillChoose" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/MaterialChoose.ascx" TagName="MaterialChoose"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/ApplyReason.ascx" TagName="ApplyReason" TagPrefix="uc6" %>

<%@ Register Src="../../../UserControl/UintInfoUC.ascx" TagName="UintInfoUC" TagPrefix="uc8" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeTypeDrpControl" tagprefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改采购申请</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/office/PurchaseManager/PurchaseApplyEdit.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 113px;
        }
        .style2
        {
            width: 113px;
            height: 20px;
        }
        .style3
        {
            height: 20px;
        }
        .style4
        {
            height: 20px;
            width: 275px;
        }
        .style5
        {
            height: 20px;
            width: 272px;
        }
        .style6
        {
            height: 20px;
            width: 263px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        <tr>
            <td valign="top" colspan="2">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" align="center" colspan="2" class="Title">
                <div id="divTitle" runat="server">
                    <uc1:BillChoose ID="BillChoose1" runat="server" />
                    <uc4:MaterialChoose ID="MaterialChoose2" runat="server" />
                    <uc6:ApplyReason ID="ApplyReason1" runat="server" />
                    <uc8:UintInfoUC ID="UintInfoUC1" runat="server" />
                    修改采购申请<uc4:MaterialChoose ID="MaterialChoose3" runat="server" />
                    <uc5:ProviderInfo ID="ProviderInfo1" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td height="40" valign="top" colspan="2">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="30" class="tdColInput">
                            <table>
                                <tr>
                                    <td>
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" id="Add_PurchaseApply"
                                            runat="server" style="cursor: hand; display: none" height="25" onclick="Fun_Clear_Input();" />
                                    </td>
                                    <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" alt="保存" id="save_PurchaseApply"
                                        style="cursor: hand" height="25" onclick="InsertPurchaseApplyData();" />&nbsp;
                                        </td>
                                    <td>
                                    <img src="../../../Images/Button/Bottom_btn_Yd.jpg" alt="源单总览" id="Get_Potential"
                                        onclick="ChooseSourceBill();" style="cursor: hand;" border="0" />
                                        </td>
                                    
                                    <td><img src="../../../Images/Button/Bottom_btn_All.jpg" alt="明细汇总" id="DspDtl_PurchaseApply"
                                        onclick="DspDtl()" style="cursor: hand;" border="0" />
                                        </td>
                                    <td><img src="../../../images/Button/Bottom_btn_Confirm.jpg" alt="确认" id="Img2" onclick="Confirm()"
                                        style="cursor: hand" height="25" border="0" />
                                        </td>
                                    <td><img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" id="btnPrint" style="cursor: hand"
                                        height="25" />
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 500px; overflow-y: scroll;">
                    <table width="98%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                        <tr>
                            <td colspan="2">
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                                <tr>
                                                    <td>
                                                        <span class="Blue">
                                                            <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />基础信息                                   </td>
                                                    <td align="right">
                                                        <div id='divBaseInfo'>
                                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblBaseInfo','divBaseInfo')" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                                    id="tblBaseInfo" style="display: block">
                                    <tr>
                                        <td align="right" bgcolor="#E6E6E6">
                                            申请编号<span class="redbold">*</span>
                                        </td>
                                        <td bgcolor="#FFFFFF">
                                           <%-- <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" />--%>
                                             <input name="CodingRuleControl1_txtCode" id="CodingRuleControl1_txtCode" readonly type="text" class="tdinput" size="15" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6">
                                            申请单主题
                                        </td>
                                        <td bgcolor="#FFFFFF" class="style5">
                                            <input name="txtTitle" id="txtTitle" type="text" class="tdinput" size="15" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6">
                                            采购类别
                                        </td>
                                        <td bgcolor="#FFFFFF">
                                            &nbsp;<uc9:CodeTypeDrpControl ID="ddlTypeID" runat="server" /></td>
                                        <td height="22" bgcolor="#FFFFFF">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#E6E6E6">
                                            申请人
                                        </td>
                                        <td height="20" align="left" bgcolor="#FFFFFF">
                                            <input name="txtApplyUserID" id="txtApplyUserID" readonly onfocus="popSellEmpObj.ShowList('txtApplyUserID','txtApplyDeptID');"
                                                class="tdinput" type="text" /><uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6">
                                            申请部门
                                        </td>
                                        <td height="20" align="left" bgcolor="#FFFFFF" class="style5">
                                            <input name="txtApplyDeptID" readonly id="txtApplyDeptID" class="tdinput" type="text"
                                                size="15" style="background-color: #CCCCCC" />
                                            <asp:HiddenField ID="txtHiddenFieldID" runat="server" />
                                            <asp:HiddenField ID="DtlCount" runat="server" Value="0" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6">
                                            源单类型
                                        </td>
                                        <td align="left" bgcolor="#FFFFFF">
                                            &nbsp;
                                            <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType" onchange="DeleteAll();">
                                                <option value="0" selected="selected">无来源</option>
                                                <option value="1">销售订单</option>
                                                <option value="2">物料资源计划</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" bgcolor="#E6E6E6">
                                            申请日期
                                        </td>
                                        <td height="20" align="left" bgcolor="#FFFFFF">
                                            <input name="txtApplyDate" id="txtApplyDate" class="tdinput" type="text" size="15"
                                                readonly onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" />
                                        </td>
                                        <td height="20" align="right" bgcolor="#E6E6E6">
                                            到货地址
                                        </td>
                                        <td height="20" bgcolor="#FFFFFF" colspan="5">
                                            <input name="txtAddress" id="txtAddress" class="tdinput" type="text" size="15" style="width: 100%" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="10">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                            </td>
                            <td align="right" valign="top">
                                <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" valign="top">
                                <span class="Blue">
                                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />备注信息
                                </span>
                            </td>
                            <td align="right" valign="top">
                                <div id='divWorkHistory'>
                                    <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('tblWorkInfo','divWorkHistory')" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="99%" border="0" id="tblWorkInfo" style="behavior: url(../../../draggrid.htc)"
                                    align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td align="right" bgcolor="#E6E6E6" class="style1">
                                            制单人
                                        </td>
                                        <td bgcolor="#FFFFFF" class="style6">
                                            <input name="txtCreator" id="txtCreator" class="tdinput" type="text" size="15" runat="server"
                                                readonly="readonly" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6" class="style4">
                                            制单日期
                                        </td>
                                        <td align="left" bgcolor="#FFFFFF">
                                            <input name="txtCreateDate" id="txtCreateDate" class="tdinput" type="text" size="15"
                                                runat="server" readonly="readonly" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6">
                                            单据状态
                                        </td>
                                        <td align="left" bgcolor="#FFFFFF">
                                            &nbsp;
                                            <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus" disabled="disabled">
                                                <option value="1" selected="selected">制单</option>
                                                <option value="2">执行</option>
                                                <option value="3">变更</option>
                                                <option value="4">手工结单</option>
                                                <option value="5">自动结单</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#E6E6E6" class="style2">
                                            确认人
                                        </td>
                                        <td bgcolor="#FFFFFF" class="style6">
                                            <input name="txtConfirmor" id="txtConfirmor" class="tdinput" type="text" size="15"
                                                readonly="readonly" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6" class="style3">
                                            确认日期
                                        </td>
                                        <td align="left" bgcolor="#FFFFFF" class="style3">
                                            <input id="txtConfirmDate" class="tdinput" type="text" size="15" readonly="readonly" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6" class="style3">
                                            最后更新人
                                        </td>
                                        <td bgcolor="#FFFFFF" class="style3">
                                            <input name="txtModifiedUserID" id="txtModifiedUserID" class="tdinput" type="text"
                                                size="15" runat="server" readonly="readonly" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#E6E6E6" class="style1">
                                            结单人
                                        </td>
                                        <td bgcolor="#FFFFFF" class="style6">
                                            <input name="txtCloser" id="txtCloser" class="tdinput" type="text" size="15" readonly="readonly" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6" class="style4">
                                            结单日期
                                        </td>
                                        <td align="left" bgcolor="#FFFFFF">
                                            <input name="txtCloseDate" id="txtCloseDate" class="tdinput" type="text" size="15"
                                                readonly="readonly" />
                                        </td>
                                        <td align="right" bgcolor="#E6E6E6">
                                            最后更新日期
                                        </td>
                                        <td align="left" bgcolor="#FFFFFF">
                                            <input name="txtModifiedDate" id="txtModifiedDate" class="tdinput" type="text" size="15"
                                                runat="server" readonly="readonly" />
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#E6E6E6" height="20" class="style1">
                                            备注
                                        </td>
                                        <td height="20" style="width: 200px;" colspan="5" bgcolor="#FFFFFF">
                                            <textarea name="txtRemark" style="width: 700px; margin-bottom: 0px;" id="txtRemark"
                                                class="tdinput"></textarea>
                                            <input id="txtCpntrolID" type="text" type="hidden" value="User|txtApplyUserID,Dept|txtApplyDeptID"
                                                style="display: none" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="style7">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                            </td>
                            <td align="right" valign="top">
                                <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" valign="top">
                                <span class="Blue">
                                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />采购申请明细来源</span>
                            </td>
                            <td align="right" valign="top">
                                <div id='divStudyHistory'>
                                    <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('mingx','divStudyHistory')" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="mingx" width="99%" border="0">
                                    <tr>
                                        <td height="28" class="tdColInput">
                                            <img src="../../../images/Button/Show_add.jpg" width="34" height="24" alt="添加" style="cursor: hand"
                                                onclick="AddSignRow();" />
                                            <img src="../../../images/Button/Show_del.jpg" width="34" height="24" alt="删除" style="cursor: hand"
                                                onclick="DelRow();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="99%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc)"
                                                align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                                <tr>
                                                    <td height="20" align="center" bgcolor="#E6E6E6" class="Blue">
                                                        序号
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="selectall()"
                                                            value="checkbox" />
                                                    </td>
                                                    <td witdh="10%" align="center" bgcolor="#E6E6E6" style="display: none" class="Blue">
                                                        物品ID
                                                    </td>
                                                    <td witdh="10%" align="center" bgcolor="#E6E6E6" class="Blue">
                                                        物品编号
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        物品名称
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" style="display: none" class="Blue">
                                                        单位ID
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        单位
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        单价
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        需求数量
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        申请数量
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        金额
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        需求日期
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        申请交货日期
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" style="display: none" class="Blue">
                                                        来源部门ID
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        来源部门
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        来源单据
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        来源单据行号
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" style="display: none" class="Blue">
                                                        供应商ID
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        供应商
                                                    </td>
                                                    <td align="center" bgcolor="#E6E6E6" class="Blue">
                                                        备注
                                                    </td>
                                                    <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
                                                    <input name='txtTRLastIndex2' type='hidden' id='txtTRLastIndex2' value="1" />
                                                    <input name='datetemp' type='hidden' id='datetemp' runat="server" />
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                            </td>
                            <td align="right" valign="top">
                                <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                            </td>
                            <input name='usernametemp' type='hidden' id='usernametemp' runat="server" />
                        </tr>
                        <tr>
                            <td height="25" valign="top">
                                <span class="Blue">
                                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />采购申请明细</span>
                            </td>
                            <td align="right" valign="top">
                                <div id='divSkill'>
                                    <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('dg_Log2','divSkill')" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="99%" border="0" id="dg_Log2" style="behavior: url(../../../draggrid.htc)"
                                    align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="20" align="center" class="Blue" bgcolor="#E6E6E6" style="width: 30px">
                                            序号
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 80">
                                            物品编号
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 80">
                                            物品名称
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 40px">
                                            单位
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 80">
                                            申请数量
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 40">
                                            需求日期
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 80">
                                            申请原因
                                        </td>
                                        <td align="center" bgcolor="#E6E6E6" class="Blue" style="width: 80">
                                            备注
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
