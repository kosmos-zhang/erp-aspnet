<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageQualityCheckAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageQualityCheckAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/StorageQualityManufacture.ascx" TagName="StorageQualityManufacture"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/StorageQualityPurchaseArrive.ascx" TagName="StorageQualityPurchaseArrive"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建质检申请单</title>
    <style type="text/css">
        .AddInputTd
        {
            width: 8%;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageQualityCheckAdd.js" type="text/javascript"></script>

    <script type="text/javascript">
    var intMasterProductID = '<%=intMasterProductID %>';
    var glb_BillTypeFlag ='<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_QUALITY %>';
    var glb_BillTypeCode ='<%=XBase.Common.ConstUtil.BILL_TYPECODE_STORAGE_QUALITYADD %>';
    var glb_BillID = intMasterProductID;//单据ID 
    var FlowJS_HiddenIdentityID ='hiddeniqualityd';
    var FlowJS_BillStatus ='ddlBillStatus';
    var glb_IsComplete = true;   
    var FlowJs_BillNo='lbInNo';
    var myFrom='<%=myFrom %>';

    var isMoreUnit = <%= IsMoreUnit.ToString().ToLower() %>;// 多计量单位控制参数
    var selPoint = <%= SelPoint %>;// 小数位数
    
    </script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <input id="isupdate" type="hidden" value="0" /><!--判断单据是否被其他的单据引用 修改需要-->
    <input id="isflow" type="hidden" value="0" /><!---判断制单状态的单据是否提交审批-->
    <input type="hidden" id="hiddeniqualityd" name="hiddeniqualityd" value="0" runat="server" />
    <uc1:Message ID="Message1" runat="server" />
    <uc4:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" />
    <uc3:StorageQualityManufacture ID="StorageQualityManufacture1" runat="server" />
    <uc6:StorageQualityPurchaseArrive ID="StorageQualityPurchaseArrive1" runat="server" />
    <uc5:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc8:FlowApply ID="FlowApply1" runat="server" />
    <div style="display: none">
        <asp:DropDownList runat="server" ID="ddlStorageInfo">
        </asp:DropDownList>
    </div>
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="addQuality">
                    <tr>
                        <td height="30" align="center" class="Title">
                            新建质检申请单
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="editQuality" style="display: none">
                    <tr align="center">
                        <td height="30" align="center" class="Title">
                            质检申请单
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                                        alt="保存" id="btnSave" style="cursor: hand" border="0" onclick="SaveStorageQualityInfo();" />
                                                </td>
                                                <td>
                                                    <span runat="server" visible="false" id="GlbFlowButtonSpan"></span>
                                                </td>
                                                <td>
                                                    <img style="display: none" onclick="BackToPage();" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
                                                        border="0" />
                                                </td>
                                    </td>
                                </tr>
                        </td>
                    </tr>
                </table>
            </td>
            <td bgcolor="#FFFFFF" align="right">
                <img onclick="PrintQua();" src="../../../images/Button/Main_btn_print.jpg" id="btn_print" />
            </td>
        </tr>
    </table>
    </td> </tr> </table> </td> </tr>
    <tr>
        <td colspan="2" valign="top">
            <table width="99%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="6">
                    </td>
                </tr>
            </table>
            <div style="overflow-y: auto;">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            质检申请单编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <div runat="server" id="div_InNo_uc">
                                <uc2:CodingRuleControl ID="txtInNo" runat="server" />
                            </div>
                            <div id="div_InNo_Lable" runat="server" style="display: none;">
                                <input runat="server" readonly type="text" class="tdinput" id="lbInNo" />
                            </div>
                            <input type="hidden" id="txtInNoHidden" value="0" name="txtInNoHidden" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            质检申请主题
                        </td>
                        <td bgcolor="#FFFFFF" width="23%" class="tdColInput">
                            <input specialworkcheck="质检申请主题" name="txtTitle" id="txtTitle" type="text" class="tdinput"
                                style="width: 95%" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            源单类型<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF" width="24%" class="tdColInput">
                            <select id="ddlFromType" onchange="ChangeValue();">
                                <option value="1">采购到货单</option>
                                <option value="0">无来源</option>
                                <option value="2">生产任务单</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            往来单位
                        </td>
                        <td withd="23%" height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <input name="txtCustNameNo" readonly id="txtCustNameNo" type="text" class="tdinput"
                                onclick="SelectCust();" />
                            <input id="hiddenCustID" value="0" type="hidden" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            往来单位大类
                        </td>
                        <td width="23%" height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <input name="CustBigType" id="CustBigType" type="text" readonly class="tdinput" />
                            <input id="hiddenCustBigType" type="hidden" value="0" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            质检类别<span class="redbold">*</span>
                        </td>
                        <td width="24%" height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <select id="CheckType">
                                <option value="1">进货检验</option>
                                <option value="2">过程检验</option>
                                <option value="3">最终检验</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            生产负责人
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <input readonly name="UserPrincipal" id="UserPrincipal" class="tdinput" onclick="SelectPri();"
                                type="text" />
                            <input id="txtPeople" type="hidden" value="0" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            生产部门
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <input name="DeptName" readonly id="DeptName" type="text" onclick="SelectDept1();"
                                class="tdinput" />
                            <input id="txtDep" type="hidden" value="0" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            检验方式<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <select id="CheckMode">
                                <option value="1">全检</option>
                                <option value="2">抽检</option>
                                <option value="3">临检</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            报检人员<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="tdColInput">
                            <input name="UserChecker" id="UserChecker" onclick="alertdiv('UserChecker,txtChecker');"
                                readonly class="tdinput" type="text" />
                            <input id="txtChecker" type="hidden" value="0" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            报检部门<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <input name="DeptCheckDept" id="DeptCheckDept" class="tdinput" title="0" type="text"
                                onclick="alertdiv('DeptCheckDept,CheckDeptId');" size="15" readonly="readonly" />
                            <input id="CheckDeptId" type="hidden" value="0" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            报检日期
                        </td>
                        <td bgcolor="#FFFFFF">
                            <asp:TextBox runat="server" name="txtEnterDate" ID="txtEnterDate" class="tdinput"
                                ReadOnly="true" type="text" size="15" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})" />
                        </td>
                    </tr>
                </table>
                <uc7:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td width="10%">
                                        合计信息
                                    </td>
                                    <td width="90%" align="right">
                                        <div id='Div1'>
                                            <img src="../../../images/Main/Open.jpg" style="cursor: pointer" onclick="oprItem('Table1','Div1')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Table1">
                    <tr>
                        <td width="10%" align="right" bgcolor="#E6E6E6">
                            报检数量合计
                        </td>
                        <td width="90%" bgcolor="#FFFFFF">
                            <input id="CountTotal1" onchange="Number_round(this,<%=SelPoint %>);" type="text"
                                disabled class="tdinput" style="width: 100%" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        备注信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick5'>
                                            <img src="../../../images/Main/Open.jpg" style="cursor: pointer" onclick="oprItem('Tb_031','searchClick5')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Tb_031">
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            单据状态
                        </td>
                        <td width="23%" bgcolor="#FFFFFF" class="style1">
                            <input id="tbBillStatus" disabled type="text" value="制单" class="tdinput" />
                            <input id="ddlBillStatus" type="hidden" value="1" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            制单人
                        </td>
                        <td width="23%" bgcolor="#FFFFFF" class="style1">
                            <asp:TextBox ID="tbCreator" class="tdinput" runat="server" size="15" Enabled="false"></asp:TextBox>
                            <input id="txtCreator" runat="server" type="hidden" value="0">
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            制单日期
                        </td>
                        <td width="24%" bgcolor="#FFFFFF" class="style1">
                            <asp:TextBox ID="txtCreateDate" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <div id="divConfirmor" style="display: none">
                                <asp:TextBox ID="tbConfirmor" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                                <input runat="server" id="txtConfirmor" type="hidden" value="0" /></div>
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <div id="divConfirmDate" style="display: none">
                                <asp:TextBox ID="txtConfirmDate" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox></div>
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            结单人
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <div id="divCloser" style="display: none">
                                <asp:TextBox ID="tbCloser" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                                <input id="txtCloser" runat="server" value="0" type="hidden" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <div id="divCloseDate" style="display: none">
                                <asp:TextBox ID="txtCloseDate" CssClass="tdinput" Enabled="false" runat="server"></asp:TextBox></div>
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <asp:TextBox ID="tbModifiedUserID" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            <input id="txtModifiedUserID" runat="server" value="0" type="hidden" />
                        </td>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            最后更新时间
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <asp:TextBox ID="txtModifiedDate" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            备注
                        </td>
                        <td height="20" colspan="5" bgcolor="#FFFFFF">
                            <textarea name="txtRemark" id="txtRemark" style="width: 99%" class="tdinput" cols="50"
                                rows="5"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" height="20" align="right" class="tdColTitle">
                            附件
                        </td>
                        <td width="90%" height="20" colspan="5" bgcolor="#FFFFFF">
                            <input id="hfPageAttachment" type="hidden" />
                            <div id="divUploadAttachment" runat="server">
                                <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                            </div>
                            <div id="divDealAttachment" runat="server" style="display: none;">
                                <table>
                                    <tr>
                                        <td>
                                            <a href="#" onclick="DealAttachment('download');">
                                                <div id="attachname">
                                                </div>
                                            </a>
                                        </td>
                                        <td>
                                            <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
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
                                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />质检单明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick4'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('dg_Log','searchClick4')" /></div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img runat="server" visible="false" id="btn_add" src="../../../images/Button/UnClick_tj.jpg"
                                style="cursor: hand;" onclick="AddOneNewRowNO();" />&nbsp;
                            <img id="DelDetail" runat="server" visible="false" src="../../../images/Button/Show_del.jpg"
                                style="cursor: hand" onclick="DeleteSignRow();" />
                            <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_From.jpg"
                                id="btn_select" onclick="ChangeForDetail();" style="cursor: hand" />
                            <img runat="server" src="../../../images/Button/Unclick_From.jpg" id="unbtn_select"
                                style="cursor: hand; display: none" />
                            <img alt="条码扫描" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods" visible="false"
                                style="cursor: pointer; display: none" onclick="GetGoodsInfoByBarCode()" runat="server" />
                            <img alt="条码扫描" visible="false" runat="server" src="../../../Images/Button/btn_tmsmu.jpg"
                                id="unbtnGetGoods" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" id="dg_Log" align="center" cellpadding="0" cellspacing="1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                选择<input type="checkbox" visible="false" name="checkall" id="checkall" onclick="selectall()"
                                    value="checkbox" />
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                物品名称<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                物品编号
                            </td>
                            <% if (IsMoreUnit)
                               {%>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                基本单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                基本数量
                            </td>
                            <% } %>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                报检数量<span class="redbold">*</span>
                            </td>
                            <td id="QuaInCount" style="display: none" align="center" bgcolor="#E6E6E6" class="ListTitle"
                                valign="middle">
                                生产数量<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                已报检数量
                            </td>
                            <td id="PurCheckedCount" style="width: 8%" align="center" bgcolor="#E6E6E6" class="ListTitle"
                                valign="middle">
                                已检数量
                            </td>
                            <td id="QuaCheckedCount" align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle"
                                style="width: 8%; display: none">
                                到货数量
                            </td>
                            <td id="QuaFromBillNo" style="display: none" align="center" bgcolor="#E6E6E6" class="ListTitle"
                                valign="middle">
                                源单编号
                            </td>
                            <td id="QuaFromLineNo" style="display: none" align="center" bgcolor="#E6E6E6" class="ListTitle"
                                valign="middle">
                                源单行号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                备注
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
            <input type="hidden" id="hidNoID" runat="server" />
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
