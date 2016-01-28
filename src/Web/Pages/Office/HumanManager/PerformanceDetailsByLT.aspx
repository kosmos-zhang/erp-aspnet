<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceDetailsByLT.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceDetailsByLT"  EnableEventValidation="false"%>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>
<%@ Register Src="../../../UserControl/Human/SelectedTaskList.ascx" TagName="TaskUC" TagPrefix="uc3" %>


<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>按考核等级分析</title>
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
             <script language="javascript" type="text/javascript">
             function fnSubmit1()
             {
        if (document .getElementById ("Type1").select )
        {
        document .getElementById ("Type2").select=false ;
        document .getElementById ("Type3").select=false ;
        }
        else if (document .getElementById ("Type2").select )
        {
            document .getElementById ("Type1").select=false ;
        document .getElementById ("Type3").select=false ;
        }
        else
        {
          document .getElementById ("Type1").select=false ;
        document .getElementById ("Type2").select=false ;
        }
        }
        
        
        </script>
</head>
<body>

<form id="frmMain" runat="server" >

<%--<div id="popupContent"  ></div>--%>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server"  />
<uc3:TaskUC ID="TaskUC1" runat="server" />
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
                                      考核类型</td>
                                    <td class="tdColInput" width="23%">
                                         <asp:DropDownList ID="ddlPerType" runat="server">
                                           </asp:DropDownList>
                                    </td>
                                      <td height="20" class="tdColTitle" width="10%">
                                                                                考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                                   <%--  onchange="getChangeAll()"--%>
                                          <asp:DropDownList ID="ddlTaskFlag" runat="server" 
                                            onselectedindexchanged="ddlTaskFlag_SelectedIndexChanged1" AutoPostBack ="true"   >
                                          <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="月考核" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="季考核" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="半年考核" Value="3"></asp:ListItem>
                                          <asp:ListItem Text="年考核" Value="4"></asp:ListItem>
                                          <asp:ListItem Text="临时考核" Value="5"></asp:ListItem>
                                           </asp:DropDownList>
                                    </td>
                                </tr>     
                                <tr>
                                  
                                    <td class="tdColTitle" width="10%">
                                        考核期间</td>
                                    <td class="tdColInput" width="23%">
                                    <div  id="Div2" style="float:left">
                                                  <asp:DropDownList ID="ddlTaskYear" runat="server">
                                           </asp:DropDownList>   </div>
                                 <div  id="dvSearchTaslNum" style="float:left">
                                               <asp:DropDownList ID="ddlTaskNum" runat="server" >
                                           </asp:DropDownList>   </div>
                                            
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">考核等级</td>
                                    <td class="tdColInput" width="23%">
                                                    <asp:DropDownList ID="ddlLevelType" runat="server">
                                                    <asp:ListItem Text ="--请选择--" Value="0"></asp:ListItem>
                                                     <asp:ListItem Text ="达到要求" Value="1"></asp:ListItem>
                                                      <asp:ListItem Text ="超过要求" Value="2"></asp:ListItem>
                                                      <asp:ListItem Text ="表现突出" Value="3"></asp:ListItem>
                                                       <asp:ListItem Text ="需要改进" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text ="不合格" Value="5"></asp:ListItem>
                                           </asp:DropDownList>
                                         
                                    </td>
                                 <td height="20" class="tdColTitle" width="10%">
                                        考核任务</td>
                                    <td class="tdColInput" width="23%">
                                      <asp:DropDownList ID="ddlTestNo" runat="server">
                                           </asp:DropDownList>
                                    </td>
                                </tr>  
                                       
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">  
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <uc1:Message ID="Message1" runat="server" />
                                     <%--   <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="true" runat="server" style='cursor:pointer;' onclick='DoSearchInfo();' width="52" height="23" /><asp:Button   ID="" runat="server" Text="Button" onclick="btnQuery_Click1"  style="height: 26px" />--%>
                                        <asp:ImageButton ID="btnQuery" runat="server"       ImageUrl="../../../images/Button/Bottom_btn_search.jpg"  Visible=" false"  onclick="btnQuery_Click"/>
                                    
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
                       <td height="30" align="right" valign="top" class="Title" style="padding-right: 25%;
                            width: 80%">按考核等级分析
                        </td>
                          <td height="30" align="right" valign="top" style="width: 20%; ">      <table border="0" cellpadding="0" cellspacing="0"  style=" margin-right:10px;">
                                <tr>
                                    <td>
                                  <%--      <input id="" type="radio" runat="server" onclick="fnSubmit1()" checked="true"
                                            onselect="fnSubmit1()" name="type" />--%>
                                        <asp:RadioButton ID="chkType1" runat="server" Text="柱状图" Checked="true" 
                                            oncheckedchanged="Type1_CheckedChanged"  AutoPostBack="true"  GroupName ="123"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                             <%--           <input id="" type="radio" runat="server" onclick="fnSubmit1()" onselect="fnSubmit1()"
                                            name="type" />--%>
                                                    <asp:RadioButton ID="chkType2" runat="server" Text="饼状图"  
                                             AutoPostBack="true" oncheckedchanged="chkType2_CheckedChanged"  GroupName ="123"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                <%--        <input id="" type="radio" runat="server" onclick="fnSubmit1()" onselect="fnSubmit1()"
                                            name="type" />--%>
                                                    <asp:RadioButton ID="chkType3" runat="server" Text="折线图"  
                                          AutoPostBack="true" oncheckedchanged="chkType3_CheckedChanged"  GroupName ="123"/>
                                    </td>
                                </tr>
                            </table></td>
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
                AutoDataBind="true"  PrintMode="ActiveX"  HasCrystalLogo="False"/> 
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
            </td>
            
        </tr>
    </table>
    
    
   </div> 
     <div id="divEditCheckItem" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:300; position: absolute;top: 20%; left: 15%;  display:none  ">    
     <input id="hidEditFlag" type="hidden" />
    <input id="hidElemID" type="hidden" />
    
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="Table1">
    
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="tdColInput">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />
       <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="true" alt="保存" id="btnSave" style="cursor:hand" height="25" onclick="DoSave();"/>
        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand" height="25" onclick="DoBack();"/>                            
            </td>
            <td align="right" valign="top" class="tdColInput">
                <div id='div1'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="Table2"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核任务编号</td>
                                    <td class="tdColInput" width="23%">
                                        
                                        <div id="txtPerformTmNo">
                                            <uc2:codingrule ID="AimNum" runat="server" class="tdinput" />
                                        </div>
                                        <input id="inpTaskNo" type="text"  style=" display:none " disabled ="true" />
                                        
                                    </td>
                                    <td class="tdColTitle" width="10%">考核任务主题</td>
                                    <td class="tdColInput" width="23%">
                                        <input id="inptTitle" type="text" maxlength="25" size="25" /></td>
                                </tr>     
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间类型</td>
                                    <td class="tdColInput" width="23%">
                                    <select id="selTaskFlag" runat="server"  onclick="getChange(this);" name="D1">
                                            <option value="1">月考核</option>
                                            <option value="2">季考核</option>
                                             <option value="3">半年考核</option>
                                            <option value="4">年考核</option>
                                            <option value="5">临时考核</option>
                                        </select></td>
                                    <td class="tdColTitle" width="10%">考核周期</td>
                                    <td class="tdColInput" width="23%">
                                
                                      <div  id="dvTaslNum">
                                
                                                <select id="selTaskNum" runat="server" name="D2" style="display:block">
                                                    <option value="1">1月</option>
                                                    <option value="2">2月</option>
                                                    <option value="3">3月</option>
                                                    <option value="4">4月</option>
                                                    <option value="5">5月</option>
                                                    <option value="6">6月</option>
                                                    <option value="7">7月</option>
                                                    <option value="8">8月</option>
                                                    <option value="9">9月</option>
                                                    <option value="10">10月</option>
                                                    <option value="11">11月</option>
                                                    <option value="12">12月</option>
                                                </select></div></td>
                                </tr>  
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核期间开始日期</td>
                                    <td class="tdColInput" width="23%">
                           <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                       
                                         
                                    </td>
                                    <td class="tdColTitle" width="10%">考核期间结束日期</td>
                                    <td class="tdColInput" width="23%">
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})"></asp:TextBox>
                    
                                    </td>
                                </tr> 
                                   <tr>
                                    <td height="20" class="tdColTitle" width="10%">备注</td>
                                    <td class="tdColInput" width="23%" colspan="3">
                                       <input id="inpRemark" type="text"  size="25" maxlength ="50"/>
                                         
                                    </td>
                                </tr> 
                                <tr>
                                
                                <td colspan="2" align="center">模板</td>
                                <td colspan="2" align="center">被考核人员</td>
                                </tr>  
                                <tr style="background-color:#FFFFFF">
                                <td  colspan="2" align="center">
                                <table id="tbTemplate" class="tab_a" >
                                <tbody align="center"></tbody>
                                
                                
                                </table>
                                
                                </td>
                               <td  colspan="2" align="center"  >
                                 <table id="tbEmployee"  class="tab_a">
                                <tbody align="center"></tbody>
                                
                                
                                </table></td> 
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
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
