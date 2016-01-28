<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeConditionMonthReport.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeConditionMonthReport" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>
 


<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>人员状况月报</title>

    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  
    
    </script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
 
 
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
                                      <asp:DropDownList ID="ddlDeptName" runat="server">
                                           </asp:DropDownList>
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                      年月</td>
                                    <td class="tdColInput" width="23%">
                                          
                                    <asp:DropDownList ID="ddlTaskYear" runat="server">
                                           </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlTaskNum" runat="server" >
                                    <asp:ListItem Value ="01" Text ="1月"></asp:ListItem>
                                    <asp:ListItem Value ="02" Text ="2月"></asp:ListItem>
                                    <asp:ListItem Value ="03" Text ="3月"></asp:ListItem>
                                    <asp:ListItem Value ="04" Text ="4月"></asp:ListItem>
                                    <asp:ListItem Value ="05" Text ="5月"></asp:ListItem>
                                     <asp:ListItem Value ="06" Text ="6月"></asp:ListItem>
                                      <asp:ListItem Value ="07" Text ="7月"></asp:ListItem>
                                       <asp:ListItem Value ="08" Text ="8月"></asp:ListItem>
                                        <asp:ListItem Value ="09" Text ="9月"></asp:ListItem>
                                        <asp:ListItem Value ="10" Text ="10月"></asp:ListItem>
                                         <asp:ListItem Value ="11" Text ="11月"></asp:ListItem>
                                          <asp:ListItem Value ="12" Text ="12月"></asp:ListItem>
                                           </asp:DropDownList>  
                                    </td>
                                      <td height="20" class="tdColTitle" width="10%">
                                                                               </td>
                                    <td class="tdColInput" width="23%">
                                            
                                            </td>
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
                        <td height="20" colspan="2" align="center" valign="top" class="Title">人员状况月报</td>
                    </tr>
        

        <tr><td colspan="5"  align="left">
      
                <span style="text-align:left"></span>
				 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" PrintMode="ActiveX"   HasCrystalLogo="False"  /> 
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
