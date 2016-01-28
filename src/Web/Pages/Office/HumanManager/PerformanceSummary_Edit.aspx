<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceSummary_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceSummary_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>评分汇总</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceSummary_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
    </style>
</head>
<body onload="InitialPage();">

<form id="frmMain" runat="server" >

<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server"  />
  <div id="divEditCheckItem" runat="server"  >    
     <input id="hidEditFlag" type="hidden" />
    <input id="hidElemID" type="hidden" runat="server" name="hidElemID"  />
     <input id="hidStatus" type="hidden" runat="server" name="hidStatus"  />
    <input id="hidEmployeId" type="hidden" />
    <table  width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
    
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            
        </tr>
         <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">评分汇总</td>
                    </tr>
        <tr>
            <td  valign="top" class="Title">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" style="float:left" />
      <%-- <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="true" alt="保存" id="btnSave" style="cursor:hand" height="25" onclick="DoSave('0');"/>--%>
      <img src="../../../Images/Button/btn_sure.jpg" runat="server" visible="false" alt=" 确定" id="btnGather" style="cursor:hand; float:left"   onclick="DoSave();"/>
  <%--     <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="true" alt="确认" id="btnCheck" style="cursor:hand" height="25" onclick="DoSave('1');"/>--%>
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand; float:left"   onclick="DoBack();"/> 
               </td>
            <td align="right" valign="top" class="Title">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核任务编号</td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:codingrule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none " class="tdinput"  />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">考核任务主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="50" size="50"  class="tdinput"/></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                                   <%-- <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1" >
                                            <option value="1">月考核</option>
                                            <option value="2">季考核</option>
                                             <option value="3">半年考核</option>
                                            <option value="4">年考核</option>
                                            <option value="5">临时考核</option>
                                        </select>--%>
                                        <div  id="selTaskFlag" runat="server" ></div>
                                        
                                        </td>
                                    <td class="tdColTitle" width="10%">考核周期</td>
                                    <td class="tdColInput" width="23%">
                                
                                      <div  id="dvTaslNum">
                                
                                                <%--<select id="selTaskNum" runat="server" name="D2" style="display:block">
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
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})" style="width:100%"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核期间结束日期</td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})" style="width:100%"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注</td>
                                    <td class="tdColInput" width="23%" >
                                       <input id="inpRemark" type="text"  size="70" maxlength ="50" class="tdinput"/>
                                         
                                    </td>
                                     <td class="tdColTitle" width="10%">评分状态</td>
                                    <td class="tdColInput" width="23%">
                                    <div id="inpTaskStaus" style="color:Red ;"></div>
                                    </td>
                                </tr> 
                                <%--<tr>
                                    <td height="20" class="tdColTitle" width="10%">评语</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                       <input id="inpAdviceNote" type="text"  size="50" maxlength ="50"/>
                                         
                                    </td>
                                </tr>
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">说明事项</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                       <input id="inpNote" type="text"  size="50" maxlength ="50"/>
                                         
                                    </td>
                                </tr>--%>
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">被考核人数</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                      
                                          <div id="inpEmployee"> </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">已评分的考评人人数</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                       
                                          <div id="inpHasScore"> </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td height="20" class="tdColTitle" width="10%">未评分的考评人人数</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                      
                                          <div id="inpNotScore"> </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">是否可汇总</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                      
                                        <div id="inpIsCheck"> </div>
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
