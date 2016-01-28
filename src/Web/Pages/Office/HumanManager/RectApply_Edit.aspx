<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RectApply_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_RectApply_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/Human/DeptQuarterSelSingleElection.ascx" tagname="DeptQuarterSel" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建申请招聘</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    
    <script src="../../../js/Calendar/lhgcore.js" type="text/javascript"></script>
        <script src="../../../js/Calendar/lhgcalendar/lhgcalendar.js" type="text/javascript"></script>
	 <script type="text/javascript">J.califrm = true;</script>
	 
<script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
       <style type="text/css">
        .style1
        {
            background-color: #FFFFFF;
            height: 26px;
        }
    </style>
        <script src="../../../js/common/TreeView.js" language="javascript"  type="text/javascript" ></script>
</head>
<body>
<form id="frmMain" runat="server">
     <uc3:DeptQuarterSel ID="DeptQuarterSel1" runat="server" />
  
                               
<input id="hidIsliebiao" type="hidden"  runat="server"/>
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建招聘申请</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand; float:left; vertical-align:top"  onclick="DoSave();"/>
                                    
                                    <span id="GlbFlowButtonSpan" style="float :left; vertical-align:top" runat="server" visible="false" ></span>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand;float:left; vertical-align:top" />
                                </td>
                                <td align="right" class="tdColInput">
                                   
                              <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="返回" id="btnPrint" onclick="Print();" style="cursor:hand; " />
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
                            <td colspan="2" height="0"   >
                                <input type="hidden" id="hidEditFlag" runat="server"   />
                                <input type="hidden" id="hidModuleID" runat="server" />
                                <input type="hidden" id="hidSearchCondition" runat="server"  />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">申请编号 <span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="23%">
                                <div id="divCodeRule" runat="server" style="float :left ">
                                    <uc1:CodingRule ID="codruleRectApply" runat="server" />
                                </div>
                                <div id="divRectApplyNo" runat="server" class="tdinput" style="float :left "  disabled ="true"></div>
                            </td>
                            <td height="20" align="right" class="tdColTitle" width="10%"  >申请部门<span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="23%">
                                <asp:TextBox ID="DeptApply"  onclick="alertdiv('DeptApply,hidDeptID');"    
                                    ReadOnly="true" Width="95%" CssClass="tdinput" runat="server"        ></asp:TextBox>
                                <input type="hidden" id="hidDeptID" runat="server"/>
                            </td>
                            <td height="20" align="right" class="tdColTitle" width="10%">现有人数</td>
                            <td height="20" class="tdColInput" width="24%">
                                <input id="txtNowNum" type="text"   style=" width:99%"   maxlength ="9" class="tdinput"    runat="server"   SpecialWorkCheck="现有人数"  />
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">编制定额</td>
                            <td height="20" class="tdColInput">
                               <input id="txtMaxNum" type="text"   style=" width:99%"   maxlength ="9" class="tdinput"  runat ="server" size="10"  SpecialWorkCheck="编制定额"/>
                            </td>
                            <td height="20" align="right" class="tdColTitle" ></td>
                            <td height="20" class="tdColInput">
                        
                      
                            </td>
                            <td height="20" align="right" class="tdColTitle"> </td>
                            <td height="20" class="tdColInput" >
                             
                            </td>
                        </tr>
             
                        
                        <tr>

                            <td height="20" align="right" class="tdColTitle" >招聘原因</td>
                            <td height="20" class="tdColInput" colspan="5">
                                <asp:TextBox ID="txtRequstReason" runat="server" TextMode="MultiLine" Width="99%" Height="30" CssClass="tdinput" MaxLength="100" SpecialWorkCheck="招聘原因"  onkeyup="textcontrol(this.id,100)"></asp:TextBox>
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
                        <span class="Blue">人员需求</span>
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
                        &nbsp;<img src="../../../images/Button/Show_add.jpg"  title="添加" style="cursor:pointer" onclick="AddGoal();" id="btnAddGoal" runat="server" visible="true"/>
                        <img src="../../../images/Button/Show_del.jpg"  title="删除" style="cursor:pointer" onclick="DeleteRows('tblRectGoalDetailInfo');" id="btnDeleteRows" visible="true" runat="server" />
                         
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
                                <span class="Blue">合计信息</span>
                            </td>
                            <td align="right" valign="top">
                                <div id='divUserInfo'>
                                    <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblUserInfo','divUserInfo')"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" id="tblUserInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%"> 需求人数(人) </td>
                            <td height="20" class="tdColInput"  width="23%">
                              <input id="txtRequireNum" type="text"   style=" width:99%"   maxlength ="5" class="tdinput"  runat ="server" size="10"  readonly ="readonly"/>
                            </td>
                          <td height="20" align="right" class="tdColTitle" width="10%">  </td>
                            <td height="20" class="tdColInput"  width="23%">
                              
                            </td>
                                <td height="20" align="right" class="tdColTitle" width="10%">  </td>
                            <td height="20" class="tdColInput"  width="23%">
                              
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
                                <span class="Blue">备注</span>
                            </td>
                            <td align="right" valign="top">
                                <div id='divRemarkInfo'>
                                    <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblRemarkInfo','divRemarkInfo')"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" id="tblRemarkInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                         <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">单据状态 </td>
                            <td height="20" class="tdColInput"  width="23%">
                            <asp:TextBox ID="txtBillStatus" runat="server" CssClass="tdinput" ReadOnly="true" Text ="制单"></asp:TextBox>
                            </td>
                            <td height="20" align="right" class="tdColTitle"  width="10%">制单人   </td>
                            <td height="20" class="tdColInput"  width="23%">
                              <asp:TextBox ID="txtCreator" runat="server"  CssClass="tdinput" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td height="20" align="right" class="tdColTitle"  width="10%">制单日期</td>
                            <td height="20" class="tdColInput"  width="24%">
                               <asp:TextBox ID="txtCreateDate" runat="server"  CssClass="tdinput" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                            <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">确认人 </td>
                            <td height="20" class="tdColInput"  width="23%">
                                <asp:TextBox ID="txtConfirmor" runat="server"  CssClass="tdinput" ReadOnly="true"></asp:TextBox>
                                <input  type="hidden" id ="hidConfirmor" runat="server"/>
                            </td>
                            <td height="20" align="right" class="tdColTitle"  width="10%">确认日期   </td>
                            <td height="20" class="tdColInput"  width="23%">
                            <asp:TextBox ID="txtConfirmDate" runat="server"  CssClass="tdinput" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td height="20" align="right" class="tdColTitle"  width="10%">结单人</td>
                            <td height="20" class="tdColInput"  width="24%">
                             <asp:TextBox ID="txtCloser" runat="server"  CssClass="tdinput" ReadOnly="true"></asp:TextBox>
                                    <input  type="hidden" id ="hidCloser" runat="server"/>
                            </td>
                        </tr>
                           <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">结单日期 </td>
                            <td height="20" class="tdColInput"  width="23%">
                                <asp:TextBox ID="txtCloseDate" runat="server"  CssClass="tdinput" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td height="20" align="right" class="tdColTitle"  width="10%">    经手人</td>
                            <td height="20" class="tdColInput"  width="23%">
                              <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtPrincipal');"
                                runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                                  <input type="hidden" id="txtPrincipal" runat="server" />
                            </td>
                            <td height="20" align="right" class="tdColTitle"  width="10%"> </td>
                            <td height="20" class="tdColInput"  width="24%">
                               
                            </td>
                        </tr> 
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">备注</td>
                            <td class="tdColInput" colspan="5">
                                <asp:TextBox ID="txtRemark" CssClass="tdinput" runat="server" TextMode="MultiLine" Height="50" Width="99%" MaxLength="250" onkeyup="textcontrol(this.id,250)" ></asp:TextBox>
                            </td>
                             
                               
                           
                        </tr>
                    </table>
                </td>
            </tr> 
            <tr><td colspan="2" height="10">
            
               </td></tr>  
        </table>
        <!-- </div> -->
    </td>
