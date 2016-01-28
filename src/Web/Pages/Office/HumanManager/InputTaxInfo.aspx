<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputTaxInfo.aspx.cs" Inherits="Pages_Office_HumanManager_InputTaxInfo" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人所得税</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/InputTaxInfo.js" type="text/javascript"></script>
    <style type="text/css">
        #tblMain
        {
            margin-top:0px;
            margin-left:0px;
		    background-color:#F0f0f0;
      	    font-family:tahoma;
      	    color:#333333;
      	    font-size:12px;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server">
<table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblMain">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title">个人所得税</td>
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
                        <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                            <tr>
                                <td width="10%" height="20" class="tdColTitle">人员编号</td>
                                <td width="23%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeNo" Width="95%" CssClass="tdinput" runat="server"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdColTitle">工号</td>
                                <td width="23%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeNum" Width="95%" CssClass="tdinput" runat="server"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdColTitle">姓名</td>
                                <td width="24%" class="tdColInput">
                                    <asp:TextBox ID="txtEmployeeName" Width="95%" CssClass="tdinput" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdColTitle" height="25">所在岗位</td>
                                <td class="tdColInput">
                                    <asp:DropDownList ID="ddlQuarter" runat="server"></asp:DropDownList>
                                </td>
                                <td class="tdColTitle">岗位职等</td>
                                <td class="tdColInput">
                                    <uc1:CodeType ID="ctAdminLevel" runat="server" />
                                </td>
                                <td class="tdColTitle">年月</td>
                                <td class="tdColInput">
                                    <asp:TextBox ID="txtSalaryMonth" Width="95%" CssClass="tdinput" ReadOnly="true" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM',el:$dp.$('txtSalaryMonth')})"></asp:TextBox>
                                </td>
                            </tr>                   
                            <tr>
                                <td colspan="6" align="center" bgcolor="#FFFFFF">
                                    <input type="hidden" id="hidSearchCondition" />
                                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearch()'   />
                                    <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false"  style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
             </table>
        </td>
    </tr>
    <tr><td colspan="2">
        <table width="98%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td align="center" valign="top"></td>
            </tr>
            <tr><td colspan="2" height="2"></td></tr>
            <tr>
                <td height="30" colspan="2" align="center" valign="top" class="Title">个人所得税</td>
            </tr>
            <tr>
                <td height="35" colspan="2" valign="top">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img src="../../../images/Button/Main_btn_out.jpg" alt="导出" visible="false" id="btnExport" runat="server" onclick="DoExport()" style='cursor:pointer;'   />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                        <tbody>
                            <tr>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" height='22'>
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('EmployeeNo','oC0');return false;">
                                        员工编号<span id="oC0" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('EmployeeName','oC1');return false;">
                                        姓名<span id="oC1" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('DeptName','oC2');return false;">
                                        部门<span id="oC2" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('QuarterName','oC3');return false;">
                                        岗位<span id="oC3" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('AdminLevelName','oC4');return false;">
                                        岗位职等<span id="oC4" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('TotalSalary','oC14');return false;">
                                        工资合计<span id="oC14" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('TaxRate','oC5');return false;">
                                        税率<span id="oC5" class="orderTip"></span>
                                    </div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg">
                                    <div class="orderClick" style="width:98%;" onclick="OrderBy('TotalTax','oC6');return false;">
                                        税额<span id="oC6" class="orderTip"></span>
                                    </div>
                                </th>
                            </tr>
                        </tbody>
                    </table>
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
                                                每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" />条&nbsp;&nbsp;
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
    </td></tr>
</table>
<div id="popupContent"></div>
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
</form>
</body>
</html>