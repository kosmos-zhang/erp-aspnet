<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RectInterview_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_RectInterview_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建面试</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script src="../../../js/common/Flow.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/RectInterview_Edit.js" type="text/javascript"></script>
</head>
<body>
<form id="frmMain" runat="server">
<input id="hidIsliebiao" type="hidden" runat="server"  />

<input id="hidIsSearch" type="hidden" runat="server" value="1" />
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td valign="top" colspan="2">
            
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建面试</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>
                                 
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand"   />
                                </td>
                                <td align="right" class="tdColInput">
                                  <%--  <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"   />--%>
                                </td>
                            </tr>
                        </table>
                        <!-- 单据状态 -->
                        <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                        <!-- 单据编号 -->
                        <input type="hidden" id="hidBillNo" name="hidBillNo" runat="server"/>
                        <!-- Start 流程处理-->
                        <uc1:FlowApply ID="FlowRectApply" runat="server" />
                        <!-- End 流程处理-->
                        <input type="hidden" id="hidInterviewID" value="" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td>
<!-- <div style="height:500px;overflow-y:scroll;"> -->
<table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
    <tr>
        <td  colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                        <table width="100%" align="center" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>基本信息</td>
                                <td align="right">
                                    <div id='divBaseInfo'>
                                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblBaseInfo','divBaseInfo')"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" height="0">
                        <input type="hidden" id="hidModuleID" runat="server" />
                        <input type="hidden" id="hidEmployeeModuleID" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">面试记录编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <div id="divCodeRule" runat="server" style="float:left; width:80%">
                            <uc1:CodingRule ID="codeRule" runat="server" />
                        </div>
                        <div id="divCodeNo" runat="server" class="tdinput" style="display:none;float:left" disabled ="true"></div>
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">对应招聘计划</td>
                    <td height="20" class="tdColInput" width="23%">
                        <asp:DropDownList ID="ddlRectPlan" runat="server"  Width="120px" ></asp:DropDownList>
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">姓名&nbsp;<a href="#" onclick="AddEmployee();">添加简历</a><span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="24%">
                        <asp:TextBox ID="txtStaffName" runat="server" ReadOnly="true" Width="95%" onclick="DoSelectReserve();" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="hidStaffName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="25" align="right" class="tdColTitle">招聘方式</td>
                    <td height="20" class="tdColInput">
                        <asp:DropDownList ID="ddlRectType" runat="server" style="margin-left:2px;" Width="120px">
                                   <asp:ListItem Value ="" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value ="1" Text="公开招聘"></asp:ListItem>
                         <asp:ListItem Value ="2" Text="推荐"></asp:ListItem>
                          <asp:ListItem Value ="3" Text="内部竞聘"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td height="20" align="right" class="tdColTitle">应聘岗位<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                           <div id="divQuarter">
                        <asp:DropDownList ID="ddlQuarter" runat="server" style="margin-left:2px;" Width="120px"></asp:DropDownList>
                        </div>
                    </td>
                    <td height="20" align="right" class="tdColTitle">面试模板<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <div id="divTemplate">
                            <asp:DropDownList ID="ddlTemplate" runat="server" style="margin-left:2px;" Width="120px"></asp:DropDownList>
                        </div>
                    </td>
                </tr>
                
                
                <tr>
                    <td height="20" align="right" class="tdColTitle">附件 </td>
                    <td height="20" class="tdColInput">
                               <table>
                            <tr>
                                <td>
                                
                                    <div id="divUploadAttachment" runat="server">
                                        <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                                    </div>
                                    <div id="divDealAttachment" runat="server" style="display:none;">
                                        <a href="#" onclick="DealAttachment('download');">
                                            <span id='spanAttachmentName' runat="server"></span>
                                        </a>&nbsp;
                                        <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfAttachment" runat="server" />
                        <asp:HiddenField ID="hfPageAttachment" runat="server" />
                    </td>
                    <td height="20" align="right" class="tdColTitle"> </td>
                    <td height="20" class="tdColInput">
                        
                    </td>
                    <td height="20" align="right" class="tdColTitle"></td>
                    <td height="20" class="tdColInput">
              
                    </td>
                </tr>
                
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>
     <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">初试记录</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divInter'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('Table1','divInter')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="2"></td></tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="Table1" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="25" align="right" class="tdColTitle" width="10%">日期<span class="redbold">*</span></td>
                    <td height="25" class="tdColInput" width="23%">
                        <asp:TextBox ID="txtInterviewDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtInterviewDate')})"></asp:TextBox>
                    </td>
                    <td height="25" align="right" class="tdColTitle" width="10%">面试方式</td>
                    <td height="25" class="tdColInput" width="23%">
                        <uc1:CodeType ID="ddlInterviewType" runat="server" />
                    </td>
                    <td height="25" align="right" class="tdColTitle" width="10%">面试地点</td>
                    <td height="25" class="tdColInput" width="24%">
                        <asp:TextBox runat="server" CssClass="tdinput" MaxLength="50" Width="95%" ID="txtInterviewPlace"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="25" align="right" class="tdColTitle">初试综合评价</td>
                    <td height="25" class="tdColInput" colspan="3">
                            <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtInterviewNote" ></asp:TextBox>
                    </td>
                    
                         
                 
                    <td height="25" align="right" class="tdColTitle">初试结果</td>
                    <td height="25" class="tdColInput">
                        <asp:DropDownList ID="ddlInterviewResult" runat="server" CssClass="tdinput">
                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                            <asp:ListItem Value="1">列入考虑</asp:ListItem>
                            <asp:ListItem Value="2">不予考虑</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
     
     
            </table>
        </td>
    </tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">复试记录</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divInterviewResultInfo'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblInterviewResultInfo','divInterviewResultInfo')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="2"></td></tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblInterviewResultInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="25" align="right" class="tdColTitle" width="10%">日期</td>
                    <td height="25" class="tdColInput" width="23%">
                     <asp:TextBox ID="txtCheckDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtDebutDate')})"></asp:TextBox>
                    </td>
                    <td height="25" align="right" class="tdColTitle" width="10%">面试方式</td>
                    <td height="25" class="tdColInput" width="23%">
                        <uc1:CodeType ID="ddlCheckType" runat="server" />
                    </td>
                    <td height="25" align="right" class="tdColTitle" width="10%">面试地点</td>
                    <td height="25" class="tdColInput" width="24%">
                     <asp:TextBox runat="server" CssClass="tdinput" MaxLength="50" Width="95%" ID="txtCheckPlace"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="25" align="right" class="tdColTitle">综合素质</td>
                    <td height="25" class="tdColInput">
                           <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtManNote" ></asp:TextBox>
                    </td>
                    <td height="25" align="right" class="tdColTitle">专业知识</td>
                    <td height="25" class="tdColInput">
                <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtKnowNote" ></asp:TextBox>
                    </td>
                    <td height="25" align="right" class="tdColTitle">工作经验</td>
                    <td height="25" class="tdColInput">
                       <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtWorkNote" ></asp:TextBox>
                    </td>
                </tr>
                   <tr>
                    <td height="25" align="right" class="tdColTitle">要求待遇</td>
                    <td height="25" class="tdColInput">
                                               <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtSalaryNote" ></asp:TextBox>
                    </td>
                  
                    <td height="25" align="right" class="tdColTitle">复试综合评价  </td>
                    <td height="25" class="tdColInput">
                                           <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtCheckNote" ></asp:TextBox>
                    </td>
                      <td height="25" align="right" class="tdColTitle">备注</td>
                    <td height="25" class="tdColInput">
                                     <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtRemark" ></asp:TextBox>
                    </td>
                </tr>
                   <tr>
                 <td height="25" align="right" class="tdColTitle">可提供的待遇</td>
                    <td height="25" class="tdColInput">
                        <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtOurSalary" ></asp:TextBox>
                    </td>
              
                    <td height="25" align="right" class="tdColTitle">确认工资   </td>
                    <td height="25" class="tdColInput">
                     <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtFinalSalary"  onkeydown='Numeric_OnKeyDown();'  onchange="Number_round(this,2)"></asp:TextBox>
                    </td>
                          <td height="25" align="right" class="tdColTitle"> 其他方面</td>
                    <td height="25" class="tdColInput">
              <asp:TextBox runat="server" CssClass="tdinput" MaxLength="300" Width="95%" ID="txtOtherNote" ></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                      <td height="25" align="right" class="tdColTitle">复试结果</td>
                    <td height="25" class="tdColInput">
                        <asp:DropDownList  ID="ddlFinalResult" runat="server" >
                       <asp:ListItem Value ="">--请选择--</asp:ListItem>
                        <asp:ListItem Value="0">不予考虑</asp:ListItem>
                        <asp:ListItem Value="1">拟予试用</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                  
                    <td height="25" align="right" class="tdColTitle">  </td>
                    <td height="25" class="tdColInput">
          
                    </td>
                    <td height="25" align="right" class="tdColTitle">    </td>
                    <td height="25" class="tdColInput">
                
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>
    <tr>
        <td valign="top" ><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
        <td align="right" valign="top"><img src="../../../images/Main/LineR.jpg" width="122" height="7" /></td>
    </tr>
    <tr><td colspan="2" height="4"></td></tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">评测记录</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divElemScoreInfo'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblElemScoreInfo','divElemScoreInfo')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblElemScoreInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td class="tdColInput">
                        <div id="divElemScoreDetail" runat="server"></div>
                    </td>
                </tr>
            </table>
            <table><tr><td height="10"></td></tr></table>
        </td>
    </tr> 
</table>
<!-- </div> -->
</td>
</tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
</form>
    
</body>
</html>
