<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeExaminationReport.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeExaminationReport" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>
 


<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>考试数量分析</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <script src="../../../js/office/HumanManager/PerformanceDetailsByLT.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        </style>
</head>
<body>

<form id="frmMain" runat="server" >

<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server"  />
 
<div id="PerformanceTypeCheck"   >
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td   class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" >
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                <td class="tdColTitle" width="10%">
                                      部门</td>
                                    <td class="tdColInput" width="23%">
                                       <asp:TextBox ID="DeptApply"  onclick="alertdiv('DeptApply,hidDeptID');"    
                                    Width="95%" CssClass="tdinput" runat="server"        ></asp:TextBox>
                                <input type="hidden" id="hidDeptID" runat="server"/>
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                      人员</td>
                                    <td class="tdColInput" width="23%">
                                         <asp:TextBox ID="UserEmployeeID" MaxLength="50" onclick="alertdiv('UserEmployeeID,txtSearchEmployeeID');"
                                runat="server" CssClass="tdinput" style="width:100%"></asp:TextBox>
                                       <input type="hidden" id="txtSearchEmployeeID" runat="server"/>
                                
                                    </td>
                                      <td height="20" class="tdColTitle" width="10%">
                                                                                起始日期</td>
                                    <td class="tdColInput" width="23%">
                                                 <input name="txtStartDate" readonly="readonly" id="txtStartDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"/>至
                                        <input readonly="readonly" id="txtStartToDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartToDate')})"/></td>
                                </tr>     
                                <tr>
                                  
                                    <td class="tdColTitle" width="10%">
                                        结束日期</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtEndDate" readonly="readonly" id="txtEndDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"/>至
                                        <input readonly="readonly" id="txtEndToDate" runat="server" maxlength="10" size="10" type="text" class="tdinput"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndToDate')})"/>
                                            
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">&nbsp;</td>
                                    <td class="tdColInput" width="23%">
                                                    &nbsp;</td>
                                 <td height="20" class="tdColTitle" width="10%">
                                        &nbsp;</td>
                                    <td class="tdColInput" width="23%">
                                        &nbsp;</td>
                                </tr>  
                                       
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">  
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <uc1:Message ID="Message1" runat="server" />
                                     <%--   <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="true" runat="server" style='cursor:pointer;' onclick='DoSearchInfo();' width="52" height="23" /><asp:Button   ID="" runat="server" Text="Button" onclick="btnQuery_Click1"  style="height: 26px" />--%>
                                        <asp:ImageButton ID="btnQuery" runat="server"       ImageUrl="../../../images/Button/Bottom_btn_search.jpg"  Visible="false"  onclick="btnQuery_Click"/>
                                    
                                        <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false" style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                    
                 </table>
            </td>
        </tr>
             <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title">考试数量分析</td>
                    </tr>
        
        <%--<tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                     
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('DeptName','oC0');return false;">
                                                考核等级<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th> 
                                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('passEmployeeName','oC1');return false;">
                                                人数<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th> 
                                    
                                    </tr>
                                </tbody>
                            </table> 
                  <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageClickInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divPage">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br/>    
            </td>
        </tr>--%>
        <tr><td colspan="3"  align="center">
       <%-- <asp:DropDownList id="ddlFormat" style="Z-INDEX: 105; LEFT: 188px; POSITION: absolute; TOP: 16px" runat="server">
					<asp:ListItem Value="Rich Text (RTF)">Rich Text (RTF)</asp:ListItem>
					<asp:ListItem Value="Portable Document (PDF)">Portable Document (PDF)</asp:ListItem>
					<asp:ListItem Value="MS Word (DOC)">MS Word (DOC)</asp:ListItem>
					<asp:ListItem Value="MS Excel (XLS)">MS Excel (XLS)</asp:ListItem>
				</asp:DropDownList><asp:Button id="btnExport"  runat="server" Width="78px" 
                Text="导出" onclick="btnExport_Click"></asp:Button>--%>
				 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" PrintMode="ActiveX"  HasCrystalLogo="False" /> 
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
            </td>
            
        </tr>
    </table>
    
    
   </div> 
     
</form>
<p>
&nbsp;&nbsp;&nbsp;
</p>
</body>
</html>