</tr>
</table>
<div id="divEditCheckItem" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:300; position: absolute;top: 20%; left: 15%; display:none ; ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblDepttemInfo">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr><td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                            <tr>
                                <td height="30" class="tdColInput">
                                    <img src="../../../Images/Button/Show_Change.jpg" runat="server" visible="true" alt="确定" id="imgCheck" style="cursor:hand"   onclick="check();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="Img2" runat="server" style="cursor:hand"   onclick="outContent();"/>
                                </td>
                                <td height="30" class="tdColInput" align="right">
                                   <%-- <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" visible="false" id="btnPrint" style="cursor:hand" height="25" />--%>
                                </td>
                            </tr>
                        </table></td></tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1"  >
                        <tr>
                            <td  colspan="2"> 
                                <input id="hidEditControl" type="hidden" />
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="Table2" style="display:block">
                                    <tr>
                                    
                                        <td height="20" class="tdColInput" width="35%" colspan="4">
                                            <textarea id="txtEdit" cols="20" rows="6" style="width:100%"></textarea>
                                        </td>
                                      
                                    </tr>
                                   
                                   
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2" height="10"></td></tr>
                    </table>
                <!-- </div> -->
                </td>
            </tr>
        </table>    
    </div>
     <div id="divCulture" runat="server"  style="display:none "><uc1:CodeType ID="ddlCulture" runat="server"/></div>
                                    <div id="divProfessional" runat="server" style="display:none "><uc1:CodeType ID="ddlProfessional" runat="server" /></div>
                          <div id="divWorkAge" runat="server" style="display:none ">   
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
                              <div id="divWorkNature" runat="server" style="display:none ">   
                                    <option value="1" selected>不限</option>
                                     <option value="2"  >全职</option>
                                      <option value="3">兼职</option>
                                      <option value="4"  >实习</option> 
                               </div>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
<!-- 单据状态 -->
<input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="1" runat="server"/>
<!-- 单据编号 -->
<input type="hidden" id="hidBillNo" name="hidBillNo" runat="server"/>
<!-- Start 流程处理-->
<uc1:FlowApply ID="FlowRectApply" runat="server" />
<!-- End 流程处理-->
<input type="hidden" id="txtIndentityID" value="0" runat="server" />
</form>
    <script type="text/javascript">
        var glb_BillTypeFlag = <%=XBase.Common.ConstUtil.BILL_TYPEFLAG_HUMAN %>;
        var glb_BillTypeCode = <%=XBase.Common.ConstUtil.BILL_TYPECODE_HUMAN_RECT_APPLY %>;
        var glb_BillID = document.getElementById("txtIndentityID").value;//单据ID
        var glb_IsComplete = true;
        var FlowJS_HiddenIdentityID ='txtIndentityID';//自增长后的隐藏域ID
        var FlowJs_BillNo ='hidBillNo'; //当前单据编码名称
        var FlowJS_BillStatus ='hiddenBillStatus';       //单据状态ID
    </script>
    <script src="../../../js/common/Flow.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/RectApply_Edit.js" type="text/javascript"></script>
</body>
</html>
