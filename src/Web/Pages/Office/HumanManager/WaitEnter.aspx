<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WaitEnter.aspx.cs" Inherits="Pages_Office_HumanManager_WaitEnter" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>待入职</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/WaitEnter_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
</head>
<body>
<form id="frmMain" runat="server">
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td width="10%" height="20" class="tdColTitle">编号</td>
                                    <td width="23%" class="tdColInput">
                                        <asp:TextBox ID="txtEmployeeNo" Width="95%" SpecialWorkCheck="编号" CssClass="tdinput" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="tdColTitle">姓名</td>
                                    <td width="23%" class="tdColInput">
                                        <asp:TextBox ID="txtEmployeeName" Width="95%"  SpecialWorkCheck="姓名" CssClass="tdinput" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="tdColTitle">性别</td>
                                    <td width="24%" class="tdColInput">
                                        <asp:DropDownList ID="ddlSex" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">男</asp:ListItem>
                                            <asp:ListItem Value="2">女</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">学历</td>
                                    <td class="tdColInput">
                                        <uc1:CodeType ID="ddlCulture" runat="server" />
                                    </td>
                                    <td class="tdColTitle">毕业院校</td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txtSchoolName" runat="server" SpecialWorkCheck="毕业院校" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="tdColTitle">应聘职位</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="ddlQuarter" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" class="tdColTitle">人员类型</td>
                                    <td width="24%" class="tdColInput">
                                        <asp:DropDownList ID="DDLFlag" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="2">人才储备</asp:ListItem>
                                            <asp:ListItem Value="3">离职人员</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdColTitle"></td>
                                    <td class="tdColInput">
                                    </td>
                                    <td class="tdColTitle"></td>
                                    <td class="tdColInput">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidContractModuleID" runat="server"/>
                                        <input type="hidden" id="hidEmplyModuleID" runat="server"/>
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearch()'/>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr><td colspan="2" height="5"></td></tr>
        <tr>
            <td colspan="2">
                <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">未入职人员列表</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td  bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/sb_wqda.jpg" title="完全档案" visible="false" id="btnAllInfo" runat="server" onclick="DoInputAllInfo()" style='cursor:pointer;'/>
                                        <img src="../../../images/Button/sb_htlr.jpg" title="合同录入" visible="false" id="btnInputContract" runat="server" onclick="DoInputContract()" style='cursor:pointer;'/>
                                        <img src="../../../images/Button/btn_ksrz.jpg" title="快速入职" visible="false" id="btnEnter" runat="server" onclick="DoEnter(1)" style='cursor:pointer;'/>
                                        <img src="../../../images/Button/btn_msrz.jpg" title="面试入职"  id="btnview1" visible="false" runat="server" onclick="DoEnter(2)" style='cursor:pointer;'/>
                                          <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"  AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--<div style="height:252px;overflow-y:scroll;">--%>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg">选择</th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;"  onclick="OrderBy('EmployeeNo','oC0');return false;">
                                                人员编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;"  onclick="OrderBy('EmployeeName','oC1');return false;">
                                                姓名<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;"  onclick="OrderBy('SexName','oC2');return false;">
                                                性别<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;"  onclick="OrderBy('QuarterName','oC3');return false;">
                                                应聘岗位<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <%--<th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;"  onclick="OrderBy('Birth','oC4');return false;">
                                                出身年月<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>--%>
                                        
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;" onclick="OrderBy('Contact','oC7');return false;">
                                                联系方式<span id="oC7" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;" onclick="OrderBy('CultureLevelName','oC8');return false;">
                                                学历<span id="oC8" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;" onclick="OrderBy('SchoolName','oC9');return false;">
                                                毕业院校<span id="oC9" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;" onclick="OrderBy('ProfessionalName','oC10');return false;">
                                                专业<span id="oC10" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;" onclick="OrderBy('Flag','oC11');return false;">
                                                人员类型<span id="oC11" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="text-align:center;width:95%;" onclick="OrderBy('FinalResult','Span1');return false;">
                                                复试结果<span id="Span1" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <%--</div>--%>
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
                                                        每页显示<input name="txtShowPageCount" type="text" maxlength="4" id="txtShowPageCount" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" maxlength="4" id="txtToPage" size="3"/>页&nbsp;&nbsp;
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
        
    <div id="divEnterInput" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:1; position: absolute;top: 20%; left: 15%;display:none;">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblEnterInfo">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="30" class="tdColInput">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="DoSave();"/>
                                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" />
                                        </td>
                                        <td align="right" class="tdColInput">
                                            <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="false" alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">人员编号<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input type="hidden" id="txtEnterEmployeeID" />
                                            <asp:TextBox ID="txtEnterEmployeeNo" runat="server" CssClass="tdinput" Enabled="false" Width="95%"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">姓名<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <asp:TextBox ID="txtEnterEmployeeName" runat="server" CssClass="tdinput" Enabled="false" Width="95%"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">证件号码</td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <asp:TextBox ID="txtEnterEmployeeCardID" runat="server" CssClass="tdinput" Enabled="false" Width="95%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">岗位<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlEnterQuarter" runat="server"></asp:DropDownList>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">部门<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="DeptEnter" runat="server" onclick="alertdiv('DeptEnter,txtDept');" ReadOnly="true" Width="95%"  CssClass="tdinput"></asp:TextBox>
                                            <input type="hidden" id="txtDept" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" >岗位职等<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <uc1:CodeType ID="ddlEnterQuarterLevel" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">职称</td>
                                        <td height="20" class="tdColInput">
                                            <uc1:CodeType ID="ddlEnterPosition" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">职务</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtPositionTitle" CssClass="tdinput" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">入职时间<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <input id="txtSystemDate" runat="server" type="hidden" />
                                            <asp:TextBox ID="txtEnterDate" Width="95%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})"></asp:TextBox>
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
        
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
</form>
</body>
</html>
