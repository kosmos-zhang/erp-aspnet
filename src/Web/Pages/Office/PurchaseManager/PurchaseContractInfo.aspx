<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseContractInfo.aspx.cs" Inherits="Pages_Office_PurchaseManager_PurchaseContractInfo" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/ProviderSelect.ascx" tagname="ProviderSelect" tagprefix="uc2" %>

<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/PurchaseManager/PurchaseContractInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <title>采购合同列表</title>
</head>
<body>
    <form id="form1" runat="server">
         <input id="HiddenPoint" type="hidden" runat="server" />
    <uc1:Message ID="Message1" runat="server" />
    <uc2:ProviderSelect ID="ProviderSelect1" runat="server" />
    <!-- 销售订单-->
    <!-- 销售订单-->
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        合同编号
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtContractNo" MaxLength="50" runat="server" CssClass="tdinput" Width="95%" SpecialWorkCheck="合同编号" ></asp:TextBox>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        合同主题
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtTitle" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="合同主题" ></asp:TextBox>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        采购类别
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <select name="DrpTypeID"  class="tdinput" runat="server" id="DrpTypeID"> </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        部门
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="DeptDeptID" onclick="alertdiv('DeptDeptID,txtHidOurDept');" runat="server" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                     <input type="hidden" id="txtHidOurDept" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        采购员
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="UsertxtSeller" runat="server" onclick="alertdiv('UsertxtSeller,HidSeller');" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="HidSeller" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        源单类型
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType"  runat="server" >
                                        <option value="-1" selected="selected">--请选择--</option>
                                        <option value="0">无来源</option>
                                        <option value="1">采购申请</option>
                                        <option value="2">采购计划</option>
                                        <option value="3">采购询价单</option> </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        供应商
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="txtProviderID"  runat="server" CssClass="tdinput" Width="95%" onclick ="popProviderObj.ShowList()"  Readonly></asp:TextBox>
                                        <input type="hidden" id="txtHidProviderID" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据状态
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus" runat="server" >
                                         <option value="0" selected="selected">--请选择--</option>
                                         <option value="1">制单</option>
                                         <option value="2">执行</option>
                                         <option value="4">手工结单</option>
                                         <option value="5">自动结单</option> </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        审批状态
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <select name="ddlUsedStatus" class="tdinput" width="119px" id="ddlUsedStatus" runat="server" >
                                        <option value="0" selected="selected">--请选择--</option>
                                        <option value="-1">待提交</option>
                                        <option value="1">待审批</option>
                                        <option value="2">审批中</option>
                                        <option value="3">审批通过</option>
                                        <option value="4">审批不通过</option>
                                        <option value="5">撤消审批</option> </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                      <span id="OtherConditon" style=" display:none">其他条件</span>
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                     
                                        <uc3:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                     
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                            
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                         
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                             
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                    <input type="hidden" id="hidModuleID" runat="server" />
                                    <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                           visible="false" runat="server"  onclick='SearchContractData()'/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                采购合同列表   </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style="cursor:hand;" onclick="CreatePurchaseContract()"  runat="server" visible="false" />
                            <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style="cursor:hand;" onclick="DelContract()"  runat="server" visible="false" />
                            <%--<img id="btnImport" src="../../../images/Button/Main_btn_out.jpg" alt="导出Excel"  runat="server" onclick="btnImport_Click" />--%>
                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                            <%--<img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" alt="打印" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="tdResult">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" visible="false" id="checkall" onclick="SelectAll()" value="checkbox" /></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ContractNo','oContractNo');return false;">合同编号<span id="oContractNo" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('Title','oTitle');return false;">合同主题<span id="oTitle" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TypeName','oTypeName');return false;">采购分类<span id="oTypeName" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">采购员<span id="oEmployeeName" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">供应商<span id="oCustName" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TotalPrice','oTotalPrice');return false;">金额合计<span id="oTotalPrice" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TotalTax','oTotalTax');return false;">税额合计<span id="oTotalTax" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('TotalFee','oTotalFee');return false;">含税金额合计<span id="oTotalFee" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('BillStatus','oBillStatus');return false;">单据状态<span id="oBillStatus" class="orderTip"></span></div></th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('UsedStatus','oUsedStatus');return false;">审批状态<span id="oUsedStatus" class="orderTip"></span></div></th>
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
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecount">
                                        </div>
                                    </td>
                                     <td height="28"  align="right">
                                                    <div id="pageDataList1_Pager" class="jPagerBar"></div>
                                     </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <%--<input name="text" type="text" id="ShowPageCount"/>--%>
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <%--<input name="text" type="text" id="ToPage"/>--%>
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>

