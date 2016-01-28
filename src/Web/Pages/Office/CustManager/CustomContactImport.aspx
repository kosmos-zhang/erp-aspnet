<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomContactImport.aspx.cs" Inherits="Pages_Office_CustManager_CustomContactImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
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
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
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
               客户联系人批量导入
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <!--导入Excel文件-->
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td class="Blue" style="background-color:#F0F0F0; padding-left:1px"><img src="../../../images/Main/arrow_1.jpg" width="12" height="18" align="absmiddle" />&nbsp;上传Excel文件
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="12%" height="20" bgcolor="#E7E7E7" align="center">
                                        <a href="客户联系人导入模板.xls">模板下载</a>
                                    </td>
                                    <td width="48%" bgcolor="#FFFFFF">
                                        <input id="fileExportExcel" style="width:100%" type="file"  runat="server"  />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="center">
                                        <asp:Button ID="btn_excel" Text="导入数据l" OnClick="btn_excel_Click" runat="server" />
                                    </td>
                                    <td width="30%" bgcolor="#FFFFFF">
                                        <asp:Label ID="lbl_result" ForeColor="#FF0000" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!--导入结束-->
                     
                
                <!--批量导入提示-->
                <table width="99%" border="0" align="left" cellpadding="0" id="tab_end" runat="server"  cellspacing="0" bgcolor="#CCCCCC" style="color:Red">
                    <tr style="height:35px">
                        <td valign="bottom" class="Blue" style="background-color:#F0F0F0; padding-left:1px">
                            <img src="../../../images/Main/arrow_1.jpg" width="12" height="18" align="absmiddle" />&nbsp;Excel数据批量导入结果
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td colspan="4" width="100%" height="30" bgcolor="#E7E7E7" align="left">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
                <!--批量导入提示结束-->
                
                <!--查看错误开始-->
                <table width="99%" border="0" align="center" cellpadding="0" id="taberror" style="display:none" cellspacing="0" bgcolor="#CCCCCC">
                    <tr style="height:35px">
                        <td valign="bottom" class="Blue" style="background-color:#F0F0F0; padding-left:1px">
                            <img src="../../../images/Main/arrow_1.jpg" width="12" height="18" align="absmiddle" />&nbsp;Excel校验结果&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" onclick="hiddenError()">关闭校验失败原因</a>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td colspan="4" width="100%" height="30" bgcolor="#E7E7E7" align="left">
                                    <div style="height:250px; overflow:scroll; overflow-x:no">
                                    
                                    </div>
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
                <!--查看错误结束-->
            </td>
        </tr>
    </table>
    
    
  </form>
</body>
</html>
