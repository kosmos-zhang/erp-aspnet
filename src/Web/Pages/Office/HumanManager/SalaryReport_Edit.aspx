<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryReport_Edit.aspx.cs" Inherits="Pages_Office_HumanManager_SalaryReport_Edit" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建工资报表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/SalaryReport_Edit.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">
       function PrintCashFlow()
       {
if (document .getElementById ("divCodeNo").innerHTML=="" || document .getElementById ("divCodeNo").innerHTML==null )
{
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
       return ;
}else
{  var searchStr = document .getElementById ("divCodeNo").innerHTML;
         window.open("PrintSalaryReportEdit.aspx?ID="+searchStr);

}
       
       }
    </script>
</head>
<body>
<form id="frmMain" runat="server">
<input id="hidIsliebiao" type="hidden"  runat="server"/>
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建工资报表</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td valign="top"  align="left" style="width:90%">
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand;float:left; vertical-align:top  "  onclick="DoSave();"/>
                             <%--      <img src="../../../Images/Button/UnClick_bc.jpg" runat="server" visible="true" alt="保存" id="btnUnSave"  style="cursor:hand;float:left; display :none; vertical-align:top "  />--%>
                                    <img src="../../../Images/Button/btn_qxsc.jpg" runat="server"   visible="false" alt="重新生成" id="ImgReBuild" onclick="DoDelete();" style="cursor:hand;  float:left ; vertical-align:top  "  />
                                    <span id="GlbFlowButtonSpan" style="float :left; vertical-align:top" runat="server" visible="false"></span>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand; float:left; vertical-align:top  " />
                                </td>
                                <td align="right" class="tdColInput">
                                      <img src="../../../images/Button/Main_btn_print.jpg" alt="打印" id="btnPrint" runat="server"
                                              onclick="PrintCashFlow();" />
                                </td>
                            </tr>
                        </table>
                            
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td>
<!-- <div style="height:500px;overflow-y:scroll;"> -->
<table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
    <tr>
        <td  colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                        <table width="100%" align="center" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>基本信息 </td>
                                <td align="right">
                                    <div id='divBaseInfo'>
                                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblBaseInfo','divBaseInfo')"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" height="0">
                        <input type="hidden" id="hidIdentityID" value="" runat="server" />
                        <input type="hidden" id="hidModuleID" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                    <td height="20" class="tdColTitle" width="10%">工资报表编号<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%">
                        <div id="divCodeRule" runat="server">
                            <uc1:CodingRule ID="codeRule" runat="server" />
                        </div>
                        <div id="divCodeNo" runat="server" class="tdinput" style="display:none" disabled="true" ></div>
                        <input type="hidden" id="txtReportNo" runat="server"   />
                    </td>
                    <td height="20" class="tdColTitle" width="10%">工资报表主题<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" width="23%" align="center">
                        <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" CssClass="tdinput" Width="98%" SpecialWorkCheck="工资报表主题"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle" width="10%">报表状态</td>
                    <td height="20" class="tdColInput" width="24%" align="center">
                        <asp:TextBox ID="txtReportStatus" Width="98%"  runat="server" Enabled="false" CssClass="tdinput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">所属月份<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput">
                  
                                                
                        <asp:DropDownList ID="ddlYear" runat="server" 
                            onselectedindexchanged="ddlYear_SelectedIndexChanged"  ></asp:DropDownList>年
                        <asp:DropDownList ID="ddlMonth" runat="server" 
                            onselectedindexchanged="ddlMonth_SelectedIndexChanged" >
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        
                        月
                    </td>
                    <td height="20" class="tdColTitle">开始时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" align="center">
                        <asp:TextBox ID="txtStartDate" Width="98%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle">结束时间<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" align="center">
                        <asp:TextBox ID="txtEndDate" Width="98%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="20" class="tdColTitle">编制人<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" align="center">
                        <asp:TextBox  ID="UserCreator" runat="server" ReadOnly="true" Width="98%"  CssClass="tdinput"></asp:TextBox>
                        <input type="hidden" id="txtCreator" runat="server" />
                    </td>
                    <td height="20" class="tdColTitle">编制日期<span class="redbold">*</span></td>
                    <td height="20" class="tdColInput" align="center">
                        <asp:TextBox ID="txtCreateDate" Width="98%" runat="server" ReadOnly="true" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCreateDate')})"></asp:TextBox>
                    </td>
                    <td height="20" class="tdColTitle"></td>
                    <td height="20" class="tdColInput"></td>
                </tr>            
                <tr>
                    <td colspan="6" align="center" bgcolor="#FFFFFF" height="25">
                        <img src="../../../Images/Button/cw_scbb.jpg" runat="server" visible="false" alt="生成报表" id="btnCreateReport" style="cursor:hand"   onclick="DoCreateReport();"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="5"></td></tr>
    <tr>
        <td height="25" valign="top" colspan="2">
            <div id="divSalaryDetailInfo" runat="server"  style="overflow-y:auto;width:100%; line-height:14pt;letter-spacing:0.2em;height:500px;  overflow-x:auto"></div>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>  
</table>
<!-- </div> -->
</td>
</tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
<input type="hidden" id="txtBillStatus" name="txtBillStatus"  runat="server"  value="1" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <uc1:FlowApply ID="FlowApply1" runat="server" />
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIdentityID" value="0" runat="server" />
</form>
</body>

<script type="text/javascript">
    var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_HUMAN %>;
    var glb_BillTypeCode = <%=XBase.Common.ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT %>;
    var glb_BillID = parseInt(document.getElementById("txtIdentityID").value);//单据ID
    var glb_IsComplete = false;
    var FlowJS_HiddenIdentityID ='txtIdentityID';
    var FlowJs_BillNo ='txtReportNo';
    var FlowJS_BillStatus ='txtBillStatus';
//    GetFlowButton_DisplayControl();
</script>
<script src="../../../js/common/Flow.js" type="text/javascript"></script>
</html>

