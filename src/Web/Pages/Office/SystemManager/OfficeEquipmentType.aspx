<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeEquipmentType.aspx.cs" Inherits="Pages_Office_SystemManager_OfficeEquipmentType" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>办公用品分类</title>

        <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        #treeContainer A{color:Black;text-decoration:none;}
        #treeContainer TD{padding:1px;}
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="txtID" />
    <div>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
        <tr>
          <td bgcolor="#F4F0ED" class="Blue" align="left">
          
          <table border="0" cellpadding="3" height="300" width="95%">
            <tr>
                <td valign="top" width="200" style="padding-left:20px;" id="treeContainer">
                    
                    <input id="hf_flag" type="hidden" value="1" />
                    
                </td>
                <td valign="top">
                    <table cellpadding="2">
                        <tr><td></td><td class="Title"><span id="curAction" style="color:Black;font-weight:bold;">办公用品分类</span></td></tr>
                        <tr><td></td><td><br />
             <a href="#" id="img_new">
                <img border="0" onclick="treeview_cancel()" style="cursor:pointer;" 
                                src="../../../images/Button/Bottom_btn_new.jpg" runat="server" visible="false" id="btnNew" /></a><img id="img_save" alt="" src="../../../Images/Button/Bottom_btn_save.jpg" 
                               onclick="treeview_save();" runat="server" visible="false" /><a href="#" id="img_delete" ><img 
                                border="0" onclick="treeview_delete()" style="cursor:pointer;" 
                                src="../../../images/Button/Main_btn_delete.jpg"  id="btnDel" runat="server" visible="false"/></a></td></tr>
                    </table>
                                        
                                        
                                                        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" bgcolor="#E6E6E6">
                            类别名称<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                            &nbsp;<input type="text" id="CodeName" specialworkcheck="类别名称" name="TypeName" class="tdinput"/></td>
                        <td align="right" bgcolor="#E6E6E6">
                            上级分类<span class="redbold">*</span>                         </td>
                        <td bgcolor="#FFFFFF">
                            &nbsp;<select id="slSupperTypeID" name="D1" ></select></td>
                        <td align="right" bgcolor="#E6E6E6">
                            启用状态
                        </td>
                        <td bgcolor="#FFFFFF">
                            &nbsp;<font color="red"><select id="UsedStatus" runat="server" name="SetPro2" width="139px">
                           <option value="1">启用</option>
                          <option value="0">停用</option>
                                </select></font></td>
                    </tr>
                    <tr id="CloseDate">
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            报警下限
                        </td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <input type="text" id="txt_WarningLimit" name="TypeName0" class="tdinput"/></td>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            描述
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="tdinput" colspan="3">
                            <input type="text" id="txt_Description"  specialworkcheck="描述" name="TypeName1" class="tdinput"/></td>
                    </tr>
                    </table>
                                        
                                    <br />
                                        
                </td>
            </tr>
          </table>
        
          </td>
        </tr>
      </table>
    </div>
    <uc1:Message ID="Message1" runat="server" />
    </form>
</body>
</html>
<script src="../../../js/office/SystemManager/CategorySet.js" type="text/javascript"></script>
