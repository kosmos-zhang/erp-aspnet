<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeTest_Info.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeTest_Info" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试结果列表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/EmployeeTest_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
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
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">考试编号</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtTestNo" id="txtTestNo" runat="server" specialworkcheck="考试编号" maxlength="50" type="text" class="tdinput" width="85%"/>
                                    </td>
                                    <td class="tdColTitle" width="10%">主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtTitle" id="txtTitle" runat="server" specialworkcheck="主题" maxlength="50" type="text" class="tdinput" width="85%"/>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">考试负责人</td>
                                    <td class="tdColInput" width="24%">
                                        <input name="UserTeacher" id="UserTeacher" readonly="readonly" onclick="alertdiv('UserTeacher,txtTeacherID');"  runat="server" maxlength="50" type="text" class="tdinput" width="85%"/>
                                        <input type="hidden" id="txtTeacherID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">开始时间</td>
                                    <td class="tdColInput">
                                        <input name="txtStartDate" readonly="readonly" id="txtStartDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"/>至
                                        <input readonly="readonly" id="txtStartToDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartToDate')})"/>
                                    </td>
                                    <td class="tdColTitle">结束时间</td>
                                    <td class="tdColInput">
                                        <input name="txtEndDate" readonly="readonly" id="txtEndDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"/>至
                                        <input readonly="readonly" id="txtEndToDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndToDate')})"/>
                                    </td>
                                    <td class="tdColTitle">考试地点</td>
                                    <td class="tdColInput">
                                        <input name="txtAddr" id="txtAddr" runat="server" specialworkcheck="考试地点" maxlength="50" type="text" class="tdinput" width="85%"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">状态</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="未开始"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="已结束"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdColTitle"></td>
                                    <td class="tdColInput"></td>
                                    <td class="tdColTitle"></td>
                                    <td class="tdColInput"></td>
                                </tr>      
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearchInfo()' />
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
            <td colspan="2" height="5">
                <input type="hidden" id="hidModuleID" runat="server" />
                <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
            </td>
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
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">考试记录列表</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand" onclick="NewTestInfo();"/>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DeleteTestInfo()" style='cursor:pointer;'/>
                                        <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg">
                                            选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')">
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('TestNo','oC0');return false;">
                                                考试编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                                主题<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('TeacherName','oC2');return false;">
                                                考试负责人<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('StartDate','oC3');return false;">
                                                开始时间<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('EndDate','oC4');return false;">
                                                结束时间<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('Addr','oC5');return false;">
                                                考试地点<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('StatusName','oC6');return false;">
                                                考试状态<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" onclick="OrderBy('AbsenceCount','oC7');return false;">
                                                缺考人数<span id="oC7" class="orderTip"></span>
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
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" maxlength="4" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" maxlength="4" size="3"/>页&nbsp;&nbsp;
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

