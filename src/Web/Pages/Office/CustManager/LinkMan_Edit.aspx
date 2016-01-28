<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkMan_Edit.aspx.cs" Inherits="Pages_Office_CustManager_LinkMan_Edit" %>

<%@ Register src="../../../UserControl/CustNameSel.ascx" tagname="CustNameSel" tagprefix="uc1" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeTypeDrpControl" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户联系人</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/office/CustManager/LinkManEdit.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadPhoto.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">     
      <input type="hidden" id="uploadKind" />                   
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
                            
                            客户联系人                            
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">                           
                            <table width="100%">
                                <tr>
                                    <td><img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" id="btn_save" style="cursor:hand" onclick="SaveLinkData();" visible="false" runat="server" />
                                           <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand;" onclick="Back();" />      
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer" title="打印" onclick="PagePrint();" />
                                    </td>
                                </tr>
                            </table>                          
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
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>基础信息  
                      <asp:HiddenField ID="hfLinkID" runat="server" />
                    </td>
                  <td align="right"><div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_01','searchClick')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01" ><!--style="display:block"-->
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">对应客户<span class="redbold">*</span></td>
                <td height="20" bgcolor="#FFFFFF" style="width:23%">
                    
                    <input id="txtCustNam" style="width:95%" type="text" class="tdinput" disabled="disabled"/><input id="hfCustNo" type="hidden" />
                  </td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">联系人姓名<span class="redbold">*</span></td>
                <td height="20" bgcolor="#FFFFFF" style="width:23%">
                    <input name="txtLinkManName" id="txtLinkManName" type="text" class="tdinput" size="15" 
                        maxlength="10" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">性别<span class="redbold">*</span></td>
                <td height="20" bgcolor="#FFFFFF" style="width:24%">
                    <select  name="seleSex" width="20px" id="seleSex">
                                  <option value="1">男</option>
                                  <option value="2">女</option>
                                </select></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">重要程度</td>
                <td height="20" bgcolor="#FFFFFF">
                    <select  name="seleImportant" 
                        width="20px" id="seleImportant">
                                <option value="0">请选择</option>
                                  <option value="1">不重要</option>
                                  <option value="2">普通</option>
                                  <option value="3">重要</option>
                                  <option value="4">关键</option>
                                </select></td>
                <td height="20" align="right" bgcolor="#E6E6E6">联系人类型</td>
                <td height="20" bgcolor="#FFFFFF">                    
                    <asp:DropDownList ID="ddlLinkType" runat="server">
                    </asp:DropDownList>                    
                            </td>
                <td height="20" align="right" bgcolor="#E6E6E6">年龄(岁)</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtAge" id="txtAge" type="text" class="tdinput" size="15" maxlength="3" /></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">称谓</td>
                <td height="20" bgcolor="#FFFFFF">
                    <input name="txtAppellation" id="txtAppellation" type="text" class="tdinput" size="15" 
                        maxlength="10" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6">负责业务</td>
                <td height="20" bgcolor="#FFFFFF" colspan="3">                    
                    <input name="txtOperation" 
                        id="txtOperation" type="text" class="tdinput" maxlength="25" style="width:95%" /></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">单位名称</td>
                <td height="20" bgcolor="#FFFFFF">
                    <input name="txtCompany" id="txtCompany" type="text" class="tdinput" style="width:95%" maxlength="25" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6">职务</td>
                <td height="20" bgcolor="#FFFFFF">                    
                    <input name="txtPosition" 
                        id="txtPosition" type="text" class="tdinput" size="15" maxlength="10" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6">部门</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtDepartment" id="txtDepartment" maxlength="10" type="text" class="tdinput" size="15" /></td>
              </tr>
              </table>
              
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>联系信息 </td>
                  <td align="right"><div id='searchClick3'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_03','searchClick3')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_03" ><!--style="display:block"--> 
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">工作电话</td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input name="txtWorkTel" id="txtWorkTel" type="text" maxlength="25" class="tdinput" size="15" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">手机号</td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input name="txtHandset" 
                        id="txtHandset" type="text" class="tdinput" maxlength="11" /></td>
                <td align="center" bgcolor="#FFFFFF" rowspan="6"> <table>
                        <tr>
                            <td colspan="2">
                                <img id="imgPhoto" runat="server" src="~/Images/Pic/Pic_Nopic.jpg" height="145" width="200" />
                                <asp:HiddenField ID="hfPhotoUrl" runat="server" />
                                <asp:HiddenField ID="hfPagePhotoUrl" runat="server" />
                                <input type="hidden" id="Hidden1" />
                            </td>
                        </tr>
                        <tr>
                            <td><a href="#" onclick="DealEmployeePhoto('upload');">上传相片</a></td>
                            <td><a href="#" onclick="DealEmployeePhoto('clear');">清除相片</a></td>
                        </tr>
                    </table></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">传真</td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width:23%" >
                    <input name="txtFax" id="txtFax" type="text" class="tdinput" maxlength="50"  style="width: 95%" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">家庭电话</td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width:23%" >
                    <input name="txtHomeTel" id="txtHomeTel" type="text" class="tdinput" maxlength="50" style="width: 95%" /></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">Email</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtMailAddress" id="txtMailAddress" maxlength="30" type="text" class="tdinput" style="width:95%"  /></td>
                <td height="20" align="right" bgcolor="#E6E6E6">邮编</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtPost" id="txtPost" type="text" maxlength="20" class="tdinput" size="15" /></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">MSN</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtMSN" id="txtMSN" type="text" maxlength="30" class="tdinput" size="15" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6">QQ</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtQQ" id="txtQQ" type="text" maxlength="20" class="tdinput" size="15" /></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">证件类型</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtPaperType" id="txtPaperType" maxlength="25" type="text" class="tdinput" size="15" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6">证件号</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <input name="txtPaperNum" id="txtPaperNum" maxlength="25" type="text" class="tdinput" style="width:95%"  /></td>
              </tr>
              <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">住址</td>
                <td height="20" align="left" bgcolor="#FFFFFF" colspan="3">
                    <input name="txtHomeAddress" id="txtHomeAddress" maxlength="100" type="text" class="tdinput" size="60" /></td>
              </tr>
              </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>客户关怀 </td>
                  <td align="right"><div id='searchClick4'><img src="../../../images/Main/Open.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_04','searchClick4')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04" style="display:none;">
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            生日
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="txtBirthday" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtUpdateCredit')})" />
                            <input type="hidden" id="Hidden2" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            籍贯
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" width="23%">
                            <input type="text" class="tdinput" id="HomeTown" maxlength="50" style="width:99%" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            民族
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            &nbsp;<uc3:CodeTypeDrpControl ID="CodeTypeDrpControl1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            出生地
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="Birthcity" type="text" class="tdinput" style="width:99%"/>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            所受教育
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" width="23%">
                            &nbsp;<uc3:CodeTypeDrpControl ID="CodeTypeDrpControl2" runat="server" />
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            所学专业
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <uc3:CodeTypeDrpControl ID="CodeTypeDrpControl3" runat="server" />
                            &nbsp;
                        </td>
                    </tr>                   
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            毕业学校
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="GraduateSchool" type="text" class="tdinput" style="width:99%"/>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            年收入情况
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" width="23%">
                            <input type="text" class="tdinput" id="IncomeYear" style="width:99%"/>
                        </td>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            饮食偏好
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <input type="text" class="tdinput" id="FuoodDrink" style="width:99%" />
                        </td>
                    </tr>
                     <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            专业描述</td>
                        <td height="20" align="left" bgcolor="#FFFFFF" colspan="5">
                             <textarea name="Professional" class="tdinput" 
                                style="width: 99%; height: 40px" id="Professional"
                                rows="3" cols="80"></textarea></td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢什么样的音乐
                        </td>
                        <td height="20" align="left" colspan="5" bgcolor="#FFFFFF" style="width: 23%">
                          
                                 <textarea name="LoveMusic" class="tdinput" style="width: 99%; height: 40px" id="LoveMusic"
                                rows="3" cols="80"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢哪些颜色
                        </td>
                        <td height="20" align="left"  colspan="5" bgcolor="#FFFFFF" width="23%">
                         
                               <textarea name="LoveColor" class="tdinput" style="width: 99%; height: 40px" id="LoveColor"
                                rows="3" cols="80"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢什么品牌的香烟
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF">
                          
                             <textarea name="LoveSmoke" class="tdinput" style="width: 99%; height: 40px" id="LoveSmoke"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            爱喝什么酒
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" style="width: 23%">
                   
                                <textarea name="LoveDrink" class="tdinput" style="width: 99%; height: 40px" id="LoveDrink"
                                rows="3" cols="80"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            爱喝什么茶
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" width="23%">
               
                             <textarea name="LoveTea" class="tdinput" style="width: 99%; height: 40px" id="LoveTea"
                                rows="3" cols="80"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢哪类书籍
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF">
                        
                              <textarea name="LoveBook" class="tdinput" style="width: 99%; height: 40px" id="LoveBook"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢什么样的运动
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" style="width: 23%">
                       
                              <textarea name="LoveSport" class="tdinput" style="width: 99%; height: 40px" id="LoveSport"
                                rows="3" cols="80"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢哪些品牌的服饰
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" width="23%">
                       
                              <textarea name="LoveClothes" class="tdinput" style="width: 99%; height: 40px" id="LoveClothes"
                                rows="3" cols="80"></textarea>
                        </td>
                        </tr>
                        <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            喜欢哪些品牌的化妆品
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF">
                           
                            <textarea name="Cosmetic" class="tdinput" style="width: 99%; height: 40px" id="Cosmetic"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            性格描述
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <textarea name="Nature" class="tdinput" style="width: 99%; height: 40px" id="Nature"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            外表描述
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" width="23%">
                            <textarea name="Appearance" class="tdinput" style="width: 99%; height: 40px" id="Appearance"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            健康状况
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF">
                            <textarea name="AdoutBody" class="tdinput" style="width: 99%; height: 40px" id="AdoutBody"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            家人情况
                        </td>
                        <td height="20" align="left" colspan="5" bgcolor="#FFFFFF" style="width: 23%">
                            <textarea name="AboutFamily" class="tdinput" style="width: 99%; height: 40px" id="AboutFamily"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            开什么车
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF" width="23%">
                            <textarea name="Car" class="tdinput" style="width: 99%; height: 40px" id="Car" rows="3"
                                cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" style="width: 10%">
                            住房情况
                        </td>
                        <td height="20" colspan="5" align="left" bgcolor="#FFFFFF">
                            <textarea name="LiveHouse" class="tdinput" style="width: 99%; height: 40px" id="LiveHouse"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            其他爱好
                        </td>
                        <td colspan="5" height="20" align="left" bgcolor="#FFFFFF" colspan="3">
                            <textarea name="txtLikes" class="tdinput" style="width: 99%; height: 40px" id="txtLikes"
                                rows="3" cols="80"></textarea>
                        </td>
                    </tr>
                </table>  
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>备注信息 </td>
                  <td align="right"><div id='search7'><img src="../../../images/Main/Open.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_07','search7')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_07" style="display:none"><!---->
          <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">建档人</td>
                <td height="20" bgcolor="#FFFFFF" style="width:23%">
                    <input name="txtCreator" id="txtCreator" type="text" class="tdinput" size="15" runat="server" disabled="disabled" /></td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">建档日期</td>
                <td height="20" bgcolor="#FFFFFF" style="width:23%">                    
                    <input id="txtCreatedDate" type="text" class="tdinput"  runat="server" disabled="disabled"/></td>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">&nbsp;</td>
                <td height="20" align="left" bgcolor="#FFFFFF" style="width:24%">
                    &nbsp;</td>
              </tr>
               <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:10%">备注</td>
                <td height="20" align="left" bgcolor="#FFFFFF" colspan="5">
                    <textarea name="txtRemark" id="txtRemark" rows="3" cols="80"></textarea></td>
              </tr>
              </table>     
              <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11" bgcolor="#F4F0ED" class="Blue"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>权限信息 </td>
                  <td align="right"><div id='search6'><img src="../../../images/Main/Open.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_06','search6')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_06" style="display:none"><!---->
               <tr>
                <td height="20" align="right" bgcolor="#E6E6E6" style="width:15%">可查看该联系人档案的人员</td>
                <td height="20" align="left" bgcolor="#FFFFFF">
                    <textarea  id="txtCanUserName" rows="3" readonly cols="80" style="width:90%" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                    <input type="hidden" id="txtCanUserID" /></td>
              </tr>
              </table>       
          <br />              
            </td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="txtRecorder" />
    <input type="hidden" runat="server" id="txtChairman" />
    <input type="hidden" runat="server" id="txtSender" />
    <uc2:Message ID="Message1" runat="server" />
    <span id="Forms" class="Spantype"></span>
    <input id="hCondition" type="hidden" />
    </form>
</body>
</html>
