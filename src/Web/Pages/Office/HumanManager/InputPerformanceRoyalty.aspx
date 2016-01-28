<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputPerformanceRoyalty.aspx.cs"
    Inherits="Pages_Office_HumanManager_InputPerformanceRoyalty" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>绩效工资录入</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/HumanManager/InputPerformanceRoyalty.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .maintb
        {
            margin-top: 10px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex1">
        <tr>
            <td valign="top">
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
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
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
                                        人员选择
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="UserEmployee" id="UserEmployee" type="text" class="tdinput" size="19"
                                            readonly="readonly" onclick="alertdiv1('UserEmployee,txtEmployeeID');" style="width: 60%" />
                                        <input name="txtEmployeeID" id="txtEmployeeID" type="hidden" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        考核类型:
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <select id="sltTaskflag">
                                            <option value="">--请选择--</option>
                                            <option value="1">月考核系数</option>
                                            <option value="2">季度考核系数</option>
                                            <option value="3">半年考核系数</option>
                                            <option value="4">年考核系数</option>
                                        </select>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="22%" bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="txtorderBy" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" runat="server"
                                            style='cursor: hand;' onclick="DoSearch();" visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex1">
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
                绩效工资录入
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img id="btnDel" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="DoDel();" visible="false" />
                            <%--<input id="btnCopy" type="button" value="同步绩效总评" onclick="DoCopy()" runat="server"
                                visible="false" />--%>
                            <img id="btnCopy" runat="server" src="../../../images/Button/btn_tbjx.jpg" alt="同步"
                                style='cursor: hand;' border="0" onclick="DoCopy();" visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="4%">
                                <input type="checkbox" name="checkall" id="checkall" onclick="SelectAllCk()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="9%">
                                <div class="orderClick" onclick="OrderBy('EmployeeName','oc1');return false;">
                                    员工名称 <span id="oc1" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('TaskNo','oc10');return false;">
                                    考核任务编号<span id="oc10" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('Title','oc11');return false;">
                                    考核任务主题<span id="oc11" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="9%">
                                <div class="orderClick" onclick="OrderBy('TaskNum','oc2');return false;">
                                    考核周期 <span id="oc2" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="9%">
                                <div class="orderClick" onclick="OrderBy('TaskFlag','Span2');return false;">
                                    考核类型 <span id="Span2" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('StartDate','oc4');return false;">
                                    开始日期 <span id="oc4" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="9%">
                                <div class="orderClick" onclick="OrderBy('EndDate','oc3');return false;">
                                    结束日期 <span id="oc3" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('BaseMoney','oc5');return false;">
                                    绩效工资基数 <span id="oc5" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="9%">
                                <div class="orderClick" onclick="OrderBy('Confficent','oc6');return false;">
                                    考核系数% <span id="oc6" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="9%">
                                <div class="orderClick" onclick="OrderBy('PerformanceMoney','Span6');return false;">
                                    绩效工资<span id="Span6" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div style="overflow-y: auto">
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
                                            <div id="pageDataList1_Pager" class="jPagerBar">
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
                </div>
                <br />
            </td>
        </tr>
    </table>
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
