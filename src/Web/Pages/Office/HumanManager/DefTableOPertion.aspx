<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefTableOPertion.aspx.cs" Inherits="Pages_DefManager_DefTableOPertion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>用户管理追加</title>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/office/DefManager/DefTableOPertion.js" type="text/javascript"></script>
    <script type="text/javascript">
    function getValues()
    {
        var str = $("#HidControlName").val();
        var strlist = new Array();
        var param="1=1";
        strlist = str.split(",");
        for(var i=0;i<strlist.length;i++)
        {
            param = param+"&"+strlist[i]+"="+$("#db_"+strlist[i]).val();
        }
        ChargeValue();
    }
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
    <asp:HiddenField ID="HidControlName" runat="server" /><!--存储控件ID，传递各个控件的值-->
    <asp:HiddenField ID="HidControlList" runat="server" /><!--存储控件ID+控件获取数据类型+容纳长度+是否允许为空，各值之间用#割开-->
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenUserID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <span lang="zh-cn">新建<asp:Label ID="lbl_title" runat="server" Text=""></asp:Label></span>
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            <asp:HiddenField ID="txtUserName" runat="server" />
                            <input id="hfuserid" type="hidden" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand;
                                float: left" border="0" onclick="getValues()" runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                                    <td>
                                        
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
               
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <asp:DataList ID="StructDL" RepeatColumns="3" RepeatDirection="Horizontal" 
                            runat="server" onitemdatabound="StructDL_ItemDataBound">
                        <ItemTemplate>
                            <td align="right" bgcolor="#E6E6E6" style="border-top:solid 1pt Black;border-left:solid 1pt Black;border-bottom:solid 1pt Black;border-right:solid 1pt Black;">
                                <%#Eval("cname")%><asp:Literal ID="ltl_tag" runat="server"></asp:Literal>
                            </td>
                            <td bgcolor="#FFFFFF" class="style1" style="border-top:solid 1pt Black;border-left:asolid 1pt Black;border-bottom:solid 1pt Black;border-right:solid 1pt Black;">
                                <asp:Literal ID="ltl_input" runat="server"></asp:Literal>
                            </td>
                        </ItemTemplate>
                        </asp:DataList>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
