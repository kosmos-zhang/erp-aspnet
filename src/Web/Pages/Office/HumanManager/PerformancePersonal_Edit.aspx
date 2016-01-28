<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformancePersonal_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_PerformancePersonal_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>自我鉴定列表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformancePersonal_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
        <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        .style1
        {
            width: 151px;
        }
    </style>
</head>
<body>

<form id="frmMain" runat="server" >

<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<input id="hidModuleID" type="hidden" runat="server" />
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
                                        自我鉴定编号</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtSearchTestNo" id="txtSearchTask" runat="server" maxlength="50" 
                                            type="text" class="tdinput"   style="width:100%" SpecialWorkCheck="自我鉴定编号"  />
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                       自我鉴定主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptSearchTitle" type="text" maxlength="50" size="50"  class="tdinput"  SpecialWorkCheck="自我鉴定主题" runat="server"/>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                                                                鉴定周期分类</td>
                                    <td class="tdColInput" width="23%">
                                    <select id="selSearchTaskFlag" runat="server"  onclick="getSearchChange(this);" name="D1">
                                          <option value="0">--请选择--</option>
                                            <option value="1">月</option>
                                            <option value="2">季</option>
                                             <option value="3">半年</option>
                                            <option value="4">年</option>
                                                <option value="5">临时</option>
                                        </select>
                                    </td>
                                </tr>     
                                <tr>
                                    
                                    <td class="tdColTitle" width="10%" style="height:23px">
                                   <div id="getSearhNull">   考核周期</div>  </td>
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
                        <td height="20" colspan="2" align="center" valign="top" class="Title">自我鉴定列表</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand"   onclick="DoRun();"/>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'   />
                                          <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                        <%--<img src="../../../images/Button/Main_btn_out.jpg" alt="导出" visible="true" id="btnExport" runat="server" onclick="DoExport()" style='cursor:pointer;' width="51" height="25" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table  width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')"/></th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TaskNo','oC0');return false;">
                                                自我鉴定编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                                主题<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TaskFlag','oC2');return false;">
                                              鉴定周期分类<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('TaskNum','oC3');return false;">
                                                考核周期<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('StartDate','oC4');return false;">
                                                开始日期<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('EndDate','oC5');return false;">
                                                结束日期<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('Status','oC6');return false;">
                                                鉴定状态<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('CreateDate','oC7');return false;">
                                                创建时间<span id="oC7" class="orderTip"></span>
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
            </td>
        </tr>
    </table>
    
    
   </div> 
     <div id="divEditCheckItem" runat="server" 
    style="background: #fff; padding: 10px; width: 800px; z-index:300; position: absolute;top: 1%; left: 9%;  display:none  ">    
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
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle"  style="display:none; float:left"/>
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand; float:left"   onclick="DoSave();"/>
       
        <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="true" alt="确认" id="btnCheck" style="cursor:hand; float:left"   onclick="DoCheck();"/>  
          <img src="../../../Images/Button/UnClick_bc.jpg" id="btnUncheckSave"  runat="server"  alt="保存"  style="cursor:hand; display:none; float:left "   />
            <img src="../../../Images/Button/UnClick_qr.jpg" runat="server"  alt="确认" id="btnUncheck" style="cursor:hand;display:none; float:left"    />
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand; float:left"   onclick="DoBack();"/>                        
            </td>
            <td align="right" valign="top" class="tdColInput">
            
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="Table2"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">自我鉴定编号<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:CodingRule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none " disabled ="true" />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">主题<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="25" size="25" /></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">  期间类型<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                    <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1">
                                            <option value="1">月</option>
                                            <option value="2">季</option>
                                             <option value="3">半年</option>
                                            <option value="4">年</option>
                                              <option value="5">临时</option>
                                        </select></td>
                                    <td class="tdColTitle" width="10%"><div id="getNull">考核周期<span style="color:Red ">*</span></div>  </td>
                                    <td class="tdColInput" width="23%">
                                                                <select id="selTaskYear" runat="server" name="D3" style="display:block; float:left">
                                                </select>
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
                                    <td height="20" class="tdColTitle" width="10%">开始日期<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput"  onclick="J.calendar.get();"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">结束日期<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput"  onclick="J.calendar.get();"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                 <tr>
                                    <td  class="tdColTitle" width="10%">工作内容</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                               
                                        <%-- <input    id="txtWorkContent"  maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" runat="server" />--%>
                                        <textarea id="txtWorkContent" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false "  ></textarea>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td  class="tdColTitle" width="10%">完成情况</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                      
                                         <%-- <input    id="" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
                                           <textarea id="txtComplete" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false " ></textarea>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">后续工作</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                    
                                                             <%--   <input    id="" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
                                                                <textarea id="txtAfterWork" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3" onpaste= "return   false "  ></textarea>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">存在不足 </td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                
                                         <%--  <input    id="" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
                                            <textarea id="txtDefects" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false " ></textarea>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">存在困难</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                  
                                         <%--    <input    id="txtProblems" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
                                          <textarea id="txtProblems" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false " ></textarea>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">个人建议</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                      <textarea id="txtAdvices" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false " ></textarea>
                                        <%--   <input    id="" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注说明</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                   <textarea id="txtRemark" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false " ></textarea>
                                        <%-- <input    id="txtRemark" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
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
