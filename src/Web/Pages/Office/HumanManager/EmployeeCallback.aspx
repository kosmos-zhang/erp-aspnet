<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeCallback.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeCallback" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人力档案回收站</title>
     <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
     <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/EmployeeCallback.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
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
                                    <td width="10%" height="20" class="tdColTitle">编号</td>
                                    <td width="23%" class="tdColInput">
                                        <input name="txtEmployeeNo" id="txtEmployeeNo" runat="server" type="text" class="tdinput" size = "13" SpecialWorkCheck="编号" />
                                    </td>
                                    <td width="10%" class="tdColTitle">拼音缩写</td>
                                    <td width="23%" class="tdColInput">
                                        <input name="txtPYShort" id="txtPYShort" runat="server" type="text" class="tdinput" size = "19" SpecialWorkCheck="拼音缩写" />
                                    </td>
                                    <td height="20" width="10%" class="tdColTitle">姓名</td>
                                    <td class="tdColInput" width="24%">
                                        <input name="txtEmployeeName" id="txtEmployeeName" runat="server" type="text" class="tdinput" size = "13"  SpecialWorkCheck="姓名" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">性别</td>
                                    <td class="tdColInput">
                                        <select id="ddlSex"  runat="server">
                                            <option value="">请选择</option>
                                            <option value="1">男</option>
                                            <option value="2">女</option>
                                        </select>
                                    </td>
                                    <td class="tdColTitle">文化程度</td>
                                    <td class="tdColInput">
                                        <uc1:codetype ID="ddlCulture" runat="server" />
                                    </td>
                                    <td class="tdColTitle">毕业院校</td>
                                    <td class="tdColInput">
                                        <input name="txtSchoolName" id="txtSchoolName" runat="server" type="text" class="tdinput" size = "20" maxlength="25" SpecialWorkCheck="毕业院校" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">应聘岗位</td>
                                    <td class="tdColInput">
                                        <input id="txtPositionTitle" runat="server" type="text" class="tdinput" maxlength="20" size = "20" SpecialWorkCheck="应聘岗位" />
                                    </td>
                                    <td class="tdColTitle">工龄(年)</td>
                                    <td class="tdColInput">
                                        <input type="text" id="txtTotalSeniority" runat="server" class="tdinput" SpecialWorkCheck="工龄" />
                                    </td>
                                    <td class="tdColTitle">手机号码</td>
                                    <td class="tdColInput"><asp:TextBox ID="txtMobile" runat="server" MaxLength="50" SpecialWorkCheck="手机号码" CssClass="tdinput" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='SearchEmployeeReserve()'/>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr><td colspan="2" height="5"><input type="hidden" id="hidModuleID" runat="server" /></td></tr>
        <tr>
            <td colspan="2">

                <table width="99%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" align="center">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">人力档案回收站</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/btn_hy.jpg" alt="还原" visible="false" id="btnCallBack" onclick="CallBack();" runat="server"  style='cursor:pointer;' />
                                        <img src="../../../images/Button/btn_yjsc.jpg" alt="彻底删除" visible="false" id="btnDel" onclick="DeleteEmp();" runat="server" style='cursor:pointer;' />
                                        
                                        <asp:ImageButton ID="btnImport" runat="server" 
                                            ImageUrl="~/Images/Button/Main_btn_out.jpg" onclick="btnImport_Click" 
                                            Visible="False" />
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
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg">选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')"></th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('EmployeeNo','oC0');return false;">
                                                编号<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('PYShort','oC1');return false;">
                                                拼音缩写<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('EmployeeName','oC2');return false;">
                                                姓名<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('SexName','oC3');return false;">
                                                性别<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('Flag','oFlag');return false;">
                                                分类<span id="oFlag" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('Birth','oC4');return false;">
                                                出生年月<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('Age','oC14');return false;">
                                                年龄<span id="oC14" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('Origin','oC5');return false;">
                                                籍贯<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('ProfessionalName','oC6');return false;">
                                                专业<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                       
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:95%;" onclick="OrderBy('PositionTitle','oC8');return false;">
                                                应聘岗位<span id="oC8" class="orderTip"></span>
                                            </div>
                                        </th>
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
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" maxlength="4" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" maxlength="4" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go"  height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
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
<input id="hiddExpOrder" type="hidden" runat="server" />
</form>
</body>
</html>
