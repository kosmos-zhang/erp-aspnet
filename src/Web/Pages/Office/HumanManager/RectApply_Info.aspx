<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RectApply_Info.aspx.cs" Inherits="Pages_Office_HumanManager_RectApply_Info" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Human/DeptQuarterSel.ascx" tagname="DeptQuarterSel" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>招聘申请列表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/RectApply_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
        <script src="../../../js/common/TreeView.js" language="javascript"  type="text/javascript" ></script>
</head>
<body>
<form id="frmMain" runat="server">
<input  type="hidden" id="hidOrderBy" runat="server"/>
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">申请编号</td>
                                    <td class="tdColInput" width="23%">
                                        <asp:TextBox ID="txtRectApplyNo" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"  SpecialWorkCheck="申请编号"></asp:TextBox>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">申请部门</td>
                                    <td class="tdColInput" width="23%">
                                        <asp:TextBox ID="DeptApply" onclick="alertdiv('DeptApply,hidDeptID');" ReadOnly="true" Width="95%" CssClass="tdinput" runat="server"></asp:TextBox>
                                        <input type="hidden" id="hidDeptID" runat="server"/>
                                    </td>
                                       <td class="tdColTitle">审批状态</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="ddlFlowStatus" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="0">待提交</asp:ListItem>
                                            <asp:ListItem Value="1">待审批</asp:ListItem>
                                          <%--  <asp:ListItem Value="2">审批中</asp:ListItem>--%>
                                            <asp:ListItem Value="3">审批通过</asp:ListItem>
                                            <asp:ListItem Value="4">审批不通过</asp:ListItem>
                                              <asp:ListItem Value="5"> 撤销审批</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <tr>
                                   
                                 
                                     <td height="20" class="tdColTitle">单据状态 </td>
                                    <td class="tdColInput">
                                     <asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">制单</asp:ListItem>
                                            <asp:ListItem Value="2">执行</asp:ListItem>
                                            <asp:ListItem Value="4">手工结单</asp:ListItem>
                                           <%--   <asp:ListItem Value="5">自动结单</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdColTitle" width="10%"> </td>
                                    <td class="tdColInput" width="24%">
                                  <%--  <uc3:DeptQuarterSel ID="DeptQuarterSel1" runat="server" />
                                            <input type="hidden" id="DeptQuarterHidden" />
                      <input id="DeptQuarter" type="text"   style=" width:99%"   maxlength ="30" class="tdinput"   runat ="server"   onclick="treeveiwPopUp.show()"/>--%>
                                    </td>
                                    <td class="tdColTitle"></td>
                                    <td class="tdColInput"></td>
                                </tr>             
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server"/>
                                        <input type="hidden" id="hidSearchCondition" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearch()' />
                                        <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="true" style='cursor:pointer;' onclick="ClearInputProxy()" width="52" height="23" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr><td colspan="2" height="10"><input type="hidden" id="hfModuleID" runat="server" /></td></tr>
        <tr><td colspan="2"  valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td></tr>
        <tr>
            <td colspan="2">
                <table width="99%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">招聘申请列表</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand" onclick="DoNew();"/>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'/>
                                           <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--<div style="height:252px;overflow-y:scroll;">--%>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg">
                                            选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')">
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('RectApplyNo','oC0');return false;">
                                                申请编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('CreateDate','oC1');return false;">
                                                制单时间<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('DeptName','oC2');return false;">
                                                申请部门<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                                                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('MaxNum','oC9');return false;">
                                                编制定额<span id="oC9" class="orderTip"></span>
                                            </div>
                                        </th>
                                                                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('NowNum','oC10');return false;">
                                                现有人数<span id="oC10" class="orderTip"></span>
                                            </div>
                                        </th>
                                         
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('RequireNum','oC4');return false;">
                                                总需求人数<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                     
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('FlowStatusName','oC7');return false;">
                                                审批状态<span id="oC7" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <%--<th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('BillStatus','Span1');return false;">
                                                单据状态<span id="Span1" class="orderTip"></span>
                                            </div>
                                        </th>--%>
                                    </tr>
                                </tbody>
                            </table>
                            <%--</div>--%>
                            <br/>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
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
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" maxlength="4" size="3"  runat="server" value="10"/>条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" maxlength="4" size="3"  runat="server"  />页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
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
    <a name="DetailListMark"></a>
    <span id="Forms" class="Spantype" name="Forms"></span>
</form>
</body>
</html>
