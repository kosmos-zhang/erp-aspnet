<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderInfoInfo.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderInfoInfo" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<style type="text/css">
         .userListCss
        {
	        position:absolute;top:100px;left:600px;
	        border-width:1pt;border-color:#EEEEEE;border-style:solid;
	        width:200px;
	        display:none;
	        height:220px;
	        z-index:100;
	    }
    </style>
    <script type="text/javascript">
 
function SelectedNodeChanged(code_text,type_code)
{   
   document.getElementById("txtCustClassName").value=code_text;
   document.getElementById("txtCustClass").value=type_code;
   hideUserList();
}
function hidetxtUserList()
{
   hideUserList();
}
function showUserList()
{
  var list = document.getElementById("userList");
  if(list.style.display != "none")
   {
      list.style.display = "none";
      return;
   }
   document.getElementById("userList").style.display = "block";
}
function hideUserList()
{
 document.getElementById("userList").style.display = "none";
}

function clearInfo() {
    document.getElementById("txtCustClassName").value = "";
    document.getElementById("txtCustClass").value = "";
    hideUserList();
}

</script>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/PurchaseManager/ProviderInfoInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <title>供应商档案列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
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
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
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
                                        供应商编号
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtCustNo" MaxLength="25" runat="server" CssClass="tdinput" Width="95%"   SpecialWorkCheck="供应商编号" ></asp:TextBox>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        供应商名称
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtCustName" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="供应商名称"  ></asp:TextBox>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        供应商简称
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:TextBox ID="txtCustNam" MaxLength="25" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="供应商简称"  ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        拼音缩写
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtPYShort" MaxLength="25" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="拼音缩写" ></asp:TextBox>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        供应商类别
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <select name="drpCustType"  class="tdinput" runat="server" id="drpCustType"> </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        供应商分类
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:TextBox ID="txtCustClassName" runat="server" onclick="showUserList()" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input id="txtCustClass" type="hidden" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        所在区域
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <select name="drpAreaID" class="tdinput" width="119px" runat="server" id="drpAreaID"> </select>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        优质级别
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <select name="drpCreditGrade" class="tdinput" width="119px" runat="server" id="drpCreditGrade"></select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        分管采购员
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:TextBox ID="UsertxtManager" onclick="alertdiv('UsertxtManager,HidManager');" runat="server"
                                             ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                             <input type="hidden" id="HidManager" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        建档日期
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtStartCreateDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartCreateDate')})" ReadOnly="true"  CssClass="tdinput" Width="35%"></asp:TextBox>
                                        <asp:TextBox ID="txtZhi" Text="至" ReadOnly="true" runat="server"  CssClass="tdinput" Width="10%"></asp:TextBox><asp:TextBox ID="txtEndCreateDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndCreateDate')})" ReadOnly="true"  runat="server" CssClass="tdinput" Width="35%"></asp:TextBox>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                    <input type="hidden" id="hidModuleID" runat="server" />
                                    <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='SearchProviderInfoData()' visible="false" runat="server"/>
                                        <%--<img id="btnClear" alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" style='cursor: hand;'
                                            onclick="Fun_ClearInput()" width="52" height="23" />--%>
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
                供应商档案列表             </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <%--<a href="MasterProductSchedule_Add.aspx">--%>
                                <%--<img id="btnAdd" src="../../../images/Button/Bottom_btn_new.jpg" alt="添加主生产计划单" style='cursor: hand;'
                                    width="51" height="25" border="0" /></a><img id="btnDel" src="../../../images/Button/Main_btn_delete.jpg"
                                        alt="删除主生产计划单" style='cursor: hand;' border="0" onclick="Fun_Delete_MasterProductSchedule();" />--%>
                            <%--                                        <img src="../../../images/Button/Main_btn_submission.jpg" alt="提交审批" />
                                        <img src="../../../images/Button/Main_btn_verification.jpg" alt="审批" />
                                        <img src="../../../images/Button/Bottom_btn_confirm.jpg" alt="确认" />
                                        <img src="../../../images/Button/Main_btn_Invoice.jpg" alt="结单" />
                                        <img src="../../../images/Button/Main_btn_qxjd.jpg" alt="取消结单" />--%>
                            <img id="btn_create"  src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style="cursor:hand;" onclick="CreateProviderInfo()"  runat="server" visible="false" />
                            <img id="btn_del"  src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" style="cursor:hand;" onclick="DelProviderInfo()"  runat="server" visible="false" />
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
                        <%--<tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanNo','oPlanNo');return false;">
                                    单据编号<span id="oPlanNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Subject','oSubject');return false;">
                                    单据主题<span id="oSubject" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Principal','oPrincipal');return false;">
                                    负责人<span id="oPrincipal" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('DeptID','oDeptID');return false;">
                                    部门<span id="oDeptID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CountTotal','oCountTotal');return false;">
                                    生产数量合计<span id="oCountTotal" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatus','oBillStatus');return false;">
                                    单据状态<span id="oBillStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FlowStatus','oFlowStatus');return false;">
                                    审批状态<span id="oFlowStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Confirmor','oConfirmor');return false;">
                                    确认人<span id="oConfirmor" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ConfirmDate','oConfirmDate');return false;">
                                    确认日期<span id="oConfirmDate" class="orderTip"></span></div>
                            </th>
                        </tr>--%>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" visible="false" id="checkall" onclick="SelectAll()" value="checkbox" /></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustNo','oCustNo');return false;">供应商编号<span id="oCustNo" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">供应商名称<span id="oCustName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustNam','oCustNam');return false;">供应商简称<span id="oCustNam" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('PYShort','oPYShort');return false;">供应商拼音代码<span id="oPYShort" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustTypeName','oCustTypeName');return false;">供应商类别<span id="oCustTypeName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CustClassName','oCustClassName');return false;">供应商分类<span id="oCustClassName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('AreaName','oAreaName');return false;">所在区域<span id="oAreaName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('ManagerName','oManagerName');return false;">分管采购员<span id="oManagerName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CreditGradeName','oCreditGradeName');return false;">优质级别<span id="oCreditGradeName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CreatorName','oCreatorName');return false;">建挡人<span id="oCreatorName" class="orderTip"></span></div></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderBy('CreateDate','oCreateDate');return false;">建挡日期<span id="oCreateDate" class="orderTip"></span></div></th>
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
                                    <td height="28" align="right">
                                        <div id="pageDataList1_PagerList" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
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
<div id="userList" class="userListCss" style="overflow-x:hidden;overflow-y:scroll">
<iframe id="aaaa" style="position: absolute; z-index: -1; width:400px; height:100px;" frameborder="1">  </iframe>
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="20" bgcolor="#E6E6E6" class="Blue">
          <table width="100%" border="0" cellspacing="0" cellpadding="">
              <tr>
                <td width="65%">供应商分类&nbsp;&nbsp;&nbsp;<a href="#" onclick="clearInfo();">清空</a></td>
                <td align="right">
                <img src="../../../Images/Pic/Close.gif" title="关闭" style="CURSOR: pointer"  onclick="document.all['userList'].style.display='none';"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
              </tr>
          </table>
          </td>
        </tr>
        <tr><td bgcolor="#F4F0ED" height="200" valign="top">
            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
    </asp:TreeView>
</td></tr>
     </table>
</div>
    </form>
</body>
</html>


