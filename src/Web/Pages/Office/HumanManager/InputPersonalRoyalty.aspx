<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputPersonalRoyalty.aspx.cs" Inherits="Pages_Office_HumanManager_InputPersonalRoyalty" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>个人提成工资录入</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
   <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
 <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/InputPersonalRoyalty.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <style type="text/css">
        #tblMain
        {
            margin-top:0px;
            margin-left:0px;
		    background-color:#F0f0f0;
      	    font-family:tahoma;
      	    color:#333333;
      	    font-size:12px;
        }
        .errorMsg
        {
	        filter:progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
	        position:absolute;
	        top:240px;
	        left:450px;
	        border-width:1pt;
	        border-color:#666666;
	        border-style:solid;
	        width:290px;
	        display:none;
	        margin-top:10px;
	        z-index:21;
        }
        #Text1
        {
            width: 165px;
        }
        #Button1
        {
            width: 69px;
        }
        #Button2
        {
            width: 69px;
        }
        #txtOpenDate
        {
            width: 103px;
        }
        #txtCloseDate
        {
            width: 105px;
        }
        #txtOpenDate0
        {
            width: 103px;
        }
        #txtCloseDate0
        {
            width: 105px;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server">
<input type="hidden" id="hiddExpBillStatus" runat="server" /><input type="hidden" id="hiddExpFlowStatus" runat="server" />
                                        <input type="hidden" id="hiddExpSendPro" runat="server" />
                                    <input type="hidden" id="hiddExpIsOpenbill" runat="server" />
                                        <input type="hidden" id="hiddExpCustID" runat="server" />
                                        <input type="hidden" id="hiddExpSeller" runat="server" />
                                                    <input type="hidden" id="hiddExpTotalPrice1" runat="server" />
                                        <input type="hidden" id="hiddExpFromBillID" runat="server" />
                                                    <input type="hidden" id="hiddExpTotalPrice" runat="server" />
<input id="hidTaxInfo" type="hidden" runat="server" />

<table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblMain">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
     <tr height="20" align="right">
            <td colspan='3' width='100%'>
                 &nbsp; <a href="InputCompanyRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">公司提成</a>&nbsp;
                            &nbsp;<a href="InputDepatmentRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">部门提成</a>&nbsp;
                               &nbsp;<a href="InputPersonalRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">个人业务提成</a>&nbsp;
             &nbsp;<a href="InputFloatSalary.aspx?ModuleID=2011702&type=1" style="text-decoration: none; color :Blue"  >计件工资</a>&nbsp;
                &nbsp;<a href="InputFloatSalary.aspx?ModuleID=2011702&type=2" style="text-decoration: none; color :Blue"  >计时工资</a>&nbsp;
                &nbsp;<a href="InputFloatSalary.aspx?ModuleID=2011702&type=3" style="text-decoration: none; color :Blue" >产品单品提成</a>&nbsp;
                &nbsp;<a href="InputPerformanceRoyalty.aspx?ModuleID=2011702" style="text-decoration: none; color :Blue">绩效工资</a>&nbsp;
            </td>
        </tr>
            <tr>
            <td valign="top" class="Blue">
                
            </td>
            <td align="right" valign="top">
                 
                &nbsp;&nbsp;
            </td>
        </tr>
    <tr>
    <td  valign="top" class="Blue">
        <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
    </td>
    <td align="right" valign="top">
        <div id='divSearch'>
            <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
        </div>&nbsp;&nbsp;
    </td>
    </tr>
    <tr>
        <td colspan="2"  >
            <table width="100%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                <tr>
                    <td bgcolor="#FFFFFF">
                        <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                            <tr>
                                <td width="10%" height="20" class="tdColTitle">员工编号</td>
                                <td width="23%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeNo" Width="95%" CssClass="tdinput" runat="server" SpecialWorkCheck="员工编号"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdColTitle">员工姓名</td>
                                <td width="23%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeName" Width="95%" CssClass="tdinput" runat="server" SpecialWorkCheck="员工姓名"></asp:TextBox>
                                </td>
                              <td width="10%" bgcolor="#E7E7E7" align="right">
                                        订单日期
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                       <input id="txtOpenDate"  runat="server"   readonly="readonly" class="tdinput" name="txtOpenDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})" />至
                                        <input id="txtCloseDate" runat="server"  readonly="readonly"  class="tdinput" name="txtCloseDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})"
                                            type="text" />
                                    </td>
                            </tr>                 
                            <tr>
                                <td colspan="6" align="center" bgcolor="#FFFFFF">
                                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="true" runat="server" style='cursor:pointer;' onclick='DoSearch()'   />
                                    <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false"  style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
             </table>
        </td>
    </tr>
    <tr>
            <td colspan="2">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                 
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title">个人提成工资录入</td>
                    </tr>
                    </table></td></tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                 <%--   <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>--%>
                                    <img src="../../../Images/Button/btn_tbxsdd.jpg" runat="server" visible="true" alt="同步销售订单" id="btnSynchronizerSell" style="cursor:hand"  onclick="ShowSellOrder();"/>
                                    <img src="../../../Images/Button/btn_jstcl.jpg" runat="server" visible="true" alt="计算提成率" id="btnSynchronizerRax" style="cursor:hand"  onclick="LoadRax();"/>
                                  <%--  
                                <td align="right" class="tdColInput">
                                <%--    <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"   />--%>
                                </td>
                            </tr>
                        </table> 
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" valign="top">
   <div style="overflow-y:auto;width:100%; line-height:14pt;letter-spacing:0.2em;height:400px;  overflow-x:scroll">
    <table width="100%"    border="0" cellpadding="0" cellspacing="0" id="tblDetail" style="height :800px;  vertical-align:top" >
        <tr>
            <td style="width:100%" valign="top">
                <div id="divInsuDetail"  runat="server" style="padding-bottom :10px; padding-top:0px;  ">
                </div>
            </td>
        </tr>
        <tr>
            <td height="10"></td>
        </tr>
    </table>
 </div> 
    </td></tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<span id="spanMsg" class="errorMsg"></span>
<uc1:Message ID="msgError" runat="server" />
 <div id="divBackShadow" style="display: none;">
        <iframe id="BackShadowIframe" frameborder="0" width="100%"></iframe>
    </div>

<div id="div_Add" style="padding: 10px; width: 900px; z-index: 2; position: absolute;
        top: 20%; left: 20%; display: none" class="checktable">
   <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
             <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='closeProductdiv();' />
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
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        订单编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input type="text" class="tdinput" specialworkcheck="订单编号" style="width: 90%" id="orderNo" />
                                        <input type="hidden" id="hiddExpOrderNo" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        业务员
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="UserSeller" class="tdinput" 
                                            style="width: 90%" type="text" />
                                        <input type="hidden" id="hiddExpTitle" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        订单日期
                                    </td>
                                    <td width="30%" bgcolor="#FFFFFF">
                                        &nbsp;<input type="hidden" id="hiddExpFromType" runat="server" /><input 
                                            id="txtOpenDate0"  runat="server"   readonly="readonly" class="tdinput" 
                                            name="txtCloseDate0" onclick="J.calendar.get()" />至<input id="txtCloseDate0" 
                                            runat="server"  readonly="readonly"  class="tdinput" name="txtCloseDate0" onclick="J.calendar.get()"
                                            type="text" /></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img runat="server"  alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            style='cursor: pointer;' id="btnSearchSell" onclick='TurnToPage(1)' />
                                            <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                                onclick="GetValue();" id="imgsure" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex" style="margin-top:0px;">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                销售订单列表
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox" visible="false" id="btnAll" onclick="OptionCheck()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OrderNo','oGroup');return false;">
                                    订单编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                    订单主题<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustName','oC2');return false;">
                                    客户<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OrderDate','Span3');return false;">
                                    订单日期<span id="Span3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RealTotal','Span4');return false;">
                                    折后含税金额<span id="Span4" class="orderTip"></span></div>
                            </th>
                          
                        
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                class="style1">
                                <div class="orderClick" onclick="OrderBy('SellerName','oC4');return false;">
                                    业务员<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CurrencyTypeName','oC8');return false;">
                                    币种<span id="oC8" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Rate','oC9');return false;">
                                    汇率<span id="oC9" class="orderTip"></span></div>
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
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pageSellOffcount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            &nbsp;<input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" maxlength="3" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" maxlength="7" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: pointer;' alt="go"
                                                align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
    <a name="pageDataList1Mark"></a>
    <input id="Seller" type="hidden" />
    <input type="hidden" id="hiddUrl" value="" />
    <input type="hidden" id="hiddExpOrder" runat="server" />
    <input type="hidden" id="hiddExpTotal" runat="server" />
    </div>
</form>
</body>
</html>
