<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceRoyaltyBase.aspx.cs"
    Inherits="Pages_Office_HumanManager_PerformanceRoyaltyBase" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>绩效考核基数设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/HumanManager/PerformanceRoyaltyBase.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .maintb
        {
            margin-top: 10px;
            margin-left: 0px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex1">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr height="20" align="right" bgcolor="#f0f0f0">
            <td colspan='2' width='100%'>
               &nbsp; <a href="SalaryCompanyRoyaltySet.aspx?ModuleID=2011701"   style="text-decoration: none; color :Blue ">公司业务提成</a>&nbsp;&nbsp;
                 <a href="SalaryDepatmentRoyaltySet.aspx?ModuleID=2011701" style="text-decoration: none; color :Blue">部门业务提成</a>&nbsp; &nbsp;
                 <a href="SalaryPersonalRoyaltySet.aspx?ModuleID=2011701" style="text-decoration: none; color :Blue">个人业务提成</a>&nbsp; &nbsp;
                <a href="SalaryPiecework.aspx?ModuleID=2011701" style="text-decoration: none; color :Blue">计件工资</a>&nbsp; &nbsp;
                <a href="SalaryTime.aspx?ModuleID=2011701" style="text-decoration: none; color :Blue">计时工资</a> &nbsp; &nbsp;
                <a href="SalaryCommission.aspx?ModuleID=2011701" style="text-decoration: none; color :Blue">产品单品提成</a> &nbsp; &nbsp; 
                <a href="PerformanceRoyaltyBase.aspx?ModuleID=2011701"    style="text-decoration: none; color :Blue">绩效薪资设置</a>&nbsp; &nbsp;
                <a href="SalaryPerformanceRoyaltySet.aspx?ModuleID=2011701" style="text-decoration: none; color :Blue">绩效系数设置</a>&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
             <td  align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        人员选择
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="UserEmployee" id="UserEmployee" type="text" class="tdinput" size="19"
                                            readonly="readonly" onclick="alertdiv1('UserEmployee,txtEmployeeID');" style="width: 90%" />
                                        <input name="txtEmployeeID" id="txtEmployeeID" type="hidden" />
                                    </td>
                                    <td height="20" align="right" bgcolor="#E6E6E6" width="10%">
                                        考核类型
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <select id="sltTaskflag">
                                            <option value="">--请选择--</option>
                                            <option value="1">月考核系数</option>
                                            <option value="2">季度考核系数</option>
                                            <option value="3">半年考核系数</option>
                                            <option value="4">年考核系数</option>
                                        </select>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="22%" bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="txtorderBy" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch"
                                            runat="server" style='cursor: hand;' onclick="DoSearch();" visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintb"
        id="mainindex1">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
          绩效薪资设置
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" id="btnNew" runat="server"
                                alt="新建" style='cursor: hand;' onclick="ShowNewDiv();" visible="false" />
                            <img id="btnDelete" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="DoDel();" visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <input type="checkbox" name="checkall" id="checkall" onclick="SelectAllCk()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick">
                                    查看</div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="30%">
                                <div class="orderClick" onclick="OrderBy('EmployeeID','oc1');return false;">
                                    人员<span id="oc1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="30%">
                                <div class="orderClick" onclick="OrderBy('BaseMoney','oc2');return false;">
                                    绩效薪资金额<span id="oc2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="30%">
                                <div class="orderClick" onclick="OrderBy('TaskFlag','oc3');return false;">
                                    考核类型<span id="oc3" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div style="overflow-y: auto">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                        class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg">
                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                    <tr>
                                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                            <div id="pagecount">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="pageDataList1_Pager" class="jPagerBar">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="divpage">
                                                <input name="text" type="text" id="Text2" style="display: none" />
                                                <span id="pageDataList1_Total"></span>每页显示
                                                <input name="text" type="text" id="ShowPageCount" />
                                                条 转到第
                                                <input name="text" type="text" id="ToPage" />
                                                页
                                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </td>
        </tr>
    </table>
    <%---------------------------------------------------新建div-Start-----------------------------------------------%>
    <div id="divBackShadow" style="display: none; z-index: 100">
        <iframe id="BackShadowIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="divNew" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 650px; z-index: 1000; position: absolute; display: none; top: 25%; left: 60%;
        margin: 5px 0 0 -400px;" class="checktable">
        <table width="100%">
            <tr>
                <td>
                    <input type="hidden" id="txtIndentityID" value="0" />
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="DoSave();"
                        id="btnSave" runat="server" visible="true" style="cursor: pointer" />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="DoHide();" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
            id="Tb_01" style="display: block">
            <tr>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    人员选择
                </td>
                <td width="23%" bgcolor="#FFFFFF">
                    <input name="UserEmployee_uc" id="UserEmployee_uc" type="text" class="tdinput" size="19"
                        readonly="readonly" onclick="alertdiv1('UserEmployee_uc,txtEmployeeID_uc');"
                        style="width: 60%" />
                    <input name="txtEmployeeID_uc" id="txtEmployeeID_uc" type="hidden" />
                </td>
                <td align="right" bgcolor="#E6E6E6" width="13%">
                    绩效薪资金额<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF" width="23%">
                    <input name="txtBaseMoney" id="txtBaseMoney" maxlength="100" type="text" class="tdinput"
                        value="0" onblur="CheckNum(this.id)" size="15" style="width: 95%" />
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6" width="10%">
                    考核类型<span class="redbold">*</span>
                </td>
                <td width="23%" bgcolor="#FFFFFF">
                    <select id="sltTaskflag_uc" name="sltTaskFlag_uc">
                        <option value="">--请选择--</option>
                        <option value="1">月考核系数</option>
                        <option value="2">季度考核系数</option>
                        <option value="3">半年考核系数</option>
                        <option value="4">年考核系数</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <%---------------------------------------------------div-End-----------------------------------------------%>
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
