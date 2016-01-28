<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RectCheckTemplate_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_RectCheckTemplate_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建面试评测模板</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/RectCheckTemplate_Edit.js" type="text/javascript"></script>
</head>
<body>
<form id="frmMain" runat="server">
<table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建面试评测模板</div></td>
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
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" />
                                </td>
                                <td align="right">
                                <%--    <img src="../../../Images/Button/Main_btn_print.jpg" runat="server" visible="true" alt="打印" id="btnPrint" style="cursor:hand" />--%>
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
                                <input type="hidden" id="hidEditFlag" runat="server" />
                                <input type="hidden" id="hidModuleID" runat="server" />
                                <input type="hidden" id="hidSearchCondition" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">模板编号<span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="23%">
                                <div id="divCodeRule" runat="server">
                                    <uc1:CodingRule ID="codeRule" runat="server" />
                                </div>
                                <div id="divCodeNo" runat="server" class="tdinput"  disabled ="true"></div>
                            </td>
                            <td height="20" align="right" class="tdColTitle" width="10%">主题<span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="23%">
                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" CssClass="tdinput"></asp:TextBox>
                            </td>
                            <td height="20" align="right" class="tdColTitle" width="10%">岗位<span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="24%">
                                <asp:DropDownList ID="ddlQuarter" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%">启用状态<span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="23%">
                                <select  name="seleUsedStatus" width="20px" id="seleUsedStatus">
                                  <option value="1">启用</option>
                                  <option value="0">停用</option>
                                </select></td>
                            <td height="20" align="right" class="tdColTitle" width="10%">&nbsp;</td>
                            <td height="20" class="tdColInput" width="23%">
                                &nbsp;</td>
                            <td height="20" align="right" class="tdColTitle" width="10%">&nbsp;</td>
                            <td height="20" class="tdColInput" width="24%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="20" align="right" class="tdColTitle" >备注</td>
                            <td height="20" class="tdColInput" colspan="5">
                                <asp:TextBox ID="txtRemark" Height="30" TextMode="MultiLine" runat="server" Width="85%" MaxLength="250" CssClass="tdinput"></asp:TextBox>
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
                <td colspan="2">
                    <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                        <tr>
                            <td height="25" valign="top" >
                                <span class="Blue">模板要素</span>
                                <input id="Button1" type="button" value="button"   onclick ="ggg();"  style="display :none "/></td>
                            <td align="right" valign="top">
                                <div id='divElem'>
                                    <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblElem','divElem')"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" id="tblElem" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" class="tdColInput">
                                <img src="../../../images/Button/Show_add.jpg" id="btnAdd" runat="server" alt="添加" style="cursor:hand" onclick="AddElem();" visible="false" />
                                <img src="../../../images/Button/Show_del.jpg" id="btnDelete" runat="server" alt="删除" style="cursor:hand" onclick="DeleteElem();" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="divElemDetail" runat="server"></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2" height="10"></td></tr>
        </table>
        <!-- </div> padding: 10px;z-index:10; -->
        <%--要素选择页面 开始--%>
        <div id="divCheckElem" runat="server"  style="background: #fff;border: solid 1px #999999; position: absolute; top: 25%; left: 15%; width: 800px; display:none;">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDeptInfo" class="maintable">
                <tr>
                    <td valign="top" colspan="2">
                        <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                            <tr>
                                <td height="28" bgcolor="#FFFFFF">
                                    <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确认" visible="false" id="btnConfirm" runat="server" onclick="DoConfirm();" style="cursor:hand" />
                                    <img src="../../../images/Button/Bottom_btn_cancel.jpg" alt="取消" id="btnCancel" runat="server" onclick="DoCancel()" style="cursor:hand;" />
                                </td>
                            </tr>
                        </table>
                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                            <tbody>
                                <tr>
                                    <th height="20" width="10%" align="center" background="../../../images/Main/Table_bg.jpg">
                                            选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')">
                                    </th>
                                    <th align="center"  width="20%"  background="../../../images/Main/Table_bg.jpg">
                                        <div class="orderClick" onclick="OrderBy('ElemName','oC0');return false;">
                                            要素名称<span id="oC0" class="orderTip"></span>
                                        </div>
                                    </th>
                                    <th align="center"  width="70%" background="../../../images/Main/Table_bg.jpg">
                                        <div class="orderClick" onclick="OrderBy('Standard','oC1');return false;">
                                            评分标准<span id="oC1" class="orderTip"></span>
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
        </div>
        <%--要素选择页面 结束--%>
    </td></tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<a name="DetailListMark"></a>
<uc1:Message ID="msgError" runat="server" />
</form>
</body>
</html>