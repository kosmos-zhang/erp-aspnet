<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateTableViewModel.aspx.cs" Inherits="Pages_Office_DefManager_CreateTableViewModel" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/EmployeeSel.ascx" TagName="EmployeeSel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板设置</title>
    <meta http-equiv="X-UA-Compatible" content="IE=7"/>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>   
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/DefManager/CreateTableViewModel.js" type="text/javascript"></script>

    <script type="text/javascript">
    function insertText()
    {   
        var structvalue = document.getElementById("txtTableFieldName").value;
        if(structvalue=="0" || structvalue=="undefined" || structvalue=="")
        {
            return;
        }
        else
        {
            var text = "{***" + structvalue + "***}";
            try{
                FTB_API["content1"].InsertHtml(text);
            }catch(e)
            {
                popMsgObj.ShowMsg('焦点捕获失败，请确定输入焦点！');
            }
        }
        //eWebEditor1.insertHTML(text);
    }
    
    function insertSubTable()
    {
        var tablename = document.getElementById("ddl_subtable").value;
        var tablestr = document.getElementById("HidSubTable").value;
        if(tablename=="0" || tablename=="undefined" || tablename=="")
        {
            return;
        }
        else
        {
            tablestr = tablestr.replace(/###tablename###/g,tablename);
            tablestr = tablestr.replace(/###tablehead###/g,"###"+tablename+"###");
            //eWebEditor1.insertHTML(tablestr);
            try
            {
                FTB_API["content1"].InsertHtml(tablestr);
            }
            catch(e)
            {
                popMsgObj.ShowMsg('焦点捕获失败，请确定输入焦点！');
            }
        }
        
    }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    
    <input id="HidSubTable" type="hidden" runat="server" />
    <input id="HiddenURLParams" type="hidden" runat="server" /><!--存储URL参数-->
    <input id="hiddTBModuleID" type="hidden" runat="server" /><!--存储模板ID-->
    <uc1:Message ID="Message1" runat="server" /><!--弹框消息控件-->
    <div <%--style="height: 500px; overflow: scroll;"--%>>
        <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" alt=""/>
                </td>
                <td align="center" valign="top">
                </td>
            </tr>
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="center" class="Title">
                                模板设置
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                            <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                        <img runat="server" src="../../../images/Button/Bottom_btn_save.jpg"
                                                 alt="" id="btn_save" style="cursor: hand" onclick="SaveTableModelStyle();" /> 
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" style="cursor: hand"
                                                            alt="返回" id="ibtnBack" onclick="fnBack();" />
                                    </td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="6">
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td align="left">
                                            基本信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_01">
                           <tr>
                                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 15%" >
                                    表名：
                                </td>
                                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 35%" >
                                    <asp:DropDownList ID="txtTableName" runat="server" Width="200px" >   
                                    </asp:DropDownList>                    
                                </td>
                                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 15%">
                                    <%--启用状态--%>
                                </td>
                                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 35%">
                                    <%--<asp:DropDownList ID="UseStatus" runat="server" Width="200px">
                                        <asp:ListItem Value="1">启用</asp:ListItem>
                                        <asp:ListItem Value="0">停用</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <input id="UseStatus" value="1" type="hidden" runat="server"/>
                                </td>
                            </tr>
                            <tr id='subTBRow' style="display:block">
                                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 15%">
                                    字段名称：
                                </td>
                                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 35%" >
                                    <asp:DropDownList ID="txtTableFieldName" runat="server" Width="200px">   
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="insertField" type="button" value="插入" onclick="insertText()" style="width:70px"/>
                                </td>
                                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 15%">
                                    子表添加：
                                </td>
                                <td height="20" align="left" bgcolor="#FFFFFF" style="width: 35%" >
                                    <asp:DropDownList ID="ddl_subtable" onchange="GetSubTableName()" runat="server" Width="200px">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="insertSubTB" type="button" value="插入" onclick="insertSubTable()" style="width:70px"/>
                                </td>
                            </tr>
                            <tr id='Tr1' style="display:block">
                                <td height="20" align="right" bgcolor="#E6E6E6" style="width: 15%">
                                    可查看菜单人员：
                                </td>
                                <td height="20" colspan="3" align="left" bgcolor="#FFFFFF" style="width: 35%" >
                                    <asp:TextBox ID="UserJoinListName" runat="server"  ReadOnly="true" Width="98%"  onclick="alertdiv('UserJoinListName,lblJoinListID,2');" CssClass="tdinput"></asp:TextBox>
                                    <input type="hidden" id="lblJoinListID" runat="server" />
                                </td>
                            </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            模板设置
                                        </td>
                                        <td align="right">
                                            <div id='searchClick2'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                        id="Tb_03">
                        <tr>
                            <td style="background-color:#FFFFFF">
                                <FTB:FreeTextBox ID="content1" Width="100%" runat="server">
                                </FTB:FreeTextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <span id="Forms" class="Spantype" ></span>
    </form>
</body>
</html>
