<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportDetailUC.ascx.cs" Inherits="UserControl_ReportDetailUC" %>
<%@ Register Src="~/UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <title><%=DetailTitle%></title>
</head>
<body>
    <form id="form1" runat="server">
    
     <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/OperatingModel/AdminManager/common.js" type="text/javascript"></script>
    <script type="text/javascript">      
    $(document).ready(function()
    {
       TurnToPage(1);
    });
    
    function TurnToPage(pageIndex)
    {      
       AjaxHandle(pageIndex,"<%=Condition%>","<%=AjaxUrl%>");//ajax操作
       
    }
    
    //返回成功数据加载
    function DoData(data)
    {
     return "<tr class='newrow'>"+
      <%for (int i = 0; i < ListArr.Length/2; i++){%>
            "<td height='22' align='center'>" +data.<%=ListArr[i,1]%> + "</td>"+
      <%}%>
            "</tr>" 
    }
    </script>
    
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
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
                <%=DetailTitle%>
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <asp:ImageButton ID="ImageButton1"  ImageUrl="~/images/Button/Main_btn_out.jpg"  runat="server" OnClick="btnImport_Click" />
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" onclick="window.history.back(-1)" style="cursor:pointer;" />
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
                           <%for (int i = 0; i < ListArr.Length/2; i++){%>
                           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('<%=ListArr[i,1]%>','<%=ListArr[i,1]%>1');return false;"><%=ListArr[i, 0]%><span id="<%=ListArr[i,1]%>1" class="orderTip"></span></div>
                           </th>
                           <%}%>
                        </tr>
                    </tbody>
                </table>
                <br />
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
                                            &nbsp;<input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" maxlength="3" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" maxlength="7" />
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
    <span id="Forms" class="Spantype" name="Forms"></span>
     <uc1:Message ID="Message1" runat="server" />
    </form>
</body>
</html>