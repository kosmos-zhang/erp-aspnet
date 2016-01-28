<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformancePersonal.aspx.cs" Inherits="Pages_Office_HumanManager_PerformancePersonal" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建自我鉴定</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformancePersonal.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        #txtWorkContent
        {
            width: 438px;
            height: 29px;
        }
        #txtWorkContent0
        {
            width: 438px;
            height: 80px;
        }
        #txtWorkContent1
        {
            width: 438px;
            height: 80px;
        }
        #txtWorkContent2
        {
            width: 438px;
            height: 80px;
        }
        #txtWorkContent3
        {
            width: 438px;
            height: 80px;
        }
        #txtWorkContent4
        {
            width: 438px;
            height: 80px;
        }
        #txtWorkContent5
        {
            width: 438px;
            height: 80px;
        }
        #txtComplete
        {
            width: 437px;
            height: 34px;
        }
        #txtAfterWork
        {
            width: 435px;
            height: 31px;
        }
        #txtDefects
        {
            width: 431px;
            height: 35px;
        }
        #txtProblems
        {
            width: 429px;
            height: 28px;
        }
        #txtAdvices
        {
            width: 431px;
            height: 26px;
        }
        #txtRemark
        {
            width: 431px;
            height: 24px;
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
    
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
   
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
 <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">新建自我鉴定</td>
                    </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" style="float:left " />
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand; float:left"  onclick="DoSave();"/>   
       <img src="../../../Images/Button/UnClick_bc.jpg" runat="server" visible="false" alt="保存" id="btnUncheckSave" style="cursor:hand; display:none; float:left"     />
       <img src="../../../Images/Button/Bottom_btn_confirm.jpg" runat="server" visible="false" alt="确认" id="btnCheck" style="cursor:hand; float:left"   onclick="DoCheck();"/>
     <img src="../../../Images/Button/UnClick_qr.jpg" runat="server" visible="false" alt="确认" id="btnUncheck" style="cursor:hand; display:none; float:left"   />
       <img id="btnBack" runat="server" alt="确认"   onclick="DoBack();"   src="../../../Images/Button/Bottom_btn_back.jpg" style="cursor:hand; float:left"   visible="true" /></td>
            <td align="right" valign="top" class="tdColInput">
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
                                    <td height="20" class="tdColTitle" width="10%">编号<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:CodingRule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none " disabled ="true" />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">主题<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="25" size="50"  class="tdinput"/></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">  鉴定周期分类<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                    <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1">
                                            <option value="1">月</option>
                                            <option value="2">季</option>
                                             <option value="3">半年</option>
                                            <option value="4">年</option>
                                              <option value="5">临时</option>
                                        </select></td>
                                    <td class="tdColTitle" width="10%">
                                  <div id="getNull"> 考核周期<span style="color:Red ">*</span></div> </td>
                                    <td class="tdColInput" width="23%">
                                <select id="selTaskYear" runat="server" name="D3" style="display:block; float:left">
                                                </select>
                                      <div  id="dvTaslNum">
                                
                                                <select id="selTaskNum" runat="server" name="D2" style="display:block; float:left">
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
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput"  onclick="J.calendar.get();" Width="398px" ReadOnly="true"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">结束日期<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput"       onclick="J.calendar.get();"    Width="400px"  ReadOnly="true"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                 <tr>
                                    <td  class="tdColTitle" width="10%">工作内容</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                               
                                        <%-- <input    id="txtWorkContent"  maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" runat="server" />--%>
                                        <textarea id="txtWorkContent" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"   onpaste= "return   false " ></textarea>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td  class="tdColTitle" width="10%">完成情况</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                      
                                         <%-- <input    id="" maxlength ="250" onkeyup="textcontrol(this.id,500)"   style="width:100%;  " class="tdinput" />--%>
                                           <textarea id="txtComplete" rows="3" class="tdinput" style="width: 100%; height:50px" onkeyup="textcontrol(this.id,500)"  runat="server"  cols="3"  onpaste= "return   false "  ></textarea>
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
