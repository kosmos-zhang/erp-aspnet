<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceTaskCheck_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceTaskCheck_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>员工确认</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceTaskCheck_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
    </style>
</head>
<body onload="ddds();">

<form id="frmMain" runat="server" >

<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server"  />
  <div id="divEditCheckItem" runat="server"  >    
     <input id="hidEditFlag" type="hidden" />
    <input id="hidElemID" type="hidden" runat="server" name="hidElemID"  />
     <input id="hidStatus" type="hidden" runat="server" name="hidStatus"  />
    <input id="hidEmployeId" type="hidden" runat="server" />
    <input id="hidTemplateNo" type="hidden" runat="server" />
     <input id="hidSign" type="hidden" runat="server" />
    <table  width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
    
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            
        </tr>
         <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">员工确认</td>
                    </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" style="float:left" /><img src="../../../Images/Button/Bottom_btn_confirm.jpg " runat="server" visible="false" alt="保存" id="btnGather" style="cursor:hand; float:left"  onclick="DoSave();"  /><img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible=" true" id="btnBack" runat="server" style="cursor:hand"   onclick="DoBack();"  /> 
               </td>
               <td align="right" valign="top" class="tdColInput">
              
            </td>
        </tr>
         <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" /> 
                <span class="Blue">考核任务信息</span></td>
            <td align="right" valign="top" class="tdColInput">
                <div id='divTask'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('trTask','divTask')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr id="trTask" runat="server"  style ="display:block ;">
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
                                        <input id="inpTaskNo" type="text"  style=" display:none " class="tdinput" />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">考核任务主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="25" size="25"  class="tdinput"/></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                             <%--       <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1" >
                                            <option value="1">月考核</option>
                                            <option value="2">季考核</option>
                                             <option value="3">半年考核</option>
                                            <option value="4">年考核</option>
                                            <option value="5">临时考核</option>
                                        </select>--%>
                                        <div id="selTaskFlag" runat="server"></div>
                                        
                                        </td>
                                    <td class="tdColTitle" width="10%">考核周期</td>
                                    <td class="tdColInput" width="23%">
                                
                                      <div  id="dvTaslNum">
                                
                                        <%--        <select id="selTaskNum" runat="server" name="D2" style="display:block">
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
                                                </select>--%>
                                                
                                                </div></td>
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
                                    <td class="tdColInput" width="23%" >
                                       <input id="inpRemark" type="text"  size="25" maxlength ="50" class="tdinput"/>
                                         
                                    </td>
                                     <td class="tdColTitle" width="10%">评分状态</td>
                                    <td class="tdColInput" width="23%">
                                    <div id="inpTaskStaus" style="color:Red ;"></div>
                                    </td>
                                </tr> 
                                  <tr>
                                    <td height="20" class="tdColTitle" width="10%">创建人</td>
                                    <td class="tdColInput" width="23%" >
                                       <div id="inpCreater"></div>
                                         
                                    </td>
                                     <td class="tdColTitle" width="10%">创建时间</td>
                                    <td class="tdColInput" width="23%">
                                    <div id="inpCreateDate"></div>
                                    </td>
                                </tr> 
                                  <tr>
                                    <td height="20" class="tdColTitle" width="10%">汇总人</td>
                                    <td class="tdColInput" width="23%" >
                                       <div id="inpSummaryer"></div>
                                         
                                    </td>
                                     <td class="tdColTitle" width="10%">汇总日期</td>
                                    <td class="tdColInput" width="23%">
                                    <div id="inpSummaryDate"></div>
                                    </td>
                                </tr> 
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">评语</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                          <textarea id="inpAdviceNote" maxlength ="50" onkeyup="textcontrol(this.id,50)" rows="2" cols="2" style="width:100%; height :62px;" class="tdinput"></textarea>
                                    </td>
                                </tr>
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">说明事项</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                              <textarea id="inpNote" maxlength ="50" onkeyup="textcontrol(this.id,50)" rows="2" cols="2" style="width:100%; height :62px;" class="tdinput"></textarea>
                                    </td>
                                </tr>
                                
                                
                    
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" /> 
                <span class="Blue">考核确认信息</span> <%-- <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="true" alt="保存" id="btnSave" style="cursor:hand" height="25" onclick="DoSave('0');"/>--%>&nbsp;
  <%--     <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="true" alt="确认" id="btnCheck" style="cursor:hand" height="25" onclick="DoSave('1');"/>--%>&nbsp; 
               </td>
            <td align="right" valign="top" class="tdColInput">
                <div id='divScore'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('trScore','divScore')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr  id="trScore" runat="server"  style ="display:block ;">
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">被考核人反馈 </td>
                                    <td class="tdColInput" width="23%">
                                     
                                          <input id="inpSignNote" type="text"  size="100" maxlength ="100" class="tdinput" style="width:100%"/>
                                    </td>
                                    <td class="tdColTitle" width="10%"></td>
                                    <td class="tdColInput" width="23%">
                                        </td>
                                </tr>     
                              
                    
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" /> 
                <span class="Blue">总评信息</span> &nbsp;
  <%--     <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="true" alt="确认" id="btnCheck" style="cursor:hand" height="25" onclick="DoSave('1');"/>--%>&nbsp; 
               </td>
            <td align="right" valign="top" class="tdColInput">
                <div id='divSumary'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('trSummary','divSumary')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr id="trSummary" runat="server"  style ="display:block ;">
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="Table3"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">累计扣分</td>
                                    <td class="tdColInput" width="23%">
                                         <input id="inpKillScore" type="text"  size="25" maxlength ="50" class="tdinput"/>
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">累计加分</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inpAddScore" type="text"  size="25" maxlength ="50" class="tdinput"/>
                                        </td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">建议事项</td>
                                    <td class="tdColInput" width="23%">
                                    <select id="sleAdviceType" runat="server"   name="D1" >
                                            <option value="1">不做处理</option>
                                            <option value="2">调整薪资</option>
                                             <option value="3">晋升</option>
                                            <option value="4">调职</option>
                                            <option value="5">辅导</option>
                                             <option value="6">培训</option>
                                            <option value="7">辞退</option>
                                        </select></td>
                                    <td class="tdColTitle" width="10%">考核等级</td>
                                    <td class="tdColInput" width="23%">
                                
                                                <select id="sleLevelType" runat="server" name="D2" style="display:block">
                                                    <option value="1">达到要求</option>
                                                    <option value="2">超过要求</option>
                                                    <option value="3">表现突出</option>
                                                    <option value="4">需要改进</option>
                                                    <option value="5">不合格</option>
                                                </select></td>
                                </tr>  
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">总评</td>
                                    <td class="tdColInput" width="23%">
                                      <input id="inpSummaryNote" type="text"  size="25" maxlength ="50" class="tdinput"/>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核总得分</td>
                                    <td class="tdColInput" width="23%">
                                        
                                      <div id="inpTotalScore"></div>
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">建议说明</td>
                                    <td class="tdColInput" width="23%" >
                                      <input id="inpsan" type="text"  size="25" maxlength ="50" class="tdinput" style="width:100%"/>
                                         
                                    </td>
                                     <td class="tdColTitle" width="10%">备注</td>
                                    <td class="tdColInput" width="23%">
                                    <input id="inpSumarryRemark" type="text"  size="25" maxlength ="50" class="tdinput" style="width:100%"/>
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">奖惩说明</td>
                                    <td class="tdColInput" width="23%" >
                                      <input id="inpRewardNote" type="text"  size="25" maxlength ="50" class="tdinput" style="width:100%"/>
                                         
                                    </td>
                                     <td class="tdColTitle" width="10%">实际得分</td>
                                    <td class="tdColInput" width="23%">
                                 <input id="inpSrs" type="text"  size="25" maxlength ="50" class="tdinput" style="width:100%"/>
                                    </td>
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
</body>
</html>
