<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogResult.aspx.cs" Inherits="Handler_Office_SupplyChain_LogResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入结果显示</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
    function showError()
    {
        document.getElementById("taberror").style.display="";
    }
    
    function hiddenError()
    {
        document.getElementById("taberror").style.display="none";
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                </div>
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="middle" class="Title" style="background-color:#F0F0F0">
                批量导入结果查看
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <!--导入Excel文件-->
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td class="Blue" style="background-color:#F0F0F0; padding-left:1px">
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" style="height:150px" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="12%" height="20" bgcolor="#E7E7E7" align="center" valign="top">
                                        导入结果：
                                    </td>
                                    <td width="88%" bgcolor="#FFFFFF" valign="top">
                                        <div style=" overflow:auto ; height:130px">
                                            <asp:Label ID="lbl_result" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!--导入结束-->
                
                
                <!--批量导入-->
                <table width="99%" border="0" align="center" cellpadding="0" id="Table2" cellspacing="0" bgcolor="#CCCCCC">
                    <tr style="height:35px">
                        <td valign="bottom" class="Blue" style="background-color:#F0F0F0; padding-left:1px">
                           
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td colspan="4" width="100%" height="30" bgcolor="#E7E7E7" align="center">
                                        <asp:Button ID="btn_back" runat="server" Text="关闭" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td colspan="4" width="100%" height="1" bgcolor="#E7E7E7" align="center">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>           
                <!--批量导入结束-->
            </td>
        </tr>
    </table>
    
    
  </form>
</body>
</html>
