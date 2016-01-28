<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeInfo_Add.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeInfo_Add" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/SysParamDrpControl.ascx" tagname="SysParam" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加人员</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/EmployeeInfo_Add.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadPhoto.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript">

</script>
    </head>
<body> 
<form id="frmMain" runat="server">
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
    <tr>
        <td height="30" align="center" colspan="2" class="Title"><div id="divTitle" runat="server">新建人员档案</div></td>
    </tr>
    <tr>
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="SaveEmployeeInfo();"/>                                    
                                   <img src="../../../Images/Button/btn_jxlr.jpg" runat="server" ID="btnCont" visible="false" alt="继续录入" style="cursor:hand" onclick="Continue();"/>

                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server"  visible="false"  alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" />                                    
                                </td>
                                <td align="right" class="tdColInput">
                                    <img src="../../../Images/Button/Main_btn_print.jpg" runat="server"  alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand"  />
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
                                <td>基本信息</td>
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
            <table>
                <tr>
                    <td colspan="2" height="4">
                        <input type="hidden" id="hidEditFlag" runat="server" />
                        <input type="hidden" id="hidFromPage" runat="server" />
                        <input type="hidden" id="hidWorkModuleID" runat="server" />
                        <input type="hidden" id="hidLeaveModuleID" runat="server" />
                        <input type="hidden" id="hidReserveModuleID" runat="server" />
                        <input type="hidden" id="hidInterviewModuleID" runat="server" />
                        <input type="hidden" id="hidWaitModuleID" runat="server" />
                        <input type="hidden" id="hidInitSysModuleID" runat="server" />
                        <input type="hidden" id="hidInitHumanModuleID" runat="server" />
                        <input type="hidden" id="hidSearchCondition" runat="server" />
                        <input type="hidden" id="hidSysteDate" runat="server" />
                        <input type="hidden" id="hidEmployeeID" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr>
                  <td height="20" align="right" class="tdColTitle">编号<span class="redbold">*</span></td>
                  <td class="tdColInput">
                    <div id="divCodeRule" runat="server" style="margin-top:2px;margin-left:2px;">
                        <uc1:CodingRule ID="codruleEmployNo" runat="server" />
                    </div>
                    <div id="divEmployeeNo" runat="server" class="tdinput" style="margin-top:2px;margin-left:2px;"></div>
                  </td>
                  <td align="right" class="tdColTitle"><div id="divNum">工号</div></td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtEmployeeNum" runat="server" Width="95%" MaxLength="20" CssClass="tdinput"></asp:TextBox>
                  </td>
                  <td colspan="2" rowspan="9" class="tdColInput" align="center">
                    <table>
                        <tr>
                            <td colspan="2">
                                <img id="imgPhoto" runat="server" src="~/Images/Pic/Pic_Nopic.jpg" height="145" width="200" />
                                <asp:HiddenField ID="hfPhotoUrl" runat="server" />
                                <asp:HiddenField ID="hfPagePhotoUrl" runat="server" />
                                <input type="hidden" id="uploadKind" />
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
                  <td height="20" align="right" class="tdColTitle">姓名<span class="redbold">*</span></td>
                  <td class="tdColInput">
                    <input type="text" maxlength="25" class="tdinput" id="txtEmployeeName" style="width:95%" onblur="GetPYShort();"  specialworkcheck="姓名"  runat="server" />
                  </td>
                  <td align="right" class="tdColTitle" >拼音缩写</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtPYShort" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">曾用名</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtUsedName" runat="server" Width="95%" MaxLength="25" CssClass="tdinput"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >英文名</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtNameEn" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle" >人员分类<span class="redbold">*</span></td>
                  <td class="tdColInput" >
                    <select id="ddlFlag" runat="server" style="margin-top:2px;margin-left:2px;">
                        <option value="1">在职人员</option>
                        <option value="2" selected="selected">人才储备</option>
                        <option value="3">离职人员</option>
                    </select>
                  </td>
                  <td align="right" class="tdColTitle">证件类型</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtDocuType" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                                    </td>
                </tr>
                <tr runat="server">
                  <td height="25" align="right" class="tdColTitle">
                      证件号码</td>
                  <td class="tdColInput" valign="middle">
                    
                    <asp:TextBox ID="txtCardID" runat="server" MaxLength="18" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >社保卡号</td>
                  <td class="tdColInput" >                      
                    <asp:TextBox ID="txtSafeguardCard" runat="server" MaxLength="100" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                </tr>
                
                <tr runat="server">
                  <td height="25" align="right" class="tdColTitle">
                    <div id="divJobTitle" runat="server">应聘职务</div>
                  </td>
                  <td class="tdColInput" valign="middle">
                    
                    <div id="divPositionTitle" runat="server">
                        <asp:TextBox ID="txtPositionTitle" runat="server" MaxLength="100" CssClass="tdinput" Enabled="True"></asp:TextBox>
                    </div>
                  </td>
                  <td align="right" class="tdColTitle" >岗位</td>
                  <td class="tdColInput" >  <asp:DropDownList ID="ddlQuarter_ddlCodeType" runat="server"></asp:DropDownList>                   
                  </td>
                </tr>
                
                <tr id="tr1" >
                  <td height="25" align="right" class="tdColTitle">
                     职称</td>
                  <td class="tdColInput" valign="middle">
                                 <uc1:CodeType ID="ddlPosition" runat="server" /> </td>
                  <td align="right" class="tdColTitle" >岗位职等</td>
                  <td class="tdColInput" >
                      <uc1:CodeType ID="ddlAdminLevelID" runat="server" />
                                    
                                    </td>
                </tr>
                <tr  id="tr2" >
                  <td height="25" align="right" class="tdColTitle">
                      所在部门</td>
                  <td class="tdColInput" valign="middle">
                      <input id="DeptName" runat="server" type="text" class="tdinput" onclick="alertdiv('DeptName,hdDeptID')" />
                      <input id="hdDeptID" type="hidden" runat="server" /> </td>
                  <td align="right" class="tdColTitle" >入职时间</td>
                  <td class="tdColInput" >
                    <input ID="txtEnterDate" readonly Class="tdinput" runat="server"
                          onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})" /></td>
                </tr>
                <tr>
                  <td height="25" align="right" class="tdColTitle">性别<span class="redbold">*</span></td>
                  <td class="tdColInput" >
                    <asp:DropDownList ID="ddlSex" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="男" Value="1"></asp:ListItem>
                        <asp:ListItem Text="女" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td align="right" class="tdColTitle" >出生日期</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtBirth" runat="server" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtBirth')})"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle" width="10%">联系电话</td>
                  <td class="tdColInput" width="23%">
                    <asp:TextBox ID="txtTelephone" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" width="10%">手机号码</td>
                  <td class="tdColInput" width="23%">
                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" width="10%">电子邮件</td>
                  <td class="tdColInput" width="24%">
                    <asp:TextBox ID="txtEMail" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">其他联系方式</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtOtherContact" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >籍贯</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtOrigin" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >婚姻状况</td>
                  <td class="tdColInput">
                    <uc1:CodeType ID="ddlMarriage" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">学历</td>
                  <td class="tdColInput" >
                    <uc1:CodeType ID="ddlCulture" runat="server" />
                  </td>
                  <td align="right" class="tdColTitle" >毕业院校</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtSchool" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >专业</td>
                  <td class="tdColInput">
                    <uc1:CodeType ID="ddlProfessional" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td height="25" align="right" class="tdColTitle">政治面貌</td>
                  <td class="tdColInput" >
                    <uc1:CodeType ID="ddlLandscape" runat="server" />
                  </td>
                  <td align="right" class="tdColTitle" >宗教信仰</td>
                  <td class="tdColInput" >
                    <uc1:CodeType ID="ddlReligion" runat="server" />
                  </td>
                  <td align="right" class="tdColTitle" >民族</td>
                  <td class="tdColInput">
                    <uc1:CodeType ID="ddlNational" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td height="25" align="right" class="tdColTitle">户口</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtAccount" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >户口性质</td>
                  <td class="tdColInput" >
                    <asp:DropDownList ID="ddlAccountNature" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                        <asp:ListItem Text="非农业" Value="1"></asp:ListItem>
                        <asp:ListItem Text="农业" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td align="right" class="tdColTitle" >国家地区</td>
                  <td class="tdColInput">
                    <uc1:CodeType ID="ddlCountry" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">身高(厘米)</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtHeight" runat="server" MaxLength="6" CssClass="tdinput"></asp:TextBox>CM
                  </td>
                  <td align="right" class="tdColTitle" >体重</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtWeight" runat="server" MaxLength="7" CssClass="tdinput"></asp:TextBox>KG
                  </td>
                  <td align="right" class="tdColTitle" >视力</td>
                  <td class="tdColInput">
                    <asp:TextBox ID="txtSight" runat="server" MaxLength="5" CssClass="tdinput"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">最高学位</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtDegree" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >健康状况</td>
                  <td class="tdColInput">
                    <asp:DropDownList ID="ddlHealth" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                        <asp:ListItem Text="一般" Value="0"></asp:ListItem>
                        <asp:ListItem Text="良好" Value="1"></asp:ListItem>
                        <asp:ListItem Text="很好" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                  <td align="right" class="tdColTitle" >特长</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtFeatures" runat="server" MaxLength="100" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">计算机水平</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtComputerLevel" runat="server" MaxLength="25" CssClass="tdinput"  Width="95%"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >参加工作时间</td>
                  <td class="tdColInput">
                    <asp:TextBox ID="txtWorkTime" runat="server" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtWorkTime')})"></asp:TextBox>
                  </td>
                  <td align="right" class="tdColTitle" >家庭住址</td>
                  <td class="tdColInput" >
                    
                    <asp:TextBox ID="txtHomeAddress" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    
                  </td>
                </tr>
                <tr>
                  <td height="25" align="right" class="tdColTitle">外语语种1</td>
                  <td class="tdColInput" >
                    <uc1:CodeType ID="ddlLanguage1" runat="server" />
                  </td>
                  <td align="right" class="tdColTitle" >外语语种2</td>
                  <td class="tdColInput" >
                    <uc1:CodeType ID="ddlLanguage2" runat="server" />
                  </td>
                  <td align="right" class="tdColTitle" >外语语种3</td>
                  <td class="tdColInput">
                    <uc1:CodeType ID="ddlLanguage3" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td height="25" align="right" class="tdColTitle">外语水平1</td>
                  <td class="tdColInput" >
                    <asp:DropDownList ID="ddlLanguageLevel1" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                        <asp:ListItem Text="一般" Value="1"></asp:ListItem>
                        <asp:ListItem Text="熟练" Value="2"></asp:ListItem>
                        <asp:ListItem Text="精通" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td align="right" class="tdColTitle" >外语水平2</td>
                  <td class="tdColInput" >
                    <asp:DropDownList ID="ddlLanguageLevel2" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                        <asp:ListItem Text="一般" Value="1"></asp:ListItem>
                        <asp:ListItem Text="熟练" Value="2"></asp:ListItem>
                        <asp:ListItem Text="精通" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td align="right" class="tdColTitle" >外语水平3</td>
                  <td class="tdColInput" >
                    <asp:DropDownList ID="ddlLanguageLevel3" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                        <asp:ListItem Text="一般" Value="1"></asp:ListItem>
                        <asp:ListItem Text="熟练" Value="2"></asp:ListItem>
                        <asp:ListItem Text="精通" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="tdColTitle">简历</td>
                  <td class="tdColInput">
                    <table>
                        <tr>
                            <td>
                                <div id="divUploadResume" runat="server">
                                    <a href="#" onclick="DealResume('upload');">上传附件</a>
                                </div>
                                <div id="divDealResume" runat="server" style="display:none;">
                                    <a href="#" onclick="DealResume('download');">
                                            <span id='spanAttachmentName' runat="server"></span>
                                        </a> &nbsp;
                                    <a href="#" onclick="DealResume('clear');">删除附件</a>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfResume" runat="server" />
                    <asp:HiddenField ID="hfPageResume" runat="server" />
                  </td>
                  <td align="right" class="tdColTitle">专业描述</td>
                  <td class="tdColInput" colspan="3" > <asp:TextBox ID="txtProfessionalDes" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                    
                  </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>  
    <tr>
        <td valign="top" ><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
        <td align="right" valign="top"><img src="../../../images/Main/LineR.jpg" width="122" height="7" /></td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>  
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">工作经历</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divWorkHistory'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblWorkInfo','divWorkHistory')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblWorkInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" class="tdColInput">
                        <img src="../../../images/Button/Show_add.jpg" alt="添加" runat="server" visible="false" style="cursor:hand" onclick="AddWorkHistory();" id="btnAdda" />
                        <img src="../../../images/Button/Show_del.jpg" alt="删除" runat="server" visible="false" style="cursor:hand" onclick="DeleteRows('tblWorkHistory');" id="btnDela" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divWorkInfo" runat="server"></div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr>  
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">教育经历</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divStudyHistory'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblStudyInfo','divStudyHistory')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr> 
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblStudyInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" class="tdColInput">
                        <img src="../../../images/Button/Show_add.jpg" alt="添加" runat="server" visible="false" style="cursor:hand" onclick="AddStudyHistory();" id="btnAddb" />
                        <img src="../../../images/Button/Show_del.jpg" alt="删除" runat="server" visible="false" style="cursor:hand" onclick="DeleteRows('tblStudyHistory');"  id="btnDelb" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divStudyInfo" runat="server"></div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr> 
    <tr>
        <td height="25" valign="top" colspan="2">
            <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                <tr>
                    <td height="25" valign="top" >
                        <span class="Blue">技能及证照信息</span>
                    </td>
                    <td align="right" valign="top">
                        <div id='divSkill'>
                            <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSkillInfo','divSkill')"/>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="99%" border="0" id="tblSkillInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="28" class="tdColInput">
                        <img src="../../../images/Button/Show_add.jpg" alt="添加" runat="server" visible="false" style="cursor:hand" onclick="AddSkill();" id="btnAddc" />
                        <img src="../../../images/Button/Show_del.jpg" alt="删除" runat="server" visible="false" style="cursor:hand" onclick="DeleteRows('tblSkill');" id="btnDelc" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divSkillInfo" runat="server"></div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td colspan="2" height="10"></td></tr> 
    <tr>
        <td colspan="2">
        <div id="divContract" runat="server" visible="false">
            <table  width="100%" border="0" cellpadding="0" cellspacing="0" id="tblContract">
                <tr>
                    <td height="25" valign="top" colspan="2">
                        <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                            <tr>
                                <td height="25" valign="top" >
                                    <span class="Blue">合同信息</span>
                                </td>
                                <td align="right" valign="top">
                                    <div id='divContractInfo'>
                                        <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblContractInfo','divContractInfo')"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="99%" border="0" id="tblContractInfo" style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                            <tr>
                                <td>
                                    <table  width='100%' border='0' id='tblEmployeeContract'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>
                                        <tr>
                                            <td class='ListTitle'>合同号</td>
                                            <td class='ListTitle'>合同名称</td>
                                            <td class='ListTitle'>合同类型</td>
                                            <td class='ListTitle'>合同属性</td>
                                            <td class='ListTitle'>签约时间</td>
                                            <td class='ListTitle'>生效时间</td>
                                            <td class='ListTitle'>状态</td>
                                        </tr>
                                        <asp:Repeater ID="rpEmployeeContract" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "ContractNo")%></td>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "ContractName")%></td>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "ContractType")%></td>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "ContractProperty")%></td>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "SigningDate")%></td>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "StartDate")%></td>
                                                    <td class='tdColInputCenter'><%#DataBinder.Eval(Container.DataItem, "ContractStatus")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table><tr><td height="10"></td></tr></table>
        </div>
        </td>
    </tr>
</table>
<!-- </div> -->
</td>
</tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
</form>
</body>
</html>
