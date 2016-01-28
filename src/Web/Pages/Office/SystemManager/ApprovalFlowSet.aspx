<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalFlowSet.aspx.cs" Inherits="Pages_Office_SystemManager_ApprovalFlowSet" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Flow.ascx" tagname="Flow" tagprefix="uc2" %>
<%--<%@ Register src="../../../UserControl/Department.ascx" tagname="Department" tagprefix="uc3" %>//待修改--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>审批流程设置</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
<script type="text/javascript">
var typeflag="";
var typecode="";
var flagtext="";
var codetext="";
function SelectedNodeChanged(code_text,type_code,type__flag,flag_text)
{   
    document.getElementById('hd_typecode').value=type_code;
    document.getElementById('hd_typeflag').value=type__flag;
    document.getElementById('sel_status').disabled=false;
    typecode=type_code;
    typeflag=type__flag;
    flagtext=flag_text;
    codetext=code_text;
    popTechObj.ShowList();
}
function turntoaddpage()
{
if(typecode==""||typecode ==null)
{
 popMsgObj.ShowMsg('添加失败,请选择单据！')
 return;
}
var usestatus=document.getElementById("sel_status").value;
requestobj = GetRequest();
     recordnoparamcode= requestobj['TypeCodeflag'];
       hfModuleID= requestobj['ModuleID'];
 window.location.href='ApprovalFlowSetAdd.aspx?typeflag='+typeflag+'&typecode='+typecode+'&flagtext='+flagtext+'&codetext='+codetext+'&usestatus='+usestatus+'&ModuleID='+hfModuleID;
}
 </script>
    <style type="text/css">
        .style1
        {
            width: 88px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">   <uc1:Message ID="Message1" runat="server" />
                 <input id="Hidden3" type="hidden" />
             
                 <input id="hd_typecode" type="hidden" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
             <td height="30" colspan="2" align="center" valign="top" class="Title">
                 <input id="hd_typeflag" type="hidden" runat="server" />              
               流程信息列表
                 </td>
        </tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            &nbsp;
                            <img alt=""  src="../../../images/Button/Bottom_btn_new.jpg" 
                                onclick="turntoaddpage();" runat="server" visible="false" id="btnNew" /> <img 
                                alt="" src="../../../Images/Button/Main_btn_delete.jpg" 
                               onclick="delflowstep();" id="btnDel" runat="server" visible="false" /><asp:Label 
                                ID="Label1" runat="server" Text="流程状态"></asp:Label>
                            <select name="sel_status" onchange="Fun_Search_Flow();" class="tdinput" id="sel_status"  width="119px" runat="server" disabled="disabled">
                                <option value="">--请选择--</option>
                                          <option value="0">草稿</option>
                                          <option value="1">停止</option>
                                          <option value="2">发布</option>
                                       </select></td>
                    </tr>
                </table>
            </td>
          <tr>
          <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
          单据列表
          </td>
          <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
          审批流程列表
          </td>
          </tr>
          <tr>
         <td valign="top">
         <div style="width:200px; height:300px;overflow-x: hidden;overflow-y: auto;"> <asp:TreeView ID="Tree_BillTpye" runat="server" ShowLines="True" >
         </asp:TreeView>
         </div>
        
         </td>
        <td valign="top">
            <uc2:Flow ID="Flow1" runat="server" />
        </td>
          </tr>

    </table>
    </form>
</body>
</html>
