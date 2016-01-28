<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefTableList.aspx.cs" Inherits="Pages_Office_DefManager_DefTableList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/office/FinanceManager/FixList.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/office/DefManager/DefTableList.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function newRecord()
    {
        window.location.href="DefTableOPertion.aspx?tableid="+document.getElementById("HidTableID").value;
    }

    </script>

</head>
<body onload="setTableHead();">
    <form id="frmMain" runat="server">
    <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
    <asp:HiddenField ID="HidColName" runat="server" /><!--存储检索字段名称-->
    <asp:HiddenField ID="HidHeadName" runat="server" /><!--存储列表字段名称-->
    <asp:HiddenField ID="HidTagName" runat="server" /><!--存储表头文字-->
    <asp:HiddenField ID="HidTableID" runat="server" /><!--存储表ID-->
    <asp:HiddenField ID="HidStatus" Value="1" runat="server" /><!--判断是否设置了标题列，如果设置了为1，没有设置为0-->
    <asp:HiddenField ID="HidTableHeadName" runat="server" /><!--存储表名-->
    
    <a name="DetailListMark"></a>
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr  id ="trSearchLine"  runat="server">
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr id ="trSearchImg"  runat="server" >
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblSearch','divSearch')" />
                </div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr id="tblSearch">
            <td colspan="2" style="background-color:#FFFFFF" valign="top" align="left">
                <asp:Literal ID="lbl_search" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5">
                <input type="hidden" id="hidModuleID" runat="server" />
                <input type="hidden" id="hidModuleIDAsse" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="98%" border="0" cellpadding="0" cellspacing="0" id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="2">
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">
                            <label id='customtablename' runat="server"></label> 
                        </td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0"  cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">&nbsp;
                                            <asp:ImageButton ID="btnNew" ImageUrl="../../../images/Button/Bottom_btn_new.jpg"    OnClientClick=" newRecord() ; return false;"
                                            AlternateText="新建" runat="server" />
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" onclick="fnDel(<%=Request.QueryString["tableid"].ToString() %>)" style='cursor: pointer;' />
                                        <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                            AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo"
                                bgcolor="#999999">
                                <tbody id="tablelist">
                                </tbody>
                            </table>
                            <br />
                            <a name="pageDataList1Mark"></a>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                                class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%" align="left">
                                                    <div id="pagecount">
                                                    </div>
                                                </td>
                                                <td height="28" align="center">
                                                    <div id="pageDataList1_Pager" class="jPagerBar">
                                                    </div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divpage">
                                                        <input name="text" type="text" id="Text2" style="display: none" />
                                                        <span id="pageDataList1_Total"></span>每页显示
                                                        <input name="lblShowPageCount" type="hidden" id="lblShowPageCount" value="10" />
                                                        <input name="text" type="text" id="ShowPageCount" value="10" />
                                                        条 转到第
                                                        <input name="text" type="text" id="ToPage" value='1' />
                                                        页
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: pointer;' alt="go"
                                                            align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>
        <input id="txtDeptCode" runat="server" type="hidden" />
        <uc1:Message ID="Message1" runat="server" />
        <p>
            <input id="txtSubjectCD2" type="hidden" runat="server" />
        </p>
        <span id="Forms" class="Spantype" name="Forms"></span>
    </form>
</body>
</html>