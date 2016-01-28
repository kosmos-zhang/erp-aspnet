<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderProductAdd.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderProductAdd" %>



<%@ Register src="../../../UserControl/ProviderInfo.ascx" tagname="ProviderInfo" tagprefix="uc2" %>




<%@ Register src="../../../UserControl/ProductInfoControl.ascx" tagname="ProductInfoControl" tagprefix="uc3" %>




<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建供应商物品推荐</title>
    
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/office/PurchaseManager/ProviderProductAdd.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadFile.js" type="text/javascript">function ddlBillStatus_onclick() {

}

</script>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
        .style1
        {
            border-width: 0pt;
            background-color: #ffffff;
            height: 9px;
            width: 100px;
        }
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
    <uc1:Message ID="Message1" runat="server" />
    <uc2:ProviderInfo ID="ProviderInfo1" runat="server" />
    <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <span id="Forms" class="Spantype"></span>
    <table width="95%"  border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                                新建供应商物品推荐</div>
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
                                        &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="save_PurchaseReject" style="cursor:pointer" onclick="InsertProviderProductInfo();" visible="false" runat="server"/>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" /></td>
                                    <td align="right">
                                        <%--<img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"
                                            title="打印" />--%>
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
                                        供应商物品推荐
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
                            供应商名称<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtCustName" MaxLength="25" onclick ="popProviderObj.ShowProviderList('txtCustName','txtCustName','txtCustNo')" ReadOnly="true" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtCustNo" runat="server" />
                        </td>
                        <td align="right" class="style2" width="10%">
                            物品名称<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtProductName" runat="server" MaxLength="10" onclick="popTechObj.ShowList('txtProductName');"  ReadOnly="true" CssClass="tdinput"  Width="95%"></asp:TextBox>
                            <input type="hidden" id="HidProductID" runat="server" />
                        </td>
                        <td align="right" class="style2" width="10%">
                            推荐程度<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="24%">
                            <select name="drpGrade"  class="tdinput"  id="drpGrade" >
                                        <option value="1" selected="selected">低</option>
                                        <option value="2">中</option><option value="3">高</option></select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            推荐人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="UsertxtJoiner" onfocus="alertdiv('UsertxtJoiner,HidJoiner');" runat="server"
                                             ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                             <input type="hidden" id="HidJoiner" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            推荐时间
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtJoinDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtJoinDate')})" ReadOnly="true"  CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            推荐理由
                        </td>
                        <td height="20" class="tdColInput" width="90%" colspan="5">
                             <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" style="width:95%"></textarea>
                             <input type="hidden" id="hidModuleID" runat="server" />
                        </td>
                        <%--<td height="20" align="right" class="tdColTitle" width="10%">
                            职务
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPosition" runat="server" MaxLength="10"  CssClass="tdinput"  Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            负责业务
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtOperation" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>--%>
                    </tr>
                   <%--
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

