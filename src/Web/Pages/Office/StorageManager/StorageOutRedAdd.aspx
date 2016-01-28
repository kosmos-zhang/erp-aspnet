<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageOutRedAdd.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageOutRedAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>红冲出库单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UnitGroup.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <input type="hidden" id="IsDisplayPrice" runat="server" value="" />
    <uc3:StorageSnapshot ID="StorageSnapshot1" runat="server" />
    <input id="HiddenPoint" type="hidden" runat="server" />
    <input id="HiddenMoreUnit" type="hidden" runat="server" />
    <input id="HiddenNow" runat="server" type="hidden" />
    <input id="HiddenIsZero" runat="server" type="hidden" />
    <div id="popupContent">
    </div>
    <div style="display: none">
        <asp:DropDownList runat="server" ID="ddlStorageInfo">
        </asp:DropDownList>
    </div>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Add" style="display: none">
                    <tr>
                        <td height="30" align="center" class="Title">
                            新建红冲出库单
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="t_Edit" style="display: none">
                    <tr>
                        <td height="30" align="center" class="Title">
                            红冲出库单
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" runat="server"
                                            style="cursor: pointer" border="0" onclick="Fun_Save_StorageOutRed();" visible="false" />
                                        <img src="../../../Images/Button/UnClick_bc.jpg" alt="保存" id="btn_UnClick_bc" runat="server"
                                            style="display: none; cursor: pointer;" border="0" visible="false" />
                                        <img src="../../../images/Button/Bottom_btn_Confirm.jpg" id="Confirm" runat="server"
                                            alt="确认" onclick="ConfirmBill()" style="display: none; cursor: pointer" border="0"
                                            visible="false" />
                                        <img id="btnPageFlowConfrim" runat="server" src="../../../images/Button/UnClick_qr.jpg"
                                            alt="确认" style="display: none;" border="0" visible="false" />
                                        <img src="../../../images/Button/Main_btn_Invoice.jpg" id="btn_Close" runat="server"
                                            alt="结单" onclick="CloseBill()" style="display: none; cursor: pointer" border="0"
                                            visible="false" />
                                        <img id="btn_Unclic_Close" runat="server" src="../../../images/Button/Button_jd.jpg"
                                            alt="结单" style="display: none;" border="0" visible="false" />
                                        <img src="../../../images/Button/Main_btn_qxjd.jpg" id="btn_CancelClose" runat="server"
                                            alt="取消结单" onclick="CancelCloseBill()" style="display: none; cursor: pointer"
                                            border="0" visible="false" />
                                        <img id="btn_Unclick_CancelClose" runat="server" src="../../../images/Button/Button_qxjd.jpg"
                                            alt="取消结单" style="display: none;" border="0" visible="false" />
                                        <input type="hidden" id="txtIndentityID" name="txtIndentityID" value="0" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" runat="server" id="btnBack"
                                            visible="false" onclick="DoBack();" style="cursor: hand" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="right">
                                        <img src="../../../images/Button/Main_btn_print.jpg" title="打印" id="btnPrint" runat="server"
                                            style="cursor: pointer" onclick="BillPrint();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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
                                            基础信息
                                        </td>
                                        <td align="right">
                                            <div id='div11'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','div11')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_01" >
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                出库单编号<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <div runat="server" id="div_OutNo_uc">
                                    <uc2:CodingRuleControl ID="txtOutNo" runat="server" />
                                </div>
                                <div id="div_OutNo_Lable" runat="server" style="display: none;">
                                    <asp:Label runat="server" ID="lbOutNo" Text=""></asp:Label>
                                </div>
                                <%--<input name="txtInNo" id="txtInNo" type="text" class="tdinput" size="15" onblur="checkonly();" />--%>
                                <input type="hidden" id="txtOutNoHidden" name="txtOutNoHidden" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                出库单主题
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTitle" id="txtTitle" maxlength="100" specialworkcheck="出库单主题" type="text"
                                    maxlength="100" class="tdinput" size="15" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                源单类型<span class="redbold">*</span>
                            </td>
                            <td bgcolor="#FFFFFF" width="24%">
                                <asp:DropDownList ID="ddlFromType" runat="server">
                                    <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="销售出库单"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="其他出库单"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<select id="fsf" onchange="">
                                    <option value=""></option>
                                </select>--%>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始出库单<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtFromBillID" id="txtFromBillID" class="tdinput" type="text" size="15"
                                    title="" readonly="readonly" onclick="fnOutRed()" style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始出库部门
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtFromOutDept" id="txtFromOutDept" class="tdinput" type="text" size="15"
                                    title="" disabled="disabled" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始出库人
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:TextBox runat="server" name="txtFromOuter" ID="txtFromOuter" class="tdinput"
                                    type="text" size="15" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始出库时间
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="txtFromOutDate" id="txtFromOutDate" class="tdinput" type="text" size="15"
                                    title="" disabled="disabled" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                原始摘要
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:TextBox runat="server" name="txtFromSummary" ID="txtFromSummary" class="tdinput"
                                    type="text" size="15" disabled="disabled" Style="width: 95%" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                出库原因
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:DropDownList ID="ddlReason" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                出库部门
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input name="DeptName" id="DeptName" type="text" class="tdinput" size="19" readonly="readonly"
                                    onclick="alertdiv('DeptName,txtDeptID');" style="width: 95%" />
                                <input name="txtDeptID" id="txtDeptID" type="hidden" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                出库人<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <input id="UserTransactor" runat="server" class="tdinput" name="UserTransactor" onclick="alertdiv('UserTransactor,txtTransactorID');"
                                    readonly="readonly" size="19" type="text" style="width: 95%" />
                                <input id="txtTransactorID" runat="server" name="txtTransactorID" type="hidden" />
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                出库时间<span class="redbold">*</span>
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                                <asp:TextBox runat="server" name="txtOutDate" ID="txtOutDate" class="tdinput" type="text"
                                    size="15" ReadOnly="true" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOutDate')})"
                                    Style="width: 95%" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                摘要
                            </td>
                            <td bgcolor="#FFFFFF">
                                <input name="txtSummary" id="txtSummary" class="tdinput" type="text" size="15" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                            </td>
                            <td bgcolor="#FFFFFF">
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                            </td>
                            <td bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                        <td height="22" align="right" bgcolor="#E6E6E6">可查看该单据信息的人员</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
              </tr>
                    </table>
                    <uc4:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" />
                    <%---------------------------------------Start弹出层Div-----------------------------------------%>
                    <div id="PurchaseRejectdiv">
                        <a name="pageOffDataList1Mark"></a>
                        <div id="divzhezhao" style="filter: Alpha(opacity=0); width: 740px; height: 500px;
                            z-index: 1000; position: absolute; display: none; top: 20%; left: 60%; margin: 5px 0 0 -400px;">
                            <iframe style="border: 0; width: 700px; height: 100%; position: absolute;"></iframe>
                        </div>
                        <!--提示信息弹出详情start-->
                        <div id="divOutType" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
                            width: 700px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
                            margin: 5px 0 0 -400px; overflow-x: auto;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img alt="关闭" id="Img1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                                            onclick='closeOutTypediv();' />
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                            class="table">
                                            <tr class="table-item">
                                                <td width="10%" height="20" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                                    单据编号
                                                </td>
                                                <td width="24%" bgcolor="#FFFFFF">
                                                    <input name="txtNo_UC" id="txtNo_UC" type="text" class="tdinput" runat="server" size="13"
                                                        specialworkcheck="单据编号" style="width: 95%" />
                                                </td>
                                                <td width="10%" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                                    单据主题
                                                </td>
                                                <td width="23%" bgcolor="#FFFFFF">
                                                    <input name="txtTitle_UC" id="txtTitle_UC" type="text" class="tdinput" runat="server"
                                                        size="19" specialworkcheck="单据主题" style="width: 95%" />
                                                </td>
                                                <td width="10%" bgcolor="#E7E7E7" align="right" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" bgcolor="#FFFFFF">
                                                    <img alt="检索" id="btn_Serch" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                        onclick='UCSearch();' />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="offerDataList"
                                bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            选择
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div onclick="OrderOffBy('FromType','oGroupOff');return false;">
                                                出库类型<span id="oGroupOff"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div onclick="OrderOffBy('OutNo','oGroupOff');return false;">
                                                出库编号<span id="Span1"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div onclick="OrderOffBy('Title','oCOff1');return false;">
                                                主题<span id="oCOff1"></span></div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div onclick="OrderOffBy('CountTotle','oCOff7');return false;">
                                                出库数量<span id="oCOff7"></span></div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                                class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                                    <div id="pageOffcount">
                                                    </div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="pageOffList_Pager" class="jPagerBar">
                                                    </div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divOffpage">
                                                        <span id="pageOffList_Total"></span>每页显示
                                                        <input name="text" type="text" id="ShowOffPageCount" style="width: 20px;" />
                                                        条 转到第
                                                        <input name="text" type="text" style="width: 20px;" id="OffToPage" />
                                                        页
                                                        <img src="../../../Images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                            width="36" height="28" align="absmiddle" onclick="ChangeOffPageCountIndex($('#ShowOffPageCount').val(),$('#OffToPage').val());" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                        <!--提示信息弹出详情end-->
                    </div>
                    <%---------------------------------------End弹出层Div-------------------------------------------%>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            合计信息
                                        </td>
                                        <td align="right">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_02" >
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                红冲数量合计
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalCount" id="txtTotalCount" type="text" class="tdinput" size="15"
                                    disabled="disabled" value="0.00" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                <span id="TotalMoneyFlag">红冲金额合计</span>
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <input name="txtTotalPrice" id="txtTotalPrice" type="text" class="tdinput" size="15"
                                    disabled="disabled" value="0.00" style="width: 95%" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                            </td>
                            <td bgcolor="#FFFFFF" width="24%">
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
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        id="Tb_03" >
                        <tr>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                制单人
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <asp:TextBox ID="txtCreator" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                                <%--<input name="txtCreator" id="txtCreator" type="text" class="tdinput" size="15" />--%>
                            </td>
                            <td align="right" bgcolor="#E6E6E6" width="10%">
                                制单日期
                            </td>
                            <td bgcolor="#FFFFFF" width="23%">
                                <asp:TextBox ID="txtCreateDate" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6" width="10%">
                                单据状态
                            </td>
                            <td bgcolor="#FFFFFF">
                                <%--<asp:TextBox ID="txtBillStatus" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>--%>
                                <select name="sltBillStatus" class="tdinput" width="119px" id="sltBillStatus" disabled="disabled">
                                    <option value="1">制单</option>
                                    <option value="2">执行</option>
                                    <option value="3">变更</option>
                                    <option value="4">手工结单</option>
                                    <option value="5">自动结单</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                确认人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtConfirmor" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                确认日期
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtConfirmDate" runat="server" class="tdinput" size="15" Enabled="false"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                结单人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <%--<input name="txtCloser" id="txtCloser" type="text" class="tdinput" size="15" />--%>
                                <asp:TextBox ID="txtCloser" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#E6E6E6">
                                结单日期
                            </td>
                            <td bgcolor="#FFFFFF">
                                <input name="txtCloseDate" id="txtCloseDate" type="text" class="tdinput" size="15" />
                            </td>
                            <td align="right" bgcolor="#E6E6E6">
                                最后更新人
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtModifiedUserID" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                最后更新时间
                            </td>
                            <td bgcolor="#FFFFFF">
                                <asp:TextBox ID="txtModifiedDate" runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" bgcolor="#E6E6E6">
                                备注
                            </td>
                            <td height="20" colspan="5" bgcolor="#FFFFFF">
                                <textarea name="txtRemark" id="txtRemark" class="tdinput" cols="50" rows="5"></textarea>
                                
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
                                &nbsp;<span class="Blue"> 出库单明细</span>
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
                                <%--<img src="../../../images/Button/Show_add.jpg" width="34" height="24" style="cursor: hand"
                                    onclick="AddRow();" />&nbsp;--%>&nbsp;
                                                <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" /></td>
                        </tr>
                    </table>
                    <table width="99%" border="0" id="dg_Log" style="behavior: url(../../../draggrid.htc)"
                        align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 4%">
                                <input type="checkbox" visible="false" name="checkall" id="checkall" onclick="SelectAll()"
                                    value="checkbox" />
                            </td>
                            <td height="20" align="center" bgcolor="#E6E6E6" style="width: 4%">
                                序号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                物品编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                物品名称
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle"  style="width: 10%">
                                批次
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%">
                                规格
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 3%;display:none" id="baseuint">
                                基本单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 5%;display:none" id="basecount">
                                基本数量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 5%">
                                单位
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                仓库
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%">
                                原始出库量
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 6%">
                                红冲数量<span class="redbold">*</span>
                            </td>
                            <td align="center" bgcolor="#E6E6E6" id="td_outprict" class="ListTitle" style="width: 6%">
                                红冲单价
                            </td>
                            <td align="center" bgcolor="#E6E6E6" id="td_outmoney" class="ListTitle" style="width: 6%">
                                红冲金额
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 8%">
                                源单编号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 5%">
                                源单行号
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                备注
                            </td>
                        </tr>
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
            </td>
        </tr>
    </table>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />

    <script src="../../../js/office/StorageManager/StorageOutRedAdd.js" type="text/javascript"></script>

    </form>
</body>
</html>
