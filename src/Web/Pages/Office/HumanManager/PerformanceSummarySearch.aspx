<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceSummarySearch.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceSummarySearch" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>考核总评</title>
   
           <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/PerformanceSummarySearch.js" type="text/javascript"></script>
     <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <%--  <script src="../../../js/personal/MessageBox/SendInfo.js" type="text/javascript"></script>--%>
           <script src="../../../js/Personal/MessageBox/send.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
    </style>
</head>
<body>

<form id="frmMain" runat="server" >
<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server"  />
<div id="PerformanceTypeCheck"   >
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td   class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">
                                        考核任务编号</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtSearchTestNo" id="txtSearchTask" runat="server" maxlength="10" type="text" class="tdinput" style="width:100%" SpecialWorkCheck="考核任务编号" />
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                       考核任务主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptSearchTitle" type="text" maxlength="50" size="50" class="tdinput"  SpecialWorkCheck="考核任务主题" runat="server"/>
                                    </td>
                                          <td height="20" class="tdColTitle" width="10%">
                                                                                考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                                      <select id="selSearchTaskFlag" runat="server"  onclick="getSearchChange(this);" name="D1">
                                          <option value="0">--请选择--</option>
                                            <option value="1">月考核</option>
                                            <option value="2">季考核</option>
                                             <option value="3">半年考核</option>
                                            <option value="4">年考核</option>
                                            <option value="5">临时考核</option>
                                        </select>
                                    </td>
                                </tr>     
                                <tr>
                              
                                    <td class="tdColTitle" width="10%">
                                        考核期间</td>
                                    <td class="tdColInput" width="23%">
                                           <select id="selSearchTaskYear" runat="server" name="D3" style="display:block; float:left">
                                                </select>
                                             <div  id="dvSearchTaslNum">
                                                <select id="selSearchTaskNum" runat="server" name="D2" style="display:block; float:left">
                                                    <option value="0">--请选择--</option>
                                                    <option value="1">1月</option>
                                                    <option value="2">2月</option>
                                                    <option value="3">3月</option>
                                                    <option value="4">4月</option>
                                                    <option value="5">5月</option>
                                                    <option value="6">6月</option>
                                                    <option value="7">7月</option>
                                                    <option value="8">8月</option>
                                                    <option value="9">9月</option>
                                                    <option value="10">10月</option>
                                                    <option value="11">11月</option>
                                                    <option value="12">12月</option>
                                                </select></div>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                                                                任务状态</td>
                                    <td class="tdColInput" width="23%">
                                      <select id="selTaskStatus" runat="server"   name="selTaskStatus">
                                            <option value="2">待总评</option>
                                            
                                             <option value="3">已完成</option>
                                        </select>
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                        被考核人</td>
                                    <td class="tdColInput" width="23%">
                                            <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtSearchScoreEmployee');"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtSearchScoreEmployee" runat="server" />
                                    </td>
                                </tr> 
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">  
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearchInfo()'   />
                                        <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false" style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5"></td>
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
                        <td height="20" colspan="2" align="center" valign="top" class="Title">考核总评</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                          <asp:ImageButton ID="btnImport" 
                                              ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" 
                                              runat="server" onclick="btnImport_Click" style="height: 24px" />
                                        <%--<img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="true" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;' width="51" height="25" />--%>
                                        <%--<img src="../../../images/Button/Main_btn_out.jpg" alt="导出" visible="true" id="btnExport" runat="server" onclick="DoExport()" style='cursor:pointer;' width="51" height="25" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                     
                                         <th  height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('passEmployeeName','oC0');return false;">
                                                被考核人<span id="Span1" class="orderTip"></span>
                                            </div>
                                        </th> 
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TaskNo','oC0');return false;">
                                                任务编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th> 
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                                任务主题<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TaskFlag','oC2');return false;">
                                                考核期间类型<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TaskNum','oC3');return false;">
                                                考核期间<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('templateName','oC4');return false;">
                                                 考核模板<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TotalScore','oC5');return false;">
                                                考核总得分<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('KillScore','oC6');return false;">
                                                累计扣分<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('AddScore','oC7');return false;">
                                                累计加分<span id="oC7" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('RealScore','oC8');return false;">
                                                实际得分<span id="oC8" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('LevelType','oC9');return false;">
                                                考核等级<span id="oC9" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('AdviceType','oC10');return false;">
                                                考核建议<span id="oC10" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('EvaluaterName','oC11');return false;">
                                                总评人<span id="oC11" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('EvaluateDate','oC12');return false;">
                                                总评时间<span id="oC12" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- </div> -->
                            <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageClickInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divPage">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3"  maxlength="5"/>条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br/>
                        </td>
                    </tr>
                </table>            
            </td>
        </tr>
    </table>
    
    
   </div> 
      

     
</form>
</body>
</html>
