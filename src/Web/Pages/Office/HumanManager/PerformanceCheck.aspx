<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceCheck.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceCheck" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>考核评分</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceCheck.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:100%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; }

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        #inptTitle
        {
            width: 398px;
        }
        .style1
        {
            background-color: #E6E6E6;
            text-align: right;
            width: 4%;
        }
    </style>
</head>
<body onload="initialPage();">

<form id="frmMain" runat="server" >

<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server"  />
  <div id="divEditCheckItem" runat="server"  >    
      <input id="hidModuleID" type="hidden" runat="server" />
            <input id="hidSearchCondition" type="hidden" runat="server" />
             <input id="hidIsliebiao" type="hidden" runat="server" />
      
     <input id="hidEditFlag" type="hidden" />
    <input id="hidElemID" type="hidden" runat="server" name="hidElemID"  />
     <input id="hidStatus" type="hidden" runat="server" name="hidStatus"  />
       <input id="hiEmpl" type="hidden" runat="server" name="hiEmpl"  />
            <input id="hidTemplateNo" type="hidden" runat="server" name="hidTemplateNo"  />
    <input id="hidEmployeId" type="hidden" />
    <input id="hidStepNo" type="hidden" />
    <table  border="0" cellpadding="0" cellspacing="0" class="checktable"  id="mainindex" width="98%">
    
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            
        </tr>
        <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title"><div id="divTitle" runat="server">  考核评分</div></td>
                    </tr>
        <tr>
            <td  valign="top" class="Title" colspan="2">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" style="float:left" />
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand; float:left"   onclick="DoSave('0');"/>
       <img src="../../../Images/Button/UnClick_bc.jpg" runat="server" visible="false" alt="保存" id="btnUncheckSave" style="cursor:hand; display:none;float:left "   />
       <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="false" alt="确认" id="btnCheck" style="cursor:hand;float:left"   onclick="DoSave('1');"/>
           <img src="../../../Images/Button/UnClick_qr.jpg" runat="server" visible="false" alt="确认" id="btnUncheck" style="cursor:hand;display:none;float:left"   />
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand;float:left"  onclick="DoBack();"/>                            <div id="mes" style="color:Red "></div>
            </td>
        
        </tr>
        <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                  <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />         
                                    考核任务信息</td>
                                       <td align="right" valign="top" class="tdColInput">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tbTask','divSearch')"/>
                </div>
            </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="Table2"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                            <tr><td colspan="4">
                            <table id="tbTask" width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核任务编号</td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:codingrule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none;border:none;border-bottom:solid   0px   black; "  />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">考核任务主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="50"  size="50" class="tdinput"/> </td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                              <%--      <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1" >
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
                                
                                             <%--   <select id="selTaskNum" runat="server" name="D2" style="display:block">
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
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput" 
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})" 
                                            Width="400px"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核期间结束日期</td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput" 
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})" 
                                            Width="400px"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                           <textarea id="inpRemark" maxlength ="50" onkeyup="textcontrol(this.id,50)"  rows="2" cols="2" style="width:100%; height :62px;" class="tdinput"></textarea>
                                    </td>
                                </tr> 
                              </table></td>
                                </tr>
                            <tr id="tbf2">
                        <td height="35" colspan="4" valign="top">
                            <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                  <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />         
                                    考核评分信息   <span id="Emp" runat="server" style=" color:Red"></span> </td>
                                       <td align="right" valign="top" class="tdColInput">
                <div id='divSearchCheck'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  id="stepCheck2" onclick="oprItem('tbCheck','divSearchCheck')"/>
                </div>
            </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr  id="tbf1"><td colspan="4" ><table id="tbCheck" width="100%" border="0"  cellpadding="0" cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td colspan="4">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table width="99%" border="0" align="left" cellpadding="1" cellspacing="1" id="tblDetailInfo" class="tab_a" >
                                <tbody>
                                    <tr>
                                           
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" >
                                            <div class="orderClick" >
                                                指标名称<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                评分细则<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                标准分<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                最低分<span id="Span3" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                最高分<span id="Span4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                           <span style="color :Red ">*</span>     打 分<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                权重(%)<span id="Span2" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                评分标准<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                评分来源<span id="Span1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                备注<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- </div> -->
                      
                            
                            <br/>
                           <%-- <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
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
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>--%>
                            <br/>
                        </td>
                    </tr>
                    <tr><td colspan="4"><table  style=" width:100%">
                    <tr>
                                    <td height="20" class="style1"  >评语   </td><td> 
                                    <input  type="text"  id="inpAdviceNote" maxlength ="50"  style="width:100%" class="tdinput" name="S1" />
                                    
                                </td>
                                 
                                </tr>
                    <tr>
                                    <td height="20" class="style1">说明事项</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                         <input  type="text"  id="inpNote" maxlength ="50"  style="width:100%" class="tdinput"   />
 
                                    </td>
                                </tr>
                     </table></td></tr>
                    <tr>
                        <td  colspan="4" valign="top" bgcolor="#FFFFFF" align="right">
                        <img src="../../../Images/Button/sb_hj.jpg" alt="返回" visible="false" id="btnCount" runat="server" style="cursor:hand" height="25" onclick="Caculate();"  />
                         <input id="Button1" type="button" value=" 总分: "   class="tdinput"/>
                               <input   id="inpAccount" type="text"  size="10" maxlength ="50" disabled ="disabled" class="tdinput" />       
                        </td>
                    </tr>
                      </table></td></tr>
                      <tr>
            <td  valign="top" class="tdColInput" style="width:15%">
                <img src="../../../images/Main/Arrow.jpg" width="30" height="18" align="absmiddle" style="float:left" />
                      <div style="float:left">考核评分记录:</div>
            </td>
            <td align="right" valign="top" class="tdColInput" colspan="4">
                <div id='divSearch2'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  id="stepCheck" onclick="oprItem('divSear','divSearch2')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
                    <tr>
                        <td colspan="4"> <div id="divSear"  style="overflow-y:auto;width:100%; line-height:14pt;letter-spacing:0.2em;height:400px" >
                    <table width="100%" border="0" align="left" cellpadding="1" cellspacing="1" id="tblDetailInf" class="tab_a">
                              <tbody>
                                    <tr>
                                           
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" >
                                            <div class="orderClick" >
                                                指标名称<span id="Span5" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                评分细则<span id="Span6" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                标准分<span id="Span7" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                最低分<span id="Span8" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                最高分<span id="Span9" class="orderTip"></span>
                                            </div>
                                        </th>
                                        
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                打 分<span id="Span10" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                权重(%)<span id="Span11" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                评分标准<span id="Span12" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                评分来源<span id="Span13" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" >
                                                备注<span id="Span14" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            
                            
                            </table>
                </div>    </td>
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
