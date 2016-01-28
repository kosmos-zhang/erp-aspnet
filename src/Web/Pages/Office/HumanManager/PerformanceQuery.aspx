<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceQuery.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceQuery" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>
<%@ Register Src="../../../UserControl/Human/SelectedTaskList.ascx" TagName="TaskUC" TagPrefix="uc3" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>考核查询</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceQuery.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
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
<uc3:TaskUC ID="TaskUC1" runat="server" />
<div id="PerformanceTypeCheck"   >
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
   
  <tr>
    <td align="right" colspan="2" style="height:20px" ><a href="PerformanceQuery.aspx?ModuleID=2011809" style="text-decoration:none; "><span style="   ">  考核查询</span></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceStatistics.aspx?ModuleID=2011809" style="text-decoration:none; "><span style="  ">考核统计</span></a></td>
    </tr>
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" >
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
                                <td class="tdColTitle" width="10%">
                                      考核类型</td>
                                    <td class="tdColInput" width="23%">
                                           <select id="SelPerType" runat="server"   name="SelPerType">
                                        </select>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                        考核任务</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtTestNo" id="txtTaskTitle" runat="server"   maxlength="50" type="text"   style="width:85%"  onclick="popTaskObj.ShowList('txtTaskTitle')" readonly="readonly" title =""  class="tdinput"   />
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
                                        考核周期</td>
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
                                                </select>
                                                </div>
                                    </td>
                                      <td height="20" class="tdColTitle" width="10%">考核等级</td>
                                    <td class="tdColInput" width="23%">
                               <select id="SelLevelType" runat="server" name="SelLevelType" style="display:block">
                                                    <option value="0">--请选择--</option>
                                                    <option value="1">达到要求</option>
                                                    <option value="2">超过要求</option>
                                                    <option value="3">表现突出</option>
                                                    <option value="4">需要改进</option>
                                                    <option value="5">不合格</option>
                                                </select>
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核建议</td>
                                    <td class="tdColInput" width="23%">
                                         <select id="SelAdviceType" runat="server"  name="SelAdviceType" style="display:block">
                                                    <option value="0">--请选择--</option>
                                                    <option value="1">不做处理</option>
                                                    <option value="2">调整薪资</option>
                                                    <option value="3">晋升</option>
                                                    <option value="4">调职</option>
                                                    <option value="5">辅导</option>
                                                      <option value="6">培训</option>
                                                    <option value="7">辞退</option>
                                                </select>
                    
                                    </td>
                                </tr>  
                               
                                       <tr>
                                    <td height="20" class="tdColTitle" width="10%">被考核人</td>
                                    <td class="tdColInput" width="23%" >
           <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtSearchEmployee');"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtSearchEmployee" runat="server" />
                                         
                                    </td>
                                      <td class="tdColTitle" width="10%"></td> <td class="tdColInput" width="23%"></td>
                                          <td class="tdColTitle" width="10%"></td> <td class="tdColInput" width="23%"></td>
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
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title">考核查询</td>
                    </tr>
                       <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                   <img src="../../../images/Button/Main_btn_print.jpg" alt="打印" id="btnPrint" runat="server"   onclick="PrintRecibal();" />
                                          <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                     
                                         <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('passEmployeeID','oC0');return false;">
                                                被考核人<span id="oC0" class="orderTip"></span>
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
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('SignDate','oC13');return false;">
                                                确认时间<span id="oC13" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>     <br/>
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
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" maxlength="5" />条&nbsp;&nbsp;
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
    
    
   </div> 
     <div id="divEditCheckItem" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:300; position: absolute;top: 20%; left: 15%;  display:none  ">    
     <input id="hidEditFlag" type="hidden" />
    <input id="hidElemID" type="hidden" />
    
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="Table1">
    
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="true" alt="保存" id="btnSave" style="cursor:hand" height="25" onclick="DoSave();"/>
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand" height="25" onclick="DoBack();"/>                            
            </td>
            <td align="right" valign="top" class="tdColInput">
                <div id='div1'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="Table2"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核任务编号</td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:codingrule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none " disabled ="true" />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">考核任务主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="25" size="25" /></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                                    <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1">
                                            <option value="1">月考核</option>
                                            <option value="2">季考核</option>
                                             <option value="3">半年考核</option>
                                            <option value="4">年考核</option>
                                            <option value="5">临时考核</option>
                                        </select></td>
                                    <td class="tdColTitle" width="10%">考核周期</td>
                                    <td class="tdColInput" width="23%">
                                
                                      <div  id="dvTaslNum">
                                
                                                <select id="selTaskNum" runat="server" name="D2" style="display:block">
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
                                                </select></div></td>
                                </tr>  
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间开始日期</td>
                                    <td class="tdColInput" width="23%">
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核期间结束日期</td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                       <input id="inpRemark" type="text"  size="25" maxlength ="50"/>
                                         
                                    </td>
                                </tr> 
                                <tr>
                                
                                <td colspan="2" align="center">模板</td>
                                <td colspan="2" align="center">被考核人员</td>
                                </tr>  
                                <tr style="background-color:#FFFFFF">
                                <td  colspan="2" align="center">
                                <table id="tbTemplate" class="tab_a" >
                                <tbody align="center"></tbody>
                                
                                
                                </table>
                                
                                </td>
                               <td  colspan="2" align="center"  >
                                 <table id="tbEmployee"  class="tab_a">
                                <tbody align="center"></tbody>
                                
                                
                                </table></td> 
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
    </table>
    </div>
</form>
<p>
&nbsp;&nbsp;&nbsp;
</p>
</body>
</html>