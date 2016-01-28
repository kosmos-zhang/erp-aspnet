<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderLinkMan_Add.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderLinkMan_Add" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>


<%@ Register src="../../../UserControl/ProviderInfo.ascx" tagname="ProviderInfo" tagprefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建供应商联系人</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/office/PurchaseManager/ProviderLinkManAdd.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadPhoto.js" type="text/javascript"></script> 
    
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
    <span id="Forms" class="Spantype"></span>
    <uc1:Message  ID="Message1" runat="server" />
    <uc2:ProviderInfo ID="ProviderInfo1" runat="server" />
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
                                新建供应商联系人</div>
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
                                        &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="save_PurchaseReject" style="cursor:pointer" onclick="InsertProviderLinkManInfo();" visible="false" runat="server"/>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" />
                                    </td>
                                    <td align="right">
                                        <img  src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style=" float:right; cursor: pointer;"  id="imgPrint"  onclick="ProviderLinkManPrint();" /> 
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <!-- End 单据状态值 -->
                            <!-- Start 流程处理-->
                            <!-- End 流程处理-->
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="txtIsliebiaoNo" value="0" runat="server" />
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
                                        <div id='searchClick1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick1')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"   id="Tb_01" style="display: block">
                    <tr>
                        <td align="right" class="style2" width="10%">
                            联系人姓名<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtLinkManName" runat="server" MaxLength="10"  CssClass="tdinput"  Width="95%"  ></asp:TextBox>
                        </td>
                        <td align="right" class="style2" width="10%">
                            供应商名称<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="23%">
                            <asp:TextBox ID="txtCustName" MaxLength="25" onclick ="popProviderObj.ShowProviderList('txtCustName','txtCustName','txtCustNo')" ReadOnly="true" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtCustNo" runat="server" />
                        </td>
                        <td align="right" class="style2" width="10%">
                            性别<span class="redbold">*</span>
                        </td>
                        <td class="style3" width="24%">
                            <select name="drpSex"  class="tdinput"  id="drpSex" >
                                        <option value="1" selected="selected">男</option>
                                        <option value="2">女</option></select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            重要程度<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                                <select name="drpImportant"  class="tdinput"  id="drpImportant" >
                                        <option value="1" selected="selected">不重要</option><option value="2">普通</option>
                                        <option value="3">重要</option><option value="4">关键</option></select>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单位
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtCompany" runat="server" MaxLength="25"  CssClass="tdinput"  Width="95%"  SpecialWorkCheck="单位" ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            称谓
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtAppellation" runat="server" MaxLength="10"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="称谓" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            部门
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <asp:TextBox ID="txtDepartment" runat="server" MaxLength="10"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="部门" ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            职务
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPosition" runat="server" MaxLength="10"  CssClass="tdinput"  Width="95%"  SpecialWorkCheck="职务" ></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <%--<asp:TextBox ID="txtOperation" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            负责业务
                        </td>
                        <td height="20" class="tdColInput" width="90%" colspan="5">
                             <textarea name="txtOperation" id="txtOperation" rows="3" cols="80" style="width:95%"></textarea>
                        </td>
                    </tr>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        联系信息
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
                    id="Tb_02" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            工作电话
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                               <asp:TextBox ID="txtWorkTel" runat="server" MaxLength="15"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="工作电话"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            传真
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtFax" runat="server" MaxLength="200"  CssClass="tdinput" Width="95%" SpecialWorkCheck="传真"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            移动电话
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtHandset" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="移动电话"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            邮件地址
                        </td>
                        <td class="tdColInput" width="23%">
                             <asp:TextBox ID="txtMailAddress" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="邮件地址"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            家庭电话
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtHomeTel" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="家庭电话"  MaxLength="200"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            MSN
                        </td>
                        <td class="tdColInput" width="24%">
                            <asp:TextBox ID="txtMSN" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="MSN"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            QQ
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <asp:TextBox ID="txtQQ" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="QQ"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            邮编
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPost" runat="server" MaxLength="10"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="邮编"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            联系人类型
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="drpLinkType" class="tdinput" width="119px" runat="server" id="drpLinkType"></select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            年龄
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                             <asp:TextBox ID="txtAge" runat="server"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="年龄"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            证件类型
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtPaperType" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="证件类型"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            证件号
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <asp:TextBox ID="txtPaperNum" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="证件号"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            最后更新日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtModifiedDate" runat="server" MaxLength="25"  ReadOnly="true"  Enabled="False"  CssClass="tdinput" Width="95%"></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput" runat="server" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            最后更新用户
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtModifiedUserIDReal" name="txtModifiedUserIDReal" class="tdinput" disabled runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID2" name="txtModifiedUserID2" class="tdinput" runat="server" readonly />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            住址
                        </td>
                        <td height="20" class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtHomeAddress" id="txtHomeAddress" rows="3" cols="80" style="width:95%"></textarea>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        关怀点
                                    </td>
                                    <td align="right">
                                        <div id='divButtonNote'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','divButtonNote')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            生日
                        </td>
                        <td class="tdColInput" width="23%">
                           <asp:TextBox ID="txtBirthday" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtBirthday')})" ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            爱好
                        </td>
                        <td class="tdColInput" width="23%">
                            <asp:TextBox ID="txtLikes" runat="server" MaxLength="100"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="爱好"></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle" width="10%">
                            
                        </td>
                        <td class="tdColInput" width="24%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            照片
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <%--<img id="imgPhoto" runat="server" src="~/Images/Pic/Pic_Nopic.jpg" height="145" width="200" />
                            <div id="divUploadResume" runat="server">
                                    <a href="#" onclick="DealResume('upload');">上传照片</a>
                                </div>
                                <div id="divDealResume" runat="server" style="display:none;">
                                    <a href="#" onclick="DealResume('download');">下载照片</a>
                                    <a href="#" onclick="DealResume('clear');">删除照片</a>
                                </div><asp:HiddenField ID="hfPageAttachment" runat="server" />--%>
                            <table>
                        <tr>
                            <td colspan="2">
                                <img id="imgPhoto" runat="server" src="~/Images/Pic/Pic_Nopic.jpg" height="145" width="200" />
                                <asp:HiddenField ID="hfPhotoUrl" runat="server" />
                                <asp:HiddenField ID="hfPagePhotoUrl" runat="server" />
                                <input type="hidden" id="Hidden1" /><input type="hidden" id="uploadKind" />
                            </td>
                        </tr>
                        <tr>
                            <td><a href="#" onclick="DealEmployeePhoto('upload');">上传相片</a></td>
                            <td><a href="#" onclick="DealEmployeePhoto('clear');">清除相片</a></td>
                        </tr>
                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle" width="10%">
                            备注
                        </td>
                        <td class="tdColInput" width="90%" colspan="5">
                            <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" style="width:95%"></textarea>
                            <input name='usernametemp' type='hidden' id='usernametemp' runat="server" />
                            <input name='datetemp' type='hidden' id='datetemp' runat="server" />
                            <input id="txtAction" type="hidden" value="1" />
                            <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server"/>
                            <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                            <input type="hidden" id="hidModuleID" runat="server" />
                            <asp:DropDownList ID="drpApplyReason" runat="server" Width="0" Height="0"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

