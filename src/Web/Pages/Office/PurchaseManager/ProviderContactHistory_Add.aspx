<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderContactHistory_Add.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderContactHistory_Add" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc3" %>


<%@ Register src="../../../UserControl/ProviderLinkManSelect.ascx" tagname="ProviderLinkManSelect" tagprefix="uc4" %>


<%@ Register src="../../../UserControl/ProviderInfo.ascx" tagname="ProviderInfo" tagprefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建供应商联络</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/office/PurchaseManager/ProviderContactHistoryAdd.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadFile.js" type="text/javascript">function ddlBillStatus_onclick() {

}

</script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
        .style2
        {
            background-color: #E6E6E6;
            text-align: right;
        }
        .style3
        {
            background-color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="divPBackShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="PBackShadowIframe" frameborder="0"
        width="100%"></iframe></div>
    <uc1:Message ID="Message1" runat="server" />
    <uc4:ProviderLinkManSelect ID="ProviderLinkManSelect1" runat="server" />
    <uc2:ProviderInfo ID="ProviderSelect1" runat="server" />
    <span id="Forms" class="Spantype"></span>
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
                            <td height="30" align="center" class="Title">
                            <div id="divTitle" runat="server">
                                新建供应商联络</div>
                        </td>
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
                                        &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="save_PurchaseReject" style="cursor:pointer" onclick="InsertProviderContactHistory();"  visible="false" runat="server"/>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" /></td>
                                    <td align="right">
                                        <img  src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style=" float:right; cursor: pointer;"  id="imgPrint"  onclick="ProviderContactHistoryPrint();" /> 
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="txtIsliebiaoNo" value="0" runat="server" />
                            <input id="txtAction" type="hidden" value="1" />
                            <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                            <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server"/>
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
                                        供应商联络
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
                    id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" class="style2" width="10%">
                            联络单编号<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <div id="divInputNo" runat="server">
                                <uc3:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divProviderContactHistoryNo" runat="server" class="tdinput"  style="display: none">
                            </div>
                        </td>
                        <td align="right" class="style2" width="10%">
                            供应商名称<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtCustName" MaxLength="25"  onclick="popProviderObj.ShowProviderList('txtCustID','txtCustName','txtCustNo');" ReadOnly="true" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtCustID" runat="server" />
                            <input type="hidden" id="txtCustNo" runat="server" />
                        </td>
                        <td align="right" class="style2" width="10%">
                            主题
                        </td>
                        <td class="style3" width="24%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput"  Width="95%"  SpecialWorkCheck="主题" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            供应商联络人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UsertxtLinkManName" runat="server"  onclick="ProviderLinkMan()"  ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                             <input type="hidden" id="HidLinkManID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            联络事由
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select name="drpLinkReasonID" class="tdinput" width="119px" runat="server" id="drpLinkReasonID"></select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            联络方式<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpLinkMode" class="tdinput" runat="server" id="drpLinkMode">
                            <option value="1" selected="selected">电话</option>
                                        <option value="2">传真</option>
                                        <option value="3">邮件</option>
                                        <option value="4">远程在线</option>
                                        <option value="5">会晤拜访</option>
                                        <option value="6">综合</option>
                            </select>
                            </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            联络时间
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtLinkDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtLinkDate')})" ReadOnly="true"  CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            我方联络人<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UsertxtLinkerName" onclick="alertdiv('UsertxtLinkerName,HidLinker');" runat="server"
                                             ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                             <input type="hidden" id="HidLinker" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            
                            </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            联络内容
                        </td>
                        <td height="20" class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtContents" id="txtContents" rows="3" cols="80" style="width:95%"></textarea>
                            <input type="hidden" id="hidModuleID" runat="server" />
                        </td>
                    </tr>
                   
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>


