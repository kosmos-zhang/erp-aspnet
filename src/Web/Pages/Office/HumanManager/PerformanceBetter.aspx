<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceBetter.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceBetter" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建绩效改进计划</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceBetter.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
      <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        #txtWorkContent
        {
            width: 438px;
            height: 80px;
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
            height: 82px;
        }
        #txtAfterWork
        {
            width: 435px;
            height: 86px;
        }
        #txtDefects
        {
            width: 431px;
            height: 92px;
        }
        #txtProblems
        {
            width: 429px;
            height: 74px;
        }
        #txtAdvices
        {
            width: 431px;
            height: 79px;
        }
        #txtRemark
        {
            width: 431px;
            height: 62px;
        }
        .style2
        {
            width: 26px;
        }
        .style3
        {
            width: 129px;
        }
        .style4
        {
            width: 118px;
        }
        #txtPerformTmNo
        {
            width: 159px;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server" >
<input id="hidSearchCondition" type="hidden"  runat="server"/>
<input id="hidIsliebiao" type="hidden" runat="server" />
<input id="hidModuleID" type="hidden" runat="server" />

<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
<div id="PerformanceTypeCheck" >
    <input id="hidEditFlag" type="hidden"  runat="server"/>
    <input id="hidElemID" type="hidden" />
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
 <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title"><div id="divTitle" runat="server">  新建绩效改进计划 </div></td>
                    </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="SaveRectPlanInfo();"/>
             <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true" alt="确认" id="btnCheck" style="cursor:hand"   onclick="DoBack();"/>                       
            </td>
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
                                    <td height="20" class="tdColTitle" width="10%">改进计划编号<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo" runat="server" style="width:auto; float:left">
                                            <uc2:CodingRule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <div id="divRectPalnNo" runat="server"   
                                            style=" float:left; width: 109px; height: 22px;"></div>
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">改进计划主题<span style="color:Red ">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        <input id="txtTitle" type="text" maxlength="50" size="50"  runat="server" class="tdinput"/></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">  创建人</td>
                                    <td class="tdColInput" width="23%">
                                       <div id="divCreater" runat="server"></div></td>
                                    <td class="tdColTitle" width="10%">创建日期</td>
                                    <td class="tdColInput" width="23%">
                                
                                      <div  id="dvCreateDate" runat="server"> </div></td>
                                </tr>  
                               
                                 <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注说明</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                         
                                         <input id="txtPlanRemark"  maxlength ="1024" style="width:100%;  "   runat="server" class="tdinput"/>
                                    </td>
                                </tr> 
                                <tr>
                                <td colspan ="4"></td>
                               
       <table id="tbDetail"  width="100%" border="0" cellspacing="0" cellpadding="3"  class="tab_a">
       <thead >
       
       <td  colspan="6"  >
   
      <%--  <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="true" id="btnNew" runat="server" style="cursor:hand" height="25" onclick="showEdit();"/>--%>
        <img src="../../../images/Button/Main_btn_add.jpg" alt="增加" visible="false" id="btnAddPublish" runat="server" onclick="AddPublish();"  style='cursor:pointer;'  />
        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDeleteRows" runat="server" onclick="DeleteRows('tbDetail');"  style='cursor:pointer;'   />&nbsp;&nbsp;
        </td>
       
       </thead>
        <tr ><th width="5%"></th><th  width="10%"  >员工<span style="color:Red ">*</span></th><th  >待改进绩效</th>
            <th >完成目标</th><th>完成期限</th><th>核查人</th><th>核查结果</th><th>核查时间</th><th>备注</th></tr>
      <tr><td><div runat="server" id="divRectPublishDetail"></div></td></tr>  
       
        </table>
        
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
