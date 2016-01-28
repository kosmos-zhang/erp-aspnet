<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceTask.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceTask" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考核任务</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceTask.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
        <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        #dvTaslNum
        {
            height: 15px;
            width: 14%;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server" >

<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
<div id="PerformanceTypeCheck" >
    <input id="hidEditFlag" type="hidden" />
    <input id="hidElemID" type="hidden" />
           <input type="hidden" id="hidSearchCondition" runat="server"  />
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
             <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">新建考核任务</td>
                    </tr>
                          <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                  <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>
       <img id="btnBack" runat="server" alt="返回"  onclick="DoOut();"   src="../../../Images/Button/Bottom_btn_back.jpg" style="cursor:hand"   visible="true" />             
                                    </td>
                                       <td align="right" valign="top" class="tdColInput">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>
            </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
        
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%"> 考核任务编号<span style="color:Red">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:CodingRule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none " disabled ="true" />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">考核任务主题<span style="color:Red">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="50" size="50"  class="tdinput"  SpecialWorkCheck="考核任务主题"
/></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间类型<span style="color:Red">*</span></td>
                                    <td class="tdColInput" width="23%">
                                    <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1">
                                            <option value="1">月考核</option>
                                            <option value="2">季考核</option>
                                             <option value="3">半年考核</option>
                                            <option value="4">年考核</option>
                                            <option value="5">临时考核</option>
                                        </select></td>
                                    <td class="tdColTitle" width="10%">   <div id="getNull"> 考核周期<span style="color:Red">*</span></div></td>
                                    <td class="tdColInput" width="23%" valign="top">
                                   <select id="selTaskYear" runat="server" name="D3" style="display:block; float:left">
                                                </select>
                                      <div  id="dvTaslNum">    <select id="selTaskNum" runat="server" name="D2" style="display:block; float:left">
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
                                                </select>  </div></td>
                                </tr>  
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核开始日期<span style="color:Red">*</span></td>
                                    <td class="tdColInput" width="23%">
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="J.calendar.get();"  style="width:100%"  ReadOnly="true"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核结束日期<span style="color:Red">*</span></td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="J.calendar.get();"   style="width:100%" ReadOnly="true"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                    
                                    <input  type="text"  id="inpRemark"   maxlength="50"onkeyup="textcontrol(this.id,50)" style="width:100%;  " class="tdinput"    />
                           
                                    </td>
                                </tr> 
                                <tr>
                                
                                <td colspan="2" align="center">模板</td>
                                <td colspan="2" align="center">被考核人员</td>
                                </tr>  
                                <tr style="background-color:#FFFFFF">
                                <td  colspan="2" align="center">
                                  <div id="Div1"  style="overflow-y:auto;width:91%; line-height:14pt;letter-spacing:0.2em;height:332px">
                                <table id="tbTemplate" class="tab_a" >
                                <tbody align="center"></tbody>
                                
                                
                                </table>
                                </div>
                                </td>
                               <td  colspan="2" align="center"  >
                                 <div id="contentContainerDiv"  style="overflow-y:auto;width:91%; line-height:14pt;letter-spacing:0.2em;height:332px">
                                 <table id="tbEmployee"  class="tab_a">
                                <tbody align="center"></tbody>
                                
                                   
                                </table></div></td> 
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
