<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RectPlan_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_RectPlan_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Human/DeptQuarterSelSingleElection.ascx" tagname="DeptQuarterSel" tagprefix="uc3" %>
<%@ Register Src="../../../UserControl/Human/SelectChannels.ascx" TagName="TaskUC" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建招聘活动</title>
        <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
       <script src="../../../js/common/check.js" type="text/javascript"></script>
           <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
        <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/RectPlan_Edit.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
<script src="../../../js/common/TreeView.js" language="javascript"  type="text/javascript" ></script>
    <style type="text/css">
        .style1
        {
            background-color: #FFFFFF;
            height: 26px;
        }
    </style>


</head>
<body>
<form id="frmMain" runat="server">
 <uc3:DeptQuarterSel ID="DeptQuarterSel1" runat="server" />
 <uc3:TaskUC ID="TaskUC1" runat="server" />
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建招聘计划</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="SaveRectPlanInfo();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" alt="返回" id="btnBack" visible="true" onclick="DoBack();" style="cursor:hand"   />
                                 </td>
                                <td align="right" class="tdColInput">
       <%--                             <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" visible="true" runat="server" id="btnPrint" style="cursor:hand"  />--%>
                                </td>
                            </tr>
                        </table>
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
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>基本信息
                            
                        </td>
                                <td align="right">
                                    <div id='divCompanyInfo'>
                                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblCompanyInfo','divCompanyInfo')"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="2" height="4"> 
                        <input type="hidden" id="hfModuleID" runat="server" />
                        <input type="hidden" id="hidEditFlag" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                        <div id="divCulture" runat="server"><uc1:CodeType ID="ddlCulture" runat="server"/></div>
                        <div id="divProfessional" runat="server"><uc1:CodeType ID="ddlProfessional" runat="server" /></div>
                          <div id="divWorkNature" runat="server" style="display:none ">   
                                    <option value="0" selected>--请选择--</option>
                                     <option value="1"  >在读学生</option>
                                    <option value="2">应届毕业生</option>
                                    <option value="3">一年以内</option>
                                    <option value="4">一年以上</option>
                                    <option value="5">三年以上</option>
                                    <option value="6">五年以上</option>
                                    <option value="7">十年以上</option>
                                    <option value="8">二十年以上</option>
                                    <option value="9">退休人员</option>
                               </div>
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblCompanyInfo" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">招聘计划编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <div id="divCodeRule" runat="server"  style="float :left">
                            <uc1:CodingRule ID="codruleRectPlan" runat="server" />
                        </div>
                        <input id="divRectPalnNo"  class="tdinput" type="text"  maxlength ="50" runat="server"  disabled ="true"/>
                        
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">主题<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <asp:TextBox ID="txtTitle" MaxLength="50" Width="90%" CssClass="tdinput" runat="server"></asp:TextBox>
                    </td>
                      <td height="20" align="right" class="tdColTitle" width="10%">负责人<span class="redbold"  >*</span></td>
                    <td height="20" class="tdColInput">
                        <input type="hidden" id="txtPrincipal" runat="server" />
                        <asp:TextBox ID="UserPrincipalName" ReadOnly="true"  onclick="alertdiv('UserPrincipalName,txtPrincipal');" CssClass="tdinput" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    
                    
                </tr>
                <tr>
                  <td height="20" align="right" class="tdColTitle" width="10%">开始时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="24%">
                        <asp:TextBox ID="txtStartDate"  Width="90%" ReadOnly="true" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle">结束时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                     <asp:TextBox ID="txtEndDate"  Width="90%" ReadOnly="true" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                    </td>
                    <td height="20" align="right" class="tdColTitle"> 计划状态<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                        <select id="ddlStatus" runat="server">
                            <option value="0">未完成</option>
                              <option value="1">已完成</option>
                        </select>
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
    <tr><td colspan="2" height="10"></td></tr>   
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">招聘目标</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRectGoalInfo'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblRectGoalInfo','divRectGoalInfo')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblRectGoalInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td class="style1"> 
                        <img src="../../../images/Button/Rs_choose.jpg"  title="从招聘申请中选择" style="cursor:pointer" onclick="AddGoalFromApply();"  id="btnAddGoalFromApply"  runat="server" visible="false"/>
                        <img src="../../../images/Button/Show_add.jpg"  title="添加" style="cursor:pointer" onclick="AddGoal();" id="btnAddGoal" runat="server" visible="false"/>
                        <img src="../../../images/Button/Show_del.jpg"  title="删除" style="cursor:pointer" onclick="DeleteRows('tblRectGoalDetailInfo');" id="btnDeleteRows" visible="false" runat="server" />
                         
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divRectGoalDetail" runat="server">                   
                        
                        </div>
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
                        <span class="Blue">信息发布时间和渠道</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRectPublishInfo'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblRectPublishInfo','divRectPublishInfo')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblRectPublishInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" class="tdColInput">
                        <img src="../../../images/Button/Show_add.jpg"  alt="添加" style="cursor:hand" onclick="AddPublish();" id="btnAddPublish" visible="false" runat="server" />
                        <img src="../../../images/Button/Show_del.jpg"   alt="删除" style="cursor:hand" onclick="DeleteRows('tblRectPublishDetailInfo');" id="btnDeleteR" visible="false" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divRectPublishDetail" runat="server"></div>
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
                        <span class="Blue">招聘预算</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRectP'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('Table1','divRectP')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="Table1" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">预算金额 </td>
                    <td height="20" class="tdColInput" width="23%">
                       <input id="txtPlanFee" type="text" runat="server"  onkeydown='Numeric_OnKeyDown();'  onchange="Number_round(this,2)"  class="tdinput" maxlength='8' style="width:98%"/>
 
                        
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%"> 预算说明</td>
                    <td height="20" class="tdColInput" width="23%">
                <asp:TextBox ID="txtFeeNote" MaxLength="200" Width="98%" CssClass="tdinput" runat="server"></asp:TextBox>
                    </td>
                      <td height="20" align="right" class="tdColTitle" width="10%">  </td>
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
                        <span class="Blue">合计信息</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRec'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('Table2','divRec')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="Table2" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">招聘人数 </td>
                    <td height="20" class="tdColInput" width="23%">
                    <input id="txtRequireNum" type="text"   style=" width:99%"   maxlength ="5" class="tdinput"  runat ="server" size="10"  readonly ="readonly"/>
 
                        
                    </td>
                    <td height="20" align="right" class="tdColTitle" width="10%">  </td>
                    <td height="20" class="tdColInput" width="23%">
         
                    </td>
                      <td height="20" align="right" class="tdColTitle" width="10%">  </td>
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
                        <span class="Blue">招聘小组成员</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divRecInfo'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('Table3','divRecInfo')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="Table3" style="display:block">
                <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">招聘小组成员 </td>
                    <td height="20" class="tdColInput"   colspan="5">
               <input type="hidden" id="txtJoinMan" runat="server" />
                        <asp:TextBox ID="UserJoinMan" ReadOnly="true"  onclick="alertdiv('UserJoinMan,txtJoinMan,1');" CssClass="tdinput" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                        <tr>
                    <td height="20" align="right" class="tdColTitle" width="10%">成员分工说明 </td>
                    <td height="20" class="tdColInput"    colspan="5">
                        <asp:TextBox ID="txtJoinNote" runat="server" TextMode ="MultiLine" Rows="6" 
                            Width="98%" CssClass="tdinput " Height="85px" MaxLength="1024"></asp:TextBox>
                        
                    </td>
                  
                    
                    
                </tr>
            </table>
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