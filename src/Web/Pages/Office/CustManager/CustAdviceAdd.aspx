<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustAdviceAdd.aspx.cs" Inherits="Pages_Office_CustManager_CustAdviceAdd" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CustNameSel.ascx" TagName="CustNameSel" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/CustLinkManSel.ascx" TagName="CustLinkManSel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户建议</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/DeleteFile.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/office/CustManager/CustAdviceAdd.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
<script type="text/javascript">
function BackToPage()
{
    if(typeof(ListType)!="undefined")
    {
        window.location.href='CustColligate.aspx?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
    }
    else
    {
        history.back(); 
    }
   
}
   function PrintCust()
   {
        var AdjustID=document.getElementById('hiddenCustAdviceID').value;
        if(parseFloat(AdjustID)>0)
        {
            if(!confirm('请确认您的单据已保存？'))
            {
                return false;
            }
            window.open("../../../Pages/Office/CustManager/PrintCustAdvice.aspx?ID="+AdjustID);
        }
   }
</script>
</head>
<body>
    <br />
    <form id="Form1" runat="server">
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <input id="hiddenCustAdviceID" type="hidden" value="0" /><!--存储本单据ID-->
    <!-- Start 消息提示-->
    <uc1:Message ID="Message1" runat="server" />
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
                            <%if (this.CustAdviceID > 0)
                              { %>
                            客户建议
                            <%}
                              else
                              { %>
                            新建客户建议
                            <%} %>
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
                                                    <img runat="server" visible="false" id="btnSave" src="../../../images/Button/Bottom_btn_save.jpg"
                                                        onclick="Fun_Save_Cust();" style="cursor: pointer" title="保存客户建议" />
                                                </td>
                                                <td>
                                                    <img runat="server" visible="false" id="UnbtnSave" src="../../../Images/Button/UnClick_bc.jpg"
                                                        style="cursor: pointer; display: none" />
                                                </td>
                                                <td>
                                                    <img onclick="BackToPage();" style="display: none" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
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
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
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
                            建议编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="25%">
                            <div id="divCodeRuleUC" runat="server">
                                <!-- Start Code Rule -->
                                <uc2:CodingRuleControl ID="checkNo" runat="server" />
                                <!-- End Code Rule -->
                            </div>
                            <div id="divTaskNo" style="display: none" class="tdinput">
                                <input id="lbInfoNo" class="tdinput" readonly runat="server" type="text" />
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            建议主题<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tdinput" MaxLength="100" Width="93%"></asp:TextBox>
                        </td>
                        <td class="tdColTitle">
                            提出建议人
                        </td>
                        <td class="tdColInput">
                            <input type="text" maxlength="20" id="txtAdvicer" class="tdinput" style="width: 100%" style="width: 90%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            提出建议客户 <span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="25%">
                            <uc3:CustNameSel ID="CustNameSel1" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            客户联系人
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <uc4:CustLinkManSel ID="CustLinkManSel1" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            接待人
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input id="UserDestClerk" style="width: 100%" readonly class="tdinput" type="text"
                                onclick="alertdiv('UserDestClerk,hiddenDestClerk');" />
                            <input id="hiddenDestClerk" type="hidden" value="0" />
                        </td>
                    </tr>
                    
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        建议信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            建议时间<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="25%">
                            <input id="txtAdviceDate" runat="server" style="width: 28%"  readonly onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})" class="tdinput" type="text" />
                             <asp:DropDownList ID="DropDownList1" runat="server">
                                </asp:DropDownList>时
                            &nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>分
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            建议类型<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select id="txtAdviceType">
                                <option value="1">不满意</option>
                                <option value="2">希望做到</option>
                                <option value="3">其他</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            
                        </td>
                    </tr>
                    <tr>
                        <td height="50" align="right" class="tdColTitle" width="10%">
                            建议内容<span class="redbold">*</span>
                        </td>
                        <td height="50" colspan="5" class="tdColInput" width="24%">
                            <asp:TextBox Height="100%" ID="txtContents" CssClass="tdinput" TextMode="MultiLine"
                                Width="100%"  runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
                
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        处理意见
                                    </td>
                                    <td align="right">
                                        <div id='div1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Table1">
                    
                   <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            领导意见
                        </td>
                        <td height="20" class="tdColInput" colspan="5" width="90%">
                         <%--   <input maxlength="500" type="text" id="" class="tdinput" style="width:90%"  />--%>
                            <textarea id="txtLeadSay" class="tdinput" style="width:99%; height:40px" cols="20" rows="2"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            对应措施
                        </td>
                        <td height="20" colspan="5"  class="tdColInput" width="24%">
                         <%--   <input maxlength="500" type="text" id="" style="width: 90%" class="tdinput" />--%>
                              <textarea id="txtDoSomething" class="tdinput" style="width:99%; height:40px" cols="20" rows="2"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采纳程度<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="25%">
                            <select id="txtAccept">
                                <option value="1">暂不考虑</option>
                                <option value="2">一般</option>
                                <option value="3">争取改进</option>
                                <option value="4">一定做到</option>
                            </select>
                        </td>
                         <td height="20" align="right" class="tdColTitle" width="10%">
                       状态<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                           <select id="txtState">
                                <option value="1">未处理</option>
                                <option value="2">处理中</option>
                                <option value="3">已处理</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                          
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                         
                        </td>
                    </tr>
                </table>
                
                
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        备注信息
                                    </td>
                                    <td align="right">
                                        <div id='divInfo'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divInfo')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04">
                    <tr>
                        <td class="tdColTitle" width="13%">
                            制单人
                        </td>
                        <td class="tdColInput" width="25%">
                            <asp:TextBox ID="tbCreater" disabled runat="server" CssClass="tdinput"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtCreateDate" MaxLength="50" runat="server" CssClass="tdinput"
                                disabled Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            最后更新人
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            最后更新时间
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" runat="server" CssClass="tdinput" Width="95%" disabled></asp:TextBox>
                        </td>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="3">
                            <asp:TextBox MaxLength="500" ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="tdinput"
                                Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            可查看该建议信息的人员</td>
                        <td height="20" class="tdColInput" colspan="5">
                            <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>

<script type="text/javascript">
var CustAdviceID ='<%=CustAdviceID %>';
</script>

