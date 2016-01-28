<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateTableReport.aspx.cs" Inherits="Pages_DefManager_CreateTableReport" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>用户管理追加</title>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/office/DefManager/CreateTableReport.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script type="text/javascript">
    function getUsername()
    {
        $("#Hidname").val($("#UserJoinListName").val());
    }
    </script>
</head>
<body onload="InsertState()">
    <form id="frmMain" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <asp:HiddenField ID="HidControl" Value="0" runat="server" /><!--记录保存后的ReportTable表的ID号,判断是添加还是修改-->
    <table width="95%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr bgcolor="#FFFFFF">
                        <td>
                            <asp:ImageButton ID="btn_save" OnClientClick="return checkData();" 
                                ImageUrl="../../../images/Button/Bottom_btn_save.jpg" runat="server"  
                                onclick="btn_save_Click" />
                            <img onclick="Golist();" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
                                border="0" style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblData" bgcolor="#999999">
                <tr>
                    <td align="center" bgcolor="#E6E6E6" style="width:15%; height:30px">
                        报表名称
                    </td>
                    <td align="left" bgcolor="#E6E6E6"  style="width: 85%">
                        <asp:TextBox ID="txt_menu" Width="500px" runat="server"></asp:TextBox>&nbsp;&nbsp;<span style="color:Red">(如: 某某报表)</span>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#E6E6E6" style="width:15%; height:30px">
                        是否添加时间筛选
                    </td>
                    <td align="left" bgcolor="#E6E6E6"  style="width: 85%">
                        <asp:DropDownList ID="ddl_timeflag" Width="100px" runat="server">
                            <asp:ListItem Value="0">否</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id='Tr1' style="display:block">
                    <td height="20" align="center" bgcolor="#E6E6E6" style="width: 15%">
                        可查看菜单人员
                    </td>
                    <td height="20" colspan="3" align="left" bgcolor="#FFFFFF" style="width: 35%" >
                        <asp:TextBox ID="UserJoinListName" runat="server"  ReadOnly="true" Width="98%"  onclick="alertdiv('UserJoinListName,hiduserid,2');getUsername()" CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="hiduserid" runat="server" />
                        <input type="hidden" id="Hidname" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#E6E6E6" style="width:15%; height:30px">
                        关联的表
                    </td>
                    <td align="left" bgcolor="#E6E6E6"  style="width: 85%">
                        <asp:CheckBoxList ID="tablelist" RepeatColumns="3" RepeatDirection="Horizontal" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
               
                
                <tr>
                    <td align="center" bgcolor="#E6E6E6">
                        SQL命令<br/><br/><span style="color:Red">起始时间参数用@begindate<br/><br/>截止时间参数用@enddate&nbsp;&nbsp;&nbsp;</span>
                        
                    </td>
                    <td align="left" bgcolor="#E6E6E6">
                        <asp:TextBox ID="txt_sql" TextMode="MultiLine" Width="99%" Height="230px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
