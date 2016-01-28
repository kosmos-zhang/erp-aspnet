<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubSellCustAdd.aspx.cs" Inherits="Pages_Office_SubStoreManager_SubSellCustAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建分店客户</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/SubStoreManager/SubStorageCustAdd.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script type="text/javascript">
    
    function BackToPage()
    {
         history.back();
    }
    </script>

    <style type="text/css">
        #Select1
        {
            width: 88px;
        }
        #TextArea1
        {
            width: 853px;
        }
        #txt_Remark
        {
            width: 595px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <input id="hiddenCustID" type="hidden" value="0" />
    <!--本单据的ID-->
    <!-- Start 消息提示-->
    <uc1:Message ID="Message1" runat="server" />
    <span id="Forms" class="Spantype" name="Forms"></span>
    <!-- End 消息提示-->
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
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
                            <% if (this.CustID < 1)
                               { %>
                            新建<%} %>分店客户
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img id="btnSave" runat="server" src="../../../images/Button/Bottom_btn_save.jpg"
                                                        onclick="Fun_Save_Cust();" style="cursor: pointer" title="保存分店客户" />
                                                </td>
                                                <td>
                                                    <img style="display: none" onclick="BackToPage();" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
                                                        border="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        <img onclick="PrintCust();" id="btnPrint" src="../../../images/Button/Main_btn_print.jpg"
                                            style="cursor: pointer" title="打印" />
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <input type="hidden" id="txtIdentityID" value="0" runat="server" />
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
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="CustName" maxlength="200" style="width: 99%" class="tdinput" runat="server"
                                type="text" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户联系电话
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="CustTel" MaxLength="50" runat="server" CssClass="tdinput" Width="93%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            客户手机号
                        </td>
                        <td class="tdColInput">
                            <input id="CustPhone" style="width: 99%" maxlength="50" class="tdinput" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户地址
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input maxlength="200" type="text" id="CustAddr" class="tdinput" style="width: 95%" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    </form>
</body>
</html>

<script type="text/javascript">
var CustID ='<%=CustID %>';
</script>

