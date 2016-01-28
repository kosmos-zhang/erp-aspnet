<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryStandard.aspx.cs" Inherits="Pages_Office_HumanManager_SalaryStandard" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资标准设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/SalaryStandard.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <style type="text/css">
        
         .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
     
        .errorMsg
        {
	        filter:progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
	        position:absolute;
	        top:240px;
	        left:450px;
	        border-width:1pt;
	        border-color:#666666;
	        border-style:solid;
	        width:290px;
	        display:none;
	        margin-top:10px;
	        z-index:21;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server">
<div>
    <table width="100%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex" >
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
                                    <td width="10%" height="20" class="tdColTitle">岗位</td>
                                    <td width="23%" class="tdColInput">
                                        <asp:DropDownList ID="ddlSearchQuarter" runat="server"></asp:DropDownList>
                                    </td>
                                    <td width="10%" class="tdColTitle">岗位职等</td>
                                    <td width="23%" class="tdColInput">
                                        <uc1:CodeType ID="ctSearchQuaterAdmin" runat="server" />
                                    </td>
                                    <td width="10%" class="tdColTitle">启用状态</td>
                                    <td width="24%" class="tdColInput">
                                        <asp:DropDownList ID="ddlSearchUsedStatus" runat="server">
                                            <asp:ListItem Value="">请选择</asp:ListItem>
                                            <asp:ListItem Value="0">停用</asp:ListItem>
                                            <asp:ListItem Value="1">启用</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>                 
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
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
                        <td height="30" colspan="2" align="center" valign="top" class="Title">岗位工资设置</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand"  onclick="DoNew();"/>
                                     <%--   <img src="../../../Images/Button/Bottom_btn_edit.jpg" alt="修改" visible="false" id="btnModify" runat="server" style="cursor:hand" height="25" onclick="DoModify();"/>--%>
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'   />
                                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
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
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg">选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')"></th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:98%;" onclick="OrderBy('QuarterName','oC0');return false;">
                                                岗位<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:98%;" onclick="OrderBy('AdminLevelName','oC1');return false;">
                                                岗位职等<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:98%;" onclick="OrderBy('ItemName','oC2');return false;">
                                                工资项名称<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:98%;" onclick="OrderBy('UnitPrice','oC3');return false;">
                                                金额<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg">
                                            <div class="orderClick" style="width:98%;" onclick="OrderBy('UsedStatusName','oC4');return false;">
                                                启用状态<span id="oC4" class="orderTip"></span>
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
    </div>
    <div id="divEditSalary" runat="server" style="background: #ffffff; padding: 10px; width: 800px; z-index:300; position: absolute;top: 20%; left: 15%; display:none ; ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable"  id="tblSalaryInfo" style="background: #ffffff;">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="30" class="tdColInput">
                                <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>
                                <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand"  onclick="DoBack();"/>
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
                                        <td colspan="2" height="4"><input type="hidden" id="hidID" /></td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    <tr>
                                        <td height="25" class="tdColTitle" width="10%">岗位<span class="redbold">*</span></td>
                                        <td height="25" class="tdColInput" width="23%">
                                            <div id="divCodeRule" runat="server">
                                                <asp:DropDownList ID="ddlQuarter" runat="server"></asp:DropDownList>
                                            </div>
                                        </td>
                                        <td class="tdColTitle" width="10%">岗位职等<span class="redbold">*</span></td>
                                        <td class="tdColInput" width="23%">
                                            <uc1:CodeType ID="ctQuaterAdmin" runat="server" />
                                        </td>
                                        <td class="tdColTitle" width="10%">工资项<span class="redbold">*</span></td>
                                        <td class="tdColInput" width="24%">
                                            <asp:DropDownList ID="ddlSalaryItem" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdColTitle">金额<span class="redbold">*</span></td>
                                        <td class="tdColInput">
                                            <asp:TextBox ID="txtUnitPrice" runat="server" Width="95%" MaxLength="10" CssClass="tdinput" onchange='Number_round(this,2);'  ></asp:TextBox>
                                        </td>
                                        <td class="tdColTitle">启用状态</td>
                                        <td class="tdColInput">
                                            <asp:DropDownList ID="ddlUsedStatus" runat="server">
                                                <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdColTitle">备注</td>
                                        <td class="tdColInput">
                                            <asp:TextBox ID="txtRemark" runat="server" Width="95%" MaxLength="50" CssClass="tdinput"  SpecialWorkCheck="备注"></asp:TextBox>
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
    
<span id="spanMsg" class="errorMsg" style="z-index:1005"></span>        
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
</form>
</body>
</html>
